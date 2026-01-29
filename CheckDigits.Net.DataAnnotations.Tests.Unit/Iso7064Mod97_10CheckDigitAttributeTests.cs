namespace CheckDigits.Net.DataAnnotations.Tests.Unit;

public class Iso7064Mod97_10CheckDigitAttributeTests
{
   private const String _customErrorMessage = "Need a valid ISO 7064 MOD 97-10 value";

   public class Iso7064Mod97_10Request
   {
      [Iso7064Mod97_10CheckDigit]
      public String ItemIdentifier { get; set; } = null!;
   }

   public class Iso7064Mod97_10RequestCustomMessage
   {
      [Iso7064Mod97_10CheckDigit(ErrorMessage = _customErrorMessage)]
      public String ItemIdentifier { get; set; } = null!;
   }

   public class Iso7064Mod97_10RequestRequiredField
   {
      [Required, Iso7064Mod97_10CheckDigit]
      public String ItemIdentifier { get; set; } = null!;
   }

   public class Iso7064Mod97_10RequestInvalidType
   {
      [Iso7064Mod97_10CheckDigit]
      public Int32 ItemIdentifier { get; set; }
   }

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Theory]
   [InlineData("12345676")]
   [InlineData("163217581835191038")]
   [InlineData("1011339391255432926101144229991433338")]    // Example from https://www.consumerfinance.gov/rules-policy/regulations/1003/c/#e7e616a4bd15acce7589cbedc4fd01fcc9623f60e4263be834c9e438
   public void Iso7064Mod97_10CheckDigitAttribute_Validate_ShouldReturnSuccess_WhenValueHasValidIso7064Mod97_10CheckDigit(String itemIdentifier)
   {
      // Arrange.
      var request = new Iso7064Mod97_10Request
      {
         ItemIdentifier = itemIdentifier
      };

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().BeEmpty();
   }

   [Fact]
   public void Iso7064Mod97_10CheckDigitAttribute_Validate_ShouldReturnSuccess_WhenNonRequiredValueIsNull()
   {
      // Arrange.
      var request = new Iso7064Mod97_10Request();

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      request.ItemIdentifier.Should().BeNull();
      results.Should().BeEmpty();
   }

   [Fact]
   public void Iso7064Mod97_10CheckDigitAttribute_Validate_ShouldReturnSuccess_WhenNonRequiredValueIsEmpty()
   {
      // Arrange.
      var request = new Iso7064Mod97_10Request
      {
         ItemIdentifier = String.Empty
      };

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().BeEmpty();
   }

   [Fact]
   public void Iso7064Mod97_10CheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueIsNotTypeString()
   {
      // Arrange.
      var item = new Iso7064Mod97_10RequestInvalidType
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
   public void Iso7064Mod97_10CheckDigitAttribute_Validate_ShouldReturnFailure_WhenRequiredValueIsNull()
   {
      // Arrange.
      var request = new Iso7064Mod97_10RequestRequiredField();
      var expectedMessage = "The ItemIdentifier field is required.";

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Fact]
   public void Iso7064Mod97_10CheckDigitAttribute_Validate_ShouldReturnFailure_WhenRequiredValueIsEmpty()
   {
      // Arrange.
      var request = new Iso7064Mod97_10RequestRequiredField
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
   [InlineData("163217541835191038")]     // 163217581835191038 with single char transcription error 8 -> 4
   [InlineData("12455676")]               // 12345676 with two char transposition error 34 -> 45 
   [InlineData("1022339391255432926101144229991433338")] // 1011339391255432926101144229991433338 with two char twin error 11 -> 22
   [InlineData("12365476")]               // 12345676 with jump transposition error 456 -> 654
   [InlineData("816321758183519103")]     // 163217581835191038 with circular shift error
   [InlineData("16321758i835191038")]     // 163217581835191038 with invalid character
   public void Iso7064Mod97_10CheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueHasInvalidIso7064Mod97_10CheckDigit(String itemIdentifier)
   {
      // Arrange.
      var request = new Iso7064Mod97_10Request
      {
         ItemIdentifier = itemIdentifier
      };
      var expectedMessage = String.Format(Messages.MultiCheckDigitFailure, nameof(request.ItemIdentifier));

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Theory]
   [InlineData("163217541835191038")]     // 163217581835191038 with single char transcription error 8 -> 4
   [InlineData("12455676")]               // 12345676 with two char transposition error 34 -> 45 
   [InlineData("1022339391255432926101144229991433338")] // 1011339391255432926101144229991433338 with two char twin error 11 -> 22
   [InlineData("12365476")]               // 12345676 with jump transposition error 456 -> 654
   [InlineData("816321758183519103")]     // 163217581835191038 with circular shift error
   [InlineData("16321758i835191038")]     // 163217581835191038 with invalid character
   public void Iso7064Mod97_10CheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueHasInvalidIso7064Mod97_10CheckDigitAndCustomErrorMessageIsSupplied(String itemIdentifier)
   {
      // Arrange.
      var request = new Iso7064Mod97_10RequestCustomMessage
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
