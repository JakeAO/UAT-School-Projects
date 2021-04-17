from random import Random
from abc import *
import pandas as pd
import twitter as twit


# base class for a tweet reader
class ReaderBase(ABC):
    @abstractmethod
    def read(self, search_term: str = None, status_limit: int = 100) -> [(str, str)]:
        pass


# ReaderBase implementation which reads tweets from Twitter using the twitter library
class TwitterReader(ReaderBase):
    __twitter: twit.Twitter = None

    def __init__(self, auth: twit.auth.Auth) -> None:
        self.__twitter = twit.Twitter(auth=auth)

    def read(self, search_term: str = None, status_limit: int = 100) -> [(str, str)]:
        if search_term and search_term.startswith('#'):
            tweets = self.__twitter.search.tweets(q=search_term, limit=status_limit)
        elif search_term and search_term.startswith('@'):
            tweets = self.__twitter.search.user_tweets(screen_name=search_term.removeprefix('@'), limit=status_limit)
        else:
            print(f"Search term \"{search_term}\" is either a Twitter screen name nor a hashtag.")
            tweets = []
        return [(tweet['user']['screen_name'], tweet.text) for tweet in tweets]


# ReaderBase implementation which reads tweets from a csv datasource: Data/Tweets.csv
class DebugReader(ReaderBase):
    def read(self, search_term: str = None, status_limit: int = 100) -> [(str, str)]:
        data: pd.DataFrame = pd.read_csv('data/Tweets.csv')
        if search_term and search_term.startswith('#'):
            values = [x for x in data.values if search_term in x[1]][0:status_limit]
        elif search_term and search_term.startswith('@'):
            search_term = search_term.removeprefix('@')
            values = [x for x in data.values if x[0] == search_term][0:status_limit]
        else:
            row_min = Random().randint(0, (1000000 - status_limit)) - 1
            row_max = row_min + status_limit
            values = data[row_min:row_max].values
        return [(val[0], val[1]) for val in values]
