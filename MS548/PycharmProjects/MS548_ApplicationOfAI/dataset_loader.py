import pandas as pd
import numpy as np
import zipcodes
import math

#################################################################
# Datasets are csv files denoting the location (city + state)
# and median home values for each month from January 2010 through
# early 2021. Each csv is broken up based on the size of home
# (number of bedrooms).
#################################################################


class DatasetLoader:
    __file_paths: [] = [
        "ZillowData/City_Zhvi_1bedroom.csv",
        "ZillowData/City_Zhvi_2bedroom.csv",
        "ZillowData/City_Zhvi_3bedroom.csv",
        "ZillowData/City_Zhvi_4bedroom.csv",
        "ZillowData/City_Zhvi_5BedroomOrMore.csv"
    ]

    @staticmethod
    def __get_coords(city: str, state: str) -> ():
        # pull the city+state pair's zip code entry
        zip_entry = next((z for z in zipcodes.filter_by(city=city, state=state)
                          if z['active']
                          and z['zip_code_type'] == 'STANDARD'), None)

        # confirm we can find a zip code entry
        if zip_entry is None:
            return None

        # confirm the zip entry has latitude/longitude components
        lat = zip_entry['lat']
        long = zip_entry['long']
        if lat is None or long is None:
            return None

        # confirm the latitude/longitude components are valid, knowable floats
        lat = float(lat)
        long = float(long)
        if math.isnan(lat) or math.isinf(lat) or math.isnan(long) or math.isinf(long):
            return None

        return lat, long

    # pull the combined dataset from csv for the provided state
    def get_dataset(self, state_id: str) -> ([], []):
        all_data = []

        for file_idx in range(len(self.__file_paths)):
            file_name = self.__file_paths[file_idx]
            bedroom_count = file_idx + 1

            dataset = pd.read_csv(file_name)
            columns = dataset.columns.values
            rows = dataset.values

            for row in rows:
                city = row[0]
                state = row[1]

                # confirm the state matches the state we're looking for
                if state != state_id:
                    continue

                # pull the city+state pair's coordinates
                coords = self.__get_coords(city, state)
                if coords is None:
                    continue

                # unpack the coordinates into individual floats
                lat = coords[0]
                long = coords[1]

                # loop over each month column to get the prices in that month
                for colIdx in range(len(columns)-1, len(columns)):
                    # pull out the price, and confirm it's a valid value as to not pollute the model
                    price = float(row[colIdx])
                    if math.isnan(price) or math.isinf(price):
                        continue

                    # pull out the month/year, and confirm they're valid as to not pollute the model
                    month, _, year = columns[colIdx].split('/')
                    month = int(month)
                    year = int(year)
                    if math.isnan(month) or math.isinf(month) or math.isnan(year) or math.isinf(year):
                        continue

                    all_data.append(((lat, long, bedroom_count), price))

        # shuffle up the data, random data is good data
        np.random.shuffle(all_data)

        # unpack the array into 'x' and 'y' components
        return [entry[0] for entry in all_data], [entry[1] for entry in all_data]
