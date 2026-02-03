// Ignore Spelling: Luhn

namespace CheckDigits.Net.DataAnnotations.Tests.Unit;

public class CheckDigitAttributeTests
{
   private const String _customErrorMessage = "Check digit validation failed";

   public class CustomMessageRequest
   {
      [CheckDigit<LuhnAlgorithm>(ErrorMessage = _customErrorMessage)]
      public String CardNumber { get; set; } = null!;
   }

   public class RequiredFieldRequest
   {
      [Required, CheckDigit<Modulus10_13Algorithm>]
      public String Upc { get; set; } = null!;
   }

   public class IntegerTypeRequest
   {
      [CheckDigit<LuhnAlgorithm>]
      public Int32 ItemNumber { get; set; }
   }

   public static TheoryData<String> AllAlgorithms => new()
   {
      { nameof(AlphanumericMod97_10Algorithm) },
      { nameof(AbaRtnAlgorithm) },
      { nameof(CusipAlgorithm) },
      { nameof(DammAlgorithm) },
      { nameof(FigiAlgorithm) },
      { nameof(IbanAlgorithm) },
      { nameof(Icao9303Algorithm) },
      { nameof(Icao9303MachineReadableVisaAlgorithm) },
      { nameof(Icao9303SizeTD1Algorithm) },
      { nameof(Icao9303SizeTD2Algorithm) },
      { nameof(Icao9303SizeTD3Algorithm) },
      { nameof(IsanAlgorithm) },
      { nameof(IsinAlgorithm) },
      { nameof(Iso6346Algorithm) },
      { nameof(Iso7064CustomDanishAlgorithm) },
      { nameof(Iso7064CustomLettersAlgorithm) },
      { nameof(Iso7064CustomNumericSupplementalAlgorithm) },
      { nameof(Iso7064Mod11_10Algorithm) },
      { nameof(Iso7064Mod11_2Algorithm) },
      { nameof(Iso7064Mod1271_36Algorithm) },
      { nameof(Iso7064Mod27_26Algorithm) },
      { nameof(Iso7064Mod37_2Algorithm) },
      { nameof(Iso7064Mod37_36Algorithm) },
      { nameof(Iso7064Mod661_26Algorithm) },
      { nameof(Iso7064Mod97_10Algorithm) },
      { nameof(LuhnAlgorithm) },
      { nameof(Modulus10_13Algorithm) },
      { nameof(Modulus10_1Algorithm) },
      { nameof(Modulus10_2Algorithm) },
      { nameof(Modulus11Algorithm) },
      { nameof(NcdAlgorithm) },
      { nameof(NhsAlgorithm) },
      { nameof(NpiAlgorithm) },
      { nameof(SedolAlgorithm) },
      { nameof(VerhoeffAlgorithm) },
      { nameof(VinAlgorithm) },
   };

   #region GetValidationResult Tests
   // ==========================================================================
   // ==========================================================================

   [Theory]
   [MemberData(nameof(AllAlgorithms))]
   public void CheckDigitAttribute_GetValidationResult_ShouldReturnNull_WhenValueHasValidCheckDigit(String algorithmName)
   {
      // Arrange.
      var sut = algorithmName.ToCheckDigitAttribute();
      var value = algorithmName.ToValidRequestValue();
      var validationContext = new ValidationContext(new Object());

      // Act.
      var result = sut.GetValidationResult(value, validationContext);

      // Assert.
      result.Should().BeNull();
   }

   [Theory]
   [MemberData(nameof(AllAlgorithms))]
   public void CheckDigitAttribute_GetValidationResult_ShouldReturnFailure_WhenValueHasInvalidCheckDigit(String algorithmName)
   {
      // Arrange.
      var sut = algorithmName.ToCheckDigitAttribute();
      var value = algorithmName.ToInvalidRequestValue();
      var validationContext = new ValidationContext(new Object());

      // Act.
      var result = sut.GetValidationResult(value, validationContext);

      // Assert.
      result.Should().NotBe(ValidationResult.Success);
   }

