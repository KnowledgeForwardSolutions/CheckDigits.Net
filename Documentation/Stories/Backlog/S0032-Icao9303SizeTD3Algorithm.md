# Icao9303SizeTD3Algorithm

As a developer of CheckDigits.Net, I want CheckDigits.Net to include the algorithm for validating ICAO 9303 Size TD3 documents (Machine Readable Passports). Size TD3 documents include check digits for individual fields as well as a composite check digit for all of the fields containing check digits.

A size TD3 document contains two lines of machine readable data of 44 characters. The second line contains fields for passport number, date of birth, date of expiry and an optional personal number, all of which have associated check digits. In addition the second line contains a composite check digit calculated for all of the fields and their check digits.

https://en.wikipedia.org/wiki/Machine-readable_passport#Official_travel_documents
https://www.icao.int/publications/Documents/9303_p4_cons_en.pdf

## Requirements

* Icao9303SizeTD3Algorithm class that implements the following interfaces:
	- ICheckDigitAlgorithm
* Resiliency. Invalid input should not throw an exception and instead should simply return Boolean false to indicate failure. Invalid input will include:
	- null
	- String.Empty

## Definition of DONE

1. Icao9303SizeTD3Algorithm class
1. Full unit tests
1. README updates
1. Performance benchmarks