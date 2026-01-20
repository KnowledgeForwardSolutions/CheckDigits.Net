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
    * [LuhnCheckDigitAttribute](#luhncheckdigitattribute)


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

In this example, attempting to send a `PaymentDetails` object with an invalid
`CardNumber` to an API endpoint with validation enabled will result in a 400 Bad Request
response.

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

### LuhnCheckDigitAttribute

The `LuhnCheckDigitAttribute` validates that a string property conforms to the
Luhn check digit algorithm. This is commonly used for credit card numbers and other
identification numbers such as IMEI numbers and national identifiers like the
Canadian Social Insurance Number (SIN).
