#include <cs50.h>
#include <ctype.h>
#include <math.h>
#include <stdio.h>
#include <string.h>

int wordCount(string text);
float averageLetters(string text);
float averageSentences(string text);

int main(void)
{
    string text = get_string("Text: ");
    float L = averageLetters(text);
    float S = averageSentences(text);
    int index = (int) round(0.0588 * L - 0.296 * S - 15.8);
    if (index < 1)
    {
        printf("Before Grade 1\n");
    }
    else if (index >= 16)
    {
        printf("Grade 16+\n");
    }
    else
    {
        printf("Grade %i\n", index);
    }
}

// Word count
int wordCount(string text)
{
    int wordCount = 1;
    for (int i = 0, n = strlen(text); i < n; i++)
    {
        if (text[i] == ' ')
        {
            wordCount += 1;
        }
    }
    return wordCount;
}

// Average letters per 100 words
float averageLetters(string text)
{
    int letterCount = 0;
    for (int i = 0, n = strlen(text); i < n; i++)
    {
        if (isupper(text[i]) || islower(text[i]))
        {
            letterCount += 1;
        }
    }
    float averageLetters = ((float) letterCount * 100) / (float) wordCount(text);
    return averageLetters;
}

// Average sentences per 100 words
float averageSentences(string text)
{
    int sentenceCount = 0;
    for (int i = 0, n = strlen(text); i < n; i++)
    {
        if (strchr(".!?", text[i]) != NULL)
        {
            sentenceCount += 1;
        }
    }
    float averageSentences = ((float) sentenceCount * 100) / (float) wordCount(text);
    return averageSentences;
}
