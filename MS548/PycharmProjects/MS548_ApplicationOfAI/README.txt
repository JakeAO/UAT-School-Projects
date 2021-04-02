MS548 Project 4 (aka 'Application of AI')

Python app which uses TensorFlow/Keras to attempt to predict
housing prices based on their location, number of bedrooms,
and their purchase year. Uses a smattering of other libraries
for calculations and display:
    ZipCodes -> Used to calculate the latitude/longitude of
        houses so that they can be used in the NN model.
    Pandas -> Used to load the ZillowData csv files.
    MatPlotLib -> Used to render the error vs epoch charts.

main.py
    Just the main launching point of the project.
program.py
    The meat of the program. Handles printing the menus to the
    console, handling input, and the overall processing.
dataset_loader.py
    Handles the loading and validation of model training data
    from the ZillowData folder.
house_pricer.py
    Handles the model training and value prediction using
    neural network. Saves/loads the trained model to the
    "model_save_[state]" folder in order to avoid having
    to retrain the model each time. Also outputs an chart
    of the error % across each epoch of training.