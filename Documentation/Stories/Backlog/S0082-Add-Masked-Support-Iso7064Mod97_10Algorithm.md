# S0082 - Add IMaskedCheckDigitAlgorithm support to Iso7064Mod97_10Algorithm

## Summary

Add IMaskedCheckDigitAlgorithm support to Iso7064Mod97_10Algorithm so that masked validation of alphabetic and numeric characters is supported for formatted inputs (spaces, dashes) while preserving current behavior and performance.

## Background

Iso7064Mod97_10Algorithm is a double-check-digit ISO7064 implementation (radix 10) used for alphanumeric values in the codebase. The algorithm currently exposes only unmasked APIs. Other similar algorithms (e.g., AlphanumericMod97_10Algorithm) support masked validation; adding masked support here provides consistency and convenience.

## Goals

- Implement IMaskedCheckDigitAlgorithm on the type.
- Provide Validate(String value, ICheckDigitMask mask) with repository-consistent semantics:
  - Throw ArgumentNullException(Resources.NullMaskMessage) when mask is null.
  - Trailing two characters are check characters and must always be processed; mask cannot exclude them.
  - Mask controls inclusion of preceding characters only.
  - Use existing _reduceThreshold and letter/digit mapping logic; avoid new allocations.
  - Return false if no preceding unmasked characters are processed.
- Add unit tests verifying masked behaviour and parity with unmasked Validate when AcceptAllMask is used.

## Acceptance Criteria

1. Iso7064Mod97_10Algorithm implements IMaskedCheckDigitAlgorithm.
2. Validate(value, mask) throws ArgumentNullException when mask is null with correct resource string.
3. Mask never applies to the trailing two check characters.
4. Validate(value, mask) returns false if zero preceding unmasked characters exist.
5. Unit tests are added and all pass.

## Tasks

1. Modify class signature to include IMaskedCheckDigitAlgorithm.
2. Implement Validate(String value, ICheckDigitMask mask) following the AlphanumericMod97_10Algorithm pattern for double-check-digit masked validation.
3. Add unit tests to Tests.Unit for masked behaviours and run tests.

## Estimated Effort

- 2 - 4 hours
