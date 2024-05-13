-- Keep a log of any SQL queries you execute as you solve the mystery.

-- Investigate crime scene report

SELECT description FROM crime_scene_reports
WHERE year = 2023
AND month = 7
AND day = 28
AND street = 'Humphrey Street';

-- Theft of the CS50 duck took place at 10:15am at the Humphrey Street
-- bakery. Interviews were conducted today with three witnesses
-- who were present at the time - each of their interview transcripts
-- mentions the bakery.


-- Investigate interviews at bakery.

SELECT name, transcript FROM interviews
WHERE year = 2023
AND month = 7
AND day = 28;

-- Ruth: Within ten minutes of the theft, I saw the thief get into a car
-- in the bakery parking lot and drive away. If you have security footage
-- from the bakery parking lot, you might want to look for cars that left
-- the parking lot in that time frame.

-- Eugene: Earlier in the morning, I was walking by the ATM on Leggett Street
-- and saw the thief withdrawing money.

-- Raymond: The thief called someone for less than a minute. They are planning
-- to take the earliest flight out of Fiftyville tomorrow. The thief asked
-- the person on the other end of the phone to purchase the flight ticket.


-- Investigate bakery security logs

SELECT license_plate FROM bakery_security_logs
WHERE year = 2023
AND month = 7
AND day = 28
AND hour = 10
AND minute >= 15
AND minute <= 25
AND activity = 'exit';

-- Lots of License Plates found exiting.


-- Investigate ATM on Leggett Street

SELECT account_number FROM atm_transactions
WHERE year = 2023
AND month = 7
AND day = 28
AND atm_location = 'Leggett Street'
AND transaction_type = 'withdraw';

-- Lots of account numbers


-- Investigate bank accounts

SELECT person_id FROM bank_accounts
WHERE account_number IN
(
SELECT account_number FROM atm_transactions
WHERE year = 2023
AND month = 7
AND day = 28
AND atm_location = 'Leggett Street'
AND transaction_type = 'withdraw'
)

--


-- Investigate phone calls

SELECT receiver FROM phone_calls
WHERE year = 2023
AND month = 7
AND day = 28
AND duration < 60
AND caller = '(367) 555-5533'

-- Lots of phone numbers


-- Investigate airport

SELECT city, full_name, abbreviation FROM airports
WHERE id = 4;

-- The thief went to LaGuardia Airport (LGA), New York City.


-- Investigate flights

SELECT id, origin_airport_id, destination_airport_id FROM flights
WHERE year = 2023
AND month = 7
AND day = 29
ORDER BY hour ASC;

-- Flight ID: 36 | Origin Airport ID: 8 | Destination Airport ID: 4


-- Investigate flight passengers

SELECT passport_number, seat FROM passengers
WHERE flight_id = 36;

-- Lots of passports

-- Find Thief

SELECT name, phone_number FROM people

WHERE license_plate IN
(
SELECT license_plate FROM bakery_security_logs
WHERE year = 2023
AND month = 7
AND day = 28
AND hour = 10
AND minute >= 15
AND minute <= 25
AND activity = 'exit'
)

AND passport_number IN
(
SELECT passport_number FROM passengers
WHERE flight_id = 36
)

AND phone_number IN
(
SELECT caller FROM phone_calls
WHERE year = 2023
AND month = 7
AND day = 28
AND duration < 60
)

AND id IN
(
SELECT person_id FROM bank_accounts
WHERE account_number IN
(
SELECT account_number FROM atm_transactions
WHERE year = 2023
AND month = 7
AND day = 28
AND atm_location = 'Leggett Street'
AND transaction_type = 'withdraw'
)
);

-- Bruce


-- Accomplice's number is (375) 555-8161
SELECT name FROM people
WHERE phone_number = '(375) 555-8161';
