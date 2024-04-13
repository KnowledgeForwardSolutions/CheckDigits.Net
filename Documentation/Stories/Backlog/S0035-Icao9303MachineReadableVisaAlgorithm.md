# Icao9303MachineReadableVisaAlgorithm

As a developer of CheckDigits.Net, I want CheckDigits.Net to include the algorithm for validating ICAO 9303 Machine Readable Visas. Machine Readable Visas include check digits for individual fields.

A Machine Readable Visa contains two lines of machine readable data of 36 characters. The second line contains fields for the document number, date of birth and valid until date. All of the fields have associated check digits.

https://en.wikipedia.org/wiki/Machine-readable_passport#Official_travel_documents
https://www.icao.int/publications/Documents/9303_p7_cons_en.pdf

## Requirements

* Icao9303MachineReadableVisaAlgorithm class that implements the following interfaces:
	- ICheckDigitAlgorithm
* Resiliency. Invalid input should not throw an exception and instead should simply return Boolean false to indicate failure. Invalid input will include:
	- null
	- String.Empty

## Definition of DONE

1. Icao9303MachineReadableVisaAlgorithm class
1. Full unit tests
1. README updates
1. Performance benchmarks