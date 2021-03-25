from textblob import TextBlob
from matplotlib import pyplot
import numpy

if __name__ == '__main__':
    book_name = "TaleOfTwoCities"
    book_path = f"Books/{book_name}.txt"

    book_list = []
    with open(book_path, "rt", encoding="utf-8") as book_file:
        utf_lines = book_file.readlines()
        book_count = 0
        chapter_count = 0
        for utf_line in utf_lines:
            ascii_line = utf_line.encode("ascii", errors="ignore").decode().strip()
            if ascii_line.startswith(("Book", "BOOK")) and len(ascii_line) <= 7:
                book_count += 1
                chapter_count = 0
                book_list.append([])
                continue
            elif ascii_line.startswith(("Chapter", "CHAPTER")) and len(ascii_line) <= 10:
                chapter_count += 1
                book_list[book_count-1].append("")
                continue
            elif ascii_line:
                book_list[book_count-1][chapter_count-1] += ascii_line + " "
                continue

    book_sentiment = []
    for book_number in range(0, len(book_list)):
        book = book_list[book_number]
        for chapter_number in range(0, len(book)):
            chapter = book[chapter_number]
            blob = TextBlob(chapter)
            sentences = blob.sentences
            print("BOOK", book_number + 1, "/", "CHAPTER", chapter_number + 1)
            for sen in sentences:
                print(sen)
                print("Polarity:", sen.polarity)
                print("Subjectivity:", sen.subjectivity)
                print()
                book_sentiment.append(sen.polarity)

    pyplot.plot(book_sentiment)
    pyplot.ylabel("Polarity")
    pyplot.xlabel("Sentence")
    pyplot.title("Polarity Over Book")
    pyplot.suptitle(book_name)
    pyplot.show()
