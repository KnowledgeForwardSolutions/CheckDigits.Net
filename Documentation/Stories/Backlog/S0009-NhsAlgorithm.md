# S0009-NhsAlgorithm

As a developer of CheckDigits.Net, I want CheckDigits.Net to include the algorithm
used for UK National Health Service (NHS) identifiers in the list of supported algorithms.

## Requirements

*NhsAlgorithm class that implements the following interfaces:
	- ICheckDigitAlgorithm
	- ISingleCheckDigitAlgorithm
* Resiliency. Invalid input should not throw an exception and instead should simply return Boolean false to indicate failure. Invalid input will include:
	- null
	- String.Empty
	- Strings of invalid length (10 characters)

## Definition of DONE

1. NhsAlgorithm class
1. Full unit tests
1. README updates
1. Performance benchmarks