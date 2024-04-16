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
- **[Algorithm Descriptions](#algorithm-descriptions)**
    * [ABA RTN (Routing Transit Number) Algorithm](#aba-rtn-algorithm)
    * [Alphanumeric MOD 97-10 Algorithm](#alphanumeric-mod-97-10-algorithm)
    * [CUSIP Algorithm](#cusip-algorithm)
    * [Damm Algorithm](#damm-algorithm)
    * [IBAN (International Bank Account Number) Algorithm](#iban-algorithm)
    * [ICAO 9303 Algorithm](#icao-9303-algorithm)
    * [ICAO 9303 Document Size TD3 Algorithm](#icao-9303-document-size-td3-algorithm)
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
* [CUSIP Algorithm](#cusip-algorithm)
* [Damm Algorithm](#damm-algorithm)
* [IBAN (International Bank Account Number) Algorithm](#iban-algorithm)
* [ICAO 9303 Algorithm](#icao-9303-algorithm)
* [ICAO 9303 Document Size TD3 Algorithm](#icao-9303-document-size-td3-algorithm)
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
* [NHS (UK National Health Service) Algorithm](#nhs-algorithm)
* [NOID Check Digit Algorithm](#noid-check-digit-algorithm)
* [NPI (US National Provider Identifier) Algorithm](#npi-algorithm)
* [SEDOL Algorithm](#sedol-algorithm)
* [Verhoeff Algorithm](#verhoeff-algorithm)
* [VIN (Vehicle Identification Number) Algorithm](#vin-algorithm)

## Value/Identifier Types and Associated Algorithms

| Value/Identifier Type | Algorithm |
| --------------------- | ----------|
| ABA Routing Transit Number | [ABA RTN Algorithm](#aba-rtn-algorithm) |
| CA Social Insurance Number | [Luhn Algorithm](#luhn-algorithm) |
| CAS Registry Number   | [Modulus10_1 Algorithm](#modulus10_1-algorithm) |
| Credit card number    | [Luhn Algorithm](#luhn-algorithm) |
| CUSIP                 | [CUSIP Algorithm](#cusip-algorithm) |
| EAN-8					| [Modulus10_13 Algorithm](#modulus10_13-algorithm) |
| EAN-13				| [Modulus10_13 Algorithm](#modulus10_13-algorithm) |
| Global Release Identifier | [ISO/IEC 7064 MOD 37-36 Algorithm](#isoiec-7064-mod-3736-algorithm) |
| GTIN-8				| [Modulus10_13 Algorithm](#modulus10_13-algorithm) |
| GTIN-12				| [Modulus10_13 Algorithm](#modulus10_13-algorithm) |
| GTIN-13				| [Modulus10_13 Algorithm](#modulus10_13-algorithm) |
| GTIN-14				| [Modulus10_13 Algorithm](#modulus10_13-algorithm) |
| IBAN                  | [IBAN Algorithm](#iban-algorithm) |
| ICAO Machine Readable Travel Document Field | [ICAO 9303 Algorithm](#icao-9303-algorithm) |
| ICAO Machine Readable Passports and Size TD3 Documents | [ICAO 9303 Document Size TD3 Algorithm](#icao-9303-document-size-td3-algorithm) |
| IMEI				    | [Luhn Algorithm](#luhn-algorithm) |
| IMO Number            | [Modulus10_2 Algorithm](#modulus10_2-algorithm) |
| ISAN                  | [ISAN Algorithm](#isan-algorithm) |
| ISBN-10				| [Modulus11 Algorithm](#modulus11-algorithm) |
| ISBN-13				| [Modulus10_13 Algorithm](#modulus10_13-algorithm) |
| ISBT Donation Identification Number |  [ISO/IEC 7064 MOD 37-2 Algorithm](#isoiec-7064-mod-37-2-algorithm) |
| ISIN                  | [ISIN Algorithm](#isin-algorithm) |
| ISMN					| [Modulus10_13 Algorithm](#modulus10_13-algorithm) |
| ISNI                  | [ISO/IEC 7064 MOD 11-2 Algorithm](#isoiec-7064-mod-11-2-algorithm) |
| ISSN   				| [Modulus11 Algorithm](#modulus11-algorithm) |
| Legal Entity Identifier | [Alphanumeric MOD 97-10 Algorithm](#alphanumeric-mod-97-10-algorithm) |
| SEDOL					| [SEDOL Algorithm](#sedol-algorithm) |
| Shipping Container Number | [ISO 6346 Algorithm](#iso-6346-algorithm) |
| SSCC					| [Modulus10_13 Algorithm](#modulus10_13-algorithm) |
| Universal Loan Identifier | [Alphanumeric MOD 97-10 Algorithm](#alphanumeric-mod-97-10-algorithm) |
| UK National Health Service Number | [NHS Algorithm](#nhs-algorithm) |
| UPC-A					| [Modulus10_13 Algorithm](#modulus10_13-algorithm) |
| UPC-E					| [Modulus10_13 Algorithm](#modulus10_13-algorithm) |
| US National Provider Identifier | [NPI Algorithm](#npi-algorithm) |
| Vehicle Identification Number | [VIN Algorithm](#vin-algorithm) |

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

The ```IEmbeddedCheckDigitAlgorithm``` interface is used by algorithms that can
expect the value to check and its associated check digit(s) to be embedded within
a larger string. (For example, the date of birth field in an ICAO 9303 machine
readable passport string.) The Validate method defined by ```IEmbeddedCheckDigitAlgorithm```
includes two additional parameters, start and length which specify the substring
within the larger string that contains the value to check. An algorithm can 
implement both ```ICheckDigitAlgorithm``` and ```IEmbeddedCheckDigitAlgorithm```
and have two overloads for the Validate method.

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

### CUSIP Algorithm

#### Description

The CUSIP (Committee on Uniform Security Identification Procedures) algorithm is 
used for nine character alphanumeric codes that identify North American financial
securities. The algorithm has similarities with both the Luhn algorithm and the 
ISIN algorithm.

The CUSIP algorithm only supports validation of check digits and does support 
calculation of check digits.

#### Details

* Valid characters - alphanumeric characters ('0' - '9', 'A' - 'Z') plus '*', '@' and '#'
* Check digit size - one character
* Check digit value - decimal digit ('0' - '9')
* Check digit location - assumed to be the trailing (right-most) character when validating
* Class name - CusipAlgorithm

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

The ICAO 9303 algorithm also implements the ```IEmbeddedCheckDigitAlgorithm interface```
which supports the validation of fields that are embedded within a larger string.

#### Details

* Valid characters - decimal digits ('0' - '9'), upper case letters ('A' - 'Z') and a filler character ('<').
* Check digit size - one character
* Check digit value - decimal digit ('0' - '9')
* Check digit location - assumed to be the trailing (right-most) character when validating
* Class name - Icao9303Algorithm

#### Links

https://en.wikipedia.org/wiki/Machine-readable_passport#Official_travel_documents
https://www.icao.int/publications/Documents/9303_p3_cons_en.pdf

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
data concatenated together. You can specify how the two lines are separated in the
concatenated value by setting the LineSeparator property of the algorithm class.
The three values are None (no line separator is used and the 45h character of the
value is the first character of the second line), Crlf (the Windows line separator,
carriage return followed by line feed) and Lf (the Unix line separator, line feed
is used). The default LineSeparator is None.

The ICAO 9303 Document Size TD3 Algorithm will validate the check digits of the
four fields (passport number, date of birth, date of expiry and optional personal
number) as well as the composite check digit. If any of the check digits fail 
validation then the Validate method will return ```false```.

#### Details

* Valid characters - decimal digits ('0' - '9'), upper case letters ('A' - 'Z') and a filler character ('<').
* Check digit size - one character
* Check digit value - decimal digit ('0' - '9')
* Check digit location - trailing (right-most) character of individual fields, trailing character of entire string for composite check digit
* Class name - Icao9303SizeTD3Algorithm

#### Links

https://www.icao.int/publications/Documents/9303_p4_cons_en.pdf

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
* Class name - IsanAlgorithm

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
* Class name - IsinAlgorithm

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
* Class name - Iso6346Algorithm

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
* Class name - NcdAlgorithm

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

### SEDOL Algorithm

#### Description

The SEDOL (Stock Exchange Daily Official List) algorithm is used for seven 
character alphanumeric codes that identify financial securities in the United
Kingdom and Ireland.

The SEDOL algorithm only supports validation of check digits and does support 
calculation of check digits.

#### Details

* Valid characters - alphanumeric characters, excluding vowels ('0' - '9', 'BCDFGHJKLMNPQRSTVWXYZ')
* Check digit size - one character
* Check digit value - decimal digit ('0' - '9')
* Check digit location - assumed to be the trailing (right-most) character when validating
* Value length - 7 characters
* Class name - SedolAlgorithm

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

## Benchmarks (.Net 8)

The methodology for the general algorithms is to generate values for the benchmarks
by taking substrings of lengths 3, 6, 9, etc. from the same randomly generated 
source string. For the TryCalculateCheckDigit or TryCalculateCheckDigits methods 
the substring is used as is. For the Validate method benchmarks the substring is 
appended with the check character or characters that make the test value valid 
for the algorithm being benchmarked.

For value specific algorithms, three separate values that are valid for the 
algorithm being benchmarked are used.

Previous .Net 7 benchmarks available at https://github.com/KnowledgeForwardSolutions/CheckDigits.Net/blob/main/Documentation/DotNet7Benchmarks.md

#### Benchmark Details

BenchmarkDotNet v0.13.10, Windows 11 (10.0.22621.2715/22H2/2022Update/SunValley2)
Intel Core i7-8700K CPU 3.70GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET SDK 8.0.100
  [Host]     : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2

### TryCalculateCheckDigit/TryCalculateCheckDigits Methods

#### General Numeric Algorithms

Note that the Modulus10_1, Modulus10_2 and Modulus11 algorithms have a maximum 
length of 10 (including the check digit) for values being validated so their
benchmarks do not cover lengths greater than 10.

| Algorithm Name    | Value                 | Mean      | Error     | StdDev    | Allocated |
|------------------ |---------------------- |----------:|----------:|----------:|----------:|
| Damm              | 140                   |  4.525 ns | 0.0571 ns | 0.0506 ns |         - |
| Damm              | 140662                |  5.477 ns | 0.0170 ns | 0.0142 ns |         - |
| Damm              | 140662538             |  8.315 ns | 0.0803 ns | 0.0712 ns |         - |
| Damm              | 140662538042          | 11.993 ns | 0.0355 ns | 0.0332 ns |         - |
| Damm              | 140662538042551       | 16.354 ns | 0.3389 ns | 0.3170 ns |         - |
| Damm              | 140662538042551028    | 19.487 ns | 0.0783 ns | 0.0654 ns |         - |
| Damm              | 140662538042551028265 | 23.192 ns | 0.0936 ns | 0.0781 ns |         - |
|                   |                       |           |           |           |           |                                           
| ISO/IEC 706 11,10 | 140                   |  6.355 ns | 0.0509 ns | 0.0476 ns |         - |
| ISO/IEC 706 11,10 | 140662                | 10.266 ns | 0.0631 ns | 0.0590 ns |         - |
| ISO/IEC 706 11,10 | 140662538             | 12.386 ns | 0.0982 ns | 0.0919 ns |         - |
| ISO/IEC 706 11,10 | 140662538042          | 15.053 ns | 0.1208 ns | 0.1130 ns |         - |
| ISO/IEC 706 11,10 | 140662538042551       | 18.437 ns | 0.1473 ns | 0.1378 ns |         - |
| ISO/IEC 706 11,10 | 140662538042551028    | 22.820 ns | 0.1971 ns | 0.1843 ns |         - |
| ISO/IEC 706 11,10 | 140662538042551028265 | 26.027 ns | 0.1479 ns | 0.1384 ns |         - |
|                   |                       |           |           |           |           |                                           
| ISO/IEC 706 11-2  | 140                   |  4.241 ns | 0.0141 ns | 0.0132 ns |         - |
| ISO/IEC 706 11-2  | 140662                |  8.603 ns | 0.0292 ns | 0.0259 ns |         - |
| ISO/IEC 706 11-2  | 140662538             | 11.325 ns | 0.0451 ns | 0.0400 ns |         - |
| ISO/IEC 706 11-2  | 140662538042          | 14.259 ns | 0.0477 ns | 0.0423 ns |         - |
| ISO/IEC 706 11-2  | 140662538042551       | 16.991 ns | 0.1129 ns | 0.1000 ns |         - |
| ISO/IEC 706 11-2  | 140662538042551028    | 14.592 ns | 0.0717 ns | 0.0636 ns |         - |
| ISO/IEC 706 11-2  | 140662538042551028265 | 22.463 ns | 0.1754 ns | 0.1555 ns |         - |
|                   |                       |           |           |           |           |                                           
| ISO/IEC 706 97-10 | 140                   |  6.887 ns | 0.0739 ns | 0.0692 ns |         - |
| ISO/IEC 706 97-10 | 140662                | 10.281 ns | 0.1422 ns | 0.1330 ns |         - |
| ISO/IEC 706 97-10 | 140662538             | 13.230 ns | 0.1022 ns | 0.0956 ns |         - |
| ISO/IEC 706 97-10 | 140662538042          | 16.044 ns | 0.1452 ns | 0.1358 ns |         - |
| ISO/IEC 706 97-10 | 140662538042551       | 18.855 ns | 0.1708 ns | 0.1426 ns |         - |
| ISO/IEC 706 97-10 | 140662538042551028    | 22.542 ns | 0.2155 ns | 0.2016 ns |         - |
| ISO/IEC 706 97-10 | 140662538042551028265 | 25.380 ns | 0.2038 ns | 0.1906 ns |         - |
|                   |                       |           |           |           |           |                                           
| Luhn              | 140                   |  6.674 ns | 0.1106 ns | 0.1035 ns |         - |
| Luhn              | 140662                |  9.396 ns | 0.0575 ns | 0.0538 ns |         - |
| Luhn              | 140662538             | 14.913 ns | 0.0464 ns | 0.0434 ns |         - |
| Luhn              | 140662538042          | 14.981 ns | 0.0720 ns | 0.0638 ns |         - |
| Luhn              | 140662538042551       | 20.813 ns | 0.1534 ns | 0.1435 ns |         - |
| Luhn              | 140662538042551028    | 22.434 ns | 0.1459 ns | 0.1365 ns |         - |
| Luhn              | 140662538042551028265 | 27.432 ns | 0.1217 ns | 0.1138 ns |         - |
|                   |                       |           |           |           |           |                                           
| Modulus10_13      | 140                   |  4.845 ns | 0.0532 ns | 0.0498 ns |         - |
| Modulus10_13      | 140662                |  8.806 ns | 0.1316 ns | 0.1167 ns |         - |
| Modulus10_13      | 140662538             | 11.743 ns | 0.1881 ns | 0.1760 ns |         - |
| Modulus10_13      | 140662538042          | 12.224 ns | 0.0869 ns | 0.0813 ns |         - |
| Modulus10_13      | 140662538042551       | 17.971 ns | 0.1486 ns | 0.1317 ns |         - |
| Modulus10_13      | 140662538042551028    | 21.347 ns | 0.1666 ns | 0.1558 ns |         - |
| Modulus10_13      | 140662538042551028265 | 24.085 ns | 0.1882 ns | 0.1761 ns |         - |
|                   |                       |           |           |           |           |                                           
| Modulus10_1       | 140                   |  3.865 ns | 0.0509 ns | 0.0476 ns |         - |
| Modulus10_1       | 140662                |  5.566 ns | 0.0775 ns | 0.0725 ns |         - |
| Modulus10_1       | 140662538             |  7.337 ns | 0.0871 ns | 0.0815 ns |         - |
|                   |                       |           |           |           |           |                                           
| Modulus10_2       | 140                   |  4.541 ns | 0.0420 ns | 0.0372 ns |         - |
| Modulus10_2       | 140662                |  6.142 ns | 0.0614 ns | 0.0513 ns |         - |
| Modulus10_2       | 140662538             |  7.874 ns | 0.0784 ns | 0.0733 ns |         - |
|                   |                       |           |           |           |           |                                           
| Modulus11         | 140                   |  6.740 ns | 0.0600 ns | 0.0562 ns |         - |
| Modulus11         | 140662                | 10.089 ns | 0.0851 ns | 0.0796 ns |         - |
| Modulus11         | 140662538             | 13.288 ns | 0.0696 ns | 0.0651 ns |         - |
|                   |                       |           |           |           |           |                                           
| Verhoeff          | 140                   |  8.358 ns | 0.1062 ns | 0.0941 ns |         - |
| Verhoeff          | 140662                | 12.916 ns | 0.0614 ns | 0.0544 ns |         - |
| Verhoeff          | 140662538             | 17.835 ns | 0.1126 ns | 0.0998 ns |         - |
| Verhoeff          | 140662538042          | 22.727 ns | 0.1362 ns | 0.1274 ns |         - |
| Verhoeff          | 140662538042551       | 27.473 ns | 0.1085 ns | 0.0961 ns |         - |
| Verhoeff          | 140662538042551028    | 32.246 ns | 0.1009 ns | 0.0842 ns |         - |
| Verhoeff          | 140662538042551028265 | 37.262 ns | 0.1306 ns | 0.1090 ns |         - |

#### General Alphabetic Algorithms

| Algorithm Name          | Value                 | Mean      | Error     | StdDev    | Allocated |
|------------------------ |---------------------- |----------:|----------:|----------:|----------:|
| ISO/IEC 7064 MOD 27,26  | EGR                   |  6.915 ns | 0.0760 ns | 0.0711 ns |         - |
| ISO/IEC 7064 MOD 27,26  | EGRNML                |  9.448 ns | 0.0751 ns | 0.0627 ns |         - |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOC             | 14.985 ns | 0.0798 ns | 0.0707 ns |         - |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOCECU          | 14.329 ns | 0.0699 ns | 0.0619 ns |         - |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOCECUJIK       | 17.069 ns | 0.0676 ns | 0.0599 ns |         - |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOCECUJIKNWW    | 19.291 ns | 0.0719 ns | 0.0600 ns |         - |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOCECUJIKNWWVVO | 26.171 ns | 0.0983 ns | 0.0871 ns |         - |
|                         |                       |           |           |           |           |                                           
| ISO/IEC 7064 MOD 661-26 | EGR                   |  6.994 ns | 0.0361 ns | 0.0282 ns |         - |
| ISO/IEC 7064 MOD 661-26 | EGRNML                | 10.143 ns | 0.0711 ns | 0.0594 ns |         - |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOC             | 13.182 ns | 0.0760 ns | 0.0674 ns |         - |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOCECU          | 16.399 ns | 0.0647 ns | 0.0605 ns |         - |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOCECUJIK       | 20.300 ns | 0.0949 ns | 0.0887 ns |         - |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOCECUJIKNWW    | 22.779 ns | 0.0896 ns | 0.0838 ns |         - |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOCECUJIKNWWVVO | 27.412 ns | 0.1551 ns | 0.1450 ns |         - |

#### General Alphanumeric Algorithms

Note that the values used for the NOID Check Digit algorithm do not include lengths
3 or 6 so that benchmarks are not run on purely numeric strings.

| Algorithm Name           | Value                 | Mean      | Error     | StdDev    | Allocated |
|------------------------- |---------------------- |----------:|----------:|----------:|----------:|
| AlphanumericMod97_10     | U7y                   | 10.603 ns | 0.0421 ns | 0.0329 ns |         - |
| AlphanumericMod97_10     | U7y8SX                | 18.684 ns | 0.0887 ns | 0.0786 ns |         - |
| AlphanumericMod97_10     | U7y8SXrC0             | 27.203 ns | 0.1398 ns | 0.1239 ns |         - |
| AlphanumericMod97_10     | U7y8SXrC0O3S          | 33.243 ns | 0.1609 ns | 0.1427 ns |         - |
| AlphanumericMod97_10     | U7y8SXrC0O3Sc4I       | 42.317 ns | 0.2219 ns | 0.1853 ns |         - |
| AlphanumericMod97_10     | U7y8SXrC0O3Sc4IHYQ    | 46.321 ns | 0.2685 ns | 0.2242 ns |         - |
| AlphanumericMod97_10     | U7y8SXrC0O3Sc4IHYQF4M | 51.706 ns | 0.1936 ns | 0.1811 ns |         - |
|                          |                       |           |           |           |           |                                           
| ISO/IEC 7064 MOD 1271-36 | U7Y                   |  9.381 ns | 0.0675 ns | 0.0599 ns |         - |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SX                | 13.931 ns | 0.0701 ns | 0.0621 ns |         - |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SXRC0             | 17.402 ns | 0.0820 ns | 0.0727 ns |         - |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SXRC0O3S          | 22.540 ns | 0.0591 ns | 0.0524 ns |         - |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SXRC0O3SC4I       | 27.310 ns | 0.1040 ns | 0.0922 ns |         - |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SXRC0O3SC4IHYQ    | 32.300 ns | 0.1056 ns | 0.0936 ns |         - |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SXRC0O3SC4IHYQF4M | 35.724 ns | 0.1192 ns | 0.0995 ns |         - |
|                          |                       |           |           |           |           |                                           
| ISO/IEC 7064 MOD 37-2    | U7Y                   |  7.391 ns | 0.0347 ns | 0.0324 ns |         - |
| ISO/IEC 7064 MOD 37-2    | U7Y8SX                | 11.461 ns | 0.0774 ns | 0.0724 ns |         - |
| ISO/IEC 7064 MOD 37-2    | U7Y8SXRC0             | 15.736 ns | 0.0734 ns | 0.0686 ns |         - |
| ISO/IEC 7064 MOD 37-2    | U7Y8SXRC0O3S          | 19.804 ns | 0.0717 ns | 0.0671 ns |         - |
| ISO/IEC 7064 MOD 37-2    | U7Y8SXRC0O3SC4I       | 23.759 ns | 0.0894 ns | 0.0836 ns |         - |
| ISO/IEC 7064 MOD 37-2    | U7Y8SXRC0O3SC4IHYQ    | 33.503 ns | 0.1415 ns | 0.1324 ns |         - |
| ISO/IEC 7064 MOD 37-2    | U7Y8SXRC0O3SC4IHYQF4M | 38.716 ns | 0.1444 ns | 0.1280 ns |         - |
|                          |                       |           |           |           |           |                                           
| ISO/IEC 7064 MOD 37,36   | U7Y                   |  7.937 ns | 0.0461 ns | 0.0385 ns |         - |
| ISO/IEC 7064 MOD 37,36   | U7Y8SX                | 12.423 ns | 0.0516 ns | 0.0482 ns |         - |
| ISO/IEC 7064 MOD 37,36   | U7Y8SXRC0             | 16.474 ns | 0.0991 ns | 0.0927 ns |         - |
| ISO/IEC 7064 MOD 37,36   | U7Y8SXRC0O3S          | 20.351 ns | 0.3404 ns | 0.2843 ns |         - |
| ISO/IEC 7064 MOD 37,36   | U7Y8SXRC0O3SC4I       | 24.541 ns | 0.1335 ns | 0.1183 ns |         - |
| ISO/IEC 7064 MOD 37,36   | U7Y8SXRC0O3SC4IHYQ    | 30.263 ns | 0.1079 ns | 0.0956 ns |         - |
| ISO/IEC 7064 MOD 37,36   | U7Y8SXRC0O3SC4IHYQF4M | 35.554 ns | 0.1783 ns | 0.1668 ns |         - |
|                          |                       |           |           |           |           |                                           
| NOID Check Digit         | 11404/2h9             |  8.473 ns | 0.0361 ns | 0.0337 ns |         - |
| NOID Check Digit         | 11404/2h9tqb          | 12.857 ns | 0.0650 ns | 0.0576 ns |         - |
| NOID Check Digit         | 11404/2h9tqbxk6       | 16.108 ns | 0.0807 ns | 0.0755 ns |         - |
| NOID Check Digit         | 11404/2h9tqbxk6rw7    | 19.350 ns | 0.1326 ns | 0.1240 ns |         - |
| NOID Check Digit         | 11404/2h9tqbxk6rw7dwm | 25.295 ns | 0.0684 ns | 0.0571 ns |         - |

#### Value Specific Algorithms

Note: ABA RTN, CUSIP, NHS, NPI and SEDOL algorithms do not support calculation of 
check digits, only validation of values containing check digits.

| Algorithm Name | Value                           | Mean     | Error    | StdDev   | Allocated |
|--------------- |-------------------------------- |---------:|---------:|---------:|----------:|
| IBAN           | BE00096123456769                | 22.20 ns | 0.108 ns | 0.096 ns |         - |
| IBAN           | GB00WEST12345698765432          | 37.50 ns | 0.316 ns | 0.280 ns |         - |
| IBAN           | SC00MCBL01031234567890123456USD | 54.90 ns | 0.559 ns | 0.467 ns |         - |
|                |                                 |          |          |          |           |                                           
| ISIN           | AU0000XVGZA                     | 27.01 ns | 0.071 ns | 0.066 ns |         - |
| ISIN           | GB000263494                     | 20.26 ns | 0.098 ns | 0.091 ns |         - |
| ISIN           | US037833100                     | 19.10 ns | 0.144 ns | 0.135 ns |         - |
|                |                                 |          |          |          |           |                                           
| ISO 6346       | CSQU305438                      | 16.74 ns | 0.216 ns | 0.202 ns |         - |
| ISO 6346       | MSKU907032                      | 16.22 ns | 0.078 ns | 0.069 ns |         - |
| ISO 6346       | TOLU473478                      | 16.22 ns | 0.135 ns | 0.113 ns |         - |
|                |                                 |          |          |          |           |                                           
| VIN            | 1G8ZG127_WZ157259               | 21.46 ns | 0.078 ns | 0.073 ns |         - |
| VIN            | 1HGEM212_2L047875               | 20.74 ns | 0.131 ns | 0.123 ns |         - |
| VIN            | 1M8GDM9A_KP042788               | 20.89 ns | 0.076 ns | 0.071 ns |         - |

### Validate Method

#### General Numeric Algorithms

All algorithms use a single check digit except ISO/IEC 7064 MOD 97-10 which uses
two check digits.

Note that the Modulus10_1, Modulus10_2 and Modulus11 algorithms have a maximum 
length of 10 (including the check digit) for values being validated so their
benchmarks do not cover lengths greater than 10.

| Algorithm Name         | Value                   | Mean      | Error     | StdDev    | Allocated |
|----------------------- | ----------------------- |----------:|----------:|----------:|----------:|
| Damm                   | 1402                    |  3.825 ns | 0.0240 ns | 0.0213 ns |         - |
| Damm                   | 1406622                 |  6.032 ns | 0.0244 ns | 0.0216 ns |         - |
| Damm                   | 1406625388              |  9.435 ns | 0.0801 ns | 0.0750 ns |         - |
| Damm                   | 1406625380422           | 13.104 ns | 0.0813 ns | 0.0760 ns |         - |
| Damm                   | 1406625380425518        | 16.887 ns | 0.1718 ns | 0.1523 ns |         - |
| Damm                   | 1406625380425510280     | 20.558 ns | 0.1472 ns | 0.1377 ns |         - |
| Damm                   | 1406625380425510282654  | 24.074 ns | 0.1599 ns | 0.1495 ns |         - |
|                        |                         |           |           |           |           |                                           
| ISO/IEC 7064 MOD 11,10 | 1409                    |  6.567 ns | 0.0645 ns | 0.0572 ns |         - |
| ISO/IEC 7064 MOD 11,10 | 1406623                 | 11.212 ns | 0.0784 ns | 0.0695 ns |         - |
| ISO/IEC 7064 MOD 11,10 | 1406625381              | 14.277 ns | 0.0848 ns | 0.0708 ns |         - |
| ISO/IEC 7064 MOD 11,10 | 1406625380426           | 18.457 ns | 0.0729 ns | 0.0682 ns |         - |
| ISO/IEC 7064 MOD 11,10 | 1406625380425514        | 20.652 ns | 0.0946 ns | 0.0884 ns |         - |
| ISO/IEC 7064 MOD 11,10 | 1406625380425510286     | 25.927 ns | 0.0780 ns | 0.0730 ns |         - |
| ISO/IEC 7064 MOD 11,10 | 1406625380425510282657  | 29.294 ns | 0.1947 ns | 0.1822 ns |         - |
|                        |                         |           |           |           |           |                                           
| ISO/IEC 7064 MOD 11-2  | 140X                    |  5.319 ns | 0.0585 ns | 0.0488 ns |         - |
| ISO/IEC 7064 MOD 11-2  | 1406628                 |  9.415 ns | 0.1067 ns | 0.0998 ns |         - |
| ISO/IEC 7064 MOD 11-2  | 1406625380              | 11.886 ns | 0.0424 ns | 0.0397 ns |         - |
| ISO/IEC 7064 MOD 11-2  | 1406625380426           | 15.054 ns | 0.1373 ns | 0.1284 ns |         - |
| ISO/IEC 7064 MOD 11-2  | 1406625380425511        | 18.343 ns | 0.1774 ns | 0.1482 ns |         - |
| ISO/IEC 7064 MOD 11-2  | 140662538042551028X     | 15.708 ns | 0.1594 ns | 0.1491 ns |         - |
| ISO/IEC 7064 MOD 11-2  | 1406625380425510282651  | 22.860 ns | 0.0759 ns | 0.0634 ns |         - |
|                        |                         |           |           |           |           |                                           
| ISO/IEC 7064 MOD 97-10 | 14066                   |  6.683 ns | 0.0606 ns | 0.0537 ns |         - |
| ISO/IEC 7064 MOD 97-10 | 14066262                |  9.404 ns | 0.0898 ns | 0.0840 ns |         - |
| ISO/IEC 7064 MOD 97-10 | 14066253823             | 12.522 ns | 0.1327 ns | 0.1241 ns |         - |
| ISO/IEC 7064 MOD 97-10 | 14066253804250          | 15.676 ns | 0.1332 ns | 0.1246 ns |         - |
| ISO/IEC 7064 MOD 97-10 | 14066253804255112       | 18.960 ns | 0.2592 ns | 0.2298 ns |         - |
| ISO/IEC 7064 MOD 97-10 | 14066253804255102853    | 22.186 ns | 0.2068 ns | 0.1935 ns |         - |
| ISO/IEC 7064 MOD 97-10 | 14066253804255102826587 | 24.965 ns | 0.5259 ns | 0.4919 ns |         - |
|                        |                         |           |           |           |           |                                           
| Luhn                   | 1404                    |  6.671 ns | 0.0366 ns | 0.0305 ns |         - |
| Luhn                   | 1406628                 |  8.910 ns | 0.0483 ns | 0.0377 ns |         - |
| Luhn                   | 1406625382              | 12.273 ns | 0.0815 ns | 0.0763 ns |         - |
| Luhn                   | 1406625380421           | 14.002 ns | 0.0486 ns | 0.0431 ns |         - |
| Luhn                   | 1406625380425514        | 18.127 ns | 0.2873 ns | 0.2687 ns |         - |
| Luhn                   | 1406625380425510285     | 19.739 ns | 0.1725 ns | 0.1613 ns |         - |
| Luhn                   | 1406625380425510282651  | 23.388 ns | 0.1535 ns | 0.1435 ns |         - |
|                        |                         |           |           |           |           |                                           
| Modulus10_13           | 1403                    |  5.844 ns | 0.0538 ns | 0.0503 ns |         - |
| Modulus10_13           | 1406627                 |  9.622 ns | 0.1128 ns | 0.1055 ns |         - |
| Modulus10_13           | 1406625385              | 12.078 ns | 0.1033 ns | 0.0966 ns |         - |
| Modulus10_13           | 1406625380425           | 16.629 ns | 0.3109 ns | 0.2908 ns |         - |
| Modulus10_13           | 1406625380425518        | 19.143 ns | 0.1654 ns | 0.1547 ns |         - |
| Modulus10_13           | 1406625380425510288     | 18.478 ns | 0.1429 ns | 0.1336 ns |         - |
| Modulus10_13           | 1406625380425510282657  | 26.205 ns | 0.1747 ns | 0.1634 ns |         - |
|                        |                         |           |           |           |           |                                           
| Modulus10_1            | 1401                    |  3.168 ns | 0.0427 ns | 0.0399 ns |         - |
| Modulus10_1            | 1406628                 |  4.805 ns | 0.0789 ns | 0.0738 ns |         - |
| Modulus10_1            | 1406625384              |  7.102 ns | 0.1677 ns | 0.1647 ns |         - |
|                        |                         |           |           |           |           |                                           
| Modulus10_2            | 1406                    |  3.313 ns | 0.0394 ns | 0.0368 ns |         - |
| Modulus10_2            | 1406627                 |  4.892 ns | 0.0527 ns | 0.0493 ns |         - |
| Modulus10_2            | 1406625389              |  6.557 ns | 0.0890 ns | 0.0833 ns |         - |
|                        |                         |           |           |           |           |                                           
| Modulus11              | 1406                    |  5.127 ns | 0.0476 ns | 0.0422 ns |         - |
| Modulus11              | 1406625                 |  6.844 ns | 0.1128 ns | 0.1000 ns |         - |
| Modulus11              | 1406625388              |  8.112 ns | 0.0454 ns | 0.0425 ns |         - |
|                        |                         |           |           |           |           |                                           
| Verhoeff               | 1401                    |  9.365 ns | 0.0523 ns | 0.0489 ns |         - |
| Verhoeff               | 1406625                 | 14.769 ns | 0.0841 ns | 0.0656 ns |         - |
| Verhoeff               | 1406625388              | 20.334 ns | 0.1164 ns | 0.1089 ns |         - |
| Verhoeff               | 1406625380426           | 25.942 ns | 0.1319 ns | 0.1234 ns |         - |
| Verhoeff               | 1406625380425512        | 31.425 ns | 0.1170 ns | 0.0977 ns |         - |
| Verhoeff               | 1406625380425510285     | 36.982 ns | 0.1119 ns | 0.0935 ns |         - |
| Verhoeff               | 1406625380425510282655  | 42.288 ns | 0.1756 ns | 0.1642 ns |         - |


#### General Alphabetic Algorithms

ISO/IEC 7064 MOD 27,26 uses a single check character. ISO/IEC 7064 MOD 661-26
uses two check characters.

| Algorithm Name          | Value                   | Mean      | Error     | StdDev    | Allocated |
|------------------------ |------------------------ |----------:|----------:|----------:|----------:|
| ISO/IEC 7064 MOD 27,26  | EGRS                    |  7.274 ns | 0.0623 ns | 0.0552 ns |         - |
| ISO/IEC 7064 MOD 27,26  | EGRNMLU                 | 10.292 ns | 0.0467 ns | 0.0436 ns |         - |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOCB              | 14.444 ns | 0.0622 ns | 0.0582 ns |         - |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOCECUA           | 18.226 ns | 0.1115 ns | 0.0988 ns |         - |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOCECUJIKA        | 21.802 ns | 0.1181 ns | 0.1047 ns |         - |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOCECUJIKNWWY     | 25.724 ns | 0.0692 ns | 0.0647 ns |         - |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOCECUJIKNWWVVOQ  | 29.716 ns | 0.1309 ns | 0.1224 ns |         - |
|                         |                         |           |           |           |           |                                           
| ISO/IEC 7064 MOD 661-26 | EGRSE                   |  6.263 ns | 0.0179 ns | 0.0167 ns |         - |
| ISO/IEC 7064 MOD 661-26 | EGRNMLDR                | 10.339 ns | 0.0777 ns | 0.0726 ns |         - |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOCCK             | 13.633 ns | 0.0456 ns | 0.0427 ns |         - |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOCECUZJ          | 16.896 ns | 0.0641 ns | 0.0568 ns |         - |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOCECUJIKFQ       | 20.183 ns | 0.0823 ns | 0.0729 ns |         - |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOCECUJIKNWWQN    | 23.412 ns | 0.0979 ns | 0.0915 ns |         - |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOCECUJIKNWWVVORC | 26.679 ns | 0.1163 ns | 0.0971 ns |         - |

#### General Alphanumeric Algorithms

AlphanumericMod97_10 algorithm and ISO/IEC 7064 MOD 1271-36 uses two check characters. 
ISO/IEC 7064 MOD 37-2, ISO/IEC 7064 MOD 37,36 and NOID Check Digit algorithms use a 
single check character.

Note also that the values used for the NOID Check Digit algorithm do not include lengths
3 or 6 so that benchmarks are not run on purely numeric strings.

| Algorithm Name           | Value                   | Mean      | Error     | StdDev    | Allocated |
|------------------------- |-------------------------|----------:|----------:|----------:|----------:|
| AlphanumericMod97_10     | U7y46                   | 10.741 ns | 0.0950 ns | 0.0793 ns |         - |
| AlphanumericMod97_10     | U7y8SX89                | 19.366 ns | 0.1097 ns | 0.0972 ns |         - |
| AlphanumericMod97_10     | U7y8SXrC087             | 28.522 ns | 0.2386 ns | 0.2232 ns |         - |
| AlphanumericMod97_10     | U7y8SXrC0O3S38          | 35.760 ns | 0.1844 ns | 0.1724 ns |         - |
| AlphanumericMod97_10     | U7y8SXrC0O3Sc4I27       | 44.344 ns | 0.0969 ns | 0.0810 ns |         - |
| AlphanumericMod97_10     | U7y8SXrC0O3Sc4IHYQ54    | 49.380 ns | 0.1968 ns | 0.1744 ns |         - |
| AlphanumericMod97_10     | U7y8SXrC0O3Sc4IHYQF4M21 | 57.042 ns | 0.1509 ns | 0.1260 ns |         - |
|                          |                         |           |           |           |           |                                           
| ISO/IEC 7064 MOD 1271-36 | U7YM0                   |  8.068 ns | 0.0306 ns | 0.0271 ns |         - |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SXOR                | 13.625 ns | 0.0857 ns | 0.0760 ns |         - |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SXRC0FI             | 17.588 ns | 0.0977 ns | 0.0914 ns |         - |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SXRC0O3SX4          | 20.950 ns | 0.0567 ns | 0.0503 ns |         - |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SXRC0O3SC4I9D       | 24.208 ns | 0.0451 ns | 0.0400 ns |         - |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SXRC0O3SC4IHYQYI    | 31.207 ns | 0.1850 ns | 0.1731 ns |         - |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SXRC0O3SC4IHYQF4M44 | 33.292 ns | 0.0991 ns | 0.0828 ns |         - |
|                          |                         |           |           |           |           |                                           
| ISO/IEC 7064 MOD 37-2    | U7YZ                    |  6.713 ns | 0.0368 ns | 0.0326 ns |         - |
| ISO/IEC 7064 MOD 37-2    | U7Y8SXV                 | 10.742 ns | 0.0440 ns | 0.0412 ns |         - |
| ISO/IEC 7064 MOD 37-2    | U7Y8SXRC0E              | 14.103 ns | 0.0575 ns | 0.0538 ns |         - |
| ISO/IEC 7064 MOD 37-2    | U7Y8SXRC0O3SU           | 18.156 ns | 0.0843 ns | 0.0747 ns |         - |
| ISO/IEC 7064 MOD 37-2    | U7Y8SXRC0O3SC4IB        | 21.720 ns | 0.0729 ns | 0.0682 ns |         - |
| ISO/IEC 7064 MOD 37-2    | U7Y8SXRC0O3SC4IHYQG     | 26.040 ns | 0.1465 ns | 0.1370 ns |         - |
| ISO/IEC 7064 MOD 37-2    | U7Y8SXRC0O3SC4IHYQF4MF  | 29.839 ns | 0.0916 ns | 0.0812 ns |         - |
|                          |                         |           |           |           |           |                                           
| ISO/IEC 7064 MOD 37,36   | U7YW                    |  8.697 ns | 0.0715 ns | 0.0669 ns |         - |
| ISO/IEC 7064 MOD 37,36   | U7Y8SX8                 | 13.124 ns | 0.0772 ns | 0.0722 ns |         - |
| ISO/IEC 7064 MOD 37,36   | U7Y8SXRC0E              | 17.682 ns | 0.0633 ns | 0.0529 ns |         - |
| ISO/IEC 7064 MOD 37,36   | U7Y8SXRC0O3SR           | 23.219 ns | 0.2057 ns | 0.1924 ns |         - |
| ISO/IEC 7064 MOD 37,36   | U7Y8SXRC0O3SC4IT        | 24.299 ns | 0.1017 ns | 0.0902 ns |         - |
| ISO/IEC 7064 MOD 37,36   | U7Y8SXRC0O3SC4IHYQD     | 27.938 ns | 0.1244 ns | 0.0971 ns |         - |
| ISO/IEC 7064 MOD 37,36   | U7Y8SXRC0O3SC4IHYQF4MP  | 32.631 ns | 0.1183 ns | 0.1049 ns |         - |
|                          |                         |           |           |           |           |                                           
| NOID Check Digit         | 11404/2h9m              | 12.579 ns | 0.0894 ns | 0.0837 ns |         - |
| NOID Check Digit         | 11404/2h9tqb0           | 16.119 ns | 0.0622 ns | 0.0551 ns |         - |
| NOID Check Digit         | 11404/2h9tqbxk6d        | 20.225 ns | 0.0792 ns | 0.0740 ns |         - |
| NOID Check Digit         | 11404/2h9tqbxk6rw74     | 24.573 ns | 0.1188 ns | 0.1111 ns |         - |
| NOID Check Digit         | 11404/2h9tqbxk6rw7dwmz  | 30.319 ns | 0.0998 ns | 0.0833 ns |         - |

#### Value Specific Algorithms

| Algorithm Name       | Value                                  | Mean      | Error     | StdDev    | Allocated |
|--------------------- |--------------------------------------- |----------:|----------:|----------:|----------:|
| ABA RTN              | 111000025                              | 10.830 ns | 0.0650 ns | 0.0580 ns |         - |
| ABA RTN              | 122235821                              | 10.400 ns | 0.1880 ns | 0.1570 ns |         - |
| ABA RTN              | 325081403                              | 10.310 ns | 0.0610 ns | 0.0570 ns |         - |
|                      |                                        |           |           |           |           |                                           
| CUSIP                | 037833100                              | 16.500 ns | 0.1990 ns | 0.1760 ns |         - |
| CUSIP                | 38143VAA7                              | 13.020 ns | 0.0830 ns | 0.0770 ns |         - |
| CUSIP                | 91282CJL6                              | 12.850 ns | 0.0630 ns | 0.0530 ns |         - |
|                      |                                        |           |           |           |           |                                           
| IBAN                 | BE71096123456769                       | 20.090 ns | 0.1710 ns | 0.1600 ns |         - |
| IBAN                 | GB82WEST12345698765432                 | 34.960 ns | 0.2120 ns | 0.1880 ns |         - |
| IBAN                 | SC74MCBL01031234567890123456USD        | 51.580 ns | 0.2410 ns | 0.2130 ns |         - |
|                      |                                        |           |           |           |           |
| ICAO 9303            | U7Y5                                   |  7.365 ns | 0.0740 ns | 0.0656 ns |         - |
| ICAO 9303            | U7Y8SX8                                | 12.722 ns | 0.1308 ns | 0.1223 ns |         - |
| ICAO 9303            | U7Y8SXRC03                             | 17.771 ns | 0.1508 ns | 0.1337 ns |         - |
| ICAO 9303            | U7Y8SXRC0O3S8                          | 23.627 ns | 0.1262 ns | 0.1119 ns |         - |
| ICAO 9303            | U7Y8SXRC0O3SC4I2                       | 27.348 ns | 0.2444 ns | 0.2286 ns |         - |
| ICAO 9303            | U7Y8SXRC0O3SC4IHYQ9                    | 32.199 ns | 0.3203 ns | 0.2996 ns |         - |
| ICAO 9303            | U7Y8SXRC0O3SC4IHYQF4M8                 | 38.621 ns | 0.2301 ns | 0.2040 ns |         - |
|                      |                                        |           |           |           |           |
| ICAO 9303 (Embedded) | +U7Y5+                                 |  8.334 ns | 0.1706 ns | 0.1512 ns |         - |
| ICAO 9303 (Embedded) | +U7Y8SX8+                              | 12.468 ns | 0.1040 ns | 0.0973 ns |         - |
| ICAO 9303 (Embedded) | +U7Y8SXRC03+                           | 15.320 ns | 0.0898 ns | 0.0750 ns |         - |
| ICAO 9303 (Embedded) | +U7Y8SXRC0O3S8+                        | 19.111 ns | 0.1357 ns | 0.1270 ns |         - |
| ICAO 9303 (Embedded) | +U7Y8SXRC0O3SC4I2+                     | 22.239 ns | 0.0915 ns | 0.0811 ns |         - |
| ICAO 9303 (Embedded) | +U7Y8SXRC0O3SC4IHYQ9+                  | 25.685 ns | 0.2428 ns | 0.2271 ns |         - |
| ICAO 9303 (Embedded) | +U7Y8SXRC0O3SC4IHYQF4M8+               | 29.081 ns | 0.3835 ns | 0.3587 ns |         - |
|                      |                                        |           |           |           |           |
| IACO 9303 Size TD3   | P<UTOERIKSSON<<ANNA<MARIA<<<<<<<<<<<<<<<<<<<<br>L898902C36UTO7408122F1204159ZE184226B<<<<<10 | 95.210 ns | 0.5933 ns | 0.5549 ns |         - |
| IACO 9303 Size TD3   | P<UTOQWERTY<<ASDF<<<<<<<<<<<<<<<<<<<<<<<<<<<<br>Q123987655UTO3311226F2010201<<<<<<<<<<<<<<06 | 95.839 ns | 1.0970 ns | 1.0261 ns |         - |
| IACO 9303 Size TD3   | P<UTOSKYWALKER<<LUKE<<<<<<<<<<<<<<<<<<<<<<<<<br>STARWARS45UTO7705256M2405252HAN<SHOT<FIRST78 | 95.098 ns | 0.6734 ns | 0.6299 ns |         - |
|                      |                                        |           |           |           |           |                                           
| ISAN                 | C594660A8B2E5D22X6DDA3272E             | 54.400 ns | 0.1940 ns | 0.1810 ns |         - |
| ISAN                 | D02C42E954183EE2Q1291C8AEO             | 51.210 ns | 0.2820 ns | 0.2640 ns |         - |
| ISAN                 | E9530C32BC0EE83B269867B20F             | 46.700 ns | 0.1390 ns | 0.1300 ns |         - |
|                      |                                        |           |           |           |           |                                           
| ISAN (Formatted)     | ISAN C594-660A-8B2E-5D22-X             | 45.420 ns | 0.1530 ns | 0.1360 ns |         - |
| ISAN (Formatted)     | ISAN D02C-42E9-5418-3EE2-Q             | 44.310 ns | 0.2520 ns | 0.2360 ns |         - |
| ISAN (Formatted)     | ISAN E953-0C32-BC0E-E83B-2             | 50.080 ns | 0.2070 ns | 0.1840 ns |         - |
| ISAN (Formatted)     | ISAN C594-660A-8B2E-5D22-X-6DDA-3272-E | 64.650 ns | 0.3200 ns | 0.3000 ns |         - |
| ISAN (Formatted)     | ISAN D02C-42E9-5418-3EE2-Q-1291-C8AE-O | 65.820 ns | 0.3030 ns | 0.2840 ns |         - |
| ISAN (Formatted)     | ISAN E953-0C32-BC0E-E83B-2-6986-7B20-F | 64.220 ns | 0.3640 ns | 0.3400 ns |         - |
|                      |                                        |           |           |           |           |                                           
| ISIN                 | AU0000XVGZA3                           | 25.520 ns | 0.1260 ns | 0.1170 ns |         - |
| ISIN                 | GB0002634946                           | 19.150 ns | 0.1290 ns | 0.1140 ns |         - |
| ISIN                 | US0378331005                           | 19.110 ns | 0.1400 ns | 0.1310 ns |         - |
|                      |                                        |           |           |           |           |                                           
| ISO 6346             | CSQU3054383                            | 14.970 ns | 0.0350 ns | 0.0280 ns |         - |
| ISO 6346             | MSKU9070323                            | 14.890 ns | 0.0930 ns | 0.0870 ns |         - |
| ISO 6346             | TOLU4734787                            | 14.840 ns | 0.0980 ns | 0.0870 ns |         - |
|                      |                                        |           |           |           |           |                                           
| NHS                  | 4505577104                             | 11.280 ns | 0.0360 ns | 0.0340 ns |         - |
| NHS                  | 5301194917                             | 11.270 ns | 0.0400 ns | 0.0360 ns |         - |
| NHS                  | 9434765919                             | 11.270 ns | 0.0450 ns | 0.0430 ns |         - |
|                      |                                        |           |           |           |           |                                           
| NPI                  | 1122337797                             | 14.490 ns | 0.0490 ns | 0.0440 ns |         - |
| NPI                  | 1234567893                             | 14.530 ns | 0.0800 ns | 0.0710 ns |         - |
| NPI                  | 1245319599                             | 14.520 ns | 0.0890 ns | 0.0830 ns |         - |
|                      |                                        |           |           |           |           |                                           
| SEDOL                | 3134865                                | 12.290 ns | 0.1440 ns | 0.1200 ns |         - |
| SEDOL                | B0YQ5W0                                | 12.180 ns | 0.0630 ns | 0.0560 ns |         - |
| SEDOL                | BRDVMH9                                | 12.220 ns | 0.0800 ns | 0.0710 ns |         - |
|                      |                                        |           |           |           |           |                                           
| VIN                  | 1G8ZG127XWZ157259                      | 21.120 ns | 0.1160 ns | 0.1080 ns |         - |
| VIN                  | 1HGEM21292L047875                      | 20.920 ns | 0.0770 ns | 0.0690 ns |         - |
| VIN                  | 1M8GDM9AXKP042788                      | 21.050 ns | 0.0940 ns | 0.0830 ns |         - |
                        

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

Additional included algorithms
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

Additional included algorithms
* ICAO Algorithm
* ICAO 9303 Document Size TD3 Algorithm