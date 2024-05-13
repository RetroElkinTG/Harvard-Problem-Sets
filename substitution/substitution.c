#include <cs50.h>
#include <ctype.h>
#include <stdio.h>
#include <string.h>

string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
int alphaLength = 26;

bool isValid(string key, int keyLength);
string encrypt(string word, string key);
char encryptLetter(char letter, string key);

int main(int argc, string argv[])
{
    // Check if key is valid
    if (argc == 2)
    {
        string key = argv[1];
        int keyLength = strlen(argv[1]);
        bool keyIsAlpha = isValid(key, keyLength);
        if (keyLength == alphaLength && keyIsAlpha)
        {
            string word = get_string("plaintext: ");
            string encryptedWord = encrypt(word, key);
            printf("ciphertext: %s\n", encryptedWord);
            return 0;
        }
        else if (keyIsAlpha)
        {
            printf("Key must contain 26 characters.\n");
            return 1;
        }
        else
        {
            printf("Usage: ./substitution key\n");
            return 1;
        }
    }
    else
    {
        printf("Usage: ./substitution key\n");
        return 1;
    }
}

// Check if key is valid
bool isValid(string key, int keyLength)
{
    for (int i = 0; i < alphaLength; i++)
    {
        if (!isalpha(key[i]))
        {
            return false;
        }
        for (int j = 0; j < alphaLength; j++)
        {
            if (key[i] == key[j] && i != j)
            {
                return false;
            }
        }
    }
    return true;
}

// Encrypt word
string encrypt(string word, string key)
{
    string encryptedWord = word;
    for (int i = 0; i < strlen(word); i++)
    {
        encryptedWord[i] = encryptLetter(encryptedWord[i], key);
    }
    return encryptedWord;
}

// Match letter with key letter
char encryptLetter(char letter, string key)
{
    for (int i = 0; i < alphaLength; i++)
    {
        if (letter == alphabet[i])
        {
            letter = toupper(key[i]);
            break;
        }
        else if (letter == tolower(alphabet[i]))
        {
            letter = tolower(key[i]);
            break;
        }
    }
    return letter;
}
