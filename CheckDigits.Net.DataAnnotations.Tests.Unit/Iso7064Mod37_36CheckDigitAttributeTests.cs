namespace CheckDigits.Net.DataAnnotations.Tests.Unit;

public class Iso7064Mod37_36CheckDigitAttributeTests
{
   private const String _customErrorMessage = "Need a valid Global Release Identifier";

   public class Iso7064Mod37_36Request
   {
      [Iso7064Mod37_36CheckDigit]
      public String GlobalReleaseIdentifier { get; set; } = null!;
   }

   public class Iso7064Mod37_36RequestCustomMessage
   {
      [Iso7064Mod37_36CheckDigit(ErrorMessage = _customErrorMessage)]
      public String GlobalReleaseIdentifier { get; set; } = null!;
   }

   public class Iso7064Mod37_36RequestRequiredField
   {
      [Required, Iso7064Mod37_36CheckDigit]
      public String GlobalReleaseIdentifier { get; set; } = null!;
   }

   public class Iso7064Mod37_36RequestInvalidType
   {
      [Iso7064Mod37_36CheckDigit]
      public Int32 GlobalReleaseIdentifier { get; set; }
   }

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Theory]
   [InlineData("A12425GABC1234002M")]           // Example Global Release Identifier https://en.wikipedia.org/wiki/Global_Release_Identifier
   [InlineData("00000000C36D002B00000000E")]    // Full ISAN for Star Trek episode "Amok Time"
   [InlineData("00000000C36D002BK")]            // ISAN root for Star Trek episode "Amok Time"
   public void Iso7064Mod37_36CheckDigitAttribute_Validate_ShouldReturnSuccess_WhenValueHasValidIso7064Mod37_36CheckDigit(String globalRelease)
   {
      // Arrange.
      var request = new Iso7064Mod37_36Request
      {
         GlobalReleaseIdentifier = globalRelease
      };

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().BeEmpty();
   }

   [Fact]
   public void Iso7064Mod37_36CheckDigitAttribute_Validate_ShouldReturnSuccess_WhenNonRequiredValueIsNull()
   {
      // Arrange.
      var request = new Iso7064Mod37_36Request();

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      request.GlobalReleaseIdentifier.Should().BeNull();
      results.Should().BeEmpty();
   }

   [Fact]
   public void Iso7064Mod37_36CheckDigitAttribute_Validate_ShouldReturnSuccess_WhenNonRequiredValueIsEmpty()
   {
      // Arrange.
      var request = new Iso7064Mod37_36Request
      {
         GlobalReleaseIdentifier = String.Empty
      };

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().BeEmpty();
   }

   [Fact]
   public void Iso7064Mod37_36CheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueIsNotTypeString()
   {
      // Arrange.
      var item = new Iso7064Mod37_36RequestInvalidType
      {
         GlobalReleaseIdentifier = 123456
      };
      var expectedMessage = String.Format(Messages.InvalidPropertyType, nameof(item.GlobalReleaseIdentifier));

      // Act.
      var results = Utility.ValidateModel(item);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Fact]
   public void Iso7064Mod37_36CheckDigitAttribute_Validate_ShouldReturnFailure_WhenRequiredValueIsNull()
   {
      // Arrange.
      var request = new Iso7064Mod37_36RequestRequiredField();
      var expectedMessage = "The GlobalReleaseIdentifier field is required.";

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Fact]
   public void Iso7064Mod37_36CheckDigitAttribute_Validate_ShouldReturnFailure_WhenRequiredValueIsEmpty()
   {
      // Arrange.
      var request = new Iso7064Mod37_36RequestRequiredField
      {
         GlobalReleaseIdentifier = String.Empty
      };
      var expectedMessage = "The GlobalReleaseIdentifier field is required.";

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Theory]
   [InlineData("QWERTUDVORAK1")]                // QWERTYDVORAK1 with single char transcription error Y -> U
   [InlineData("A1B2C3D4E5F67GH8I9J0KI")]       // A1B2C3D4E5F6G7H8I9J0KI with two char transposition error G7 -> 7G 
   [InlineData("QWERDYTVORAK1")]                // QWERTYDVORAK1 with jump transposition error TYD -> DYT
   [InlineData("EIOUUA")]                       // AEIOUU with circular shift error
   public void Iso7064Mod37_36CheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueHasInvalidIso7064Mod37_36CheckDigit(String globalRelease)
   {
      // Arrange.
      var request = new Iso7064Mod37_36Request
      {
         GlobalReleaseIdentifier = globalRelease
      };
      var expectedMessage = String.Format(Messages.SingleCheckDigitFailure, nameof(request.GlobalReleaseIdentifier));

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Theory]
   [InlineData("QWERTUDVORAK1")]                // QWERTYDVORAK1 with single char transcription error Y -> U
   [InlineData("A1B2C3D4E5F67GH8I9J0KI")]       // A1B2C3D4E5F6G7H8I9J0KI with two char transposition error G7 -> 7G 
   [InlineData("QWERDYTVORAK1")]                // QWERTYDVORAK1 with jump transposition error TYD -> DYT
   [InlineData("EIOUUA")]                       // AEIOUU with circular shift error
   public void Iso7064Mod37_36CheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueHasInvalidIso7064Mod37_36CheckDigitAndCustomErrorMessageIsSupplied(String globalRelease)
   {
      // Arrange.
      var request = new Iso7064Mod37_36RequestCustomMessage
      {
         GlobalReleaseIdentifier = globalRelease
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
