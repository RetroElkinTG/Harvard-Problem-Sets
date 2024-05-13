# Readability Python script

from cs50 import get_string


def main():
    text = get_string("Text: ")
    L = averageLetters(text)
    S = averageSentences(text)
    index = round(0.0588 * L - 0.296 * S - 15.8)
    if index < 1:
        print("Before Grade 1")
    elif index >= 16:
        print("Grade 16+")
    else:
        print("Grade " + str(index))


def averageLetters(text):
    letterCount = 0
    for i in range(len(text)):
        if (text[i].isalpha()):
            letterCount += 1
    averageLetters = (letterCount * 100) / wordCount(text)
    return averageLetters


def averageSentences(text):
    sentenceCount = 0
    sentenceEnders = [".", "!", "?"]
    for i in range(len(text)):
        if any(sentenceEnder in sentenceEnders for sentenceEnder in text[i]):
            sentenceCount += 1
    averageSentences = (sentenceCount * 100) / wordCount(text)
    return averageSentences


def wordCount(text):
    wordCount = 1
    for i in range(len(text)):
        if text[i] == ' ':
            wordCount += 1
    return wordCount


main()
