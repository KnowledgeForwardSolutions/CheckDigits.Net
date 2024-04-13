# Icao9303SizeTD1Algorithm

As a developer of CheckDigits.Net, I want CheckDigits.Net to include the algorithm for validating ICAO 9303 Size TD2 documents (Machine Readable Official Travel Documents). Size TD2 documents include check digits for individual fields as well as a composite check digit for all of the fields containing check digits.

A size TD2 document contains two lines of machine readable data of 36 characters. The second line contains fields for the document number, date of birth and date of expiry. All of the fields have associated check digits. In addition the second line contains a composite check digit calculated for all of the fields and their check digits.

https://en.wikipedia.org/wiki/Machine-readable_passport#Official_travel_documents
https://www.icao.int/publications/Documents/9303_p6_cons_en.pdf

## Requirements

* Icao9301SizeTD2Algorithm class that implements the following interfaces:
	- ICheckDigitAlgorithm
* Resiliency. Invalid input should not throw an exception and instead should simply return Boolean false to indicate failure. Invalid input will include:
	- null
	- String.Empty

## Definition of DONE

1. Icao9303SizeTD2Algorithm class
1. Full unit tests
1. README updates
1. Performance benchmarks