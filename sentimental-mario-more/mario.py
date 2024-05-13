from cs50 import get_int

n = 0
while n < 1 or n > 8:
    n = get_int("Size: ")

leftCount = n - 1
rightCount = 0
for i in range(n):
    for j in range(n):
        if leftCount <= j:
            print("#", end="")
        else:
            print(" ", end="")
    print("  ", end="")
    for j in range(n):
        if rightCount >= j:
            print("#", end="")
    leftCount -= 1
    rightCount += 1
    print("")
