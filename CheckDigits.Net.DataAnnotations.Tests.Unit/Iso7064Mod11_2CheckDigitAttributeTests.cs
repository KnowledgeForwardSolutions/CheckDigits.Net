// Ignore Spelling: isni

namespace CheckDigits.Net.DataAnnotations.Tests.Unit;

public class Iso7064Mod11_2CheckDigitAttributeTests
{
   private const String _customErrorMessage = "Need a valid ISO 7064 MOD 11-2 value";

   public class Iso7064Mod11_2Request
   {
      [Iso7064Mod11_2CheckDigit]
      public String StandardNameIdentifier { get; set; } = null!;
   }

   public class Iso7064Mod11_2RequestCustomMessage
   {
      [Iso7064Mod11_2CheckDigit(ErrorMessage = _customErrorMessage)]
      public String StandardNameIdentifier { get; set; } = null!;
   }

   public class Iso7064Mod11_2RequestRequiredField
   {
      [Required, Iso7064Mod11_2CheckDigit]
      public String StandardNameIdentifier { get; set; } = null!;
   }

   public class Iso7064Mod11_2RequestInvalidType
   {
      [Iso7064Mod11_2CheckDigit]
      public Int32 StandardNameIdentifier { get; set; }
   }

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Theory]
   [InlineData("0000000073669144")]    // ISNI for Richard, Zachary from https://isni.org/page/search-database/
   [InlineData("000000012095650X")]    // ISNI for Lucas, George from https://isni.org/page/search-database/
   [InlineData("0000000109302468")]    // ISNI for Roddenberry, Gene from https://isni.org/page/search-database/
   public void Iso7064Mod11_2CheckDigitAttribute_Validate_ShouldReturnSuccess_WhenValueHasValidIso7064Mod11_2CheckDigit(String isni)
   {
      // Arrange.
      var request = new Iso7064Mod11_2Request
      {
         StandardNameIdentifier = isni
      };

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().BeEmpty();
   }

   [Fact]
   public void Iso7064Mod11_2CheckDigitAttribute_Validate_ShouldReturnSuccess_WhenNonRequiredValueIsNull()
   {
      // Arrange.
      var request = new Iso7064Mod11_2Request();

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      request.StandardNameIdentifier.Should().BeNull();
      results.Should().BeEmpty();
   }

   [Fact]
   public void Iso7064Mod11_2CheckDigitAttribute_Validate_ShouldReturnSuccess_WhenNonRequiredValueIsEmpty()
   {
      // Arrange.
      var request = new Iso7064Mod11_2Request
      {
         StandardNameIdentifier = String.Empty
      };

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().BeEmpty();
   }

   [Fact]
   public void Iso7064Mod11_2CheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueIsNotTypeString()
   {
      // Arrange.
      var item = new Iso7064Mod11_2RequestInvalidType
      {
         StandardNameIdentifier = 123456
      };
      var expectedMessage = String.Format(Messages.InvalidPropertyType, nameof(item.StandardNameIdentifier));

      // Act.
      var results = Utility.ValidateModel(item);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Fact]
   public void Iso7064Mod11_2CheckDigitAttribute_Validate_ShouldReturnFailure_WhenRequiredValueIsNull()
   {
      // Arrange.
      var request = new Iso7064Mod11_2RequestRequiredField();
      var expectedMessage = "The StandardNameIdentifier field is required.";

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Fact]
   public void Iso7064Mod11_2CheckDigitAttribute_Validate_ShouldReturnFailure_WhenRequiredValueIsEmpty()
   {
      // Arrange.
      var request = new Iso7064Mod11_2RequestRequiredField
      {
         StandardNameIdentifier = String.Empty
      };
      var expectedMessage = "The StandardNameIdentifier field is required.";

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Theory]
   [InlineData("000000012156438X")]    // 000000012146438X with single digit transcription error 4 -> 5
   [InlineData("0000000073696144")]    // 0000000073669144 with two digit transposition error 69 -> 96 
   [InlineData("0000000059970317")]    // 0000000058870317 with two digit twin error 88 -> 99
   [InlineData("0000000901302468")]    // 0000000109302468 with jump transposition error 109 -> 901
   [InlineData("0000000736691440")]    // 0000000073669144 with circular shift error
   public void Iso7064Mod11_2CheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueHasInvalidIso7064Mod11_2CheckDigit(String isni)
   {
      // Arrange.
      var request = new Iso7064Mod11_2Request
      {
         StandardNameIdentifier = isni
      };
      var expectedMessage = String.Format(Messages.SingleCheckDigitFailure, nameof(request.StandardNameIdentifier));

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Theory]
   [InlineData("000000012156438X")]    // 000000012146438X with single digit transcription error 4 -> 5
   [InlineData("0000000073696144")]    // 0000000073669144 with two digit transposition error 69 -> 96 
   [InlineData("0000000059970317")]    // 0000000058870317 with two digit twin error 88 -> 99
   [InlineData("0000000901302468")]    // 0000000109302468 with jump transposition error 109 -> 901
   [InlineData("0000000736691440")]    // 0000000073669144 with circular shift error
   public void Iso7064Mod11_2CheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueHasInvalidIso7064Mod11_2CheckDigitAndCustomErrorMessageIsSupplied(String isni)
   {
      // Arrange.
      var request = new Iso7064Mod11_2RequestCustomMessage
      {
         StandardNameIdentifier = isni
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
