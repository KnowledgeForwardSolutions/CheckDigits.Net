namespace CheckDigits.Net.DataAnnotations.Tests.Unit;

public class Iso7064Mod37_2CheckDigitAttributeTests
{
   private const String _customErrorMessage = "Need a valid ISO 7064 MOD 37-2 value";

   public class Iso7064Mod37_2Request
   {
      [Iso7064Mod37_2CheckDigit]
      public String DonationIdentifier { get; set; } = null!;
   }

   public class Iso7064Mod37_2RequestCustomMessage
   {
      [Iso7064Mod37_2CheckDigit(ErrorMessage = _customErrorMessage)]
      public String DonationIdentifier { get; set; } = null!;
   }

   public class Iso7064Mod37_2RequestRequiredField
   {
      [Required, Iso7064Mod37_2CheckDigit]
      public String DonationIdentifier { get; set; } = null!;
   }

   public class Iso7064Mod37_2RequestInvalidType
   {
      [Iso7064Mod37_2CheckDigit]
      public Int32 DonationIdentifier { get; set; }
   }

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Theory]
   [InlineData("A999914123456N")]      // Example ISBT from https://www.isbt128.org/_files/ugd/83d6e1_9c7ba55fbdd44a80947bc310cdd92382.pdf
   [InlineData("C0003070014666")]      // Example ISBT from https://www.isbt128.org/_files/ugd/83d6e1_10edb0e64b234943abd9c100b925575c.pdf
   [InlineData("A999922012346*")]      // Example ISBT from https://www.isbt128.org/_files/ugd/79eb0b_1a92d4e286af404183d03bf5bab9120f.pdf
   public void Iso7064Mod37_2CheckDigitAttribute_Validate_ShouldReturnSuccess_WhenValueHasValidIso7064Mod37_2CheckDigit(String donation)
   {
      // Arrange.
      var request = new Iso7064Mod37_2Request
      {
         DonationIdentifier = donation
      };

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().BeEmpty();
   }

   [Fact]
   public void Iso7064Mod37_2CheckDigitAttribute_Validate_ShouldReturnSuccess_WhenNonRequiredValueIsNull()
   {
      // Arrange.
      var request = new Iso7064Mod37_2Request();

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      request.DonationIdentifier.Should().BeNull();
      results.Should().BeEmpty();
   }

   [Fact]
   public void Iso7064Mod37_2CheckDigitAttribute_Validate_ShouldReturnSuccess_WhenNonRequiredValueIsEmpty()
   {
      // Arrange.
      var request = new Iso7064Mod37_2Request
      {
         DonationIdentifier = String.Empty
      };

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().BeEmpty();
   }

   [Fact]
   public void Iso7064Mod37_2CheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueIsNotTypeString()
   {
      // Arrange.
      var item = new Iso7064Mod37_2RequestInvalidType
      {
         DonationIdentifier = 123456
      };
      var expectedMessage = String.Format(Messages.InvalidPropertyType, nameof(item.DonationIdentifier));

      // Act.
      var results = Utility.ValidateModel(item);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Fact]
   public void Iso7064Mod37_2CheckDigitAttribute_Validate_ShouldReturnFailure_WhenRequiredValueIsNull()
   {
      // Arrange.
      var request = new Iso7064Mod37_2RequestRequiredField();
      var expectedMessage = "The DonationIdentifier field is required.";

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Fact]
   public void Iso7064Mod37_2CheckDigitAttribute_Validate_ShouldReturnFailure_WhenRequiredValueIsEmpty()
   {
      // Arrange.
      var request = new Iso7064Mod37_2RequestRequiredField
      {
         DonationIdentifier = String.Empty
      };
      var expectedMessage = "The DonationIdentifier field is required.";

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Theory]
   [InlineData("A999915123456N")]      // A999914123456N with single digit transcription error 4 -> 5
   [InlineData("F123498654321H")]      // G123498654321H with single char transcription error G -> F
   [InlineData("A999920212346*")]      // A999922012346* with two digit transposition error 20 -> 02
   [InlineData("A999933123458J")]      // A999922123458J with two digit twin error 22 -> 33
   [InlineData("G123468954321H")]      // G123498654321H with jump transposition error 986 -> 689
   [InlineData("6C000307001466")]      // C0003070014666 with circular shift error
   [InlineData("B999922123469H")]      // A999922123459H with two single transcription errors A -> B, 5 -> 6
   [InlineData("a999922012346*")]      // Lowercase variant of valid value
   public void Iso7064Mod37_2CheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueHasInvalidIso7064Mod37_2CheckDigit(String donation)
   {
      // Arrange.
      var request = new Iso7064Mod37_2Request
      {
         DonationIdentifier = donation
      };
      var expectedMessage = String.Format(Messages.SingleCheckDigitFailure, nameof(request.DonationIdentifier));

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Theory]
   [InlineData("A999915123456N")]      // A999914123456N with single digit transcription error 4 -> 5
   [InlineData("F123498654321H")]      // G123498654321H with single char transcription error G -> F
   [InlineData("A999920212346*")]      // A999922012346* with two digit transposition error 20 -> 02
   [InlineData("A999933123458J")]      // A999922123458J with two digit twin error 22 -> 33
   [InlineData("G123468954321H")]      // G123498654321H with jump transposition error 986 -> 689
   [InlineData("6C000307001466")]      // C0003070014666 with circular shift error
   [InlineData("B999922123469H")]      // A999922123459H with two single transcription errors A -> B, 5 -> 6
   [InlineData("a999922012346*")]      // Lowercase variant of valid value
   public void Iso7064Mod37_2CheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueHasInvalidIso7064Mod37_2CheckDigitAndCustomErrorMessageIsSupplied(String donation)
   {
      // Arrange.
      var request = new Iso7064Mod37_2RequestCustomMessage
      {
         DonationIdentifier = donation
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
