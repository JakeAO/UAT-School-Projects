from abc import ABC, abstractmethod
from typing.io import TextIO


# abstract base class of the output role
class OutputRole(ABC):
    # (abs) write given string to the output
    @abstractmethod
    def write(self, text: str) -> None:
        pass


class ConsoleOutput(OutputRole):
    def write(self, text: str) -> None:
        print(text)

    @staticmethod
    def create() -> OutputRole:
        return ConsoleOutput()


class FileOutput(OutputRole):
    __file_path: str = None
    __file: TextIO = None

    def __init__(self, file_path: str) -> None:
        self.__file_path = file_path
        self.__file = open(file_path, "wt")

    def __del__(self):
        if self.__file is not None:
            self.__file.close()

    def write(self, text: str) -> None:
        if self.__file is not None:
            self.__file.write(text)
            self.__file.write("\n")
        else:
            print("ERROR: output file does not exist ->", self.__file_path)

    def get_path(self) -> str:
        return self.__file_path

    def get_file(self) -> TextIO:
        return self.__file

    @staticmethod
    def create() -> OutputRole:
        try:
            file_path = input("[OUTPUT] File Path: ")
            role = FileOutput(file_path)
            return role
        except OSError as err:
            print("ERROR: Failed to initialize output role!\n" + str(err))
            return None
