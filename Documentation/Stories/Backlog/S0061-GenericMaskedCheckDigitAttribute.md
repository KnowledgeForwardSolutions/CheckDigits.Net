# S0061: Generic Masked Check Digit Data Annotation

## Story
**As a** .NET developer  
**I want** to use the masked check digit algorithms provided by **CheckDigits.Net** in a data annotation attribute 
**So that** I can declaratively enforce check digit validation on my model properties containing formatted values without duplicating validation logic.

## Background
**CheckDigits.Net** implements check digit algorithms that support the use of masks to ignore formatting characters in classes that implement `IMaskedCheckDigitAlgorithm`. This makes it possible to include check digit validation in code by creating an instance of the desired check digit algorithm class and then invoking the Validate(String, Mask) method. However, it is not possible to declaratively use **CheckDigits.Net** in a data annotation attribute without creating a custom attribute class. In order to prevent duplication of effort, CheckDigits.Net.Annotations should impliment a MaskedCheckDigitAttribute<TAlgorithm, TMask> class that can be applied to business object properties. For example,

```csharp
public class Foo
{
    [MaskedCheckDigit<LuhnAlgorithm, CreditCardMask>]     // To support validation of '1234 5678 9012 3456'
    public string CardNumber { get; set; }
}
```

## Acceptance Criteria
- New MaskedCheckDigit<TAlgorithm, TMask> implemented as a custom attribute derived from `System.ComponentModel.DataAnnotations.ValidationAttribute`.
- The TAlgorithm generic parameter must be a class that implements IMaskedCheckDigitAlgorithm and must have a parameterless constructor.
- The TMask generic parameter must be a class that implements ICheckDigitMask and must have a parameterless constructor.
- The annotation can be applied to `string` model properties.
- Validation succeeds when the value is null, empty or is valid per TAlgorithm.
- Validation fails when a non-null, non-empty value is not valid per TAlgorithm.

## Definition of Done
- The annotation is usable in standard .NET model validation (e.g., ASP.NET Core MVC / Web API).
- Validation errors appear in model state and are returned in API responses where applicable.
- Unit tests exist for valid, invalid, and edge-case inputs.
- Unit tests exist that span the range of masked check digit algorithms provided by **CheckDigits.Net** and should test both valid and invalid values.
- README updates
