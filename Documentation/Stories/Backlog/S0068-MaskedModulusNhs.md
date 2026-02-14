# S0068: MaskedNhs

## Story
**As a** .NET developer  
**I want** to use the Nhs algorithm with a check digit mask to handle values that have been formatted for human readablility.

## Background
Luhn algorithm currently supports IMaskedCheckDigitAlgorithm. I want to have the same capability with Nhs algorithm.

## Acceptance Criteria
- Nhs algorithm updated to implement IMaskedCheckDigitAlgorithm
- Add Nhs algorithm to MaskedAlgorithms class.

## Definition of Done
- Updated Nhs algorithm
- Updated MaskedAlgorithms class
- Unit tests for Validate(String, ICheckDigitMask) method of Nhs algorithm
- Unit tests for MaskedAlgorithms class
- README updates
