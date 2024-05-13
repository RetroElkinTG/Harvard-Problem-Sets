#include <cs50.h>
#include <ctype.h>
#include <stdio.h>
#include <string.h>

string uppercase(string s);
int score(string s);

int main(void)
{
    // Get player word
    string word1 = uppercase(get_string("Player 1: ")), word2 = uppercase(get_string("Player 2: "));

    // Get player score
    int score1 = score(word1), score2 = score(word2);

    // Print the winner
    if (score1 > score2)
    {
        printf("Player 1 wins!\n");
    }
    else if (score2 > score1)
    {
        printf("Player 2 wins!\n");
    }
    else
    {
        printf("Tie!\n");
    }
}

// Convert word to uppercase
string uppercase(string s)
{
    int n = strlen(s);
    for (int i = 0; i < n; i++)
    {
        s[i] = toupper(s[i]);
    }
    return s;
}

// Find score of word
int score(string s)
{
    int n = strlen(s), score = 0;
    for (int i = 0; i < n; i++)
    {
        if (strchr("AEILNORSTU", s[i]) != NULL)
        {
            score += 1;
        }
        else if (strchr("DG", s[i]) != NULL)
        {
            score += 2;
        }
        else if (strchr("BCMP", s[i]) != NULL)
        {
            score += 3;
        }
        else if (strchr("FHVWY", s[i]) != NULL)
        {
            score += 4;
        }
        else if (strchr("K", s[i]) != NULL)
        {
            score += 5;
        }
        else if (strchr("JX", s[i]) != NULL)
        {
            score += 8;
        }
        else if (strchr("QZ", s[i]) != NULL)
        {
            score += 10;
        }
    }
    return score;
}
