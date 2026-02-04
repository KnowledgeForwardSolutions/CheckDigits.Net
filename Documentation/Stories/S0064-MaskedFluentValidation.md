# S0063: Generic Fluent Validation

## Story
**As a** .NET developer  
**I want** a custom extension method that extends **CheckDigit.Net** so that it appears as custom validator compatible with FluentValidation.
**So that** I can enforce maksed check digit validation on my model validators without duplicating validation logic.

## Background
**CheckDigits.Net** can be used with FluentValidation by using the Must method. For example:

```csharp
IMaskedCheckDigitAlgorithm algorithm = new LuhnAlgorithm();
ICheckDigitMask mask = new CreditCardMask();    // Where CreditCardMask implements ICheckDigitMask

RuleFor(x => CardNumber)
   .Must(x => algorithm.Validate(x, mask))
   ...
```

Tighter integration with FluentValidation is possible by the creation of a reusable property validator and an extension method that assigns the reusable validator the same was as other FluentValidation validators. We want to change the above code to this:

```csharp
RuleFor(x => CardNumber)
   .MaskedCheckDigit(new LuhnAlgorithm(), new CreditCardMask)
   ...
```

## Acceptance Criteria
- New CheckDigits.Net.FluentValidation project
- Targets netstandard2.0;net8.0;net10.0
- New MaskedCheckDigitValidator class that derives from PropertyValidator
- New extension MaskedCheckDigit(IMaskedCheckDigitAlgorithm algorithm, ICheckDigitMask mask) method that adds the check digit validator to the current rule builder
- The algorithm parameter must be a class that implements IMaskCheckDigitAlgorithm and must be stateless and thread-safe.
- The mask parameter must be a class that implements ICheckDigitMask and must be stateless and thread-safe.
- The new validator class expects a `string` property.
- Validation succeeds when the value is null, empty or is valid per the algorithm.
- Validation fails when a non-null, non-empty value is not valid per the algorithm.

## Definition of Done
- New MaskedCheckDigit(IMaskedCheckDigitAlgorithm algorithm, ICheckDigitMask mask) extension method demonstrated to work as expected
- Unit tests exist for valid, invalid, and edge-case inputs.
- Unit tests exist that span the range of algorithms provided by **CheckDigits.Net** and should test both valid and invalid values.
- README updates
