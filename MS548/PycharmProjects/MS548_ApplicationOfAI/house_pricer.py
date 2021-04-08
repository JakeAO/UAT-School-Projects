import zipcodes

import pandas as pd

from os import path
from datetime import date
from matplotlib import pyplot
from dataset_loader import DatasetLoader
from tensorflow.keras.losses import MeanAbsolutePercentageError, MeanSquaredError
from tensorflow.keras.models import Sequential
from tensorflow.keras.optimizers import Adam
from tensorflow.keras.layers import InputLayer, Dense, Dropout
from tensorflow.keras.activations import relu, linear


class HousePricer:
    __dataset_loader: DatasetLoader = DatasetLoader()
    __state: str = None
    __trained: bool = False
    __model_save_path: str = None
    __model: Sequential = Sequential([
        InputLayer(input_shape=3),
        Dense(2, activation=relu),
        Dense(1, activation=relu)
    ])

    def __init__(self, state: str) -> None:
        self.__state = state
        self.__model_save_path = f"model_save_{state}"

    # get a housing price prediction based on the trained model and the input values
    def predict_house_price(self, city: str, beds: int) -> float:
        self.__ensure_model()
        zip_entry = next((z for z in zipcodes.filter_by(city=city, state=self.__state)
                          if z['active']
                          and z['zip_code_type'] == 'STANDARD'), None)
        if zip_entry is None:
            return None
        return self.__predict_house_price(float(zip_entry['lat']),
                                          float(zip_entry['long']),
                                          beds)

    # get a housing price prediction based on the trained model and the input values
    def __predict_house_price(self, lat: float, long: float, beds: int) -> float:
        input_data = (lat, long, beds)
        output_data = self.__model.predict([input_data])
        return output_data[0][0]
        pass

    # guarantee that we have a model trained before making any predictions
    def __ensure_model(self) -> None:
        if self.__trained:
            return
        if path.exists(self.__model_save_path):
            self.__model.load_weights(self.__model_save_path)
            self.__trained = True
        else:
            self.__trained = self.__train_model()

    # train the model based on the pricer's state
    def __train_model(self) -> bool:
        print(f"There is no trained neural network model for {self.__state}.")
        print("Training the model will take some time, press X if you'd like to "
              "cancel the training process. Any other key will commence training.")
        option = input(">> Choice: ")
        if option == 'x' or option == 'X':
            return False

        print("\nWould you like to observe the fitting process? (Y/N)")
        option = input(">> Choice: ")
        verbosity = int((option == 'Y' or option == 'y'))

        print("\nCompiling neural network model...")
        self.__model.compile(
            # using MeanAbsolutePercentageError because it's useful for the end user to know
            loss=MeanAbsolutePercentageError(),
            # using Adam as a good all-around optimizer
            optimizer=Adam())

        while True:
            print(f"Loading Zillow housing data for {self.__state}...")
            x_data, y_data = self.__dataset_loader.get_dataset(self.__state)

            # we're using 90% for training and 10% for testing
            test_amount = int(len(x_data) * .10)
            x_train = x_data[:test_amount]
            y_train = y_data[:test_amount]
            x_test = x_data[-test_amount:]
            y_test = y_data[-test_amount:]

            print("Evaluating untrained model for comparison...")
            old_error_percent = self.__model.evaluate(x_test, y_test)
            old_error_percent = round(old_error_percent, 2)
            print(f"Untrained error rating: {old_error_percent}%")

            print("Fitting model...")
            history = self.__model.fit(x_train, y_train,
                                       epochs=100,
                                       batch_size=16,
                                       verbose=verbosity,
                                       validation_data=(x_test, y_test))
            print("Fitting completed!")

            print("Evaluating trained model for comparison...")
            new_error_percent = self.__model.evaluate(x_test, y_test)
            new_error_percent = round(new_error_percent, 2)
            print(f"Trained error rating: {new_error_percent}% (Original: {old_error_percent})")

            self.__print_training_history(history)

            if new_error_percent > 30:
                print(f"\n\n{new_error_percent}% error is pretty bad, would you like to retrain the model? (Y/N)")
                option = input(">> Choice: ")
                if option == 'Y' or option == 'y':
                    print()
                    continue

            break

        self.__model.save(self.__model_save_path)

        return True

    # print the training history's error rate over time (epoch)
    def __print_training_history(self, history) -> None:
        history = pd.DataFrame(history.history)

        pyplot.figure()
        pyplot.suptitle("Error Rate v Training Epoch")
        pyplot.title(f"Housing Prices in {self.__state}")
        pyplot.xlabel("Epoch #")
        pyplot.ylabel("Error %")
        pyplot.plot(history.loss)
        pyplot.show()
