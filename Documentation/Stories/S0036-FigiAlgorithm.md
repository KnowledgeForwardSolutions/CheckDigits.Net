# FigiAlgorithm

As a developer of CheckDigits.Net, I want CheckDigits.Net to include the algorithm for validating Financial Instrument Global Identifiers (FIGI).

https://en.wikipedia.org/wiki/Financial_Instrument_Global_Identifier
https://www.openfigi.com/assets/content/figi-check-digit-2173341b2d.pdf

## Requirements

* FigiAlgorithm class that implements the following interfaces:
	- ICheckDigitAlgorithm
* Resiliency. Invalid input should not throw an exception and instead should simply return Boolean false to indicate failure. Invalid input will include:
	- null
	- String.Empty

## Definition of DONE

1. FigiAlgorithm class
1. Full unit tests
1. README updates
1. Performance benchmarks