namespace CheckDigits.Net.DataAnnotations.Tests.Unit;

public class Modulus10_2CheckDigitAttributeTests
{
   private const String _customErrorMessage = "Need a valid IMO Number";

   public class Modulus10_2Request
   {
      [Modulus10_2CheckDigit]
      public String ImoNumber { get; set; } = null!;
   }

   public class Modulus10_2RequestCustomMessage
   {
      [Modulus10_2CheckDigit(ErrorMessage = _customErrorMessage)]
      public String ImoNumber { get; set; } = null!;
   }

   public class Modulus10_2RequestRequiredField
   {
      [Required, Modulus10_2CheckDigit]
      public String ImoNumber { get; set; } = null!;
   }

   public class Modulus10_2RequestInvalidType
   {
      [Modulus10_2CheckDigit]
      public Int32 ImoNumber { get; set; }
   }

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Theory]
   [InlineData("9074729")]       // Worked example of IMO Number from Wikipedia https://en.wikipedia.org/wiki/IMO_number
   [InlineData("9707792")]       // IMO Number from https://www.marinetraffic.com/en/ais/details/ships/shipid:155821/mmsi:219018788/imo:9707792/vessel:CARRIER#:~:text=CARRIER%20(IMO%3A%209707792)%20is,under%20the%20flag%20of%20Denmark.
   public void Modulus10_2CheckDigitAttribute_Validate_ShouldReturnSuccess_WhenValueHasValidModulus10_2CheckDigit(String imo)
   {
      // Arrange.
      var request = new Modulus10_2Request
      {
         ImoNumber = imo
      };

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().BeEmpty();
   }

   [Fact]
   public void Modulus10_2CheckDigitAttribute_Validate_ShouldReturnSuccess_WhenNonRequiredValueIsNull()
   {
      // Arrange.
      var request = new Modulus10_2Request();

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      request.ImoNumber.Should().BeNull();
      results.Should().BeEmpty();
   }

   [Fact]
   public void Modulus10_2CheckDigitAttribute_Validate_ShouldReturnSuccess_WhenNonRequiredValueIsEmpty()
   {
      // Arrange.
      var request = new Modulus10_2Request
      {
         ImoNumber = String.Empty
      };

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().BeEmpty();
   }

   [Fact]
   public void Modulus10_2CheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueIsNotTypeString()
   {
      // Arrange.
      var item = new Modulus10_2RequestInvalidType
      {
         ImoNumber = 123456
      };
      var expectedMessage = String.Format(Messages.InvalidPropertyType, nameof(item.ImoNumber));

      // Act.
      var results = Utility.ValidateModel(item);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Fact]
   public void Modulus10_2CheckDigitAttribute_Validate_ShouldReturnFailure_WhenRequiredValueIsNull()
   {
      // Arrange.
      var request = new Modulus10_2RequestRequiredField();
      var expectedMessage = "The ImoNumber field is required.";

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Fact]
   public void Modulus10_2CheckDigitAttribute_Validate_ShouldReturnFailure_WhenRequiredValueIsEmpty()
   {
      // Arrange.
      var request = new Modulus10_2RequestRequiredField
      {
         ImoNumber = String.Empty
      };
      var expectedMessage = "The ImoNumber field is required.";

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Theory]
   [InlineData("1020569")]       // IMO Number 1010569 with single digit transcription error 1 -> 2
   [InlineData("9704729")]       // IMO Number 9074729 with two digit transposition error 07 -> 70
   [InlineData("9470729")]       // IMO Number 9074729 with jump transposition 074 -> 470
   public void Modulus10_2CheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueHasInvalidModulus10_2CheckDigit(String imo)
   {
      // Arrange.
      var request = new Modulus10_2Request
      {
         ImoNumber = imo
      };
      var expectedMessage = String.Format(Messages.SingleCheckDigitFailure, nameof(request.ImoNumber));

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Theory]
   [InlineData("1020569")]       // IMO Number 1010569 with single digit transcription error 1 -> 2
   [InlineData("9704729")]       // IMO Number 9074729 with two digit transposition error 07 -> 70
   [InlineData("9470729")]       // IMO Number 9074729 with jump transposition 074 -> 470
   public void Modulus10_2CheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueHasInvalidModulus10_2CheckDigitAndCustomErrorMessageIsSupplied(String imo)
   {
      // Arrange.
      var request = new Modulus10_2RequestCustomMessage
      {
         ImoNumber = imo
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
