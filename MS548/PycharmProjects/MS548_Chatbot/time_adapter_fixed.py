from datetime import datetime
from chatterbot.logic import LogicAdapter
from chatterbot.conversation import Statement


# Chatterbot TimeLogicAdapter but it's been fixed.
# By default, will totally respond to any question regardless of if it contains time or any mention of it.
class TimeLogicAdapterFixed(LogicAdapter):
    def __init__(self, chatbot, **kwargs):
        super().__init__(chatbot, **kwargs)
        from nltk import NaiveBayesClassifier

        self.positive = kwargs.get('positive', [
            'what time is it',
            'hey what time is it',
            'do you have the time',
            'do you know the time',
            'do you know what time it is',
            'what is the time',
            'what day is it',
            'hey what day is it',
            'do you have the date',
            'do you know the date',
            'do you know what date it is',
            'what is the date'
        ])

        self.negative = kwargs.get('negative', [
            'it is time to go to sleep',
            'what is your favorite color',
            'i had a great time',
            'thyme is my favorite herb',
            'do you have time to look at my essay',
            'how do you have the time to do all this',
            'what is it',
            "what's up",
            "how's it going",
            'how are you',
            'what are you',
            'what '
        ])

        labeled_data = (
            [
                (name, 0) for name in self.negative
            ] + [
                (name, 1) for name in self.positive
            ]
        )

        train_set = [
            (self.time_question_features(text), n) for (text, n) in labeled_data
        ]

        self.classifier = NaiveBayesClassifier.train(train_set)

    def time_question_features(self, text):
        """
        Provide an analysis of significant features in the string.
        """
        features = {}

        # A list of all words from the known sentences
        all_words = " ".join(self.positive + self.negative).split()

        # A list of the first word in each of the known sentence
        all_first_words = []
        for sentence in self.positive + self.negative:
            all_first_words.append(
                sentence.split(' ', 1)[0]
            )

        for word in text.split():
            features['first_word({})'.format(word)] = (word in all_first_words)

        for word in text.split():
            features['contains({})'.format(word)] = (word in all_words)

        for letter in 'abcdefghijklmnopqrstuvwxyz':
            features['count({})'.format(letter)] = text.lower().count(letter)
            features['has({})'.format(letter)] = (letter in text.lower())

        return features

    def can_process(self, statement):
        lower_text = statement.text.lower()
        if 'time' in lower_text and 'times' not in lower_text:
            return super().can_process(statement)
        if 'date' in lower_text or 'day' in lower_text:
            return super().can_process(statement)
        return False

    def process(self, statement, additional_response_selection_parameters=None):
        lower_text = statement.text.lower()
        if 'time' in lower_text:
            now = datetime.now()
            response = Statement(text='The current time is ' + now.strftime('%I:%M %p'))
        elif 'date' in lower_text or 'day' in lower_text:
            now = datetime.today()
            response = Statement(text='The current date is ' + now.strftime('%A, %d %b %y'))
        else:
            return None

        time_features = self.time_question_features(lower_text)
        confidence = self.classifier.classify(time_features)
        response.confidence = confidence
        return response
