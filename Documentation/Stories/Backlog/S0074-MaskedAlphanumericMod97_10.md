# S0074: MaskedAlphanumericMod97_10

## Story
**As a** .NET developer  
**I want** to use the AlphanumericMod97_10 algorithm with a check digit mask to handle values that have been formatted for human readablility.

## Background
Luhn algorithm currently supports IMaskedCheckDigitAlgorithm. I want to have the same capability with AlphanumericMod97_10 algorithm.

## Acceptance Criteria
- AlphanumericMod97_10 algorithm updated to implement IMaskedCheckDigitAlgorithm
- Add AlphanumericMod97_10 algorithm to MaskedAlgorithms class

## Definition of Done
- Updated AlphanumericMod97_10 algorithm
- Updated MaskedAlgorithms class
- Unit tests for Validate(String, ICheckDigitMask) method of AlphanumericMod97_10 algorithm
- Unit tests for MaskedAlgorithms class
- README updates
