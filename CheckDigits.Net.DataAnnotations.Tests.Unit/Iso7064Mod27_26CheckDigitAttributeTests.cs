namespace CheckDigits.Net.DataAnnotations.Tests.Unit;

public class Iso7064Mod27_26CheckDigitAttributeTests
{
   private const String _customErrorMessage = "Need a valid ISO 7064 MOD 27,266 value";

   public class Iso7064Mod27_26Request
   {
      [Iso7064Mod27_26CheckDigit]
      public String ItemIdentifier { get; set; } = null!;
   }

   public class Iso7064Mod27_26RequestCustomMessage
   {
      [Iso7064Mod27_26CheckDigit(ErrorMessage = _customErrorMessage)]
      public String ItemIdentifier { get; set; } = null!;
   }

   public class Iso7064Mod27_26RequestRequiredField
   {
      [Required, Iso7064Mod27_26CheckDigit]
      public String ItemIdentifier { get; set; } = null!;
   }

   public class Iso7064Mod27_26RequestInvalidType
   {
      [Iso7064Mod27_26CheckDigit]
      public Int32 ItemIdentifier { get; set; }
   }

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Theory]
   [InlineData("AEIOUI")]
   [InlineData("QWERTYDVORAKY")]
   [InlineData("ABCDEFGHIJKLMNOPQRO")]
   public void Iso7064Mod27_26CheckDigitAttribute_Validate_ShouldReturnSuccess_WhenValueHasValidIso7064Mod27_26CheckDigit(String itemIdentifier)
   {
      // Arrange.
      var request = new Iso7064Mod27_26Request
      {
         ItemIdentifier = itemIdentifier
      };

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().BeEmpty();
   }

   [Fact]
   public void Iso7064Mod27_26CheckDigitAttribute_Validate_ShouldReturnSuccess_WhenNonRequiredValueIsNull()
   {
      // Arrange.
      var request = new Iso7064Mod27_26Request();

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      request.ItemIdentifier.Should().BeNull();
      results.Should().BeEmpty();
   }

   [Fact]
   public void Iso7064Mod27_26CheckDigitAttribute_Validate_ShouldReturnSuccess_WhenNonRequiredValueIsEmpty()
   {
      // Arrange.
      var request = new Iso7064Mod27_26Request
      {
         ItemIdentifier = String.Empty
      };

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().BeEmpty();
   }

   [Fact]
   public void Iso7064Mod27_26CheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueIsNotTypeString()
   {
      // Arrange.
      var item = new Iso7064Mod27_26RequestInvalidType
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
   public void Iso7064Mod27_26CheckDigitAttribute_Validate_ShouldReturnFailure_WhenRequiredValueIsNull()
   {
      // Arrange.
      var request = new Iso7064Mod27_26RequestRequiredField();
      var expectedMessage = "The ItemIdentifier field is required.";

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Fact]
   public void Iso7064Mod27_26CheckDigitAttribute_Validate_ShouldReturnFailure_WhenRequiredValueIsEmpty()
   {
      // Arrange.
      var request = new Iso7064Mod27_26RequestRequiredField
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
   [InlineData("ACIOUI")]                       // AEIOUI with single char transcription error E -> C
   [InlineData("QWERTUDVORAKY")]                // QWERTYDVORAKY with single char transcription error Y -> U
   [InlineData("ABCDEFHGIJKLMNOPQRO")]          // ABCDEFGHIJKLMNOPQRO with two char transposition error GH -> HG 
   [InlineData("THISISATESUUHISISONLYATESTT")]  // THISISATESTTHISISONLYATESTT with two char twin error TT -> UU
   [InlineData("QWERDYTVORAKY")]                // QWERTYDVORAKY with jump transposition error TYD -> DYT
   [InlineData("IAEIOU")]                       // AEIOUI with circular shift error
   [InlineData("aeiouI")]                       // lowercase check character
   [InlineData("AEIOUi")]
   public void Iso7064Mod27_26CheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueHasInvalidIso7064Mod27_26CheckDigit(String itemIdentifier)
   {
      // Arrange.
      var request = new Iso7064Mod27_26Request
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
   [InlineData("ACIOUI")]                       // AEIOUI with single char transcription error E -> C
   [InlineData("QWERTUDVORAKY")]                // QWERTYDVORAKY with single char transcription error Y -> U
   [InlineData("ABCDEFHGIJKLMNOPQRO")]          // ABCDEFGHIJKLMNOPQRO with two char transposition error GH -> HG 
   [InlineData("THISISATESUUHISISONLYATESTT")]  // THISISATESTTHISISONLYATESTT with two char twin error TT -> UU
   [InlineData("QWERDYTVORAKY")]                // QWERTYDVORAKY with jump transposition error TYD -> DYT
   [InlineData("IAEIOU")]                       // AEIOUI with circular shift error
   [InlineData("aeiouI")]                       // lowercase check character
   [InlineData("AEIOUi")]
   public void Iso7064Mod27_26CheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueHasInvalidIso7064Mod27_26CheckDigitAndCustomErrorMessageIsSupplied(String itemIdentifier)
   {
      // Arrange.
      var request = new Iso7064Mod27_26RequestCustomMessage
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
