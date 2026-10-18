# S0085 - Add IMaskedCheckDigitAlgorithm support to Iso7064PureSystemSingleCharacterAlgorithm

## Summary

Add IMaskedCheckDigitAlgorithm support to Iso7064PureSystemSingleCharacterAlgorithm by implementing a masked Validate overload. This enables validation of single-character pure-system ISO7064 values when input contains formatting characters.

## Background

Iso7064PureSystemSingleCharacterAlgorithm computes a single-character check according to ISO/IEC 7064 pure system rules. Currently it does not support masked validation. Bringing it in line with other algorithms will make the API consistent and reduce the need for callers to pre-clean input.

## Goals

- Implement IMaskedCheckDigitAlgorithm on the algorithm type.
- Add Validate(String value, ICheckDigitMask mask) with the repository's expected behavior:
  - Throws ArgumentNullException(Resources.NullMaskMessage) if mask is null.
  - The trailing single check character must always be processed and cannot be masked.
  - Apply mask.ExcludeCharacter(index) to preceding characters only; skip masked ones.
  - Preserve existing overflow protection (use algorithm's _reduceThreshold logic if present).
  - Return false when no preceding unmasked characters were processed.
- Add unit tests to validate behavior in typical and edge-case scenarios (null mask, accept/reject masks, invalid characters, minimal lengths, benchmark values).

## Acceptance Criteria

1. Iso7064PureSystemSingleCharacterAlgorithm implements IMaskedCheckDigitAlgorithm.
2. Validate(value, mask) throws ArgumentNullException(Resources.NullMaskMessage) for null mask.
3. Validate(value, mask) returns false for null/empty or values shorter than minimum length.
4. Trailing single check character is always processed; mask only applies to previous characters.
5. Validate(value, mask) returns false when zero preceding unmasked characters exist.
6. Tests added and all pass.

## Tasks

1. Modify class declaration to implement IMaskedCheckDigitAlgorithm.
2. Implement Validate(String value, ICheckDigitMask mask) following patterns used elsewhere in the codebase.
3. Add unit tests to Tests.Unit.Iso7064 for masked behaviours and edge cases.
4. Run unit tests and resolve any issues.

## Estimated Effort

- 1 - 3 hours
