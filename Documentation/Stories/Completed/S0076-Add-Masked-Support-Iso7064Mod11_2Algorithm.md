# S0076 - Add IMaskedCheckDigitAlgorithm support to Iso7064Mod11_2Algorithm

## Summary

Enable masked validation support for Iso7064Mod11_2Algorithm by implementing IMaskedCheckDigitAlgorithm and adding a Validate(string value, ICheckDigitMask mask) overload. This will allow callers to validate values containing formatting characters (spaces, dashes) without manual preprocessing.

## Background

Iso7064Mod11_2Algorithm currently implements ISingleCheckDigitAlgorithm only. Several other algorithms support masked validation and unit tests assume masks such as AcceptAllMask, GroupsOfThreeCheckDigitMask, and RejectAllMask. Adding masked support will align Iso7064Mod11_2Algorithm with the rest of the library.

## Goals

- Implement IMaskedCheckDigitAlgorithm.
- Add Validate(String value, ICheckDigitMask mask) following the same semantics used across the repository:
  - If mask is null, throw ArgumentNullException(Resources.NullMaskMessage).
  - The trailing single check digit must always be processed; mask applies to preceding characters.
  - Skip characters where mask.ExcludeCharacter(index) is true.
  - Use existing _reduceThreshold logic to avoid overflow.
  - Return false if zero unmasked non-check-digit characters were processed.
- Add/update unit tests to verify masked behaviour and edge cases.

## Acceptance Criteria

1. Iso7064Mod11_2Algorithm implements IMaskedCheckDigitAlgorithm.
2. Validate(value, mask) throws ArgumentNullException when mask is null with Resources.NullMaskMessage.
3. Validate(value, mask) returns false for null/empty or too short values (same limits as existing Validate).
4. Validate(value, mask) processes the final check digit regardless of the mask and applies mask only to preceding characters.
5. Validate(value, mask) returns false when no non-check-digit characters are unmasked.
6. Unit tests added/updated to assert expected masked behaviours using existing masks.

## Tasks

1. Update type signature to implement IMaskedCheckDigitAlgorithm.
2. Implement Validate(String value, ICheckDigitMask mask) matching repository patterns.
3. Add unit tests in Tests.Unit.Iso7064 to cover null mask, AcceptAllMask equivalence, Group masks, RejectAllMask, invalid characters and boundaries.
4. Run unit tests and ensure all pass.
5. Update README/docs if necessary.

## Estimated Effort

- 1 - 3 hours (implementation + tests + run)
