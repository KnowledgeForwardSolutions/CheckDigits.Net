# S0081 - Add IMaskedCheckDigitAlgorithm support to Iso7064Mod661_26Algorithm

## Summary

Enable IMaskedCheckDigitAlgorithm support for Iso7064Mod661_26Algorithm by implementing a masked Validate overload. This lets callers validate alphabetic values that include formatting characters without pre-cleaning.

## Background

Iso7064Mod661_26Algorithm currently implements double-character ISO7064 logic without masked validation support. Other ISO7064 implementations support masks; adding masked support here promotes consistency and usability.

## Goals

- Implement IMaskedCheckDigitAlgorithm on the type.
- Add Validate(String value, ICheckDigitMask mask) that:
  - Throws ArgumentNullException(Resources.NullMaskMessage) when mask is null.
  - Treats final two characters as check characters and does not allow them to be masked.
  - Applies mask to preceding characters only and skips masked characters.
  - Uses existing _reduceThreshold and modulus logic to avoid overflow.
  - Returns false if zero preceding unmasked characters are processed.
- Add unit tests covering masked behaviour and edge cases.

## Acceptance Criteria

1. Class implements IMaskedCheckDigitAlgorithm.
2. Mask argument validation and exception message match repository conventions.
3. Validate(value, mask) enforces non-maskability of trailing check characters.
4. Returns false when no preceding unmasked characters exist.
5. Tests added and passing.

## Tasks

1. Update class declaration to implement IMaskedCheckDigitAlgorithm.
2. Implement masked Validate following patterns from other double-check-digit algorithms.
3. Add/adjust tests and run the test suite.

## Estimated Effort

- 2 - 4 hours
