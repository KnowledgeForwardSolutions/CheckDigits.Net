// Ignore Spelling: Luhn

namespace CheckDigits.Net.DataAnnotations.Tests.Unit;

public class LuhnCheckDigitAttributeTests
{
   private const String _customErrorMessage = "Need a valid card number";

   public class PaymentRequest
   {
      [LuhnCheckDigit]
      public String CardNumber { get; set; } = null!;
   }

   public class PaymentRequestCustomMessage
   {
      [LuhnCheckDigit(ErrorMessage = _customErrorMessage)]
      public String CardNumber { get; set; } = null!;
   }

   public class RequiredPaymentRequest
   {
      [Required, LuhnCheckDigit]
      public String CardNumber { get; set; } = null!;
   }

   public class PaymentRequestInvalidType
   {
      [LuhnCheckDigit]
      public Int32 ItemCode { get; set; }
   }

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void LuhnCheckDigitAttribute_Validate_ShouldReturnSuccess_WhenValueHasValidLuhnCheckDigit()
   {
      // Arrange.
      var request = new PaymentRequest
      {
         CardNumber = "4539148803436467" // Valid Luhn number
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
      var request = new PaymentRequest();

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
      var request = new PaymentRequest
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
      var item = new PaymentRequestInvalidType
      {
         ItemCode = 123456
      };
      var expectedMessage = String.Format(Messages.InvalidPropertyType, nameof(item.ItemCode));

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
      var request = new RequiredPaymentRequest();
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
      var request = new RequiredPaymentRequest 
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

   [Fact]
   public void LuhnCheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueHasInvalidLuhnCheckDigit()
   {
      // Arrange.
      var request = new PaymentRequest
      {
         CardNumber = "4539148803436468"
      };
      var expectedMessage = String.Format(Messages.SingleCheckDigitFailure, nameof(request.CardNumber));

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Fact]
   public void LuhnCheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueHasInvalidLuhnCheckDigitAndCustomErrorMessageIsSupplied()
   {
      // Arrange.
      var request = new PaymentRequestCustomMessage
      {
         CardNumber = "4539148803436468"
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
