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
| ISBN-10				                                 | [Modulus11 Algorithm](#modulus11-algorithm) |
| ISBN-13				                                 | [Modulus10_13 Algorithm](#modulus10_13-algorithm) |
| ISBT Donation Identification Number                    | [ISO/IEC 7064 MOD 37-2 Algorithm](#isoiec-7064-mod-37-2-algorithm) |
| ISIN                                                   | [ISIN Algorithm](#isin-algorithm) |
| ISMN					                                 | [Modulus10_13 Algorithm](#modulus10_13-algorithm) |
| ISNI                                                   | [ISO/IEC 7064 MOD 11-2 Algorithm](#isoiec-7064-mod-11-2-algorithm) |
| ISSN   				                                 | [Modulus11 Algorithm](#modulus11-algorithm) |
| Legal Entity Identifier                                | [Alphanumeric MOD 97-10 Algorithm](#alphanumeric-mod-97-10-algorithm) |
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

The ABA RTN algorithm only supports validation of check digits and does not
support calculation of check digits.

#### Details

* Valid characters - decimal digits ('0' - '9')
* Check digit size - one character
* Check digit value - decimal digit ('0' - '9')
* Check digit location - ninth digit
* Value length - 9 characters
* Class name - ```AbaRtnAlgorithm```

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
* Class name - ```AlphanumericMod97_10Algorithm```

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
* Class name - ```CusipAlgorithm```

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
* Class name - ```DammAlgorithm```

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
* Class name - ```FigiAlgorithm```

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
* Class name - ```IbanAlgorithm```

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
* Class name - ```Icao9303Algorithm```

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
check digit are all calculated using the[ICAO 9303 Algorithm](#icao-9303-algorithm).

The machine readable zone of a Size TD1 document consists of three lines of 30
characters. The value passed to the Validate method should contain all lines of
data concatenated together. You can specify how the lines are separated in the
concatenated value by setting the LineSeparator property of the algorithm class.
The three values are None (no line separator is used and the 31st character of the
value is the first character of the second line), Crlf (the Windows line separator,
i.e. a carriage return character followed by a line feed character - '\r\n') and 
Lf (the Unix line separator, i.e a line feed character - '\n').The default 
LineSeparator is None.

The ICAO 9303 Document Size TD1 Algorithm will validate the check digits of the
three fields (document number, date of birth and date of expiry) as well as the 
composite check digit. If any of the check digits fail validation then the 
Validate method will return ```false```.

The ICAO 9303 Document Size TD1 algorithm only supports validation of check 
digits and does not support calculation of check digits.

#### Details

* Valid characters - decimal digits ('0' - '9'), upper case letters ('A' - 'Z') and a filler character ('<').
* Check digit size - one character
* Check digit value - decimal digit ('0' - '9')
* Check digit location - trailing (right-most) character of individual fields, trailing character of second line for composite check digit
* Value length - three lines of 30 characters plus additional line separator characters as specified by the LineSeparator property
* Class name - ```Icao9303SizeTD1Algorithm```

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
calculated using the[ICAO 9303 Algorithm](#icao-9303-algorithm).

The machine readable zone of a Size TD2 document consists of two lines of 36
characters. The value passed to the Validate method should contain all lines of
data concatenated together. You can specify how the lines are separated in the
concatenated value by setting the LineSeparator property of the algorithm class.
The three values are None (no line separator is used and the 37th character of the
value is the first character of the second line), Crlf (the Windows line separator,
i.e. a carriage return character followed by a line feed character - '\r\n') and 
Lf (the Unix line separator, i.e a line feed character - '\n').The default 
LineSeparator is None.

The ICAO 9303 Document Size TD2 Algorithm will validate the check digits of the
three fields (document number, date of birth and date of expiry) as well as the 
composite check digit. If any of the check digits fail validation then the 
Validate method will return ```false```.

The ICAO 9303 Document Size TD2 algorithm only supports validation of check 
digits and does not support calculation of check digits.

#### Details

* Valid characters - decimal digits ('0' - '9'), upper case letters ('A' - 'Z') and a filler character ('<').
* Check digit size - one character
* Check digit value - decimal digit ('0' - '9')
* Check digit location - trailing (right-most) character of individual fields, trailing character of second line for composite check digit
* Value length - two lines of 36 characters plus additional line separator characters as specified by the LineSeparator property
* Class name - ```Icao9303SizeTD2Algorithm```

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
data concatenated together. You can specify how the lines are separated in the
concatenated value by setting the LineSeparator property of the algorithm class.
The three values are None (no line separator is used and the 45th character of the
value is the first character of the second line), Crlf (the Windows line separator,
i.e. a carriage return character followed by a line feed character - '\r\n') and 
Lf (the Unix line separator, i.e a line feed character - '\n').The default 
LineSeparator is None.

The ICAO 9303 Document Size TD3 Algorithm will validate the check digits of the
four fields (passport number, date of birth, date of expiry and optional personal
number) as well as the composite check digit. If any of the check digits fail 
validation then the Validate method will return ```false```.

The ICAO 9303 Document Size TD3 algorithm only supports validation of check 
digits and does not support calculation of check digits.

#### Details

* Valid characters - decimal digits ('0' - '9'), upper case letters ('A' - 'Z') and a filler character ('<').
* Check digit size - one character
* Check digit value - decimal digit ('0' - '9')
* Check digit location - trailing (right-most) character of individual fields, trailing character of entire string for composite check digit
* Value length - two lines of 44 characters plus additional line separator characters as specified by the LineSeparator property
* Class name - ```Icao9303SizeTD3Algorithm```

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
calculated using the[ICAO 9303 Algorithm](#icao-9303-algorithm).

Machine Readable Visas have two formats: MRV-A and MRV-B. The MRV-A format uses
two lines of 44 characters while the MRV-B format uses two lines of 36 characters.
The individual fields in the second line of the machine readable zone are located
in the same character positions regardless of the format. The Validate method
can validate either format

The machine readable zone of a Machine Readable Visa consists of two lines of 36
characters. The value passed to the Validate method should contain all lines of
data concatenated together. You can specify how the lines are separated in the
concatenated value by setting the LineSeparator property of the algorithm class.
The three values are None (no line separator is used and the 37th character of the
value is the first character of the second line), Crlf (the Windows line separator,
i.e. a carriage return character followed by a line feed character - '\r\n') and 
Lf (the Unix line separator, i.e a line feed character - '\n').The default 
LineSeparator is None.

The ICAO 9303 Machine Readable Visa Algorithm will validate the check digits of 
the three fields (document number, date of birth and date of expiry). If any of 
the check digits fail validation then the Validate method will return ```false```.
In addition, if the value is not the correct length (two lines of either 44 or 
36 characters, plus line separator characters matching the LineSeparator 
property) then the method will return false.

The ICAO 9303 Machine Readable Visa algorithm only supports validation of check 
digits and does not support calculation of check digits.

#### Details

* Valid characters - decimal digits ('0' - '9'), upper case letters ('A' - 'Z') and a filler character ('<').
* Check digit size - one character
* Check digit value - decimal digit ('0' - '9')
* Check digit location - trailing (right-most) character of individual fields
* Value length - two lines of either 44 characters (MRV-A) or 36 characters (MRV-B), plus additional line separator characters as specified by the LineSeparator property
* Class name - ```Icao9303MachineReadableVisaAlgorithm```

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
* Class name - ```IsanAlgorithm```

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
* Class name - ```IsinAlgorithm```

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
* Class name - ```Iso6346Algorithm```

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
* Class name - ```Iso7064Mod11_10Algorithm```

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
* Class name - ```Iso7064Mod11_2Algorithm```

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
* Class name - ```Iso7064Mod1271_36Algorithm```

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
* Class name - ```Iso7064Mod37_2Algorithm```

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
* Class name - ```Iso7064Mod37_36Algorithm```

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
* Class name - ```Iso7064Mod661_26Algorithm```

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
* Class name - ```Iso7064Mod97_10Algorithm```

### Luhn Algorithm

#### Description

The Luhn algorithm is a modulus 10 algorithm that was developed in 1960 by Hans
Peter Luhn. It can detect all single digit transcription errors and most two digit
transposition errors except *09 -> 90* and vice versa. It can also detect most
twin errors (i.e. *11 <-> 44*) except *22 <-> 55*,  *33 <-> 66* and *44 <-> 77*.

```LuhnAlgorithm``` implements ```IMaskedCheckDigitAlgorithm``` and can be used 
to validate values that are formatted with non-check digit characters (for example,
a credit card number formatted with spaces or dashes).

#### Details

* Valid characters - decimal digits ('0' - '9')
* Check digit size - one character
* Check digit value - decimal digit ('0' - '9')
* Check digit location - assumed to be the trailing (right-most) character when validating
* Class name - ```LuhnAlgorithm```

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
* Class name - ```Modulus10_1Algorithm```

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
* Class name - ```Modulus10_2Algorithm```

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
* Class name - ```Modulus10_13Algorithm```

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
* Class name - ```Modulus11Algorithm```

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

The NHS algorithm only supports validation of check digits and does not support 
calculation of check digits.

#### Details

* Valid characters - decimal digits ('0' - '9')
* Check digit size - one character
* Check digit value - decimal digit ('0' - '9')
* Check digit location - assumed to be the trailing (right-most) character when validating
* Value length - 10 characters
* Class name - ```NhsAlgorithm```

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

The NPI algorithm only supports validation of check digits and does not support 
calculation of check digits.

#### Details

* Valid characters - decimal digits ('0' - '9')
* Check digit size - one character
* Check digit value - decimal digit ('0' - '9')
* Check digit location - assumed to be the trailing (right-most) character when validating
* Value length - 10 characters
* Class name - ```NpiAlgorithm```

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
* Class name - ```VerhoeffAlgorithm```

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
* Class name - ```VinAlgorithm```

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

Previous .Net 7 benchmarks available at https://github.com/KnowledgeForwardSolutions/CheckDigits.Net/blob/main/Documentation/DotNet7Benchmarks.md

Previous .Net 8 benchmarks available at https://github.com/KnowledgeForwardSolutions/CheckDigits.Net/blob/main/Documentation/DotNet8Benchmarks.md

#### Benchmark Details

enchmarkDotNet v0.15.8, Windows 11 (10.0.26200.7462/25H2/2025Update/HudsonValley2)
AMD RYZEN AI MAX+ 395 w/ Radeon 8060S 3.00GHz, 1 CPU, 32 logical and 16 physical cores
.NET SDK 10.0.101
  [Host]    : .NET 8.0.22 (8.0.22, 8.0.2225.52707), X64 RyuJIT x86-64-v4
  .NET 10.0 : .NET 10.0.1 (10.0.1, 10.0.125.57005), X64 RyuJIT x86-64-v4
  .NET 8.0  : .NET 8.0.22 (8.0.22, 8.0.2225.52707), X64 RyuJIT x86-64-v4

Note that the computer used to run the current benchmarks is different than the 
one used in previous .Net 7 and .Net 8 benchmarks. Therefore, columns for both 
.Net 8 and .Net 10 from the same computer are included here for direct comparison.

### TryCalculateCheckDigit/TryCalculateCheckDigits Methods

#### General Numeric Algorithms

Note that the Modulus10_1, Modulus10_2 and Modulus11 algorithms have a maximum 
length of 10 (including the check digit) for values being validated so their
benchmarks do not cover lengths greater than 10.

| Algorithm Name         | Value                 | Mean (.Net 8.0) | Allocated (.Net 8.0) | Mean (.Net 10.0) | Allocated (.Net 10.0) | Performance Delta<br>(.Net 8.0 -> .Net 10.0) |
|----------------------- |---------------------- |-----------------|----------------------|------------------|-----------------------|--------------------------------------------- |
| Damm                   | 140                   |  2.140 ns       | -                    |  2.128 ns        | -                     |  1%                                          |
| Damm                   | 140662                |  4.664 ns       | -                    |  3.617 ns        | -                     | 22%                                          |
| Damm                   | 140662538             |  5.740 ns       | -                    |  5.411 ns        | -                     |  6%                                          |
| Damm                   | 140662538042          |  7.801 ns       | -                    |  7.707 ns        | -                     |  1%                                          |
| Damm                   | 140662538042551       | 10.183 ns       | -                    |  9.992 ns        | -                     |  2%                                          |
| Damm                   | 140662538042551028    | 13.874 ns       | -                    | 13.920 ns        | -                     |  0%                                          |
| Damm                   | 140662538042551028265 | 17.891 ns       | -                    | 17.150 ns        | -                     |  4%                                          |
|                        |                       |                 |                      |                  |                       |                                              |                                   
| ISO/IEC 7064 MOD 11,10 | 140                   |  2.340 ns       | -                    |  2.228 ns        | -                     |  5%                                          |
| ISO/IEC 7064 MOD 11,10 | 140662                |  4.350 ns       | -                    |  3.977 ns        | -                     |  9%                                          |
| ISO/IEC 7064 MOD 11,10 | 140662538             |  5.364 ns       | -                    |  5.070 ns        | -                     |  5%                                          |
| ISO/IEC 7064 MOD 11,10 | 140662538042          |  6.734 ns       | -                    |  6.322 ns        | -                     |  6%                                          |
| ISO/IEC 7064 MOD 11,10 | 140662538042551       |  8.258 ns       | -                    |  7.878 ns        | -                     |  5%                                          |
| ISO/IEC 7064 MOD 11,10 | 140662538042551028    |  9.671 ns       | -                    |  9.299 ns        | -                     |  4%                                          |
| ISO/IEC 7064 MOD 11,10 | 140662538042551028265 | 11.511 ns       | -                    | 10.910 ns        | -                     |  5%                                          |
|                        |                       |                 |                      |                  |                       |                                              |                                   
| ISO/IEC 7064 MOD 11-2  | 140                   |  2.170 ns       | -                    |  2.167 ns        | -                     |  8%                                          |
| ISO/IEC 7064 MOD 11-2  | 140662                |  3.937 ns       | -                    |  3.635 ns        | -                     |  1%                                          |
| ISO/IEC 7064 MOD 11-2  | 140662538             |  4.496 ns       | -                    |  4.464 ns        | -                     | 12%                                          |
| ISO/IEC 7064 MOD 11-2  | 140662538042          |  6.296 ns       | -                    |  5.517 ns        | -                     |  4%                                          |
| ISO/IEC 7064 MOD 11-2  | 140662538042551       |  6.617 ns       | -                    |  6.340 ns        | -                     | -1%                                          |
| ISO/IEC 7064 MOD 11-2  | 140662538042551028    |  7.537 ns       | -                    |  7.618 ns        | -                     |  3%                                          |
| ISO/IEC 7064 MOD 11-2  | 140662538042551028265 |  8.806 ns       | -                    |  8.536 ns        | -                     |                                              |
|                        |                       |                 |                      |                  |                       |                                              |                                   
| ISO/IEC 7064 MOD 97-10 | 140                   |  3.436 ns       | -                    |  2.867 ns        | -                     | 17%                                          |
| ISO/IEC 7064 MOD 97-10 | 140662                |  4.229 ns       | -                    |  4.166 ns        | -                     |  1%                                          |
| ISO/IEC 7064 MOD 97-10 | 140662538             |  6.651 ns       | -                    |  5.948 ns        | -                     | 11%                                          |
| ISO/IEC 7064 MOD 97-10 | 140662538042          |  7.638 ns       | -                    |  7.425 ns        | -                     |  3%                                          |
| ISO/IEC 7064 MOD 97-10 | 140662538042551       | 9.163 ns        | -                    |  8.967 ns        | -                     |  2%                                          |
| ISO/IEC 7064 MOD 97-10 | 140662538042551028    | 11.611 ns       | -                    | 11.091 ns        | -                     |  4%                                          |
| ISO/IEC 7064 MOD 97-10 | 140662538042551028265 | 13.412 ns       | -                    | 12.914 ns        | -                     |  4%                                          |
|                        |                       |                 |                      |                  |                       |                                              |                                   
| Luhn                   | 140                   |  2.552 ns       | -                    |  2.754 ns        | -                     | -8%                                          |
| Luhn                   | 140662                |  4.487 ns       | -                    |  4.299 ns        | -                     |  4%                                          |
| Luhn                   | 140662538             |  5.479 ns       | -                    |  5.809 ns        | -                     | -6%                                          |
| Luhn                   | 140662538042          |  6.606 ns       | -                    |  6.453 ns        | -                     |  2%                                          |
| Luhn                   | 140662538042551       |  8.167 ns       | -                    |  8.102 ns        | -                     |  1%                                          |
| Luhn                   | 140662538042551028    |  9.605 ns       | -                    |  9.534 ns        | -                     |  1%                                          |
| Luhn                   | 140662538042551028265 | 11.290 ns       | -                    | 11.440 ns        | -                     | -1%                                          |
|                        |                       |                 |                      |                  |                       |                                              |                                   
| Modulus10_13           | 140                   |  2.577 ns       | -                    |  2.603 ns        | -                     | -1%                                          |
| Modulus10_13           | 140662                |  4.378 ns       | -                    |  4.224 ns        | -                     |  3%                                          |
| Modulus10_13           | 140662538             |  5.135 ns       | -                    |  5.132 ns        | -                     |  0%                                          |
| Modulus10_13           | 140662538042          |  6.386 ns       | -                    |  6.975 ns        | -                     | -9%                                          |
| Modulus10_13           | 140662538042551       |  7.671 ns       | -                    |  7.685 ns        | -                     |  0%                                          |
| Modulus10_13           | 140662538042551028    |  9.039 ns       | -                    |  8.919 ns        | -                     |  1%                                          |
| Modulus10_13           | 140662538042551028265 | 10.622 ns       | -                    | 10.287 ns        | -                     |  3%                                          |
|                        |                       |                 |                      |                  |                       |                                              |                                   
| Modulus10_1            | 140                   |  1.812 ns       | -                    |  1.804 ns        | -                     |  0%                                          |
| Modulus10_1            | 140662                |  2.857 ns       | -                    |  2.599 ns        | -                     |  9%                                          |
| Modulus10_1            | 140662538             |  4.202 ns       | -                    |  3.973 ns        | -                     |  5%                                          |
|                        |                       |                 |                      |                  |                       |                                              |                                  
| Modulus10_2            | 140                   |  1.843 ns       | -                    |  1.810 ns        | -                     |  2%                                          |
| Modulus10_2            | 140662                |  2.786 ns       | -                    |  2.585 ns        | -                     |  7%                                          |
| Modulus10_2            | 140662538             |  4.179 ns       | -                    |  3.980 ns        | -                     |  5%                                          |
|                        |                       |                 |                      |                  |                       |                                              |                                  
| Modulus11              | 140                   |  2.410 ns       | -                    |  2.319 ns        | -                     |  4%                                          |
| Modulus11              | 140662                |  3.746 ns       | -                    |  3.304 ns        | -                     | 12%                                          |
| Modulus11              | 140662538             |  4.325 ns       | -                    |  4.183 ns        | -                     |  3%                                          |
|                        |                       |                 |                      |                  |                       |                                              |                                   
| Verhoeff               | 140                   |  4.344 ns       | -                    |  4.133 ns        | -                     |  5%                                          |
| Verhoeff               | 140662                |  6.599 ns       | -                    |  6.186 ns        | -                     |  6%                                          |
| Verhoeff               | 140662538             |  9.924 ns       | -                    |  9.881 ns        | -                     |  0%                                          |
| Verhoeff               | 140662538042          | 13.347 ns       | -                    | 13.193 ns        | -                     |  1%                                          |
| Verhoeff               | 140662538042551       | 16.834 ns       | -                    | 16.671 ns        | -                     |  1%                                          |
| Verhoeff               | 140662538042551028    | 19.969 ns       | -                    | 19.555 ns        | -                     |  2%                                          |
| Verhoeff               | 140662538042551028265 | 23.187 ns       | -                    | 22.675 ns        | -                     |  2%                                          |

#### General Alphabetic Algorithms

| Algorithm Name          | Value                 | Mean (.Net 8.0) | Allocated (.Net 8.0) | Mean (.Net 10.0) | Allocated (.Net 10.0) | Performance Delta<br>(.Net 8.0 -> .Net 10.0) |
|------------------------ |---------------------- |-----------------|----------------------|------------------|-----------------------|--------------------------------------------- |
| ISO/IEC 7064 MOD 27,26  | EGR                   |  2.404 ns       | -                    |  2.217 ns        | -                     |   8%                                         |
| ISO/IEC 7064 MOD 27,26  | EGRNML                |  4.354 ns       | -                    |  3.950 ns        | -                     |   9%                                         |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOC             |  5.767 ns       | -                    |  5.695 ns        | -                     |   1%                                         |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOCECU          |  7.165 ns       | -                    |  6.808 ns        | -                     |   5%                                         |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOCECUJIK       |  8.946 ns       | -                    |  10.582 ns        | -                     | -18%                                         |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOCECUJIKNWW    | 10.589 ns       | -                    |  9.674 ns        | -                     |   9%                                         |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOCECUJIKNWWVVO | 12.218 ns       | -                    | 11.011 ns        | -                     |  10%                                         |
|                         |                       |                 |                      |                  |                       |                                              |                                           
| ISO/IEC 7064 MOD 661-26 | EGR                   |  3.323 ns       | -                    |  3.154 ns        | -                     |   5%                                         |
| ISO/IEC 7064 MOD 661-26 | EGRNML                |  5.340 ns       | -                    |  4.907 ns        | -                     |   8%                                         |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOC             |  6.837 ns       | -                    |  6.456 ns        | -                     |   6%                                         |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOCECU          |  9.708 ns       | -                    |  8.820 ns        | -                     |   9%                                         |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOCECUJIK       | 12.467 ns       | -                    | 11.282 ns        | -                     |  10%                                         |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOCECUJIKNWW    | 14.415 ns       | -                    | 13.344 ns        | -                     |   7%                                         |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOCECUJIKNWWVVO | 17.969 ns       | -                    | 16.138 ns        | -                     |  10%                                         |

#### General Alphanumeric Algorithms

Note that the values used for the NOID Check Digit algorithm do not include lengths
3 or 6 so that benchmarks are not run on purely numeric strings.

| Algorithm Name           | Value                 | Mean (.Net 8.0) | Allocated (.Net 8.0) | Mean (.Net 10.0) | Allocated (.Net 10.0) | Performance Delta<br>(.Net 8.0 -> .Net 10.0) |
|------------------------- |---------------------- |-----------------|----------------------|------------------|-----------------------|--------------------------------------------- |
| AlphanumericMod97_10     | U7y                   |  4.939 ns       | -                    |  5.023 ns        | -                     | -2%                                          |
| AlphanumericMod97_10     | U7y8SX                |  8.575 ns       | -                    |  8.310 ns        | -                     |  3%                                          |
| AlphanumericMod97_10     | U7y8SXrC0             | 12.375 ns       | -                    | 11.792 ns        | -                     |  5%                                          |
| AlphanumericMod97_10     | U7y8SXrC0O3S          | 15.880 ns       | -                    | 15.184 ns        | -                     |  4%                                          |
| AlphanumericMod97_10     | U7y8SXrC0O3Sc4I       | 20.458 ns       | -                    | 19.243 ns        | -                     |  6%                                          |
| AlphanumericMod97_10     | U7y8SXrC0O3Sc4IHYQ    | 25.418 ns       | -                    | 24.129 ns        | -                     |  5%                                          |
| AlphanumericMod97_10     | U7y8SXrC0O3Sc4IHYQF4M | 30.169 ns       | -                    | 28.308 ns        | -                     |  6%                                          |
|                          |                       |                 |                      |                  |                       |                                              |                                           
| ISO/IEC 7064 MOD 1271-36 | U7Y                   |  4.147 ns       | -                    |  4.010 ns        | -                     |  3%                                          |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SX                |  5.936 ns       | -                    | 17.238 ns        | -                     | 12%                                          |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SXRC0             |  9.121 ns       | -                    |  7.945 ns        | -                     | 13%                                          |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SXRC0O3S          | 10.523 ns       | -                    |  9.729 ns        | -                     |  8%                                          |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SXRC0O3SC4I       | 13.200 ns       | -                    | 11.875 ns        | -                     | 10%                                          |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SXRC0O3SC4IHYQ    | 16.453 ns       | -                    | 14.703 ns        | -                     | 11%                                          |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SXRC0O3SC4IHYQF4M | 19.511 ns       | -                    |  5.760 ns        | -                     |  3%                                          |
|                          |                       |                 |                      |                  |                       |                                              |                                           
| ISO/IEC 7064 MOD 37-2    | U7Y                   |  3.538 ns       | -                    |  3.133 ns        | -                     | 11%                                          |
| ISO/IEC 7064 MOD 37-2    | U7Y8SX                |  4.660 ns       | -                    |  4.498 ns        | -                     |  3%                                          |
| ISO/IEC 7064 MOD 37-2    | U7Y8SXRC0             |  6.114 ns       | -                    |  6.056 ns        | -                     |  1%                                          |
| ISO/IEC 7064 MOD 37-2    | U7Y8SXRC0O3S          |  7.229 ns       | -                    |  7.078 ns        | -                     |  2%                                          |
| ISO/IEC 7064 MOD 37-2    | U7Y8SXRC0O3SC4I       |  8.526 ns       | -                    |  8.343 ns        | -                     |  2%                                          |
| ISO/IEC 7064 MOD 37-2    | U7Y8SXRC0O3SC4IHYQ    |  9.911 ns       | -                    |  9.745 ns        | -                     |  2%                                          |
| ISO/IEC 7064 MOD 37-2    | U7Y8SXRC0O3SC4IHYQF4M | 11.231 ns       | -                    | 11.176 ns        | -                     |  0%                                          |
|                          |                       |                 |                      |                  |                       |                                              |                                           
| ISO/IEC 7064 MOD 37,36   | U7Y                   |  5.005 ns       | -                    |  3.098 ns        | -                     | 38%                                          |
| ISO/IEC 7064 MOD 37,36   | U7Y8SX                |  5.136 ns       | -                    |  4.837 ns        | -                     |  6%                                          |
| ISO/IEC 7064 MOD 37,36   | U7Y8SXRC0             |  6.800 ns       | -                    |  6.641 ns        | -                     |  2%                                          |
| ISO/IEC 7064 MOD 37,36   | U7Y8SXRC0O3S          |  8.513 ns       | -                    |  8.219 ns        | -                     |  3%                                          |
| ISO/IEC 7064 MOD 37,36   | U7Y8SXRC0O3SC4I       | 10.846 ns       | -                    |  9.906 ns        | -                     |  9%                                          |
| ISO/IEC 7064 MOD 37,36   | U7Y8SXRC0O3SC4IHYQ    | 13.508 ns       | -                    | 11.580 ns        | -                     | 14%                                          |
| ISO/IEC 7064 MOD 37,36   | U7Y8SXRC0O3SC4IHYQF4M | 15.356 ns       | -                    | 13.155 ns        | -                     | 14%                                          |
|                          |                       |                 |                      |                  |                       |                                              |                                           
| NOID Check Digit         | 11404/2h9             |  4.959 ns       | -                    |  4.403 ns        | -                     | 11%                                          |
| NOID Check Digit         | 11404/2h9tqb          |  6.317 ns       | -                    |  5.980 ns        | -                     |  5%                                          |
| NOID Check Digit         | 11404/2h9tqbxk6       |  7.695 ns       | -                    |  7.178 ns        | -                     |  7%                                          |
| NOID Check Digit         | 11404/2h9tqbxk6rw7    |  8.957 ns       | -                    |  9.154 ns        | -                     | -2%                                          |
| NOID Check Digit         | 11404/2h9tqbxk6rw7dwm | 11.803 ns       | -                    | 10.440 ns        | -                     | 12%                                          |


#### Value Specific Algorithms

Note: ABA RTN, CUSIP, ICAO 9303 multi-field algorithms (Machine Readable Visa, Size TD1, 
TD2 and TD3), ISAN, NHS, NPI and SEDOL algorithms do not support calculation of check digits, 
only validation of values containing check digits.

| Algorithm Name | Value                           | Mean (.Net 8.0) | Allocated (.Net 8.0) | Mean (.Net 10.0) | Allocated (.Net 10.0) | Performance Delta<br>(.Net 8.0 -> .Net 10.0) |
|--------------- |-------------------------------- |-----------------|----------------------|------------------|-----------------------|--------------------------------------------- |
| ICAO 9303      | U7Y                             |  5.497 ns       | -                    |  3.989 ns        | -                     |  27%                                         |
| ICAO 9303      | U7Y8SX                          |  6.154 ns       | -                    |  5.769 ns        | -                     |   6%                                         |
| ICAO 9303      | U7Y8SXRC0                       |  9.505 ns       | -                    |  7.103 ns        | -                     |  25%                                         |
| ICAO 9303      | U7Y8SXRC0O3S                    | 11.701 ns       | -                    |  8.796 ns        | -                     |  25%                                         |
| ICAO 9303      | U7Y8SXRC0O3SC4I                 | 14.353 ns       | -                    | 10.514 ns        | -                     |  27%                                         |
| ICAO 9303      | U7Y8SXRC0O3SC4IHYQ              | 18.138 ns       | -                    | 14.586 ns        | -                     |  20%                                         |
| ICAO 9303      | U7Y8SXRC0O3SC4IHYQF4M           | 19.707 ns       | -                    | 17.068 ns        | -                     |  13%                                         |
|                |                                 |                 |                      |                  |                       |                                              |                                           
| IBAN           | BE00096123456769                | 13.425 ns       | -                    | 12.713 ns        | -                     |   5%                                         |
| IBAN           | GB00WEST12345698765432          | 23.336 ns       | -                    | 21.497 ns        | -                     |   8%                                         |
| IBAN           | SC00MCBL01031234567890123456USD | 34.706 ns       | -                    | 33.375 ns        | -                     |   4%                                         |
|                |                                 |                 |                      |                  |                       |                                              |                                           
| ISIN           | AU0000XVGZA                     |  7.468 ns       | -                    |  8.011 ns        | -                     |  -7%                                         |
| ISIN           | GB000263494                     |  7.922 ns       | -                    |  7.536 ns        | -                     |   5%                                         |
| ISIN           | US037833100                     |  7.930 ns       | -                    |  7.596 ns        | -                     |   4%                                         |
|                |                                 |                 |                      |                  |                       |                                              |                                           
| ISO 6346       | CSQU305438                      |  7.345 ns       | -                    |  7.197 ns        | -                     |   2%                                         |
| ISO 6346       | MSKU907032                      |  7.344 ns       | -                    |  7.203 ns        | -                     |   2%                                         |
| ISO 6346       | TOLU473478                      |  7.318 ns       | -                    |  7.181 ns        | -                     |   2%                                         |
|                |                                 |                 |                      |                  |                       |                                              |                                           
| VIN            | 1G8ZG127_WZ157259               | 11.711 ns       | -                    | 13.019 ns        | -                     | -11%                                         |
| VIN            | 1HGEM212_2L047875               | 11.801 ns       | -                    | 13.235 ns        | -                     | -12%                                         |
| VIN            | 1M8GDM9A_KP042788               | 11.642 ns       | -                    | 12.801 ns        | -                     | -10%                                         |

### Validate Method

#### General Numeric Algorithms

All algorithms use a single check digit except ISO/IEC 7064 MOD 97-10 which uses
two check digits.

Note that the Modulus10_1, Modulus10_2 and Modulus11 algorithms have a maximum 
length of 10 (including the check digit) for values being validated so their
benchmarks do not cover lengths greater than 10.

| Algorithm Name         | Value                   | Mean (.Net 8.0) | Allocated (.Net 8.0) | Mean (.Net 10.0) | Allocated (.Net 10.0) | Performance Delta<br>(.Net 8.0 -> .Net 10.0) |
|----------------------- |------------------------ |-----------------|----------------------|------------------|-----------------------|----------------------------------------------|
| Damm                   | 1402                    |  2.513 ns       | -                    |  2.289 ns        | -                     |   9%                                         |
| Damm                   | 1406622                 |  4.617 ns       | -                    |  4.212 ns        | -                     |   9%                                         |
| Damm                   | 1406625388              |  5.634 ns       | -                    |  6.032 ns        | -                     |  -7%                                         |
| Damm                   | 1406625380422           |  8.322 ns       | -                    |  8.771 ns        | -                     |  -5%                                         |
| Damm                   | 1406625380425518        | 10.849 ns       | -                    | 12.298 ns        | -                     | -13%                                         |
| Damm                   | 1406625380425510280     | 14.540 ns       | -                    | 15.589 ns        | -                     |  -7%                                         |
| Damm                   | 1406625380425510282654  | 17.838 ns       | -                    | 18.620 ns        | -                     |  -4%                                         |
|                        |                         |                 |                      |                  |                       |                                              |                                          
| ISO/IEC 7064 MOD 11,10 | 1409                    |  2.495 ns       | -                    |  2.282 ns        | -                     |   9%                                         |
| ISO/IEC 7064 MOD 11,10 | 1406623                 |  4.447 ns       | -                    |  4.126 ns        | -                     |   7%                                         |
| ISO/IEC 7064 MOD 11,10 | 1406625381              |  5.462 ns       | -                    |  5.205 ns        | -                     |   5%                                         |
| ISO/IEC 7064 MOD 11,10 | 1406625380426           |  6.788 ns       | -                    |  6.362 ns        | -                     |   6%                                         |
| ISO/IEC 7064 MOD 11,10 | 1406625380425514        |  8.273 ns       | -                    |  7.661 ns        | -                     |   7%                                         |
| ISO/IEC 7064 MOD 11,10 | 1406625380425510286     |  9.812 ns       | -                    |  9.362 ns        | -                     |   5%                                         |
| ISO/IEC 7064 MOD 11,10 | 1406625380425510282657  | 11.392 ns       | -                    | 10.868 ns        | -                     |   5%                                         |
|                        |                         |                 |                      |                  |                       |                                              |                                          
| ISO/IEC 7064 MOD 11-2  | 140X                    |  2.093 ns       | -                    | 2.087 ns         | -                     |   0%                                         |
| ISO/IEC 7064 MOD 11-2  | 1406628                 |  3.988 ns       | -                    | 3.687 ns         | -                     |   8%                                         |
| ISO/IEC 7064 MOD 11-2  | 1406625380              |  4.608 ns       | -                    | 4.480 ns         | -                     |   3%                                         |
| ISO/IEC 7064 MOD 11-2  | 1406625380426           |  5.347 ns       | -                    | 5.218 ns         | -                     |   2%                                         |
| ISO/IEC 7064 MOD 11-2  | 1406625380425511        |  6.253 ns       | -                    | 5.953 ns         | -                     |   5%                                         |
| ISO/IEC 7064 MOD 11-2  | 140662538042551028X     |  8.354 ns       | -                    | 7.961 ns         | -                     |   5%                                         |
| ISO/IEC 7064 MOD 11-2  | 1406625380425510282651  |  7.220 ns       | -                    | 6.943 ns         | -                     |   4%                                         |
|                        |                         |                 |                      |                  |                       |                                              |                                          
| ISO/IEC 7064 MOD 97-10 | 14066                   |  2.418 ns       | -                    |  2.373 ns        | -                     |   2%                                         |
| ISO/IEC 7064 MOD 97-10 | 14066262                |  4.161 ns       | -                    |  4.031 ns        | -                     |   3%                                         |
| ISO/IEC 7064 MOD 97-10 | 14066253823             |  5.441 ns       | -                    |  5.391 ns        | -                     |   1%                                         |
| ISO/IEC 7064 MOD 97-10 | 14066253804250          |  6.791 ns       | -                    |  6.872 ns        | -                     |  -1%                                         |
| ISO/IEC 7064 MOD 97-10 | 14066253804255112       |  9.182 ns       | -                    |  8.874 ns        | -                     |   3%                                         |
| ISO/IEC 7064 MOD 97-10 | 14066253804255102853    | 10.718 ns       | -                    | 10.413 ns        | -                     |   3%                                         |
| ISO/IEC 7064 MOD 97-10 | 14066253804255102826587 | 12.301 ns       | -                    | 11.832 ns        | -                     |   4%                                         |
|                        |                         |                 |                      |                  |                       |                                              |                                          
| Luhn                   | 1404                    |  3.562 ns       | -                    |  3.407 ns        | -                     |  4%                                          |
| Luhn                   | 1406628                 |  4.855 ns       | -                    |  4.581 ns        | -                     |  6%                                          |
| Luhn                   | 1406625382              |  6.014 ns       | -                    |  6.227 ns        | -                     | -4%                                          |
| Luhn                   | 1406625380421           |  7.267 ns       | -                    |  7.078 ns        | -                     |  3%                                          |
| Luhn                   | 1406625380425514        |  9.052 ns       | -                    |  8.659 ns        | -                     |  4%                                          |
| Luhn                   | 1406625380425510285     | 10.257 ns       | -                    |  9.822 ns        | -                     |  4%                                          |
| Luhn                   | 1406625380425510282651  | 12.062 ns       | -                    | 11.765 ns        | -                     |  2%                                          |
|                        |                         |                 |                      |                  |                       |                                              |                                           
| Modulus10_13           | 1403                    |  2.982 ns       | -                    |  2.953 ns        | -                     |  1%                                          |
| Modulus10_13           | 1406627                 |  4.414 ns       | -                    |  4.376 ns        | -                     |  1%                                          |
| Modulus10_13           | 1406625385              |  5.448 ns       | -                    |  5.453 ns        | -                     |  0%                                          |
| Modulus10_13           | 1406625380425           |  6.750 ns       | -                    |  6.533 ns        | -                     |  3%                                          |
| Modulus10_13           | 1406625380425518        |  8.194 ns       | -                    |  8.231 ns        | -                     |  0%                                          |
| Modulus10_13           | 1406625380425510288     |  9.488 ns       | -                    | 10.041 ns        | -                     | -6%                                          |
| Modulus10_13           | 1406625380425510282657  |  10.916 ns      | -                    | 10.875 ns        | -                     |  0%                                          |
|                        |                         |                 |                      |                  |                       |                                              |                                           
| Modulus10_1            | 1401                    |  1.961 ns       | -                    |  1.867 ns        | -                     |  5%                                          |
| Modulus10_1            | 1406628                 |  3.011 ns       | -                    |  2.724 ns        | -                     |  10%                                         |
| Modulus10_1            | 1406625384              |  4.216 ns       | -                    |  4.086 ns        | -                     |  3%                                          |
|                        |                         |                 |                      |                  |                       |                                              |                                           
| Modulus10_2            | 1406                    |  1.950 ns       | -                    |  1.792 ns        | -                     |  8%                                          |
| Modulus10_2            | 1406627                 |  3.223 ns       | -                    |  2.811 ns        | -                     |  13%                                         |
| Modulus10_2            | 1406625389              |  4.355 ns       | -                    |  4.054 ns        | -                     |  7%                                          |
|                        |                         |                 |                      |                  |                       |                                              |                                           
| Modulus11              | 1406                    |  3.085 ns       | -                    |  2.895 ns        | -                     |  6%                                          |
| Modulus11              | 1406625                 |  4.148 ns       | -                    |  3.957 ns        | -                     |  5%                                          |
| Modulus11              | 1406625388              |  4.679 ns       | -                    |  4.345 ns        | -                     |  7%                                          |
|                        |                         |                 |                      |                  |                       |                                              |                                           
| Verhoeff               | 1401                    |  5.662 ns       | -                    |  4.579 ns        | -                     | 19%                                          |
| Verhoeff               | 1406625                 |  6.910 ns       | -                    |  6.675 ns        | -                     |  3%                                          |
| Verhoeff               | 1406625388              | 10.139 ns       | -                    | 10.084 ns        | -                     |  1%                                          |
| Verhoeff               | 1406625380426           | 13.779 ns       | -                    | 13.504 ns        | -                     |  2%                                          |
| Verhoeff               | 1406625380425512        | 17.176 ns       | -                    | 17.051 ns        | -                     |  1%                                          |
| Verhoeff               | 1406625380425510285     | 20.188 ns       | -                    | 19.774 ns        | -                     |  2%                                          |
| Verhoeff               | 1406625380425510282655  | 23.386 ns       | -                    | 23.085 ns        | -                     |  1%                                          |


#### General Alphabetic Algorithms

ISO/IEC 7064 MOD 27,26 uses a single check character. ISO/IEC 7064 MOD 661-26
uses two check characters.

| Algorithm Name          | Value                   | Mean (.Net 8.0) | Allocated (.Net 8.0) | Mean (.Net 10.0) | Allocated (.Net 10.0) | Performance Delta<br>(.Net 8.0 -> .Net 10.0) |
|------------------------ |------------------------ |-----------------|----------------------|------------------|-----------------------|--------------------------------------------- |
| ISO/IEC 7064 MOD 27,26  | EGRS                    |  2.448 ns       | -                    |  2.353 ns        | -                     |  4%                                          |
| ISO/IEC 7064 MOD 27,26  | EGRNMLU                 |  4.697 ns       | -                    |  4.105 ns        | -                     | 13%                                          |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOCB              |  5.551 ns       | -                    |  5.298 ns        | -                     |  5%                                          |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOCECUA           |  7.587 ns       | -                    |  6.911 ns        | -                     |  9%                                          |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOCECUJIKA        |  9.596 ns       | -                    |  8.277 ns        | -                     | 14%                                          |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOCECUJIKNWWY     | 10.929 ns       | -                    |  9.625 ns        | -                     | 12%                                          |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOCECUJIKNWWVVOQ  | 12.595 ns       | -                    | 11.115 ns        | -                     | 12%                                          |
|                         |                         |                 |                      |                  |                       |                                              |                                          
| ISO/IEC 7064 MOD 661-26 | EGRSE                   |  2.803 ns       | -                    |  2.617 ns        | -                     |  7%                                          |
| ISO/IEC 7064 MOD 661-26 | EGRNMLDR                |  4.642 ns       | -                    |  4.296 ns        | -                     |  7%                                          |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOCCK             |  7.066 ns       | -                    |  6.398 ns        | -                     |  9%                                          |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOCECUZJ          |  8.675 ns       | -                    |  8.321 ns        | -                     |  4%                                          |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOCECUJIKFQ       | 11.459 ns       | -                    | 10.845 ns        | -                     |  5%                                          |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOCECUJIKNWWQN    | 14.497 ns       | -                    | 13.446 ns        | -                     |  7%                                          |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOCECUJIKNWWVVORC | 16.463 ns       | -                    | 15.430 ns        | -                     |  6%                                          |

#### General Alphanumeric Algorithms

AlphanumericMod97_10 algorithm and ISO/IEC 7064 MOD 1271-36 uses two check characters. 
ISO/IEC 7064 MOD 37-2, ISO/IEC 7064 MOD 37,36 and NOID Check Digit algorithms use a 
single check character.

Note also that the values used for the NOID Check Digit algorithm do not include lengths
3 or 6 so that benchmarks are not run on purely numeric strings.

| Algorithm Name           | Value                   | Mean (.Net 8.0) | Allocated (.Net 8.0) | Mean (.Net 10.0) | Allocated (.Net 10.0) | Performance Delta<br>(.Net 8.0 -> .Net 10.0) |
|------------------------- |------------------------ |-----------------|----------------------|------------------|-----------------------|--------------------------------------------- |
| AlphanumericMod97_10     | U7y46                   |  4.562 ns       | -                    |  4.900 ns        | -                     | -7%                                          |
| AlphanumericMod97_10     | U7y8SX89                |  7.542 ns       | -                    |  7.506 ns        | -                     |  0%                                          |
| AlphanumericMod97_10     | U7y8SXrC087             | 11.450 ns       | -                    | 10.967 ns        | -                     |  4%                                          |
| AlphanumericMod97_10     | U7y8SXrC0O3S38          | 14.851 ns       | -                    | 14.361 ns        | -                     |  3%                                          |
| AlphanumericMod97_10     | U7y8SXrC0O3Sc4I27       | 19.575 ns       | -                    | 18.510 ns        | -                     |  5%                                          |
| AlphanumericMod97_10     | U7y8SXrC0O3Sc4IHYQ54    | 24.436 ns       | -                    | 23.303 ns        | -                     |  5%                                          |
| AlphanumericMod97_10     | U7y8SXrC0O3Sc4IHYQF4M21 | 29.116 ns       | -                    | 27.540 ns        | -                     |  5%                                          |
|                          |                         |                 |                      |                  |                       |                                              |                                           
| ISO/IEC 7064 MOD 1271-36 | U7YM0                   |  3.848 ns       | -                    |  3.732 ns        | -                     |  3%                                          |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SXOR                |  5.364 ns       | -                    |  5.149 ns        | -                     |  4%                                          |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SXRC0FI             |  8.003 ns       | -                    |  7.150 ns        | -                     | 11%                                          |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SXRC0O3SX4          | 10.208 ns       | -                    |  9.087 ns        | -                     | 11%                                          |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SXRC0O3SC4I9D       | 12.261 ns       | -                    | 11.030 ns        | -                     | 10%                                          |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SXRC0O3SC4IHYQYI    | 15.567 ns       | -                    | 13.515 ns        | -                     | 13%                                          |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SXRC0O3SC4IHYQF4M44 | 18.665 ns       | -                    | 16.054 ns        | -                     | 14%                                          |
|                          |                         |                 |                      |                  |                       |                                              |                                           
| ISO/IEC 7064 MOD 37-2    | U7YZ                    |  2.807 ns       | -                    |  2.599 ns        | -                     |  7%                                          |
| ISO/IEC 7064 MOD 37-2    | U7Y8SXV                 |  4.752 ns       | -                    |  4.603 ns        | -                     |  3%                                          |
| ISO/IEC 7064 MOD 37-2    | U7Y8SXRC0E              |  6.582 ns       | -                    |  5.131 ns        | -                     | 22%                                          |
| ISO/IEC 7064 MOD 37-2    | U7Y8SXRC0O3SU           |  7.037 ns       | -                    |  7.150 ns        | -                     | -2%                                          |
| ISO/IEC 7064 MOD 37-2    | U7Y8SXRC0O3SC4IB        |  8.335 ns       | -                    |  7.513 ns        | -                     | 10%                                          |
| ISO/IEC 7064 MOD 37-2    | U7Y8SXRC0O3SC4IHYQG     |  9.729 ns       | -                    |  8.759 ns        | -                     | 10%                                          |
| ISO/IEC 7064 MOD 37-2    | U7Y8SXRC0O3SC4IHYQF4MF  | 11.103 ns       | -                    |  9.895 ns        | -                     | 11%                                          |
|                          |                         |                 |                      |                  |                       |                                              |                                           
| ISO/IEC 7064 MOD 37,36   | U7YW                    |  3.396 ns       | -                    |  3.228 ns        | -                     |  5%                                          |
| ISO/IEC 7064 MOD 37,36   | U7Y8SX8                 |  5.293 ns       | -                    |  4.698 ns        | -                     | 11%                                          |
| ISO/IEC 7064 MOD 37,36   | U7Y8SXRC0E              |  6.757 ns       | -                    |  6.347 ns        | -                     |  6%                                          |
| ISO/IEC 7064 MOD 37,36   | U7Y8SXRC0O3SR           |  8.296 ns       | -                    |  7.975 ns        | -                     |  4%                                          |
| ISO/IEC 7064 MOD 37,36   | U7Y8SXRC0O3SC4IT        | 11.002 ns       | -                    |  9.497 ns        | -                     | 14%                                          |
| ISO/IEC 7064 MOD 37,36   | U7Y8SXRC0O3SC4IHYQD     | 12.623 ns       | -                    | 11.252 ns        | -                     | 11%                                          |
| ISO/IEC 7064 MOD 37,36   | U7Y8SXRC0O3SC4IHYQF4MP  | 14.467 ns       | -                    | 13.636 ns        | -                     |  6%                                          |
|                          |                         |                 |                      |                  |                       |                                              |                                           
| NOID Check Digit         | 11404/2h9m              |  5.367 ns       | -                    |  5.196 ns        | -                     |  3%                                          |
| NOID Check Digit         | 11404/2h9tqb0           |  6.565 ns       | -                    |  6.392 ns        | -                     |  3%                                          |
| NOID Check Digit         | 11404/2h9tqbxk6d        |  7.765 ns       | -                    |  7.587 ns        | -                     |  2%                                          |
| NOID Check Digit         | 11404/2h9tqbxk6rw74     |  8.960 ns       | -                    |  8.754 ns        | -                     |  2%                                          |
| NOID Check Digit         | 11404/2h9tqbxk6rw7dwmz  | 10.152 ns       | -                    |  9.957 ns        | -                     |  2%                                          |


#### Value Specific Algorithms

| Algorithm Name                  | Value                                  | Mean (.Net 8.0) | Allocated (.Net 8.0) | Mean (.Net 10.0) | Allocated (.Net 10.0) | Performance Delta<br>(.Net 8.0 -> .Net 10.0) |
|-------------------------------- |--------------------------------------- |-----------------|----------------------|------------------|-----------------------|--------------------------------------------- |
| ABA RTN                         | 111000025                              |  5.033 ns       | -                    |  4.692 ns        | -                     |  7%                                          |
| ABA RTN                         | 122235821                              |  5.038 ns       | -                    |  4.678 ns        | -                     |  7%                                          |
| ABA RTN                         | 325081403                              |  5.028 ns       | -                    |  4.600 ns        | -                     |  9%                                          |
|                                 |                                        |                 |                      |                  |                       |                                              |                                           
| CUSIP                           | 037833100                              |  6.844 ns       | -                    |  6.232 ns        | -                     |  9%                                          |
| CUSIP                           | 38143VAA7                              |  6.875 ns       | -                    |  6.083 ns        | -                     | 11%                                          |
| CUSIP                           | 91282CJL6                              |  6.831 ns       | -                    |  6.229 ns        | -                     |  9%                                          |
|                                 |                                        |                 |                      |                  |                       |                                              |                                           
| FIGI                            | BBG000B9Y5X2                           |  9.662 ns       | -                    |  8.008 ns        | -                     | 17%                                          |
| FIGI                            | BBG111111160                           |  9.716 ns       | -                    |  8.170 ns        | -                     | 16%                                          |
| FIGI                            | BBGZYXWVTSR7                           |  9.648 ns       | -                    |  8.004 ns        | -                     | 17%                                          |
|                                 |                                        |                 |                      |                  |                       |                                              |                                           
| IBAN                            | BE71096123456769                       | 12.425 ns       | -                    | 11.738 ns        | -                     |  6%                                          |
| IBAN                            | GB82WEST12345698765432                 | 21.797 ns       | -                    | 20.776 ns        | -                     |  5%                                          |
| IBAN                            | SC74MCBL01031234567890123456USD        | 33.618 ns       | -                    | 31.848 ns        | -                     |  5%                                          |
|                                 |                                        |                 |                      |                  |                       |                                              |
| ICAO 9303                       | U7Y5                                   |  4.614 ns       | -                    |  4.171 ns        | -                     | 10%                                          |
| ICAO 9303                       | U7Y8SX8                                |  6.050 ns       | -                    |  5.820 ns        | -                     |  4%                                          |
| ICAO 9303                       | U7Y8SXRC03                             |  7.776 ns       | -                    |  7.358 ns        | -                     |  5%                                          |
| ICAO 9303                       | U7Y8SXRC0O3S8                          |  9.595 ns       | -                    |  9.811 ns        | -                     | -2%                                          |
| ICAO 9303                       | U7Y8SXRC0O3SC4I2                       | 11.372 ns       | -                    | 11.839 ns        | -                     | -4%                                          |
| ICAO 9303                       | U7Y8SXRC0O3SC4IHYQ9                    | 13.186 ns       | -                    | 13.739 ns        | -                     | -4%                                          |
| ICAO 9303                       | U7Y8SXRC0O3SC4IHYQF4M8                 | 15.074 ns       | -                    | 15.888 ns        | -                     | -5%                                          |
|                                 |                                                                                                    |                 |                      |                  |                       |                                              |
| ICAO 9303 Machine Readable Visa | I<UTOERIKSSON<<ANNA<MARIA<<<<<<<<<<<<br>D231458907UTO7408122F1204159<<<<<<<<                       | 19.565 ns       | -                    | 18.568 ns        | -                     |  5%                                          |
| ICAO 9303 Machine Readable Visa | I<UTOSKYWALKER<<LUKE<<<<<<<<<<<<<<<<<br>STARWARS45UTO7705256M2405252<<<<<<<<                       | 19.520 ns       | -                    | 18.535 ns        | -                     |  5%                                          |
| ICAO 9303 Machine Readable Visa | V<UTOERIKSSON<<ANNA<MARIA<<<<<<<<<<<<<<<<<<<<br>L898902C<3UTO6908061F9406236ZE184226B<<<<<<<       | 18.751 ns       | -                    | 18.520 ns        | -                     |  1%                                          |
|                                 |                                                                                                    |                 |                      |                  |                       |                                              |
| ICAO 9303 Size TD1              | I<UTOD231458907<<<<<<<<<<<<<<<<br>7408122F1204159UTO<<<<<<<<<<<6<br>ERIKSSON<<ANNA<MARIA<<<<<<<<<< | 32.991 ns       | -                    | 33.525 ns        | -                     | -2%                                          |
| ICAO 9303 Size TD1              | I<UTOSTARWARS45<<<<<<<<<<<<<<<<br>7705256M2405252UTO<<<<<<<<<<<4<br>SKYWALKER<<LUKE<<<<<<<<<<<<<<< | 41.699 ns       | -                    | 37.326 ns        | -                     | 10%                                          |
| ICAO 9303 Size TD1              | I<UTOD23145890<AB112234566<<<<<br>7408122F1204159UTO<<<<<<<<<<<4<br>ERIKSSON<<ANNA<MARIA<<<<<<<<<< | 32.963 ns       | -                    | 33.294 ns        | -                     | -1%                                          |
|                                 |                                                                                                    |                 |                      |                  |                       |                                              |
| ICAO 9303 Size TD2              | I<UTOERIKSSON<<ANNA<MARIA<<<<<<<<<<<<br>D231458907UTO7408122F1204159<<<<<<<6                       | 30.565 ns       | -                    | 30.815 ns        | -                     | -1%                                          |
| ICAO 9303 Size TD2              | I<UTOQWERTY<<ASDF<<<<<<<<<<<<<<<<<<<<br>D23145890<UTO7408122F1204159AB1124<4                       | 30.817 ns       | -                    | 30.814 ns        | -                     |  0%                                          |
| ICAO 9303 Size TD2              | I<UTOSKYWALKER<<LUKE<<<<<<<<<<<<<<<<<br>STARWARS45UTO7705256M2405252<<<<<<<8                       | 30.745 ns       | -                    | 30.814 ns        | -                     |  0%                                          |
|                                 |                                                                                                    |                 |                      |                  |                       |                                              |
| ICAO 9303 Size TD3              | P<UTOERIKSSON<<ANNA<MARIA<<<<<<<<<<<<<<<<<<<<br>L898902C36UTO7408122F1204159ZE184226B<<<<<10       | 39.632 ns       | -                    | 40.555 ns        | -                     | -2%                                          |
| ICAO 9303 Size TD3              | P<UTOQWERTY<<ASDF<<<<<<<<<<<<<<<<<<<<<<<<<<<<br>Q123987655UTO3311226F2010201<<<<<<<<<<<<<<06       | 40.310 ns       | -                    | 40.680 ns        | -                     | -1%                                          |
| ICAO 9303 Size TD3              | P<UTOSKYWALKER<<LUKE<<<<<<<<<<<<<<<<<<<<<<<<<br>STARWARS45UTO7705256M2405252HAN<SHOT<FIRST78       | 39.529 ns       | -                    | 40.659 ns        | -                     | -3%                                          |
|                                 |                                        |                 |                      |                  |                       |                                              |
| ISAN                            | C594660A8B2E5D22X6DDA3272E             | 19.312 ns       | -                    | 17.983 ns        | -                     |  7%                                          |
| ISAN                            | D02C42E954183EE2Q1291C8AEO             | 19.336 ns       | -                    | 20.953 ns        | -                     | -8%                                          |
| ISAN                            | E9530C32BC0EE83B269867B20F             | 19.682 ns       | -                    | 16.865 ns        | -                     | 14%                                          |
|                                 |                                        |                 |                      |                  |                       |                                              |                                          
| ISAN (Formatted)                | ISAN C594-660A-8B2E-5D22-X             | 17.979 ns       | -                    | 14.649 ns        | -                     | 19%                                          |
| ISAN (Formatted)                | ISAN D02C-42E9-5418-3EE2-Q             | 17.555 ns       | -                    | 15.328 ns        | -                     | 13%                                          |
| ISAN (Formatted)                | ISAN E953-0C32-BC0E-E83B-2             | 17.977 ns       | -                    | 15.452 ns        | -                     | 14%                                          |
| ISAN (Formatted)                | ISAN C594-660A-8B2E-5D22-X-6DDA-3272-E | 26.585 ns       | -                    | 22.362 ns        | -                     | 16%                                          |
| ISAN (Formatted)                | ISAN D02C-42E9-5418-3EE2-Q-1291-C8AE-O | 26.834 ns       | -                    | 22.603 ns        | -                     | 16%                                          |
| ISAN (Formatted)                | ISAN E953-0C32-BC0E-E83B-2-6986-7B20-F | 26.475 ns       | -                    | 22.446 ns        | -                     | 15%                                          |
|                                 |                                        |                 |                      |                  |                       |                                              |                                          
| ISIN                            | AU0000XVGZA3                           |  8.262 ns       | -                    |  7.678 ns        | -                     |  7%                                          |
| ISIN                            | GB0002634946                           |  8.165 ns       | -                    |  7.469 ns        | -                     |  9%                                          |
| ISIN                            | US0378331005                           |  8.195 ns       | -                    |  7.505 ns        | -                     |  8%                                          |
|                                 |                                        |                 |                      |                  |                       |                                              |                                          
| ISO 6346                        | CSQU3054383                            |  7.441 ns       | -                    |  7.811 ns        | -                     | -5%                                          |
| ISO 6346                        | MSKU9070323                            |  7.432 ns       | -                    |  7.835 ns        | -                     | -5%                                          |
| ISO 6346                        | TOLU4734787                            |  7.458 ns       | -                    |  7.831 ns        | -                     | -5%                                          |
|                                 |                                        |                 |                      |                  |                       |                                              |                                          
| NHS                             | 4505577104                             |  4.404 ns       | -                    |  4.455 ns        | -                     | -1%                                          |
| NHS                             | 5301194917                             |  4.411 ns       | -                    |  4.445 ns        | -                     | -1%                                          |
| NHS                             | 9434765919                             |  4.404 ns       | -                    |  4.438 ns        | -                     | -1%                                          |
|                                 |                                        |                 |                      |                  |                       |                                              |                                          
| NPI                             | 1122337797                             |  6.166 ns       | -                    |  5.829 ns        | -                     |  5%                                          |
| NPI                             | 1234567893                             |  5.962 ns       | -                    |  5.931 ns        | -                     |  1%                                          |
| NPI                             | 1245319599                             |  5.951 ns       | -                    |  5.830 ns        | -                     |  2%                                          |
|                                 |                                        |                 |                      |                  |                       |                                              |                                          
| SEDOL                           | 3134865                                |  5.612 ns       | -                    |  5.815 ns        | -                     | -4%                                          |
| SEDOL                           | B0YQ5W0                                |  5.603 ns       | -                    |  5.813 ns        | -                     | -4%                                          |
| SEDOL                           | BRDVMH9                                |  5.609 ns       | -                    |  5.819 ns        | -                     | -4%                                          |
|                                 |                                        |                 |                      |                  |                       |                                              |                                          
| VIN                             | 1G8ZG127XWZ157259                      | 12.377 ns       | -                    | 12.944 ns        | -                     | -5%                                          |
| VIN                             | 1HGEM21292L047875                      | 12.398 ns       | -                    | 12.865 ns        | -                     | -4%                                          |
| VIN                             | 1M8GDM9AXKP042788                      | 12.134 ns       | -                    | 13.088 ns        | -                     | -8%                                          |

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

| Algorithm Name         | Value                          | Mean (.Net 8.0) | Allocated (.Net 8.0) | Mean (.Net 10.0) | Allocated (.Net 10.0) | Performance Delta<br>(.Net 8.0 -> .Net 10.0) |
|----------------------- |------------------------------- |-----------------|----------------------|------------------|-----------------------|----------------------------------------------|
| Luhn                   | 140 4                          |  4.743 ns       | -                    |  4.675 ns        | -                     |  1%                                          |
| Luhn                   | 140 662 8                      |  6.800 ns       | -                    |  6.032 ns        | -                     | 11%                                          |
| Luhn                   | 140 662 538 2                  | 10.294 ns       | -                    |  7.948 ns        | -                     | 23%                                          |
| Luhn                   | 140 662 538 042 1              | 11.998 ns       | -                    |  9.833 ns        | -                     | 18%                                          |
| Luhn                   | 140 662 538 042 551 4          | 14.930 ns       | -                    | 12.079 ns        | -                     | 19%                                          |
| Luhn                   | 140 662 538 042 551 028 5      | 17.257 ns       | -                    | 14.282 ns        | -                     | 17%                                          |
| Luhn                   | 140 662 538 042 551 028 265 1  | 19.668 ns       | -                    | 16.428 ns        | -                     | 16%                                          |

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
* NpiAlgorithm, Validate method ~8 improvement

## v2.3.1

Documentation update. Fix several minor documentation errors in README file. No code changes.

## v3.0.0

Updated to .Net 10.0

Average performance improvement for .Net 10.0 across all algorithms:
  Validate method ~4% improvement, TryCalculateCheckDigit method ~6% improvement

Detailed benchmark results for .Net 8 vs .Net 10 located at 

Added masked validation support for algorithms via ICheckDigitMask and IMaskedCheckDigitAlgorithm interfaces. Algorithms that implement IMaskedCheckDigitAlgorithm:
* Luhn Algorithm
