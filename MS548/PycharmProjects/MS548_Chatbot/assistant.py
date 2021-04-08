import speech_recognition as sr
import pyttsx3 as tts
import enum as enum
import chatterbot as cb
import chatterbot.trainers as cb_trainers
import chatterbot.comparisons as cb_comps
import chatterbot.conversation as cb_convo


class CommandCategory(enum.Enum):
    conversation: int = 0
    change_input: int = 1
    change_output: int = 2
    exit_program: int = 3


class InputType(enum.Enum):
    text: int = 0
    speech_prompted: int = 1
    speech_dynamic: int = 2


class OutputType(enum.Enum):
    text: int = 0
    speech: int = 1


class Assistant:
    __header: str = "############################################\n" \
                    "# JJ McChatbot\n" \
                    "#\n" \
                    "# Welcome to JJ McChatbot, a simple chatbot\n" \
                    "# which can accept either verbal or textual\n" \
                    "# commands and perform various services,\n" \
                    "# such as defining terms or looking up info.\n" \
                    "############################################"
    __prompt_help: str = "JJ McChatbot can reply to simple requests, " \
                         "or attempt to hold a conversation. Try asking " \
                         "\"what time is it\" to get the current time or " \
                         "\"who is Jules Verne\" to search Wikipedia. Say " \
                         "\"exit\", \"change input\", or \"change output\" " \
                         "at any prompt to leave the program or adjust settings."
    __prompt_exit: str = "Thank you for using JJ McChatbot!"
    __prompt_error: str = "I'm sorry, but I've encountered an error during that request."

    __input_type: InputType = InputType.text
    __output_type: OutputType = OutputType.text

    __chatbot: cb.ChatBot = cb.ChatBot('JJ',
                                       storage_adapter='chatterbot.storage.SQLStorageAdapter',
                                       preprocessors=[
                                         'chatterbot.preprocessors.clean_whitespace',
                                         'chatterbot.preprocessors.unescape_html',
                                         'chatterbot.preprocessors.convert_to_ascii'
                                       ],
                                       logic_adapters=[
                                           {
                                               'import_path': 'specific_response_fixed.SpecificResponseAdapterFixed',
                                               'input_text': 'help',
                                               'output_text': __prompt_help
                                           },
                                           {
                                               'import_path': 'time_adapter_fixed.TimeLogicAdapterFixed'
                                           },
                                           {
                                               'import_path': 'chatterbot.logic.BestMatch',
                                               'default_response': "I'm sorry, I don't quite understand.",
                                               'statement_comparison_function': cb_comps.SynsetDistance,
                                           },
                                           {
                                               'import_path': 'chatterbot.logic.UnitConversion'
                                           },
                                           {
                                               'import_path': 'chatterbot.logic.MathematicalEvaluation'
                                           },
                                           {
                                               'import_path': 'wiki_logic_adapter.WikiLogicAdapter'
                                           }
                                       ])
    __listener: sr.Recognizer = sr.Recognizer()
    __ttsEngine: tts.Engine = tts.init(debug=True)

    # constructor for the assistant, accepts boolean argument on whether to retrain or not
    def __init__(self, retrain: bool = False) -> None:
        def gather_and_train_from_cleaned_data(path: str, chatbot: cb.ChatBot) -> None:
            training_data = []
            with open(path, mode='rt', encoding='utf-8', errors='ignore') as file:
                for line in file:
                    line = line.strip('\n\r\t')
                    training_data.append(line)
            list_trainer = cb_trainers.ListTrainer(chatbot)
            list_trainer.train(training_data)

        if retrain:
            # train with default corpus
            cb_trainers.ChatterBotCorpusTrainer(self.__chatbot).train("chatterbot.corpus.english")
            # train with Q&A data
            gather_and_train_from_cleaned_data('TrainingData/QuestionAnswer/data.txt', self.__chatbot)

    # prompt user for text input in the console
    def __input_from_text(self) -> str:
        return input("Command << ").lower().replace('jj', '')

    # prompt user to press the enter key, then listen for audio input and return the result
    def __input_from_speech_prompted(self) -> str:
        try:
            input("Press [ENTER] or [RETURN] to begin listening. << ")
            with sr.Microphone() as source:
                print("Listening...")
                self.__listener.adjust_for_ambient_noise(source)
                input_value = self.__listener.listen(source)
                input_value = self.__listener.recognize_google(input_value)
                input_value = input_value.lower()
                input_value = input_value.replace('hey jj', '')
                input_value = input_value.strip()
                return input_value
        except sr.UnknownValueError:
            pass  # swallow errors :(
        finally:
            print("Stopped listening.")
        return None

    # automatically begin listening to the user until a 'Hey JJ' command is detected
    def __input_from_speech_dynamic(self) -> str:
        try:
            with sr.Microphone() as source:
                self.__listener.adjust_for_ambient_noise(source)
                print("Listening...")
                while True:
                    input_value = self.__listener.listen(source)
                    input_value = self.__listener.recognize_google(input_value)
                    input_value = input_value.lower()
                    if input_value.startswith('hey jj'):
                        input_value = input_value.replace('hey jj', '')
                        input_value = input_value.strip()
                        return input_value
        except sr.UnknownValueError:
            pass  # swallow errors :(
        finally:
            print("Stopped listening.")
        return None

    # print the output value to the console
    def __output_to_text(self, value: str) -> None:
        if value is not None:
            print(f"JJ >> \"{value}\"\n")

    # speak the output value using tts library
    def __output_to_speech(self, value: str) -> None:
        if value is not None:
            self.__ttsEngine.say(value)
            self.__ttsEngine.runAndWait()

    __input_actions: {} = dict({
        InputType.text: __input_from_text,
        InputType.speech_prompted: __input_from_speech_prompted,
        InputType.speech_dynamic: __input_from_speech_dynamic
    })
    __output_actions: {} = dict({
        OutputType.text: __output_to_text,
        OutputType.speech: __output_to_speech
    })

    # routes input from the currently selected source
    def __input(self) -> str:
        return self.__input_actions[self.__input_type](self).lower()

    # routes output to the currently selected target
    def __output(self, value: str) -> None:
        return self.__output_actions[self.__output_type](self, str(value))

    # main run method, repeatedly prompts user for options until exit is chosen
    def run(self) -> None:
        print(self.__header)

        while True:
            command = self.__input()
            if command is not None:
                if not self.__process_command(command):
                    break

        self.__output(self.__prompt_exit)

    # process a request: user Chatterbot to attempt to reply logically
    def __process_conversation_request(self, subject: str) -> bool:
        try:
            statement = cb_convo.Statement(subject)
            response = self.__chatbot.get_response(statement)
            self.__output(response)
        except:
            self.__output(self.__prompt_error)
        return True

    # process a request: prompt to change the current method of input
    def __process_input_request(self, subject: str) -> bool:
        while True:
            if 'text' in subject:
                self.__input_type = InputType.text
                self.__output("Input type changed to \"text\".")
                return True
            if 'prompted speech' in subject:
                self.__input_type = InputType.speech_prompted
                self.__output("Input type changed to \"prompted speech\".")
                return True
            if 'dynamic speech' in subject:
                self.__input_type = InputType.speech_dynamic
                self.__output("Input type changed to \"dynamic speech\". Preface any commands with \"Hey JJ\".")
                return True
            if 'cancel' in subject:
                return True
            if 'exit' in subject:
                return False

            self.__output("Please choose from the following input options:\n"
                          "\"Text\", \"Prompted Speech\", or \"Dynamic Speech\".")
            subject = self.__input()

    # process a request: prompt to change the current method of output
    def __process_output_request(self, subject: str) -> bool:
        while True:
            if 'text' in subject:
                self.__output_type = OutputType.text
                self.__output("Output type changed to \"text\".")
                return True
            if 'speech' in subject:
                self.__output_type = OutputType.speech
                self.__output("Output type changed to \"speech\".")
                return True
            if 'cancel' in subject:
                return True
            if 'exit' in subject:
                return False

            self.__output("Please choose from the following output options:\n"
                          "\"Text\" or \"Speech\".")
            subject = self.__input()

    # process a request: exit the application
    def __process_exit_request(self, subject: str) -> bool:
        return False

    __request_actions: {} = dict({
        CommandCategory.conversation: __process_conversation_request,
        CommandCategory.change_input: __process_input_request,
        CommandCategory.change_output: __process_output_request,
        CommandCategory.exit_program: __process_exit_request,
    })

    # parse the command into component pieces and funnel into the appropriate handler
    def __process_command(self, command: str) -> bool:
        category, subject = self.__parse_command_string(command)
        return self.__request_actions[category](self, subject)

    # parse the input command into a tuple of Category and Subject
    @staticmethod
    def __parse_command_string(command: str) -> ():
        if command.startswith('exit'):
            return CommandCategory.exit_program, command

        if command.startswith('change input'):
            return CommandCategory.change_input, command

        if command.startswith('change output'):
            return CommandCategory.change_output, command

        # fallback to Chatterbot AI
        return CommandCategory.conversation, command
