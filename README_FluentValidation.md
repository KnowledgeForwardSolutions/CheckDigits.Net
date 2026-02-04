# CheckDigits.Net.FluentValidation

CheckDigits.Net.FluentValidation integrates the CheckDigits.Net library with 
FluentValidation, providing validators for check digit algorithms in .NET applications.

For full documentation of the various check digit algorithms supported, please refer 
to the CheckDigits.Net [README file](https://github.com/KnowledgeForwardSolutions/CheckDigits.Net/blob/main/README.md).

## Using CheckDigits.Net.FluentValidation

Install the CheckDigits.Net.FluentValidation package via command line:
```
dotnet add package CheckDigits.Net.FluentValidation --version 1.0.0
```
or by searching for "CheckDigits.Net.FluentValidation" in your IDE's Package Manager.

Once installed, you can use the `CheckDigit(ICheckDigitAlgorithm)` extension method
to include check digit validation in your FluentValidation validators. You can
use any of the algorithms provided in the CheckDigits.Net package or your own
custom implementations. The supplied algorithm must be stateless and thread-safe.

For example, to validate that a UPC code conforms to the Modulus10_13 algorithm
included in CheckDigits.Net (the algorithm used by EAC, GTIN, ISBN-13, UPC and
other international standard identifiers), you would do the following:

```csharp
using CheckDigits.Net.GeneralAlgorithms;
using CheckDigits.Net.FluentValidation;

public class ProductDetails
{
	public string UpcCode { get; set; }
	
	// Other properties...
}

public class ProductDetailsValidator : AbstractValidator<ProductDetails>
{
	public ProductDetailsValidator()
	{
		RuleFor(x => x.UpcCode)
			.NotEmpty()
			.CheckDigit(Algorithms.Modulus10_13);		// Using a predefined, lazy created instance of the Modulus10_13 algorithm
	}
}
```
By default, the error message for an invalid check digit will be:
` {PropertyName} must have valid {AlgorithmName} check digit(s).` You may use
the FluentValidation mechanisms to customize the error message as needed.

Note the use of the `NotEmpty()` method to ensure that the property is not null, 
empty or whitespace. The check digit validator does not perform null, empty or
whitespace checks by default and should be used in conjunction with `NotEmpty()` 
when necessary.

In cases where you need to use a custom implementation of `ICheckDigitAlgorithm`,
you can do so by creating an instance of your algorithm and passing it to the
`CheckDigit` method:

```csharp
public class ProductDetailsValidator : AbstractValidator<ProductDetails>
{
	public ProductDetailsValidator()
	{
		RuleFor(x => x.UpcCode)
			.NotEmpty()
			.CheckDigit(new Modulus10_13Algorithm());		// Instantiate the Modulus10_13 algorithm directly
	}
}
```
A common use case for custom implementations is when you need to use the
`Iso7064HybridSystemAlgorithm`, `Iso7064PureSystemDoubleCharacterAlgorithm`
or `Iso7064PureSystemSingleCharacterAlgorithm` classes to validate check digits
that use custom character sets.

# Release History/Release Notes

## v1.0.0

Initial release. 