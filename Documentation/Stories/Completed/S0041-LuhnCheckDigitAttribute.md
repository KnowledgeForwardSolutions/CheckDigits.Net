# S0041: Luhn Check Digit Data Annotation

## Story
**As a** .NET developer  
**I want** a reusable C# data annotation that validates values using the Luhn check digit algorithm via **CheckDigits.Net**  
**So that** I can declaratively enforce check digit validation on my model properties without duplicating validation logic.

## Background
Many identifiers (such as credit card numbers and account references) rely on the Luhn algorithm for integrity. Implementing this logic repeatedly across applications leads to duplication and inconsistencies. A custom data annotation backed by **CheckDigits.Net** enables consistent, maintainable validation using standard .NET validation mechanisms.

## Acceptance Criteria
- The annotation is implemented as a custom attribute derived from `System.ComponentModel.DataAnnotations.ValidationAttribute`.
- The annotation uses **CheckDigits.Net** to validate values according to the Luhn algorithm.
- The annotation can be applied to `string` model properties.
- Validation succeeds when the value is valid per the Luhn algorithm.
- Validation fails when the value does not pass the Luhn check.
- Validation succeeds when the value is `null` or empty and the field is not marked as required.
- A default error message is provided (e.g., *“The field {0} fails Luhn check digit validation”*).
- The error message can be customized using the `ErrorMessage` property.
- New annotation to exist in CheckDigits.Net.DataAnnotations namespace.
- New annotation named LuhnCheckDigit

## Definition of Done
- The annotation is usable in standard .NET model validation (e.g., ASP.NET Core MVC / Web API).
- Validation errors appear in model state and are returned in API responses where applicable.
- Unit tests exist for valid, invalid, and edge-case inputs.
- The implementation has no direct dependency on application-specific code.
- README updates

## Out of Scope
- Client-side validation.
- Generating or calculating Luhn check digits (validation only).
- Support for non-Luhn check digit algorithms.

## Example Usage
```csharp
public class PaymentRequest
{
    [LuhnCheckDigit]
    public string CardNumber { get; set; }
}