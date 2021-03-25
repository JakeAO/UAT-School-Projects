from textblobextensions import TextBlobExtensions
import enum
import inputrole
import outputrole


class Project2:
    # codifies all possible menu options of easier reading
    class MenuOption(enum.Enum):
        Invalid = 0
        ClearInput = 1
        ClearOutput = 2
        GetWordCount = 3
        GetSentenceCount = 4
        GetDefinitions = 5
        GetSentiment = 6
        GetPartsOfSpeech = 7
        PrintBlob = 8
        Exit = -1

    __input: inputrole.InputRole = None
    __output: outputrole.OutputRole = None

    # maps text input from the user to MenuOptions
    __option_menu_switcher: {} = {
        'p': MenuOption.PrintBlob,
        'P': MenuOption.PrintBlob,
        '1': MenuOption.GetWordCount,
        '2': MenuOption.GetSentenceCount,
        '3': MenuOption.GetDefinitions,
        '4': MenuOption.GetSentiment,
        '5': MenuOption.GetPartsOfSpeech,
        'CI': MenuOption.ClearInput,
        'ci': MenuOption.ClearInput,
        'CO': MenuOption.ClearOutput,
        'co': MenuOption.ClearOutput,
        'X': MenuOption.Exit,
        'x': MenuOption.Exit,
    }

    # print the content of the current text input
    def __do_print_text(self) -> None:
        text = self.__input.read()

        self.__output.write("===> This is the currently loaded blob...")
        self.__output.write("    " + str(text))

    # clear the current input's cached value
    def __do_clear_input_cache(self) -> None:
        self.__output.write("===> Clearing the current input cache...")
        self.__input.clear_cache()

    # clear the current input role
    def __do_clear_input(self) -> None:
        self.__output.write("===> Clearing the current input role...")
        self.__input = None

    # clear the current output role
    def __do_clear_output(self) -> None:
        self.__output.write("===> Clearing the current output role...")
        self.__output = None

    # calculate and print the word count
    def __do_word_count(self) -> None:
        text = self.__input.read()

        word_count_tuple = TextBlobExtensions.get_word_count(text)
        self.__output.write("===> Word Count")
        self.__output.write("   Count: " + str(word_count_tuple[0]) + " (" + str(word_count_tuple[1]) + " unique)")

    # calculate and print the sentence count
    def __do_sentence_count(self) -> None:
        text = self.__input.read()

        sentence_count = TextBlobExtensions.get_sentence_count(text)
        self.__output.write("===> Sentence Count")
        self.__output.write("   Count: " + str(sentence_count))

    # calculate and print the word definitions
    def __do_define(self) -> None:
        text = self.__input.read()

        def_map = TextBlobExtensions.get_definition_map(text)
        self.__output.write("===> Word Definitions")
        for word in def_map.keys():
            self.__output.write("   \"" + word + "\"")
            for definition in def_map[word]:
                self.__output.write("      - " + definition)

    # calculate and print the sentiment
    def __do_sentiment_analysis(self) -> None:
        text = self.__input.read()

        sentiment = TextBlobExtensions.get_sentiment(text)
        self.__output.write("===> Sentiment Analysis")
        self.__output.write("   Polarity: " + sentiment[0].name)
        self.__output.write("   Subjectivity: " + sentiment[1].name)

    # calculate and print the parts of speech
    def __do_parts_of_speech(self) -> None:
        text = self.__input.read()

        pos_map = TextBlobExtensions.get_part_of_speech_map(text)
        self.__output.write("===> Parts of Speech")
        for part_of_speech in pos_map:
            self.__output.write("   Part of Speech: " + part_of_speech)
            self.__output.write("       " + str(pos_map.get(part_of_speech)))

    # maps MenuOptions to their corresponding methods
    __menu_option_switcher: {} = {
        MenuOption.Invalid: None,
        MenuOption.PrintBlob: __do_print_text,
        MenuOption.ClearInput: __do_clear_input,
        MenuOption.ClearOutput: __do_clear_output,
        MenuOption.GetWordCount: __do_word_count,
        MenuOption.GetSentenceCount: __do_sentence_count,
        MenuOption.GetDefinitions: __do_define,
        MenuOption.GetSentiment: __do_sentiment_analysis,
        MenuOption.GetPartsOfSpeech: __do_parts_of_speech,
    }

    # print out the menu options, wait for user input, and validate it
    def __print_input_menu(self) -> None:
        print("   Input role is not satisfied, please select one to continue...")
        print("   [C] Read from Console: Load text manually from the console.")
        print("   [F] Read from File: Load text from a provided file path.")

        choice = input("Option: ")
        if choice == "C" or choice == "c":
            self.__input = inputrole.ConsoleInput.create()
        elif choice == "F" or choice == "f":
            self.__input = inputrole.FileInput.create()

        print("")

    # print out the menu options, wait for user input, and validate it
    def __print_output_menu(self) -> None:
        print("   Output role is not satisfied, please select one to continue...")
        print("   [C] Write to Console: Print text to the console.")
        print("   [F] Write to File: Print text to a provided file path.")

        choice = input("Option: ")
        if choice == "C" or choice == "c":
            self.__output = outputrole.ConsoleOutput.create()
        elif choice == "F" or choice == "f":
            self.__output = outputrole.FileOutput.create()

        print("")

    # print out the menu options, wait for user input, and validate it
    def __print_option_menu(self) -> MenuOption:
        print("   Enter one of the following commands to continue...")
        print("   [P] Print: Prints the currently loaded blob to the command line.")
        print("   [1] Word Count: Counts the number of words in a block of text or a file.")
        print("   [2] Sentence Count: Counts the number of sentences in a block of text or a file.")
        print("   [3] Define: Defines each word in a block of text or a file.")
        print("   [4] Sentiment Analysis: Finds the polarity and subjectivity of a block of text or a file.")
        print("   [5] Parts of Speech: Groups all words in a block of text or a file by their part of speech.")
        print("   [CI] Clear Input: Clear the current input role.")
        print("   [CO] Clear Output: Clear the current output role.")
        print("   [X] Exit: Close the program.")

        choice = input("Option: ")
        option = self.__option_menu_switcher.get(choice, self.MenuOption.Invalid)

        if option == self.MenuOption.Invalid:
            print("Invalid option " + choice + " selected, please try again.")

        print("")
        return option

    def run(self) -> None:
        print("#### TextBlob Testing Tool #####")
        selected_option = self.MenuOption.Invalid

        # loop until user selects an option that resolves to Exit
        while selected_option != self.MenuOption.Exit:
            # check that the InputRole is satisfied
            if self.__input is None:
                self.__print_input_menu()
                continue

            # check that the OutputRole is satisfied
            if self.__output is None:
                self.__print_output_menu()
                continue

            # prompt user for option, then validate it and execute
            selected_option = self.__print_option_menu()
            menu_func = self.__menu_option_switcher.get(selected_option)
            if menu_func is not None:
                menu_func(self)

            print("")
        else:
            print("Have a nice day!")
