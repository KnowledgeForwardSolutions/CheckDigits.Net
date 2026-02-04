# CheckDigits.Net.Annotations

CheckDigits.Net.Annotations extends the CheckDigits.Net library with data annotation 
attributes for validating check digits in .NET applications. These attributes can be 
used to decorate properties in your data models to ensure that the values conform to 
specific check digit algorithms.

For full documentation of the various check digit algorithms supported, please refer 
to the CheckDigits.Net [README file]( https://github.com/KnowledgeForwardSolutions/CheckDigits.Net/blob/main/README.md ).

## Using CheckDigits.Net.Annotations

Install the CheckDigits.Net.Annotations package via command line:
```
dotnet add package CheckDigits.Net.Annotations --version 1.0.0
```
or by searching for "CheckDigits.Net.Annotations" in your IDE's Package Manager.

Once installed, you can decorate a model property with the [CheckDigit<TAlgorithm>] 
to validate that the value conforms to the specified check digit algorithm.
TAlgorithm is the name of a class that implements `ICheckDigitAlgorithm`, including
any of the algorithms provided in the CheckDigits.Net package or your own custom implementations.
TAlgorithm must have a parameterless constructor and must be stateless and thread-safe.

For example, to validate that a credit card number conforms to the Luhn algorithm
included in CheckDigits.Net, you would do the following:

```csharp
using System.ComponentModel.DataAnnotations;

using CheckDigits.Net.GeneralAlgorithms;
using CheckDigits.Net.Annotations;

public class PaymentDetails
{
	[Required]
	[CheckDigit<LuhnAlgorithm>]
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
	[CheckDigit<LuhnAlgorithm>(ErrorMessage = "The card number is invalid.")]
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
	[CheckDigit<LuhnAlgorithm>(
		ErrorMessageResourceType = typeof(Resources.ValidationMessages),
		ErrorMessageResourceName = "InvalidCardNumber")]
	public string CardNumber { get; set; }
	
	// Other properties...
}
```
Note the use of the `Required` attribute to ensure that the property is not null 
or empty. The check digit attributes do not perform null or empty checks by default
and should be used in conjunction with the `Required` attribute when necessary.

### ISO/IEC 7064 Algorithms with custom alphabets

CheckDigits.Net includes support for ISO/IEC 7064 check digit algorithms that 
use custom alphabets with the classes `Iso7064HybridSystemAlgorithm`,
`Iso7064PureSystemDoubleCharacterAlgorithm` and `Iso7064PureSystemSingleCharacterAlgorithm`.
Refer to the CheckDigits.Net [README file]( https://github.com/KnowledgeForwardSolutions/CheckDigits.Net/blob/main/README.md ),
in particular the section **Custom Alphabets for ISO 7064** for more information
on these algorithms and how to create custom alphabets.

You can not use `Iso7064HybridSystemAlgorithm`, `Iso7064PureSystemDoubleCharacterAlgorithm`
or `Iso7064PureSystemSingleCharacterAlgorithm` directly with CheckDigitAttribute<TAlgorithm>
as they do not have parameterless constructors. Instead, you must create a custom algorithm class 
that derives from one of these classes and provides a parameterless constructor
that initializes the base class with your custom alphabet and other values. This
is an example that uses the `DanishAlphabet` and `Iso7064PureSystemDoubleCharacterAlgorithm`
described in the CheckDigits.Net README:

```csharp
public class Iso7064CustomDanishAlgorithm : 
   Iso7064PureSystemDoubleCharacterAlgorithm
{
   public Iso7064CustomDanishAlgorithm()
      : base("Danish", "Danish, modulus = 29, radix = 2", 29, 2, new DanishAlphabet())
   { }
}

public class Foo
{
   [CheckDigit<Iso7064CustomDanishAlgorithm>]
   public String BarValue { get; set; } = null!;
}
```

### Check Digit Attributes with Masks

There are cases where values being validated may include formatting characters
that should be ignored when performing check digit validation (for example, a 
credit card number that has been formatted with spaces, '1234 5678 9012 3456'). 
CheckDigits.Net supports this scenario through the use of check digit masks. An 
algorithm that supports check digit masks implements the `ICheckDigitMaskableAlgorithm` 
interface which extends `ICheckDigitAlgorithm` to indicate that it can work with 
masks (currently, only the Luhn algorithm does so, but additional algorithms will 
be added in the future).

To use a check digit mask, create a class that implements the `ICheckDigitMask`
interface (defined in the CheckDigits.Net namespace). As with the algorithm class,
the class that implements `ICheckDigitMask` must have a parameterless constructor
and must be stateless and thread-safe.

Then, use the [MaskedCheckDigit<TAlgorithm, TMask>] attribute to decorate a model
property. For example:

```csharp
// Excludes every 5th character, allowing for spaces or dashes in credit card numbers.
public class CreditCardMask : ICheckDigitMask
{
   public Boolean ExcludeCharacter(Int32 index) => (index + 1) % 5 == 0;

   public Boolean IncludeCharacter(Int32 index) => (index + 1) % 5 != 0;
}

public class PaymentDetails
{
	[Required]
	[MaskedCheckDigit<LuhnAlgorithm, CreditCardMask>(ErrorMessage = "The card number is invalid.")]
	public string CardNumber { get; set; }
	
	// Other properties...
}
```

# Release History/Release Notes

## v1.0.0

Initial release. Supports [CheckDigit<TAlgorithm>] and [MaskedCheckDigit<TAlgorithm, TMask>] attributes
for validating check digits using data annotations.