# S0016-Iso7064DoubleCheckDigitAlgorithm

As a developer of CheckDigits.Net, I want CheckDigits.Net to include the ISO 7064 family of check digit algorithms in the list of supported algorithms. This story is limited to ISO 7064 pure systems that generate two check characters. The supported algorithms are ISO/IEC 7064, MOD 97-10, ISO/IEC 7064 MOD 661-26 and ISO/IEC 7064 MOD 1271-36.


## Requirements

* Implement an abstract base class for ISO 7064 pure system double check character algorithms. Should implement IDoubleCheckDigitAlgorithm
* Implement Iso7064Mod97_10Algorithm class
* Implement Iso7064Mod661_26Algorithm class
* Implement Iso7064Mod1271_36Algorithm class
* Resiliency. Invalid input should not throw an exception and instead should simply return Boolean false to indicate failure. Invalid input will include:
	- null
	- String.Empty
	- String less than 2 characters in length
	- Characters not included in the character domain

## Definition of DONE

1. Iso7064PureDoubleDigit class
1. Iso7064Mod97_10Algorithm class
1. Iso7064Mod661_26Algorithm class
1. Iso7064Mod1271_36Algorithm class
1. Full unit tests
1. README updates
1. Performance benchmarks