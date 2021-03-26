from textblob import TextBlob
from matplotlib import pyplot
from nltk.corpus import stopwords
import numpy
import numpy.random


def get_book(name: str) -> []:
    book_path = f"Books/{name.replace(' ', '')}.txt"

    chap_list = []
    with open(book_path, "rt", encoding="utf-8") as book_file:
        utf_lines = book_file.readlines()
        for utf_line in utf_lines:
            ascii_line = utf_line.encode("ascii", errors="ignore").decode().strip()
            if ascii_line.startswith(("Chapter", "CHAPTER")) and len(ascii_line) <= 10:
                chap_list.append("")
                continue
            elif ascii_line is not None:
                chap_list[len(chap_list) - 1] += ascii_line + " "
                continue
    return chap_list


def get_subjectivity_mapping(chap_list: []) -> ([], []):
    avg_by_chap = []
    med_by_chap = []
    for chapter in chap_list:
        blob = TextBlob(chapter)
        sentences = blob.sentences

        chapter_subjectivity = []
        for sen in sentences:
            chapter_subjectivity.append(sen.subjectivity)

        avg_chap = numpy.average(chapter_subjectivity)
        med_chap = numpy.median(chapter_subjectivity)

        avg_by_chap.append(avg_chap)
        med_by_chap.append(med_chap)

    return avg_by_chap, med_by_chap


def get_polarity_mapping(chap_list: []) -> ([], []):
    avg_by_chap = []
    med_by_chap = []
    for chapter in chap_list:
        blob = TextBlob(chapter)
        sentences = blob.sentences

        chapter_polarity = []
        for sen in sentences:
            chapter_polarity.append(sen.polarity)

        avg_chap = numpy.average(chapter_polarity)
        med_chap = numpy.median(chapter_polarity)

        avg_by_chap.append(avg_chap)
        med_by_chap.append(med_chap)

    return avg_by_chap, med_by_chap


def get_proper_mapping(chap_list: []) -> []:
    ignore_set = set(stopwords.words('english'))
    nnp_by_chap = []
    for chapter in chap_list:
        chap_set = set()
        for sent in TextBlob(chapter).sentences:
            tags = sent.tags
            tags = [tag for tag in tags if tag[0] not in ignore_set and len(tag[0]) > 4 and tag[1] == "NNP"]
            for (word, tag) in tags:
                chap_set.add(word)
        nnp_by_chap.append(list(chap_set))
    return nnp_by_chap


def run(name: str) -> None:
    chapter_list = get_book(name)

    nnp_by_chapter = get_proper_mapping(chapter_list)

    chapter_summary = []
    chapter_summary.append(["Chapter", '', '', '', '', ''])
    for row_idx in range(1, len(nnp_by_chapter)+1):
        chapter_summary.append([str(row_idx)])
        for col_idx in range(1, 6):
            rand_idx = numpy.random.randint(len(nnp_by_chapter[row_idx-1]))
            word = nnp_by_chapter[row_idx-1][rand_idx]
            nnp_by_chapter[row_idx-1].remove(word)
            chapter_summary[row_idx].append(word)

    for row in chapter_summary:
        print(f"[{row[0]:0>4}] {row[1]:<15} {row[2]:<15} {row[3]:<15} {row[4]:<15} {row[5]:<15}")

    pol_avg, pol_median = get_polarity_mapping(chapter_list)
    sub_avg, sub_median = get_subjectivity_mapping(chapter_list)
    avg_overall_pol = numpy.average(pol_avg)
    avg_overall_sub = numpy.average(sub_avg)

    fig, (ax1, ax2) = pyplot.subplots(2, sharex=True)
    pyplot.suptitle(name)
    pyplot.xlabel("Chapter")

    ax1.plot(pol_avg, label="Average")
    ax1.plot(pol_median, label="Median")
    ax1.hlines(avg_overall_pol, 0, len(chapter_list), label="Book Average", linestyles="--")
    ax1.set_title("Polarity")
    ax1.legend()

    ax2.plot(sub_avg, label="Average")
    ax2.plot(sub_median, label="Median")
    ax2.hlines(avg_overall_sub, 0, len(chapter_list), label="Book Average", linestyles="--")
    ax2.set_title("Subjectivity")

    fig.show()


if __name__ == '__main__':
    run("Pride and Prejudice")
