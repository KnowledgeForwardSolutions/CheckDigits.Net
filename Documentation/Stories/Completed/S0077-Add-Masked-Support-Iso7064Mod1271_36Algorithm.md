# S0077 - Add IMaskedCheckDigitAlgorithm support to Iso7064Mod1271_36Algorithm

## Summary

Update Iso7064Mod1271_36Algorithm to implement IMaskedCheckDigitAlgorithm and add masked validation overloads. This will allow validation of formatted alphanumeric values (spaces, dashes) while preserving algorithm correctness and overflow protections.

## Background

Iso7064Mod1271_36Algorithm is a double-check-digit algorithm (radix 36) and currently only exposes double-check-digit APIs. Other ISO7064 implementations in the codebase support masked validation; adding support here will make behavior consistent across the library and simplify callers' requirements to pre-clean formatted values.

## Goals

- Implement IMaskedCheckDigitAlgorithm on Iso7064Mod1271_36Algorithm.
- Add Validate(String value, ICheckDigitMask mask) that:
  - Throws ArgumentNullException when mask is null using Resources.NullMaskMessage.
  - Treats the last two characters as check characters which must always be processed (not maskable).
  - Applies mask.ExcludeCharacter(index) only to preceding characters.
  - Preserves existing _reduceThreshold overflow protection and computation flow.
  - Returns false when zero unmasked non-check-digit characters are processed.
- Add/adjust unit tests to verify masked behavior and edge cases.

## Acceptance Criteria

1. Class implements IMaskedCheckDigitAlgorithm in addition to IDoubleCheckDigitAlgorithm.
2. Validate(value, mask) throws ArgumentNullException if mask is null with correct resource message.
3. Mask cannot exclude the final two check characters; mask applies only to previous characters.
4. Validate(value, mask) returns false if no preceding characters are unmasked.
5. Tests added that mirror patterns used for other double-check-digit masked algorithms.

## Tasks

1. Modify class declaration to include IMaskedCheckDigitAlgorithm.
2. Implement Validate(String value, ICheckDigitMask mask) following AlphanumericMod97_10Algorithm pattern.
3. Add unit tests covering null mask, AcceptAllMask pass-through, GroupsOfThreeCheckDigitMask behaviour, RejectAllMask zero-processed check, invalid character handling and benchmarks.
4. Run unit tests and fix any regressions.

## Estimated Effort

- 2 - 4 hours
