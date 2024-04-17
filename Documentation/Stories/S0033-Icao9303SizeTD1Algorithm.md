# Icao9303SizeTD1Algorithm

As a developer of CheckDigits.Net, I want CheckDigits.Net to include the algorithm for validating ICAO 9303 Size TD1 documents (Machine Readable Official Travel Documents). Size TD1 documents include check digits for individual fields as well as a composite check digit for all of the fields containing check digits.

A size TD1 document contains three lines of machine readable data of 30 characters. The first line contains a field for the document number. The second line contains fields for date of birth, date of expiry and a field for optional data elements. All of the fields have associated check digits. In addition the second line contains a composite check digit calculated for all of the fields and their check digits.

https://en.wikipedia.org/wiki/Machine-readable_passport#Official_travel_documents
https://www.icao.int/publications/Documents/9303_p5_cons_en.pdf

## Requirements

* Icao9301SizeTD1Algorithm class that implements the following interfaces:
	- ICheckDigitAlgorithm
* Resiliency. Invalid input should not throw an exception and instead should simply return Boolean false to indicate failure. Invalid input will include:
	- null
	- String.Empty

## Definition of DONE

1. Icao9303SizeTD1Algorithm class
1. Full unit tests
1. README updates
1. Performance benchmarks