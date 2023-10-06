# S0006-AbaAlgorithm

As a developer of CheckDigits.Net, I want CheckDigits.Net to include the ABA
routing number check digit algorithm in the list of supported algorithms.

## Requirements

* AbaAlgorithm class that implements the following interfaces:
	- ICheckDigitAlgorithm
	- ISingleCheckDigitAlgorithm
* Resiliency. Invalid input should not throw an exception and instead should simply return Boolean false to indicate failure. Invalid input will include:
	- null
	- String.Empty
	- Strings of invalid length (8 for TryGenerateCheckDigit and 9 for Validate)
	- Strings containing non-digit characters (i.e. not 0-9).

## Definition of DONE

1. AbaAlgorithm class
1. Full unit tests
1. README updates
1. Performance benchmarks