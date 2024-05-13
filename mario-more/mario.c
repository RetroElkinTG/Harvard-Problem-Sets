#include <cs50.h>
#include <stdio.h>

int main(void)
{
    int n;
    do
    {
        n = get_int("Size: ");
    }
    while (n < 1);

    int leftCount = n - 1;
    int rightCount = 0;
    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < n; j++)
        {
            if (leftCount <= j)
            {
                printf("#");
            }
            else
            {
                printf(" ");
            }
        }
        printf("  ");
        for (int j = 0; j < n; j++)
        {
            if (rightCount >= j)
            {
                printf("#");
            }
        }
        leftCount--;
        rightCount++;
        printf("\n");
    }
}
