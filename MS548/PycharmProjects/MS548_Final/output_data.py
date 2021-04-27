class OutputData:
    __characters: [] = None
    __debug_characters: [] = None

    def __init__(self,
                 characters: [],
                 debug_characters: []):
        self.__characters = characters
        self.__debug_characters = debug_characters

    def get_characters(self) -> []: return self.__characters
    def get_debug_characters(self) -> []: return self.__debug_characters
