import imutils

from input_data import InputData
from output_data import OutputData
from statistics import mean
from tensorflow.keras.applications import ResNet152 as ResNetModel
from tensorflow.keras.layers import *
from tensorflow.keras.models import Model
import os.path as path
import numpy as np
import cv2


class OCRModel:
    __save_path: str = 'resnet_weights.h5'
    __model: ResNetModel = None

    LABELS: str = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ'
    LABEL_NAMES: [] = [char for char in LABELS]
    LABEL_COUNT: int = len(LABELS)

    def __init__(self):
        model = ResNetModel(weights='imagenet',
                            include_top=False,
                            input_shape=(32, 32, 3))
        tail = model.output
        tail = GlobalAveragePooling2D()(tail)
        tail = Dense(1024, activation='relu', name="head1")(tail)
        tail = Dense(512, activation='relu', name="head2")(tail)
        tail = Dense(256, activation='relu', name="head3")(tail)
        tail = Dense(128, activation='relu', name="head4")(tail)
        tail = Dense(26, activation='softmax', name="head_end")(tail)
        self.__model = Model(inputs=model.input,
                             outputs=tail)
        for layer in self.__model.layers:
            layer.trainable = True

        self.load_weights()

    def get_model(self) -> ResNetModel:
        return self.__model

    def load_weights(self) -> None:
        if path.exists(self.__save_path):
            self.__model.load_weights(self.__save_path, by_name=True)

    def save_weights(self) -> None:
        self.__model.save_weights(self.__save_path, save_format='h5')

    def process(self, input_data: InputData) -> OutputData:
        MIN_WIDTH = 5
        MAX_WIDTH = 150
        MIN_HEIGHT = 15
        MAX_HEIGHT = 120

        # grab all the contours from the image/input and convert them to predict-able values
        contours_to_predict: [] = []
        for contour in input_data.get_contours():
            (x, y, w, h) = cv2.boundingRect(contour)

            # filter out badly sized contours
            if w < MIN_WIDTH or w > MAX_WIDTH or h < MIN_HEIGHT or h > MAX_HEIGHT:
                continue

            # pull the greyscale region from the image
            contour_grey = input_data.get_image_grey()[y:y+h, x:x+w]
            threshold = cv2.threshold(contour_grey, 0, 255, cv2.THRESH_BINARY_INV | cv2.THRESH_OTSU)[1]
            threshold_height, threshold_width = threshold.shape

            # resize image to 32 our model expects
            if threshold_height > threshold_width:
                threshold = imutils.resize(threshold, height=32)
            else:
                threshold = imutils.resize(threshold, width=32)

            # pull resized threshold and pad the image to 32x32 square
            threshold_height, threshold_width = threshold.shape
            adjust_x = int(max(0, 32 - threshold_width) / 2.0)
            adjust_y = int(max(0, 32 - threshold_height) / 2.0)
            padded = cv2.copyMakeBorder(threshold,
                                        top=adjust_y, bottom=adjust_y,
                                        left=adjust_x, right=adjust_x,
                                        borderType=cv2.BORDER_CONSTANT,
                                        value=(0, 0, 0))
            padded = cv2.resize(padded, (32, 32))

            # convert padded image to array for ML model input
            image_array = padded.astype('float32') / 255.0
            image_array = np.stack((image_array, image_array, image_array), axis=-1)

            contours_to_predict.append((image_array, (x, y, w, h)))

        # unpack the final doctored contour images and their positions
        character_contours = np.array([c[0] for c in contours_to_predict], dtype='float32')
        bounding_boxes = [c[1] for c in contours_to_predict]

        # predict which character each contour is
        predictions = self.__model.predict(character_contours)

        # compound the predicted output (and show an image of what we thing went where)
        avg_glyph_width = mean([bbox[2] for bbox in bounding_boxes])
        avg_glyph_height = mean([bbox[3] for bbox in bounding_boxes])
        debug_index = 0
        characters: [] = []
        debug_characters: [] = []
        prev_box: () = None
        for prediction, (x, y, w, h) in zip(predictions, bounding_boxes):
            max_index = np.argmax(prediction)
            pred_value = prediction[max_index]
            pred_as_char = self.LABEL_NAMES[max_index]

            debug_index += 1

            # deal with line breaks, spaces, and non-characters
            if prev_box is not None:
                (px, py, pw, ph) = prev_box
                if y > py + ph + avg_glyph_height:  # probably a line break, insert one
                    characters.append('\n')
                elif x > px + pw + avg_glyph_width:  # probably a space, insert one
                    characters.append(' ')
            prev_box = (x, y, w, h)

            # load debug info into the list
            debug_characters.append((debug_index, pred_as_char, pred_value))

            # only draw characters we are at least a little confident about
            if pred_value > 0.4:
                characters.append(pred_as_char)

                color = (0, 255 * pred_value, 512 * (1 - pred_value))
                cv2.putText(input_data.get_image(), pred_as_char, (x, y-8), cv2.FONT_HERSHEY_SIMPLEX, 0.75, color, 1)
                cv2.putText(input_data.get_image(), str(debug_index), (x, y+h+12), cv2.FONT_HERSHEY_SIMPLEX, 0.5, (255, 0, 0), 1)
            else:
                color = (0, 0, 255)
                characters.append(f'[{pred_as_char}]')

            cv2.rectangle(input_data.get_image(), (x, y), (x+w, y+h), color, 1)

        cv2.imshow("Image", input_data.get_image())
        cv2.waitKey(0)

        return OutputData(characters, debug_characters)
