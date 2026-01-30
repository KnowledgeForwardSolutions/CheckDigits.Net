# S0059: Iso7064 Hybrid Check Digit Data Annotation

## Story
**As a** .NET developer  
**I want** a reusable C# data annotation that validates formatted values using a custom ISO/IEC 7064 hybrid system check digit algorithm via the Iso7064HybridSystemAlgorithm in **CheckDigits.Net**  
**So that** I can declaratively enforce check digit validation on my model properties without duplicating validation logic.

## Background
The ISO/IEC 7064 specification defines check digit algorithms suitable for numeric (0-9), alphabetic (A-Z) and alphanumeric (0-9, A-Z) values. However, when a handling values that use symbol sets other than numeric, alphabetic or alphanumeric, the generic hybrid or pure system algorithms must be used. **CheckDigits.Net** exposes a generic hybrid check digit algorithm and I want to be able to use that in a data annotation.

## Acceptance Criteria
- The annotation is implemented as a custom attribute derived from `System.ComponentModel.DataAnnotations.ValidationAttribute`.
- The annotation uses **CheckDigits.Net** to validate values according to the generic ISO/IEC 7064 hybrid system algorithm.
- The annotation can be applied to `string` model properties.
- Validation succeeds when the value is valid per the generic ISO/IEC 7064 hybrid system algorithm.
- Validation fails when the value does not pass the generic ISO/IEC 7064 hybrid system check.
- Validation succeeds when the value is `null` or empty and the field is not marked as required.
- A default error message is provided (e.g., *“The field {0} fails check digit validation”*).
- The error message can be customized using the `ErrorMessage` property.
- New annotation to exist in CheckDigits.Net.DataAnnotations namespace.
- New annotation named Iso7065HybridCheckDigitAttribute
- Iso7065HybridCheckDigitAttribute will use a generic algorithm implementation, i.e. Iso7065HybridCheckDigitAttribute<GenericAlgorithm>.
- The generic algorithm type must be derived from Iso7064HybridSystemAlgorithm have a parameterless constructor to support new GenericAlgorithm().

## Definition of Done
- The annotation is usable in standard .NET model validation (e.g., ASP.NET Core MVC / Web API).
- Validation errors appear in model state and are returned in API responses where applicable.
- Unit tests exist for valid, invalid, and edge-case inputs.
- The implementation has no direct dependency on application-specific code.
- README updates

## Out of Scope
- Client-side validation.
- Generating or calculating ISO/IEC 7064 hybrid system check digits (validation only).
- Support for non-ISO/IEC 7064 hybrid system check digit algorithms.

## Example Usage
```csharp
public class Foo
{
    [Iso7065HybridCheckDigitAttribute<CustomAlphabetAlgorithm>]
    public string ItemIdentifer { get; set; }
}