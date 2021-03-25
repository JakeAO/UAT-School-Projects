from textblob import TextBlob
import enum
import os


class PolarityValence(enum.Enum):
    Neutral = 0
    Positive = 1
    VeryPositive = 2
    Negative = -1
    VeryNegative = -2

    @staticmethod
    def from_value(polarity_value: float):
        if polarity_value > 0.6:
            return PolarityValence.VeryPositive
        if polarity_value > 0.3:
            return PolarityValence.Positive
        if polarity_value > -0.3:
            return PolarityValence.Neutral
        if polarity_value > -0.6:
            return PolarityValence.Negative
        return PolarityValence.VeryNegative


class SubjectivityValence(enum.Enum):
    Objective = 0
    MostlyObjective = 1
    Subjective = 2
    VerySubjective = 3

    @staticmethod
    def from_value(subjectivity_value: float):
        if subjectivity_value > 0.75:
            return SubjectivityValence.VerySubjective
        if subjectivity_value > 0.5:
            return SubjectivityValence.Subjective
        if subjectivity_value > 0.25:
            return SubjectivityValence.MostlyObjective
        return SubjectivityValence.Objective


# get a TextBlob containing either
#    a) the content of a text file, if a file path is provided
#    b) the raw text provided
def handle_input(input_text: str):
    if os.path.isfile(input_text):
        file = open(input_text)
        blob = TextBlob(file.read())
        file.close()
        return blob
    else:
        return TextBlob(input_text)


# print out the menu options, wait for user input, and validate it
# returns either a valid selection (1-5), an exit code (-1), or a default value (0)
def print_menu():
    print("### TextBlob Testing Tool ####")
    print("   Enter one of the following commands to continue.")
    print("   [1] Word Count: Counts the number of words in a block of text or a file.")
    print("   [2] Sentence Count: Counts the number of sentences in a block of text or a file.")
    print("   [3] Define: Defines each word in a block of text or a file.")
    print("   [4] Sentiment Analysis: Finds the polarity and subjectivity of a block of text or a file.")
    print("   [5] Parts of Speech: Groups all words in a block of text or a file by their part of speech.")
    print("   [X] Exit: Close the program.")

    choice = input("Option: ")
    if choice == 'x' or choice == 'X':
        choice_value = -1  # exit code selected
    else:
        # roundabout TryParse() method
        try:
            choice_value = int(choice)
        except(ValueError, TypeError):
            choice_value = 0

        if choice_value < 1 or choice_value > 5:
            print("!! Invalid option selected:", choice)
            choice_value = 0  # default (no-op) option

    print("#### #### #### #### #### ####")
    return choice_value  # parsed input option


def internal_get_word_count(text_blob: TextBlob):
    return (
        len(text_blob.words),
        len(text_blob.word_counts)
    )


def do_word_count():
    print("#### Word Count ####")
    print("   Enter the text you'd like to count, or the path to a file.")

    input_text = input("Text: ")
    input_blob = handle_input(input_text)  # returns either raw text or content of file

    word_count_tuple = internal_get_word_count(input_blob)

    print("Word Count:", word_count_tuple[0], "(" + str(word_count_tuple[1]) + " unique)")


def internal_get_sentence_count(text_blob: TextBlob):
    return len(text_blob.sentences)


def do_sentence_count():
    print("#### Sentence Count ####")
    print("   Enter the text you'd like to count, or the path to a file.")

    input_text = input("Text: ")
    input_blob = handle_input(input_text)  # returns either raw text or content of file

    sentence_count = internal_get_sentence_count(input_blob)

    print("Sentence Count:", sentence_count)


def internal_get_definition_map(text_blob: TextBlob):
    def_map = {}
    for word in text_blob.words:
        if len(word.definitions) > 0:
            def_map[word] = word.definitions[:3]
    return def_map


def do_define():
    print("#### Spell 'Check' ####")
    print("   Enter the text you'd like to 'check', or the path to a file.")
    input_text = input("Text: ")
    input_blob = handle_input(input_text)  # returns either raw text or content of file

    def_map = internal_get_definition_map(input_blob)

    for word in def_map.keys():
        print("   Word:", word)
        for definition in def_map[word]:
            print("      -", definition)


def internal_get_sentiment(text_blob: TextBlob):
    return (
        PolarityValence.from_value(text_blob.polarity),
        SubjectivityValence.from_value(text_blob.subjectivity)
    )


def do_sentiment_analysis():
    print("#### Sentiment Analysis ####")
    print("   Enter the text you'd like to analyze, or the path to a file.")
    input_text = input("Text: ")
    input_blob = handle_input(input_text)  # returns either raw text or content of file

    sentiment = internal_get_sentiment(input_blob)

    print("Polarity:", sentiment[0].name)
    print("Subjectivity:", sentiment[1].name)


def internal_get_part_of_speech_map(text_blob: TextBlob):
    pos_map = {}
    for word_tag_tuple in text_blob.tags:
        if not pos_map.get(word_tag_tuple[1]):
            pos_map[word_tag_tuple[1]] = set()
        pos_map[word_tag_tuple[1]].add(word_tag_tuple[0].lower())
    return pos_map


def do_parts_of_speech():
    print("#### Parts of Speech ####")
    print("   Enter the text you'd like to analyze, or the path to a file.")
    input_text = input("Text: ")
    input_blob = handle_input(input_text)  # returns either raw text or content of file

    pos_map = internal_get_part_of_speech_map(input_blob)

    for part_of_speech in pos_map:
        print("Part of Speech:", part_of_speech)
        print("   ", pos_map.get(part_of_speech))


def run():
    result = print_menu()
    while result != -1:
        if result == 1:
            do_word_count()
        elif result == 2:
            do_sentence_count()
        elif result == 3:
            do_define()
        elif result == 4:
            do_sentiment_analysis()
        elif result == 5:
            do_parts_of_speech()
        print("\n")
        result = print_menu()


if __name__ == "__main__":
    run()
