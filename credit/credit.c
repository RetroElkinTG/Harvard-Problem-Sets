#include <cs50.h>
#include <math.h>
#include <stdio.h>
#include <stdlib.h>

int main(void)
{
    // Get card number
    long cardNumber;
    do
    {
        cardNumber = get_long("Number: ");
    }
    while (cardNumber < 0);

    // Sum every second digit starting from second-last number
    long tempNumber = cardNumber / 10;
    int sum = 0;
    do
    {
        int digit = tempNumber % 100;
        int firstDigit = digit % 10;
        int newNumber = firstDigit * 2;
        if (newNumber >= 10)
        {
            newNumber = newNumber % 10 + 1;
        }
        sum += newNumber;
        tempNumber = tempNumber / 100;
    }
    while (tempNumber > 0);

    // Sum every second digits starting from last number
    long tempNumber2 = cardNumber;
    do
    {
        int digit = tempNumber2 % 100;
        int lastDigit = digit % 10;
        sum += lastDigit;
        tempNumber2 = tempNumber2 / 100;
    }
    while (tempNumber2 > 0);

    // Check if card is valid
    int numDigits = floor(log10(labs(cardNumber))) + 1;
    long firstTwoDigits = cardNumber;
    while (firstTwoDigits > 99)
    {
        firstTwoDigits = firstTwoDigits / 10;
    }
    if (sum % 10 == 0)
    {
        if ((numDigits == 13 || numDigits == 16) && firstTwoDigits >= 40 && firstTwoDigits <= 49)
        {
            printf("VISA\n");
        }
        else if (numDigits == 15 && (firstTwoDigits == 34 || firstTwoDigits == 37))
        {
            printf("AMEX\n");
        }
        else if (numDigits == 16 && firstTwoDigits >= 51 && firstTwoDigits <= 55)
        {
            printf("MASTERCARD\n");
        }
        else
        {
            printf("INVALID\n");
        }
    }
    else
    {
        printf("INVALID\n");
    }
}
