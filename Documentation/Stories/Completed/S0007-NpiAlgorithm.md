# S0007-NpiAlgorithm

As a developer of CheckDigits.Net, I want CheckDigits.Net to include the US 
National Provider Identifier (NPI)check digit algorithm in the list of supported 
algorithms. The NPI algorithm employs the Luhn algorithm but first prefixes the 
value with a constant "80840" before calculating the check digit.

## Requirements

* NpiAlgorithm class that implements the following interfaces:
	- ICheckDigitAlgorithm
	- ISingleCheckDigitAlgorithm
* Resiliency. Invalid input should not throw an exception and instead should simply return Boolean false to indicate failure. Invalid input will include:
	- null
	- String.Empty
	- Strings of invalid length (9 for TryGenerateCheckDigit and 10 for Validate)
	- Strings containing non-digit characters (i.e. not 0-9).
* Should not allocate additional memory (i.e. should not create a new copy of the value) while calculating the check digit

## Definition of DONE

1. NpiAlgorithm class
1. Full unit tests
1. README updates
1. Performance benchmarks