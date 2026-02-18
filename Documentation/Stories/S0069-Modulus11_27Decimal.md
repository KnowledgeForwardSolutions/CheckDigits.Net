# S0069: IBM Modulus 11 (Weights 2-7) Check Digit Algorithm

## Story
As a developer integrating check digit validation into my application,  
I want to use an IBM Modulus 11 check digit algorithm with weights 2, 3, 4, 5, 6, 7,  
So that I can validate identifiers that use this specific weighted modulus 11 scheme.

## Acceptance Criteria

### Algorithm Characteristics
- Algorithm class is named `Modulus11_27Decimal`
- Implements `ISingleCheckDigitAlgorithm` interface
- Implements `IMaskedCheckDigitAlgorithm` interface
- Supports decimal digits only (0-9)
- Uses repeating weight pattern: 2, 3, 4, 5, 6, 7 (applied right-to-left)
- Modulus value is 11

### Calculation Rules
- Multiply each digit by its corresponding weight (right-to-left, repeating pattern)
- Sum the weighted products
- Calculate check digit as: (11 - (sum % 11)) % 11
- If the calculated check digit equals 10, both `TryCalculateCheckDigit` and `Validate` methods return `false`
- Valid check digits are 0-9 only

### TryCalculateCheckDigit Method
- Returns `true` and outputs the calculated check digit when successful
- Returns `false` (with checkDigit = '\0') when:
  - Input value is `null` or empty
  - Input value contains non-decimal digit characters
  - Calculated check digit equals 10

### Validate Method (ISingleCheckDigitAlgorithm)
- Returns `true` when the check digit matches the calculated value
- Returns `false` when:
  - Input value is `null`, empty, or invalid length (less than two characters in length)
  - Input value contains invalid characters
  - Check digit does not match calculated value
  - Calculated check digit would be 10

### Validate Method (IMaskedCheckDigitAlgorithm)
- Accepts a mask parameter to include/exclude characters from calculation
- Check digit position is not affected by the mask
- Returns `true` when the check digit matches the calculated value (with mask applied)
- Returns `false` under same conditions as the non-masked version

### Algorithm Properties
- `AlgorithmName`: Human-readable name for the algorithm
- `AlgorithmDescription`: Description of the algorithm's purpose and characteristics

## Technical Notes
- The weight pattern 2, 3, 4, 5, 6, 7 repeats as needed for longer values
- Weights are applied right-to-left (starting with the rightmost digit)
- Check digit is positioned as the rightmost character
- The special case of check digit = 10 being invalid is a key distinguishing feature of this variant

## Definition of DONE

- New Modulus11_27Decimal class
- Full unit tests
- README updates
- Performance benchmarks
- Algorithms class updated
- MaskedAlgorithms class updated