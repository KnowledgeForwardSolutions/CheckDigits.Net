# S0015-Iso7064SingleCheckDigitAlgorithm

As a developer of CheckDigits.Net, I want CheckDigits.Net to include the ISO 7064 family of check digit algorithms in the list of supported algorithms. This story is limited to ISO 7064 pure systems that generate a single check character. The supported algorithms are ISO/IEC 7064, MOD 11-2 and ISO/IEC 7064 MOD 37-2.


## Requirements

* Implement an abstract base class that supports all ISO 7064 pure systems
* Implement an abstract base class for ISO 7064 pure system single check character algorithms. Should implement ISingleCheckDigitAlgorithm
* Implement Iso7064Mod11_2Algorithm class
* Implement Iso7064Mod32_2Algorithm class
* Create ICharacterDomain interface
* Implement character domain objects for Iso7064Mod11_2Algorithm and Iso7064Mod32_2Algorithm
* Resiliency. Invalid input should not throw an exception and instead should simply return Boolean false to indicate failure. Invalid input will include:
	- null
	- String.Empty
	- String less than 2 characters in length
	- Characters not included in the character domain

## Definition of DONE

1. Iso7064PureSystemBase class
1. Iso7064PureSingleDigit class
1. Iso7064Mod11_2Algorithm class
1. Iso7064Mod32_2Algorithm class
1. SupplementaryDigitCharacterDomain class
1. SupplementaryAlphanumericCharacterDomain class
1. Full unit tests
1. README updates
1. Performance benchmarks