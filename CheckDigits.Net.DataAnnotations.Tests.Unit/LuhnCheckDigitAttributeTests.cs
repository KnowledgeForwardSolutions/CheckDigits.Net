// Ignore Spelling: Luhn

namespace CheckDigits.Net.DataAnnotations.Tests.Unit;

public class LuhnCheckDigitAttributeTests
{
   public class PaymentRequest
   {
      [LuhnCheckDigit]
      public String CardNumber { get; set; } = null!;
   }

   public class OrderItem
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
   public void LuhnCheckDigitAttribute_Validate_ShouldReturnSuccess_WhenValueIsNull()
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
   public void LuhnCheckDigitAttribute_Validate_ShouldReturnSuccess_WhenValueIsEmpty()
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
      var item = new OrderItem
      {
         ItemCode = 123456
      };

      // Act.
      var results = Utility.ValidateModel(item);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be("The property 'ItemCode' is of an invalid type for check digit validation.");
   }

   [Fact]
   public void LuhnCheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueHasInvalidLuhnCheckDigit()
   {
      // Arrange.
      var request = new PaymentRequest
      {
         CardNumber = "4539148803436468"
      };

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be("The field CardNumber fails Luhn check digit validation");
   }

   #endregion
}
