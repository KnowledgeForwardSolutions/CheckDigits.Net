# S0070: IBM Modulus 11 (Weights 2-7) Extended Check Digit Algorithm

## Story
As a developer integrating check digit validation into my application,  
I want to use an IBM Modulus 11 check digit algorithm with weights 2, 3, 4, 5, 6, 7 that supports an extended character for check digit value 10,  
So that I can validate identifiers that use this weighted modulus 11 scheme with a configurable extended character representation.

## Acceptance Criteria

### Algorithm Characteristics
- Algorithm class is named `Modulus11_234567Extended`
- Implements `ISingleCheckDigitAlgorithm` interface
- Implements `IMaskedCheckDigitAlgorithm` interface
- Supports decimal digits (0-9) for value characters
- Supports decimal digits (0-9) and a configurable extended character for check digit
- Uses repeating weight pattern: 2, 3, 4, 5, 6, 7 (applied right-to-left)
- Modulus value is 11

### Constructor
- Accepts a `Char` parameter named `extendedCharacter` for the character representing check digit value 10
- Default value for `extendedCharacter` parameter is `'X'`
- Extended character should be configurable to support different standards (e.g., 'X', '*', '-', etc.)
- Constructor signature: `Modulus11_234567Extended(Char extendedCharacter = 'X')`

### Calculation Rules
- Multiply each digit by its corresponding weight (right-to-left, repeating pattern 2, 3, 4, 5, 6, 7)
- Sum the weighted products
- Calculate check digit as: (11 - (sum % 11)) % 11
- Valid check digits are 0-9 and the extended character (representing value 10)
- When calculated check digit equals 10, use the extended character

### TryCalculateCheckDigit Method
- Returns `true` and outputs the calculated check digit when successful
- When calculated check digit is 0-9, outputs the corresponding digit character ('0'-'9')
- When calculated check digit equals 10, outputs the extended character and returns `true`
- Returns `false` (with checkDigit = '\0') when:
  - Input value is `null` or empty
  - Input value contains non-decimal digit characters

### Validate Method (ISingleCheckDigitAlgorithm)
- Returns `true` when the check digit matches the calculated value
- When calculated check digit is 10, returns `true` if the check digit character equals the extended character
- Returns `false` when:
  - Input value is `null`, empty, or invalid length (less than 2 characters)
  - Input value (excluding check digit) contains invalid characters
  - Check digit character is not a decimal digit or the extended character
  - Check digit does not match calculated value
  - Check digit is the extended character but calculated value is not 10

### Validate Method (IMaskedCheckDigitAlgorithm)
- Accepts a mask parameter to include/exclude characters from calculation
- Check digit position is not affected by the mask
- Returns `true` when the check digit matches the calculated value (with mask applied)
- When calculated check digit is 10, returns `true` if the check digit character equals the extended character
- Returns `false` under same conditions as the non-masked version

### Algorithm Properties
- `AlgorithmName`: Human-readable name for the algorithm
- `AlgorithmDescription`: Description of the algorithm's purpose and characteristics

## Technical Notes
- The weight pattern 2, 3, 4, 5, 6, 7 repeats as needed for longer values
- Weights are applied right-to-left (starting with the rightmost digit before the check digit)
- Check digit is positioned as the rightmost character
- The extended character is configurable to support different identifier standards
- Common extended character values include 'X', '*', '-', but any character can be configured
- The algorithm is case-sensitive with respect to the extended character

## Examples
Given extended character = 'X':
- If calculated check digit = 10, `TryCalculateCheckDigit` returns `true` with checkDigit = 'X'
- Value ending in 'X' validates successfully when calculated check digit = 10
- Value ending in '5' validates successfully when calculated check digit = 5

Given extended character = '*':
- If calculated check digit = 10, `TryCalculateCheckDigit` returns `true` with checkDigit = '*'
- Value ending in '*' validates successfully when calculated check digit = 10


## Definition of DONE

- New Modulus11_27Extended class
- Full unit tests
- README updates
- Performance benchmarks
- Algorithms class updated
- MaskedAlgorithms class updated