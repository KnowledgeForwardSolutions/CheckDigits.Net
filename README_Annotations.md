# CheckDigits.Net.Annotations

CheckDigits.Net.Annotations extends the CheckDigits.Net library with data annotation 
attributes for validating check digits in .NET applications. These attributes can be 
used to decorate properties in your data models to ensure that the values conform to 
specific check digit algorithms.

For full documentation of the various check digit algorithms supported, please refer 
to the CheckDigits.Net [README file]( https://github.com/KnowledgeForwardSolutions/CheckDigits.Net/blob/main/README.md ).

## Table of Contents

- **[Using CheckDigits.Net.Annotations](#using-checkdigits.net.annotations)**
- **[Supported Attributes](#supported-attributes)**
    * [AlphanumericMod97_10DigitAttribute](#alphanumericmod97_10checkdigitattribute)
    * [DammCheckDigitAttribute](#dammcheckdigitattribute)
	* [Iso7064Mod11_10CheckDigitAttribute](#iso7064mod11_10checkdigitattribute)
	* [Iso7064Mod11_2CheckDigitAttribute](#iso7064mod11_2checkdigitattribute)
	* [Iso7064Mod1271_36CheckDigitAttribute](#iso7064mod1271_36checkdigitattribute)
	* [Iso7064Mod27_26CheckDigitAttribute](#iso7064mod27_26checkdigitattribute)
	* [Iso7064Mod37_2CheckDigitAttribute](#iso7064mod37_2checkdigitattribute)
    * [LuhnCheckDigitAttribute](#luhncheckdigitattribute)
    * [Modulus10_13CheckDigitAttribute](#modulus10_13checkdigitattribute)
    * [Modulus10_1CheckDigitAttribute](#modulus10_1checkdigitattribute)
    * [Modulus10_2CheckDigitAttribute](#modulus10_2checkdigitattribute)
    * [Modulus11CheckDigitAttribute](#modulus11checkdigitattribute)
    * [NoidCheckDigitAttribute](#noidcheckdigitattribute)
    * [VerhoeffCheckDigitAttribute](#verhoeffcheckdigitattribute)


## Using CheckDigits.Net.Annotations

Install the CheckDigits.Net.Annotations package via command line:
```
dotnet add package CheckDigits.Net.Annotations --version 1.0.0
```
or by searching for "CheckDigits.Net.Annotations" in your IDE's Package Manager.
Once installed, you can use the provided data annotation attributes in your model 
classes.

```csharp
using System.ComponentModel.DataAnnotations;

using CheckDigits.Net.Annotations;

public class PaymentDetails
{
	[Required]
	[LuhnCheckDigit]
	public string CardNumber { get; set; }
	
	// Other properties...
}
```

In this example, attempting to send a `PaymentDetails` object with a `CardNumber`
that does not pass the Luhn check digit validation to an API endpoint with validation 
enabled will result in a 400 Bad Request response.

By default, the error message for an invalid check digit will be:
`{0} has an invalid check digit.` (or `{0} has invalid check digits.` for check
digit algorithms that use two check digits) where `{0}` is replaced with the name 
of the property being validated.

You can customize the error message by providing a custom message to the attribute:
```csharp

public class PaymentDetails
{
	[Required]
	[LuhnCheckDigit(ErrorMessage = "The card number is invalid.")]
	public string CardNumber { get; set; }
	
	// Other properties...
}
```

You can also use resource files for localization by specifying the `ErrorMessageResourceType`
and `ErrorMessageResourceName` properties:

```csharp

public class PaymentDetails
{
	[Required]
	[LuhnCheckDigit(
		ErrorMessageResourceType = typeof(Resources.ValidationMessages),
		ErrorMessageResourceName = "InvalidCardNumber")]
	public string CardNumber { get; set; }
	
	// Other properties...
}
```
Note the use of the `Required` attribute to ensure that the property is not null 
or empty. The check digit attributes do not perform null or empty checks by default
and should be used in conjunction with the `Required` attribute when necessary.

## Supported Attributes

### AlphanumericMod97_10DigitAttribute

The `AlphanumericMod97_10DigitAttribute` validates that a string property conforms to the
AlphanumericMod97_10 check digit algorithm, a variation of the ISO/IEC 7064 MOD 97-10 algorithm. 
The algorithm is supports alphanumeric characters by mapping letters A-Z to the values 10-35.
The algorithm is case-insensitive and lower case letters are treated as their uppercase equivalents.

The `AlphanumericMod97_10DigitAttribute` will return validation errors for the following conditions:
- The value does not contain a valid AlphanumericMod97_10 check digit.
- The value contains characters other than 0-9, A-Z and a-z.
- The value is shorter than 3 characters (i.e., it cannot contain the two check digit characters required by the algorithm).
- The value being validated is not of type `string`.

### DammCheckDigitAttribute

The `DammCheckDigitAttribute` validates that a string property conforms to the
Damm check digit algorithm. The Damm algorithm is quite capable of detecting
transcription errors and is used in a range of applications.

The `DammCheckDigitAttribute` will return validation errors for the following conditions:
- The value does not contain a valid Damm check digit.
- The value contains non-ASCII digit characters.
- The value is shorter than two characters (i.e., it cannot contain a check digit).
- The value being validated is not of type `string`.

### Iso7064Mod11_10CheckDigitAttribute

The `Iso7064Mod11_10CheckDigitAttribute` validates that a string property 
conforms to the ISO/IEC 7064 MOD 11,10 check digit algorithm. The ISO/IEC 7064 MOD 11,10
algorithm is designed for numeric values and uses a single numeric check digit.

The `Iso7064Mod11_10CheckDigitAttribute` will return validation errors for the following conditions:
- The value does not contain a valid ISO/IEC 7064 MOD 11,10 check digit.
- The value contains non-ASCII digit characters.
- The value is shorter than two characters (i.e., it cannot contain a check digit).
- The value being validated is not of type `string`.

### Iso7064Mod11_2CheckDigitAttribute

The `Iso7064Mod11_2CheckDigitAttribute` validates that a string property 
conforms to the ISO/IEC 7064 MOD 11-2 check digit algorithm. The ISO/IEC 7064 MOD 11-2
algorithm is designed for numeric values and uses a single numeric check 
character that can be either a numeric digit (0-9) or the letter 'X'.

The `Iso7064Mod11_2CheckDigitAttribute` will return validation errors for the following conditions:
- The value does not contain a valid ISO/IEC 7064 MOD 11-2 check digit.
- The value contains characters other than ASCII digits or 'X'.
- The value is shorter than two characters (i.e., it cannot contain a check digit).
- The value being validated is not of type `string`.

### Iso7064Mod1271_36CheckDigitAttribute

The `Iso7064Mod1271_36CheckDigitAttribute` validates that a string property 
conforms to the ISO/IEC 7064 MOD 1271-36 check digit algorithm. The ISO/IEC 7064 MOD 1271-36
algorithm is designed for alphanumeric values and uses two alphanumeric check 
characters.

The `Iso7064Mod1271_36CheckDigitAttribute` will return validation errors for the following conditions:
- The value does not contain valid ISO/IEC 7064 MOD 1271-36 check characters.
- The value contains characters other than uppercase alphanumeric characters (0-9, A-Z).
- The value is shorter than three characters (i.e., it cannot contain a pair of check characters).
- The value being validated is not of type `string`.

### Iso7064Mod27_26CheckDigitAttribute

The `Iso7064Mod27_26CheckDigitAttribute` validates that a string property 
conforms to the ISO/IEC 7064 MOD 27,26 check digit algorithm. The ISO/IEC 7064 MOD 27,26
algorithm is designed for alphabetic values and uses a single alphabetic check 
character.

The `Iso7064Mod27_26CheckDigitAttribute` will return validation errors for the following conditions:
- The value does not contain a valid ISO/IEC 7064 MOD 27,26 check character.
- The value contains characters other than uppercase alphabetic characters (A-Z).
- The value is shorter than two characters (i.e., it cannot contain a check character).
- The value being validated is not of type `string`.

### Iso7064Mod37_2CheckDigitAttribute

The `Iso7064Mod37_2CheckDigitAttribute` validates that a string property 
conforms to the ISO/IEC 7064 MOD 37-2 check digit algorithm. The ISO/IEC 7064 MOD 37-2
algorithm is designed for alphanumeric values and uses a single alphanumeric check 
character.

The `Iso7064Mod37_2CheckDigitAttribute` will return validation errors for the following conditions:
- The value does not contain valid ISO/IEC 7064 MOD 37-2 check characters.
- The value contains characters other than uppercase alphanumeric characters (0-9, A-Z).
- The value is shorter than two characters (i.e., it cannot contain a of check character).
- The value being validated is not of type `string`.

### LuhnCheckDigitAttribute

The `LuhnCheckDigitAttribute` validates that a string property conforms to the
Luhn check digit algorithm. This is commonly used for credit card numbers and other
identification numbers such as IMEI numbers and national identifiers like the
Canadian Social Insurance Number (SIN).

The `LuhnCheckDigitAttribute` will return validation errors for the following conditions:
- The value does not contain a valid Luhn check digit.
- The value contains non-ASCII digit characters.
- The value is shorter than two characters (i.e., it cannot contain a check digit).
- The value being validated is not of type `string`.

### Modulus10_13CheckDigitAttribute

The `Modulus10_13CheckDigitAttribute` validates that a string property contains 
a valid modulus 10 check digit computed using alternating weights of 1 and 3. 
This algorithm is commonly used for global item numbers such as GTIN, EAN and 
UPC codes.

The `Modulus10_13CheckDigitAttribute` will return validation errors for the following conditions:
- The value does not contain a valid Modulus10_13 check digit.
- The value contains non-ASCII digit characters.
- The value is shorter than two characters (i.e., it cannot contain a check digit).
- The value being validated is not of type `string`.

### Modulus10_1CheckDigitAttribute

The `Modulus10_1CheckDigitAttribute` validates that a string property contains a 
valid modulus 10 check digit computed using progressive weights starting with 1. 
One prominent use of this algorithm is by the Chemical Abstracts Service (CAS) 
Registry Number.

The `Modulus10_1CheckDigitAttribute` will return validation errors for the following conditions:
- The value does not contain a valid Modulus10_1 check digit.
- The value contains non-ASCII digit characters.
- The value is shorter than two characters (i.e., it cannot contain a check digit).
- The value is longer than 10 characters (the maximum length supported by the Modulus10_1 algorithm).
- The value being validated is not of type `string`.

### Modulus10_2CheckDigitAttribute

The `Modulus10_2CheckDigitAttribute` validates that a string property contains a 
valid modulus 10 check digit computed using progressive weights starting with 2. 
This algorithm is commonly used for International Maritime Organization (IMO) 
numbers.

The `Modulus10_2CheckDigitAttribute` will return validation errors for the following conditions:
- The value does not contain a valid Modulus10_2 check digit.
- The value contains non-ASCII digit characters.
- The value is shorter than two characters (i.e., it cannot contain a check digit).
- The value is longer than 10 characters (the maximum length supported by the Modulus10_2 algorithm).
- The value being validated is not of type `string`.

### Modulus11CheckDigitAttribute

The `Modulus11CheckDigitAttribute` validates that a string property conforms 
to the Modulus11 check digit algorithm. Two common uses of the Modulus11
algorithm are ISBN-10 (International Standard Book Number, 10 character format,
in use prior to January 1, 2007) and ISSN (International Standard Serial Number).

Because the algorithm uses modulus 11, the calculated check digit can be the 
value 10. In order to represent this in a single character, the letter 'X' is 
used to represent the value 10. This is only valid in the trailing check digit
position.

The `Modulus11CheckDigitAttribute` will return validation errors for the following conditions:
- The value does not contain a valid Modulus11 check digit.
- The value contains characters other than ASCII digits or 'X'.
- The value is shorter than two characters (i.e., it cannot contain a check digit).
- The value is longer than 10 characters (the maximum length supported by the Modulus11 algorithm).
- The value being validated is not of type `string`.

### NoidCheckDigitAttribute

The `NoidCheckDigitAttribute` validates that a string property conforms to the
Nice Opaque Identifier (NOID) check digit algorithm, used by systems that deal
with persistent identifiers (for example, ARK (Archival Resource Key) 
identifiers). The NOID algorithm calculates check digits for lowercase 
betanumeric identifiers (0-9 and a-z, excluding vowels and the letters l and y).

Note that unlike most other check digit algorithms, a non-betanumeric character
will not cause validation to fail; such characters are simply ignored and
contribute nothing to the check digit calculation.

In CheckDigits.Net, the NOID algorithm is implemented by the `NcdAlgorithm`
class where "Ncd" stands for "NOID Check Digit". The name "NoidCheckDigitAttribute"
is used here because "NcdCheckDigitAttribute" would be effectively redundant
(i.e. NOID Check Digit Check Digit Attribute).

The `NoidCheckDigitAttribute` will return validation errors for the following conditions:
- The value does not contain a valid NOID check digit.
- The value is shorter than two characters (i.e., it cannot contain a check digit).
- The value being validated is not of type `string`.

### VerhoeffCheckDigitAttribute

The `VerhoeffCheckDigitAttribute` validates that a string property conforms to the
Verhoeff check digit algorithm. The Verhoeff algorithm was the first algorithm using 
a single decimal check digit that was capable of detecting all single digit 
transcription errors and all two digit transposition errors. It is used in a 
variety of applications including the Aadhaar identification number in India.

The `VerhoeffCheckDigitAttribute` will return validation errors for the following conditions:
- The value does not contain a valid Verhoeff check digit.
- The value contains non-ASCII digit characters.
- The value is shorter than two characters (i.e., it cannot contain a check digit).
- The value being validated is not of type `string`.

