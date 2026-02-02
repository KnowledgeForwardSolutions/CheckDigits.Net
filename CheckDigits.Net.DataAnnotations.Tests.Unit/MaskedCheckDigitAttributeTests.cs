namespace CheckDigits.Net.DataAnnotations.Tests.Unit;

public class MaskedCheckDigitAttributeTests
{
   private const String _customErrorMessage = "Check digit validation failed";

   public class CustomMessageRequest
   {
      [MaskedCheckDigit<LuhnAlgorithm, CreditCardMask>(ErrorMessage = _customErrorMessage)]
      public String CardNumber { get; set; } = null!;
   }

   public class RequiredFieldRequest
   {
      [Required, MaskedCheckDigit<LuhnAlgorithm, CreditCardMask>]
      public String CardNumber { get; set; } = null!;
   }

   public class IntegerTypeRequest
   {
      [MaskedCheckDigit<LuhnAlgorithm, CreditCardMask>]
      public Int32 ItemNumber { get; set; }
   }

   public class RejectAllRequest
   {
      [MaskedCheckDigit<LuhnAlgorithm, RejectAllMask>]
      public String CardNumber { get; set; } = null!;
   }

   public static TheoryData<String> AllAlgorithms => new()
   {
      { nameof(LuhnAlgorithm) },
   };

   #region GetValidationResult Tests
   // ==========================================================================
   // ==========================================================================

   [Theory]
   [MemberData(nameof(AllAlgorithms))]
   public void MaskedCheckDigitAttribute_GetValidationResult_ShouldReturnNull_WhenValueHasValidCheckDigit(String algorithmName)
   {
      // Arrange.
      var sut = algorithmName.ToMaskedCheckDigitAttribute();
      var value = algorithmName.ToValidMaskedRequestValue();
      var validationContext = new ValidationContext(new Object());

      // Act.
      var result = sut.GetValidationResult(value, validationContext);

      // Assert.
      result.Should().BeNull();
   }

   [Theory]
   [MemberData(nameof(AllAlgorithms))]
   public void MaskedCheckDigitAttribute_GetValidationResult_ShouldReturnFailure_WhenValueHasInvalidCheckDigit(String algorithmName)
   {
      // Arrange.
      var sut = algorithmName.ToMaskedCheckDigitAttribute();
      var value = algorithmName.ToInvalidMaskedRequestValue();
      var validationContext = new ValidationContext(new Object());

      // Act.
      var result = sut.GetValidationResult(value, validationContext);

      // Assert.
      result.Should().NotBe(ValidationResult.Success);
   }

   [Fact]
   public void MaskedCheckDigitAttribute_GetValidationResult_ShouldReturnFailure_WhenMaskRejectsAllInput()
   {
      // Arrange.
      var sut = new MaskedCheckDigitAttribute<LuhnAlgorithm, RejectAllMask>();
      var value = "5558 5555 5555 4444";                // MasterCard test card number
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
   public void MaskedCheckDigitAttribute_IsValid_ShouldReturnTrue_WhenValueHasValidCheckDigit(String algorithmName)
   {
      // Arrange.
      var sut = algorithmName.ToMaskedCheckDigitAttribute();
      var value = algorithmName.ToValidMaskedRequestValue();

      // Act.
      var result = sut.IsValid(value);

      // Assert.
      result.Should().BeTrue();
   }

   [Theory]
   [MemberData(nameof(AllAlgorithms))]
   public void MaskedCheckDigitAttribute_IsValid_ShouldReturnFalse_WhenValueHasInvalidCheckDigit(String algorithmName)
   {
      // Arrange.
      var sut = algorithmName.ToMaskedCheckDigitAttribute();
      var value = algorithmName.ToInvalidMaskedRequestValue();

      // Act.
      var result = sut.IsValid(value);

      // Assert.
      result.Should().BeFalse();
   }

   [Fact]
   public void MaskedCheckDigitAttribute_IsValid_ShouldReturnFalse_WhenMaskRejectsAllInput()
   {
      // Arrange.
      var sut = new MaskedCheckDigitAttribute<LuhnAlgorithm, RejectAllMask>();
      var value = "5558 5555 5555 4444";                // MasterCard test card number

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
   public void MaskedCheckDigitAttribute_Validate_ShouldReturnSuccess_WhenValueHasValidCheckDigit(String algorithmName)
   {
      // Arrange.
      var request = algorithmName.ToMaskedFooRequest();
      request.BarValue = algorithmName.ToValidMaskedRequestValue();

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().BeEmpty();
   }

   [Theory]
   [MemberData(nameof(AllAlgorithms))]
   public void MaskedCheckDigitAttribute_Validate_ShouldReturnSuccess_WhenNonRequiredValueIsNull(String algorithmName)
   {
      // Arrange.
      var request = algorithmName.ToMaskedFooRequest();

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().BeEmpty();
   }

   [Theory]
   [MemberData(nameof(AllAlgorithms))]
   public void MaskedCheckDigitAttribute_Validate_ShouldReturnSuccess_WhenNonRequiredValueIsEmpty(String algorithmName)
   {
      // Arrange.
      var request = algorithmName.ToMaskedFooRequest();
      request.BarValue = String.Empty;

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().BeEmpty();
   }

   [Fact]
   public void MaskedCheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueIsNotTypeString()
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
   public void MaskedCheckDigitAttribute_Validate_ShouldReturnFailure_WhenRequiredValueIsNull()
   {
      // Arrange.
      var request = new RequiredFieldRequest();
      var expectedMessage = "The CardNumber field is required.";

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
   public void MaskedCheckDigitAttribute_Validate_ShouldReturnFailure_WhenRequiredValueIsEmptyOrWhitespace(String cardNumber)
   {
      // Arrange.
      var request = new RequiredFieldRequest
      {
         CardNumber = cardNumber
      };
      var expectedMessage = "The CardNumber field is required.";

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
   public void MaskedCheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueHasInvalidCheckDigitAndCustomErrorMessageIsSupplied()
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

   [Fact]
   public void MaskedCheckDigitAttribute_Validate_ShouldReturnFailure_WhenMaskRejectsAllInput()
   {
      // Arrange.
      var request = new RejectAllRequest
      {
         CardNumber = "5558 5555 5555 4444"           // MasterCard test card number with single digit transcription error 5 -> 8
      };
      var expectedMessage = "The field CardNumber is invalid.";

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   #endregion

#endif
}
