# S0072: MaskedModulus10_2

## Story
**As a** .NET developer  
**I want** to use the Modulus10_2 algorithm with a check digit mask to handle values that have been formatted for human readablility.

## Background
Luhn algorithm currently supports IMaskedCheckDigitAlgorithm. I want to have the same capability with Modulus10_2 algorithm.

## Acceptance Criteria
- Modulus10_2 algorithm updated to implement IMaskedCheckDigitAlgorithm
- Add Modulus10_2 algorithm to MaskedAlgorithms class

## Definition of Done
- Updated Modulus10_2 algorithm
- Updated MaskedAlgorithms class
- Unit tests for Validate(String, ICheckDigitMask) method of Modulus10_2 algorithm
- Unit tests for MaskedAlgorithms class
- README updates
