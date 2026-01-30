// Ignore Spelling: Luhn

namespace CheckDigits.Net.DataAnnotations.Tests.Unit;

public class MaskedLuhnCheckDigitAttributeTests
{
   private const String _customErrorMessage = "Need a valid card number";

   public class MaskedLuhnRequest
   {
      [MaskedLuhnCheckDigit<CreditCardMask>]
      public String CardNumber { get; set; } = null!;
   }

   public class MaskedLuhnRequestCustomMessage
   {
      [MaskedLuhnCheckDigit<CreditCardMask>(ErrorMessage = _customErrorMessage)]
      public String CardNumber { get; set; } = null!;
   }

   public class MaskedLuhnRequestRequiredField
   {
      [Required, MaskedLuhnCheckDigit<CreditCardMask>]
      public String CardNumber { get; set; } = null!;
   }

   public class MaskedLuhnRequestInvalidType
   {
      [MaskedLuhnCheckDigit<CreditCardMask>]
      public Int32 CardNumber { get; set; }
   }

   public class MaskedLuhnRequestRejectAll
   {
      [MaskedLuhnCheckDigit<RejectAllMask>]
      public String CardNumber { get; set; } = null!;
   }

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Theory]
   [InlineData("3782 8224 6310 005")]     // American Express test credit card number
   [InlineData("6011 1111 1111 1117")]    // Discover test credit card number
   [InlineData("5555 5555 5555 4444")]    // MasterCard test credit card number
   [InlineData("4012 8888 8888 1881")]    // Visa test credit card number
   [InlineData("3056 9300 0902 0004")]    // Diners Club test credit card number
   [InlineData("3566 1111 1111 1113")]    // JCB test credit card number
   public void MaskedLuhnCheckDigitAttribute_Validate_ShouldReturnSuccess_WhenValueHasValidLuhnCheckDigit(String cardNumber)
   {
      // Arrange.
      var request = new MaskedLuhnRequest
      {
         CardNumber = cardNumber
      };

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().BeEmpty();
   }

   [Fact]
   public void MaskedLuhnCheckDigitAttribute_Validate_ShouldReturnSuccess_WhenNonRequiredValueIsNull()
   {
      // Arrange.
      var request = new MaskedLuhnRequest();

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      request.CardNumber.Should().BeNull();
      results.Should().BeEmpty();
   }

   [Fact]
   public void MaskedLuhnCheckDigitAttribute_Validate_ShouldReturnSuccess_WhenNonRequiredValueIsEmpty()
   {
      // Arrange.
      var request = new MaskedLuhnRequest
      {
         CardNumber = String.Empty
      };

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().BeEmpty();
   }

   [Fact]
   public void MaskedLuhnCheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueIsNotTypeString()
   {
      // Arrange.
      var item = new MaskedLuhnRequestInvalidType
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
   public void MaskedLuhnCheckDigitAttribute_Validate_ShouldReturnFailure_WhenRequiredValueIsNull()
   {
      // Arrange.
      var request = new MaskedLuhnRequestRequiredField();
      var expectedMessage = "The CardNumber field is required.";

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Fact]
   public void MaskedLuhnCheckDigitAttribute_Validate_ShouldReturnFailure_WhenRequiredValueIsEmpty()
   {
      // Arrange.
      var request = new MaskedLuhnRequestRequiredField
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
   [InlineData("5558 5555 5555 4444")]    // MasterCard test card number with single digit transcription error 5 -> 8
   [InlineData("3059 6300 0902 0004")]    // Diners Club test card number with two digit transposition error 69 -> 96 
   [InlineData("3566 1111 4411 1113")]    // JCB test card number with two digit twin error 11 -> 44
   public void MaskedLuhnCheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueHasInvalidLuhnCheckDigit(String cardNumber)
   {
      // Arrange.
      var request = new MaskedLuhnRequest
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
   [InlineData("5558 5555 5555 4444")]    // MasterCard test card number with single digit transcription error 5 -> 8
   [InlineData("3059 6300 0902 0004")]    // Diners Club test card number with two digit transposition error 69 -> 96 
   [InlineData("3566 1111 4411 1113")]    // JCB test card number with two digit twin error 11 -> 44
   public void MaskedLuhnCheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueHasInvalidLuhnCheckDigitAndCustomErrorMessageIsSupplied(String cardNumber)
   {
      // Arrange.
      var request = new MaskedLuhnRequestCustomMessage
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

   [Fact]
   public void MaskedLuhnCheckDigitAttribute_Validate_ShouldReturnFailure_WhenMaskRejectsAllValues()
   {
      // Arrange.
      var item = new MaskedLuhnRequestRejectAll
      {
         CardNumber = "3782 8224 6310 005"
      };
      var expectedMessage = String.Format(Messages.SingleCheckDigitFailure, nameof(item.CardNumber));
      // Act.
      var results = Utility.ValidateModel(item);
      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   #endregion
}
