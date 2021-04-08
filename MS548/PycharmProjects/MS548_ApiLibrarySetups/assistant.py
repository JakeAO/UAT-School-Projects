import speech_recognition as sr
import pyttsx3 as tts
import datetime as dt
import wikipedia as wiki
import enum as enum
import chatterbot as cb
import textblob as tb
import random as rand


class CommandCategory(enum.Enum):
    lookup_request: int = 0
    date_request: int = 1
    time_request: int = 2
    conversation: int = 3
    change_input: int = -1
    change_output: int = -2
    exit_program: int = -3
    help: int = -4


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
                    "# such as defining terms, looking up info,\n" \
                    "# or playing music.\n" \
                    "############################################"
    __prompt_help: str = "JJ McChatbot can reply to simple requests, " \
                         "or attempt to hold a conversation. Try asking " \
                         "\"what time is it\" to get the current time or " \
                         "\"who is Jules Verne\" to search Wikipedia. Say " \
                         "\"exit\", \"change input\", or \"change output\" " \
                         "at any prompt to leave the program or adjust settings."
    __prompt_exit: str = "Thank you for using JJ McChatbot!"

    __chatbot: cb.ChatBot = cb.ChatBot('JJ')
    __listener: sr.Recognizer = sr.Recognizer()
    __ttsEngine: tts.Engine = tts.init(debug=True)

    # prompt user for text input in the console
    def __input_from_text(self) -> str:
        return input("Command << ")

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

    __input_type: InputType = InputType.text
    __output_type: OutputType = OutputType.text

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
        return self.__input_actions[self.__input_type](self)

    # routes output to the currently selected target
    def __output(self, value: str) -> None:
        return self.__output_actions[self.__output_type](self, value)

    # main run method, repeatedly prompts user for options until exit is chosen
    def run(self) -> None:
        print(self.__header)

        while True:
            command = self.__input()
            if command is not None:
                if not self.__process_command(command):
                    break

        self.__output(self.__prompt_exit)

    # process a request: lookup 'subject' on wikipedia, outputting a few sentences
    def __process_lookup_request(self, subject: str) -> bool:
        response = wiki.summary(subject, 3)
        self.__output(response)
        return True

    # process a request: output the current date
    def __process_date_request(self, subject: str) -> bool:
        date = dt.datetime.today()
        date_format = "%B %d %Y"
        response = f"The current date is {date.strftime(date_format)}"
        self.__output(response)
        return True

    # process a request: output the current time
    def __process_time_request(self, subject: str) -> bool:
        time = dt.datetime.now().time()
        time_format = "%I:%M"
        response = f"The current time is {time.strftime(time_format)}."
        self.__output(response)
        return True

    # process a request: user Chatterbot to attempt to reply logically
    def __process_conversation_request(self, subject: str) -> bool:
        response = self.__chatbot.get_response(subject)
        self.__output(response)
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
    def __process_help_request(self, subject: str) -> bool:
        self.__output(self.__prompt_help)
        return True

    # process a request: exit the application
    def __process_exit_request(self, subject: str) -> bool:
        return False

    __request_actions: {} = dict({
        CommandCategory.lookup_request: __process_lookup_request,
        CommandCategory.date_request:  __process_date_request,
        CommandCategory.time_request:  __process_time_request,
        CommandCategory.conversation:  __process_conversation_request,
        CommandCategory.change_input:  __process_input_request,
        CommandCategory.change_output:  __process_output_request,
        CommandCategory.help: __process_help_request,
        CommandCategory.exit_program: __process_exit_request,
    })

    # parse the command into component pieces and funnel into the appropriate handler
    def __process_command(self, command: str) -> bool:
        category, subject = self.__parse_command_string(command)
        return self.__request_actions[category](self, subject)

    # parse the input command into a tuple of Category and Subject
    @staticmethod
    def __parse_command_string(command: str) -> ():
        blob: tb.TextBlob = tb.TextBlob(command)

        if command.startswith('exit'):
            return CommandCategory.exit_program, command

        if command.startswith('help'):
            return CommandCategory.help, command

        if command.startswith('change input'):
            return CommandCategory.change_input, command

        if command.startswith('change output'):
            return CommandCategory.change_output, command

        # try parsing interesting commands based on the PoS tags.
        #   e.g. 'what' + 'day' -> datetime.today()
        #        'what' + 'los angeles' -> wiki('los angeles')
        tags = blob.pos_tags
        chunks = blob.noun_phrases
        questions = [tag[0] for tag in tags if tag[1][0] == 'W']
        nouns = [tag[0] for tag in tags if tag[1][0] == 'N']
        if len(questions) > 0 and len(nouns) > 0:
            subject = rand.choice(nouns)
            if len(chunks) > 0:
                subject = rand.choice(chunks)

            if 'what' in questions:
                if 'time' in nouns:
                    return CommandCategory.time_request, None
                if 'date' in nouns or 'day' in nouns:
                    return CommandCategory.date_request, None
                return CommandCategory.lookup_request, subject
            if 'who' in questions or 'where' in questions:
                return CommandCategory.lookup_request, subject

        # fallback to Chatterbot AI
        return CommandCategory.conversation, command
