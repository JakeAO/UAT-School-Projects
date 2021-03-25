import unittest
from main import *


class TestCases(unittest.TestCase):
    def test_input_raw(self):
        blob = handle_input("This is a string with words in it.")
        self.assertIsInstance(blob, TextBlob)

    def test_input_file(self):
        blob = handle_input("QuickBrownFox.txt")
        self.assertIsInstance(blob, TextBlob)

    def test_word_count(self):
        blob = TextBlob("One two three four. Five six seven eight. Nine nine.")
        count = internal_get_word_count(blob)
        self.assertEqual(10, count[0])
        self.assertEqual(9, count[1])

    def test_sentence_count(self):
        blob = TextBlob("One two three four. Five six seven eight. Nine nine.")
        count = internal_get_sentence_count(blob)
        self.assertEqual(3, count)

    def test_definition_map(self):
        blob = TextBlob("Cat dog fish frog")
        def_map = internal_get_definition_map(blob)
        self.assertEqual(4, len(def_map.keys()))
        for key in def_map:
            self.assertGreaterEqual(3, len(def_map.get(key)))

    def test_sentiment_positive(self):
        blob = TextBlob("Things are great and I love them to bits!!!")
        sentiment = internal_get_sentiment(blob)
        self.assertEqual(PolarityValence.VeryPositive, sentiment[0])
        self.assertEqual(SubjectivityValence.Subjective, sentiment[1])

    def test_sentiment_negative(self):
        blob = TextBlob("Things are dumb and bad and I hate them!!!")
        sentiment = internal_get_sentiment(blob)
        self.assertEqual(PolarityValence.VeryNegative, sentiment[0])
        self.assertEqual(SubjectivityValence.Subjective, sentiment[1])

    def test_parts_of_speech(self):
        blob = TextBlob("The big pig went riding in the snow storm today.")
        pos_map = internal_get_part_of_speech_map(blob)
        self.assertEqual(6, len(pos_map.keys()))
        self.assertEqual(1, len(pos_map["DT"]))
        self.assertEqual(2, len(pos_map["JJ"]))
        self.assertEqual(3, len(pos_map["NN"]))
        self.assertEqual(1, len(pos_map["VBD"]))
        self.assertEqual(1, len(pos_map["VBG"]))
        self.assertEqual(1, len(pos_map["IN"]))


if __name__ == '__main__':
    unittest.main()
