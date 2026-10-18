# S0080 - Add IMaskedCheckDigitAlgorithm support to Iso7064Mod37_36Algorithm

## Summary

Add IMaskedCheckDigitAlgorithm support to Iso7064Mod37_36Algorithm (double-check-digit variant) so it can validate formatted alphanumeric values while preserving algorithm semantics and overflow guards.

## Background

Iso7064Mod37_36Algorithm is a double-character ISO7064 algorithm that currently lacks masked validation support. Other double-check-digit implementations support masking; adding it here provides consistency and convenience for consumers.

## Goals

- Implement IMaskedCheckDigitAlgorithm.
- Add Validate(String value, ICheckDigitMask mask) with behaviour consistent with other double-character masked implementations:
  - Throw ArgumentNullException(Resources.NullMaskMessage) when mask is null.
  - Trailing two check characters must always be processed and are not maskable.
  - Mask applies only to preceding characters; skip masked chars.
  - Return false if no preceding unmasked characters are processed.
  - Preserve existing _reduceThreshold and modulus logic.
- Add unit tests mirroring other double-check-digit masked tests.

## Acceptance Criteria

1. Iso7064Mod37_36Algorithm implements IMaskedCheckDigitAlgorithm.
2. Validate(value, mask) throws when mask is null.
3. Mask cannot exclude the trailing two check characters; mask only affects earlier characters.
4. Validate(value, mask) returns false if zero unmasked preceding characters.
5. Tests added/updated and all pass.

## Tasks

1. Update type declaration to include IMaskedCheckDigitAlgorithm.
2. Implement Validate(String value, ICheckDigitMask mask) following AlphanumericMod97_10Algorithm as a template.
3. Add unit tests (null mask, AcceptAllMask parity, GroupsOfThreeMask behaviour, RejectAllMask zero-processed) and run tests.

## Estimated Effort

- 2 - 4 hours
