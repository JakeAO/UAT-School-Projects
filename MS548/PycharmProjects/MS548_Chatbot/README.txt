MS548 Project 5 - Build a Chat-Bot

Expanded my previous chatbot assignment to include a much deeper
training set and extracting many of the features that were
explicitly implemented out into Chatterbot-style LogicAdapter
implementations. Assistant has been trained with both the standard
Chatterbot corpus and a dataset of 2455 question-answer pairs
distilled manually from Wikipedia articles.

main.py
    Just the main launching point of the project.
    Also contains utility methods to process the files in the
    TrainingDataRaw folder into corresponding flat files in the
    TrainingData folder. These aren't necessary anymore, but I
    left them in place for the sake of visibility.
assistant.py
    Contains all the code for the project. The Assistant
    object handles input/output/processing of text and
    voice commands. Voice input uses the speech_recognition
    library's Google recognition functionality, and output
    uses pyttsx3 for text-to-speech translation. The
    chatterbot and wikipedia libraries are imported to
    generate responses to specific inputs.
time_adapter_fixed.py
    Contains a modified version of Chatterbot's TimeLogicAdapter.
    The original version was improperly returning time answers
    to unrelated questions such as "what's up" and "hey". The
    modification ensures that the word 'time' appears in the
    request. Also adds handling for 'date' requests in the adapter.
specific_response_fixed.py
    Contains a modified version of Chatterbot's SpecificResponseLogicAdapter.
    The original version was improperly comparing a Statement-type object
    to a str-type object, which always resulted in False. The change
    rectifies this as well as cleans up the code a slight bit.
wiki_logic_adapter.py
    Contains a new implementation of LogicAdapter which parses
    questions out of input statements and tries to look them up
    on Wikipedia. Questions must be proceeded by 'wh' words
    (e.g. 'who', 'what', etc.) and contain at least one noun
    later in the sentence. The sentence's noun (or ideally the
    noun-pair chunk containing the noun) are looked up on Wikipedia
    and the first 3 sentences are returned as a response.