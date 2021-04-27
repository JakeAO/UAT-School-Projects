from preprocessor import PreProcessor
from ocr_model import OCRModel
# from ocr_model_trainer import OCRModelTrainer
from results_writer import ResultWriter
import os.path as path


class Main:
    __preProcessor: PreProcessor = PreProcessor()
    __ocrModel: OCRModel = OCRModel()
    __resultWriter: ResultWriter = ResultWriter()

    __headerText: str = "This is an OCR program trained to recognize handwriting."
    __promptText: str = "Enter a file path to a .jpg containing text to recognize.\n" \
                        "An output .txt file will be generated with the same name as the input."
    __exitText: str = "Thank you for using this OCR program."

    def run(self) -> None:
        print(self.__headerText)
        while True:
            print(self.__promptText)
            input_path = input("Path: ")
            if path.exists(input_path) and input_path.endswith('.jpg'):  # valid input
                input_data = self.__preProcessor.process(input_path)  # load and preprocess image
                output_data = self.__ocrModel.process(input_data)  # pull text from image using OCR model
                output_path = self.__resultWriter.write(input_data, output_data)  # print results to file
                if output_path:
                    print(f"\n[SUCCESS] OCR results have been printed to file \"{output_path}\"\n")
                else:
                    print(f"\n[ERROR] OCR results could not be calculated or saved!\n")
            elif input_path:  # invalid, but non-null input
                print("\n[ERROR] The provided file path does not exist or cannot be read, please try again.\n")
            else:  # null input, exit loop
                print()
                break

        print(self.__exitText)


if __name__ == '__main__':
    # ocr_trainer = OCRModelTrainer()
    # ocr_trainer.train_and_save()

    main = Main()
    main.run()
