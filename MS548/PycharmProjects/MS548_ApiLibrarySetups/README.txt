MS548 Project 4 (aka 'API Library Setups')

Decided to write a simple little chatbot in order to
familiarize myself with some more Python libraries
aside from Tensorflow (since I used that in the other
project this week). This chatbot, called 'JJ' can
accept input in one of three different modes and can
output using two different modes. Basic commands like
the current time, date, and wikipedia lookup can be
handled, as well as changing input/output modes. If
a command isn't handled directly, it is passed off
to a marginally-trained Chatterbot object in order
to generate a response.

main.py
    Just the main launching point of the project.
assistant.py
    Contains all the code for the project. The Assistant
    object handles input/output/processing of text and
    voice commands. Voice input uses the speech_recognition
    library's Google recognition functionality, and output
    uses pyttsx3 for text-to-speech translation. The
    chatterbot and wikipedia libraries are imported to
    generate responses to specific inputs.

    Input Modes:
        text -> prompts the command line
        prompted speech -> begins listening to commands after a key is pressed
        dynamic speech -> always listens for speech starting with 'Hey JJ'
    Output Modes:
        text -> prints response to command line
        speech -> speaks response