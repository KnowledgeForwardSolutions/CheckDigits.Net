# S0008-VinAlgorithm

As a developer of CheckDigits.Net, I want CheckDigits.Net to include the algorithm
used for North American (US and Canada) Vehicle Identification Number (VIN) in
the list of supported algorithms.

## Requirements

* VinAlgorithm class that implements the following interfaces:
	- ICheckDigitAlgorithm
	- ISingleCheckDigitAlgorithm
* Resiliency. Invalid input should not throw an exception and instead should simply return Boolean false to indicate failure. Invalid input will include:
	- null
	- String.Empty
	- Strings of invalid length (17 characters)
* VinAlgorithm class should expose TransliterateCharacter method that correctly transliterates non-digit characters to their integer equivalent

## Definition of DONE

1. VinAlgorithm class
1. Full unit tests
1. README updates
1. Performance benchmarks