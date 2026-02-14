# S0067: MaskedModulus10_13

## Story
**As a** .NET developer  
**I want** to use the Modulus10_13 algorithm with a check digit mask to handle values that have been formatted for human readablility.

## Background
Luhn algorithm currently supports IMaskedCheckDigitAlgorithm. I want to have the same capability with Modulus10_13 algorithm.

## Acceptance Criteria
- Modulus10_13 algorithm updated to implement IMaskedCheckDigitAlgorithm
- New MaskedAlgorithms class similar to Algorithms class, but returning IMaskedCheckDigitAlgorithm instances instead of ICheckDigitAlgorithm instances.

## Definition of Done
- Updated Modulus10_13 algorithm
- New MaskedAlgorithms class
- Unit tests for Validate(String, ICheckDigitMask) method of Modulus10_13 algorithm
- Unit tests for MaskedAlgorithms class
- README updates
