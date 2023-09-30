# S0001-LuhnAlgorithm

As a developer of CheckDigits.Net, I want CheckDigits.Net to include the Luhn
algorithm in the list of supported algorithms.

## Requirements

* LuhnAlgorithm class that implements the following interfaces:
	- ICheckDigitAlgorithm
	- IGenerateSingleCheckDigit
	- IValidateCheckDigit
* Resiliency. Invalid input should not throw an exception and instead should simply return Boolean false to indicate failure. Invalid input will include:
	- null
	- String.Empty
	- Strings of length < 2 (IValidateCheckDigit.Validate only)
	- Strings containing non-digit characters (i.e. not 0-9).

## Definition of DONE

1. LuhnAlgorithm class
1. Full unit tests
1. README updates
1. Performance benchmarks