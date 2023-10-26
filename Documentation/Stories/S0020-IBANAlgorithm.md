# S0020-IBANAlgorithm

As a developer of CheckDigits.Net, I want CheckDigits.Net to include the IBAN (International Bank Account Number) check digit algorithm in the list of supported algorithms.

https://en.wikipedia.org/wiki/International_Bank_Account_Number

## Requirements

* IbanAlgorithm class that implements the following interfaces:
	- ICheckDigitAlgorithm
	- IDoubleCheckDigitAlgorithm
* Resiliency. Invalid input should not throw an exception and instead should simply return Boolean false to indicate failure. Invalid input will include:
	- null
	- String.Empty

## Definition of DONE

1. IbanAlgorithm class
1. Full unit tests
1. README updates
1. Performance benchmarks