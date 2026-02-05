// Ignore Spelling: Validator

namespace CheckDigits.Net.FluentValidation.Tests.Unit;

public class MaskedCheckDigitValidatorTests
{
   private const String _customMessage = "Card number must have a valid check digit.";

   public class PaymentRequest
   {
      public String CardNumber { get; set; } = null!;
   }

   public class PaymentRequestValidator : AbstractValidator<PaymentRequest>
   {
      public PaymentRequestValidator()
      {
         RuleFor(x => x.CardNumber)
            .MaskedCheckDigit(new LuhnAlgorithm(), new CreditCardMask())
            .WithMessage(_customMessage);
      }
   }

   public class RejectAllValidator : AbstractValidator<PaymentRequest>
   {
      public RejectAllValidator()
      {
         RuleFor(x => x.CardNumber)
            .MaskedCheckDigit(new LuhnAlgorithm(), new RejectAllMask())
            .WithMessage(_customMessage);
      }
   }

   public static TheoryData<IMaskedCheckDigitAlgorithm, ICheckDigitMask> AllAlgorithms => new()
   {
      { new LuhnAlgorithm(), new CreditCardMask() },
   };

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Theory]
   [MemberData(nameof(AllAlgorithms))]
   public void MaskedCheckDigitValidator_Validate_ShouldPass_WhenValueHasValidCheckDigit(
      IMaskedCheckDigitAlgorithm algorithm,
      ICheckDigitMask mask)
   {
      // Arrange.
      var request = new FooRequest
      {
         Bar = algorithm.GetType().Name.ToMaskedValidRequestValue()
      };
      var validator = new MaskedFooValidator(algorithm, mask);

      // Act.
      var result = validator.Validate(request);

      // Assert.
      result.IsValid.Should().BeTrue();
   }

   [Theory]
   [MemberData(nameof(AllAlgorithms))]
   public void MaskedCheckDigitValidator_Validate_ShouldPass_WhenValueIsNull(
      IMaskedCheckDigitAlgorithm algorithm,
      ICheckDigitMask mask)
   {
      // Arrange.
      var request = new FooRequest
      {
         Bar = null!
      };
      var validator = new MaskedFooValidator(algorithm, mask);

      // Act.
      var result = validator.Validate(request);

      // Assert.
      result.IsValid.Should().BeTrue();
   }

   [Theory]
   [MemberData(nameof(AllAlgorithms))]
   public void MaskedCheckDigitValidator_Validate_ShouldPass_WhenValueIsStringEmpty(
      IMaskedCheckDigitAlgorithm algorithm,
      ICheckDigitMask mask)
   {
      // Arrange.
      var request = new FooRequest
      {
         Bar = String.Empty
      };
      var validator = new MaskedFooValidator(algorithm, mask);

      // Act.
      var result = validator.Validate(request);

      // Assert.
      result.IsValid.Should().BeTrue();
   }

   [Theory]
   [MemberData(nameof(AllAlgorithms))]
   public void MaskedCheckDigitValidator_Validate_ShouldPass_WhenValueIsWhiteSpace(
      IMaskedCheckDigitAlgorithm algorithm,
      ICheckDigitMask mask)
   {
      // Arrange.
      var request = new FooRequest
      {
         Bar = "\t"
      };
      var validator = new MaskedFooValidator(algorithm, mask);

      // Act.
      var result = validator.Validate(request);

      // Assert.
      result.IsValid.Should().BeTrue();
   }

   [Theory]
   [MemberData(nameof(AllAlgorithms))]
   public void MaskedCheckDigitValidator_Validate_ShouldFail_WhenValueHasInvalidCheckDigit(
      IMaskedCheckDigitAlgorithm algorithm,
      ICheckDigitMask mask)
   {
      // Arrange.
      var request = new FooRequest
      {
         Bar = algorithm.GetType().Name.ToMaskedInvalidRequestValue()
      };
      var validator = new MaskedFooValidator(algorithm, mask);
      var expectedMessage = $"Bar must have valid {algorithm.AlgorithmName} check digit(s).";

      // Act.
      var result = validator.Validate(request);

      // Assert.
      result.IsValid.Should().BeFalse();
      result.Errors.Should().ContainSingle()
         .Which.ErrorMessage.Should().Be(expectedMessage);
   }

   [Fact]
   public void MaskedCheckDigitValidator_Validate_ShouldUseCustomErrorMessage_WhenSpecified()
   {
      // Arrange.
      var request = new PaymentRequest
      {
         CardNumber = "1234 5678 9012 3456" // Invalid Luhn check digit.
      };
      var validator = new PaymentRequestValidator();

      // Act.
      var result = validator.Validate(request);

      // Assert.
      result.IsValid.Should().BeFalse();
      result.Errors.Should().ContainSingle()
         .Which.ErrorMessage.Should().Be(_customMessage);
   }

   [Fact]
   public void MaskedCheckDigitValidator_Validate_ShouldFail_WhenMaskRejectsAllCharacters()
   {
      // Arrange.
      var request = new PaymentRequest
      {
         CardNumber = "1234 5678 9012 3456" // Invalid Luhn check digit.
      };
      var validator = new RejectAllValidator();

      // Act.
      var result = validator.Validate(request);

      // Assert.
      result.IsValid.Should().BeFalse();
      result.Errors.Should().ContainSingle()
         .Which.ErrorMessage.Should().Be(_customMessage);
   }

   #endregion
}
