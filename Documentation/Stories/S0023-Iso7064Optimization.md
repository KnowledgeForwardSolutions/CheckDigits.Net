# S0023-Iso7064Optimization

As a developer of CheckDigits.Net, I want CheckDigits.Net to include highest performance possible implementations of ISO/IEC 7064 algorithms.

## Requirements

* Examine the MOD 1271-36, MOD 37-2 and MOD 37,36 algorithms and see if the mapping of characters to integers can be improved. If possible, then apply the changes to those algorithms.

## Definition of DONE

1. Benchmarks for proposed changes
1. README updates
1. Performance benchmarks


Notes:
 MOD 1271-36 algorithm - Validate method ~18% improvement. TryCalculateCheckDigits no improvement
 MOD 37-2 algorithm - Validate method ~17% improvement. TryCalculateCheckDigit method ~20% improvement
 MOD 37,26 algorithm - Validate method ~18% improvement.