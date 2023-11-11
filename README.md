# CheckDigits.Net

CheckDigits.Net brings together in one library an extensive collection of different
check digit algorithms. CheckDigits.Net has the goal that each algorithm supported
be optimized, be resilient to malformed input and that memory allocations be 
minimized or eliminated completely. Benchmarks for each algorithm are provided to
demonstrate performance over a range of values and the memory allocation (if any).

Benchmarks have shown that the optimized versions of the algorithms in CheckDigits.Net 
are up to 10X-50X faster than those in popular Nuget packages.

## Table of Contents

- **[Check Digit Overview](#check-digit-overview)**
- **[ISO/IEC 7064 Algorithms](#isoiec-7064-algorithms)**
- **[Supported Algorithms](#supported-algorithms)**
- **[Value/Identifier Types and Associated Algorithms](#valueidentifier-types-and-associated-algorithms)**
- **[Using CheckDigits.Net](#using-checkdigits.net)**
- **[Algorithm Descriptions](#algorithm-descriptions)**
    * [ABA RTN (Routing Transit Number) Algorithm](#aba-rtn-algorithm)
    * [Alphanumeric MOD 97-10 Algorithm](#alphanumeric-mod-97-10-algorithm)
    * [Damm Algorithm](#damm-algorithm)
    * [IBAN (International Bank Account Number) Algorithm](#iban-algorithm)
    * [ISIN (International Securities Identification Number) Algorithm](#isin-algorithm)
    * [ISO/IEC 7064 MOD 11,10 Algorithm](#isoiec-7064-mod-1110-algorithm)
    * [ISO/IEC 7064 MOD 11-2 Algorithm](#isoiec-7064-mod-11-2-algorithm)
    * [ISO/IEC 7064 MOD 1271-36 Algorithm](#isoiec-7064-mod-1271-36-algorithm)
    * [ISO/IEC 7064 MOD 27,26 Algorithm](#isoiec-7064-mod-2726-algorithm)
    * [ISO/IEC 7064 MOD 37-2 Algorithm](#isoiec-7064-mod-37-2-algorithm)
    * [ISO/IEC 7064 MOD 37-36 Algorithm](#isoiec-7064-mod-3736-algorithm)
    * [ISO/IEC 7064 MOD 661-26 Algorithm](#isoiec-7064-mod-661-26-algorithm)
    * [ISO/IEC 7064 MOD 97-10 Algorithm](#isoiec-7064-mod-97-10-algorithm)
    * [Luhn Algorithm](#luhn-algorithm)
    * [Modulus10_1 Algorithm](#modulus10_1-algorithm)
    * [Modulus10_2 Algorithm](#modulus10_2-algorithm)
    * [Modulus10_13 Algorithm (UPC/EAN/ISBN-13/etc.)](#modulus10_13-algorithm)
    * [Modulus11 Algorithm (ISBN-10/ISSN/etc.)](#modulus11-algorithm)
    * [NHS (UK National Health Service) Algorithm](#nhs-algorithm)
    * [NPI (US National Provider Identifier) Algorithm](#npi-algorithm)
    * [Verhoeff Algorithm](#verhoeff-algorithm)
    * [VIN (Vehicle Identification Number) Algorithm](#vin-algorithm)
- **[Benchmarks](#benchmarks)**
- **[Release History/Release Notes](#release-historyrelease-notes)**
    - [v1.0.0-alpha](#v100alpha)
    - [v1.0.0](#v100)

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

Note also that if a value has a valid check digit, it does not imply that the 
value is valid, only that the value was transcribed correctly. There may be other
requirements that are specific to the type of value that could cause a value with
a valid check digit to be considered incorrect/invalid.

## ISO/IEC 7064 Algorithms

The ISO/IEC 7064 standard defines a family of algorithms capable of detecting a
broad range of errors including all single character transcription errors as well
as all or nearly all two character transposition errors, two character jump 
transposition errors, circular shift errors and double transcription errors (two
separate single transcription errors in a single value). The algorithms are 
suitable for numeric strings, alphabetic strings, alphanumeric strings and can
be extended to handle custom character domains beyond ASCII alphanumeric 
characters.

ISO/IEC 7064 algorithms fall into different categories. Pure system algorithms
use a single modulus value and a radix value and can generate one or two check 
characters, depending on the algorithm. If a pure system algorithm generates a 
single check character, the check character produced will either be one of the 
valid input characters or a single supplementary character that is only valid as
a check digit. Hybrid system algorithms use two modulus values, M and M+1, and
generate a single check character that will be one of the valid input characters.

While CheckDigits.Net provides optimized implementations of all of the algorithms
defined in the ISO/IEC 7064 standard, the standard is flexible enough to support
the creation of algorithms for custom alphabets. For example, Annex B of the 
ISO/IEC 7064 standard demonstrates the creation of a system for the Danish 
alphabet which includes three additional characters. 

CheckDigits.Net includes three classes to support custom alphabets:

* ```Iso7064PureSystemSingleCharacterAlgorithm``` (generates a single check character, including a supplementary character)
* ```Iso7064PureSystemDoubleCharacterAlgorithm``` (generates two check characters)
* ```Iso7064HybridSystemAlgorithm``` (generates a single check character)

Refer to [Using CheckDigits.Net](#using-checkdigits.net) for more information
about using these classes.

The ISO/IEC 7064:2003 standard is available at https://www.iso.org/standard/31531.html

## Supported Algorithms

* [ABA RTN (Routing Transit Number) Algorithm](#aba-rtn-algorithm)
* [Alphanumeric MOD 97-10 Algorithm](#alphanumeric-mod-97-10-algorithm)
* [Damm Algorithm](#damm-algorithm)
* [IBAN (International Bank Account Number) Algorithm](#iban-algorithm)
* [ISIN (International Securities Identification Number) Algorithm](#isin-algorithm)
* [ISO/IEC 7064 MOD 11,10 Algorithm](#isoiec-7064-mod-1110-algorithm)
* [ISO/IEC 7064 MOD 11-2 Algorithm](#isoiec-7064-mod-11-2-algorithm)
* [ISO/IEC 7064 MOD 1271-36 Algorithm](#isoiec-7064-mod-1271-36-algorithm)
* [ISO/IEC 7064 MOD 27,26 Algorithm](#isoiec-7064-mod-2726-algorithm)
* [ISO/IEC 7064 MOD 37-2 Algorithm](#isoiec-7064-mod-37-2-algorithm)
* [ISO/IEC 7064 MOD 37-36 Algorithm](#isoiec-7064-mod-3736-algorithm)
* [ISO/IEC 7064 MOD 661-26 Algorithm](#isoiec-7064-mod-661-26-algorithm)
* [ISO/IEC 7064 MOD 97-10 Algorithm](#isoiec-7064-mod-97-10-algorithm)
* [Luhn Algorithm](#luhn-algorithm)
* [Modulus10_1 Algorithm](#modulus10_1-algorithm)
* [Modulus10_2 Algorithm](#modulus10_2-algorithm)
* [Modulus10_13 Algorithm (UPC/EAN/ISBN-13/etc.)](#modulus10_13-algorithm)
* [Modulus11 Algorithm (ISBN-10/ISSN/etc.)](#modulus11-algorithm)
* [NHS (UK National Health Service) Algorithm](#nhs-algorithm)
* [NPI (US National Provider Identifier) Algorithm](#npi-algorithm)
* [Verhoeff Algorithm](#verhoeff-algorithm)
* [VIN (Vehicle Identification Number) Algorithm](#vin-algorithm)

## Value/Identifier Types and Associated Algorithms

| Value/Identifier Type | Algorithm |
| --------------------- | ----------|
| ABA Routing Transit Number | [ABA RTN Algorithm](#aba-rtn-algorithm) |
| CA Social Insurance Number | [Luhn Algorithm](#luhn-algorithm) |
| CAS Registry Number   | [Modulus10 Algorithm](#modulus10_1-algorithm) |
| Credit card number    | [Luhn Algorithm](#luhn-algorithm) |
| EAN-8					| [Modulus10_13 Algorithm](#modulus10_13-algorithm) |
| EAN-13				| [Modulus10_13 Algorithm](#modulus10_13-algorithm) |
| Global Release Identifier | [ISO/IEC 7064 MOD 37-36 Algorithm](#isoiec-7064-mod-3736-algorithm) |
| GTIN-8				| [Modulus10_13 Algorithm](#modulus10_13-algorithm) |
| GTIN-12				| [Modulus10_13 Algorithm](#modulus10_13-algorithm) |
| GTIN-13				| [Modulus10_13 Algorithm](#modulus10_13-algorithm) |
| GTIN-14				| [Modulus10_13 Algorithm](#modulus10_13-algorithm) |
| IBAN                  | [IBAN Algorithm](#iban-algorithm) |
| IMEI				    | [Luhn Algorithm](#luhn-algorithm) |
| IMO Number            | [Modulus10 Algorithm](#modulus10_2-algorithm) |
| ISBN-10				| [Modulus11 Algorithm](#modulus11-algorithm) |
| ISBN-13				| [Modulus10_13 Algorithm](#modulus10_13-algorithm) |
| ISBT Donation Identification Number |  [ISO/IEC 7064 MOD 37-2 Algorithm](#isoiec-7064-mod-37-2-algorithm) |
| ISIN                  | [ISIN Algorithm](#isin-algorithm) |
| ISMN					| [Modulus10_13 Algorithm](#modulus10_13-algorithm) |
| ISNI                  | [ISO/IEC 7064 MOD 11-2 Algorithm](#isoiec-7064-mod-11-2-algorithm) |
| ISSN   				| [Modulus11 Algorithm](#modulus11-algorithm) |
| Legal Entity Identifier | [Alphanumeric MOD 97-10 Algorithm](#alphanumeric-mod-97-10-algorithm) |
| UK National Health Service Number | [NHS Algorithm](#nhs-algorithm) |
| US National Provider Identifier | [NPI Algorithm](#npi-algorithm) |
| SSCC					| [Modulus10_13 Algorithm](#modulus10_13-algorithm) |
| Vehicle Identification Number | [VIN Algorithm](#vin-algorithm) |
| Universal Loan Identifier | [Alphanumeric MOD 97-10 Algorithm](#alphanumeric-mod-97-10-algorithm) |
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
var lazy = Algorithms.Luhn;


// Calculate the check digit for a value that does not already contain a check digit.
var newValue = "123456789012345";
var successful = algorithm.TryCalculateCheckDigit(newValue, out var checkDigit);  // Returns true; checkDigit will equal '2'

// Validate a value that contains a check digit.
var toValidate = "1234567890123452";
var isValid = lazy.Validate(toValidate);    // Returns true
```

**Custom Alphabets for ISO 7064**

The three classes that allow the use of custom alphabets are:

* ```Iso7064PureSystemSingleCharacterAlgorithm``` (generates a single check character, including a supplementary character)
* ```Iso7064PureSystemDoubleCharacterAlgorithm``` (generates two check characters)
* ```Iso7064HybridSystemAlgorithm``` (generates a single check character)

To use one of these classes you must first create an instance of a class that 
implements ```IAlphabet``` or ```ISupplementalCharacterAlphabet```. Then you 
create an instance of the desired generic ISO 7064 class, supplying the algorithm 
details (including the alphabet) to the class constructor.

The custom Danish alphabet check algorithm covered in Annex B of the ISO/IEC 7064 
standard, uses a pure system algorithm that generates two check characters and 
has a modulus = 29 and radix = 2.

**Danish Alphabet Example**

```
public class DanishAlphabet : IAlphabet
{
   // Additional characters:
   // diphthong AE (\u00C6) has value 26
   // slashed O (\u00D8) has value 27
   // A with diaeresis (\u00C4) has value 28
   private const String _validCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ\u00C6\u00D8\u00C4";

   public Int32 CharacterToInteger(Char ch)
      => ch switch
      {
         var x when x >= 'A' && x <= 'Z' => x - 'A',
         '\u00C6' => 26,
         '\u00D8' => 27,
         '\u00C4' => 28,
         _ => -1
      };

   public Char IntegerToCheckCharacter(Int32 checkDigit) => _validCharacters[checkDigit];
}

var checkAlgorithm = new Iso7064PureSystemDoubleCharacterAlgorithm(
    "Danish", 
    "Danish, modulus = 29, radix = 2", 
    29, 
    2, 
    new DanishAlphabet());

// Calculate the check digit for Danish word for sister (uses slashed O instead of i)
var str = "S\u00D8STER";
var successful = checkAlgorithm.TryCalculateCheckDigits(str, out var firstChar, out var secondChar);    // Returns true, firstChar = 'D', secondChar = 'A'


// Validate a value containing check digit(s).
var isValid = checkAlgorithm.Validate("S\u00D8STERDA");     // Returns true
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
```IDoubleCheckDigitAlgorithm```. This interface has a TryCalculateCheckDigits
method that has two output parameters, one for each check digit.

Note that ```ISingleCheckDigitAlgorithm``` and ```IDoubleCheckDigitAlgorithm```
are not implemented for algorithms for government issued identifiers (for example,
UK NHS numbers and US NPI numbers) or values issued by a single authority (such
as ABA Routing Transit Numbers).

The ```IAlphabet``` and ```ISupplementalCharacterAlphabet``` interfaces are used 
for ISO/IEC 7064 algorithms with custom alphabets. ```IAlphabet``` has two 
methods: CharacterToInteger, which maps a character in the value being processed 
to its integer equivalent and IntegerToCheckCharacter which maps a calculated 
check digit to its character equivalent. ```ISupplementalCharacterAlphabet``` 
extends ```IAlphabet``` by adding the CheckCharacterToInteger method which maps 
a check character to its integer equivalent. ```ISupplementalCharacterAlphabet```
is only used by ```Iso7064PureSystemSingleCharacterAlgorithm```.

## Algorithm Descriptions

### ABA RTN Algorithm

#### Description

The American Bankers Association (ABA) Routing Transit Number (RTN) algorithm is
a modulus 10 algorithm that uses weights 3, 7 and 1. The algorithm can detect all
single digit transcription errors and most two digit transposition errors except
those where the transposed digits differ by 5 (i.e. *1 <-> 6*, *2 <-> 7*, etc.).

The ABA RTN algorithm only supports validation of check digits and does support 
calculation of check digits.

#### Details

* Valid characters - decimal digits ('0' - '9')
* Check digit size - one character
* Check digit value - decimal digit ('0' - '9')
* Check digit location - ninth digit
* Value length - 9 characters
* Class name - AbaRtnAlgorithm

#### Links

Wikipedia: https://en.wikipedia.org/wiki/ABA_routing_transit_number#Check_digit

### Alphanumeric MOD 97-10 Algorithm

#### Description

The Alphanumeric MOD 97-10 algorithm uses a variation of the ISO/IEC 7064 MOD 97-10 
algorithm where alphabetic characters (A-Z) are mapped to integers (10-35) before 
calculating the check digit. The algorithm is case insensitive and lowercase 
letters are mapped to their uppercase equivalent before conversion to integers.

#### Details

* Valid characters - alphanumeric characters ('0' - '9', 'A' - 'Z')
* Check digit size - two characters
* Check digit value - decimal digits ('0' - '9')
* Check digit location - assumed to be the trailing (right-most) characters when validating
* Class name - AlphanumericMod97_10Algorithm

#### Common Applications

* Legal Entity Identifier (LEI)
* Universal Loan Identifier (ULI)

#### Links

Wikipedia: https://en.wikipedia.org/wiki/Legal_Entity_Identifier

https://www.govinfo.gov/content/pkg/CFR-2016-title12-vol8/xml/CFR-2016-title12-vol8-part1003-appC.xml

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
* Class name - DammAlgorithm

#### Links

Wikipedia: https://en.wikipedia.org/wiki/Damm_algorithm

### IBAN Algorithm

#### Description

The IBAN (International Bank Account Number) algorithm uses a variation of the
ISO/IEC 7064 MOD 97-10 algorithm where alphabetic characters (A-Z) are mapped
to integers (10-35) before calculating the check digit. Additionally, the first
four characters (2 character country code and 2 decimal check digits) are moved
to the end of the string before calculating the check digit.

Note that this implementation only confirms that the length of the value is
sufficient to calculate the check digits (min length = 5) and that check digit 
characters in positions 3 & 4 are valid for the string. All other IBAN checks
(the leading two characters indicating a valid country code, the check digit 
positions only contain digits, maximum length, country specific check digits 
contained in account number, etc.) are left to the application developer.

#### Details

* Valid characters - alphanumeric characters ('0' - '9', 'A' - 'Z')
* Check digit size - two characters
* Check digit value - decimal digits ('0' - '9')
* Check digit location - character positions 3 & 4 (1-based) when validating
* Value minimum length - 5
* Class name - IbanAlgorithm

#### Common Applications

* International Securities Identification Number (ISIN)

#### Links

Wikipedia: https://en.wikipedia.org/wiki/International_Bank_Account_Number

### ISIN Algorithm

#### Description

The ISIN (International Securities Identification Number) algorithm uses a 
variation of the Luhn algorithm and has all of the capabilities of the Luhn 
algorithm, including the ability to detect all single digit (or character) 
transcription errors and most two digit transposition errors except *09 -> 90* 
and vice versa. 

The algorithm has significant weaknesses. Transpositions of two letters cannot 
be detected. Additionally, transpositions of a digit character and the letters B, 
M or X cannot be detected (because B is converted to 11, M to 22 and X to 33 and 
when combined with another digit, the result is a jump transposition that the Luhn 
algorithm cannot detect).

#### Details

* Valid characters - alphanumeric characters ('0' - '9', 'A' - 'Z')
* Check digit size - one character
* Check digit value - decimal digit ('0' - '9')
* Check digit location - assumed to be the trailing (right-most) character when validating
* Value length - 12
* Class name - IsinAlgorithm

#### Common Applications

* International Securities Identification Number (ISIN)

#### Links

Wikipedia: https://en.wikipedia.org/wiki/International_Securities_Identification_Number

### ISO/IEC 7064 MOD 11,10 Algorithm

The ISO/IEC 7064 MOD 11,10 algorithm is a hybrid system algorithm (with M = 10
and M+1 = 11) that is suitable for use with numeric strings. It generates a 
single check character that is a decimal digit.

#### Details

* Valid characters - decimal digits ('0' - '9')
* Check digit size - one character
* Check digit value - decimal digit ('0' - '9')
* Check digit location - assumed to be the trailing (right-most) character when validating
* Class name - Iso7064Mod11_10Algorithm

### ISO/IEC 7064 MOD 11-2 Algorithm

The ISO/IEC 7064 MOD 11-2 algorithm is a pure system algorithm (with modulus 11
and radix 2) that is suitable for use with numeric strings. It generates a 
single check character that is either a decimal digit or a supplementary 'X' 
character.

#### Details

* Valid characters - decimal digits ('0' - '9')
* Check digit size - one character
* Check digit value - either decimal digit ('0' - '9') or an uppercase 'X'
* Check digit location - assumed to be the trailing (right-most) character when validating
* Class name - Iso7064Mod11_2Algorithm

#### Common Applications

* International Standard Name Identifier (ISNI)

### ISO/IEC 7064 MOD 1271-36 Algorithm

The ISO/IEC 7064 MOD 1271-36 algorithm is a pure system algorithm (with modulus 
1271 and radix 36) that is suitable for use with alphanumeric strings. It 
generates two check alphanumeric characters.

#### Details

* Valid characters - alphanumeric characters ('0' - '9', 'A' - 'Z')
* Check digit size - two characters
* Check digit value - alphanumeric characters ('0' - '9', 'A' - 'Z')
* Check digit location - assumed to be the trailing (right-most) characters when validating
* Class name - Iso7064Mod1271_36Algorithm

### ISO/IEC 7064 MOD 27,26 Algorithm

The ISO/IEC 7064 MOD 27,26 algorithm is a hybrid system algorithm (with M = 26
and M+1 = 27) that is suitable for use with alphabetic strings. It generates a 
single check character that is an alphabetic character.

#### Details

* Valid characters - alphabetic characters ('A' - 'Z')
* Check digit size - one character
* Check digit value - alphabetic characters ('A' - 'Z')
* Check digit location - assumed to be the trailing (right-most) character when validating
* Class name - Iso7064Mod27_26Algorithm

### ISO/IEC 7064 MOD 37-2 Algorithm

The ISO/IEC 7064 MOD 37-2 algorithm is a pure system algorithm (with modulus 37
and radix 2) that suitable for use with alphanumeric strings. It generates a 
single check character that is either an alphanumeric character or a 
supplementary '*' character.

#### Details

* Valid characters - alphanumeric characters ('0' - '9', 'A' - 'Z')
* Check digit size - one character
* Check digit value - either decimal digit ('0' - '9', 'A' - 'Z') or an asterisk '*'
* Check digit location - assumed to be the trailing (right-most) character when validating
* Class name - Iso7064Mod37_2Algorithm

### ISO/IEC 7064 MOD 37,36 Algorithm

The ISO/IEC 7064 MOD 37,36 algorithm is a hybrid system algorithm (with M = 36
and M+1 = 37) that is suitable for use with alphanumeric strings. It generates a 
single check character that is an alphanumeric character.

#### Details

* Valid characters - alphanumeric characters ('0' - '9', 'A' - 'Z')
* Check digit size - one character
* Check digit value - alphanumeric characters ('0' - '9', 'A' - 'Z')
* Check digit location - assumed to be the trailing (right-most) character when validating
* Class name - Iso7064Mod37_36Algorithm

#### Common Applications

* Global Release Identifier (GRid)

#### Common Applications

* International Society of Blood Transfusion (ISBT) Donation Identification Numbers

### ISO/IEC 7064 MOD 661-26 Algorithm

The ISO/IEC 7064 MOD 661-26 algorithm is a pure system algorithm (with modulus 
661 and radix 26) that is suitable for use with alphabetic strings. It generates 
two check alphabetic characters.

#### Details

* Valid characters - alphabetic characters ('A' - 'Z')
* Check digit size - two characters
* Check digit value - alphabetic characters ('A' - 'Z')
* Check digit location - assumed to be the trailing (right-most) characters when validating
* Class name - Iso7064Mod661_26Algorithm

### ISO/IEC 7064 MOD 97-10 Algorithm

The ISO/IEC 7064 MOD 97-10 algorithm is a pure system algorithm (with modulus 97
and radix 210) that is suitable for use with numeric strings. It generates a two 
numeric check digits.

Note: the ISO/IEC 7064 MOD 97-10 algorithm is the basis of a number of check digit 
algorithms that first map alphabetic characters to numbers between 10 and 35. 
Examples include International Bank Account Numbers (IBAN) and Universal Loan
Identifiers (ULI). However this implementation is limited to values containing
only decimal digits. Other algorithms will handle values like IBAN and ULI and 
perform the mapping of alphabetic characters internally.

#### Details

* Valid characters - decimal digits ('0' - '9')
* Check digit size - two characters
* Check digit value - decimal digits ('0' - '9')
* Check digit location - assumed to be the trailing (right-most) characters when validating
* Class name - Iso7064Mod97_10Algorithm

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
* Class name - LuhnAlgorithm

#### Common Applications

* Credit card numbers
* International Mobile Equipment Identity (IMEI) numbers
* Canadian Social Insurance Number (SIN)

#### Links

Wikipedia: https://en.wikipedia.org/wiki/Luhn_algorithm

### Modulus10_1 Algorithm

The Modulus10 algorithm uses modulus 10 and each digit is weighted by its position
in the value, starting with weight 1 for the right-most non-check digit character.

#### Details

* Valid characters - decimal digits ('0' - '9')
* Check digit size - one character
* Check digit value - either decimal digit ('0' - '9')
* Check digit location - assumed to be the trailing (right-most) character when validating
* Max length - 9 characters when generating a check digit; 10 characters when validating
* Class name - Modulus10_1Algorithm

#### Common Applications

* Chemical Abstracts Service (CAS) Registry Number

#### Links

Wikipedia: https://en.wikipedia.org/wiki/CAS_Registry_Number

### Modulus10_2 Algorithm

The Modulus10 algorithm uses modulus 10 and each digit is weighted by its position
in the value, starting with weight 2 for the right-most non-check digit character.

#### Details

* Valid characters - decimal digits ('0' - '9')
* Check digit size - one character
* Check digit value - either decimal digit ('0' - '9')
* Check digit location - assumed to be the trailing (right-most) character when validating
* Max length - 9 characters when generating a check digit; 10 characters when validating
* Class name - Modulus10_2Algorithm

#### Common Applications

* International Maritime Organization (IMO) Number

#### Links

Wikipedia: https://en.wikipedia.org/wiki/IMO_number

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
* Class name - Modulus10_13Algorithm

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
* Class name - Modulus11Algorithm

#### Common Applications

* International Standard Book Number, prior to January 1, 2007 (ISBN-10)
* International Standard Serial Number (ISSN)

#### Links

Wikipedia: 
  https://en.wikipedia.org/wiki/ISBN#ISBN-10_check_digits
  https://en.wikipedia.org/wiki/ISSN

### NHS Algorithm

#### Description

UK National Health Service (NHS) identifiers use a variation of the Modulus 11 
algorithm. However, instead of generating 11 possible values for the check digit,
the NHS algorithm does not allow a remainder of 10 (the 'X' character used by the
Modulus 11 algorithm). Any possible NHS number that would generate a remainder of 
10 is not allowed and those numbers are not issued. This means that the check 
digit for a NHS number remains '0' - '9'. The NHS algorithm retains all error 
detecting capabilities of the Modulus 11 algorithm (detecting all single digit 
transcription errors and all two digit transposition errors).

The NHS algorithm only supports validation of check digits and does support 
calculation of check digits.

#### Details

* Valid characters - decimal digits ('0' - '9')
* Check digit size - one character
* Check digit value - decimal digit ('0' - '9')
* Check digit location - assumed to be the trailing (right-most) character when validating
* Value length - 10 characters
* Class name - NhsAlgorithm

#### Links

Wikipedia: 
	https://en.wikipedia.org/wiki/NHS_number#Format,_number_ranges,_and_check_characters
	https://www.datadictionary.nhs.uk/attributes/nhs_number.html

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

The NPI algorithm only supports validation of check digits and does support 
calculation of check digits.

#### Details

* Valid characters - decimal digits ('0' - '9')
* Check digit size - one character
* Check digit value - decimal digit ('0' - '9')
* Check digit location - assumed to be the trailing (right-most) character when validating
* Value length - 10 characters
* Class name - NpiAlgorithm

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
* Class name - VerhoeffAlgorithm

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
* Class name - VinAlgorithm

#### Links

Wikipedia: https://en.wikipedia.org/wiki/Vehicle_identification_number#Check-digit_calculation

## Benchmarks

The methodology for the general algorithms is to generate values for the benchmarks
by taking substrings of lengths 3, 6, 9, etc. from the same randomly generated 
source string. For the TryCalculateCheckDigit or TryCalculateCheckDigits methods 
the substring is used as is. For the Validate method benchmarks the substring is 
appended with the check character or characters that make the test value valid 
for the algorithm being benchmarked.

For value specific algorithms, three separate values that are valid for the 
algorithm being benchmarked are used.

### TryCalculateCheckDigit/TryCalculateCheckDigits Methods

#### General Numeric Algorithms

Note that the Modulus10_1, Modulus10_2 and Modulus11 algorithms have a maximum 
length of 10 (including the check digit) for values being validated so their
benchmarks do not cover lengths greater than 10.

| Algorithm Name    | Value                 | Mean      | Error     | StdDev    | Allocated |
|------------------ |---------------------- |----------:|----------:|----------:|----------:|
| Damm              | 140                   |  4.949 ns | 0.1024 ns | 0.0908 ns |         - |
| Damm              | 140662                |  8.825 ns | 0.0712 ns | 0.0666 ns |         - |
| Damm              | 140662538             | 14.690 ns | 0.2001 ns | 0.1774 ns |         - |
| Damm              | 140662538042          | 19.815 ns | 0.1659 ns | 0.1471 ns |         - |
| Damm              | 140662538042551       | 24.766 ns | 0.1932 ns | 0.1713 ns |         - |
| Damm              | 140662538042551028    | 29.666 ns | 0.1946 ns | 0.1725 ns |         - |
| Damm              | 140662538042551028265 | 35.442 ns | 0.5445 ns | 0.5093 ns |         - |
|
| ISO/IEC 706 11,10 | 140                   |  7.461 ns | 0.0557 ns | 0.0465 ns |         - |
| ISO/IEC 706 11,10 | 140662                | 10.676 ns | 0.0782 ns | 0.0731 ns |         - |
| ISO/IEC 706 11,10 | 140662538             | 14.477 ns | 0.1290 ns | 0.1144 ns |         - |
| ISO/IEC 706 11,10 | 140662538042          | 18.263 ns | 0.2106 ns | 0.1970 ns |         - |
| ISO/IEC 706 11,10 | 140662538042551       | 21.087 ns | 0.1063 ns | 0.0942 ns |         - |
| ISO/IEC 706 11,10 | 140662538042551028    | 23.131 ns | 0.2303 ns | 0.2042 ns |         - |
| ISO/IEC 706 11,10 | 140662538042551028265 | 24.776 ns | 0.1663 ns | 0.1474 ns |         - |
|
| ISO/IEC 706 11-2  | 140                   |  6.268 ns | 0.1064 ns | 0.0995 ns |         - |
| ISO/IEC 706 11-2  | 140662                | 10.061 ns | 0.1256 ns | 0.1175 ns |         - |
| ISO/IEC 706 11-2  | 140662538             | 13.671 ns | 0.2918 ns | 0.2730 ns |         - |
| ISO/IEC 706 11-2  | 140662538042          | 17.033 ns | 0.1795 ns | 0.1679 ns |         - |
| ISO/IEC 706 11-2  | 140662538042551       | 20.707 ns | 0.2469 ns | 0.2189 ns |         - |
| ISO/IEC 706 11-2  | 140662538042551028    | 24.420 ns | 0.1823 ns | 0.1616 ns |         - |
| ISO/IEC 706 11-2  | 140662538042551028265 | 27.453 ns | 0.1923 ns | 0.1799 ns |         - |
|
| ISO/IEC 706 97-10 | 140                   |  6.853 ns | 0.1224 ns | 0.1085 ns |         - |
| ISO/IEC 706 97-10 | 140662                | 10.487 ns | 0.1345 ns | 0.1258 ns |         - |
| ISO/IEC 706 97-10 | 140662538             | 15.189 ns | 0.1493 ns | 0.1247 ns |         - |
| ISO/IEC 706 97-10 | 140662538042          | 18.234 ns | 0.1331 ns | 0.1180 ns |         - |
| ISO/IEC 706 97-10 | 140662538042551       | 21.893 ns | 0.3501 ns | 0.3275 ns |         - |
| ISO/IEC 706 97-10 | 140662538042551028    | 25.735 ns | 0.2219 ns | 0.2076 ns |         - |
| ISO/IEC 706 97-10 | 140662538042551028265 | 28.758 ns | 0.1250 ns | 0.0976 ns |         - |
|
| Luhn              | 140                   |  7.013 ns | 0.1099 ns | 0.1028 ns |         - |
| Luhn              | 140662                | 10.537 ns | 0.1623 ns | 0.1518 ns |         - |
| Luhn              | 140662538             | 13.909 ns | 0.1060 ns | 0.0991 ns |         - |
| Luhn              | 140662538042          | 17.530 ns | 0.1428 ns | 0.1266 ns |         - |
| Luhn              | 140662538042551       | 21.001 ns | 0.2169 ns | 0.2029 ns |         - |
| Luhn              | 140662538042551028    | 24.310 ns | 0.2837 ns | 0.2654 ns |         - |
| Luhn              | 140662538042551028265 | 27.940 ns | 0.2464 ns | 0.2184 ns |         - |
|
| Modulus10_13      | 140                   |  6.798 ns | 0.1453 ns | 0.1359 ns |         - |
| Modulus10_13      | 140662                | 10.110 ns | 0.2074 ns | 0.1940 ns |         - |
| Modulus10_13      | 140662538             | 12.569 ns | 0.1022 ns | 0.0853 ns |         - |
| Modulus10_13      | 140662538042          | 16.103 ns | 0.1472 ns | 0.1229 ns |         - |
| Modulus10_13      | 140662538042551       | 18.845 ns | 0.2321 ns | 0.2171 ns |         - |
| Modulus10_13      | 140662538042551028    | 22.300 ns | 0.1369 ns | 0.1214 ns |         - |
| Modulus10_13      | 140662538042551028265 | 25.200 ns | 0.2025 ns | 0.1795 ns |         - |
|
| Modulus10_1       | 140                   |  4.139 ns | 0.0645 ns | 0.0603 ns |         - |
| Modulus10_1       | 140662                |  5.800 ns | 0.0984 ns | 0.0920 ns |         - |
| Modulus10_1       | 140662538             |  7.435 ns | 0.0742 ns | 0.0620 ns |         - |
|
| Modulus10_2       | 140                   |  3.952 ns | 0.0486 ns | 0.0431 ns |         - |
| Modulus10_2       | 140662                |  5.621 ns | 0.1259 ns | 0.1178 ns |         - |
| Modulus10_2       | 140662538             |  7.305 ns | 0.0875 ns | 0.0776 ns |         - |
|
| Modulus11         | 140                   |  4.415 ns | 0.0479 ns | 0.0400 ns |         - |
| Modulus11         | 140662                |  6.528 ns | 0.1075 ns | 0.1005 ns |         - |
| Modulus11         | 140662538             |  7.811 ns | 0.1584 ns | 0.1945 ns |         - |
|
| Verhoeff          | 140                   | 10.665 ns | 0.1498 ns | 0.1402 ns |         - |
| Verhoeff          | 140662                | 18.160 ns | 0.0953 ns | 0.0796 ns |         - |
| Verhoeff          | 140662538             | 25.813 ns | 0.1021 ns | 0.0853 ns |         - |
| Verhoeff          | 140662538042          | 33.391 ns | 0.3761 ns | 0.2936 ns |         - |
| Verhoeff          | 140662538042551       | 40.823 ns | 0.2580 ns | 0.2413 ns |         - |
| Verhoeff          | 140662538042551028    | 48.616 ns | 0.4191 ns | 0.3921 ns |         - |
| Verhoeff          | 140662538042551028265 | 56.447 ns | 0.5107 ns | 0.4777 ns |         - |

#### General Alphabetic Algorithms

| Algorithm Name          | Value                 | Mean      | Error     | StdDev    | Allocated |
|------------------------ |---------------------- |----------:|----------:|----------:|----------:|
| ISO/IEC 7064 MOD 27,26  | EGR                   |  7.323 ns | 0.0744 ns | 0.0660 ns |         - |
| ISO/IEC 7064 MOD 27,26  | EGRNML                | 10.075 ns | 0.0852 ns | 0.0711 ns |         - |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOC             | 13.165 ns | 0.1768 ns | 0.1567 ns |         - |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOCECU          | 16.666 ns | 0.1215 ns | 0.1137 ns |         - |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOCECUJIK       | 20.371 ns | 0.1702 ns | 0.1508 ns |         - |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOCECUJIKNWW    | 22.317 ns | 0.1799 ns | 0.1682 ns |         - |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOCECUJIKNWWVVO | 25.042 ns | 0.1730 ns | 0.1533 ns |         - |
|
| ISO/IEC 7064 MOD 661-26 | EGR                   |  6.285 ns | 0.1391 ns | 0.1161 ns |         - |
| ISO/IEC 7064 MOD 661-26 | EGRNML                |  9.172 ns | 0.1611 ns | 0.1507 ns |         - |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOC             | 11.706 ns | 0.0904 ns | 0.0755 ns |         - |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOCECU          | 16.000 ns | 0.1052 ns | 0.0932 ns |         - |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOCECUJIK       | 20.669 ns | 0.2951 ns | 0.2616 ns |         - |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOCECUJIKNWW    | 23.121 ns | 0.2003 ns | 0.1874 ns |         - |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOCECUJIKNWWVVO | 27.655 ns | 0.0923 ns | 0.0771 ns |         - |

#### General Alphanumeric Algorithms

| Algorithm Name           | Value                 | Mean      | Error     | StdDev    | Allocated |
|------------------------- |---------------------- |----------:|----------:|----------:|----------:|
| AlphanumericMod97_10     | K1M                   | 11.002 ns | 0.1365 ns | 0.1277 ns |         - |
| AlphanumericMod97_10     | K1MEL3                | 17.797 ns | 0.2443 ns | 0.2165 ns |         - |
| AlphanumericMod97_10     | K1MEL3765             | 22.489 ns | 0.1779 ns | 0.1485 ns |         - |
| AlphanumericMod97_10     | K1MEL37655H2          | 28.817 ns | 0.4608 ns | 0.4310 ns |         - |
| AlphanumericMod97_10     | K1MEL37655H24ED       | 34.900 ns | 0.3153 ns | 0.2633 ns |         - |
| AlphanumericMod97_10     | K1MEL37655H24EDKCA    | 43.881 ns | 0.6774 ns | 0.6337 ns |         - |
| AlphanumericMod97_10     | K1MEL37655H24EDKCA69I | 51.921 ns | 1.0400 ns | 1.3523 ns |         - |
|
| ISO/IEC 7064 MOD 1271-36 | K1M                   |  8.863 ns | 0.1743 ns | 0.1630 ns |         - |
| ISO/IEC 7064 MOD 1271-36 | K1MEL3                | 12.580 ns | 0.0931 ns | 0.0871 ns |         - |
| ISO/IEC 7064 MOD 1271-36 | K1MEL3765             | 16.587 ns | 0.2760 ns | 0.2446 ns |         - |
| ISO/IEC 7064 MOD 1271-36 | K1MEL37655H2          | 20.065 ns | 0.2071 ns | 0.1836 ns |         - |
| ISO/IEC 7064 MOD 1271-36 | K1MEL37655H24ED       | 24.160 ns | 0.3622 ns | 0.3388 ns |         - |
| ISO/IEC 7064 MOD 1271-36 | K1MEL37655H24EDKCA    | 27.477 ns | 0.1746 ns | 0.1547 ns |         - |
| ISO/IEC 7064 MOD 1271-36 | K1MEL37655H24EDKCA69I | 32.238 ns | 0.5370 ns | 0.5023 ns |         - |
|
| ISO/IEC 7064 MOD 37-2    | K1M                   |  7.182 ns | 0.0572 ns | 0.0507 ns |         - |
| ISO/IEC 7064 MOD 37-2    | K1MEL3                | 11.216 ns | 0.0702 ns | 0.0622 ns |         - |
| ISO/IEC 7064 MOD 37-2    | K1MEL3765             | 14.887 ns | 0.1013 ns | 0.0948 ns |         - |
| ISO/IEC 7064 MOD 37-2    | K1MEL37655H2          | 19.123 ns | 0.1784 ns | 0.1582 ns |         - |
| ISO/IEC 7064 MOD 37-2    | K1MEL37655H24ED       | 23.105 ns | 0.1653 ns | 0.1465 ns |         - |
| ISO/IEC 7064 MOD 37-2    | K1MEL37655H24EDKCA    | 27.188 ns | 0.1583 ns | 0.1481 ns |         - |
| ISO/IEC 7064 MOD 37-2    | K1MEL37655H24EDKCA69I | 30.627 ns | 0.1662 ns | 0.1555 ns |         - |
|
| ISO/IEC 7064 MOD 37,36   | K1M                   |  8.321 ns | 0.0571 ns | 0.0534 ns |         - |
| ISO/IEC 7064 MOD 37,36   | K1MEL3                | 13.299 ns | 0.0963 ns | 0.0854 ns |         - |
| ISO/IEC 7064 MOD 37,36   | K1MEL3765             | 17.224 ns | 0.1407 ns | 0.1247 ns |         - |
| ISO/IEC 7064 MOD 37,36   | K1MEL37655H2          | 21.530 ns | 0.1734 ns | 0.1622 ns |         - |
| ISO/IEC 7064 MOD 37,36   | K1MEL37655H24ED       | 26.526 ns | 0.1751 ns | 0.1638 ns |         - |
| ISO/IEC 7064 MOD 37,36   | K1MEL37655H24EDKCA    | 32.078 ns | 0.1808 ns | 0.1691 ns |         - |
| ISO/IEC 7064 MOD 37,36   | K1MEL37655H24EDKCA69I | 36.350 ns | 0.3844 ns | 0.3596 ns |         - |

#### Value Specific Algorithms

Note: ABA RTN, NHS and NPI algorithms do not support calculation of check digits, 
only validation of values containing check digits.

| Algorithm Name | Value                           | Mean     | Error    | StdDev   | Allocated |
|--------------- |-------------------------------- |---------:|---------:|---------:|----------:|
| IBAN           | BE00096123456769                | 35.28 ns | 0.211 ns | 0.197 ns |         - |
| IBAN           | GB00WEST12345698765432          | 52.85 ns | 0.645 ns | 0.571 ns |         - |
| IBAN           | SC00MCBL01031234567890123456USD | 74.39 ns | 0.522 ns | 0.463 ns |         - |
|
| ISIN           | AU0000XVGZA                     | 29.73 ns | 0.588 ns | 0.550 ns |         - |
| ISIN           | GB000263494                     | 23.10 ns | 0.253 ns | 0.237 ns |         - |
| ISIN           | US037833100                     | 23.02 ns | 0.264 ns | 0.247 ns |         - |
|                                                  
| VIN            | 1G8ZG127_WZ157259               | 41.17 ns | 0.607 ns | 0.568 ns |         - |
| VIN            | 1HGEM212_2L047875               | 40.46 ns | 0.332 ns | 0.277 ns |         - |
| VIN            | 1M8GDM9A_KP042788               | 41.28 ns | 0.769 ns | 0.719 ns |         - |

### Validate Method

#### General Numeric Algorithms

All algorithms use a single check digit except ISO/IEC 7064 MOD 97-10 which uses
two check digits.

Note that the Modulus10_1, Modulus10_2 and Modulus11 algorithms have a maximum 
length of 10 (including the check digit) for values being validated so their
benchmarks do not cover lengths greater than 10.

| Algorithm Name         | Value                   | Mean      | Error     | StdDev    | Allocated |
|----------------------- | ----------------------- |----------:|----------:|----------:|----------:|
| Damm                   | 1402                    |  6.130 ns | 0.1318 ns | 0.1233 ns |         - |
| Damm                   | 1406622                 | 10.398 ns | 0.1068 ns | 0.0999 ns |         - |
| Damm                   | 1406625388              | 15.985 ns | 0.2725 ns | 0.2549 ns |         - |
| Damm                   | 1406625380422           | 21.212 ns | 0.2268 ns | 0.2122 ns |         - |
| Damm                   | 1406625380425518        | 26.353 ns | 0.1993 ns | 0.1864 ns |         - |
| Damm                   | 1406625380425510280     | 31.554 ns | 0.3449 ns | 0.3226 ns |         - |
| Damm                   | 1406625380425510282654  | 37.529 ns | 0.2162 ns | 0.1917 ns |         - |
|
| ISO/IEC 7064 MOD 11,10 | 1409                    |  7.648 ns | 0.1277 ns | 0.1195 ns |         - |
| ISO/IEC 7064 MOD 11,10 | 1406623                 | 11.939 ns | 0.1648 ns | 0.1541 ns |         - |
| ISO/IEC 7064 MOD 11,10 | 1406625381              | 16.038 ns | 0.1963 ns | 0.1836 ns |         - |
| ISO/IEC 7064 MOD 11,10 | 1406625380426           | 20.212 ns | 0.2354 ns | 0.2202 ns |         - |
| ISO/IEC 7064 MOD 11,10 | 1406625380425514        | 24.405 ns | 0.2345 ns | 0.2194 ns |         - |
| ISO/IEC 7064 MOD 11,10 | 1406625380425510286     | 28.210 ns | 0.2072 ns | 0.1730 ns |         - |
| ISO/IEC 7064 MOD 11,10 | 1406625380425510282657  | 32.046 ns | 0.1599 ns | 0.1248 ns |         - |
|
| ISO/IEC 7064 MOD 11-2  | 140X                    |  5.999 ns | 0.0537 ns | 0.0476 ns |         - |
| ISO/IEC 7064 MOD 11-2  | 1406628                 |  9.602 ns | 0.1235 ns | 0.1156 ns |         - |
| ISO/IEC 7064 MOD 11-2  | 1406625380              | 13.766 ns | 0.2956 ns | 0.3163 ns |         - |
| ISO/IEC 7064 MOD 11-2  | 1406625380426           | 16.767 ns | 0.3326 ns | 0.2949 ns |         - |
| ISO/IEC 7064 MOD 11-2  | 1406625380425511        | 20.032 ns | 0.2177 ns | 0.2037 ns |         - |
| ISO/IEC 7064 MOD 11-2  | 140662538042551028X     | 24.437 ns | 0.1978 ns | 0.1850 ns |         - |
| ISO/IEC 7064 MOD 11-2  | 1406625380425510282651  | 27.452 ns | 0.2728 ns | 0.2552 ns |         - |
|
| ISO/IEC 7064 MOD 97-10 | 14066                   |  7.052 ns | 0.0363 ns | 0.0303 ns |         - |
| ISO/IEC 7064 MOD 97-10 | 14066262                | 10.809 ns | 0.1203 ns | 0.1125 ns |         - |
| ISO/IEC 7064 MOD 97-10 | 14066253823             | 15.062 ns | 0.2334 ns | 0.2184 ns |         - |
| ISO/IEC 7064 MOD 97-10 | 14066253804250          | 18.767 ns | 0.1066 ns | 0.0945 ns |         - |
| ISO/IEC 7064 MOD 97-10 | 14066253804255112       | 22.839 ns | 0.2047 ns | 0.1815 ns |         - |
| ISO/IEC 7064 MOD 97-10 | 14066253804255102853    | 25.885 ns | 0.1921 ns | 0.1703 ns |         - |
| ISO/IEC 7064 MOD 97-10 | 14066253804255102826587 | 29.362 ns | 0.3131 ns | 0.2928 ns |         - |
| 
| Luhn                   | 1404                    |  7.285 ns | 0.1008 ns | 0.0943 ns |         - |
| Luhn                   | 1406628                 | 11.476 ns | 0.2376 ns | 0.2106 ns |         - |
| Luhn                   | 1406625382              | 14.897 ns | 0.1170 ns | 0.1037 ns |         - |
| Luhn                   | 1406625380421           | 19.188 ns | 0.2519 ns | 0.2356 ns |         - |
| Luhn                   | 1406625380425514        | 22.925 ns | 0.2653 ns | 0.2482 ns |         - |
| Luhn                   | 1406625380425510285     | 27.039 ns | 0.3149 ns | 0.2945 ns |         - |
| Luhn                   | 1406625380425510282651  | 30.417 ns | 0.3339 ns | 0.3123 ns |         - |
|
| Modulus10_13           | 1403                    |  7.743 ns | 0.0618 ns | 0.0548 ns |         - |
| Modulus10_13           | 1406627                 | 11.071 ns | 0.1356 ns | 0.1269 ns |         - |
| Modulus10_13           | 1406625385              | 14.656 ns | 0.1905 ns | 0.1689 ns |         - |
| Modulus10_13           | 1406625380425           | 19.068 ns | 0.2373 ns | 0.2103 ns |         - |
| Modulus10_13           | 1406625380425518        | 22.867 ns | 0.4330 ns | 0.3839 ns |         - |
| Modulus10_13           | 1406625380425510288     | 26.763 ns | 0.3102 ns | 0.2901 ns |         - |
| Modulus10_13           | 1406625380425510282657  | 29.478 ns | 0.2864 ns | 0.2391 ns |         - |
|
| Modulus10_1            | 1401                    |  4.642 ns | 0.1087 ns | 0.1017 ns |         - |
| Modulus10_1            | 1406628                 |  6.595 ns | 0.1182 ns | 0.1048 ns |         - |
| Modulus10_1            | 1406625384              |  8.193 ns | 0.0861 ns | 0.0763 ns |         - |
|
| Modulus10_2            | 1406                    |  5.222 ns | 0.0586 ns | 0.0489 ns |         - |
| Modulus10_2            | 1406627                 |  7.420 ns | 0.1605 ns | 0.1340 ns |         - |
| Modulus10_2            | 1406625389              |  9.537 ns | 0.1169 ns | 0.1093 ns |         - |
|
| Modulus11              | 1406                    |  6.240 ns | 0.0799 ns | 0.0709 ns |         - |
| Modulus11              | 1406625                 |  8.356 ns | 0.0805 ns | 0.0753 ns |         - |
| Modulus11              | 1406625388              |  9.767 ns | 0.1164 ns | 0.1089 ns |         - |
|
| Verhoeff               | 1401                    | 13.822 ns | 0.0920 ns | 0.0815 ns |         - |
| Verhoeff               | 1406625                 | 22.863 ns | 0.1531 ns | 0.1432 ns |         - |
| Verhoeff               | 1406625388              | 32.046 ns | 0.3094 ns | 0.2584 ns |         - |
| Verhoeff               | 1406625380426           | 41.280 ns | 0.4174 ns | 0.3700 ns |         - |
| Verhoeff               | 1406625380425512        | 50.391 ns | 0.5977 ns | 0.5591 ns |         - |
| Verhoeff               | 1406625380425510285     | 58.905 ns | 0.8780 ns | 0.7332 ns |         - |
| Verhoeff               | 1406625380425510282655  | 67.668 ns | 0.7107 ns | 0.6648 ns |         - |

#### General Alphabetic Algorithms

ISO/IEC 7064 MOD 27,26 uses a single check character. ISO/IEC 7064 MOD 661-26
uses two check characters.

| Algorithm Name          | Value                   | Mean      | Error     | StdDev    | Allocated |
|------------------------ |------------------------ |----------:|----------:|----------:|----------:|
| ISO/IEC 7064 MOD 27,26  | EGRS                    |  7.528 ns | 0.1329 ns | 0.1243 ns |         - |
| ISO/IEC 7064 MOD 27,26  | EGRNMLU                 | 11.776 ns | 0.1623 ns | 0.1439 ns |         - |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOCB              | 16.419 ns | 0.2130 ns | 0.1779 ns |         - |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOCECUA           | 20.097 ns | 0.1830 ns | 0.1712 ns |         - |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOCECUJIKA        | 24.309 ns | 0.2585 ns | 0.2291 ns |         - |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOCECUJIKNWWY     | 28.131 ns | 0.2375 ns | 0.2106 ns |         - |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOCECUJIKNWWVVOQ  | 32.378 ns | 0.2915 ns | 0.2727 ns |         - |
|
| ISO/IEC 7064 MOD 661-26 | EGRSE                   |  7.655 ns | 0.0919 ns | 0.0860 ns |         - |
| ISO/IEC 7064 MOD 661-26 | EGRNMLDR                | 11.261 ns | 0.1051 ns | 0.0983 ns |         - |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOCCK             | 15.377 ns | 0.2277 ns | 0.2018 ns |         - |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOCECUZJ          | 18.679 ns | 0.2463 ns | 0.2304 ns |         - |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOCECUJIKFQ       | 22.296 ns | 0.1711 ns | 0.1429 ns |         - |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOCECUJIKNWWQN    | 26.118 ns | 0.2422 ns | 0.2147 ns |         - |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOCECUJIKNWWVVORC | 29.567 ns | 0.6012 ns | 0.6433 ns |         - |

#### General Alphanumeric Algorithms

ISO/IEC 7064 MOD 1271-36 uses two check characters. ISO/IEC 7064 MOD 37-2 and 
ISO/IEC 7064 MOD 37,36 use a single check character.

| Algorithm Name           | Value                   | Mean      | Error     | StdDev    | Allocated |
|------------------------- |-------------------------|----------:|----------:|----------:|----------:|
| AlphanumericMod97_10     | K1M66                   | 12.387 ns | 0.1193 ns | 0.1058 ns |         - |
| AlphanumericMod97_10     | K1MEL372                | 20.063 ns | 0.2217 ns | 0.2074 ns |         - |
| AlphanumericMod97_10     | K1MEL376530             | 24.766 ns | 0.3120 ns | 0.2919 ns |         - |
| AlphanumericMod97_10     | K1MEL37655H272          | 31.257 ns | 0.2680 ns | 0.2376 ns |         - |
| AlphanumericMod97_10     | K1MEL37655H24ED07       | 38.967 ns | 0.6239 ns | 0.5836 ns |         - |
| AlphanumericMod97_10     | K1MEL37655H24EDKCA67    | 47.562 ns | 0.3638 ns | 0.3403 ns |         - |
| AlphanumericMod97_10     | K1MEL37655H24EDKCA69I17 | 54.507 ns | 0.4677 ns | 0.4375 ns |         - |
|
| ISO/IEC 7064 MOD 1271-36 | K1M0W                   |  8.954 ns | 0.0646 ns | 0.0539 ns |         - |
| ISO/IEC 7064 MOD 1271-36 | K1MEL34W                | 14.014 ns | 0.1141 ns | 0.1011 ns |         - |
| ISO/IEC 7064 MOD 1271-36 | K1MEL37654L             | 17.518 ns | 0.1200 ns | 0.1122 ns |         - |
| ISO/IEC 7064 MOD 1271-36 | K1MEL37655H2KZ          | 22.687 ns | 0.1336 ns | 0.1184 ns |         - |
| ISO/IEC 7064 MOD 1271-36 | K1MEL37655H24EDRD       | 26.384 ns | 0.2536 ns | 0.2372 ns |         - |
| ISO/IEC 7064 MOD 1271-36 | K1MEL37655H24EDKCA8P    | 30.657 ns | 0.3042 ns | 0.2697 ns |         - |
| ISO/IEC 7064 MOD 1271-36 | K1MEL37655H24EDKCA69I8W | 35.379 ns | 0.4214 ns | 0.3942 ns |         - |
|
| ISO/IEC 7064 MOD 37-2    | K1MF                    |  7.115 ns | 0.0568 ns | 0.0531 ns |         - |
| ISO/IEC 7064 MOD 37-2    | K1MEL3M                 | 11.459 ns | 0.1226 ns | 0.1147 ns |         - |
| ISO/IEC 7064 MOD 37-2    | K1MEL37655              | 14.364 ns | 0.1356 ns | 0.1269 ns |         - |
| ISO/IEC 7064 MOD 37-2    | K1MEL37655H2W           | 18.817 ns | 0.1800 ns | 0.1683 ns |         - |
| ISO/IEC 7064 MOD 37-2    | K1MEL37655H24EDO        | 23.357 ns | 0.2114 ns | 0.1978 ns |         - |
| ISO/IEC 7064 MOD 37-2    | K1MEL37655H24EDKCAV     | 28.158 ns | 0.2954 ns | 0.2763 ns |         - |
| ISO/IEC 7064 MOD 37-2    | K1MEL37655H24EDKCA69IA  | 31.930 ns | 0.3158 ns | 0.2799 ns |         - |
|
| ISO/IEC 7064 MOD 37,36   | K1ME                    |  8.637 ns | 0.0738 ns | 0.0690 ns |         - |
| ISO/IEC 7064 MOD 37,36   | K1MEL3D                 | 13.006 ns | 0.0574 ns | 0.0508 ns |         - |
| ISO/IEC 7064 MOD 37,36   | K1MEL3765E              | 16.348 ns | 0.1423 ns | 0.1331 ns |         - |
| ISO/IEC 7064 MOD 37,36   | K1MEL37655H2Z           | 20.454 ns | 0.2239 ns | 0.2094 ns |         - |
| ISO/IEC 7064 MOD 37,36   | K1MEL37655H24EDI        | 24.746 ns | 0.1230 ns | 0.1091 ns |         - |
| ISO/IEC 7064 MOD 37,36   | K1MEL37655H24EDKCAH     | 29.077 ns | 0.1466 ns | 0.1371 ns |         - |
| ISO/IEC 7064 MOD 37,36   | K1MEL37655H24EDKCA69IG  | 33.149 ns | 0.2918 ns | 0.2730 ns |         - |

#### Value Specific Algorithms

| Algorithm Name | Value                           | Mean      | Error     | StdDev    | Allocated |
|--------------- |-------------------------------- |----------:|----------:|----------:|----------:|
| ABA RTN        | 111000025                       |  8.862 ns | 0.1623 ns | 0.1518 ns |         - |
| ABA RTN        | 122235821                       |  8.692 ns | 0.1737 ns | 0.1624 ns |         - |
| ABA RTN        | 325081403                       |  8.684 ns | 0.1237 ns | 0.1157 ns |         - |
|
| IBAN           | BE71096123456769                | 22.310 ns | 0.2240 ns | 0.1980 ns |         - |
| IBAN           | GB82WEST12345698765432          | 34.930 ns | 0.3060 ns | 0.2870 ns |         - |
| IBAN           | SC74MCBL01031234567890123456USD | 51.930 ns | 0.8720 ns | 0.7730 ns |         - |
|
| ISIN           | AU0000XVGZA3                    | 25.624 ns | 0.2618 ns | 0.2449 ns |         - |
| ISIN           | GB0002634946                    | 21.148 ns | 0.2497 ns | 0.2335 ns |         - |
| ISIN           | US0378331005                    | 21.139 ns | 0.3062 ns | 0.2865 ns |         - |
|                                                  
| NHS            | 4505577104                      | 11.933 ns | 0.1477 ns | 0.1309 ns |         - |
| NHS            | 5301194917                      | 11.898 ns | 0.1416 ns | 0.1324 ns |         - |
| NHS            | 9434765919                      | 11.917 ns | 0.1627 ns | 0.1522 ns |         - |
|                                                  
| NPI            | 1122337797                      | 15.106 ns | 0.2468 ns | 0.2309 ns |         - |
| NPI            | 1234567893                      | 14.986 ns | 0.0968 ns | 0.0808 ns |         - |
| NPI            | 1245319599                      | 15.067 ns | 0.2008 ns | 0.1878 ns |         - |
|                                                  
| VIN            | 1G8ZG127XWZ157259               | 40.107 ns | 0.3094 ns | 0.2743 ns |         - |
| VIN            | 1HGEM21292L047875               | 40.206 ns | 0.2919 ns | 0.2438 ns |         - |
| VIN            | 1M8GDM9AXKP042788               | 40.266 ns | 0.5329 ns | 0.4985 ns |         - |

# Release History/Release Notes

## v1.0.0-alpha

Initial limited release. Included algorithms:
* ABA RTN (Routing Transit Number) Algorithm
* Damm Algorithm
* ISIN (International Securities Identification Number) Algorithm
* Luhn Algorithm
* Modulus10_1 Algorithm
* Modulus10_2 Algorithm
* Modulus10_13 Algorithm (UPC/EAN/ISBN-13/etc.)
* Modulus11 Algorithm (ISBN-10/ISSN/etc.)
* NHS (UK National Health Service) Algorithm
* NPI (US National Provider Identifier) Algorithm
* Verhoeff Algorithm
* VIN (Vehicle Identification Number) Algorithm

## v1.0.0

Initial release. Additional included algorithms
* ISO/IEC 7064 MOD 11,10
* ISO/IEC 7064 MOD 11-2
* ISO/IEC 7064 MOD 1271-36
* ISO/IEC 7064 MOD 27,26
* ISO/IEC 7064 MOD 37-2
* ISO/IEC 7064 MOD 37,36
* ISO/IEC 7064 MOD 661-26
* ISO/IEC 7064 MOD 97-10

## v1.1.0
Additional included algorithms
* AlphanumericMod97_10Algorithm
* IbanAlgorithm
 
Performance increases for:
* ISO/IEC 7064 MOD 1271-36, Validate method ~18% improvement
* ISO/IEC 7064 MOD 37-2, Validate method ~17% improvement, TryCalculateCheckDigit method ~20% improvement
* ISO/IEC 7064 MOD 37-36, ValidateMethod ~18% improvement, TryCalculateCheckDigit method ~21% improvement




