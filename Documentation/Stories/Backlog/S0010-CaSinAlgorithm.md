# S0010-CaSinAlgorithm

As a developer of CheckDigits.Net, I want CheckDigits.Net to include the algorithm
used for Canadian Social Insurance Number (SIN) in the list of supported algorithms.

## Requirements

*CaSinAlgorithm class that implements the following interfaces:
	- ICheckDigitAlgorithm
	- ISingleCheckDigitAlgorithm
* Resiliency. Invalid input should not throw an exception and instead should simply return Boolean false to indicate failure. Invalid input will include:
	- null
	- String.Empty
	- Strings of invalid length (9 characters)

## Definition of DONE

1. CaSinAlgorithm class
1. Full unit tests
1. README updates
1. Performance benchmarks