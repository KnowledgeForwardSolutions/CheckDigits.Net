// Ignore Spelling: Aadhaar, Verhoeff

namespace CheckDigits.Net.DataAnnotations.Tests.Unit;

public class VerhoeffCheckDigitAttributeTests
{
   private const String _customErrorMessage = "Need a valid Aadhaar ID number";

   public class PersonRequest
   {
      [VerhoeffCheckDigit]
      public String AadhaarIdNumber { get; set; } = null!;
   }

   public class PersonRequestCustomMessage
   {
      [VerhoeffCheckDigit(ErrorMessage = _customErrorMessage)]
      public String AadhaarIdNumber { get; set; } = null!;
   }

   public class RequiredPersonRequest
   {
      [Required, VerhoeffCheckDigit]
      public String AadhaarIdNumber { get; set; } = null!;
   }

   public class PersonRequestInvalidType
   {
      [VerhoeffCheckDigit]
      public Int32 AadhaarIdNumber { get; set; }
   }

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Theory]
   [InlineData("2363")]                      // Worked example from Wikipedia
   [InlineData("123451")]                    // Test data from https://rosettacode.org/wiki/Verhoeff_algorithm
   [InlineData("84736430954837284567892")]   // Test data from https://codereview.stackexchange.com/questions/221229/verhoeff-check-digit-algorithm
   public void VerhoeffCheckDigitAttribute_Validate_ShouldReturnSuccess_WhenValueHasValidVerhoeffCheckDigit(String value)
   {
      // Arrange.
      var request = new PersonRequest
      {
         AadhaarIdNumber = value
      };

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().BeEmpty();
   }

   [Fact]
   public void VerhoeffCheckDigitAttribute_Validate_ShouldReturnSuccess_WhenNonRequiredValueIsNull()
   {
      // Arrange.
      var request = new PersonRequest();

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      request.AadhaarIdNumber.Should().BeNull();
      results.Should().BeEmpty();
   }

   [Fact]
   public void VerhoeffCheckDigitAttribute_Validate_ShouldReturnSuccess_WhenNonRequiredValueIsEmpty()
   {
      // Arrange.
      var request = new PersonRequest
      {
         AadhaarIdNumber = String.Empty
      };

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().BeEmpty();
   }

   [Fact]
   public void VerhoeffCheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueIsNotTypeString()
   {
      // Arrange.
      var item = new PersonRequestInvalidType
      {
         AadhaarIdNumber = 123456
      };
      var expectedMessage = String.Format(Messages.InvalidPropertyType, nameof(item.AadhaarIdNumber));

      // Act.
      var results = Utility.ValidateModel(item);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Fact]
   public void VerhoeffCheckDigitAttribute_Validate_ShouldReturnFailure_WhenRequiredValueIsNull()
   {
      // Arrange.
      var request = new RequiredPersonRequest();
      var expectedMessage = "The AadhaarIdNumber field is required.";

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Fact]
   public void VerhoeffCheckDigitAttribute_Validate_ShouldReturnFailure_WhenRequiredValueIsEmpty()
   {
      // Arrange.
      var request = new RequiredPersonRequest
      {
         AadhaarIdNumber = String.Empty
      };
      var expectedMessage = "The AadhaarIdNumber field is required.";

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Theory]
   [InlineData("84736430459837284567892")]   // Jump transposition error using "84736430954837284567892" as a valid value (954 -> 459)
   [InlineData("112255445566778899009")]     // Twin error using "112233445566778899009" as a valid value (33 -> 55)
   public void VerhoeffCheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueHasInvalidVerhoeffCheckDigit(String value)
   {
      // Arrange.
      var request = new PersonRequest
      {
         AadhaarIdNumber = value
      };
      var expectedMessage = String.Format(Messages.SingleCheckDigitFailure, nameof(request.AadhaarIdNumber));

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Theory]
   [InlineData("84736430459837284567892")]   // Jump transposition error using "84736430954837284567892" as a valid value (954 -> 459)
   [InlineData("112255445566778899009")]     // Twin error using "112233445566778899009" as a valid value (33 -> 55)
   public void VerhoeffCheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueHasInvalidVerhoeffCheckDigitAndCustomErrorMessageIsSupplied(String value)
   {
      // Arrange.
      var request = new PersonRequestCustomMessage
      {
         AadhaarIdNumber = value
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
