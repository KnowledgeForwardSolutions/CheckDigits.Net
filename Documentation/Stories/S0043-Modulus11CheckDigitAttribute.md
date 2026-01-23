# S0043: Modulus11 Check Digit Data Annotation

## Story
**As a** .NET developer  
**I want** a reusable C# data annotation that validates values using the Modulus 11 check digit algorithm via **CheckDigits.Net**  
**So that** I can declaratively enforce check digit validation on my model properties without duplicating validation logic.

## Background
Identifiers such as International Standard Book Number, prior to January 1, 2007 (ISBN-10) and International Standard Serial Number (ISSN) rely on a Modulus 11 algorithm to ensure data integrity. Implementing this logic repeatedly across applications leads to duplication and inconsistencies. A custom data annotation backed by **CheckDigits.Net** enables consistent, maintainable validation using standard .NET validation mechanisms.

## Acceptance Criteria
- The annotation is implemented as a custom attribute derived from `System.ComponentModel.DataAnnotations.ValidationAttribute`.
- The annotation uses **CheckDigits.Net** to validate values according to the Modulus11 algorithm.
- The annotation can be applied to `string` model properties.
- Validation succeeds when the value is valid per the Modulus11 algorithm.
- Validation fails when the value does not pass the Modulus11 check.
- Validation succeeds when the value is `null` or empty and the field is not marked as required.
- A default error message is provided (e.g., *“The field {0} has an invalid Modulus11 check digit*).
- The error message can be customized using the `ErrorMessage` property.
- New annotation to exist in CheckDigits.Net.DataAnnotations namespace.
- New annotation named Modulus11CheckDigit

## Definition of Done
- The annotation is usable in standard .NET model validation (e.g., ASP.NET Core MVC / Web API).
- Validation errors appear in model state and are returned in API responses where applicable.
- Unit tests exist for valid, invalid, and edge-case inputs.
- The implementation has no direct dependency on application-specific code.
- README updates

## Out of Scope
- Client-side validation.
- Generating or calculating Modulus11 check digits (validation only).
- Support for non-Modulus11 check digit algorithms.

## Example Usage
```csharp
public class Foo
{
    [Modulus11CheckDigit]
    public string Issn { get; set; }
}