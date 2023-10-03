# CheckDigits.Net

Check digits are a useful tool for detecting human transcription errors. By embedding
a check digit in a piece of information it is possible to detect common data entry
errors early, often before performing more extensive and time consuming processing.

Typical errors that can be detected by check digit algorithms include:

* Single digit transcription errors (any single digit in a value being entered incorrectly).
* Two digit transposition errors (two adjacent digits being swapped, i.e. *ab -> ba*).
* Twin errors (two identical digits being replaced by another pair, i.e. *aa -> bb*).
* Two digit jump transpositions (two digits separated by one position being swapped, i.e. *abc -> cba*).
* Jump twin errors (two identical digits separated by one position being replaced by another pair, i.e. *aba -> cbc*).

Check digit algorithms attempt to balance detection capabilities with the cost in 
execution time and/or the complexity to implement.

CheckDigits.Net brings together in one library an extensive collection of different
check digit algorithms. CheckDigits.Net has the goal that each algorithm supported
be optimized, be resilient to malformed input and that memory allocations be 
minimized or eliminated completely. Benchmarks for each algorithm are provided to
demonstrate performance over a range of values and the memory allocation (if any).

## Using CheckDigits.Net

Add a reference to CheckDigits.Net to your project.

Obtain an instance of the desired check digit algorithm. Either create an instance
by using ```new AlgorithmXyz()``` or using the static ```Algorithms``` class to
get a lazily instantiated singleton instance of the desired algorithm.

Calculate a check digit for a value by invoking the TryCalculateCheckDigit method.

Validate a value that contains a check digit by invoking the Validate method.

**Examples:**
```C#
using CheckDigits.Net;

// Create a new instance of the Luhn algorithm.
var algorithm = new LuhnAlgorithm();

// Get a lazily instantiated singleton instance of the Luhn algorithm.
var lazy = Algorithms.LuhnAlgorithm;


// Calculate the check digit for a value that does not already contain a check digit.
var newValue = "123456789012345";
var successful = algorithm.TryCalculateCheckDigit(newValue, out var checkDigit);  // Returns true; checkDigit will equal '2'

// Validate a value that contains a check digit.
var toValidate = "1234567890123452";
var isValid = lazy.Validate(toValidate);    // Returns true
```

## Interfaces

A check digit algorithm is a class that implements two different interfaces. Every
algorithm implements ```ICheckDigitAlgorithm``` which has properties for getting
the algorithm name and algorithm description and a Validate method that accepts 
a string and returns a boolean value that indicates if the string contains a valid
check digit.

Check digit algorithms that use a single character also implement 
```ISingleCheckDigitAlgorithm``` which has a TryCalculateCheckDigit method that
accepts a string value and an out parameter which will contain the calculated 
check digit or '\0' if it was not possible to calculate the check digit.
TryCalculateCheckDigit also returns a boolean value that indicates if the check
digit was calculated or not. Mal-formed input such as a null value, an empty string,
a string of incorrect length or a string that contains characters that are not
valid for the algorithm will return false instead of throwing an exception.

Check digit algorithms that use two character check digits also implement
```IDoubleCheckDigitAlgorithm```. This interface also has a TryCalculateCheckDigit
method, but the output parameter is a string instead of a character.

## Supported Algorithms

* [Luhn Algorithm](#luhn-algorithm)
* [Modulus10_13 Algorithm (UPC/EAN/ISBN-13/etc.)](#Modulus10_13-Algorithm)

[Benchmarks](#Benchmarks)

### Luhn Algorithm

#### Description

The Luhn algorithm is a modulus 10 algorithm that was developed in 1960 by Hans
Peter Luhn. It can detect all single digit transcription errors and most two digit
transposition errors except 09 -> 90 and vice versa. It can also detect
most twin errors (i.e. 11 <-> 44) except 22 <-> 55,  33 <-> 66 and 44 <-> 77.

#### Common Applications

* Credit card numbers
* International Mobile Equipment Identity (IMEI) numbers
* A wide variety of government identifiers. See Wikipedia link for more info.

#### Links

Wikipedia: https://en.wikipedia.org/wiki/Luhn_algorithm

### Modulus10_13 Algorithm

#### Description

The Modulus10_13 algorithm is a widely used modulus 10 algorithm that uses weights
1 and 3 (odd positions have weight 3, even positions have weight 1). It can detect
all single digit transcription errors and ~80% of two digit transposition errors
(except where the transposed digits have a difference of 5, i.e. *1 <-> 6*, *2 <-> 7*,
etc.). Nor can it detect two digit two digit jump transpositions.

#### Common Applications

* Global Trade Item Number (GTIN-8, GTIN-12, GTIN-13, GTIN-14)
* International Article Number/European Article Number (EAN-8, EAN-13)
* International Standard Book Number (ISBN-13)
* International Standard Music Number (ISMN)
* International Standard Serial Number (ISSN)
* Serial Shipping Container Code (SSCC)
* Universal Product Code (UPC-A, UPC-E)

#### Links

Wikipedia: 
  https://en.wikipedia.org/wiki/Universal_Product_Code#Check_digit_calculation
  https://en.wikipedia.org/wiki/International_Article_Number#Calculation_of_checksum_digit

## Benchmarks

* [Luhn Algorithm](#Luhn-Algorithm-Benchmarks)
* [Modulus10_13 Algorithm](#Modulus10-13-Algorithm-Benchmarks)

### Luhn Algorithm Benchmarks

| Method                 | Value            | Mean      | Error     | StdDev    | Allocated |
|----------------------- |----------------- |----------:|----------:|----------:|----------:|
| TryCalculateCheckDigit | 123              |  8.425 ns | 0.1046 ns | 0.0978 ns |         - |
| TryCalculateCheckDigit | 1234567          | 11.525 ns | 0.1462 ns | 0.1367 ns |         - |
| TryCalculateCheckDigit | 12345678901      | 15.825 ns | 0.1335 ns | 0.1115 ns |         - |
| TryCalculateCheckDigit | 123456789012345  | 20.783 ns | 0.3826 ns | 0.3195 ns |         - |
| Validate               | 1230             | 11.150 ns | 0.1040 ns | 0.0970 ns |         - |
| Validate               | 12345674         | 13.440 ns | 0.2080 ns | 0.1850 ns |         - |
| Validate               | 123456789015     | 18.130 ns | 0.2040 ns | 0.1810 ns |         - |
| Validate               | 1234567890123452 | 22.540 ns | 0.1200 ns | 0.0940 ns |         - |


### Modulus10_13 Algorithm Benchmarks
