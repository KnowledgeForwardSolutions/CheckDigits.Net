# S0039-LuhnValidateMasked

As a developer who uses CheckDigits.Net, I want to be able to perform a Luhn validation on a string value that contains characters that should be ignored. For example, a Canadian Social Insurance Number with format characters that separate the three groups of digits (200-320-133).

## Requirements

* Define ICheckDigitMask, an inteface that defines if a character at a specific character position should be processed or ignored.
* ICheckDigitMask should define IncludeCharacter method that has an integer parameter that identifies the character position to check and returns TRUE if the character should be processed or FALSE if the character should be ignored.
* ICheckDigitMask should define ExcludeCharacter method that has an integer parameter that identifies the character position to check and returns TRUE if the character should be ignored or FALSE if the character should be processed.
* Define IMaskedCheckDigitAlgorithm, derived from ICheckDigitAlgorithm that adds an overload of the ValidateMethid that accepts an ICheckDigitMask and that uses the mask to include or exclude characters while calculating the Luhn check digit.
* Update LuhnAlgorithm to impliment IMaskedCheckDigitAlgorithm. The overloaded Validate method should perform the check digit calculation in situ, an not require the allocation of a copy of the origonal value without format characters.

## Definition of DONE

1. New interfaces, ICheckDigitMask and IMaskedCheckDigitAlgorithm
2. LuhnAlgorithm updated to impliment IMaskedCheckDigitAlgorithm
3. Full unit tests
4. README updates
5. Performance benchmarks
