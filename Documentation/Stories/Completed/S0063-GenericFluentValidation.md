# S0063: Generic Fluent Validation

## Story
**As a** .NET developer  
**I want** a custom extension method that extends **CheckDigit.Net** so that it appears as custom validator compatible with FluentValidation.
**So that** I can enforce check digit validation on my model validators without duplicating validation logic.

## Background
**CheckDigits.Net** can be used with FluentValidation by using the Must method. For example:

```csharp
RuleFor(x => UpcCode)
   .Must(x => Algorithms.Modulus11_13.Validate(x))
   ...
```

Tighter integration with FluentValidation is possible by the creation of a reusable property validator and an extension method that assigns the reusable validator the same was as other FluentValidation validators. We want to change the above code to this:

```csharp
RuleFor(x => UpcCode)
   .CheckDigit(Algorithms.Modulus10_13)
   ...
```

## Acceptance Criteria
- New CheckDigits.Net.FluentValidation project
- Targets netstandard2.0;net8.0;net10.0
- New CheckDigitValidator class that derives from PropertyValidator
- New extension CheckDigit(ICheckDigitAlgorithm algorithm) method that adds the check digit validator to the current rule builder
- The algorithm generic parameter must be a class that implements ICheckDigitAlgorithm and must be stateless and thread-safe.
- The new validator class expects a `string` property.
- Validation succeeds when the value is null, empty or is valid per TAlgorithm.
- Validation fails when a non-null, non-empty value is not valid per TAlgorithm.

## Definition of Done
- New CheckDigit(ICheckDigitAlgorithm algorithm) extension method demonstrated to work as expected
- Unit tests exist for valid, invalid, and edge-case inputs.
- Unit tests exist that span the range of algorithms provided by **CheckDigits.Net** and should test both valid and invalid values.
- README updates
