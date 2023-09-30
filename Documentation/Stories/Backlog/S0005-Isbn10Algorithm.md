# S0001-Isbn10Algorithm

As a developer of CheckDigits.Net, I want CheckDigits.Net to include the ISBN-10
algorithm in the list of supported algorithms.

## Requirements

* Isbn10Algorithm class that implements the following interfaces:
	- ICheckDigitAlgorithm
	- IGenerateSingleCheckDigit
	- IValidateCheckDigit
* Resiliency. Invalid input should not throw an exception and instead should simply return Boolean false to indicate failure. Invalid input will include:
	- null
	- String.Empty
	- Strings of invalid length (9 for IGenerateSingleCheckDigit.TryGenerateCheckDigit and 10 for IValidateCheckDigit.Validate)
	- Strings containing non-digit characters (i.e. not 0-9).

## Definition of DONE

1. Isbn10Algorithm class
1. Full unit tests
1. README updates
1. Performance benchmarks