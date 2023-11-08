# S0021-ULIAlgorithm

As a developer of CheckDigits.Net, I want CheckDigits.Net to include the ULI (Universal Load Identifier) check digit algorithm in the list of supported algorithms.

https://www.consumerfinance.gov/rules-policy/regulations/1003/c/#8b759d2c74679b99455472481cd454ad70e3436aba9a855d42fea3aa

## Requirements

* UliAlgorithm class that implements the following interfaces:
	- ICheckDigitAlgorithm
	- IDoubleCheckDigitAlgorithm
* Resiliency. Invalid input should not throw an exception and instead should simply return Boolean false to indicate failure. Invalid input will include:
	- null
	- String.Empty

## Definition of DONE

1. UliAlgorithm class
1. Full unit tests
1. README updates
1. Performance benchmarks