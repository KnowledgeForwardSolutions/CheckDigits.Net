# Modulus10_731Algorithm

As a developer of CheckDigits.Net, I want CheckDigits.Net to include the modulus 10 with weights 7, 3, 1 check digit algorithm used  by ICAO (International Civil Aviation Organization) MRTODT (Machine Readable Official Travel Documents) in the list of supported algorithms.

https://en.wikipedia.org/wiki/Machine-readable_passport#Official_travel_documents
https://www.icao.int/publications/Documents/9303_p3_cons_en.pdf

## Requirements

* New IEmbeddedCheckDigitAlgorithm inteface that defines a Boolean Validate(String value, Int32 start, Int32 length) method for validating the check digit of a field that is embedded in a larger string.
* Modulus10_731Algorithm class that implements the following interfaces:
	- ICheckDigitAlgorithm
	- IEmbeddedCheckDigitAlgorithm
* Resiliency. Invalid input should not throw an exception and instead should simply return Boolean false to indicate failure. Invalid input will include:
	- null
	- String.Empty

## Definition of DONE

1. Modulus10_731Algorithm class
1. Full unit tests
1. README updates
1. Performance benchmarks