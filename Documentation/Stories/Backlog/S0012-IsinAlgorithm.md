# S0012-IsinAlgorithm

As a developer of CheckDigits.Net, I want CheckDigits.Net to include the International
Securities Identification Number (ISIN) algorithm in the list of supported algorithms.

## Requirements

* IsinAlgorithm class that implements the following interfaces:
	- ICheckDigitAlgorithm
	- ISingleCheckDigitAlgorithm
* Resiliency. Invalid input should not throw an exception and instead should simply return Boolean false to indicate failure. Invalid input will include:
	- null
	- String.Empty
	- Strings of invalid length (12 characters)

## Definition of DONE

1. IsinAlgorithm class
1. Full unit tests
1. README updates
1. Performance benchmarks