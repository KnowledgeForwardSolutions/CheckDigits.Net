# CheckDigits.Net

CheckDigits.Net brings together in one library an extensive collection of different
check digit algorithms. CheckDigits.Net has the goal that each algorithm supported
be optimized, be resilient to malformed input and that memory allocations be 
minimized or eliminated completely. Benchmarks for each algorithm are provided to
demonstrate performance over a range of values and the memory allocation (if any).

## Check Digit Overview

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

## Supported Algorithms

* [ABA RTN (Routing Transit Number) Algorithm](#aba-rtn-algorithm)
* [Damm Algorithm](#damm-algorithm)
* [Luhn Algorithm](#luhn-algorithm)
* [Modulus10_13 Algorithm (UPC/EAN/ISBN-13/etc.)](#modulus10_13-algorithm)
* [Modulus11 Algorithm (ISBN-10/ISSN/etc.)](#modulus11-algorithm)
* [NPI (US National Provider Identifier) Algorithm](#npi-algorithm)
* [Verhoeff Algorithm](#verhoeff-algorithm)
* [VIN (Vehicle Identification Number) Algorithm](#vin-algorithm)

## Value/Identifier Type and Associated Algorithm

| Value/Identifier Type | Algorithm |
| --------------------- | ----------|
| ABA RTN				| [ABA RTN Algorithm](#aba-rtn-algorithm) |
| Credit card number    | [Luhn Algorithm](#luhn-algorithm) |
| EAN-8					| [Modulus10_13 Algorithm](#modulus10_13-algorithm) |
| EAN-13				| [Modulus10_13 Algorithm](#modulus10_13-algorithm) |
| GTIN-8				| [Modulus10_13 Algorithm](#modulus10_13-algorithm) |
| GTIN-12				| [Modulus10_13 Algorithm](#modulus10_13-algorithm) |
| GTIN-13				| [Modulus10_13 Algorithm](#modulus10_13-algorithm) |
| GTIN-14				| [Modulus10_13 Algorithm](#modulus10_13-algorithm) |
| IMEI				    | [Luhn Algorithm](#luhn-algorithm) |
| ISBN-10				| [Modulus11 Algorithm](#modulus11-algorithm) |
| ISBN-13				| [Modulus10_13 Algorithm](#modulus10_13-algorithm) |
| ISMN					| [Modulus10_13 Algorithm](#modulus10_13-algorithm) |
| ISSN   				| [Modulus11 Algorithm](#modulus11-algorithm) |
| NPI   				| [NPI Algorithm](#npi-algorithm) |
| SSCC					| [Modulus10_13 Algorithm](#modulus10_13-algorithm) |
| VIN                   | [VIN Algorithm](#vin-algorithm) |
| UPC-A					| [Modulus10_13 Algorithm](#modulus10_13-algorithm) |
| UPC-E					| [Modulus10_13 Algorithm](#modulus10_13-algorithm) |

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

## Algorithm Descriptions

### ABA RTN Algorithm

#### Description

The American Bankers Association (ABA) Routing Transit Number (RTN) algorithm is
a modulus 10 algorithm that uses weights 3, 7 and 1. The algorithm can detect all
single digit transcription errors and most two digit transposition errors except
those where the transposed digits differ by 5 (i.e. *1 <-> 6*, *2 <-> 7*, etc.).

#### Details

* Valid characters - decimal digits ('0' - '9')
* Check digit size - one character
* Check digit value - decimal digit ('0' - '9')
* Check digit location - ninth digit
* Max length - 8 characters when generating a check digit; 9 characters when validating

#### Links

Wikipedia: https://en.wikipedia.org/wiki/ABA_routing_transit_number#Check_digit

### Damm Algorithm

#### Description

The Damm algorithm was first described by H. Michael Damm in 2004. It is similar
to the Verhoeff algorithm in that it can detect all single digit transcription
errors and all two digit transposition errors and that it uses a precomputed table
instead of modulus operations to calculate the check digit. Unlike the Verhoeff 
algorithm, the Damm algorithm uses a single quasigroup table of order 10 instead 
of the multiple tables used by Verhoeff. The implementation of the Damm algorithm
provided by CheckDigits.Net uses the table generated from the quasigroup specified
on page 111 of Damm's doctoral dissertation.

#### Details

* Valid characters - decimal digits ('0' - '9')
* Check digit size - one character
* Check digit value - decimal digit ('0' - '9')
* Check digit location - assumed to be the trailing (right-most) character when validating

#### Links

Wikipedia: https://en.wikipedia.org/wiki/Damm_algorithm

### Luhn Algorithm

#### Description

The Luhn algorithm is a modulus 10 algorithm that was developed in 1960 by Hans
Peter Luhn. It can detect all single digit transcription errors and most two digit
transposition errors except *09 -> 90* and vice versa. It can also detect most
twin errors (i.e. *11 <-> 44*) except *22 <-> 55*,  *33 <-> 66* and *44 <-> 77*.

#### Details

* Valid characters - decimal digits ('0' - '9')
* Check digit size - one character
* Check digit value - decimal digit ('0' - '9')
* Check digit location - assumed to be the trailing (right-most) character when validating

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
etc.). The algorithm cannot detect two digit jump transpositions.

#### Details

* Valid characters - decimal digits ('0' - '9')
* Check digit size - one character
* Check digit value - decimal digit ('0' - '9')
* Check digit location - assumed to be the trailing (right-most) character when validating

#### Common Applications

* Global Trade Item Number (GTIN-8, GTIN-12, GTIN-13, GTIN-14)
* International Article Number/European Article Number (EAN-8, EAN-13)
* International Standard Book Number, starting January 1, 2007 (ISBN-13)
* International Standard Music Number (ISMN)
* Serial Shipping Container Code (SSCC)
* Universal Product Code (UPC-A, UPC-E)

#### Links

Wikipedia: 
  https://en.wikipedia.org/wiki/Universal_Product_Code#Check_digit_calculation
  https://en.wikipedia.org/wiki/International_Article_Number#Calculation_of_checksum_digit

### Modulus11 Algorithm

#### Description

The Modulus11 algorithm uses modulus 11 and each digit is weighted by its position
in the value, starting from the right-most digit. Prior to the existence of the
Verhoeff algorithm and the Damm algorithm it was popular because it was able to
detect two digit transposition errors while using only a single character. However,
because it used modulus 11, the check digit could not be a single decimal digit.
Commonly an 'X' character was used when the modulus operation resulted in a value
of 10. This meant that identifiers that used the Modulus11 algorithm could not be
stored as numbers and instead must be strings.

#### Details

* Valid characters - decimal digits ('0' - '9')
* Check digit size - one character
* Check digit value - either decimal digit ('0' - '9') or an uppercase 'X'
* Check digit location - assumed to be the trailing (right-most) character when validating
* Max length - 9 characters when generating a check digit; 10 characters when validating

#### Common Applications

* International Standard Book Number, prior to January 1, 2007 (ISBN-10)
* International Standard Serial Number (ISSN)

#### Links

Wikipedia: 
  https://en.wikipedia.org/wiki/ISBN#ISBN-10_check_digits
  https://en.wikipedia.org/wiki/ISSN

### NPI Algorithm

#### Description

US National Provider Identifiers (NPI) use the Luhn algorithm to calculate the
check digit located in the trailing (right-most) position. However, before 
calculating, the value is prefixed with a constant "80840" and the check digit
is calculated using the entire 15 digit string. The resulting check digit has all
the capabilities of the base Luhn algorithm (detecting all single digit transcription 
errors and most two digit transposition errors except *09 -> 90* and vice versa
as well as most twin errors (i.e. *11 <-> 44*) except *22 <-> 55*,  *33 <-> 66* 
and *44 <-> 77*.

(You can create and validate NPI check digits using the standard Luhn algorithm 
by first prefixing your value with "80840". However, CheckDigits.Net's 
implementation of the NPI algorithm handles the prefix internally and without 
allocating an extra string.)

#### Details

* Valid characters - decimal digits ('0' - '9')
* Check digit size - one character
* Check digit value - decimal digit ('0' - '9')
* Check digit location - assumed to be the trailing (right-most) character when validating
* Max length - 9 characters when generating a check digit; 10 characters when validating

#### Links

Wikipedia: https://en.wikipedia.org/wiki/National_Provider_Identifier

### Verhoeff Algorithm

#### Description

The Verhoeff algorithm was the first algorithm using a single decimal check digit
that was capable of detecting all single digit transcription errors and all two 
digit transposition errors. It was first described by Jacobus Verhoeff in 1969.
Prior to Verhoeff it was believed that it was not possible to define an algorithm
that used a single decimal check digit that could detect both all single digit
transcription errors *and* all two digit transposition errors. Verhoeff's algorithm
does not use modulus operations and instead uses a dihedral group (typically
implemented as a set of lookup tables). Additionally, Verhoeff's algorithm can
detect many, though not all, twin errors, two digit jump transpositions and jump
twin errors.

#### Details

* Valid characters - decimal digits ('0' - '9')
* Check digit size - one character
* Check digit value - decimal digit ('0' - '9')
* Check digit location - assumed to be the trailing (right-most) character when validating

#### Links

Wikipedia: https://en.wikipedia.org/wiki/Verhoeff_algorithm

### VIN Algorithm

#### Description

The VIN (Vehicle Identification Number) algorithm is used on the VIN of vehicles
sold in North America (US and Canada). The check digit is the 9th character of
the 17 character value. Upper-case alphabetic characters (except 'I', 'O' and 'Q')
are allowed in the value and must be transliterated to integer values before 
weighting, summing and calculating sum modulus 11.

#### Details

* Valid characters - decimal digits ('0' - '9') and upper case letters ('A' - 'Z'), excluding 'I', 'O' and 'Q'
* Check digit size - one character
* Check digit value - either decimal digit ('0' - '9') or an uppercase 'X'
* Check digit location - 9th character of 17
* Length - 17 characters

#### Links

Wikipedia: https://en.wikipedia.org/wiki/Vehicle_identification_number#Check-digit_calculation

## Benchmarks

* [ABA RTN Algorithm](#aba-rtn-algorithm-benchmarks)
* [Damm Algorithm](#damm-algorithm-benchmarks)
* [Luhn Algorithm](#luhn-algorithm-benchmarks)
* [Modulus10_13 Algorithm](#modulus10_13-algorithm-benchmarks)
* [Modulus11 Algorithm](#modulus11-algorithm-benchmarks)
* [NPI Algorithm](#npi-algorithm-benchmarks)
* [Verhoeff Algorithm](#verhoeff-algorithm-benchmarks)
* [VIN Algorithm](#vin-algorithm-benchmarks)

### ABA RTN Algorithm Benchmarks

| Method                 | Value     | Mean     | Error     | StdDev    | Allocated |
|----------------------- |---------- |---------:|----------:|----------:|----------:|
| TryCalculateCheckDigit | 11100002  | 11.76 ns | 0.1060 ns | 0.0940 ns |         - |
| TryCalculateCheckDigit | 12223582  | 10.41 ns | 0.1230 ns | 0.1150 ns |         - |
| Validate               | 111000025 | 9.766 ns | 0.1837 ns | 0.1719 ns |         - |
| Validate               | 122235821 | 7.654 ns | 0.0508 ns | 0.0424 ns |         - |

### Damm Algorithm Benchmarks

| Method                 | Value            | Mean      | Error     | StdDev    | Allocated |
|----------------------- |----------------- |----------:|----------:|----------:|----------:|
| TryCalculateCheckDigit | 123              |  6.777 ns | 0.1300 ns | 0.1391 ns |         - |
| TryCalculateCheckDigit | 1234567          | 10.331 ns | 0.0842 ns | 0.0747 ns |         - |
| TryCalculateCheckDigit | 12345678901      | 17.846 ns | 0.1478 ns | 0.1383 ns |         - |
| TryCalculateCheckDigit | 123456789012345  | 24.807 ns | 0.1143 ns | 0.1013 ns |         - |
| Validate               | 1234             |  7.076 ns | 0.0772 ns | 0.0722 ns |         - |
| Validate               | 12345671         | 14.179 ns | 0.1678 ns | 0.1569 ns |         - |
| Validate               | 123456789018     | 20.616 ns | 0.1890 ns | 0.1676 ns |         - |
| Validate               | 1234567890123450 | 27.317 ns | 0.3768 ns | 0.3340 ns |         - |

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

| Method                 | Value              | Mean      | Error     | StdDev    | Allocated |
|----------------------- |------------------- |----------:|----------:|----------:|----------:|
| TryCalculateCheckDigit | 42526              |  9.252 ns | 0.1673 ns | 0.1565 ns |         - |
| TryCalculateCheckDigit | 7351353            | 11.178 ns | 0.1086 ns | 0.1016 ns |         - |
| TryCalculateCheckDigit | 03600029145        | 16.945 ns | 0.2497 ns | 0.2336 ns |         - |
| TryCalculateCheckDigit | 400638133393       | 17.807 ns | 0.2595 ns | 0.2427 ns |         - |
| TryCalculateCheckDigit | 01234567800004567  | 25.594 ns | 0.3092 ns | 0.2892 ns |         - |
| Validate               | 425261             | 10.750 ns | 0.0710 ns | 0.0630 ns |         - |
| Validate               | 73513537           | 13.770 ns | 0.2600 ns | 0.2310 ns |         - |
| Validate               | 036000291452       | 17.660 ns | 0.1380 ns | 0.1230 ns |         - |
| Validate               | 4006381333931      | 18.990 ns | 0.1660 ns | 0.1390 ns |         - |
| Validate               | 012345678000045678 | 28.090 ns | 0.2570 ns | 0.2400 ns |         - |

### Modulus11 Algorithm Benchmarks

| Method                 | Value      | Mean      | Error     | StdDev    | Allocated |
|----------------------- |----------- |----------:|----------:|----------:|----------:|
| TryCalculateCheckDigit | 123        |  4.294 ns | 0.0272 ns | 0.0242 ns |         - |
| TryCalculateCheckDigit | 0317847    |  8.360 ns | 0.1112 ns | 0.1040 ns |         - |
| TryCalculateCheckDigit | 050027293  |  7.497 ns | 0.1352 ns | 0.1265 ns |         - |
| Validate               | 1235       |  5.634 ns | 0.0657 ns | 0.0549 ns |         - |
| Validate               | 03178471   |  9.295 ns | 0.1752 ns | 0.1874 ns |         - |
| Validate               | 050027293X | 10.052 ns | 0.1027 ns | 0.0911 ns |         - |

### NPI Algorithm Benchmarks

| Method                 | Value      | Mean     | Error    | StdDev   | Allocated |
|----------------------- |----------- |---------:|---------:|---------:|----------:|
| TryCalculateCheckDigit | 123456789  | 14.87 ns | 0.231 ns | 0.216 ns |         - |
| TryCalculateCheckDigit | 124531959  | 13.33 ns | 0.186 ns | 0.174 ns |         - |
| Validate               | 1234567893 | 15.59 ns | 0.296 ns | 0.277 ns |         - |
| Validate               | 1245319599 | 14.51 ns | 0.256 ns | 0.239 ns |         - |

### Verhoeff Algorithm Benchmarks

| Method                 | Value            | Mean     | Error    | StdDev   | Allocated |
|----------------------- |----------------- |---------:|---------:|---------:|----------:|
| TryCalculateCheckDigit | 123              | 11.19 ns | 0.140 ns | 0.131 ns |         - |
| TryCalculateCheckDigit | 1234567          | 19.94 ns | 0.196 ns | 0.174 ns |         - |
| TryCalculateCheckDigit | 12345678901      | 30.21 ns | 0.313 ns | 0.293 ns |         - |
| TryCalculateCheckDigit | 123456789012345  | 40.84 ns | 0.575 ns | 0.538 ns |         - |
| Validate               | 1233             | 12.70 ns | 0.169 ns | 0.150 ns |         - |
| Validate               | 12345679         | 25.02 ns | 0.163 ns | 0.136 ns |         - |
| Validate               | 123456789010     | 36.81 ns | 0.304 ns | 0.270 ns |         - |
| Validate               | 1234567890123455 | 48.69 ns | 0.255 ns | 0.199 ns |         - |

### VIN Algorithm Benchmarks

