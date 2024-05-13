import math


def get_int(prompt):
    while True:
        try:
            return int(input(prompt))
        except ValueError:
            print("Not an integer")


def sum_sl(sum, tempNumber):
    digit = tempNumber % 100
    firstDigit = digit % 10
    newNumber = firstDigit * 2
    if newNumber >= 10:
        newNumber = newNumber % 10 + 1
    sum += newNumber
    tempNumber = tempNumber // 100
    return sum, tempNumber


def sum_l(sum, tempNumber2):
    digit = tempNumber2 % 100
    lastDigit = digit % 10
    sum += lastDigit
    tempNumber2 = tempNumber2 // 100
    return sum, tempNumber2


def main():
    cardNumber = -1
    while cardNumber < 0:
        cardNumber = get_int("Number: ")

    tempNumber = cardNumber // 10
    sum = 0
    sum, tempNumber = sum_sl(sum, tempNumber)
    while tempNumber > 0:
        sum, tempNumber = sum_sl(sum, tempNumber)

    tempNumber2 = cardNumber
    sum, tempNumber2 = sum_l(sum, tempNumber2)
    while tempNumber2 > 0:
        sum, tempNumber2 = sum_l(sum, tempNumber2)

    numDigits = math.floor(math.log10(abs(cardNumber))) + 1
    firstTwoDigits = cardNumber
    while firstTwoDigits > 99:
        firstTwoDigits = firstTwoDigits // 10
    if sum % 10 == 0:
        if (numDigits == 13 or numDigits == 16) and firstTwoDigits >= 40 and firstTwoDigits <= 49:
            print("VISA")
        elif numDigits == 15 and (firstTwoDigits == 34 or firstTwoDigits == 37):
            print("AMEX")
        elif numDigits == 16 and firstTwoDigits >= 51 and firstTwoDigits <= 55:
            print("MASTERCARD")
        else:
            print("INVALID")
    else:
        print("INVALID")


main()
