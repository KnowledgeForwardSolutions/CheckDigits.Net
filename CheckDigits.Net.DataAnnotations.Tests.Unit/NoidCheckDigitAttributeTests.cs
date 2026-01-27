// Ignore Spelling: Noid

namespace CheckDigits.Net.DataAnnotations.Tests.Unit;

public class NoidCheckDigitAttributeTests
{
   private const String _customErrorMessage = "Need a valid ARK identifier";

   public class NoidRequest
   {
      [NoidCheckDigit]
      public String ArkIdentifier { get; set; } = null!;
   }

   public class NoidRequestCustomMessage
   {
      [NoidCheckDigit(ErrorMessage = _customErrorMessage)]
      public String ArkIdentifier { get; set; } = null!;
   }

   public class NoidRequestRequiredField
   {
      [Required, NoidCheckDigit]
      public String ArkIdentifier { get; set; } = null!;
   }

   public class NoidRequestInvalidType
   {
      [NoidCheckDigit]
      public Int32 ArkIdentifier { get; set; }
   }

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Theory]
   [InlineData("13030/xf93gt2q")]      // Worked example from https://metacpan.org/dist/Noid/view/noid#NOID-CHECK-DIGIT-ALGORITHM
   [InlineData("13030/tf5p30086k")]    // Example from https://n2t.net/e/noid.html
   public void NoidCheckDigitAttribute_Validate_ShouldReturnSuccess_WhenValueHasValidNoidCheckDigit(String ark)
   {
      // Arrange.
      var request = new NoidRequest
      {
         ArkIdentifier = ark
      };

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().BeEmpty();
   }

   [Fact]
   public void NoidCheckDigitAttribute_Validate_ShouldReturnSuccess_WhenNonRequiredValueIsNull()
   {
      // Arrange.
      var request = new NoidRequest();

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      request.ArkIdentifier.Should().BeNull();
      results.Should().BeEmpty();
   }

   [Fact]
   public void NoidCheckDigitAttribute_Validate_ShouldReturnSuccess_WhenNonRequiredValueIsEmpty()
   {
      // Arrange.
      var request = new NoidRequest
      {
         ArkIdentifier = String.Empty
      };

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().BeEmpty();
   }

   [Fact]
   public void NoidCheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueIsNotTypeString()
   {
      // Arrange.
      var item = new NoidRequestInvalidType
      {
         ArkIdentifier = 123456
      };
      var expectedMessage = String.Format(Messages.InvalidPropertyType, nameof(item.ArkIdentifier));

      // Act.
      var results = Utility.ValidateModel(item);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Fact]
   public void NoidCheckDigitAttribute_Validate_ShouldReturnFailure_WhenRequiredValueIsNull()
   {
      // Arrange.
      var request = new NoidRequestRequiredField();
      var expectedMessage = "The ArkIdentifier field is required.";

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Fact]
   public void NoidCheckDigitAttribute_Validate_ShouldReturnFailure_WhenRequiredValueIsEmpty()
   {
      // Arrange.
      var request = new NoidRequestRequiredField
      {
         ArkIdentifier = String.Empty
      };
      var expectedMessage = "The ArkIdentifier field is required.";

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Theory]
   [InlineData("13030/xf83gt2q")]      // 13030/xf93gt2q with single digit transcription error 9 -> 8
   [InlineData("13030/xd93gt2q")]      // 13030/xf93gt2q with single char transcription error f -> d
   [InlineData("13030/tf5p30806k")]    // 13030/tf5p30086k with two digit transposition error 08 -> 80 
   public void NoidCheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueHasInvalidNoidCheckDigit(String ark)
   {
      // Arrange.
      var request = new NoidRequest
      {
         ArkIdentifier = ark
      };
      var expectedMessage = String.Format(Messages.SingleCheckDigitFailure, nameof(request.ArkIdentifier));

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Theory]
   [InlineData("13030/xf83gt2q")]      // 13030/xf93gt2q with single digit transcription error 9 -> 8
   [InlineData("13030/xd93gt2q")]      // 13030/xf93gt2q with single char transcription error f -> d
   [InlineData("13030/tf5p30806k")]    // 13030/tf5p30086k with two digit transposition error 08 -> 80 
   public void NoidCheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueHasInvalidNoidCheckDigitAndCustomErrorMessageIsSupplied(String ark)
   {
      // Arrange.
      var request = new NoidRequestCustomMessage
      {
         ArkIdentifier = ark
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
