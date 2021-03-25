from textblob import TextBlob
from polarityvalence import PolarityValence
from subjectivityvalence import SubjectivityValence


class TextBlobExtensions:
    @staticmethod
    def get_word_count(text_blob: TextBlob) -> ():
        return (
            len(text_blob.words),
            len(text_blob.word_counts)
        )

    @staticmethod
    def get_sentence_count(text_blob: TextBlob) -> int:
        return len(text_blob.sentences)

    @staticmethod
    def get_definition_map(text_blob: TextBlob) -> {}:
        def_map = {}
        for word in text_blob.words:
            if len(word.definitions) > 0:
                def_map[word] = word.definitions[:3]
        return def_map

    @staticmethod
    def get_sentiment(text_blob: TextBlob) -> ():
        return (
            PolarityValence.from_value(text_blob.polarity),
            SubjectivityValence.from_value(text_blob.subjectivity)
        )

    @staticmethod
    def get_part_of_speech_map(text_blob: TextBlob) -> {}:
        pos_map = {}
        for word_tag_tuple in text_blob.tags:
            if not pos_map.get(word_tag_tuple[1]):
                pos_map[word_tag_tuple[1]] = set()
            pos_map[word_tag_tuple[1]].add(word_tag_tuple[0].lower())
        return pos_map
