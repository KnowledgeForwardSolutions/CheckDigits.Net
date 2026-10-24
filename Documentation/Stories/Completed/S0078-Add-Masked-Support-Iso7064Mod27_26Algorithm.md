# S0078 - Add IMaskedCheckDigitAlgorithm support to Iso7064Mod27_26Algorithm

## Summary

Add IMaskedCheckDigitAlgorithm support to Iso7064Mod27_26Algorithm by implementing a masked Validate overload. This enables validating alphabetic values that include formatting characters without requiring callers to pre-clean input.

## Background

Iso7064Mod27_26Algorithm currently exposes only standard (unmasked) validation APIs. Other algorithms follow a consistent masked validation contract; adding masked support here ensures a consistent API across ISO7064 implementations.

## Goals

- Implement IMaskedCheckDigitAlgorithm.
- Introduce Validate(String value, ICheckDigitMask mask) with the same semantics as other masked algorithms (mask null -> ArgumentNullException(Resources.NullMaskMessage); trailing check character(s) are not maskable; processed character count must be > 0).
- Preserve numeric overflow protection using existing _reduceThreshold logic.
- Add unit tests covering masked behaviour.

## Acceptance Criteria

1. Type implements IMaskedCheckDigitAlgorithm.
2. Validate(value, mask) throws ArgumentNullException when mask is null (Resources.NullMaskMessage).
3. Validate(value, mask) honors mask and always processes check character(s) per algorithm definition.
4. Validate(value, mask) returns false when no non-check-digit characters are unmasked.
5. Tests added/updated to assert masked behaviours.

## Tasks

1. Update class declaration to implement IMaskedCheckDigitAlgorithm.
2. Implement Validate(String value, ICheckDigitMask mask) following repository patterns.
3. Add unit tests to the Tests.Unit.Iso7064 project to verify masked scenarios and edge cases.
4. Run tests and ensure success.

## Estimated Effort

- 1 - 3 hours
