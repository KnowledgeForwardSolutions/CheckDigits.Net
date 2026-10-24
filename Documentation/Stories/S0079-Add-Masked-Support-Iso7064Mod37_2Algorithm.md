# S0079 - Add IMaskedCheckDigitAlgorithm support to Iso7064Mod37_2Algorithm

## Summary

Implement IMaskedCheckDigitAlgorithm on Iso7064Mod37_2Algorithm and add a Validate(string value, ICheckDigitMask mask) overload so callers can validate values containing non-data formatting characters without manual cleanup.

## Background

Iso7064Mod37_2Algorithm is a single-character ISO7064 implementation and currently only offers unmasked APIs. Consistent masked support across algorithms improves usability for values with formatting characters and matches patterns used by other algorithms in the project.

## Goals

- Implement IMaskedCheckDigitAlgorithm.
- Add Validate(String value, ICheckDigitMask mask). Behavior must match repository conventions:
  - Throw ArgumentNullException(Resources.NullMaskMessage) when mask is null.
  - The trailing single check character must always be processed.
  - Mask only affects preceding characters; skip masked chars.
  - Return false if zero unmasked preceding characters were processed.
  - Preserve existing overflow protection via _reduceThreshold.
- Add tests to cover masked behaviour and ensure parity with unmasked Validate when AcceptAllMask is used.

## Acceptance Criteria

1. Class implements IMaskedCheckDigitAlgorithm.
2. Validate(value, mask) throws ArgumentNullException when mask is null with Resources.NullMaskMessage.
3. Validate(value, mask) returns false when value is null/empty or too short.
4. Validate(value, mask) always processes final check character and returns false when no prior unmasked characters exist.
5. Unit tests added to cover masked scenarios with existing masks.

## Tasks

1. Modify class signature to implement IMaskedCheckDigitAlgorithm.
2. Implement Validate(String value, ICheckDigitMask mask) following patterns from Modulus and other ISO7064 algorithms.
3. Add unit tests and run the test suite.

## Estimated Effort

- 1 - 3 hours
