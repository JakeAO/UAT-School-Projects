from chatterbot.logic import LogicAdapter
from chatterbot.conversation import Statement


# Chatterbot SpecificResponseAdapter, but it's been fixed so it actually works.
# By default, can_process() always returns False since Statement and str can't be compared.
class SpecificResponseAdapterFixed(LogicAdapter):
    def __init__(self, chatbot, **kwargs):
        super().__init__(chatbot, **kwargs)

        self.input_text = kwargs.get('input_text')
        self.response_statement = Statement(kwargs.get('output_text'))

    def can_process(self, statement):
        return statement.text.lower() == self.input_text.lower()

    def process(self, statement, additional_response_selection_parameters=None):
        if statement.text.lower() == self.input_text.lower():
            self.response_statement.confidence = 1
        else:
            self.response_statement.confidence = 0

        return self.response_statement
