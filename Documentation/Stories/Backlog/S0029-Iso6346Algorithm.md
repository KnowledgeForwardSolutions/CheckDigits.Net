# S0029-Iso6346Algorithm

As a developer of CheckDigits.Net, I want CheckDigits.Net to include the ISO 6346 check digit algorithm for shipping container codes in the list of supported algorithms.

https://en.wikipedia.org/wiki/ISO_6346
https://www.bic-code.org/check-digit-calculator/

## Requirements

* Iso6346Algorithm class that implements the following interfaces:
	- ICheckDigitAlgorithm
	- ISingleCheckDigitAlgorithm
* Resiliency. Invalid input should not throw an exception and instead should simply return Boolean false to indicate failure. Invalid input will include:
	- null
	- String.Empty

## Definition of DONE

1. Iso6346Algorithm class
1. Full unit tests
1. README updates
1. Performance benchmarks