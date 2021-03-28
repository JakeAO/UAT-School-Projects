MS548 Project 3

A simple Python app which takes in and performs analysis on
books from the Project Gutenburg catalog. The program outputs
both a .jpg containing a graph of the sentiment-over-time of
the book (divided into polarity and subjectivity) as well as
a .txt containing rough summaries of each chapter's contents
based on the frequency of what TextBlob considers to be proper
nouns (doctored a bit to remove obviously false positives).

main.py
    Just the main launching point of the project.
PythonBookAnalysis.py
    AnalysisWrapper
        Scans the hierarchy for files in the Books directory
        and presents the user with a menu containing the books
        that were found. Repeats until the user exits.
    Analyzer
        Loads the provided book path and attempts to load its
        content on a chapter-by-chapter basis in order to aid
        analysis. Once loaded, the analyze_book() method can
        be called to perform the analysis and output files
        for both the summary and sentiment analyses.

Project Duration
    ~2 hr attempting to come up with a project idea and
    searching through the TextBlob documentation for something
    useful to do.
    ~1 hr writing the foundational code: scanning directory,
    loading files, calculating average sentiments, etc.
    ~2 hr trying to convince the Python graphing library to work
    and output reasonable graphs and tables

    Overall this project didn't take very long, but I did lose
    a lot of time because of Python's lack of explicit typing.
    Every time I tried a new thing in matplotlib or numpy I ended
    up having to Google for 5 minutes to actually see what the
    return values were supposed to be.