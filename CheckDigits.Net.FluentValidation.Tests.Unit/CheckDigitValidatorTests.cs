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

   public static TheoryData<String, String> AllAlgorithms => new()
   {
      { nameof(AbaRtnAlgorithm), Algorithms.AbaRtn.AlgorithmName },
      { nameof(AlphanumericMod97_10Algorithm), Algorithms.AlphanumericMod97_10.AlgorithmName },
      { nameof(CusipAlgorithm), Algorithms.Cusip.AlgorithmName },
      { nameof(DammAlgorithm), Algorithms.Damm.AlgorithmName },
      { nameof(FigiAlgorithm), Algorithms.Figi.AlgorithmName },
      { nameof(IbanAlgorithm), Algorithms.Iban.AlgorithmName },
      { nameof(Icao9303Algorithm), Algorithms.Icao9303.AlgorithmName },
      { nameof(Icao9303MachineReadableVisaAlgorithm), Algorithms.Icao9303MachineReadableVisa.AlgorithmName },
      { nameof(Icao9303SizeTD1Algorithm), Algorithms.Icao9303SizeTD1.AlgorithmName },
      { nameof(Icao9303SizeTD2Algorithm), Algorithms.Icao9303SizeTD2.AlgorithmName },
      { nameof(Icao9303SizeTD3Algorithm), Algorithms.Icao9303SizeTD3.AlgorithmName },
      { nameof(IsanAlgorithm), Algorithms.Isan.AlgorithmName },
      { nameof(IsinAlgorithm), Algorithms.Isin.AlgorithmName },
      { nameof(Iso6346Algorithm), Algorithms.Iso6346.AlgorithmName },
      { nameof(Iso7064CustomDanishAlgorithm), "Danish" },
      { nameof(Iso7064CustomLettersAlgorithm), "Alphabetic" },
      { nameof(Iso7064CustomNumericSupplementalAlgorithm), "Numeric" },
      { nameof(Iso7064Mod11_10Algorithm), Algorithms.Iso7064Mod11_10.AlgorithmName },
      { nameof(Iso7064Mod11_2Algorithm), Algorithms.Iso7064Mod11_2.AlgorithmName },
      { nameof(Iso7064Mod1271_36Algorithm), Algorithms.Iso7064Mod1271_36.AlgorithmName },
      { nameof(Iso7064Mod27_26Algorithm), Algorithms.Iso7064Mod27_26.AlgorithmName },
      { nameof(Iso7064Mod37_2Algorithm), Algorithms.Iso7064Mod37_2.AlgorithmName },
      { nameof(Iso7064Mod37_36Algorithm), Algorithms.Iso7064Mod37_36.AlgorithmName },
      { nameof(Iso7064Mod661_26Algorithm), Algorithms.Iso7064Mod661_26.AlgorithmName },
      { nameof(Iso7064Mod97_10Algorithm), Algorithms.Iso7064Mod97_10.AlgorithmName },
      { nameof(LuhnAlgorithm), Algorithms.Luhn.AlgorithmName },
      { nameof(Modulus10_13Algorithm), Algorithms.Modulus10_13.AlgorithmName },
      { nameof(Modulus10_1Algorithm), Algorithms.Modulus10_1.AlgorithmName },
      { nameof(Modulus10_2Algorithm), Algorithms.Modulus10_2.AlgorithmName },
      { nameof(Modulus11Algorithm), Algorithms.Modulus11.AlgorithmName },
      { nameof(NcdAlgorithm), Algorithms.Ncd.AlgorithmName },
      { nameof(NhsAlgorithm), Algorithms.Nhs.AlgorithmName },
      { nameof(NpiAlgorithm), Algorithms.Npi.AlgorithmName },
      { nameof(SedolAlgorithm), Algorithms.Sedol.AlgorithmName },
      { nameof(VerhoeffAlgorithm), Algorithms.Verhoeff.AlgorithmName },
      { nameof(VinAlgorithm), Algorithms.Vin.AlgorithmName },
   };

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Theory]
   [MemberData(nameof(AllAlgorithms))]
   public void CheckDigitValidator_Validate_ShouldPass_WhenValueHasValidCheckDigit(
      String algorithmClass,
      String _)
   {
      // Arrange.
      var request = new FooRequest
      {
         Bar = algorithmClass.ToValidRequestValue()
      };
      var validator = algorithmClass.ToFooValidator();

      // Act.
      var result = validator.Validate(request);

      // Assert.
      result.IsValid.Should().BeTrue();
   }

   [Theory]
   [MemberData(nameof(AllAlgorithms))]
   public void CheckDigitValidator_Validate_ShouldPass_WhenValueIsNull(
      String algorithmClass,
      String _)
   {
      // Arrange.
      var request = new FooRequest
      {
         Bar = null!
      };
      var validator = algorithmClass.ToFooValidator();

      // Act.
      var result = validator.Validate(request);

      // Assert.
      result.IsValid.Should().BeTrue();
   }

   [Theory]
   [MemberData(nameof(AllAlgorithms))]
   public void CheckDigitValidator_Validate_ShouldPass_WhenValueIsStringEmpty(
      String algorithmClass,
      String _)
   {
      // Arrange.
      var request = new FooRequest
      {
         Bar = String.Empty
      };
      var validator = algorithmClass.ToFooValidator();

      // Act.
      var result = validator.Validate(request);

      // Assert.
      result.IsValid.Should().BeTrue();
   }

   [Theory]
   [MemberData(nameof(AllAlgorithms))]
   public void CheckDigitValidator_Validate_ShouldPass_WhenValueIsWhiteSpace(
      String algorithmClass,
      String _)
   {
      // Arrange.
      var request = new FooRequest
      {
         Bar = "\t"
      };
      var validator = algorithmClass.ToFooValidator();

      // Act.
      var result = validator.Validate(request);

      // Assert.
      result.IsValid.Should().BeTrue();
   }

   [Theory]
   [MemberData(nameof(AllAlgorithms))]
   public void CheckDigitValidator_Validate_ShouldFail_WhenValueHasValidCheckDigit(
      String algorithmClass,
      String algorithmName)
   {
      // Arrange.
      var request = new FooRequest
      {
         Bar = algorithmClass.ToInvalidRequestValue()
      };
      var validator = algorithmClass.ToFooValidator();
      var expectedMessage =$"Bar must have valid {algorithmName} check digit(s).";

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
