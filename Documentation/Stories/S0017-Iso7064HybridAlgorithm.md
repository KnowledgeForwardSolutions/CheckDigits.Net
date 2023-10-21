# S0016-Iso7064DoubleCheckDigitAlgorithm

As a developer of CheckDigits.Net, I want CheckDigits.Net to include the ISO 7064 family of check digit algorithms in the list of supported algorithms. This story is limited to ISO 7064 hybrid algorithms. The supported algorithms are ISO/IEC 7064, MOD 11,10, ISO/IEC 7064 MOD 27,26 and ISO/IEC 7064 MOD 37,36.


## Requirements

* Implement an abstract base class for ISO 7064 hybrid check character algorithms. Should implement ISingleCheckDigitAlgorithm
* Implement Iso7064Mod11_10Algorithm class
* Implement Iso7064Mod27_26Algorithm class
* Implement Iso7064Mod37_36Algorithm class
* Resiliency. Invalid input should not throw an exception and instead should simply return Boolean false to indicate failure. Invalid input will include:
	- null
	- String.Empty
	- String less than 2 characters in length
	- Characters not included in the character domain

## Definition of DONE

1. Iso7064PureDoubleDigit class
1. Iso7064Mod11_10Algorithm class
1. Iso7064Mod27_26Algorithm class
1. Iso7064Mod37_36Algorithm class
1. Full unit tests
1. README updates
1. Performance benchmarks