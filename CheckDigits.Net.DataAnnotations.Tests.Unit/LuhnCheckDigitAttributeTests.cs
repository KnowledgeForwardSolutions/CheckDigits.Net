// Ignore Spelling: Luhn

namespace CheckDigits.Net.DataAnnotations.Tests.Unit;

public class LuhnCheckDigitAttributeTests
{
   private const String _customErrorMessage = "Need a valid card number";

   public class LuhnRequest
   {
      [LuhnCheckDigit]
      public String CardNumber { get; set; } = null!;
   }

   public class LuhnRequestCustomMessage
   {
      [LuhnCheckDigit(ErrorMessage = _customErrorMessage)]
      public String CardNumber { get; set; } = null!;
   }

   public class LuhnRequestRequiredField
   {
      [Required, LuhnCheckDigit]
      public String CardNumber { get; set; } = null!;
   }

   public class LuhnRequestInvalidType
   {
      [LuhnCheckDigit]
      public Int32 CardNumber { get; set; }
   }

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Theory]
   [InlineData("5555555555554444")]    // MasterCard test credit card number
   [InlineData("4012888888881881")]    // Visa test credit card number
   [InlineData("3056930009020004")]    // Diners Club test credit card number
   [InlineData("808401234567893")]     // NPI (National Provider Identifier), including 80840 prefix
   [InlineData("490154203237518")]     // IMEI (International Mobile Equipment Identity)
   [InlineData("293443438")]           // Canadian Social Insurance Number from https://www.ibm.com/docs/en/sga?topic=patterns-canada-social-insurance-number
   public void LuhnCheckDigitAttribute_Validate_ShouldReturnSuccess_WhenValueHasValidLuhnCheckDigit(String cardNumber)
   {
      // Arrange.
      var request = new LuhnRequest
      {
         CardNumber = cardNumber
      };

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().BeEmpty();
   }

   [Fact]
   public void LuhnCheckDigitAttribute_Validate_ShouldReturnSuccess_WhenNonRequiredValueIsNull()
   {
      // Arrange.
      var request = new LuhnRequest();

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      request.CardNumber.Should().BeNull();
      results.Should().BeEmpty();
   }

   [Fact]
   public void LuhnCheckDigitAttribute_Validate_ShouldReturnSuccess_WhenNonRequiredValueIsEmpty()
   {
      // Arrange.
      var request = new LuhnRequest
      {
         CardNumber = String.Empty
      };

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().BeEmpty();
   }

   [Fact]
   public void LuhnCheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueIsNotTypeString()
   {
      // Arrange.
      var item = new LuhnRequestInvalidType
      {
         CardNumber = 123456
      };
      var expectedMessage = String.Format(Messages.InvalidPropertyType, nameof(item.CardNumber));

      // Act.
      var results = Utility.ValidateModel(item);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Fact]
   public void LuhnCheckDigitAttribute_Validate_ShouldReturnFailure_WhenRequiredValueIsNull()
   {
      // Arrange.
      var request = new LuhnRequestRequiredField();
      var expectedMessage = "The CardNumber field is required.";

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Fact]
   public void LuhnCheckDigitAttribute_Validate_ShouldReturnFailure_WhenRequiredValueIsEmpty()
   {
      // Arrange.
      var request = new LuhnRequestRequiredField 
      { 
         CardNumber = String.Empty 
      };
      var expectedMessage = "The CardNumber field is required.";

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Theory]
   [InlineData("5558555555554444")]    // MasterCard test card number with single digit transcription error 5 -> 8
   [InlineData("3059630009020004")]    // Diners Club test card number with two digit transposition error 69 -> 96 
   [InlineData("5559955555554444")]    // MasterCard test card number with two digit twin error 55 -> 99
   public void LuhnCheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueHasInvalidLuhnCheckDigit(String cardNumber)
   {
      // Arrange.
      var request = new LuhnRequest
      {
         CardNumber = cardNumber
      };
      var expectedMessage = String.Format(Messages.SingleCheckDigitFailure, nameof(request.CardNumber));

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Theory]
   [InlineData("5558555555554444")]    // MasterCard test card number with single digit transcription error 5 -> 8
   [InlineData("3059630009020004")]    // Diners Club test card number with two digit transposition error 69 -> 96 
   [InlineData("5559955555554444")]    // MasterCard test card number with two digit twin error 55 -> 99
   public void LuhnCheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueHasInvalidLuhnCheckDigitAndCustomErrorMessageIsSupplied(String cardNumber)
   {
      // Arrange.
      var request = new LuhnRequestCustomMessage
      {
         CardNumber = cardNumber
      };
      var expectedMessage = _customErrorMessage;

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   #endregion
}
