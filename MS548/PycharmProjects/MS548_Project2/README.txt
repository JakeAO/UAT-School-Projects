MS548 Project 2

Just a simple Python app which exposes various TextBlob
functions through a repeating console menu. Exposes word
count, sentence count, definitions, sentiment analysis,
and parts of speech extraction methods on TextBlob. Input
and output methods can both be switched between Console
and File through the menu using the role class pattern.

main.py
    Just the main launching point of the project.
project2.py
    The meat of the program. Handles printing the menus to the
    console, handling input, and the overall processing.
textblobextensions.py
    The main business logic of the project. Contains static methods
    which interface between the TextBlob API and the Project2 object,
    mutating results into a format that is expected.
subjectivityvalence.py
    Enum declaration that maps TextBlob's subjectivity float value
    onto defined values.
polarityvalence.py
    Enum declaration that maps TextBlob's polarity float value
    onto defined values.
inputrole.py
    Defines the abstract InputRole object and two implementations,
    ConsoleInput and FileInput. The InputRole is responsible for taking
    user input and converting it into a TextBlob that can be used by
    the consumer without worrying about input method.
outputrole.py
    Defines the abstract OutputRole object and two implementations,
    ConsoleOutput and FileOutput. The OutputRole is responsible for
    taking program output in the form of strings and writing them to
    a specific type of output method, without worrying about the type.

Project Duration
    ~1 hr extracting Project 1's responsibilities into
    multiple objects (polarityvalence.py, subjectivityvalence.py,
    textblobextensions.py, and project2.py).
    ~1 hr writing the required inherited classes. Pulled the inline
    console input/output out into role pattern objects.

    This project mostly just took time in determining what could
    theoretically use an inheritance structure. Ended up going with
    a simple role pattern for the input/output handlers, since
    nothing else was really worthy of individual classes that
    had inheritance.