# S0071: MaskedVerhoeff

## Story
**As a** .NET developer  
**I want** to use the Verhoeff algorithm with a check digit mask to handle values that have been formatted for human readablility.

## Background
Luhn algorithm currently supports IMaskedCheckDigitAlgorithm. I want to have the same capability with Verhoeff algorithm.

## Acceptance Criteria
- Verhoeff algorithm updated to implement IMaskedCheckDigitAlgorithm
- Add Verhoeff algorithm to MaskedAlgorithms class

## Definition of Done
- Updated Verhoeff algorithm
- Updated MaskedAlgorithms class
- Unit tests for Validate(String, ICheckDigitMask) method of Modulus10_13 algorithm
- Unit tests for MaskedAlgorithms class
- README updates
