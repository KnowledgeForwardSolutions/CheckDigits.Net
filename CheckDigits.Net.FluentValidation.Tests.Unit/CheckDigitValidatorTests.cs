// Ignore Spelling: Validator

namespace CheckDigits.Net.FluentValidation.Tests.Unit;

public class CheckDigitValidatorTests
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
            .CheckDigit(Algorithms.Luhn)
            .WithMessage(_customMessage);
      }
   }

   public static TheoryData<ICheckDigitAlgorithm> AllAlgorithms => new()
   {
      { Algorithms.AbaRtn },
      { Algorithms.AlphanumericMod97_10 },
      { Algorithms.Cusip },
      { Algorithms.Damm },
      { Algorithms.Figi },
      { Algorithms.Iban },
      { Algorithms.Icao9303 },
      { Algorithms.Icao9303MachineReadableVisa },
      { Algorithms.Icao9303SizeTD1 },
      { Algorithms.Icao9303SizeTD2 },
      { Algorithms.Icao9303SizeTD3 },
      { Algorithms.Isan },
      { Algorithms.Isin },
      { Algorithms.Iso6346 },
      { new Iso7064CustomDanishAlgorithm() },
      { new Iso7064CustomLettersAlgorithm() },
      { new Iso7064CustomNumericSupplementalAlgorithm() },
      { Algorithms.Iso7064Mod11_10 },
      { Algorithms.Iso7064Mod11_2 },
      { Algorithms.Iso7064Mod1271_36 },
      { Algorithms.Iso7064Mod27_26 },
      { Algorithms.Iso7064Mod37_2 },
      { Algorithms.Iso7064Mod37_36 },
      { Algorithms.Iso7064Mod661_26 },
      { Algorithms.Iso7064Mod97_10 },
      { Algorithms.Luhn },
      { Algorithms.Modulus10_13 },
      { Algorithms.Modulus10_1 },
      { Algorithms.Modulus10_2 },
      { Algorithms.Modulus11 },
      { Algorithms.Ncd },
      { Algorithms.Nhs },
      { Algorithms.Npi },
      { Algorithms.Sedol },
      { Algorithms.Verhoeff },
      { Algorithms.Vin },
   };

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Theory]
   [MemberData(nameof(AllAlgorithms))]
   public void CheckDigitValidator_Validate_ShouldPass_WhenValueHasValidCheckDigit(
      ICheckDigitAlgorithm algorithm)
   {
      // Arrange.
      var request = new FooRequest
      {
         Bar = algorithm.GetType().Name.ToValidRequestValue()
      };
      var validator = new FooValidator(algorithm);

      // Act.
      var result = validator.Validate(request);

      // Assert.
      result.IsValid.Should().BeTrue();
   }

   [Theory]
   [MemberData(nameof(AllAlgorithms))]
   public void CheckDigitValidator_Validate_ShouldPass_WhenValueIsNull(
      ICheckDigitAlgorithm algorithm)
   {
      // Arrange.
      var request = new FooRequest
      {
         Bar = null!
      };
      var validator = new FooValidator(algorithm);

      // Act.
      var result = validator.Validate(request);

      // Assert.
      result.IsValid.Should().BeTrue();
   }

   [Theory]
   [MemberData(nameof(AllAlgorithms))]
   public void CheckDigitValidator_Validate_ShouldPass_WhenValueIsStringEmpty(
      ICheckDigitAlgorithm algorithm)
   {
      // Arrange.
      var request = new FooRequest
      {
         Bar = String.Empty
      };
      var validator = new FooValidator(algorithm);

      // Act.
      var result = validator.Validate(request);

      // Assert.
      result.IsValid.Should().BeTrue();
   }

   [Theory]
   [MemberData(nameof(AllAlgorithms))]
   public void CheckDigitValidator_Validate_ShouldPass_WhenValueIsWhiteSpace(
      ICheckDigitAlgorithm algorithm)
   {
      // Arrange.
      var request = new FooRequest
      {
         Bar = "\t"
      };
      var validator = new FooValidator(algorithm);

      // Act.
      var result = validator.Validate(request);

      // Assert.
      result.IsValid.Should().BeTrue();
   }

   [Theory]
   [MemberData(nameof(AllAlgorithms))]
   public void CheckDigitValidator_Validate_ShouldFail_WhenValueHasInvalidCheckDigit(
      ICheckDigitAlgorithm algorithm)
   {
      // Arrange.
      var request = new FooRequest
      {
         Bar = algorithm.GetType().Name.ToInvalidRequestValue()
      };
      var validator = new FooValidator(algorithm);
      var expectedMessage =$"Bar must have valid {algorithm.AlgorithmName} check digit(s).";

      // Act.
      var result = validator.Validate(request);

      // Assert.
      result.IsValid.Should().BeFalse();
      result.Errors.Should().ContainSingle()
         .Which.ErrorMessage.Should().Be(expectedMessage);
   }

   [Fact]
   public void CheckDigitValidator_Validate_ShouldUseCustomErrorMessage_WhenSpecified()
   {
      // Arrange.
      var request = new PaymentRequest
      {
         CardNumber = "1234567890123456" // Invalid Luhn check digit.
      };
      var validator = new PaymentRequestValidator();

      // Act.
      var result = validator.Validate(request);

      // Assert.
      result.IsValid.Should().BeFalse();
      result.Errors.Should().ContainSingle()
         .Which.ErrorMessage.Should().Be(_customMessage);
   }

   #endregion
}