   #endregion

   #region IsValid Tests
   // ==========================================================================
   // ==========================================================================

   [Theory]
   [MemberData(nameof(AllAlgorithms))]
   public void CheckDigitAttribute_IsValid_ShouldReturnTrue_WhenValueHasValidCheckDigit(String algorithmName)
   {
      // Arrange.
      var sut = algorithmName.ToCheckDigitAttribute();
      var value = algorithmName.ToValidRequestValue();

      // Act.
      var result = sut.IsValid(value);

      // Assert.
      result.Should().BeTrue();
   }

   [Theory]
   [MemberData(nameof(AllAlgorithms))]
   public void CheckDigitAttribute_IsValid_ShouldReturnFalse_WhenValueHasInvalidCheckDigit(String algorithmName)
   {
      // Arrange.
      var sut = algorithmName.ToCheckDigitAttribute();
      var value = algorithmName.ToInvalidRequestValue();

      // Act.
      var result = sut.IsValid(value);

      // Assert.
      result.Should().BeFalse();
   }

   #endregion

#if NET8_0_OR_GREATER
   // Using Validator.TryValidateObject (via Utility.ValidateModel) does not seem
   // to work as expected in .Net Framework 4.8, so these tests are limited to .Net 8.0 and later.

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Theory]
   [MemberData(nameof(AllAlgorithms))]
   public void CheckDigitAttribute_Validate_ShouldReturnSuccess_WhenValueHasValidCheckDigit(String algorithmName)
   {
      // Arrange.
      var request = algorithmName.ToFooRequest();
      request.BarValue = algorithmName.ToValidRequestValue();

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().BeEmpty();
   }

   [Theory]
   [MemberData(nameof(AllAlgorithms))]
   public void CheckDigitAttribute_Validate_ShouldReturnSuccess_WhenNonRequiredValueIsNull(String algorithmName)
   {
      // Arrange.
      var request = algorithmName.ToFooRequest();

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().BeEmpty();
   }

   [Theory]
   [MemberData(nameof(AllAlgorithms))]
   public void CheckDigitAttribute_Validate_ShouldReturnSuccess_WhenNonRequiredValueIsEmpty(String algorithmName)
   {
      // Arrange.
      var request = algorithmName.ToFooRequest();
      request.BarValue = String.Empty;

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().BeEmpty();
   }

   [Fact]
   public void CheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueIsNotTypeString()
   {
      // Arrange.
      var item = new IntegerTypeRequest
      {
         ItemNumber = 123456
      };
      var expectedMessage = String.Format(Messages.InvalidPropertyType, nameof(item.ItemNumber));

      // Act.
      var results = Utility.ValidateModel(item);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Fact]
   public void CheckDigitAttribute_Validate_ShouldReturnFailure_WhenRequiredValueIsNull()
   {
      // Arrange.
      var request = new RequiredFieldRequest();
      var expectedMessage = "The Upc field is required.";

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Theory]
   [InlineData("")]
   [InlineData("  ")]
   [InlineData("\t")]
   public void CheckDigitAttribute_Validate_ShouldReturnFailure_WhenRequiredValueIsEmptyOrWhitespace(String upc)
   {
      // Arrange.
      var request = new RequiredFieldRequest
      {
         Upc = upc
      };
      var expectedMessage = "The Upc field is required.";

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Theory]
   [MemberData(nameof(AllAlgorithms))]
   public void CheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueHasInvalidCheckDigit(String algorithmName)
   {
      var request = algorithmName.ToFooRequest();
      request.BarValue = algorithmName.ToInvalidRequestValue();
      var expectedMessage = "The field BarValue is invalid.";

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Fact]
   public void CheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueHasInvalidCheckDigitAndCustomErrorMessageIsSupplied()
   {
      // Arrange.
      var request = new CustomMessageRequest
      {
         CardNumber = "5558555555554444"           // MasterCard test card number with single digit transcription error 5 -> 8
      };
      var expectedMessage = _customErrorMessage;

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   #endregion

#endif
}
