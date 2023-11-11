# S0013-NoidAlgorithm

As a developer of CheckDigits.Net, I want CheckDigits.Net to include the NOID check digit algorithm in the list of supported algorithms.

https://metacpan.org/dist/Noid/view/noid

## Requirements

* NoidAlgorithm class that implements the following interfaces:
	- ICheckDigitAlgorithm
	- ISingleCheckDigitAlgorithm
* Resiliency. Invalid input should not throw an exception and instead should simply return Boolean false to indicate failure. Invalid input will include:
	- null
	- String.Empty

## Definition of DONE

1. NoidAlgorithm class
1. Full unit tests
1. README updates
1. Performance benchmarks