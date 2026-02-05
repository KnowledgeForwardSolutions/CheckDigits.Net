// Ignore Spelling: Validator

namespace CheckDigits.Net.FluentValidation.Tests.Unit.TestData;

public class MaskedFooValidator : AbstractValidator<FooRequest>
{
   public MaskedFooValidator(
      IMaskedCheckDigitAlgorithm algorithm,
      ICheckDigitMask mask)
   {
      RuleFor(x => x.Bar).MaskedCheckDigit(algorithm, mask);
   }
}
