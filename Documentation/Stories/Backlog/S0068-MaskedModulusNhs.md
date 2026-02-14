# S0068: Deprecate Modulus11 and NHS Algorithms - Replace with Modulus11Decimal and Modulus11Extended

## Story
As a developer maintaining the CheckDigits.Net library,  
I want to deprecate the existing `Modulus11Algorithm` and `NhsAlgorithm` classes and replace them with more clearly named alternatives,  
So that the library provides a clearer, more consistent naming convention that distinguishes between modulus 11 variants that reject check digit value 10 versus those that accept it with an extended character.

## Background
The current `Modulus11Algorithm` accepts check digit value 10 and represents it as 'X', while `NhsAlgorithm` rejects check digit value 10. This creates confusion about which variant to use. The new naming convention (`Modulus11Decimal` and `Modulus11Extended`) makes the behavior explicit:
- **Modulus11Decimal**: Check digits are decimal only (0-9), reject if calculated value is 10
- **Modulus11Extended**: Check digits include 0-9 plus extended character (default 'X') for value 10

## Acceptance Criteria

### New Algorithm Classes

#### Modulus11Decimal
- Class name: `Modulus11Decimal`
- Namespace: `CheckDigits.Net.GeneralAlgorithms`
- Implements: `ISingleCheckDigitAlgorithm`, `IMaskedCheckDigitAlgorithm`
- Behavior: Identical to current `NhsAlgorithm` but without fixed length constraint
- Valid input characters: Decimal digits (0-9)
- Valid check digits: 0-9 only
- Weights: Position-based (1, 2, 3, 4, ... from right to left)
- Calculation: `(11 - (sum % 11)) % 11`
- Returns `false` when calculated check digit equals 10
- No fixed length requirement (unlike NHS which requires exactly 10 characters)
- Maximum length for `TryCalculateCheckDigit`: 9 characters
- Maximum length for `Validate`: 10 characters
- Minimum length for `Validate`: 2 characters

#### Modulus11Extended
- Class name: `Modulus11Extended`
- Namespace: `CheckDigits.Net.GeneralAlgorithms`
- Implements: `ISingleCheckDigitAlgorithm`, `IMaskedCheckDigitAlgorithm`
- Behavior: Identical to current `Modulus11Algorithm`
- Constructor: `Modulus11Extended(Char extendedCharacter = 'X')`
- Valid input characters: Decimal digits (0-9)
- Valid check digits: 0-9 and extended character
- Weights: Position-based (1, 2, 3, 4, ... from right to left)
- Calculation: `(11 - (sum % 11)) % 11`
- When calculated check digit equals 10, uses extended character
- Maximum length for `TryCalculateCheckDigit`: 9 characters
- Maximum length for `Validate`: 10 characters
- Minimum length for `Validate`: 2 characters

### Deprecation Strategy

#### Mark as Obsolete
- Add `[Obsolete]` attribute to `Modulus11Algorithm` class with message:
```
[Obsolete("Modulus11Algorithm is deprecated. Use Modulus11Extended for check digits including 'X' (or custom extended character), or use Modulus11Decimal for decimal-only check digits (0-9). This type will be removed in a future version.")]
```

- Add `[Obsolete]` attribute to `NhsAlgorithm` class with message:
```
[Obsolete("NhsAlgorithm is deprecated. Use Modulus11Decimal for general modulus 11 with decimal-only check digits. For NHS-specific validation with fixed 10-character length, continue using this algorithm until a dedicated NhsNumber validator is provided. This type will be removed in a future version.")]
```

#### Update Algorithms Static Class
- Mark `Algorithms.Modulus11` property as obsolete with message:
`[Obsolete("Modulus11 is deprecated. Use Modulus11Extended instead. This property will be removed in a future version.")]`

- Mark `Algorithms.Nhs` property as obsolete with message:
`[Obsolete("Nhs is deprecated. Use Modulus11Decimal for general modulus 11 with decimal-only check digits, or continue using Nhs for NHS-specific validation. This property will be removed in a future version.")]`


### Documentation Updates
- Update library documentation to explain the deprecation and migration path
- Add migration guide showing:
- `Modulus11Algorithm` ? `Modulus11Extended`
- `NhsAlgorithm` ? `Modulus11Decimal` (for general use) or keep using `NhsAlgorithm` (for NHS-specific 10-character validation)
- Update README.md with deprecation notices
- Add XML documentation comments to new classes explaining the differences

### Testing Requirements
- Create comprehensive test suite for `Modulus11Decimal`:
- All tests from `NhsAlgorithmTests` adapted for variable length
- Additional edge cases for lengths 2-10 characters
- Verify rejection of check digit value 10
- Test both `ISingleCheckDigitAlgorithm` and `IMaskedCheckDigitAlgorithm` interfaces
- Create comprehensive test suite for `Modulus11Extended`:
- All tests from `Modulus11AlgorithmTests`
- Tests with default extended character 'X'
- Tests with custom extended characters ('*', '-', etc.)
- Test both `ISingleCheckDigitAlgorithm` and `IMaskedCheckDigitAlgorithm` interfaces
- Ensure all existing tests for deprecated classes continue to pass
- Add integration tests verifying backward compatibility

### Backward Compatibility
- Deprecated classes remain fully functional
- No breaking changes in current version
- Deprecated classes will be removed in next major version (e.g., v2.0.0)
- All existing client code continues to work with compiler warnings

### Implementation Order
1. Implement `Modulus11Decimal` class with full test coverage
2. Implement `Modulus11Extended` class with full test coverage
3. Add new properties to `Algorithms` static class
3. Add new properties to `MaskedAlgorithms` static class
4. Add `[Obsolete]` attributes to deprecated classes and properties
5. Update documentation and migration guides
6. Create pull request with all changes

## Technical Notes
- Both new algorithms use the same core modulus 11 calculation
- The key difference is handling of check digit value 10:
- `Modulus11Decimal`: Returns `false` (invalid)
- `Modulus11Extended`: Returns extended character (valid)
- `Modulus11Extended` maintains the same weighting scheme as the original `Modulus11Algorithm` (position-based: 1, 2, 3, ...)
- `Modulus11Decimal` removes the fixed 10-character length constraint from `NhsAlgorithm`, making it a general-purpose algorithm
- The NHS-specific use case can continue using `NhsAlgorithm` until a dedicated NHS Number validator is implemented
- Consider implementing `IMaskedCheckDigitAlgorithm` on both new classes for consistency with library patterns
