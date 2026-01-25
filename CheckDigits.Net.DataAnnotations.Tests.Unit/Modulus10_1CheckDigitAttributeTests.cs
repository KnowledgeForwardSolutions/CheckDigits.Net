// Ignore Spelling: Cas

namespace CheckDigits.Net.DataAnnotations.Tests.Unit;

public class Modulus10_1CheckDigitAttributeTests
{
   private const String _customErrorMessage = "Need a valid CAS Registry Number";

   public class Modulus10_1Request
   {
      [Modulus10_1CheckDigitAttribute]
      public String CasNumber { get; set; } = null!;
   }

   public class Modulus10_1RequestCustomMessage
   {
      [Modulus10_1CheckDigitAttribute(ErrorMessage = _customErrorMessage)]
      public String CasNumber { get; set; } = null!;
   }

   public class Modulus10_1RequestRequiredField
   {
      [Required, Modulus10_1CheckDigitAttribute]
      public String CasNumber { get; set; } = null!;
   }

   public class Modulus10_1RequestInvalidType
   {
      [Modulus10_1CheckDigitAttribute]
      public Int32 CasNumber { get; set; }
   }

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Theory]
   [InlineData("7732185")]       // Worked example of CAS Registry Number from Wikipedia https://en.wikipedia.org/wiki/CAS_Registry_Number
   [InlineData("58082")]         // CAS Registry Number for caffeine
   public void Modulus10_1CheckDigitAttribute_Validate_ShouldReturnSuccess_WhenValueHasValidModulus10_1CheckDigit(String cas)
   {
      // Arrange.
      var request = new Modulus10_1Request
      {
         CasNumber = cas
      };

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().BeEmpty();
   }

   [Fact]
   public void Modulus10_1CheckDigitAttribute_Validate_ShouldReturnSuccess_WhenNonRequiredValueIsNull()
   {
      // Arrange.
      var request = new Modulus10_1Request();

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      request.CasNumber.Should().BeNull();
      results.Should().BeEmpty();
   }

   [Fact]
   public void Modulus10_1CheckDigitAttribute_Validate_ShouldReturnSuccess_WhenNonRequiredValueIsEmpty()
   {
      // Arrange.
      var request = new Modulus10_1Request
      {
         CasNumber = String.Empty
      };

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().BeEmpty();
   }

   [Fact]
   public void Modulus10_1CheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueIsNotTypeString()
   {
      // Arrange.
      var item = new Modulus10_1RequestInvalidType
      {
         CasNumber = 123456
      };
      var expectedMessage = String.Format(Messages.InvalidPropertyType, nameof(item.CasNumber));

      // Act.
      var results = Utility.ValidateModel(item);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Fact]
   public void Modulus10_1CheckDigitAttribute_Validate_ShouldReturnFailure_WhenRequiredValueIsNull()
   {
      // Arrange.
      var request = new Modulus10_1RequestRequiredField();
      var expectedMessage = "The CasNumber field is required.";

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Fact]
   public void Modulus10_1CheckDigitAttribute_Validate_ShouldReturnFailure_WhenRequiredValueIsEmpty()
   {
      // Arrange.
      var request = new Modulus10_1RequestRequiredField
      {
         CasNumber = String.Empty
      };
      var expectedMessage = "The CasNumber field is required.";

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Theory]
   [InlineData("7742185")]       // CAS Registry Number 7732185 with single digit transcription error 3 -> 4
   [InlineData("50882")]         // CAS Registry Number 58082 with two digit transposition error 80 -> 08
   [InlineData("28827554")]      // CAS Registry Number 28728554 with jump transposition 728 -> 827
   public void Modulus10_1CheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueHasInvalidModulus10_1CheckDigit(String cas)
   {
      // Arrange.
      var request = new Modulus10_1Request
      {
         CasNumber = cas
      };
      var expectedMessage = String.Format(Messages.SingleCheckDigitFailure, nameof(request.CasNumber));

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Theory]
   [InlineData("7742185")]       // CAS Registry Number 7732185 with single digit transcription error 3 -> 4
   [InlineData("50882")]         // CAS Registry Number 58082 with two digit transposition error 80 -> 08
   [InlineData("28827554")]      // CAS Registry Number 28728554 with jump transposition 728 -> 827
   public void Modulus10_1CheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueHasInvalidModulus10_1CheckDigitAndCustomErrorMessageIsSupplied(String cas)
   {
      // Arrange.
      var request = new Modulus10_1RequestCustomMessage
      {
         CasNumber = cas
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
