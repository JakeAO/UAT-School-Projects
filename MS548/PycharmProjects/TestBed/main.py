import nltk
from nltk.chunk.regexp import RegexpParser


# chunker which is designed to parse (relatively simple) ability text into meaningful components
chunker = RegexpParser(
    '''
    NP: {<DT|VB>? <JJ>* <NN>*}
    V: {<V.*>}
    VP: {<V|NP|NNP> <CD>? <NP|PP>*}
    COST: {<.*> <CD> <:>}
    COST: {<VP> <:>}
    EFFECT: {<V|VP> <TO> <NP>}
    EFFECT: {<VP>}
    EFFECT: {<NNP> <EFFECT>}
    EFFECT: {<EFFECT> <TO> <EFFECT>}
    ''')


def parse_ability_text(text: str) -> nltk.tree.Tree:
    tokens = nltk.word_tokenize(text)  # tokenize the string into individual words
    tags = nltk.pos_tag(tokens)  # tag each word with it's part-of-speech
    return chunker.parse(tags)  # parse tagged text into meaningful chunks tree


all_ability_text = [
    "Sacrifice a card: Destroy target card.",
    "Draw a card.",
    "Pay 2: Draw a card.",
    "Pay 123: Destroy target green creature.",
    "Pay 999: Destroy target creature.",
    "Deal 10 damage to target creature.",
    "Pay 2: Deal 1 damage to each opponent.",
    "Pay 2: Draw a card, then discard a card.",
]


def unpack_tree(tree: nltk.tree.Tree) -> str:
    result: str = ""
    for leaf in tree.leaves():
        result += leaf[0] + " "
    result = result.strip("\'':\\/ ")
    return result


if __name__ == '__main__':
    print()
    for ability_text in all_ability_text:
        tree = parse_ability_text(ability_text)
        # print(tree)
        print(f"[[ {ability_text} ]]")
        for subtree in tree:
            if isinstance(subtree, nltk.tree.Tree):
                if subtree.label() == 'COST':
                    print(f"Cost: {unpack_tree(subtree)}")
                elif subtree.label() == 'EFFECT':
                    print(f"Effect: {unpack_tree(subtree)}")
        print()
        # tree.draw()
