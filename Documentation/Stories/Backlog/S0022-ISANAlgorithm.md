# S0022-ISANlgorithm

As a developer of CheckDigits.Net, I want CheckDigits.Net to include the ISAN (International Standard Audiovisual Number) check digit algorithm in the list of supported algorithms.

https://www.isan.org/docs/isan_check_digit_calculation_v2.0.pdf

Note that the ISAN specification includes two check digits, one for the root segment that appears in the 17th character position and a second check digit for the root + version segment that appears in the 26th character position.

## Requirements

* IsanAlgorithm class that implements the following interfaces:
	- ICheckDigitAlgorithm
	- IDoubleCheckDigitAlgorithm
* Resiliency. Invalid input should not throw an exception and instead should simply return Boolean false to indicate failure. Invalid input will include:
	- null
	- String.Empty

## Definition of DONE

1. IsanAlgorithm class
1. Full unit tests
1. README updates
1. Performance benchmarks