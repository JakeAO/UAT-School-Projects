import wikipedia as wiki
import chatterbot.logic as cb_logic
import chatterbot.conversation as cb_convo
import textblob as tb
import random as rand


# logic adapter implementation that searches wikipedia for any 'wh' prefixed questions
class WikiLogicAdapter(cb_logic.LogicAdapter):
    def can_process(self, statement):
        blob: tb.TextBlob = tb.TextBlob(str(statement))
        tags = blob.pos_tags
        if tags[0][1][0] != 'W':  # the first word MUST be a 'wh' word (who, what, why, etc.)
            return False
        nouns = [tag[0] for tag in tags if tag[1][0] == 'N']  # any nouns
        return len(nouns) > 0

    def get_default_response(self, input_statement):
        return cb_convo.Statement("I'm sorry, I don't quite understand.", in_response_to=input_statement)

    def process(self, statement, additional_response_selection_parameters=None):
        # try parsing interesting commands based on the PoS tags.
        #   e.g. 'what' + 'los angeles' -> wiki('los angeles')
        try:
            blob: tb.TextBlob = tb.TextBlob(str(statement))
            tags = blob.pos_tags
            if len(tags) < 3:
                return None

            if tags[0][1][0] != 'W':  # the first word MUST be a 'wh' word (who, what, why, etc.)
                return None

            nouns = [tag[0] for tag in tags if tag[1][0] == 'N']  # any nouns
            if len(nouns) > 0:
                chunks = blob.noun_phrases
                subject = rand.choice(nouns)
                if len(chunks) > 0:
                    subject = rand.choice(chunks)  # always prefer chunks (e.g. 'los angeles' vs 'angeles')

                response = cb_convo.Statement(wiki.summary(subject, 3), in_response_to=str(statement))
                response.confidence = 1
                return response
        except:  # swallowing errors feels bad, but we could get any number of errors here and none really matter
            pass
        return None
