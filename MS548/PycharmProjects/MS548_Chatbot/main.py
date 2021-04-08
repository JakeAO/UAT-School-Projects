from assistant import Assistant
import json


def run_assistant():
    lib = Assistant()
    lib.run()


def process_ccpe():
    convos = list()
    # read raw into memory by conversation
    # [ { 'utterances': [ { 'text': 'dialog string' } ] } ]
    with open('TrainingDataRaw/ccpe-main/data.json', mode='rt', encoding='utf-8', errors='ignore') as file:
        jObject = json.load(file)
        for convo in jObject:
            if len(convo['utterances']) < 2:
                continue

            convo_lines = list()
            convos.append(convo_lines)
            for utterance in convo['utterances']:
                line = utterance['text'].lower()
                line = line.replace(u"\u00A0", ' ')
                convo_lines.append(line)
    # write conversations to clean file
    with open('TrainingData/CCPE/data.txt', mode='wt', encoding='utf-8', errors='ignore') as file:
        for convo in convos:
            for line in convo:
                file.write(line)
                file.write('\n')


def process_convai():
    convos = list()
    # read raw into memory by conversation
    # [ { 'dialog': [ { 'text': 'dialog string' } ] } ]
    with open('TrainingDataRaw/ConvAI3/data_volunteers.json', mode='rt', encoding='utf-8', errors='ignore') as file:
        jObject = json.load(file)
        for convo in jObject:
            if len(convo['dialog']) < 2:
                continue

            convo_lines = list()
            convos.append(convo_lines)
            for dialog in convo['dialog']:
                line = dialog['text'].lower()
                convo_lines.append(line)
    # write conversations to clean file
    with open('TrainingData/ConvAI/data.txt', mode='wt', encoding='utf-8', errors='ignore') as file:
        for convo in convos:
            for line in convo:
                file.write(line)
                file.write('\n')


def process_questionanswer():
    # read raw into memory by 'conversation' (Q&A pair)
    # ArticleTitle [tab] Question [tab] Answer
    def load_internal(path: str, convos: dict):
        with open(path, mode='rt', encoding='utf-8', errors='ignore') as file:
            file.readline()
            line = file.readline()
            while line:
                parts = line.lower().split('\t')
                if len(parts) >= 3 and parts[1] and parts[2] and parts[2] != 'null':
                    convos[parts[1]] = parts[2]
                line = file.readline()

    convos = dict()  # unlike the other processors, use a dict here because there are duplicates in the data
    load_internal('TrainingDataRaw/Question_Answer_Dataset_v1.2/S08/question_answer_pairs.txt', convos)
    load_internal('TrainingDataRaw/Question_Answer_Dataset_v1.2/S09/question_answer_pairs.txt', convos)
    load_internal('TrainingDataRaw/Question_Answer_Dataset_v1.2/S10/question_answer_pairs.txt', convos)

    # write conversations to clean file
    with open('TrainingData/QuestionAnswer/data.txt', mode='wt', encoding='utf-8', errors='ignore') as file:
        for key in convos:
            file.write(f"{key}\n")
            file.write(f"{convos[key]}\n")


def process_movielines():
    all_lines = dict()
    # read raw lines into memory by conversation
    # [id] +++$+++ [speaker] +++$+++ [movie] +++$+++ [character] +++$+++ [dialog]
    with open('TrainingDataRaw/cornell movie-dialogs corpus/movie_lines.txt',
              mode='rt', encoding='utf-8', errors='ignore') as file:
        for line in file:
            parts = line.split(' +++$+++ ')
            if len(parts) < 5:
                continue
            all_lines[parts[0].strip()] = parts[4].strip().lower()

    convos = list()
    # read each conversation line and compose the component pieces into a single array
    # [entity1] +++$+++ [entity2] +++$+++ [movie] +++$+++ [component ids]
    with open('TrainingDataRaw/cornell movie-dialogs corpus/movie_conversations.txt',
              mode='rt', encoding='utf-8', errors='ignore') as file:
        for line in file:
            parts = line.split(' +++$+++ ')
            if len(parts) < 4:
                continue
            convo_line_ids = parts[3]\
                .replace('\n', '')\
                .replace('\r', '')\
                .replace('[', '')\
                .replace(']', '')\
                .replace('\'', '')\
                .split(', ')
            convo_lines = []
            for id in convo_line_ids:
                line = all_lines.get(id)
                if line:
                    convo_lines.append(line)
            if len(convo_lines) > 1:
                convos.append(convo_lines)

    # write conversations to clean file
    with open('TrainingData/MovieLines/data.txt',
              mode='wt', encoding='utf-8', errors='ignore') as file:
        for convo in convos:
            for line in convo:
                file.write(line)
                file.write('\n')


if __name__ == "__main__":
    # process_ccpe()
    # process_convai()
    # process_movielines()
    # process_questionanswer()
    run_assistant()
