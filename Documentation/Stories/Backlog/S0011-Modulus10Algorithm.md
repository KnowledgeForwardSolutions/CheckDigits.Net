# S0011-Modulus10Algorithm

As a developer of CheckDigits.Net, I want CheckDigits.Net to include the modulus 10
algorithm used by a variety of identifiers (IMO/International Maritime Organization,
Chemical Abstract System (CAS) Registry Number, etc.) 
in the list of supported algorithms.

## Requirements

*Modulus10Algorithm class that implements the following interfaces:
	- ICheckDigitAlgorithm
	- ISingleCheckDigitAlgorithm
* Resiliency. Invalid input should not throw an exception and instead should simply return Boolean false to indicate failure. Invalid input will include:
	- null
	- String.Empty
	- Strings of length < 2 (Validate only)

## Definition of DONE

1. Modulus10Algorithm class
1. Full unit tests
1. README updates
1. Performance benchmarks