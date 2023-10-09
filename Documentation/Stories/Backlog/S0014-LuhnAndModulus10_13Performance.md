# S0014-LuhnAndModulus10_13Peformance

As a developer of CheckDigits.Net, I want CheckDigits.Net to provide the highest performance for all algorithms.

## Requirements

* Benchmark removing the CalculateCheckDigit method from the Luhn and Modulus10_13 algorithms and using separate versions for Validate and TryCalculateCheckDigit methods
* Implement separate versions if performance warrents it

## Definition of DONE

1. Benchmarks for separate implementation of Validate and TryCalculateCheckDigit vs using common CalculateCheckDigit method
1. Update implementation if necessary
1. Verify changes via existing unit tests