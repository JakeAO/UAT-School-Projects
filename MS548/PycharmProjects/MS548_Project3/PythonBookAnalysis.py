from textblob import TextBlob
from matplotlib import pyplot
from nltk.corpus import stopwords
import numpy
import numpy.random
import collections
import os
import re


# class which facilitates the book selection
class AnalysisWrapper:
    options = {}
    header: str
    prompt: str

    # initializes the wrapper and sets up the available book options
    def __init__(self) -> None:
        idx = 0
        for root, dirs, files in os.walk("Books", followlinks=False):
            for file in files:
                if '.txt' in file:
                    self.options[idx] = os.path.join(root, file)
                    idx += 1

        self.header = "[Python Book Analysis]\n" \
                      f"{len(self.options)} book files found in the Books directory.\n"
        self.prompt = "Select an option below to construct a subject summary and sentiment analysis.\n"
        for opt_id in self.options:
            self.prompt += f"   [{opt_id}] - {self.options[opt_id]}\n"
        self.prompt += "   [X] - Exit the program."

    # runs the wrapper and prompts users for their selections
    def run(self) -> None:
        print(self.header)

        while True:
            print(self.prompt)
            selection = input("Selection:")
            print()
            if selection == 'x' or selection == 'X':
                print("Thank you for using Python Book Analysis.")
                break
            try:
                int_selection = int(selection)
                file_selection = self.options.get(int_selection)
                if file_selection is not None:
                    analyzer = Analyzer(file_selection)
                    analyzer.analyze_book()
                    continue
            except ValueError:
                pass  # swallow int parsing errors, as they're handled by the generic "invalid choice" message
            print(f"'{selection}' is an invalid choice, please try again.")


