# S0083 - Add IMaskedCheckDigitAlgorithm support to Iso7064HybridSystemAlgorithm

## Summary

Extend Iso7064HybridSystemAlgorithm to implement IMaskedCheckDigitAlgorithm and add a Validate(string value, ICheckDigitMask mask) overload so the hybrid ISO/IEC 7064 implementation can validate values containing formatting characters (spaces, dashes) without manual pre-cleaning.

## Background

Iso7064HybridSystemAlgorithm combines aspects of pure and hybrid ISO7064 algorithms to compute check characters. Currently it only exposes unmasked validation APIs. Other ISO7064 implementations in the project support masked validation; adding parity here will make the API consistent and easier for callers who accept formatted input.

## Goals

- Implement IMaskedCheckDigitAlgorithm on Iso7064HybridSystemAlgorithm.
- Add Validate(String value, ICheckDigitMask mask) with semantics consistent with other masked algorithms in the repo:
  - If mask is null, throw ArgumentNullException(Resources.NullMaskMessage).
  - Respect the algorithm's definition of check character count (one or two) — trailing check character(s) must always be processed and are not maskable.
  - For the characters that are maskable, skip those where mask.ExcludeCharacter(index) returns true and only include unmasked characters in the calculation.
  - Use existing overflow avoidance logic (e.g., _reduceThreshold) as implemented by the algorithm.
  - Return false if zero maskable characters were processed (no data to compute against).
- Add unit tests to verify masked behaviours using existing masks (AcceptAllMask, GroupsOfThreeCheckDigitMask, RejectAllMask).

## Acceptance Criteria

1. Iso7064HybridSystemAlgorithm implements IMaskedCheckDigitAlgorithm.
2. Validate(value, mask) throws ArgumentNullException with Resources.NullMaskMessage when mask is null.
3. Validate(value, mask) returns false for null/empty values or values shorter than the algorithm's minimum length.
4. Trailing check character(s) are always processed and cannot be excluded by the mask.
5. Validate(value, mask) returns false when no maskable characters are unmasked.
6. Unit tests cover null mask, AcceptAllMask parity, GroupsOfThree behavior, RejectAll zero-processed case, invalid characters, and benchmarks.

## Tasks

1. Update class declaration to implement IMaskedCheckDigitAlgorithm.
2. Implement Validate(String value, ICheckDigitMask mask) consistent with other algorithms.
3. Add tests in the Tests.Unit.Iso7064 project to cover masked scenarios and edge cases.
4. Run unit tests and resolve regressions as needed.
5. Update README/docs if necessary to indicate masked support.

## Estimated Effort

- 2 - 4 hours
