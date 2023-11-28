# S0027-SedolAlgorithm

As a developer of CheckDigits.Net, I want CheckDigits.Net to include the SEDOL check digit algorithm in the list of supported algorithms.

https://en.wikipedia.org/wiki/SEDOL

## Requirements

* SedolAlgorithm class that implements the following interfaces:
	- ICheckDigitAlgorithm
* Resiliency. Invalid input should not throw an exception and instead should simply return Boolean false to indicate failure. Invalid input will include:
	- null
	- String.Empty

## Definition of DONE

1. SedolAlgorithm class
1. Full unit tests
1. README updates
1. Performance benchmarks