from tensorflow.keras.preprocessing.image import ImageDataGenerator
from tensorflow.keras.optimizers import Adam
from sklearn.preprocessing import LabelBinarizer
from sklearn.model_selection import train_test_split
from sklearn.metrics import classification_report
from ocr_model import OCRModel
import cv2
import numpy as np
import matplotlib.pyplot as pyplot


class OCRModelTrainer:
    __model: OCRModel = OCRModel()

    def train_and_save(self) -> None:
        model = self.__model.get_model()

        images, labels = self.__load_all_training_data()

        binarizer = LabelBinarizer()
        labels = binarizer.fit_transform(labels)

        totals = labels.sum(axis=0)
        weights = {}
        for i in range(len(totals)):
            weights[i] = totals.max() / totals[i]

        (trainX, testX, trainY, testY) = train_test_split(images,
                                                          labels,
                                                          test_size=0.20,
                                                          stratify=labels)

        EPOCHS = 10
        STEPS = 500
        BATCH = 512

        generator = ImageDataGenerator(
            rotation_range=20,
            zoom_range=0.1,
            shear_range=0.1,
            width_shift_range=0.1,
            height_shift_range=0.1,
            horizontal_flip=False,
            vertical_flip=False,
            fill_mode='nearest'
        )

        model.compile(loss='categorical_crossentropy',
                      optimizer=Adam(),
                      metrics=['accuracy'])

        H = model.fit(
            generator.flow(trainX, trainY, batch_size=BATCH),
            validation_data=(testX, testY),
            validation_steps=STEPS / 4,
            steps_per_epoch=STEPS,
            epochs=EPOCHS,
            class_weight=weights,
            verbose=1
        )

        self.__model.save_weights()

        predictions = model.predict(testX, batch_size=BATCH)
        print(classification_report(testY.argmax(axis=1),
                                    predictions.argmax(axis=1),
                                    target_names=self.__model.LABEL_NAMES))

        N = np.arange(0, EPOCHS)
        pyplot.figure()
        pyplot.plot(N, H.history['accuracy'], label='accuracy')
        pyplot.title('Training Accuracy')
        pyplot.xlabel('Epoch')
        pyplot.ylabel('Accuracy')
        pyplot.savefig('training_results.jpg')
        pyplot.show()

    def __load_all_training_data(self) -> ([], []):
        n19_images, n19_labels = self.__load_nist19()

        images = np.vstack([n19_images])
        labels = np.hstack([n19_labels])

        images = [cv2.resize(img, (32, 32)) for img in images]
        images = [cv2.cvtColor(img, cv2.COLOR_GRAY2RGB) for img in images]
        images = np.array(images, dtype='float32')
        images /= 255.0

        return images, labels

    @staticmethod
    def __load_nist19() -> ([], []):
        images = []
        labels = []

        for row in open('TrainingData/NIST_Special19.csv'):
            cols = row.split(',')
            label = int(cols[0])
            image = np.array([int(x) for x in cols[1:]], dtype='uint8')
            image = image.reshape((28, 28))

            images.append(image)
            labels.append(label)

        images = np.array(images, dtype='float32')
        labels = np.array(labels, dtype='int')
        return images, labels
