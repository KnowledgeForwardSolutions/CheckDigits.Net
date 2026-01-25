namespace CheckDigits.Net.DataAnnotations.Tests.Unit;

public class AlphanumericMod97_10CheckDigitAttributeTests
{
   private const String _customErrorMessage = "Need a valid Legal Entity Identifier";

   public class AlphanumericMod97_10Request
   {
      [AlphanumericMod97_10CheckDigitAttribute]
      public String LegalEntityIdentifier { get; set; } = null!;
   }

   public class AlphanumericMod97_10RequestCustomMessage
   {
      [AlphanumericMod97_10CheckDigitAttribute(ErrorMessage = _customErrorMessage)]
      public String LegalEntityIdentifier { get; set; } = null!;
   }

   public class AlphanumericMod97_10RequestRequiredField
   {
      [Required, AlphanumericMod97_10CheckDigitAttribute]
      public String LegalEntityIdentifier { get; set; } = null!;
   }

   public class AlphanumericMod97_10RequestInvalidType
   {
      [AlphanumericMod97_10CheckDigitAttribute]
      public Int32 LegalEntityIdentifier { get; set; }
   }

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Theory]
   [InlineData("10Bx939c5543TqA1144M999143X38")]         // Worked example from https://www.govinfo.gov/content/pkg/CFR-2016-title12-vol8/xml/CFR-2016-title12-vol8-part1003-appC.xml
   [InlineData("549300KM40FP4MSQU94112345QWERTY987648")] // Generated ULI from https://ffiec.cfpb.gov/tools/check-digit
   [InlineData("549300UDFJVWBIHXA058")]                  // LEI for Alphabet, from https://lei.info/
   public void AlphanumericMod97_10CheckDigitAttribute_Validate_ShouldReturnSuccess_WhenValueHasValidAlphanumericMod97_10CheckDigit(String value)
   {
      // Arrange.
      var request = new AlphanumericMod97_10Request
      {
         LegalEntityIdentifier = value
      };

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().BeEmpty();
   }

   [Fact]
   public void AlphanumericMod97_10CheckDigitAttribute_Validate_ShouldReturnSuccess_WhenNonRequiredValueIsNull()
   {
      // Arrange.
      var request = new AlphanumericMod97_10Request();

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      request.LegalEntityIdentifier.Should().BeNull();
      results.Should().BeEmpty();
   }

   [Fact]
   public void AlphanumericMod97_10CheckDigitAttribute_Validate_ShouldReturnSuccess_WhenNonRequiredValueIsEmpty()
   {
      // Arrange.
      var request = new AlphanumericMod97_10Request
      {
         LegalEntityIdentifier = String.Empty
      };

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().BeEmpty();
   }

   [Fact]
   public void AlphanumericMod97_10CheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueIsNotTypeString()
   {
      // Arrange.
      var item = new AlphanumericMod97_10RequestInvalidType
      {
         LegalEntityIdentifier = 123456
      };
      var expectedMessage = String.Format(Messages.InvalidPropertyType, nameof(item.LegalEntityIdentifier));

      // Act.
      var results = Utility.ValidateModel(item);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Fact]
   public void AlphanumericMod97_10CheckDigitAttribute_Validate_ShouldReturnFailure_WhenRequiredValueIsNull()
   {
      // Arrange.
      var request = new AlphanumericMod97_10RequestRequiredField();
      var expectedMessage = "The LegalEntityIdentifier field is required.";

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Fact]
   public void AlphanumericMod97_10CheckDigitAttribute_Validate_ShouldReturnFailure_WhenRequiredValueIsEmpty()
   {
      // Arrange.
      var request = new AlphanumericMod97_10RequestRequiredField
      {
         LegalEntityIdentifier = String.Empty
      };
      var expectedMessage = "The LegalEntityIdentifier field is required.";

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Theory]
   [InlineData("540300UDFJVWBIHXA058")]                  // 549300UDFJVWBIHXA058 with single digit transcription error 9 -> 0
   [InlineData("SC74MC0LB1031234567890123456USD")]       // SC74MCBL01031234567890123456USD with jump transposition error BL0 -> 0LB
   [InlineData("967600AJD1Q8K13MZ845")]                  // 967600DJA1Q8K13MZ845 with jump transposition error DJA -> AJD
   public void AlphanumericMod97_10CheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueHasInvalidAlphanumericMod97_10CheckDigit(String value)
   {
      // Arrange.
      var request = new AlphanumericMod97_10Request
      {
         LegalEntityIdentifier = value
      };
      var expectedMessage = String.Format(Messages.MultiCheckDigitFailure, nameof(request.LegalEntityIdentifier));

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Theory]
   [InlineData("540300UDFJVWBIHXA058")]                  // 549300UDFJVWBIHXA058 with single digit transcription error 9 -> 0
   [InlineData("SC74MC0LB1031234567890123456USD")]       // SC74MCBL01031234567890123456USD with jump transposition error BL0 -> 0LB
   [InlineData("967600AJD1Q8K13MZ845")]                  // 967600DJA1Q8K13MZ845 with jump transposition error DJA -> AJD
   public void AlphanumericMod97_10CheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueHasInvalidAlphanumericMod97_10CheckDigitAndCustomErrorMessageIsSupplied(String value)
   {
      // Arrange.
      var request = new AlphanumericMod97_10RequestCustomMessage
      {
         LegalEntityIdentifier = value
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
