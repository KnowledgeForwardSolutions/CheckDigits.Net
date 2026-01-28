namespace CheckDigits.Net.DataAnnotations.Tests.Unit;

public class Iso7064Mod1271_36CheckDigitAttributeTests
{
   private const String _customErrorMessage = "Need a valid ISO 7064 MOD 1271-36 value";

   public class Iso7064Mod1271_36Request
   {
      [Iso7064Mod1271_36CheckDigit]
      public String ItemIdentifier { get; set; } = null!;
   }

   public class Iso7064Mod1271_36RequestCustomMessage
   {
      [Iso7064Mod1271_36CheckDigit(ErrorMessage = _customErrorMessage)]
      public String ItemIdentifier { get; set; } = null!;
   }

   public class Iso7064Mod1271_36RequestRequiredField
   {
      [Required, Iso7064Mod1271_36CheckDigit]
      public String ItemIdentifier { get; set; } = null!;
   }

   public class Iso7064Mod1271_36RequestInvalidType
   {
      [Iso7064Mod1271_36CheckDigit]
      public Int32 ItemIdentifier { get; set; }
   }

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Theory]
   [InlineData("ISO793W")]                // Example from ISO/IEC 7064 specification
   [InlineData("XS868977863229AU")]       // Example Nigerian Virtual National Identification Number https://nin.mtn.ng/
   public void Iso7064Mod1271_36CheckDigitAttribute_Validate_ShouldReturnSuccess_WhenValueHasValidIso7064Mod1271_36CheckDigit(String itemIdentifier)
   {
      // Arrange.
      var request = new Iso7064Mod1271_36Request
      {
         ItemIdentifier = itemIdentifier
      };

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().BeEmpty();
   }

   [Fact]
   public void Iso7064Mod1271_36CheckDigitAttribute_Validate_ShouldReturnSuccess_WhenNonRequiredValueIsNull()
   {
      // Arrange.
      var request = new Iso7064Mod1271_36Request();

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      request.ItemIdentifier.Should().BeNull();
      results.Should().BeEmpty();
   }

   [Fact]
   public void Iso7064Mod1271_36CheckDigitAttribute_Validate_ShouldReturnSuccess_WhenNonRequiredValueIsEmpty()
   {
      // Arrange.
      var request = new Iso7064Mod1271_36Request
      {
         ItemIdentifier = String.Empty
      };

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().BeEmpty();
   }

   [Fact]
   public void Iso7064Mod1271_36CheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueIsNotTypeString()
   {
      // Arrange.
      var item = new Iso7064Mod1271_36RequestInvalidType
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
   public void Iso7064Mod1271_36CheckDigitAttribute_Validate_ShouldReturnFailure_WhenRequiredValueIsNull()
   {
      // Arrange.
      var request = new Iso7064Mod1271_36RequestRequiredField();
      var expectedMessage = "The ItemIdentifier field is required.";

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Fact]
   public void Iso7064Mod1271_36CheckDigitAttribute_Validate_ShouldReturnFailure_WhenRequiredValueIsEmpty()
   {
      // Arrange.
      var request = new Iso7064Mod1271_36RequestRequiredField
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
   [InlineData("XT868977863229AU")]    // XS868977863229AU with single char transcription error S -> T
   [InlineData("XS869877863229AU")]    // XS868977863229AU with two digit transposition error 89 -> 98 
   [InlineData("123456789BACDEH2")]    // 123456789ABCDEH2 with two char transposition error AB -> BA 
   [InlineData("XS868977863339AU")]    // XS868977863229AU two digit twin error 22 -> 33
   [InlineData("123456987ABCDEH2")]    // 123456789ABCDEH2 with jump transposition error 789 -> 987
   [InlineData("UXS868977863229A")]    // XS868977863229AU with circular shift error
   public void Iso7064Mod1271_36CheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueHasInvalidIso7064Mod1271_36CheckDigit(String itemIdentifier)
   {
      // Arrange.
      var request = new Iso7064Mod1271_36Request
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
   [InlineData("XT868977863229AU")]    // XS868977863229AU with single char transcription error S -> T
   [InlineData("XS869877863229AU")]    // XS868977863229AU with two digit transposition error 89 -> 98 
   [InlineData("123456789BACDEH2")]    // 123456789ABCDEH2 with two char transposition error AB -> BA 
   [InlineData("XS868977863339AU")]    // XS868977863229AU two digit twin error 22 -> 33
   [InlineData("123456987ABCDEH2")]    // 123456789ABCDEH2 with jump transposition error 789 -> 987
   [InlineData("UXS868977863229A")]    // XS868977863229AU with circular shift error
   public void Iso7064Mod1271_36CheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueHasInvalidIso7064Mod1271_36CheckDigitAndCustomErrorMessageIsSupplied(String itemIdentifier)
   {
      // Arrange.
      var request = new Iso7064Mod1271_36RequestCustomMessage
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
