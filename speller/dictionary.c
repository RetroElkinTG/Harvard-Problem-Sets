// Implements a dictionary's functionality

#include <ctype.h>
#include <stdbool.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <strings.h>

#include "dictionary.h"

// Represents a node in a hash table
typedef struct node
{
    char word[LENGTH + 1];
    struct node *next;
} node;

// TODO: Choose number of buckets in hash table
const unsigned int N = 26;

// Hash table
node *table[N];

// Dictionary word count
int word_count = 0;

// Returns true if word is in dictionary, else false
bool check(const char *word)
{
    unsigned int i = hash(word);
    for (node *ptr = table[i]; ptr != NULL; ptr = ptr->next)
    {
        //printf("%s%s\n", word, ptr->word);
        if (strcasecmp(word, ptr->word) == 0)
        {
            return true;
        }
    }
    return false;
}

// Hashes word to a number
unsigned int hash(const char *word)
{
    // TODO: Improve this hash function
    return toupper(word[0]) - 'A';
}

// Loads dictionary into memory, returning true if successful, else false
bool load(const char *dictionary)
{
    FILE *source = fopen(dictionary, "r");
    if (source != NULL)
    {
        // Set all nodes to NULL
        for (int i = 0; i < N; i++)
        {
            table[i] = NULL;
        }

        // Sort dictionary into hash table
        char *word = malloc(sizeof(char) * LENGTH);
        if (word != NULL)
        {
            while (fscanf(source, "%s", word) != EOF)
            {
                // Copy word into linked list
                node *n = malloc(sizeof(node));
                if (n != NULL)
                {
                    strcpy(n->word, word);
                    unsigned int i = hash(n->word);
                    n->next = table[i];
                    table[i] = n;
                    word_count++;
                }
                else
                {
                    return false;
                }
            }
        }
        else
        {
            return false;
        }
        free(word);
        fclose(source);
        return true;
    }
    return false;
}

// Returns number of words in dictionary if loaded, else 0 if not yet loaded
unsigned int size(void)
{
    return word_count;
}

// Unloads dictionary from memory, returning true if successful, else false
bool unload(void)
{
    // Free memory
    for (int i = 0; i < N; i++)
    {
        node *ptr = table[i];
        while (ptr != NULL)
        {
            node *next = ptr->next;
            free(ptr);
            ptr = next;
        }
    }
    return true;
}
