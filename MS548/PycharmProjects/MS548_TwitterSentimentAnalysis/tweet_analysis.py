from nltk.sentiment.vader import SentimentIntensityAnalyzer


# analyze tweet (string) text using nltk
class TweetAnalysis:
    __analyzer: SentimentIntensityAnalyzer = SentimentIntensityAnalyzer()

    # returns a sentiment analysis in the form of a tuple (positivity, compound polarity)
    def analyze_text(self, tweet: str) -> (float, float):
        pol = self.__analyzer.polarity_scores(tweet)
        return pol['pos'], pol['compound']
