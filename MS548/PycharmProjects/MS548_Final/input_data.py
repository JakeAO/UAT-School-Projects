class InputData:
    __file_path: str = None
    __image = None
    __image_grey = None
    __contours = None

    def __init__(self,
                 file_path: str,
                 image,
                 image_grey,
                 contours):
        self.__file_path = file_path
        self.__image = image
        self.__image_grey = image_grey
        self.__contours = contours

    def get_file_path(self) -> str: return self.__file_path
    def get_image(self): return self.__image
    def get_image_grey(self): return self.__image_grey
    def get_contours(self) -> []: return self.__contours
