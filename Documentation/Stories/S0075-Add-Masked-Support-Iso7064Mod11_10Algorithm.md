# S0051 - Add IMaskedCheckDigitAlgorithm support to Iso7064Mod11_10Algorithm

## Summary

Update the Iso7064Mod11_10Algorithm implementation so it implements IMaskedCheckDigitAlgorithm and correctly validates values when a mask (ICheckDigitMask) excludes formatting or other non-data characters. The algorithm must continue to behave identically for unmasked usage and preserve existing performance and overflow protections.

## Background

The Iso7064Mod11_10Algorithm currently implements ISingleCheckDigitAlgorithm only. Several other algorithms in the codebase support masked validation (IMaskedCheckDigitAlgorithm) and include unit tests to verify behaviour with AcceptAllMask, GroupsOfThreeCheckDigitMask and RejectAllMask. Adding masked support to Iso7064Mod11_10Algorithm will allow consumers to validate human-readable values (spaces, dashes) without stripping them beforehand and will align this algorithm with the rest of the library.

## Goals

- Implement IMaskedCheckDigitAlgorithm on Iso7064Mod11_10Algorithm.
- Add a Validate(string value, ICheckDigitMask mask) overload with the same semantics as other masked algorithms in the project:
  - Trailing check digit remains unmaskable and must always be processed.
  - Mask controls which non-check-digit characters are included in the check calculation.
  - If mask is null, throw ArgumentNullException with the same resource message as other algorithms.
  - Return false if no unmasked non-check-digit characters are available for calculation.
- Preserve existing overflow avoidance logic (use existing _reduceThreshold logic).
- Add/update unit tests to cover masked behaviour and boundary conditions consistent with other algorithms.

## Acceptance Criteria

1. Iso7064Mod11_10Algorithm implements IMaskedCheckDigitAlgorithm in addition to ISingleCheckDigitAlgorithm.
2. Validate(value, mask) returns false when mask is null? — No: it should throw ArgumentNullException with Resources.NullMaskMessage.
3. Validate(value, mask) returns false when value is null or empty.
4. Validate(value, mask) returns false when value length is less than the minimum required (same as Validate(value)).
5. Validate(value, mask) processes the check digit (right-most character) regardless of the mask; mask applies only to preceding characters.
6. Validate(value, mask) returns false if processed (unmasked) characters count is zero.
7. Unit tests are added/updated to assert masked behaviour using existing masks (_acceptAllMask, _groupsOfThreeMask, _rejectAllMask).
8. No behavior change for existing Validate(value) or TryCalculateCheckDigit(value, out char) APIs.

## Tasks

1. Update Iso7064Mod11_10Algorithm signature to implement IMaskedCheckDigitAlgorithm.
2. Add Validate(String value, ICheckDigitMask mask) method. Follow the implementation pattern used in other algorithms (AlphanumericMod97_10Algorithm, Modulus10_1Algorithm) to ensure consistent semantics:
   - Validate mask parameter and value null/empty
   - Iterate over value up to (value.Length - 1) (check digit excluded from mask)
   - Skip characters where mask.ExcludeCharacter(index) is true
   - Use existing character-to-integer mapping and _reduceThreshold logic
   - Track processed character count and return false if zero
   - Add final check digit handling and return sum % _modulus == 1
3. Add unit tests in CheckDigits.Net.Tests.Unit.Iso7064 to cover:
   - Null mask throws ArgumentNullException
   - Masked validation with AcceptAllMask matches unmasked Validate
   - Masked validation with GroupsOfThreeCheckDigitMask and RejectAllMask exercises skipping and zero-processed-character behaviour
   - Edge cases: non-digit characters, too-short values, and benchmark values
4. Run unit tests and ensure all tests pass.
5. Update README or docs if necessary to note that Iso7064Mod11_10Algorithm supports masked validation.

## Notes and Constraints

- Follow existing coding patterns in the repository: zero allocations, use _reduceThreshold to avoid overflow, and use resource messages for exceptions.
- Keep changes limited to Iso7064Mod11_10Algorithm and unit tests; do not alter other algorithms.
- Use existing masks and test helpers from the test project.

## Estimated Effort

- 2 - 4 hours (implementation + tests + run)

## Dependencies

- None beyond the existing test utilities and masks in the test project.
