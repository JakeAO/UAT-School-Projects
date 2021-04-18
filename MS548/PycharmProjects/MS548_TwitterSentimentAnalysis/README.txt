https://synchronic.uat.edu/courses/3953/assignments/141367?module_item_id=357530

MS548 Assignment 6 (Sentiment Analysis)

!!! NOTE !!!
As of the writing of this README, I have yet to receive API access from
Twitter, so the program is using debug input/output implementations that
rely on a Kaggle dataset of roughly one million tweets.
!!! END NOTE !!!

Simple program that pulls tweets from Twitter and reports their overall
sentiment using NLTK's vader sentiment intensity analyzer. Prompts user
for input on which tweets to analyze; accepting usernames (@username),
hashtags (#hashtag), or null to pull from the users timeline. The program
will pull a maximum of 100 results from Twitter for analysis.

main.py
    Contains the Main class which handles user input loop and otherwise
    calls into the other classes for the 'business' logic.
tweet_analysis.py
    Contains the TweetAnalysis class, which uses NLTK's vader
    SentimentIntensityAnalyzer to analyze input tweet text and report
    the polarity in the form of a tuple (positivity [0 to 1], polarity [-1 to 1]).
twitter_reader.py
    Contains the ReaderBase, TwitterReader, and DebugReader classes.
    ReaderBase is the abstract base class for a generic 'tweet reader' that pulls
    data and returns it in the form of an array of tuples (username, tweet).
    TwitterReader implements ReaderBase using the twitter Python library and
    pulls tweets from Twitter based on a provided auth object.
    DebugReader implements ReaderBase using debug data stored in Data/Tweets.csv,
    which contains over one million tweets sourced from a Kaggle dataset.
twitter_writer.py
    Contains WriterBase, TwitterWriter, and DebugWriter classes.
    WriterBase is the abstract base class for a generic 'tweet writer' that
    writes text to the user's timeline.
    TwitterWriter implements WriterBase using the twitter Python library and
    publishes tweets to the user's timeline based on a provided auth object.
    DebugWriter implements WriterBase by simply printing the given tweet
    to the console.