import keras

import numpy as np
import pandas as pd
import matplotlib.pyplot as pyplot

# TensorFlow
# Sci-Kit
# Anaconda
# Keras


def load_training_data() -> ():
    return pd.read_csv("IowaHousingPrices.csv")


def train_keras_model(training_data: ()):
    sq_ft, price = training_data

    model = keras.Sequential()
    model.add(keras.layers.Dense(1, input_shape=(1,)))
    model.compile(keras.optimizers.Adam(lr=1), 'mean_squared_error')
    model.fit(sq_ft, price, epochs=30, batch_size=30)

    return model


if __name__ == '__main__':
    data = load_training_data()
    model = train_keras_model(
        (data[['SquareFeet']].values,
         data[['SalePrice']].values))

    sq_ft = data[['SquareFeet']].values
    data.plot(kind='scatter',
              x="SquareFeet",
              y="SalePrice",
              title="Housing Prices")
    y_pred = model.predict(sq_ft)
    pyplot.plot(sq_ft, y_pred, color='red')
    pyplot.show()
