// Ignore Spelling: Issn

namespace CheckDigits.Net.DataAnnotations.Tests.Unit;

public class Modulus11CheckDigitAttributeTests
{
   private const String _customErrorMessage = "Need a valid ISSN";

   public class Modulus11Request
   {
      [Modulus11CheckDigit]
      public String Issn { get; set; } = null!;
   }

   public class Modulus11RequestCustomMessage
   {
      [Modulus11CheckDigit(ErrorMessage = _customErrorMessage)]
      public String Issn { get; set; } = null!;
   }

   public class Modulus11RequestRequiredField
   {
      [Required, Modulus11CheckDigit]
      public String Issn { get; set; } = null!;
   }

   public class Modulus11RequestInvalidType
   {
      [Modulus11CheckDigit]
      public Int32 Issn { get; set; }
   }

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Theory]
   [InlineData("1568656521")]       // ISBN-10 Island in the Stream of Time, S. M. Sterling
   [InlineData("2434561X")]         // Example ISSN from Wikipedia
   public void Modulus11CheckDigitAttribute_Validate_ShouldReturnSuccess_WhenValueHasValidModulus11CheckDigit(String issn)
   {
      // Arrange.
      var request = new Modulus11Request
      {
         Issn = issn
      };

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().BeEmpty();
   }

   [Fact]
   public void Modulus11CheckDigitAttribute_Validate_ShouldReturnSuccess_WhenNonRequiredValueIsNull()
   {
      // Arrange.
      var request = new Modulus11Request();

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      request.Issn.Should().BeNull();
      results.Should().BeEmpty();
   }

   [Fact]
   public void Modulus11CheckDigitAttribute_Validate_ShouldReturnSuccess_WhenNonRequiredValueIsEmpty()
   {
      // Arrange.
      var request = new Modulus11Request
      {
         Issn = String.Empty
      };

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().BeEmpty();
   }

   [Fact]
   public void Modulus11CheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueIsNotTypeString()
   {
      // Arrange.
      var item = new Modulus11RequestInvalidType
      {
         Issn = 123456
      };
      var expectedMessage = String.Format(Messages.InvalidPropertyType, nameof(item.Issn));

      // Act.
      var results = Utility.ValidateModel(item);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Fact]
   public void Modulus11CheckDigitAttribute_Validate_ShouldReturnFailure_WhenRequiredValueIsNull()
   {
      // Arrange.
      var request = new Modulus11RequestRequiredField();
      var expectedMessage = "The Issn field is required.";

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Fact]
   public void Modulus11CheckDigitAttribute_Validate_ShouldReturnFailure_WhenRequiredValueIsEmpty()
   {
      // Arrange.
      var request = new Modulus11RequestRequiredField
      {
         Issn = String.Empty
      };
      var expectedMessage = "The Issn field is required.";

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Theory]
   [InlineData("1568646521")]    // ISBN-10 with single digit transcription error 5 -> 4
   [InlineData("0441050608")]    // ISBN-10 with two digit transposition error 05 -> 50
   [InlineData("050029273X")]    // ISBN-10 with jump transposition 729 -> 927
   [InlineData("0551005608")]    // ISBN-10 with twin error 44 -> 55
   public void Modulus11CheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueHasInvalidModulus11CheckDigit(String issn)
   {
      // Arrange.
      var request = new Modulus11Request
      {
         Issn = issn
      };
      var expectedMessage = String.Format(Messages.SingleCheckDigitFailure, nameof(request.Issn));

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Theory]
   [InlineData("1568646521")]    // ISBN-10 with single digit transcription error 5 -> 4
   [InlineData("0441050608")]    // ISBN-10 with two digit transposition error 05 -> 50
   [InlineData("050029273X")]    // ISBN-10 with jump transposition 729 -> 927
   [InlineData("0551005608")]    // ISBN-10 with twin error 44 -> 55
   public void Modulus11CheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueHasInvalidModulus11CheckDigitAndCustomErrorMessageIsSupplied(String issn)
   {
      // Arrange.
      var request = new Modulus11RequestCustomMessage
      {
         Issn = issn
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
