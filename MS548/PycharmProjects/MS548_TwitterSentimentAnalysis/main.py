from twitter_reader import *
from twitter_writer import *
from tweet_analysis import *


class Main:
    __reader: ReaderBase = DebugReader()
    __writer: WriterBase = DebugWriter()
    __analysis: TweetAnalysis = TweetAnalysis()

    __program_header: str = '===================================================\n' \
                            '====         TWITTER SENTIMENT ANALYZER        ====\n' \
                            '====                                           ====\n' \
                            '====     Follow the prompts to analyze tweets  ====\n' \
                            '====  for their positivity and overall         ====\n' \
                            '====  polarity. You can search for specific    ====\n' \
                            '====  users with the @ prefix or for specific  ====\n' \
                            '====  tags with the # prefix. The program will ====\n' \
                            '====  return a maximum of 100 tweets.          ====\n' \
                            '===================================================\n'
    __program_prompt: str = 'Enter a search query (@username, #hashtag, or nothing).\n' \
                            'The input \'exit\' will quit the program.'
    __program_footer: str = 'Thank you for using the Twitter sentiment analyzer!'

    @staticmethod
    def polarity_text(polarity: float) -> str:
        if polarity > 0.7:
            return 'very positive'
        if polarity > 0.4:
            return 'positive'
        if polarity > -0.4:
            return 'neutral'
        if polarity > -0.7:
            return 'negative'
        return 'very negative'

    def run(self) -> None:
        # welcome message
        print(self.__program_header)

        while True:
            # prompt for input
            print(self.__program_prompt)
            val: str = input("Query: ")

            # handle quit case
            if val.lower() == 'exit':
                print()
                break

            # gather tweets
            results = self.__reader.read(val, 100)

            if results:
                # analyze print results
                for result in results:
                    user: str = result[0]
                    tweet: str = result[1]
                    analysis = self.__analysis.analyze_text(tweet)
                    positivity: float = analysis[0]
                    polarity: float = analysis[1]

                    print(f"[@{user}] {tweet}")
                    print(f"     ({self.polarity_text(polarity)} [{polarity}])")
            else:
                print(f'No tweets found for query.')

            print()

        # farewell message
        print(self.__program_footer)


if __name__ == '__main__':
    main = Main()
    main.run()
