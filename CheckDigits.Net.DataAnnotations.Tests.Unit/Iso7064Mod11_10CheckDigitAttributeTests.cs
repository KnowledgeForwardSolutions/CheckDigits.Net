namespace CheckDigits.Net.DataAnnotations.Tests.Unit;

public class Iso7064Mod11_10CheckDigitAttributeTests
{
   private const String _customErrorMessage = "Need a valid ISO 7064 MOD 11,10 value";

   public class Iso7064Mod11_10Request
   {
      [Iso7064Mod11_10CheckDigit]
      public String ItemIdentifier { get; set; } = null!;
   }

   public class Iso7064Mod11_10RequestCustomMessage
   {
      [Iso7064Mod11_10CheckDigit(ErrorMessage = _customErrorMessage)]
      public String ItemIdentifier { get; set; } = null!;
   }

   public class Iso7064Mod11_10RequestRequiredField
   {
      [Required, Iso7064Mod11_10CheckDigit]
      public String ItemIdentifier { get; set; } = null!;
   }

   public class Iso7064Mod11_10RequestInvalidType
   {
      [Iso7064Mod11_10CheckDigit]
      public Int32 ItemIdentifier { get; set; }
   }

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Theory]
   [InlineData("07945")]               // Example from ISO 7064 specification
   [InlineData("123456789012345678901234565")]
   public void Iso7064Mod11_10CheckDigitAttribute_Validate_ShouldReturnSuccess_WhenValueHasValidIso7064Mod11_10CheckDigit(String itemIdentifier)
   {
      // Arrange.
      var request = new Iso7064Mod11_10Request
      {
         ItemIdentifier = itemIdentifier
      };

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().BeEmpty();
   }

   [Fact]
   public void Iso7064Mod11_10CheckDigitAttribute_Validate_ShouldReturnSuccess_WhenNonRequiredValueIsNull()
   {
      // Arrange.
      var request = new Iso7064Mod11_10Request();

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      request.ItemIdentifier.Should().BeNull();
      results.Should().BeEmpty();
   }

   [Fact]
   public void Iso7064Mod11_10CheckDigitAttribute_Validate_ShouldReturnSuccess_WhenNonRequiredValueIsEmpty()
   {
      // Arrange.
      var request = new Iso7064Mod11_10Request
      {
         ItemIdentifier = String.Empty
      };

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().BeEmpty();
   }

   [Fact]
   public void Iso7064Mod11_10CheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueIsNotTypeString()
   {
      // Arrange.
      var item = new Iso7064Mod11_10RequestInvalidType
      {
         ItemIdentifier = 123456
      };
      var expectedMessage = String.Format(Messages.InvalidPropertyType, nameof(item.ItemIdentifier));

      // Act.
      var results = Utility.ValidateModel(item);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Fact]
   public void Iso7064Mod11_10CheckDigitAttribute_Validate_ShouldReturnFailure_WhenRequiredValueIsNull()
   {
      // Arrange.
      var request = new Iso7064Mod11_10RequestRequiredField();
      var expectedMessage = "The ItemIdentifier field is required.";

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Fact]
   public void Iso7064Mod11_10CheckDigitAttribute_Validate_ShouldReturnFailure_WhenRequiredValueIsEmpty()
   {
      // Arrange.
      var request = new Iso7064Mod11_10RequestRequiredField
      {
         ItemIdentifier = String.Empty
      };
      var expectedMessage = "The ItemIdentifier field is required.";

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Theory]
   [InlineData("123556788")]           // 123456788 with single digit transcription error 4 -> 5
   [InlineData("123465788")]           // 123456788 with two digit transposition error 56 -> 65 
   [InlineData("114433446")]           // 112233446 with two digit twin error 22 -> 44
   public void Iso7064Mod11_10CheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueHasInvalidIso7064Mod11_10CheckDigit(String itemIdentifier)
   {
      // Arrange.
      var request = new Iso7064Mod11_10Request
      {
         ItemIdentifier = itemIdentifier
      };
      var expectedMessage = String.Format(Messages.SingleCheckDigitFailure, nameof(request.ItemIdentifier));

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Theory]
   [InlineData("123556788")]           // 123456788 with single digit transcription error 4 -> 5
   [InlineData("123465788")]           // 123456788 with two digit transposition error 56 -> 65 
   [InlineData("114433446")]           // 112233446 with two digit twin error 22 -> 44
   public void Iso7064Mod11_10CheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueHasInvalidIso7064Mod11_10CheckDigitAndCustomErrorMessageIsSupplied(String itemIdentifier)
   {
      // Arrange.
      var request = new Iso7064Mod11_10RequestCustomMessage
      {
         ItemIdentifier = itemIdentifier
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
