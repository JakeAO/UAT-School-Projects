from dateutil import parser as date_parser
from house_pricer import HousePricer


class ApiLibrarySetups:
    __stateAbbreviations: [] = [
        'AL', 'AK', 'AS', 'AZ', 'AR', 'CA', 'CO', 'CT', 'DE', 'DC', 'FM', 'FL', 'GA',
        'GU', 'HI', 'ID', 'IL', 'IN', 'IA', 'KS', 'KY', 'LA', 'ME', 'MH', 'MD', 'MA',
        'MI', 'MN', 'MS', 'MO', 'MT', 'NE', 'NV', 'NH', 'NJ', 'NM', 'NY', 'NC', 'ND',
        'MP', 'OH', 'OK', 'OR', 'PW', 'PA', 'PR', 'RI', 'SC', 'SD', 'TN', 'TX', 'UT',
        'VT', 'VI', 'VA', 'WA', 'WV', 'WI', 'WY'
    ]

    __header: str = "############################################\n" \
                    "# Zillow US Home Pricing Estimator\n" \
                    "#\n" \
                    "# Uses historical data from Zillow from\n" \
                    "# the past 10+ years to predict home prices\n" \
                    "# based on location and number of bedrooms.\n" \
                    "############################################"
    __prompt_main: str = "Use the following options to predict home prices either historically or in the future.\n" \
                         "[P] Predict housing price\n" \
                         "[X] Exit the program"
    __prompt_exit: str = "\n\nThank you for using Zillow Home Pricing Estimator!"

    __pricers: {} = dict()

    # main run method, repeatedly prompts user for options until exit is chosen
    def run(self) -> None:
        print(self.__header)

        while True:
            print(self.__prompt_main)
            option: str = input(">> Option: ")
            if option == 'x' or option == 'X':
                break
            elif option == 'p' or option == 'P':
                print("\n")
                self.__run_prediction()
            else:
                print(f"\n\n\"{option}\" is not a valid option, please try again...\n\n")

        print(self.__prompt_exit)

    # run the housing price prediction logic, relying heavily on house_pricer.py
    def __run_prediction(self) -> None:
        print("Please enter the following details about the house you would like to predict the value of.\n"
              "State: 2 characters (e.g. AZ, WY, etc.)\n"
              "City: full name (e.g. Tempe, Cheyenne, etc.)\n"
              "Beds: number of bedrooms (e.g. 2, 3, etc.\n"
              "Date: date of the prediction (e.g. 1/15/2021, etc.)")
        state = input(">> State: ")
        city = input(">> City: ")
        beds = input(">> Beds: ")
        pred_date = input(">> Date: ")

        # validate all inputs
        if state not in self.__stateAbbreviations:
            print(f"\n\n\"{state}\" is not a known state code, please try again...\n\n")
            return
        if city is None:
            print(f"\n\n\"{city}\" is not a known city, please try again...\n\n")
            return
        try:
            beds = int(beds)
        except ValueError:
            print(f"\n\n\"{beds}\" is not a valid bed number, please try again...\n\n")
            return
        try:
            pred_date = date_parser.parse(pred_date)
        except ValueError:
            print(f"\n\n\"{pred_date}\" is not a valid date, please try again...\n\n")
            return

        # find or create the pricer
        pricer = self.__pricers.get(state)
        if pricer is None:
            self.__pricers[state] = pricer = HousePricer(state)

        print("\n")

        # ask pricer for prediction
        prediction = pricer.predict_house_price(city, beds, pred_date)
        if prediction is None:
            print(f"\n\nPrediction failed due to invalid parameter(s), please try again...\n\n")
            return

        print(f"\n\n[Price Prediction Results]\n"
              f"{beds} bedroom home in {city}, {state}.\n"
              f"Predicting price on {pred_date}.\n"
              f"Prediction: ${prediction:0,.0f}\n\n")
