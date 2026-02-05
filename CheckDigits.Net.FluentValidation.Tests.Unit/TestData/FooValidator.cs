// Ignore Spelling: Validator

namespace CheckDigits.Net.FluentValidation.Tests.Unit.TestData;

public class FooValidator : AbstractValidator<FooRequest>
{
   public FooValidator(ICheckDigitAlgorithm algorithm)
   {
      RuleFor(x => x.Bar).CheckDigit(algorithm);
   }
}
