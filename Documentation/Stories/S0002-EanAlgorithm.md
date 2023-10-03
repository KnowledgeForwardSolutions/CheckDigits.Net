# S0002-Modulus10_13Algorithm

As a developer of CheckDigits.Net, I want CheckDigits.Net to include the UPC/EAN algorithm (modulus 10 with weights 1, 3) in the list of supported algorithms.

## Requirements

* Modulus10_13Algorithm class that implements the following interfaces:
	- ICheckDigitAlgorithm
	- ISingleCheckDigitAlgorithm
* Resiliency. Invalid input should not throw an exception and instead should simply return Boolean false to indicate failure. Invalid input will include:
	- null
	- String.Empty
	- Strings of length < 2 (Validate method only)
	- Strings containing non-digit characters (i.e. not 0-9).

## Definition of DONE

1. Modulus10_13Algorithm class
1. Full unit tests
1. README updates
1. Performance benchmarks