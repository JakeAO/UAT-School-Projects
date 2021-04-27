from input_data import InputData
import imutils
import cv2


class PreProcessor:
    def process(self, file_path: str) -> InputData:
        image = cv2.imread(file_path)
        grey = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)
        blurred = cv2.GaussianBlur(grey, (5, 5), 0)
        edges = cv2.Canny(blurred, 30, 150)
        contours = cv2.findContours(edges.copy(), cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)
        contours = imutils.grab_contours(contours)
        contours = self.sort_contours(contours)

        return InputData(file_path, image, grey, contours)

    @staticmethod
    def sort_contours(contours) -> []:
        bounding_boxes = [cv2.boundingRect(c) for c in contours]
        (contours, bounding_boxes) = zip(*sorted(
            zip(contours, bounding_boxes),
            key=lambda b: (
                round(b[1][1] / 100.0) * 100,  # order by Y bucketed to 100 pixels
                b[1][0]  # then by X
            )
        )
                                         )
        return contours