# class which actually performs the book analysis
class Analyzer:
    book_path: str
    book_name: str
    summary_output_path: str
    sentiment_output_path: str

    book_contents_by_chapter: []

    # load the contents of the book into memory as an array of chapter strings
    def __load_book(self) -> None:
        print("   Loading chapter text...")

        chap_list = []
        with open(self.book_path, "rt", encoding="utf-8") as book_file:
            utf_lines = book_file.readlines()
            for utf_line in utf_lines:
                ascii_line = utf_line.encode("ascii", errors="ignore").decode().strip()
                if ascii_line.startswith(("Chapter", "CHAPTER")):
                    chap_list.append("")  # add a string to hold the new chapter
                    continue
                elif ascii_line is not None:
                    chap_list[len(chap_list) - 1] += ascii_line + " "  # append each line to the chapter string
                    continue
        self.book_contents_by_chapter = chap_list

    # initialize the Analyzer by setting the paths, name, and loading the active book
    def __init__(self, book_path: str) -> None:
        self.book_path = book_path

        book_name = os.path.basename(book_path)  # just the file, not the whole path
        book_name = os.path.splitext(book_name)[0]  # just the name, not the extension
        book_name = re.sub(r'(\w)([A-Z])', r'\1 \2', book_name)  # regex-insert some spaces between compacted book names
        self.book_name = book_name

        book_name_no_space = book_name.replace(' ', '')
        self.summary_output_path = f"Output/{book_name_no_space}_Summary.txt"
        self.sentiment_output_path = f"Output/{book_name_no_space}_Sentiment.jpg"

        print(f"Processing book \"{self.book_name}\" at path {self.book_path}")

        self.__load_book()

    # calculates the average and median sentiment values for each chapter of the active book
    def __get_sentiment_mapping(self) -> (([], []), ([], [])):
        avg_sub_by_chap = []
        med_sub_by_chap = []
        avg_pol_by_chap = []
        med_pol_by_chap = []
        for chapter in self.book_contents_by_chapter:
            blob = TextBlob(chapter)
            sentences = blob.sentences

            chapter_subjectivity = []
            chapter_polarity = []
            for sen in sentences:
                if len(sen) > 10:  # Ignore extreme short sentences (e.g. "Oh!", "Yes.", etc.)
                    chapter_subjectivity.append(sen.subjectivity)
                    chapter_polarity.append(sen.polarity)

            avg_chap_sub = numpy.average(chapter_subjectivity)
            med_chap_sub = numpy.median(chapter_subjectivity)
            avg_chap_pol = numpy.average(chapter_polarity)
            med_chap_pol = numpy.median(chapter_polarity)

            avg_sub_by_chap.append(avg_chap_sub)
            med_sub_by_chap.append(med_chap_sub)
            avg_pol_by_chap.append(avg_chap_pol)
            med_pol_by_chap.append(med_chap_pol)

        return (avg_sub_by_chap, med_sub_by_chap), (avg_pol_by_chap, med_pol_by_chap)

    # calculates proper noun pairs within each chapter and orders them by frequency
    def __get_proper_mapping(self) -> []:
        ignore_set = set(stopwords.words('english'))
        nnp_by_chap = []
        for chapter in self.book_contents_by_chapter:
            chap_list = []
            for sent in TextBlob(chapter).sentences:
                tagged_words = [tag[0].singularize().lower() for tag in sent.pos_tags
                                if tag[0] not in ignore_set
                                and len(tag[0]) > 4
                                and tag[1] == "NNP"]
                if len(tagged_words) == 0:
                    continue
                np_list = sent.noun_phrases
                for np in np_list:
                    for noun in TextBlob(np).words.singularize():
                        if noun in tagged_words:
                            chap_list.append(np.singularize().title())
            nnp_by_chap.append(collections.Counter(chap_list))
        return nnp_by_chap

    # outputs chapter summaries to the book's corresponding summary file
    def __print_chapter_summaries(self) -> None:
        print("   Calculating summary analysis...")

        nnp_by_chapter = self.__get_proper_mapping()

        with open(self.summary_output_path, "wt") as out_file:
            out_file.write("[MOST COMMON SUBJECTS BY CHAPTER]\n\n")
            for chapter_idx in range(0, len(nnp_by_chapter)):
                out_file.write(f"Chapter {chapter_idx + 1}\n")
                for nnp_pair in nnp_by_chapter[chapter_idx].most_common(6):
                    out_file.write(f"   {nnp_pair[0]}\n")
                out_file.write("\n")

    # outputs chapter sentiment graph to the book's corresponding sentiment file
    def __print_sentiment_graph(self) -> None:
        print("   Calculating sentiment analysis...")

        (sub_avg, sub_median), (pol_avg, pol_median) = self.__get_sentiment_mapping()
        avg_overall_pol = numpy.average(pol_avg)
        avg_overall_sub = numpy.average(sub_avg)

        fig, (ax1, ax2) = pyplot.subplots(2, sharex=True)
        pyplot.suptitle(f"[{self.book_name}]")
        pyplot.xlabel("Chapter")

        ax1.plot(pol_avg, label="Average")
        ax1.plot(pol_median, label="Median")
        ax1.hlines(avg_overall_pol, 0, len(self.book_contents_by_chapter), label="Book Average", linestyles="--")
        ax1.set_title("Polarity")
        ax1.legend()

        ax2.plot(sub_avg, label="Average")
        ax2.plot(sub_median, label="Median")
        ax2.hlines(avg_overall_sub, 0, len(self.book_contents_by_chapter), label="Book Average", linestyles="--")
        ax2.set_title("Subjectivity")

        fig.savefig(self.sentiment_output_path)

    # main 'run' method of the class
    def analyze_book(self) -> None:
        if not os.path.exists("Output"):
            os.mkdir("Output")  # output files are printed to the Output folder, so create it here

        self.__print_chapter_summaries()

        self.__print_sentiment_graph()

        print("   Operation complete, results can be found in the Output folder.\n"
              f"      Summary: {self.summary_output_path}\n"
              f"      Sentiment: {self.sentiment_output_path}\n")
