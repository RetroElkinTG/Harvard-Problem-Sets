import csv
import sys


def main():

    # TODO: Check for command-line usage
    if len(sys.argv) == 3:
        database = sys.argv[1]
        dna = sys.argv[2]
    else:
        print("Usage: python dna.py databases/small.csv sequences/1.txt")
        sys.exit(1)

    # TODO: Read database file into a variable
    rows = []
    with open(database) as file:
        reader = csv.DictReader(file)
        person = reader.fieldnames
        for row in reader:
            rows.append(row)

    # TODO: Read DNA sequence file into a variable
    with open(dna) as file:
        dna_string = file.read()

    # TODO: Find longest match of each STR in DNA sequence
    match_dict = {}
    i = 1
    while i < len(person):
        value = longest_match(dna_string, person[i])
        match_dict[person[i]] = value
        i += 1

    # TODO: Check database for matching profiles
    for row in rows:
        is_match = True
        for match in match_dict:
            if row[match] == str(match_dict[match]):
                continue
            else:
                is_match = False
                break
        if is_match:
            print(row['name'])
            return
    print("No match")
    return


def longest_match(sequence, subsequence):
    """Returns length of longest run of subsequence in sequence."""

    # Initialize variables
    longest_run = 0
    subsequence_length = len(subsequence)
    sequence_length = len(sequence)

    # Check each character in sequence for most consecutive runs of subsequence
    for i in range(sequence_length):

        # Initialize count of consecutive runs
        count = 0

        # Check for a subsequence match in a "substring" (a subset of characters) within sequence
        # If a match, move substring to next potential match in sequence
        # Continue moving substring and checking for matches until out of consecutive matches
        while True:

            # Adjust substring start and end
            start = i + count * subsequence_length
            end = start + subsequence_length

            # If there is a match in the substring
            if sequence[start:end] == subsequence:
                count += 1

            # If there is no match in the substring
            else:
                break

        # Update most consecutive matches found
        longest_run = max(longest_run, count)

    # After checking for runs at each character in seqeuence, return longest run found
    return longest_run


main()
