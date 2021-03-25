MS548 Project 1

Just a simple Python app which exposes various TextBlob
functions through a repeating console menu. Exposes word
count, sentence count, definitions, sentiment analysis,
and parts of speech extraction methods on TextBlob. Can
run the processes on either raw text entered from the
command line, or by loading the content of a file whose
name is entered from the command line.

main.py
    Contains the content of the solution as functions.
    Wrapper functions (do_word_count, etc.) contain the
    input and output code, while the behavior functions
    (internal_get_word_count, etc.) contain the actual
    behavior logic.

tests.py
    Contains simple unit tests for each of the behavior
    functions in main.py.

Project Duration
    ~2 hrs setting up Python, PyCharm, TextBlob, and all
    of the dependencies.
    ~1 hr writing (and re-writing) the content of main.py,
    had to do a lot of googling to figure out how Python
    works.
    ~0.5 hr writing the content of tests.py, luckily Python's
    unit test library works very similarly to others.
    ~1 hr adding enums for Polarity and Subjectivity as well as
    unique word counts after discussion post suggestions.

    Overall took slightly longer than expected, mostly because
    I didn't know how much TextBlob would need to download
    or how to write Python at all. I'm sure there are common
    Python style things that I'm missing.