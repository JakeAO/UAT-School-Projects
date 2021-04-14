import io
import os
import random
import matplotlib.pyplot as plt
from PIL import Image
from azure.cognitiveservices.vision.face import FaceClient
from azure.cognitiveservices.vision.face.models import DetectedFace, Gender
from msrest.authentication import CognitiveServicesCredentials

KEY = "KEY_GOES_HERE"
ENDPOINT = "ENDPOINT_GOES_HERE"
FACE_CLIENT = FaceClient(ENDPOINT, CognitiveServicesCredentials(KEY))


def get_image(file_path: str) -> (io.BufferedReader, Image):
    return open(file_path, 'rb'), Image.open(file_path)


def get_faces(reader: io.BufferedReader) -> [(DetectedFace, float, Gender, str)]:
    faces = FACE_CLIENT.face.detect_with_stream(reader, return_face_attributes=['age', 'emotion', 'gender'])
    result = []
    if faces:
        for face in faces:
            if face:
                emotions = [('anger', face.face_attributes.emotion.anger),
                            ('contempt', face.face_attributes.emotion.contempt),
                            ('disgust', face.face_attributes.emotion.disgust),
                            ('fear', face.face_attributes.emotion.fear),
                            ('happiness', face.face_attributes.emotion.happiness),
                            ('neutral', face.face_attributes.emotion.neutral),
                            ('sadness', face.face_attributes.emotion.sadness),
                            ('surprise', face.face_attributes.emotion.surprise)]
                emotions = sorted(emotions, key=lambda x: x[1], reverse=True)
                result.append((
                    face,
                    int(face.face_attributes.age),
                    face.face_attributes.gender.value.title(),
                    emotions[0][0].title()))
    return result


if __name__ == '__main__':
    rate_limiter: int = 10  # avoid hitting the Azure rate limit

    all_file_names = os.listdir('faces')
    for _ in range(rate_limiter):
        file_name = random.choice(all_file_names)
        file_path = os.path.join('faces', file_name)

        all_file_names.remove(file_name)

        reader, image = get_image(file_path)
        faces = get_faces(reader)

        if len(faces) > 0:
            table_content = [['Face #', 'Age', 'Gender', 'Primary Emotion']]
            for i in range(len(faces)):
                table_content.append([i+1, faces[i][1], faces[i][2], faces[i][3]])
        else:
            table_content = [['No faces detected in image.']]

        plt.axis('off')
        plt.rc('font', size=14)
        plt.table(table_content, loc='bottom')
        plt.imshow(image)
        plt.show()
