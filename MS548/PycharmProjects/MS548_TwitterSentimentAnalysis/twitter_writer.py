from abc import *
import twitter as twit


# base class for a tweet writer
class WriterBase(ABC):
    @abstractmethod
    def write(self, status: str) -> None:
        pass


# WriterBase implementation which writes tweets to Twitter using the twitter library
class TwitterWriter(WriterBase):
    __twitter: twit.Twitter = None

    def __init__(self, auth: twit.auth.Auth) -> None:
        self.__twitter = twit.Twitter(auth=auth)

    def write(self, status: str) -> None:
        self.__twitter.update(status=status)
        pass


# WriterBase implementation which writes Tweets to the console
class DebugWriter(WriterBase):
    def write(self, status: str) -> None:
        # debug flow, just printing
        print(f"[STATUS]: {status}\n")
