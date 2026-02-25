# CheckDigits.Net

CheckDigits.Net brings together in one library an extensive collection of different
check digit algorithms. CheckDigits.Net has the goal that each algorithm supported
be optimized, be resilient to malformed input and that memory allocations be 
minimized or eliminated completely. Benchmarks for each algorithm are provided to
demonstrate performance over a range of values and the memory allocation (if any).

Benchmarks have shown that the optimized versions of the algorithms in CheckDigits.Net 
are up to 10X-50X faster than those in popular Nuget packages.

### Future Algorithms
Is there an algorithm that you would like to see included in CheckDigits.Net? Use
the "Contact owners" link on https://www.nuget.org/packages/CheckDigits.Net and 
let us know. Or contribute to the CheckDigits.Net repository: https://github.com/KnowledgeForwardSolutions/CheckDigits.Net

## Table of Contents

- **[Check Digit Overview](#check-digit-overview)**
- **[ISO/IEC 7064 Algorithms](#isoiec-7064-algorithms)**
- **[Supported Algorithms](#supported-algorithms)**
- **[Value/Identifier Types and Associated Algorithms](#valueidentifier-types-and-associated-algorithms)**
- **[Using CheckDigits.Net](#using-checkdigits.net)**
- **[Interfaces](#interfaces)**
- **[Algorithm Descriptions](#algorithm-descriptions)**
    * [ABA RTN (Routing Transit Number) Algorithm](#aba-rtn-algorithm)
    * [Alphanumeric MOD 97-10 Algorithm](#alphanumeric-mod-97-10-algorithm)
    * [CUSIP Algorithm](#cusip-algorithm)
    * [Damm Algorithm](#damm-algorithm)
    * [FIGI (Financial Instrument Global Identifier) Algorithm](#figi-algorithm)
    * [IBAN (International Bank Account Number) Algorithm](#iban-algorithm)
    * [ICAO 9303 Algorithm](#icao-9303-algorithm)
    * [ICAO 9303 Document Size TD1 Algorithm](#icao-9303-document-size-td1-algorithm)
    * [ICAO 9303 Document Size TD2 Algorithm](#icao-9303-document-size-td2-algorithm)
    * [ICAO 9303 Document Size TD3 Algorithm](#icao-9303-document-size-td3-algorithm)
    * [ICAO 9303 Machine Readable Visa Algorithm](#icao-9303-machine-readable-visa-algorithm)
    * [ISAN (International Standard Audiovisual Number) Algorithm](#isan-algorithm)
    * [ISIN (International Securities Identification Number) Algorithm](#isin-algorithm)
    * [ISO 6346 Algorithm](#iso-6346-algorithm)
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
    * [Modulus11_27Decimal Algorithm](#modulus11_27decimal-algorithm)
    * [Modulus11_27Extended Algorithm](#modulus11_27extended-algorithm)
    * [Modulus11Decimal Algorithm (NHS Number/etc.)](#modulus11decimal-algorithm)
    * [Modulus11Extended Algorithm (ISBN-10/ISSN/etc.)](#modulus11extended-algorithm)
    * [NHS (UK National Health Service) Algorithm](#nhs-algorithm)
    * [NOID Check Digit Algorithm](#noid-check-digit-algorithm)
    * [NPI (US National Provider Identifier) Algorithm](#npi-algorithm)
    * [SEDOL Algorithm](#sedol-algorithm)
    * [Verhoeff Algorithm](#verhoeff-algorithm)
    * [VIN (Vehicle Identification Number) Algorithm](#vin-algorithm)
- **[Benchmarks](#benchmarks)**
- **[Release History/Release Notes](#release-historyrelease-notes)**
    - [v1.0.0-alpha](#v1.0.0alpha)
    - [v1.0.0](#v1.0.0)
    - [v1.1.0](#v1.1.0)
    - [v2.0.0](#v2.0.0)
    - [v2.1.0](#v2.1.0)
    - [v2.2.0](#v2.2.0)
    - [v2.3.0](#v2.3.0)
    - [v2.3.1](#v2.3.1)
    - [v3.0.0](#v3.0.0)

## Check Digit Overview

Check digits are a useful tool for detecting data transcription errors. By embedding
a check digit in a piece of information it is possible to detect common data entry
errors early, often before performing more extensive and time consuming processing.
A very common example of early error detection is validating a credit card number's 
check digit in the UI layer while the user is entering the credit card number before 
attempting to authorize a transaction with the card issuer.

Typical errors that can be detected by check digit algorithms include:

* Single digit transcription errors (any single digit in a value being entered incorrectly).
* Two digit transposition errors (two adjacent digits being swapped, i.e. *ab -> ba*).
* Twin errors (two identical digits being replaced by another pair, i.e. *aa -> bb*).
* Two digit jump transpositions (two digits separated by one position being swapped, i.e. *abc -> cba*).
* Jump twin errors (two identical digits separated by one position being replaced by another pair, i.e. *aba -> cbc*).

Check digit algorithms attempt to balance detection capabilities with the cost in 
execution time and/or the complexity to implement. While check digits are designed
to catch common errors early, they are not foolproof. Statistically, check digit 
algorithms are subject to "False Positive" results where the check digit appears 
to be valid, but the value contains a transcription error that the algorithm is not 
capable of detecting (for example, the Luhn algorithm fails to detect two digit 
transposition errors with the digits *9* and *0*, i.e. *90 -> 09* or vice versa).
Conversely, check digit algorithms are not subject to "False Negative" results. 
If a value contains an incorrect check digit, then you can be certain that the 
value was not transcribed correctly.
 
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

* `Iso7064PureSystemSingleCharacterAlgorithm` (generates a single check character, including a supplementary character)
* `Iso7064PureSystemDoubleCharacterAlgorithm` (generates two check characters)
* `Iso7064HybridSystemAlgorithm` (generates a single check character)

Refer to [Using CheckDigits.Net](#using-checkdigits.net) for more information
about using these classes.

The ISO/IEC 7064:2003 standard is available at https://www.iso.org/standard/31531.html

## Supported Algorithms

* [ABA RTN (Routing Transit Number) Algorithm](#aba-rtn-algorithm)
* [Alphanumeric MOD 97-10 Algorithm](#alphanumeric-mod-97-10-algorithm)
* [CUSIP Algorithm](#cusip-algorithm)
* [Damm Algorithm](#damm-algorithm)
* [FIGI (Financial Instrument Global Identifier) Algorithm](#figi-algorithm)
* [IBAN (International Bank Account Number) Algorithm](#iban-algorithm)
* [ICAO 9303 Algorithm](#icao-9303-algorithm)
* [ICAO 9303 Document Size TD1 Algorithm](#icao-9303-document-size-td1-algorithm)
* [ICAO 9303 Document Size TD2 Algorithm](#icao-9303-document-size-td2-algorithm)
* [ICAO 9303 Document Size TD3 Algorithm](#icao-9303-document-size-td3-algorithm)
* [ICAO 9303 Machine Readable Visa Algorithm](#icao-9303-machine-readable-visa-algorithm)
* [ISAN (International Standard Audiovisual Number) Algorithm](#isan-algorithm)
* [ISIN (International Securities Identification Number) Algorithm](#isin-algorithm)
* [ISO 6346 Algorithm](#iso-6346-algorithm)
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
* [Modulus11_27Decimal Algorithm](#modulus11_27decimal-algorithm)
* [Modulus11_27Extended Algorithm](#modulus11_27extended-algorithm)
* [Modulus11Decimal Algorithm (NHS Number/etc.)](#modulus11decimal-algorithm)
* [Modulus11Extended Algorithm (ISBN-10/ISSN/etc.)](#modulus11extended-algorithm)
* [NHS (UK National Health Service) Algorithm](#nhs-algorithm)
* [NOID Check Digit Algorithm](#noid-check-digit-algorithm)
* [NPI (US National Provider Identifier) Algorithm](#npi-algorithm)
* [SEDOL Algorithm](#sedol-algorithm)
* [Verhoeff Algorithm](#verhoeff-algorithm)
* [VIN (Vehicle Identification Number) Algorithm](#vin-algorithm)

## Value/Identifier Types and Associated Algorithms

| Value/Identifier Type                                  | Algorithm                                         |
| ------------------------------------------------------ | ------------------------------------------------- |
| ABA Routing Transit Number                             | [ABA RTN Algorithm](#aba-rtn-algorithm)           |
| CA Social Insurance Number                             | [Luhn Algorithm](#luhn-algorithm)                 |
| CAS Registry Number                                    | [Modulus10_1 Algorithm](#modulus10_1-algorithm)   |
| Credit card number                                     | [Luhn Algorithm](#luhn-algorithm)                 |
| CUSIP                                                  | [CUSIP Algorithm](#cusip-algorithm)               |
| EAN-8					                                 | [Modulus10_13 Algorithm](#modulus10_13-algorithm) |
| EAN-13				                                 | [Modulus10_13 Algorithm](#modulus10_13-algorithm) |
| FIGI                                                   | [FIGI Algorithm](#figi-algorithm)                 |
| Global Release Identifier                              | [ISO/IEC 7064 MOD 37-36 Algorithm](#isoiec-7064-mod-3736-algorithm) |
| GTIN-8				                                 | [Modulus10_13 Algorithm](#modulus10_13-algorithm) |
| GTIN-12				                                 | [Modulus10_13 Algorithm](#modulus10_13-algorithm) |
| GTIN-13				                                 | [Modulus10_13 Algorithm](#modulus10_13-algorithm) |
| GTIN-14				                                 | [Modulus10_13 Algorithm](#modulus10_13-algorithm) |
| IBAN                                                   | [IBAN Algorithm](#iban-algorithm) |
| ICAO Machine Readable Travel Document Field            | [ICAO 9303 Algorithm](#icao-9303-algorithm) |
| ICAO Machine Readable Travel Documents Size TD1        | [ICAO 9303 Document Size TD1 Algorithm](#icao-9303-document-size-td1-algorithm) |
| ICAO Machine Readable Travel Documents Size TD2        | [ICAO 9303 Document Size TD2 Algorithm](#icao-9303-document-size-td2-algorithm) |
| ICAO Machine Readable Passports and Size TD3 Documents | [ICAO 9303 Document Size TD3 Algorithm](#icao-9303-document-size-td3-algorithm) |
| ICAO Machine Readable Visas                            | [ICAO 9303 Machine Readable Visa Algorithm](#icao-9303-machine-readable-visa-algorithm) |
| IMEI				                                     | [Luhn Algorithm](#luhn-algorithm) |
| IMO Number                                             | [Modulus10_2 Algorithm](#modulus10_2-algorithm) |
| ISAN                                                   | [ISAN Algorithm](#isan-algorithm) |
| ISBN-10				                                 | [Modulus11Extended Algorithm](#modulus11extended-algorithm) or [Modulus11 Algorithm](#modulus11-algorithm) |
| ISBN-13				                                 | [Modulus10_13 Algorithm](#modulus10_13-algorithm) |
| ISBT Donation Identification Number                    | [ISO/IEC 7064 MOD 37-2 Algorithm](#isoiec-7064-mod-37-2-algorithm) |
| ISIN                                                   | [ISIN Algorithm](#isin-algorithm) |
| ISMN					                                 | [Modulus10_13 Algorithm](#modulus10_13-algorithm) |
| ISNI                                                   | [ISO/IEC 7064 MOD 11-2 Algorithm](#isoiec-7064-mod-11-2-algorithm) |
| ISSN   				                                 | [Modulus11Extended Algorithm](#modulus11Extended-algorithm) or [Modulus11 Algorithm](#modulus11-algorithm) |
| Legal Entity Identifier                                | [Alphanumeric MOD 97-10 Algorithm](#alphanumeric-mod-97-10-algorithm) |
| NHS Number                                             | [Modulus11Decimal Algorithm](#modulus11decimal-algorithm) or [NHS (UK National Health Service) Algorithm](#nhs-algorithm) |
| SEDOL					                                 | [SEDOL Algorithm](#sedol-algorithm) |
| Shipping Container Number                              | [ISO 6346 Algorithm](#iso-6346-algorithm) |
| SSCC					                                 | [Modulus10_13 Algorithm](#modulus10_13-algorithm) |
| Universal Loan Identifier                              | [Alphanumeric MOD 97-10 Algorithm](#alphanumeric-mod-97-10-algorithm) |
| UK National Health Service Number                      | [NHS Algorithm](#nhs-algorithm) |
| UPC-A					                                 | [Modulus10_13 Algorithm](#modulus10_13-algorithm) |
| UPC-E					                                 | [Modulus10_13 Algorithm](#modulus10_13-algorithm) |
| US National Provider Identifier                        | [NPI Algorithm](#npi-algorithm) |
| Vehicle Identification Number                          | [VIN Algorithm](#vin-algorithm) |

## Using CheckDigits.Net

Add a reference to CheckDigits.Net to your project.

Obtain an instance of the desired check digit algorithm. Either create an instance
by using `new AlgorithmXyz()` or using the static `Algorithms` class to
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

* `Iso7064PureSystemSingleCharacterAlgorithm` (generates a single check character, including a supplementary character)
* `Iso7064PureSystemDoubleCharacterAlgorithm` (generates two check characters)
* `Iso7064HybridSystemAlgorithm` (generates a single check character)

To use one of these classes you must first create an instance of a class that 
implements `IAlphabet` or `ISupplementalCharacterAlphabet`. Then you 
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
algorithm implements `ICheckDigitAlgorithm` which has properties for getting
the algorithm name and algorithm description and a Validate method that accepts 
a string and returns a boolean value that indicates if the string contains a valid
check digit. Mal-formed input such as a null value, an empty string,
a string of incorrect length or a string that contains characters that are not
valid for the algorithm will return false instead of throwing an exception.

Check digit algorithms that use a single character also implement 
`ISingleCheckDigitAlgorithm` which has a TryCalculateCheckDigit method that
accepts a string value and an out parameter which will contain the calculated 
check digit or '\0' if it was not possible to calculate the check digit.
TryCalculateCheckDigit also returns a boolean value that indicates if the check
digit was calculated or not. As with the Validate method, mal-formed input such 
as a null value, an empty string, a string of incorrect length or a string that 
contains characters that are not valid for the algorithm will return false instead 
of throwing an exception.

Check digit algorithms that use two character check digits also implement
`IDoubleCheckDigitAlgorithm`. This interface has a TryCalculateCheckDigits
method that has two output parameters, one for each check digit.

Note that `ISingleCheckDigitAlgorithm` and `IDoubleCheckDigitAlgorithm`
are not implemented for algorithms for government issued identifiers (for example,
UK NHS numbers and US NPI numbers) or values issued by a single authority (such
as ABA Routing Transit Numbers).

The `IAlphabet` and `ISupplementalCharacterAlphabet` interfaces are used 
for ISO/IEC 7064 algorithms with custom alphabets. `IAlphabet` has two 
methods: CharacterToInteger, which maps a character in the value being processed 
to its integer equivalent and IntegerToCheckCharacter which maps a calculated 
check digit to its character equivalent. `ISupplementalCharacterAlphabet` 
extends `IAlphabet` by adding the CheckCharacterToInteger method which maps 
a check character to its integer equivalent. `ISupplementalCharacterAlphabet`
is only used by `Iso7064PureSystemSingleCharacterAlgorithm`.

The `ICheckDigitMask` interface is used to define a mask that filters out 
characters from the value being checked. `ICheckDigitMask` defines 
IncludeCharacter and ExcludeCharacter methods that return true/false to indicate
if a character at a particular zero based index should be included or excluded
when calculating the check digit.

The `IMaskedCheckDigitAlgorithm` is derived from `ICheckDigitAlgorithm`
and defines an overload for the Validate method that accepts an `ICheckDigitMask` 
instance that is used to filter characters from the value being checked. Currently
the following algorithms implement `IMaskedCheckDigitAlgorithm`:
* [Luhn Algorithm](#luhn-algorithm)
* [Modulus10_13 Algorithm](#modulus10_13-algorithm)
* [Modulus11_27Decimal Algorithm](#modulus11_27decimal-algorithm)
* [Modulus11_27Extended Algorithm](#modulus11_27extended-algorithm)
* [Modulus11Decimal Algorithm](#modulus11decimal-algorithm)
* [Modulus11Extended Algorithm](#modulus11extended-algorithm)

**ICheckDigitMask Example:**
```C#

// Excludes every 5th character, allowing for spaces or dashes in credit card numbers.
public class CreditCardMask : ICheckDigitMask
{
   public Boolean ExcludeCharacter(Int32 index) => (index + 1) % 5 == 0;

   public Boolean IncludeCharacter(Int32 index) => (index + 1) % 5 != 0;
}

// Excludes the 4th and 8th characters from Canadian Social Insurance Numbers which breaks the SIN into groups of three digits.
public class CaSocialInsuranceNumberMask : ICheckDigitMask
{
   public Boolean ExcludeCharacter(Int32 index) => index == 3 || index == 7;

   public Boolean IncludeCharacter(Int32 index) => index != 3 && index != 7;
}

```

## Algorithm Descriptions

### ABA RTN Algorithm

#### Description

The American Bankers Association (ABA) Routing Transit Number (RTN) algorithm is
a modulus 10 algorithm that uses weights 3, 7 and 1. The algorithm can detect all
single digit transcription errors and most two digit transposition errors except
those where the transposed digits differ by 5 (i.e. *1 <-> 6*, *2 <-> 7*, etc.).

The ABA RTN algorithm only supports validation of check digits and does not
support calculation of check digits.

#### Details

* Valid characters - decimal digits ('0' - '9')
* Check digit size - one character
* Check digit value - decimal digit ('0' - '9')
* Check digit location - ninth digit
* Value length - 9 characters
* Class name - `AbaRtnAlgorithm`

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
* Class name - `AlphanumericMod97_10Algorithm`

#### Common Applications

* Legal Entity Identifier (LEI)
* Universal Loan Identifier (ULI)

#### Links

Wikipedia: https://en.wikipedia.org/wiki/Legal_Entity_Identifier

https://www.govinfo.gov/content/pkg/CFR-2016-title12-vol8/xml/CFR-2016-title12-vol8-part1003-appC.xml

### CUSIP Algorithm

#### Description

The CUSIP (Committee on Uniform Security Identification Procedures) algorithm is 
used for nine character alphanumeric codes that identify North American financial
securities. The algorithm has similarities with both the Luhn algorithm and the 
ISIN algorithm.

The CUSIP algorithm only supports validation of check digits and does not
support calculation of check digits.

#### Details

* Valid characters - alphanumeric characters ('0' - '9', 'A' - 'Z') plus '*', '@' and '#'
* Check digit size - one character
* Check digit value - decimal digit ('0' - '9')
* Check digit location - assumed to be the trailing (right-most) character when validating
* Class name - `CusipAlgorithm`

#### Links

Wikipedia: https://en.wikipedia.org/wiki/CUSIP

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
* Class name - `DammAlgorithm`

#### Links

Wikipedia: https://en.wikipedia.org/wiki/Damm_algorithm

### FIGI Algorithm

#### Description

The FIGI (Financial Instrument Global Identifier) algorithm is used for 12
character values issued by Bloomberg L.P. that are used to identify a variety of
financial instruments including common stock, futures, derivatives, bonds and 
more. The algorithm is a variation of the [Luhn](#luhn-algorithm) algorithm and
has the same weaknesses as the Luhn algorithm with digit characters. But like the
[ISIN](#isin-algorithm) algorithm (which also extends the Luhn algorithm to 
support alphanumeric strings), the FIGI algorithm has additional weaknesses when
detecting errors involving alphabetic characters. Each alphabetic character has
a digit character and at least one other alphabetic character that can be freely
substituted for that character and which will result in the same check digit
being calculated. This means that single character transcription errors involving
those characters can not be detected by the algorithm.

The FIGI algorithm only supports validation of check digits and does not support 
calculation of check digits.

#### Details

* Valid characters - decimal digits ('0' - '9') and upper case consonants ('BCDFGHJKLMNPQRSTVWXYZ')
* Check digit size - one character
* Check digit value - decimal digits ('0' - '9')
* Check digit location - character position 12 (1-based)
* Value length - 12
* Class name - `FigiAlgorithm`

#### Links

https://en.wikipedia.org/wiki/Financial_Instrument_Global_Identifier
https://www.openfigi.com/assets/content/figi-check-digit-2173341b2d.pdf

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
* Class name - `IbanAlgorithm`

#### Links

Wikipedia: https://en.wikipedia.org/wiki/International_Bank_Account_Number

### ICAO 9303 Algorithm

#### Description

The ICAO 9303 (International Civil Aviation Organization) algorithm is a modulus 10 
algorithm used in the field of MRTODTs (Machine Readable Official Travel Documents).
The algorithm uses weights 7, 3, and 1 with the weights applied starting from the 
left most character. 

The algorithm can not detect single character transcription errors where the difference
between the correct character and the incorrect character is 10, i.e. *0 -> A*, *B->L*, 
and vice versa. Nor can the algorithm detect two character transposition errors 
where the difference between the transposed characters is a multiple of 5, i.e. 
*27 <-> 72*, *D8 <-> 8D*, *BL <-> LB*).

#### Details

* Valid characters - decimal digits ('0' - '9'), upper case letters ('A' - 'Z') and a filler character ('<').
* Check digit size - one character
* Check digit value - decimal digit ('0' - '9')
* Check digit location - assumed to be the trailing (right-most) character when validating
* Class name - `Icao9303Algorithm`

#### Links

https://en.wikipedia.org/wiki/Machine-readable_passport#Official_travel_documents
https://www.icao.int/publications/Documents/9303_p3_cons_en.pdf

### ICAO 9303 Document Size TD1 Algorithm

The ICAO 9303 (International Civil Aviation Organization) specification for 
Machine Readable Travel Documents Size TD1 uses multiple check digits in the 
machine readable zone of the document. The first line of the machine readable 
zone contains a field for the document number (including a possible extended
document number) and associated check digit. The second line of the machine 
readable zone contains fields for date of birth, date of expiry and associated 
check digits for each field. The individual field check digits and the composite
check digit are all calculated using the [ICAO 9303 Algorithm](#icao-9303-algorithm).

The machine readable zone of a Size TD1 document consists of three lines of 30
characters. The value passed to the Validate method should contain all lines of
data concatenated together. You may optionally use line separator characters in 
the concatenated value, either the Windows line separator (a carriage return 
character followed by a line feed character - '\r\n') or the Unix line separator 
(a line feed character - '\n'). The ICAO 9303 Document Size TD1 algorithm will
determine the line separator used from the length the value passed to the Validate 
method - 90 characters for no line separators, 94 characters for Windows line 
separators and 92 characters for Unix line separators. If the length of the value 
does not match one of these lengths then the Validate method will return `false`.

The ICAO 9303 Document Size TD1 Algorithm will validate the check digits of the
three fields (document number, date of birth and date of expiry) as well as the 
composite check digit. If any of the check digits fail validation then the 
Validate method will return `false`.

The ICAO 9303 Document Size TD1 algorithm only supports validation of check 
digits and does not support calculation of check digits.

#### Details

* Valid characters - 
  * decimal digits ('0' - '9'), upper case letters ('A' - 'Z') and a filler character ('<') for document number/extended document number field
  * decimal digits ('0' - '9') and a filler character ('<') for date of birth and date of expiry fields
* Check digit size - one character
* Check digit value - decimal digit ('0' - '9')
* Check digit location - trailing (right-most) character of individual fields, trailing character of second line for composite check digit
* Value length - three lines of 30 characters plus additional line separator characters as specified by the LineSeparator property
* Class name - `Icao9303SizeTD1Algorithm`

#### Links

https://www.icao.int/publications/Documents/9303_p5_cons_en.pdf

### ICAO 9303 Document Size TD2 Algorithm

#### Description

The ICAO 9303 (International Civil Aviation Organization) specification for 
Machine Readable Travel Documents Size TD2 uses multiple check digits in the 
machine readable zone of the document. The second line of the machine readable 
zone contains fields for document number (including a possible extended document
number), date of birth and date of expiry and associated check digits for each 
field. The individual field check digits and the composite check digit are all 
calculated using the [ICAO 9303 Algorithm](#icao-9303-algorithm).

The machine readable zone of a Size TD2 document consists of two lines of 36
characters. The value passed to the Validate method should contain all lines of
data concatenated together. You may optionally use line separator characters in 
the concatenated value, either the Windows line separator (a carriage return 
character followed by a line feed character - '\r\n') or the Unix line separator 
(a line feed character - '\n'). The ICAO 9303 Document Size TD2 algorithm will
determine the line separator used from the length the value passed to the Validate 
method - 72 characters for no line separators, 74 characters for Windows line 
separators and 73 characters for Unix line separators. If the length of the value 
does not match one of these lengths then the Validate method will return `false`.

The ICAO 9303 Document Size TD2 Algorithm will validate the check digits of the
three fields (document number, date of birth and date of expiry) as well as the 
composite check digit. If any of the check digits fail validation then the 
Validate method will return `false`.

The ICAO 9303 Document Size TD2 algorithm only supports validation of check 
digits and does not support calculation of check digits.

#### Details

* Valid characters - 
  * decimal digits ('0' - '9'), upper case letters ('A' - 'Z') and a filler character ('<') for document number/extended document number field
  * decimal digits ('0' - '9') and a filler character ('<') for date of birth and date of expiry fields
* Check digit size - one character
* Check digit value - decimal digit ('0' - '9')
* Check digit location - trailing (right-most) character of individual fields, trailing character of second line for composite check digit
* Value length - two lines of 36 characters plus additional line separator characters as specified by the LineSeparator property
* Class name - `Icao9303SizeTD2Algorithm`

#### Links

https://www.icao.int/publications/Documents/9303_p6_cons_en.pdf

### ICAO 9303 Document Size TD3 Algorithm

#### Description

The ICAO 9303 (International Civil Aviation Organization) specification for 
Machine Readable Passports and other Size TD3 travel documents uses multiple
check digits in the machine readable zone of the document. The second line of the
machine readable zone contains fields for passport number, date of birth, date of
expiry and an optional personal number field with each field having a check digit 
calculated using the [ICAO 9303 Algorithm](#icao-9303-algorithm). In addition, 
the machine readable zone contains a final composite check digit calculated for 
all four of the above fields and their check digits. The composite check digit 
is also calculated using the [ICAO 9303 Algorithm](#icao-9303-algorithm).

The machine readable zone of a Size TD3 document consists of two lines of 44
characters. The value passed to the Validate method should contain both lines of
data concatenated together. You may optionally use line separator characters in 
the concatenated value, either the Windows line separator (a carriage return 
character followed by a line feed character - '\r\n') or the Unix line separator 
(a line feed character - '\n'). The ICAO 9303 Document Size TD3 algorithm will
determine the line separator used from the length the value passed to the Validate 
method - 88 characters for no line separators, 90 characters for Windows line 
separators and 89 characters for Unix line separators. If the length of the value 
does not match one of these lengths then the Validate method will return `false`.

The ICAO 9303 Document Size TD3 Algorithm will validate the check digits of the
four fields (passport number, date of birth, date of expiry and optional personal
number) as well as the composite check digit. If any of the check digits fail 
validation then the Validate method will return `false`.

The ICAO 9303 Document Size TD3 algorithm only supports validation of check 
digits and does not support calculation of check digits.

#### Details

* Valid characters - 
  * decimal digits ('0' - '9'), upper case letters ('A' - 'Z') and a filler character ('<') for document number/extended document number field
  * decimal digits ('0' - '9') and a filler character ('<') for date of birth and date of expiry fields
* Check digit size - one character
* Check digit value - decimal digit ('0' - '9')
* Check digit location - trailing (right-most) character of individual fields, trailing character of entire string for composite check digit
* Value length - two lines of 44 characters plus additional line separator characters as specified by the LineSeparator property
* Class name - `Icao9303SizeTD3Algorithm`

#### Links

https://www.icao.int/publications/Documents/9303_p4_cons_en.pdf

### ICAO 9303 Machine Readable Visa Algorithm

#### Description

The ICAO 9303 (International Civil Aviation Organization) specification for 
Machine Readable Visas uses multiple check digits in the machine readable zone 
of the document. The second line of the machine readable zone contains fields 
for document number, date of birth and date of expiry and associated check 
digits for each field. (Unlike other ICAO 9303 TD1, TD2 or TD3 documents, no 
composite check digit is used.) The individual field check digits are all 
calculated using the [ICAO 9303 Algorithm](#icao-9303-algorithm).

Machine Readable Visas have two formats: MRV-A and MRV-B. The MRV-A format uses
two lines of 44 characters while the MRV-B format uses two lines of 36 characters.
The individual fields in the second line of the machine readable zone are located
in the same character positions regardless of the format. The Validate method
can validate either format and the algorithm will determine the format from the 
length of the value passed to the Validate method.

The machine readable zone of a Machine Readable Visa consists of two lines of 36
characters. The value passed to the Validate method should contain all lines of
data concatenated together. You may optionally use line separator characters in 
the concatenated value, either the Windows line separator (a carriage return 
character followed by a line feed character - '\r\n') or the Unix line separator 
(a line feed character - '\n'). The ICAO 9303 Machine Readable Visa algorithm will
determine the line separator used from the length the value passed to the Validate 
method - 88 characters (MRV-A) or 72 characters (MRV-B) for no line separators, 
90 characters (MRV-A) or 74 characters (MRV-B) for Windows line separators and 
89 characters (MRV-A) or 73 characters (MRV-B) for Unix line separators. If the 
length of the value does not match one of these lengths then the Validate method 
will return `false`.

The ICAO 9303 Machine Readable Visa Algorithm will validate the check digits of 
the three fields (document number, date of birth and date of expiry). If any of 
the check digits fail validation then the Validate method will return `false`.
In addition, if the value is not the correct length (two lines of either 44 or 
36 characters, plus line separator characters matching the LineSeparator 
property) then the method will return false.

The ICAO 9303 Machine Readable Visa algorithm only supports validation of check 
digits and does not support calculation of check digits.

#### Details

* Valid characters - 
  * decimal digits ('0' - '9'), upper case letters ('A' - 'Z') and a filler character ('<') for document number/extended document number field
  * decimal digits ('0' - '9') and a filler character ('<') for date of birth and date of expiry fields
* Check digit size - one character
* Check digit value - decimal digit ('0' - '9')
* Check digit location - trailing (right-most) character of individual fields
* Value length - two lines of either 44 characters (MRV-A) or 36 characters (MRV-B), plus additional line separator characters as specified by the LineSeparator property
* Class name - `Icao9303MachineReadableVisaAlgorithm`

#### Links

https://www.icao.int/publications/Documents/9303_p7_cons_en.pdf

### ISAN Algorithm

#### Description

The ISAN (International Standard Audiovisual Number) algorithm uses a variation 
of the ISO/IEC 7064 MOD 37,36 algorithm and can have either one or two check
characters. A full ISAN value consists of 12 hexadecimal digits for the "root" 
segment, 4 hexadecimal digits for the "episode" segment, an alphanumeric check
character calculated for the 16 characters of the root/episode segments and 
optionally, 8 hexadecimal digits for the version segment and an alphanumeric check 
character calculated for the 24 characters of the root/episode/version segments. 
Per https://www.isan.org/docs/isan_check_digit_calculation_v2.0.pdf, both check 
characters must be correct if the value includes a version segment.

CheckDigits.Net can validate either unformatted ISAN values consisting only of 
hexadecimal digits and alphanumeric check characters or ISAN values that have
been formatted for human readability. 

To validate unformatted root+version ISAN values, use the Validate method. The
Validate method only checks 26 character unformatted ISAN root+version values.
(To check 17 character root/episode only ISAN values, use the ISO/IEC 7064 MOD 37,36
algorithm directly.)

To validate formatted ISAN values, either root/episode values or root/episode/version values,
use the ValidateFormatted method. The ValidateFormatted method will check both
the format of the value ("ISAN " prefix plus dash characters that separate the
value into 4 character groups) and the check character(s) in the value.

An example formatted root/episode ISAN value is ISAN 0000-0000-C36D-002B-K. An
example formatted root/episode/version ISAN value is ISAN 0000-0000-C36D-002B-K-0000-0000-E.

#### Details

* Valid characters - hexadecimal characters ('0' - '9', 'A' - 'F')
* Check digit size - one character
* Check digit value - alphanumeric characters ('0' - '9', 'A' - 'Z')
* Check digit location - the 17th non-format character (**and** the 26th non-format character for root+version values)
* Class name - `IsanAlgorithm`

#### Links

https://en.wikipedia.org/wiki/International_Standard_Audiovisual_Number
https://www.isan.org/docs/isan_check_digit_calculation_v2.0.pdf
https://web.isan.org/public/en/search

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
* Class name - `IsinAlgorithm`

#### Links

Wikipedia: https://en.wikipedia.org/wiki/International_Securities_Identification_Number

### ISO 6346 Algorithm

The ISO 6346 algorithm is used for eleven character shipping container numbers.

#### Details

* Valid characters - alphanumeric characters ('0' - '9', 'A' - 'Z')
* Check digit size - one character
* Check digit value - decimal digit ('0' - '9')
* Check digit location - assumed to be the trailing (right-most) character when validating
* Value length - 11
* Class name - `Iso6346Algorithm`

#### Links

Wikipedia: https://en.wikipedia.org/wiki/ISO_6346

### ISO/IEC 7064 MOD 11,10 Algorithm

The ISO/IEC 7064 MOD 11,10 algorithm is a hybrid system algorithm (with M = 10
and M+1 = 11) that is suitable for use with numeric strings. It generates a 
single check character that is a decimal digit.

#### Details

* Valid characters - decimal digits ('0' - '9')
* Check digit size - one character
* Check digit value - decimal digit ('0' - '9')
* Check digit location - assumed to be the trailing (right-most) character when validating
* Class name - `Iso7064Mod11_10Algorithm`

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
* Class name - `Iso7064Mod11_2Algorithm`

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
* Class name - `Iso7064Mod1271_36Algorithm`

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
and radix 2) that is suitable for use with alphanumeric strings. It generates a 
single check character that is either an alphanumeric character or a 
supplementary '*' character.

#### Details

* Valid characters - alphanumeric characters ('0' - '9', 'A' - 'Z')
* Check digit size - one character
* Check digit value - either decimal digit ('0' - '9', 'A' - 'Z') or an asterisk '*'
* Check digit location - assumed to be the trailing (right-most) character when validating
* Class name - `Iso7064Mod37_2Algorithm`

#### Common Applications

* International Society of Blood Transfusion (ISBT) Donation Identification Numbers

### ISO/IEC 7064 MOD 37,36 Algorithm

The ISO/IEC 7064 MOD 37,36 algorithm is a hybrid system algorithm (with M = 36
and M+1 = 37) that is suitable for use with alphanumeric strings. It generates a 
single check character that is an alphanumeric character.

#### Details

* Valid characters - alphanumeric characters ('0' - '9', 'A' - 'Z')
* Check digit size - one character
* Check digit value - alphanumeric characters ('0' - '9', 'A' - 'Z')
* Check digit location - assumed to be the trailing (right-most) character when validating
* Class name - `Iso7064Mod37_36Algorithm`

#### Common Applications

* Global Release Identifier (GRid)

### ISO/IEC 7064 MOD 661-26 Algorithm

The ISO/IEC 7064 MOD 661-26 algorithm is a pure system algorithm (with modulus 
661 and radix 26) that is suitable for use with alphabetic strings. It generates 
two check alphabetic characters.

#### Details

* Valid characters - alphabetic characters ('A' - 'Z')
* Check digit size - two characters
* Check digit value - alphabetic characters ('A' - 'Z')
* Check digit location - assumed to be the trailing (right-most) characters when validating
* Class name - `Iso7064Mod661_26Algorithm`

### ISO/IEC 7064 MOD 97-10 Algorithm

The ISO/IEC 7064 MOD 97-10 algorithm is a pure system algorithm (with modulus 97
and radix 210) that is suitable for use with numeric strings. It generates two 
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
* Class name - `Iso7064Mod97_10Algorithm`

### Luhn Algorithm

#### Description

The Luhn algorithm is a modulus 10 algorithm that was developed in 1960 by Hans
Peter Luhn. It can detect all single digit transcription errors and most two digit
transposition errors except *09 -> 90* and vice versa. It can also detect most
twin errors (i.e. *11 <-> 44*) except *22 <-> 55*,  *33 <-> 66* and *44 <-> 77*.

`LuhnAlgorithm` implements `IMaskedCheckDigitAlgorithm` and can be used 
to validate values that are formatted with non-check digit characters (for example,
a credit card number formatted with spaces or dashes).

#### Details

* Valid characters - decimal digits ('0' - '9')
* Check digit size - one character
* Check digit value - decimal digit ('0' - '9')
* Check digit location - assumed to be the trailing (right-most) character when validating
* Class name - `LuhnAlgorithm`

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
* Class name - `Modulus10_1Algorithm`

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
* Class name - `Modulus10_2Algorithm`

#### Common Applications

* International Maritime Organization (IMO) Number

#### Links

Wikipedia: https://en.wikipedia.org/wiki/IMO_number

### Modulus10_13 Algorithm

#### Description

The Modulus10_13 algorithm is a widely used modulus 10 algorithm that uses weights
1 and 3 (odd positions have weight 3, even positions have weight 1). It can detect
all single digit transcription errors and ~89% of two digit transposition errors
(except where the transposed digits have a difference of 5, i.e. *1 <-> 6*, *2 <-> 7*,
etc.). The algorithm cannot detect two digit jump transpositions.

`Modulus10_13Algorithm` implements `IMaskedCheckDigitAlgorithm` and can be used 
to validate values that are formatted with non-check digit characters (for example,
a value formatted with spaces or dashes for human readability).

#### Details

* Valid characters - decimal digits ('0' - '9')
* Check digit size - one character
* Check digit value - decimal digit ('0' - '9')
* Check digit location - assumed to be the trailing (right-most) character when validating
* Class name - `Modulus10_13Algorithm`

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

NOTE: This algorithm has been deprecated in favor of using the Modulus11Extended 
algorithm.

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
* Class name - `Modulus11Algorithm`

#### Common Applications

* International Standard Book Number, prior to January 1, 2007 (ISBN-10)
* International Standard Serial Number (ISSN)

#### Links

Wikipedia: 
  https://en.wikipedia.org/wiki/ISBN#ISBN-10_check_digits
  https://en.wikipedia.org/wiki/ISSN

### Modulus11_27Decimal Algorithm

The Modulus11_27Decimal algorithm uses modulus 11 and the IBM modulus 11 weighting
scheme where each digit is weighted by the repeating sequence of weights 2, 3, 4, 
5, 6, 7 starting with weight 2 for the right-most non-check digit character. The 
sequence of weights is repeated as necessary for values longer than 6 characters.

Prior to the existence of the Verhoeff algorithm and the Damm algorithm, modulus 
11 algorithms were popular because they were very capable of detecting two digit 
transposition errors while using only a single check character. However, because 
it used modulus 11, the check character could not be a single decimal digit. 

There are two common solutions to this problem: use a non-digit character to 
represent the 11th possible check value or reject any value that would require a
non-digit check character. Using a non-digit check character (commonly 'X') means
that the value is not an integer and must be stored as a string. Rejecting any
value that could require a non-digit check character means that one out of eleven 
possible values must be rejected, or approximately 9.09% of all values.

The Modulus11_27Decimal algorithm takes the latter approach and the `TryCalculateCheckDigit`
and `Validate` methods return false if the value would require a non-digit check
character.

#### Details

* Valid characters - decimal digits ('0' - '9')
* Check digit size - one character
* Check digit value - either decimal digit ('0' - '9')
* Check digit location - assumed to be the trailing (right-most) character when validating
* Class name - `Modulus11_27DecimalAlgorithm`

#### Common Applications

* Norwegian fødselsnummer (Norwegian National Identity Number), second of two included check digits

#### Links

Wikipedia: 
	https://en.wikipedia.org/wiki/National_identity_number_(Norway)

https://www.ibm.com/docs/en/rbd/9.6.0?topic=syslib-calculatechkdigitmod11

### Modulus11_27Extended Algorithm

The Modulus11_27Extended algorithm uses modulus 11 and the IBM modulus 11 weighting
scheme where each digit is weighted by the repeating sequence of weights 2, 3, 4, 
5, 6, 7 starting with weight 2 for the right-most non-check digit character. The 
sequence of weights is repeated as necessary for values longer than 6 characters.

Prior to the existence of the Verhoeff algorithm and the Damm algorithm, modulus 
11 algorithms were popular because they were very capable of detecting two digit 
transposition errors while using only a single check character. However, because 
it used modulus 11, the check character could not be a single decimal digit. 

There are two common solutions to this problem: use a non-digit character to 
represent the 11th possible check value or reject any value that would require a
non-digit check character. Using a non-digit check character (commonly 'X') means
that the value is not an integer and must be stored as a string. Rejecting any
value that could require a non-digit check character means that one out of eleven 
possible values must be rejected, or approximately 9.09% of all values.

The Modulus11_27Extended algorithm takes the former approach and the `TryCalculateCheckDigit`
and `Validate` allow values that include 'X' as an extended check character.

#### Details

* Valid characters - decimal digits ('0' - '9')
* Check digit size - one character
* Check digit value - either decimal digit ('0' - '9') or an uppercase 'X'
* Check digit location - assumed to be the trailing (right-most) character when validating
* Class name - `Modulus11_27ExtendedAlgorithm`

### Modulus11Decimal Algorithm

#### Description

The Modulus11Decimal algorithm uses modulus 11 and each digit is weighted by its 
position in the value, starting from the right-most digit. Prior to the existence 
of the Verhoeff algorithm and the Damm algorithm, modulus 11 algorithms were 
popular because they were very capable of detecting two digit transposition errors 
while using only a single check character. However, because it used modulus 11, 
the check character could not be a single decimal digit. 

There are two common solutions to this problem: use a non-digit character to 
represent the 11th possible check value or reject any value that would require a
non-digit check character. Using a non-digit check character (commonly 'X') means
that the value is not an integer and must be stored as a string. Rejecting any
value that could require a non-digit check character means that one out of eleven 
possible values must be rejected, or approximately 9.09% of all values.

The Modulus11Decimal algorithm takes the latter approach and the `TryCalculateCheckDigit`
and `Validate` methods return false if the value would require a non-digit check
character.

Modulus11Decimal is a generalized version of the NhsAlgorithm which drops the
fixed 10 character length required by NhsAlgorithm.

#### Details

* Valid characters - decimal digits ('0' - '9')
* Check digit size - one character
* Check digit value - either decimal digit ('0' - '9')
* Check digit location - assumed to be the trailing (right-most) character when validating
* Max length - 9 characters when generating a check digit; 10 characters when validating
* Class name - `Modulus11DecimalAlgorithm`

#### Common Applications

* UK National Health Service Number

#### Links

Wikipedia: 
	https://en.wikipedia.org/wiki/NHS_number#Format,_number_ranges,_and_check_characters

### Modulus11Extended Algorithm

#### Description

The Modulus11Extended algorithm uses modulus 11 and each digit is weighted by its 
position in the value, starting from the right-most digit. Prior to the existence 
of the Verhoeff algorithm and the Damm algorithm, modulus 11 algorithms were 
popular because they were very capable of detecting two digit transposition errors 
while using only a single check character. However, because it used modulus 11, 
the check character could not be a single decimal digit. 

There are two common solutions to this problem: use a non-digit character to 
represent the 11th possible check value or reject any value that would require a
non-digit check character. Using a non-digit check character (commonly 'X') means
that the value is not an integer and must be stored as a string. Rejecting any
value that could require a non-digit check character means that one out of eleven 
possible values must be rejected, or approximately 9.09% of all values.

The Modulus11Extended algorithm takes the former approach and the `TryCalculateCheckDigit`
and `Validate` allow values that include 'X' as an extended check character.

Modulus11Extended replaces the deprecated Modulus11 algorithm.

#### Details

* Valid characters - decimal digits ('0' - '9')
* Check digit size - one character
* Check digit value - either decimal digit ('0' - '9') or an uppercase 'X'
* Check digit location - assumed to be the trailing (right-most) character when validating
* Max length - 9 characters when generating a check digit; 10 characters when validating
* Class name - `Modulus11Extended`

#### Common Applications

* International Standard Book Number, prior to January 1, 2007 (ISBN-10)
* International Standard Serial Number (ISSN)

#### Links

Wikipedia: 
  https://en.wikipedia.org/wiki/ISBN#ISBN-10_check_digits
  https://en.wikipedia.org/wiki/ISSN

### NHS Algorithm

#### Description

NOTE: This algorithm has been deprecated in favor of using the Modulus11Decimal 
algorithm.

UK National Health Service (NHS) identifiers use a variation of the Modulus 11 
algorithm. However, instead of generating 11 possible values for the check digit,
the NHS algorithm does not allow a remainder of 10 (the 'X' character used by the
Modulus 11 algorithm). Any possible NHS number that would generate a remainder of 
10 is not allowed and those numbers are not issued. This means that the check 
digit for a NHS number remains '0' - '9'. The NHS algorithm retains all error 
detecting capabilities of the Modulus 11 algorithm (detecting all single digit 
transcription errors and all two digit transposition errors).

The NHS algorithm only supports validation of check digits and does not support 
calculation of check digits.

#### Details

* Valid characters - decimal digits ('0' - '9')
* Check digit size - one character
* Check digit value - decimal digit ('0' - '9')
* Check digit location - assumed to be the trailing (right-most) character when validating
* Value length - 10 characters
* Class name - `NhsAlgorithm`

#### Links

Wikipedia: 
	https://en.wikipedia.org/wiki/NHS_number#Format,_number_ranges,_and_check_characters
	https://www.datadictionary.nhs.uk/attributes/nhs_number.html

### NOID Check Digit Algorithm

#### Description

The NOID (Nice Opaque Identifier) Check Digit Algorithm is used by systems that
deal with persistent identifiers (for example, ARK (Archival Resource Key) 
identifiers). The algorithm can detect single character transcription errors and
two character transposition errors for values that are less than 29 characters 
in length. If the value is 29 character in length or greater then the algorithm
is slightly less capable. The algorithm operates on lower case betanumeric 
characters (i.e. alphanumeric characters, minus vowels, including 'y', and the 
letter 'l'). The use of betanumeric characters reduces the likelihood that an 
identifier would equal a recognizable word or that the digits 0 or 1 could be 
confused for the letters 'o' or 'l'.

#### Details

* Valid characters - betanumeric characters ('0123456789bcdfghjkmnpqrstvwxz')
* Check digit size - one character
* Check digit value - betanumeric characters ('0123456789bcdfghjkmnpqrstvwxz')
* Check digit location - assumed to be the trailing (right-most) character when validating
* Class name - `NcdAlgorithm`

#### Links

https://metacpan.org/dist/Noid/view/noid#NOID-CHECK-DIGIT-ALGORITHM

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

The NPI algorithm only supports validation of check digits and does not support 
calculation of check digits.

#### Details

* Valid characters - decimal digits ('0' - '9')
* Check digit size - one character
* Check digit value - decimal digit ('0' - '9')
* Check digit location - assumed to be the trailing (right-most) character when validating
* Value length - 10 characters
* Class name - `NpiAlgorithm`

#### Links

Wikipedia: https://en.wikipedia.org/wiki/National_Provider_Identifier

### SEDOL Algorithm

#### Description

The SEDOL (Stock Exchange Daily Official List) algorithm is used for seven 
character alphanumeric codes that identify financial securities in the United
Kingdom and Ireland.

The SEDOL algorithm only supports validation of check digits and does not 
support calculation of check digits.

#### Details

* Valid characters - alphanumeric characters, excluding vowels ('0' - '9', 'BCDFGHJKLMNPQRSTVWXYZ')
* Check digit size - one character
* Check digit value - decimal digit ('0' - '9')
* Check digit location - assumed to be the trailing (right-most) character when validating
* Value length - 7 characters
* Class name - `SedolAlgorithm`

#### Links

Wikipedia: https://en.wikipedia.org/wiki/SEDOL

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
* Class name - `VerhoeffAlgorithm`

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
* Class name - `VinAlgorithm`

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

Previous .Net 8 benchmarks available at https://github.com/KnowledgeForwardSolutions/CheckDigits.Net/blob/main/Documentation/DotNet8Benchmarks.md

Detailed benchmark results for .Net 8 vs .Net 10 located at https://github.com/KnowledgeForwardSolutions/CheckDigits.Net/blob/main/Documentation/DotNet8_DotNet10_PerformanceComparision.md

Note that the benchmarks for version 2.x.x of CheckDigits.Net were run on an Intel
i7-7700HQ CPU @ 2.80GHz computer while the benchmarks for version 3.x.x of 
CheckDigits.Net were run on an AMD RYZEN AI MAX+ 3950 CPU @ 3.00GHz computer.
The comparison benchmarks between .Net 8.0 and .Net 10.0 were all run on the same 
newer computer to ensure accurate comparisons. 

#### Benchmark Details

BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.7462/25H2/2025Update/HudsonValley2)
AMD RYZEN AI MAX+ 395 w/ Radeon 8060S 3.00GHz, 1 CPU, 32 logical and 16 physical cores
.NET SDK 10.0.101
  [Host]     : .NET 10.0.1 (10.0.1, 10.0.125.57005), X64 RyuJIT x86-64-v4
  DefaultJob : .NET 10.0.1 (10.0.1, 10.0.125.57005), X64 RyuJIT x86-64-v4

### TryCalculateCheckDigit/TryCalculateCheckDigits Methods

#### General Numeric Algorithms

Note that the Modulus10_1, Modulus10_2 and Modulus11 algorithms have a maximum 
length of 10 (including the check digit) for values being validated so their
benchmarks do not cover lengths greater than 10.

| Algorithm Name        | Value                 | Mean      | Error     | StdDev    | Allocated |
|-----------------------|-----------------------|----------:|-----------|-----------|-----------|
| Damm                  | 140                   | 1.956 ns  | 0.0017 ns | 0.0015 ns | -         |
| Damm                  | 140662                | 3.367 ns  | 0.0102 ns | 0.0096 ns | -         |
| Damm                  | 140662538             | 5.075 ns  | 0.0045 ns | 0.0040 ns | -         |
| Damm                  | 140662538042          | 7.286 ns  | 0.0254 ns | 0.0238 ns | -         |
| Damm                  | 140662538042551       | 9.441 ns  | 0.0226 ns | 0.0212 ns | -         |
| Damm                  | 140662538042551028    | 13.189 ns | 0.0125 ns | 0.0105 ns | -         |
| Damm                  | 140662538042551028265 | 16.549 ns | 0.0689 ns | 0.0611 ns | -         |
|                       |                       |           |           |           |           |
| ISO/IEC 706 MOD 11,10 | 140                   | 2.256 ns  | 0.0292 ns | 0.0273 ns | -         |
| ISO/IEC 706 MOD 11,10 | 140662                | 4.046 ns  | 0.0359 ns | 0.0318 ns | -         |
| ISO/IEC 706 MOD 11,10 | 140662538             | 5.119 ns  | 0.0255 ns | 0.0239 ns | -         |
| ISO/IEC 706 MOD 11,10 | 140662538042          | 6.555 ns  | 0.0703 ns | 0.0657 ns | -         |
| ISO/IEC 706 MOD 11,10 | 140662538042551       | 8.083 ns  | 0.0997 ns | 0.0933 ns | -         |
| ISO/IEC 706 MOD 11,10 | 140662538042551028    | 9.338 ns  | 0.0474 ns | 0.0420 ns | -         |
| ISO/IEC 706 MOD 11,10 | 140662538042551028265 | 10.988 ns | 0.0404 ns | 0.0378 ns | -         |
|                       |                       |           |           |           |           |
| ISO/IEC 706 MOD 11-2  | 140                   | 2.235 ns  | 0.0176 ns | 0.0165 ns | -         |
| ISO/IEC 706 MOD 11-2  | 140662                | 3.839 ns  | 0.0250 ns | 0.0234 ns | -         |
| ISO/IEC 706 MOD 11-2  | 140662538             | 4.636 ns  | 0.0240 ns | 0.0200 ns | -         |
| ISO/IEC 706 MOD 11-2  | 140662538042          | 5.603 ns  | 0.0362 ns | 0.0321 ns | -         |
| ISO/IEC 706 MOD 11-2  | 140662538042551       | 6.611 ns  | 0.0755 ns | 0.0707 ns | -         |
| ISO/IEC 706 MOD 11-2  | 140662538042551028    | 7.308 ns  | 0.0351 ns | 0.0329 ns | -         |
| ISO/IEC 706 MOD 11-2  | 140662538042551028265 | 8.910 ns  | 0.0669 ns | 0.0626 ns | -         |
|                       |                       |           |           |           |           |
| ISO/IEC 706 MOD 97-10 | 140                   | 2.926 ns  | 0.0236 ns | 0.0221 ns | -         |
| ISO/IEC 706 MOD 97-10 | 140662                | 4.230 ns  | 0.0289 ns | 0.0270 ns | -         |
| ISO/IEC 706 MOD 97-10 | 140662538             | 6.013 ns  | 0.0216 ns | 0.0202 ns | -         |
| ISO/IEC 706 MOD 97-10 | 140662538042          | 7.501 ns  | 0.0379 ns | 0.0336 ns | -         |
| ISO/IEC 706 MOD 97-10 | 140662538042551       | 9.122 ns  | 0.0407 ns | 0.0361 ns | -         |
| ISO/IEC 706 MOD 97-10 | 140662538042551028    | 11.271 ns | 0.0390 ns | 0.0364 ns | -         |
| ISO/IEC 706 MOD 97-10 | 140662538042551028265 | 13.185 ns | 0.0484 ns | 0.0429 ns | -         |
|                       |                       |           |           |           |           |
| Luhn                  | 140                   | 2.739 ns  | 0.0284 ns | 0.0265 ns | -         |
| Luhn                  | 140662                | 4.375 ns  | 0.0256 ns | 0.0227 ns | -         |
| Luhn                  | 140662538             | 5.987 ns  | 0.0550 ns | 0.0514 ns | -         |
| Luhn                  | 140662538042          | 6.769 ns  | 0.0602 ns | 0.0533 ns | -         |
| Luhn                  | 140662538042551       | 8.452 ns  | 0.0505 ns | 0.0448 ns | -         |
| Luhn                  | 140662538042551028    | 9.905 ns  | 0.0575 ns | 0.0538 ns | -         |
| Luhn                  | 140662538042551028265 | 11.526 ns | 0.0598 ns | 0.0559 ns | -         |
|                       |                       |           |           |           |           |
| Modulus10_13          | 140                   | 2.536 ns  | 0.0202 ns | 0.0179 ns | -         |
| Modulus10_13          | 140662                | 4.280 ns  | 0.0267 ns | 0.0250 ns | -         |
| Modulus10_13          | 140662538             | 5.105 ns  | 0.0284 ns | 0.0265 ns | -         |
| Modulus10_13          | 140662538042          | 6.363 ns  | 0.0230 ns | 0.0192 ns | -         |
| Modulus10_13          | 140662538042551       | 7.715 ns  | 0.0326 ns | 0.0305 ns | -         |
| Modulus10_13          | 140662538042551028    | 9.209 ns  | 0.0357 ns | 0.0334 ns | -         |
| Modulus10_13          | 140662538042551028265 | 10.628 ns | 0.0633 ns | 0.0561 ns | -         |
|                       |                       |           |           |           |           |
| Modulus10_1           | 140                   | 1.808 ns  | 0.0124 ns | 0.0096 ns | -         |
| Modulus10_1           | 140662                | 2.587 ns  | 0.0193 ns | 0.0171 ns | -         |
| Modulus10_1           | 140662538             | 4.043 ns  | 0.0233 ns | 0.0218 ns | -         |
|                       |                       |           |           |           |           |
| Modulus10_2           | 140                   | 1.854 ns  | 0.0206 ns | 0.0183 ns | -         |
| Modulus10_2           | 140662                | 2.656 ns  | 0.0145 ns | 0.0136 ns | -         |
| Modulus10_2           | 140662538             | 4.024 ns  | 0.0173 ns | 0.0162 ns | -         |
|                       |                       |           |           |           |           |
| Modulus11             | 140                   | 2.364 ns  | 0.0171 ns | 0.0152 ns | -         |
| Modulus11             | 140662                | 3.323 ns  | 0.0300 ns | 0.0280 ns | -         |
| Modulus11             | 140662538             | 4.341 ns  | 0.0301 ns | 0.0281 ns | -         |
|                       |                       |           |           |           |           |
| Modulus11_27Decimal   | 140                   | 3.292 ns  | 0.0590 ns | 0.0523 ns | -         |
| Modulus11_27Decimal   | 140662                | 4.785 ns  | 0.0273 ns | 0.0255 ns | -         |
| Modulus11_27Decimal   | 140662538             | 5.915 ns  | 0.0624 ns | 0.0583 ns | -         |
| Modulus11_27Decimal   | 140662538042          | 7.329 ns  | 0.0601 ns | 0.0502 ns | -         |
| Modulus11_27Decimal   | 140662538042551       | 8.629 ns  | 0.0962 ns | 0.0900 ns | -         |
| Modulus11_27Decimal   | 140662538042551028    | 9.721 ns  | 0.1167 ns | 0.1035 ns | -         |
| Modulus11_27Decimal   | 140662538042551028265 | 11.694 ns | 0.1306 ns | 0.1090 ns | -         |
|                       |                       |           |           |           |           |
| Modulus11_27Extended  | 140                   | 3.353 ns  | 0.0221 ns | 0.0207 ns | -         |
| Modulus11_27Extended  | 140662                | 4.813 ns  | 0.0994 ns | 0.0976 ns | -         |
| Modulus11_27Extended  | 140662538             | 6.043 ns  | 0.0908 ns | 0.0849 ns | -         |
| Modulus11_27Extended  | 140662538042          | 7.206 ns  | 0.0460 ns | 0.0407 ns | -         |
| Modulus11_27Extended  | 140662538042551       | 8.581 ns  | 0.0666 ns | 0.0623 ns | -         |
| Modulus11_27Extended  | 140662538042551028    | 9.427 ns  | 0.0393 ns | 0.0329 ns | -         |
| Modulus11_27Extended  | 140662538042551028265 | 11.830 ns | 0.1384 ns | 0.1295 ns | -         |
|                       |                       |           |           |           |           |
| Modulus11Decimal      | 140                   | 2.322 ns  | 0.0348 ns | 0.0325 ns | -         |
| Modulus11Decimal      | 140662                | 3.220 ns  | 0.0485 ns | 0.0453 ns | -         |
| Modulus11Decimal      | 140662538             | 4.239 ns  | 0.0544 ns | 0.0509 ns | -         |
|                       |                       |           |           |           |           |
| Verhoeff              | 140                   | 4.250 ns  | 0.0312 ns | 0.0277 ns | -         |
| Verhoeff              | 140662                | 6.399 ns  | 0.0542 ns | 0.0452 ns | -         |
| Verhoeff              | 140662538             | 9.977 ns  | 0.0363 ns | 0.0340 ns | -         |
| Verhoeff              | 140662538042          | 13.351 ns | 0.0441 ns | 0.0413 ns | -         |
| Verhoeff              | 140662538042551       | 16.793 ns | 0.0614 ns | 0.0574 ns | -         |
| Verhoeff              | 140662538042551028    | 19.818 ns | 0.0811 ns | 0.0758 ns | -         |
| Verhoeff              | 140662538042551028265 | 22.861 ns | 0.0866 ns | 0.0768 ns | -         |

#### General Alphabetic Algorithms

| Algorithm Name          | Value                 | Mean      | Error     | StdDev    | Allocated |
|-------------------------|-----------------------|----------:|-----------|-----------|-----------|
| ISO/IEC 7064 MOD 27,26  | EGR                   | 2.178 ns  | 0.0032 ns | 0.0030 ns | -         |
| ISO/IEC 7064 MOD 27,26  | EGRNML                | 3.900 ns  | 0.0091 ns | 0.0085 ns | -         |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOC             | 5.088 ns  | 0.0505 ns | 0.0472 ns | -         |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOCECU          | 6.933 ns  | 0.0953 ns | 0.0891 ns | -         |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOCECUJIK       | 7.753 ns  | 0.0516 ns | 0.0458 ns | -         |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOCECUJIKNWW    | 9.899 ns  | 0.2137 ns | 0.3570 ns | -         |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOCECUJIKNWWVVO | 11.228 ns | 0.2467 ns | 0.4694 ns | -         |
|                         |                       |           |           |           |           |
| ISO/IEC 7064 MOD 661-26 | EGR                   | 3.193 ns  | 0.0212 ns | 0.0198 ns | -         |
| ISO/IEC 7064 MOD 661-26 | EGRNML                | 4.940 ns  | 0.0303 ns | 0.0268 ns | -         |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOC             | 6.459 ns  | 0.0187 ns | 0.0174 ns | -         |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOCECU          | 8.844 ns  | 0.0380 ns | 0.0297 ns | -         |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOCECUJIK       | 11.127 ns | 0.0432 ns | 0.0361 ns | -         |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOCECUJIKNWW    | 13.005 ns | 0.0354 ns | 0.0331 ns | -         |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOCECUJIKNWWVVO | 15.670 ns | 0.0925 ns | 0.0820 ns | -         |

#### General Alphanumeric Algorithms

Note that the values used for the NOID Check Digit algorithm do not include lengths
3 or 6 so that benchmarks are not run on purely numeric strings.

| Algorithm Name           | Value                 | Mean      | Error     | StdDev    | Allocated |
|--------------------------|-----------------------|----------:|-----------|-----------|-----------|
| AlphanumericMod97_10     | U7y                   | 4.951 ns  | 0.0035 ns | 0.0033 ns | -         |
| AlphanumericMod97_10     | U7y8SX                | 8.103 ns  | 0.0134 ns | 0.0112 ns | -         |
| AlphanumericMod97_10     | U7y8SXrC0             | 11.861 ns | 0.0131 ns | 0.0116 ns | -         |
| AlphanumericMod97_10     | U7y8SXrC0O3S          | 14.879 ns | 0.0324 ns | 0.0270 ns | -         |
| AlphanumericMod97_10     | U7y8SXrC0O3Sc4I       | 19.017 ns | 0.0234 ns | 0.0219 ns | -         |
| AlphanumericMod97_10     | U7y8SXrC0O3Sc4IHYQ    | 24.122 ns | 0.0783 ns | 0.0654 ns | -         |
| AlphanumericMod97_10     | U7y8SXrC0O3Sc4IHYQF4M | 28.224 ns | 0.0837 ns | 0.0742 ns | -         |
|                          |                       |           |           |           |           |
| ISO/IEC 7064 MOD 1271-36 | U7Y                   | 3.993 ns  | 0.0239 ns | 0.0223 ns | -         |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SX                | 5.886 ns  | 0.0195 ns | 0.0173 ns | -         |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SXRC0             | 7.876 ns  | 0.0498 ns | 0.0466 ns | -         |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SXRC0O3S          | 9.690 ns  | 0.0557 ns | 0.0494 ns | -         |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SXRC0O3SC4I       | 12.064 ns | 0.0856 ns | 0.0801 ns | -         |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SXRC0O3SC4IHYQ    | 14.832 ns | 0.0857 ns | 0.0760 ns | -         |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SXRC0O3SC4IHYQF4M | 17.734 ns | 0.1363 ns | 0.1275 ns | -         |
|                          |                       |           |           |           |           |
| ISO/IEC 7064 MOD 37-2    | U7Y                   | 3.182 ns  | 0.0303 ns | 0.0253 ns | -         |
| ISO/IEC 7064 MOD 37-2    | U7Y8SX                | 4.540 ns  | 0.0299 ns | 0.0280 ns | -         |
| ISO/IEC 7064 MOD 37-2    | U7Y8SXRC0             | 6.188 ns  | 0.0817 ns | 0.0764 ns | -         |
| ISO/IEC 7064 MOD 37-2    | U7Y8SXRC0O3S          | 6.959 ns  | 0.0358 ns | 0.0299 ns | -         |
| ISO/IEC 7064 MOD 37-2    | U7Y8SXRC0O3SC4I       | 8.238 ns  | 0.0535 ns | 0.0474 ns | -         |
| ISO/IEC 7064 MOD 37-2    | U7Y8SXRC0O3SC4IHYQ    | 9.840 ns  | 0.0572 ns | 0.0507 ns | -         |
| ISO/IEC 7064 MOD 37-2    | U7Y8SXRC0O3SC4IHYQF4M | 10.781 ns | 0.0573 ns | 0.0508 ns | -         |
|                          |                       |           |           |           |           |
| ISO/IEC 7064 MOD 37,36   | U7Y                   | 3.191 ns  | 0.0277 ns | 0.0231 ns | -         |
| ISO/IEC 7064 MOD 37,36   | U7Y8SX                | 4.903 ns  | 0.0462 ns | 0.0432 ns | -         |
| ISO/IEC 7064 MOD 37,36   | U7Y8SXRC0             | 6.787 ns  | 0.1198 ns | 0.1120 ns | -         |
| ISO/IEC 7064 MOD 37,36   | U7Y8SXRC0O3S          | 8.334 ns  | 0.0563 ns | 0.0499 ns | -         |
| ISO/IEC 7064 MOD 37,36   | U7Y8SXRC0O3SC4I       | 9.866 ns  | 0.0656 ns | 0.0548 ns | -         |
| ISO/IEC 7064 MOD 37,36   | U7Y8SXRC0O3SC4IHYQ    | 11.466 ns | 0.0786 ns | 0.0735 ns | -         |
| ISO/IEC 7064 MOD 37,36   | U7Y8SXRC0O3SC4IHYQF4M | 13.145 ns | 0.0616 ns | 0.0577 ns | -         |
|                          |                       |           |           |           |           |
| NOID Check Digit         | 11404/2h9             | 4.511 ns  | 0.0313 ns | 0.0262 ns | -         |
| NOID Check Digit         | 11404/2h9tqb          | 6.029 ns  | 0.0305 ns | 0.0285 ns | -         |
| NOID Check Digit         | 11404/2h9tqbxk6       | 7.134 ns  | 0.0436 ns | 0.0407 ns | -         |
| NOID Check Digit         | 11404/2h9tqbxk6rw7    | 8.861 ns  | 0.0767 ns | 0.0718 ns | -         |
| NOID Check Digit         | 11404/2h9tqbxk6rw7dwm | 10.324 ns | 0.0848 ns | 0.0793 ns | -         |

#### Value Specific Algorithms

Note: ABA RTN, CUSIP, ICAO 9303 multi-field algorithms (Machine Readable Visa, Size TD1, 
TD2 and TD3), ISAN, NHS, NPI and SEDOL algorithms do not support calculation of check digits, 
only validation of values containing check digits.

| Algorithm Name | Value                           | Mean      | Error     | StdDev    | Allocated |
|----------------|---------------------------------|----------:|-----------|-----------|-----------|
| IBAN           | BE00096123456769                | 12.797 ns | 0.0626 ns | 0.0586 ns | -         |
| IBAN           | GB00WEST12345698765432          | 22.264 ns | 0.2030 ns | 0.1695 ns | -         |
| IBAN           | SC00MCBL01031234567890123456USD | 33.121 ns | 0.1529 ns | 0.1355 ns | -         |
|                |                                 |           |           |           |           |
| ICAO 9303      | U7Y                             | 4.193 ns  | 0.0450 ns | 0.0421 ns | -         |
| ICAO 9303      | U7Y8SX                          | 5.840 ns  | 0.0395 ns | 0.0370 ns | -         |
| ICAO 9303      | U7Y8SXRC0                       | 7.757 ns  | 0.1317 ns | 0.1293 ns | -         |
| ICAO 9303      | U7Y8SXRC0O3S                    | 9.235 ns  | 0.0897 ns | 0.0839 ns | -         |
| ICAO 9303      | U7Y8SXRC0O3SC4I                 | 11.032 ns | 0.2064 ns | 0.1723 ns | -         |
| ICAO 9303      | U7Y8SXRC0O3SC4IHYQ              | 12.823 ns | 0.0541 ns | 0.0452 ns | -         |
| ICAO 9303      | U7Y8SXRC0O3SC4IHYQF4M           | 14.776 ns | 0.1006 ns | 0.0941 ns | -         |
|                |                                 |           |           |           |           |
| ISIN           | AU0000XVGZA                     | 7.917 ns  | 0.0690 ns | 0.0645 ns | -         |
| ISIN           | GB000263494                     | 7.118 ns  | 0.0536 ns | 0.0475 ns | -         |
| ISIN           | US037833100                     | 7.132 ns  | 0.0948 ns | 0.0792 ns | -         |
|                |                                 |           |           |           |           |
| ISO 6346       | CSQU305438                      | 7.445 ns  | 0.0693 ns | 0.0579 ns | -         |
| ISO 6346       | MSKU907032                      | 7.493 ns  | 0.0903 ns | 0.0800 ns | -         |
| ISO 6346       | TOLU473478                      | 7.467 ns  | 0.0621 ns | 0.0550 ns | -         |
|                |                                 |           |           |           |           |
| VIN            | 1G8ZG127_WZ157259               | 13.113 ns | 0.0649 ns | 0.0542 ns | -         |
| VIN            | 1HGEM212_2L047875               | 12.975 ns | 0.0850 ns | 0.0710 ns | -         |
| VIN            | 1M8GDM9A_KP042788               | 13.377 ns | 0.1008 ns | 0.0942 ns | -         |

### Validate Method

#### General Numeric Algorithms

All algorithms use a single check digit except ISO/IEC 7064 MOD 97-10 which uses
two check digits.

Note that the Modulus10_1, Modulus10_2 and Modulus11 algorithms have a maximum 
length of 10 (including the check digit) for values being validated so their
benchmarks do not cover lengths greater than 10.

| Algorithm Name        | Value                         | Mean      | Error     | StdDev    | Allocated |
|-----------------------|-------------------------------|----------:|-----------|-----------|-----------|
| Damm                  | 1402                          | 2.107 ns  | 0.0153 ns | 0.0144 ns | -         |
| Damm                  | 1406622                       | 3.959 ns  | 0.0172 ns | 0.0160 ns | -         |
| Damm                  | 1406625388                    | 6.171 ns  | 0.0498 ns | 0.0441 ns | -         |
| Damm                  | 1406625380422                 | 8.236 ns  | 0.0080 ns | 0.0071 ns | -         |
| Damm                  | 1406625380425518              | 11.350 ns | 0.2009 ns | 0.1879 ns | -         |
| Damm                  | 1406625380425510280           | 14.888 ns | 0.1275 ns | 0.1130 ns | -         |
| Damm                  | 1406625380425510282654        | 17.861 ns | 0.1244 ns | 0.1103 ns | -         |
|                       |                               |           |           |           |           |
| ISO/IEC 706 MOD 11,10 | 1409                          | 2.482 ns  | 0.0221 ns | 0.0196 ns | -         |
| ISO/IEC 706 MOD 11,10 | 1406623                       | 4.196 ns  | 0.0318 ns | 0.0297 ns | -         |
| ISO/IEC 706 MOD 11,10 | 1406625381                    | 5.715 ns  | 0.0421 ns | 0.0394 ns | -         |
| ISO/IEC 706 MOD 11,10 | 1406625380426                 | 6.553 ns  | 0.0593 ns | 0.0526 ns | -         |
| ISO/IEC 706 MOD 11,10 | 1406625380425514              | 8.079 ns  | 0.1089 ns | 0.0909 ns | -         |
| ISO/IEC 706 MOD 11,10 | 1406625380425510286           | 9.670 ns  | 0.1054 ns | 0.0934 ns | -         |
| ISO/IEC 706 MOD 11,10 | 1406625380425510282657        | 11.182 ns | 0.0617 ns | 0.0577 ns | -         |
|                       |                               |           |           |           |           |
| ISO/IEC 706 MOD 11-2  | 140X                          | 2.074 ns  | 0.0171 ns | 0.0152 ns | -         |
| ISO/IEC 706 MOD 11-2  | 1406628                       | 3.803 ns  | 0.0240 ns | 0.0213 ns | -         |
| ISO/IEC 706 MOD 11-2  | 1406625380                    | 4.420 ns  | 0.0263 ns | 0.0246 ns | -         |
| ISO/IEC 706 MOD 11-2  | 1406625380426                 | 5.091 ns  | 0.0302 ns | 0.0282 ns | -         |
| ISO/IEC 706 MOD 11-2  | 1406625380425511              | 5.880 ns  | 0.0254 ns | 0.0225 ns | -         |
| ISO/IEC 706 MOD 11-2  | 140662538042551028X           | 6.881 ns  | 0.0434 ns | 0.0385 ns | -         |
| ISO/IEC 706 MOD 11-2  | 1406625380425510282651        | 7.915 ns  | 0.0558 ns | 0.0466 ns | -         |
|                       |                               |           |           |           |           |
| ISO/IEC 706 MOD 97-10 | 14066                         | 2.411 ns  | 0.0131 ns | 0.0122 ns | -         |
| ISO/IEC 706 MOD 97-10 | 14066262                      | 4.086 ns  | 0.0298 ns | 0.0264 ns | -         |
| ISO/IEC 706 MOD 97-10 | 14066253823                   | 5.394 ns  | 0.0314 ns | 0.0294 ns | -         |
| ISO/IEC 706 MOD 97-10 | 14066253804250                | 6.914 ns  | 0.0428 ns | 0.0380 ns | -         |
| ISO/IEC 706 MOD 97-10 | 14066253804255112             | 8.808 ns  | 0.0543 ns | 0.0508 ns | -         |
| ISO/IEC 706 MOD 97-10 | 14066253804255102853          | 10.441 ns | 0.0491 ns | 0.0460 ns | -         |
| ISO/IEC 706 MOD 97-10 | 14066253804255102826587       | 12.202 ns | 0.0511 ns | 0.0478 ns | -         |
|                       |                               |           |           |           |           |
| Luhn                  | 1404                          | 3.544 ns  | 0.0239 ns | 0.0224 ns | -         |
| Luhn                  | 1406628                       | 4.607 ns  | 0.0323 ns | 0.0286 ns | -         |
| Luhn                  | 1406625382                    | 6.550 ns  | 0.0597 ns | 0.0558 ns | -         |
| Luhn                  | 1406625380421                 | 7.363 ns  | 0.0547 ns | 0.0485 ns | -         |
| Luhn                  | 1406625380425514              | 9.138 ns  | 0.0823 ns | 0.0730 ns | -         |
| Luhn                  | 1406625380425510285           | 10.409 ns | 0.0735 ns | 0.0614 ns | -         |
| Luhn                  | 1406625380425510282651        | 12.164 ns | 0.0669 ns | 0.0626 ns | -         |
|                       |                               |           |           |           |           |
| Modulus10_13          | 1403                          | 3.020 ns  | 0.0234 ns | 0.0219 ns | -         |
| Modulus10_13          | 1406627                       | 4.638 ns  | 0.0212 ns | 0.0177 ns | -         |
| Modulus10_13          | 1406625385                    | 5.447 ns  | 0.0333 ns | 0.0311 ns | -         |
| Modulus10_13          | 1406625380425                 | 6.674 ns  | 0.0399 ns | 0.0333 ns | -         |
| Modulus10_13          | 1406625380425518              | 8.093 ns  | 0.0361 ns | 0.0320 ns | -         |
| Modulus10_13          | 1406625380425510288           | 9.428 ns  | 0.0437 ns | 0.0387 ns | -         |
| Modulus10_13          | 1406625380425510282657        | 10.903 ns | 0.0325 ns | 0.0304 ns | -         |
|                       |                               |           |           |           |           |
| Modulus10_1           | 1401                          | 1.904 ns  | 0.0188 ns | 0.0157 ns | -         |
| Modulus10_1           | 1406628                       | 2.879 ns  | 0.0315 ns | 0.0295 ns | -         |
| Modulus10_1           | 1406625384                    | 4.125 ns  | 0.0316 ns | 0.0295 ns | -         |
|                       |                               |           |           |           |           |
| Modulus10_2           | 1406                          | 1.847 ns  | 0.0170 ns | 0.0151 ns | -         |
| Modulus10_2           | 1406627                       | 2.898 ns  | 0.0227 ns | 0.0212 ns | -         |
| Modulus10_2           | 1406625389                    | 4.048 ns  | 0.0270 ns | 0.0253 ns | -         |
|                       |                               |           |           |           |           |
| Modulus11             | 1406                          | 2.948 ns  | 0.0241 ns | 0.0214 ns | -         |
| Modulus11             | 1406620                       | 4.062 ns  | 0.0213 ns | 0.0200 ns | -         |
| Modulus11             | 1406625388                    | 4.506 ns  | 0.0278 ns | 0.0247 ns | -         |
|                       |                               |           |           |           |           |
| Modulus11_27Decimal   | 1406                          | 2.978 ns  | 0.0309 ns | 0.0274 ns | -         |
| Modulus11_27Decimal   | 1406620                       | 4.800 ns  | 0.0882 ns | 0.0825 ns | -         |
| Modulus11_27Decimal   | 1406625385                    | 5.773 ns  | 0.0791 ns | 0.0740 ns | -         |
| Modulus11_27Decimal   | 1406625380421                 | 6.889 ns  | 0.0383 ns | 0.0340 ns | -         |
| Modulus11_27Decimal   | 1406625380425510              | 8.146 ns  | 0.0545 ns | 0.0483 ns | -         |
| Modulus11_27Decimal   | 1406625380425510288           | 9.203 ns  | 0.0955 ns | 0.0846 ns | -         |
| Modulus11_27Decimal   | 1406625380425510282650        | 10.384 ns | 0.0623 ns | 0.0521 ns | -         |
|                       |                               |           |           |           |           |
| Modulus11_27Extended  | 1406                          | 3.252 ns  | 0.0207 ns | 0.0193 ns | -         |
| Modulus11_27Extended  | 1406620                       | 4.646 ns  | 0.0365 ns | 0.0324 ns | -         |
| Modulus11_27Extended  | 1406625385                    | 5.760 ns  | 0.0251 ns | 0.0235 ns | -         |
| Modulus11_27Extended  | 1406625380421                 | 7.035 ns  | 0.0508 ns | 0.0476 ns | -         |
| Modulus11_27Extended  | 1406625380425510              | 8.416 ns  | 0.0916 ns | 0.0812 ns | -         |
| Modulus11_27Extended  | 1406625380425510288           | 9.086 ns  | 0.0228 ns | 0.0190 ns | -         |
| Modulus11_27Extended  | 1406625380425510282650        | 10.344 ns | 0.0773 ns | 0.0645 ns | -         |
|                       |                               |           |           |           |           |
| Modulus11Decimal      | 1406                          | 1.892 ns  | 0.0528 ns | 0.0791 ns | -         |
| Modulus11Decimal      | 1406620                       | 3.126 ns  | 0.0801 ns | 0.1403 ns | -         |
| Modulus11Decimal      | 1406625388                    | 4.123 ns  | 0.0335 ns | 0.0313 ns | -         |
|                       |                               |           |           |           |           |
| Modulus11Extended     | 1406                          | 2.316 ns  | 0.0099 ns | 0.0093 ns | -         |
| Modulus11Extended     | 1406620                       | 3.931 ns  | 0.0054 ns | 0.0051 ns | -         |
| Modulus11Extended     | 1406625388                    | 4.563 ns  | 0.0169 ns | 0.0158 ns | -         |
|                       |                               |           |           |           |           |
| Verhoeff              | 1401                          | 4.827 ns  | 0.0255 ns | 0.0239 ns | -         |
| Verhoeff              | 1406625                       | 6.848 ns  | 0.0410 ns | 0.0383 ns | -         |
| Verhoeff              | 1406625388                    | 10.197 ns | 0.0473 ns | 0.0419 ns | -         |
| Verhoeff              | 1406625380426                 | 13.644 ns | 0.0384 ns | 0.0340 ns | -         |
| Verhoeff              | 1406625380425512              | 17.068 ns | 0.0725 ns | 0.0643 ns | -         |
| Verhoeff              | 1406625380425510285           | 20.048 ns | 0.0891 ns | 0.0790 ns | -         |
| Verhoeff              | 1406625380425510282655        | 23.163 ns | 0.0715 ns | 0.0597 ns | -         |

#### General Alphabetic Algorithms

ISO/IEC 7064 MOD 27,26 uses a single check character. ISO/IEC 7064 MOD 661-26
uses two check characters.

| Algorithm Name          | Value                   | Mean      | Error     | StdDev    | Allocated |
|-------------------------|-------------------------|----------:|-----------|-----------|-----------|
| ISO/IEC 7064 MOD 27,26  | EGRS                    | 2.332 ns  | 0.0226 ns | 0.0211 ns | -         |
| ISO/IEC 7064 MOD 27,26  | EGRNMLU                 | 4.097 ns  | 0.0308 ns | 0.0273 ns | -         |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOCB              | 5.679 ns  | 0.1293 ns | 0.2195 ns | -         |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOCECUA           | 7.347 ns  | 0.1623 ns | 0.2379 ns | -         |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOCECUJIKA        | 9.034 ns  | 0.1990 ns | 0.4729 ns | -         |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOCECUJIKNWWY     | 10.673 ns | 0.1450 ns | 0.1356 ns | -         |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOCECUJIKNWWVVOQ  | 11.102 ns | 0.1145 ns | 0.0956 ns | -         |
|                         |                         |           |           |           |           |
| ISO/IEC 7064 MOD 661-26 | EGRSE                   | 2.641 ns  | 0.0136 ns | 0.0121 ns | -         |
| ISO/IEC 7064 MOD 661-26 | EGRNMLDR                | 4.459 ns  | 0.0364 ns | 0.0340 ns | -         |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOCCK             | 6.448 ns  | 0.0262 ns | 0.0245 ns | -         |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOCECUZJ          | 8.374 ns  | 0.0898 ns | 0.0796 ns | -         |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOCECUJIKFQ       | 10.262 ns | 0.0453 ns | 0.0402 ns | -         |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOCECUJIKNWWQN    | 13.125 ns | 0.0858 ns | 0.0760 ns | -         |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOCECUJIKNWWVVORC | 15.238 ns | 0.0584 ns | 0.0546 ns | -         |

#### General Alphanumeric Algorithms

AlphanumericMod97_10 algorithm and ISO/IEC 7064 MOD 1271-36 uses two check characters. 
ISO/IEC 7064 MOD 37-2, ISO/IEC 7064 MOD 37,36 and NOID Check Digit algorithms use a 
single check character.

Note also that the values used for the NOID Check Digit algorithm do not include lengths
3 or 6 so that benchmarks are not run on purely numeric strings.

| Algorithm Name           | Value                   | Mean      | Error     | StdDev    | Allocated |
|--------------------------|-------------------------|----------:|-----------|-----------|-----------|
| AlphanumericMod97_10     | U7y46                   | 5.297 ns  | 0.0975 ns | 0.0912 ns | -         |
| AlphanumericMod97_10     | U7y8SX89                | 7.339 ns  | 0.0166 ns | 0.0147 ns | -         |
| AlphanumericMod97_10     | U7y8SXrC087             | 10.939 ns | 0.0123 ns | 0.0115 ns | -         |
| AlphanumericMod97_10     | U7y8SXrC0O3S38          | 14.217 ns | 0.0401 ns | 0.0375 ns | -         |
| AlphanumericMod97_10     | U7y8SXrC0O3Sc4I27       | 18.788 ns | 0.3287 ns | 0.7553 ns | -         |
| AlphanumericMod97_10     | U7y8SXrC0O3Sc4IHYQ54    | 23.346 ns | 0.0715 ns | 0.0669 ns | -         |
| AlphanumericMod97_10     | U7y8SXrC0O3Sc4IHYQF4M21 | 27.472 ns | 0.0671 ns | 0.0595 ns | -         |
|                          |                         |           |           |           |           |
| ISO/IEC 7064 MOD 1271-36 | U7YM0                   | 3.781 ns  | 0.0263 ns | 0.0233 ns | -         |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SXOR                | 5.045 ns  | 0.0353 ns | 0.0330 ns | -         |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SXRC0FI             | 7.174 ns  | 0.0804 ns | 0.0713 ns | -         |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SXRC0O3SX4          | 9.202 ns  | 0.0654 ns | 0.0580 ns | -         |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SXRC0O3SC4I9D       | 11.000 ns | 0.0479 ns | 0.0424 ns | -         |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SXRC0O3SC4IHYQYI    | 13.747 ns | 0.0482 ns | 0.0376 ns | -         |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SXRC0O3SC4IHYQF4M44 | 16.055 ns | 0.0694 ns | 0.0615 ns | -         |
|                          |                         |           |           |           |           |
| ISO/IEC 7064 MOD 37-2    | U7YZ                    | 2.688 ns  | 0.0120 ns | 0.0100 ns | -         |
| ISO/IEC 7064 MOD 37-2    | U7Y8SXV                 | 4.350 ns  | 0.0338 ns | 0.0299 ns | -         |
| ISO/IEC 7064 MOD 37-2    | U7Y8SXRC0E              | 5.145 ns  | 0.0262 ns | 0.0245 ns | -         |
| ISO/IEC 7064 MOD 37-2    | U7Y8SXRC0O3SU           | 6.425 ns  | 0.0441 ns | 0.0391 ns | -         |
| ISO/IEC 7064 MOD 37-2    | U7Y8SXRC0O3SC4IB        | 7.772 ns  | 0.0609 ns | 0.0570 ns | -         |
| ISO/IEC 7064 MOD 37-2    | U7Y8SXRC0O3SC4IHYQG     | 9.111 ns  | 0.0870 ns | 0.0813 ns | -         |
| ISO/IEC 7064 MOD 37-2    | U7Y8SXRC0O3SC4IHYQF4MF  | 10.416 ns | 0.1256 ns | 0.1113 ns | -         |
|                          |                         |           |           |           |           |
| ISO/IEC 7064 MOD 37,36   | U7YW                    | 3.189 ns  | 0.0284 ns | 0.0252 ns | -         |
| ISO/IEC 7064 MOD 37,36   | U7Y8SX8                 | 5.281 ns  | 0.0747 ns | 0.0624 ns | -         |
| ISO/IEC 7064 MOD 37,36   | U7Y8SXRC0E              | 6.783 ns  | 0.0679 ns | 0.0635 ns | -         |
| ISO/IEC 7064 MOD 37,36   | U7Y8SXRC0O3SR           | 8.329 ns  | 0.0880 ns | 0.0780 ns | -         |
| ISO/IEC 7064 MOD 37,36   | U7Y8SXRC0O3SC4IT        | 9.865 ns  | 0.1457 ns | 0.1292 ns | -         |
| ISO/IEC 7064 MOD 37,36   | U7Y8SXRC0O3SC4IHYQD     | 11.894 ns | 0.1117 ns | 0.0990 ns | -         |
| ISO/IEC 7064 MOD 37,36   | U7Y8SXRC0O3SC4IHYQF4MP  | 13.173 ns | 0.0399 ns | 0.0354 ns | -         |
|                          |                         |           |           |           |           |
| NOID Check Digit         | 11404/2h9m              | 5.089 ns  | 0.0247 ns | 0.0219 ns | -         |
| NOID Check Digit         | 11404/2h9tqb0           | 6.419 ns  | 0.0417 ns | 0.0369 ns | -         |
| NOID Check Digit         | 11404/2h9tqbxk6d        | 7.598 ns  | 0.0334 ns | 0.0279 ns | -         |
| NOID Check Digit         | 11404/2h9tqbxk6rw74     | 8.816 ns  | 0.0623 ns | 0.0552 ns | -         |
| NOID Check Digit         | 11404/2h9tqbxk6rw7dwmz  | 10.007 ns | 0.0597 ns | 0.0466 ns | -         |

#### Value Specific Algorithms

| Algorithm Name                  | Value                                  | Mean      | Error     | StdDev    | Allocated |
|---------------------------------|----------------------------------------|----------:|-----------|-----------|-----------|
| ABA RTN                         | 111000025                              | 4.572 ns  | 0.0099 ns | 0.0088 ns | -         |
| ABA RTN                         | 122235821                              | 4.528 ns  | 0.0049 ns | 0.0046 ns | -         |
| ABA RTN                         | 325081403                              | 4.526 ns  | 0.0029 ns | 0.0022 ns | -         |
|                                 |                                        |           |           |           |           |
| CUSIP                           | 37833100                               | 6.268 ns  | 0.0053 ns | 0.0049 ns | -         |
| CUSIP                           | 38143VAA7                              | 6.258 ns  | 0.0057 ns | 0.0050 ns | -         |
| CUSIP                           | 91282CJL6                              | 6.258 ns  | 0.0073 ns | 0.0065 ns | -         |
|                                 |                                        |           |           |           |           |
| FIGI                            | BBG000B9Y5X2                           | 7.885 ns  | 0.0056 ns | 0.0050 ns | -         |
| FIGI                            | BBG111111160                           | 7.929 ns  | 0.0053 ns | 0.0045 ns | -         |
| FIGI                            | BBGZYXWVTSR7                           | 8.047 ns  | 0.0575 ns | 0.0537 ns | -         |
|                                 |                                        |           |           |           |           |
| IBAN                            | BE71096123456769                       | 11.903 ns | 0.0768 ns | 0.0718 ns | -         |
| IBAN                            | GB82WEST12345698765432                 | 21.263 ns | 0.2220 ns | 0.2076 ns | -         |
| IBAN                            | SC74MCBL01031234567890123456USD        | 32.737 ns | 0.3820 ns | 0.3190 ns | -         |
|                                 |                                        |           |           |           |           |
| ICAO 9303                       | U7Y5                                   | 4.321 ns  | 0.0208 ns | 0.0195 ns | -         |
| ICAO 9303                       | U7Y8SX8                                | 6.020 ns  | 0.0632 ns | 0.0591 ns | -         |
| ICAO 9303                       | U7Y8SXRC03                             | 7.693 ns  | 0.0856 ns | 0.0801 ns | -         |
| ICAO 9303                       | U7Y8SXRC0O3S8                          | 9.483 ns  | 0.2059 ns | 0.2749 ns | -         |
| ICAO 9303                       | U7Y8SXRC0O3SC4I2                       | 10.983 ns | 0.0744 ns | 0.0696 ns | -         |
| ICAO 9303                       | U7Y8SXRC0O3SC4IHYQ9                    | 12.690 ns | 0.0482 ns | 0.0427 ns | -         |
| ICAO 9303                       | U7Y8SXRC0O3SC4IHYQF4M8                 | 14.541 ns | 0.0988 ns | 0.0876 ns | -         |
|                                 |                                        |           |           |           |           |
| ICAO 9303 Machine Readable Visa | I<UTOERIKSSON<<ANNA<MARIA<<<<<<<<<<<<br>D231458907UTO7408122F1204159<<<<<<<<                 | 19.684 ns | 0.1291 ns | 0.1207 ns | -         |
| ICAO 9303 Machine Readable Visa | I<UTOSKYWALKER<<LUKE<<<<<<<<<<<<<<<<<br>STARWARS45UTO7705256M2405252<<<<<<<<                 | 19.651 ns | 0.1502 ns | 0.1405 ns | -         |
| ICAO 9303 Machine Readable Visa | V<UTOERIKSSON<<ANNA<MARIA<<<<<<<<<<<<<<<<<<<<br>L898902C<3UTO6908061F9406236ZE184226B<<<<<<< | 18.538 ns | 0.2393 ns | 0.1998 ns | -         |
|                                 |                                        |           |           |           |           |
| ICAO 9303 Size TD1              | I<UTOD231458907<<<<<<<<<<<<<<<<br>7408122F1204159UTO<<<<<<<<<<<6<br>ERIKSSON<<ANNA<MARIA<<<<<<<<<< | 33.773 ns | 0.1454 ns | 0.1289 ns | -         |
| ICAO 9303 Size TD1              | I<UTOSTARWARS45<<<<<<<<<<<<<<<<br>7705256M2405252UTO<<<<<<<<<<<4<br>SKYWALKER<<LUKE<<<<<<<<<<<<<<< | 38.206 ns | 0.1840 ns | 0.1631 ns | -         |
| ICAO 9303 Size TD1              | I<UTOD23145890<AB112234566<<<<<br>7408122F1204159UTO<<<<<<<<<<<4<br>ERIKSSON<<ANNA<MARIA<<<<<<<<<< | 34.386 ns | 0.2079 ns | 0.1945 ns | -         |
|                                 |                                        |           |           |           |           |
| ICAO 9303 Size TD2              | I<UTOERIKSSON<<ANNA<MARIA<<<<<<<<<<<<br>D231458907UTO7408122F1204159<<<<<<<6 | 31.622 ns | 0.2665 ns | 0.2492 ns | -         |
| ICAO 9303 Size TD2              | I<UTOQWERTY<<ASDF<<<<<<<<<<<<<<<<<<<<br>D23145890<UTO7408122F1204159AB1124<4 | 31.700 ns | 0.2532 ns | 0.2368 ns | -         |
| ICAO 9303 Size TD2              | I<UTOSKYWALKER<<LUKE<<<<<<<<<<<<<<<<<br>STARWARS45UTO7705256M2405252<<<<<<<8 | 31.443 ns | 0.1810 ns | 0.1604 ns | -         |
|                                 |                                        |           |           |           |           |
| ICAO 9303 Size TD3              | P<UTOERIKSSON<<ANNA<MARIA<<<<<<<<<<<<<<<<<<<<br>L898902C36UTO7408122F1204159ZE184226B<<<<<10 | 41.579 ns | 0.2136 ns | 0.1998 ns | -         |
| ICAO 9303 Size TD3              | P<UTOQWERTY<<ASDF<<<<<<<<<<<<<<<<<<<<<<<<<<<<br>Q123987655UTO3311226F2010201<<<<<<<<<<<<<<06 | 44.731 ns | 0.1975 ns | 0.1847 ns | -         |
| ICAO 9303 Size TD3              | P<UTOSKYWALKER<<LUKE<<<<<<<<<<<<<<<<<<<<<<<<<br>STARWARS45UTO7705256M2405252HAN<SHOT<FIRST78 | 44.764 ns | 0.2196 ns | 0.1947 ns | -         |
|                                 |                                        |           |           |           |           |
| ISAN                            | C594660A8B2E5D22X6DDA3272E             | 18.823 ns | 0.2636 ns | 0.2336 ns | -         |
| ISAN                            | D02C42E954183EE2Q1291C8AEO             | 17.367 ns | 0.1634 ns | 0.1364 ns | -         |
| ISAN                            | E9530C32BC0EE83B269867B20F             | 16.964 ns | 0.1627 ns | 0.1442 ns | -         |
|                                 |                                        |           |           |           |           |
| ISAN (Formatted)                | ISAN C594-660A-8B2E-5D22-X             | 15.646 ns | 0.1663 ns | 0.1474 ns | -         |
| ISAN (Formatted)                | ISAN D02C-42E9-5418-3EE2-Q             | 14.682 ns | 0.1183 ns | 0.1049 ns | -         |
| ISAN (Formatted)                | ISAN E953-0C32-BC0E-E83B-2             | 15.727 ns | 0.1711 ns | 0.1516 ns | -         |
| ISAN (Formatted)                | ISAN C594-660A-8B2E-5D22-X-6DDA-3272-E | 24.230 ns | 0.2041 ns | 0.1909 ns | -         |
| ISAN (Formatted)                | ISAN D02C-42E9-5418-3EE2-Q-1291-C8AE-O | 25.131 ns | 0.1821 ns | 0.1614 ns | -         |
| ISAN (Formatted)                | ISAN E953-0C32-BC0E-E83B-2-6986-7B20-F | 25.201 ns | 0.1258 ns | 0.1051 ns | -         |
|                                 |                                        |           |           |           |           |
| ISIN                            | AU0000XVGZA3                           | 7.816 ns  | 0.0852 ns | 0.0797 ns | -         |
| ISIN                            | GB0002634946                           | 7.346 ns  | 0.0458 ns | 0.0428 ns | -         |
| ISIN                            | US0378331005                           | 7.348 ns  | 0.0563 ns | 0.0526 ns | -         |
|                                 |                                        |           |           |           |           |
| ISO 6346                        | CSQU3054383                            | 7.957 ns  | 0.0592 ns | 0.0554 ns | -         |
| ISO 6346                        | MSKU9070323                            | 7.953 ns  | 0.0625 ns | 0.0554 ns | -         |
| ISO 6346                        | TOLU4734787                            | 7.950 ns  | 0.0467 ns | 0.0437 ns | -         |
|                                 |                                        |           |           |           |           |
| NHS                             | 4505577104                             | 4.584 ns  | 0.0319 ns | 0.0298 ns | -         |
| NHS                             | 5301194917                             | 4.577 ns  | 0.0302 ns | 0.0268 ns | -         |
| NHS                             | 9434765919                             | 4.574 ns  | 0.0262 ns | 0.0245 ns | -         |
|                                 |                                        |           |           |           |           |
| NPI                             | 1122337797                             | 6.099 ns  | 0.0248 ns | 0.0232 ns | -         |
| NPI                             | 1234567893                             | 6.748 ns  | 0.0648 ns | 0.0574 ns | -         |
| NPI                             | 1245319599                             | 6.038 ns  | 0.0320 ns | 0.0267 ns | -         |
|                                 |                                        |           |           |           |           |
| SEDOL                           | 3134865                                | 5.849 ns  | 0.0271 ns | 0.0240 ns | -         |
| SEDOL                           | B0YQ5W0                                | 5.872 ns  | 0.0328 ns | 0.0291 ns | -         |
| SEDOL                           | BRDVMH9                                | 5.860 ns  | 0.0361 ns | 0.0302 ns | -         |
|                                 |                                        |           |           |           |           |
| VIN                             | 1G8ZG127XWZ157259                      | 13.278 ns | 0.0645 ns | 0.0538 ns | -         |
| VIN                             | 1HGEM21292L047875                      | 13.297 ns | 0.0520 ns | 0.0434 ns | -         |
| VIN                             | 1M8GDM9AXKP042788                      | 12.869 ns | 0.0624 ns | 0.0521 ns | -         |

### Validate Method (with ICheckDigitMask)

The following implementation of ICheckDigitMask is used for the benchmarks:
```C#
public class GroupsOfThreeCheckDigitMask : ICheckDigitMask
{
   public Boolean ExcludeCharacter(Int32 index) => (index + 1) % 4 == 0;

   public Boolean IncludeCharacter(Int32 index) => (index + 1) % 4 != 0;
}
```

#### General Numeric Algorithms

| Algorithm Name        | Value                         | Mean      | Error     | StdDev    | Allocated |
|-----------------------|-------------------------------|----------:|-----------|-----------|-----------|
| Luhn                  | 140 4                         | 4.647 ns  | 0.0278 ns | 0.0232 ns | -         |
| Luhn                  | 140 662 8                     | 6.048 ns  | 0.0490 ns | 0.0458 ns | -         |
| Luhn                  | 140 662 538 2                 | 8.229 ns  | 0.0595 ns | 0.0528 ns | -         |
| Luhn                  | 140 662 538 042 1             | 9.500 ns  | 0.0665 ns | 0.0622 ns | -         |
| Luhn                  | 140 662 538 042 551 4         | 11.272 ns | 0.0564 ns | 0.0528 ns | -         |
| Luhn                  | 140 662 538 042 551 028 5     | 13.034 ns | 0.0692 ns | 0.0613 ns | -         |
| Luhn                  | 140 662 538 042 551 028 265 1 | 14.754 ns | 0.1212 ns | 0.1075 ns | -         |
|                       |                               |           |           |           |           |
| Modulus10_13          | 140 3                         |  4.536 ns | 0.0544 ns | 0.0509 ns | -         |
| Modulus10_13          | 140 662 7                     |  5.908 ns | 0.1229 ns | 0.1090 ns | -         |
| Modulus10_13          | 140 662 538 5                 |  7.526 ns | 0.0957 ns | 0.0895 ns | -         |
| Modulus10_13          | 140 662 538 042 5             |  9.083 ns | 0.1075 ns | 0.0898 ns | -         |
| Modulus10_13          | 140 662 538 042 551 8         | 10.977 ns | 0.1084 ns | 0.1014 ns | -         |
| Modulus10_13          | 140 662 538 042 551 028 8     | 12.475 ns | 0.1031 ns | 0.0965 ns | -         |
| Modulus10_13          | 140 662 538 042 551 028 265 7 | 14.315 ns | 0.1808 ns | 0.1691 ns | -         |
|                       |                               |           |           |           |           |
| Modulus11_27Decimal   | 140 6                         |  4.353 ns | 0.0255 ns | 0.0213 ns | -         |
| Modulus11_27Decimal   | 140 662 0                     |  6.006 ns | 0.0760 ns | 0.0711 ns | -         |
| Modulus11_27Decimal   | 140 662 538 5                 |  7.363 ns | 0.0624 ns | 0.0521 ns | -         |
| Modulus11_27Decimal   | 140 662 538 042 1             |  9.900 ns | 0.2009 ns | 0.1880 ns | -         |
| Modulus11_27Decimal   | 140 662 538 042 551 0         | 11.595 ns | 0.0500 ns | 0.0417 ns | -         |
| Modulus11_27Decimal   | 140 662 538 042 551 028 8     | 12.468 ns | 0.1190 ns | 0.1055 ns | -         |
| Modulus11_27Decimal   | 140 662 538 042 551 028 265 0 | 14.361 ns | 0.0864 ns | 0.0808 ns | -         |
|                       |                               |           |           |           |           |
| Modulus11_27Extended  | 140 6                         |  4.445 ns | 0.0202 ns | 0.0179 ns | -         |
| Modulus11_27Extended  | 140 662 0                     |  5.954 ns | 0.0248 ns | 0.0220 ns | -         |
| Modulus11_27Extended  | 140 662 538 5                 |  7.632 ns | 0.0815 ns | 0.0723 ns | -         |
| Modulus11_27Extended  | 140 662 538 042 1             |  9.070 ns | 0.0376 ns | 0.0352 ns | -         |
| Modulus11_27Extended  | 140 662 538 042 551 0         | 10.724 ns | 0.0461 ns | 0.0385 ns | -         |
| Modulus11_27Extended  | 140 662 538 042 551 028 8     | 12.649 ns | 0.2177 ns | 0.2036 ns | -         |
| Modulus11_27Extended  | 140 662 538 042 551 028 265 0 | 14.339 ns | 0.2057 ns | 0.1824 ns | -         |
|                       |                               |           |           |           |           |
| Modulus11Decimal      | 140 6                         |  4.200 ns | 0.0472 ns | 0.0442 ns | -         |
| Modulus11Decimal      | 140 662 0                     |  5.134 ns | 0.0596 ns | 0.0529 ns | -         |
| Modulus11Decimal      | 140 662 538 8                 |  6.342 ns | 0.0429 ns | 0.0401 ns | -         |
|                       |                               |           |           |           |           |
| Modulus11Extended     | 140 6                         |  4.317 ns | 0.0073 ns | 0.0068 ns | -         |
| Modulus11Extended     | 140 662 0                     |  5.268 ns | 0.0358 ns | 0.0335 ns | -         |
| Modulus11Extended     | 140 662 538 8                 |  6.603 ns | 0.0696 ns | 0.0651 ns | -         |

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

Initial release. Additional included algorithms:
* ISO/IEC 7064 MOD 11,10
* ISO/IEC 7064 MOD 11-2
* ISO/IEC 7064 MOD 1271-36
* ISO/IEC 7064 MOD 27,26
* ISO/IEC 7064 MOD 37-2
* ISO/IEC 7064 MOD 37,36
* ISO/IEC 7064 MOD 661-26
* ISO/IEC 7064 MOD 97-10

## v1.1.0

Additional included algorithms:
* AlphanumericMod97_10Algorithm
* IbanAlgorithm
* IsanAlgorithm (including ValidateFormatted method)
* NcdAlgorithm (NOID Check Digit)
 
Performance increases for:
* ISO/IEC 7064 MOD 1271-36, Validate method ~18% improvement
* ISO/IEC 7064 MOD 37-2, Validate method ~17% improvement, TryCalculateCheckDigit method ~20% improvement
* ISO/IEC 7064 MOD 37-36, Validate method ~18% improvement, TryCalculateCheckDigit method ~21% improvement

## v2.0.0

Updated to .Net 8.0

Average performance improvement for .Net 8.0 across all algorithms:
  Validate method ~8% improvement, TryCalculateCheckDigit method ~4.9% improvement

Detailed benchmark results for .Net 7 vs .Net 8 located at https://github.com/KnowledgeForwardSolutions/CheckDigits.Net/blob/main/Documentation/DotNet7_DotNet8_PerformanceComparision.md

## v2.1.0

Additional included algorithms:
* CUSIP Algorithm
* ISO 6346 Algorithm
* SEDOL Algorithm

Performance increases for:
* Luhn Algorithm, Validate method ~15% improvement over .Net 7, TryCalculateCheckDigit method ~27% improvement over .Net 7
  (Luhn algorithm originally saw a slight performance decrease when switching from .Net 7 to .Net 8. This release addresses that performance decrease.) 
* Damm Algorithm, Validate and TryCalculateCheckDigit methods ~30% improvement
* Verhoeff Algorithm, Validate method ~20% improvement, TryCalculateCheckDigit method ~30% improvement

## v2.2.0

Support for netstandard2.0

Thanks to Steff Beckers for this addition

## v2.3.0

Additional included algorithms:
* FIGI Algorithm
* ICAO Algorithm
* ICAO 9303 Document Size TD1 Algorithm
* ICAO 9303 Document Size TD2 Algorithm
* ICAO 9303 Document Size TD3 Algorithm
* ICAO 9303 Machine Readable Visa Algorithm

Performance increases for:
* AlphanumericMod97_10Algorithm, Validate method ~15% improvement, TryCalculateCheckDigits method ~8% improvement
* IBAN Algorithm, Validate method ~8% improvement
* ISIN algorithm, ~9% improvement for Validate and TryCalculateCheckDigit methods
* ISO/IEC 7064 MOD 1271-36, TryCalculateCheckDigits method ~8% improvement
* NcdAlgorithm (NOID Check Digit), minimum 10% improvement for Validate method, improvement increases with length of value.
* NpiAlgorithm, Validate method ~8% improvement

## v2.3.1

Documentation update. Fix several minor documentation errors in README file. No code changes.

## v3.0.0

Updated to .Net 10.0

Average performance improvement for .Net 10.0 across all algorithms is ~4% for 
both Validate and TryCalculateCheckDigit methods.

Detailed benchmark results for .Net 8 vs .Net 10 located at https://github.com/KnowledgeForwardSolutions/CheckDigits.Net/blob/main/Documentation/DotNet8_DotNet10_PerformanceComparision.md

Added masked validation support for algorithms via ICheckDigitMask and IMaskedCheckDigitAlgorithm interfaces. Algorithms that implement IMaskedCheckDigitAlgorithm:
* Luhn Algorithm

## v3.1.0

Additional included algorithms:
* Modulus11Decimal Algorithm
* Modulus11Extended Algorithm
* Modulus11_27Decimal Algorithm
* Modulus11_27Extended Algorithm

Added masked validation support to the following algorithms:
* Modulus10_13 Algorithm

Minor updates to the following algorithms:
* ICAO 9303 Document Size TD1 Algorithm
* ICAO 9303 Document Size TD2 Algorithm
* ICAO 9303 Document Size TD3 Algorithm
* ICAO 9303 Machine Readable Visa Algorithm

The original implementation of the above algorithms allowed alphabetic characters in the date of birth/expiration date fields
as long as the check digit(s) were valid for the characters used. The updated implementation only allows numeric characters 
in those fields, which is consistent with the ICAO 9303 specification. 

Additionally, the LineSeparator property of the above algorithms was marked Obsolete and will be removed in a future release.
Instead, the separator used between lines in the MRZ is inferred from the length of the value being validated. 

Algorithms marked as Obsolete:
* Modulus11 Algorithm (ISBN-10/ISSN/etc.) - Replaced by the Modulus11Extended algorithm.
* NHS (UK National Health Service) Algorithm - Replaced by the Modulus11_27Decimal algorithm.
