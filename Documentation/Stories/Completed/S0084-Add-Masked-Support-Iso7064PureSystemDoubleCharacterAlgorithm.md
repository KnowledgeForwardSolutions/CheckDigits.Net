# S0084 - Add IMaskedCheckDigitAlgorithm support to Iso7064PureSystemDoubleCharacterAlgorithm

## Summary

Enable masked validation for Iso7064PureSystemDoubleCharacterAlgorithm by implementing IMaskedCheckDigitAlgorithm and adding a Validate(string value, ICheckDigitMask mask) overload. This allows double-character pure-system ISO7064 values to be validated when they contain formatting characters.

## Background

Iso7064PureSystemDoubleCharacterAlgorithm is a two-character ISO7064 pure system implementation. It currently supports only unmasked validation. Other pure-system algorithms in the codebase support masked validation; adding this support ensures consistent behavior across the library and simplifies input handling for consumers.

## Goals

- Implement IMaskedCheckDigitAlgorithm on the algorithm type.
- Add Validate(String value, ICheckDigitMask mask) with behavior aligned to other double-character masked algorithms:
  - Throw ArgumentNullException(Resources.NullMaskMessage) when mask is null.
  - Treat final two characters as check characters that are never maskable.
  - Apply mask.ExcludeCharacter(index) to preceding characters only; skip masked ones.
  - Preserve existing _reduceThreshold and modulus logic.
  - Return false if no preceding unmasked characters were processed.
- Add unit tests mirroring existing tests for other double-character masked algorithms.

## Acceptance Criteria

1. Class implements IMaskedCheckDigitAlgorithm in addition to IDoubleCheckDigitAlgorithm.
2. Validate(value, mask) throws with correct resource message when mask is null.
3. Masking cannot exclude the trailing two check characters; these are always processed.
4. Validate(value, mask) returns false if zero preceding unmasked characters exist.
5. Tests added/updated and passing.

## Tasks

1. Modify class declaration to implement IMaskedCheckDigitAlgorithm.
2. Implement masked Validate method following AlphanumericMod97_10Algorithm style.
3. Add tests for null mask, AcceptAllMask equivalence, group masks, reject-all behavior, invalid characters, and benchmark values.
4. Run tests and fix any regressions.

## Estimated Effort

- 2 - 4 hours
