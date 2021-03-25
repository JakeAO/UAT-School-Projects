from textblob import TextBlob
from abc import ABC, abstractmethod


# abstract base class of the input role
class InputRole(ABC):
    # (abs) read text input from the role
    @abstractmethod
    def read(self) -> TextBlob:
        pass


class ConsoleInput(InputRole):
    __cached_text: TextBlob = None

    def __init__(self, text: str):
        self.__cached_text = TextBlob(text)

    def read(self) -> TextBlob:
        return self.__cached_text

    @staticmethod
    def create():
        try:
            text = input("[INPUT] Text: ")
            role = ConsoleInput(text)
            return role
        except Exception as err:
            print("ERROR: Failed to initialize input role!\n" + str(err))
            return None


class FileInput(InputRole):
    __cached_text: TextBlob = None

    def __init__(self, file_path: str) -> None:
        file = open(file_path, "rt")
        text = file.read()
        self.__cached_text = TextBlob(text)
        file.close()

    def read(self) -> TextBlob:
        return self.__cached_text

    @staticmethod
    def create():
        try:
            file_path = input("[INPUT] File Path: ")
            role = FileInput(file_path)
            return role
        except Exception as err:
            print("ERROR: Failed to initialize input role!\n" + str(err))
            return None
