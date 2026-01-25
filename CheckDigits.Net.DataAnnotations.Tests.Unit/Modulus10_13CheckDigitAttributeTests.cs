namespace CheckDigits.Net.DataAnnotations.Tests.Unit;

public class Modulus10_13CheckDigitAttributeTests
{
   private const String _customErrorMessage = "Need a valid UPC code";

   public class Modulus10_13Request
   {
      [Modulus10_13CheckDigit]
      public String UpcCode { get; set; } = null!;
   }

   public class Modulus10_13RequestCustomMessage
   {
      [Modulus10_13CheckDigit(ErrorMessage = _customErrorMessage)]
      public String UpcCode { get; set; } = null!;
   }

   public class Modulus10_13RequestRequiredField
   {
      [Required, Modulus10_13CheckDigit]
      public String UpcCode { get; set; } = null!;
   }

   public class Modulus10_13RequestInvalidType
   {
      [Modulus10_13CheckDigit]
      public Int32 UpcCode { get; set; }
   }

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Modulus10_13CheckDigitAttribute_Validate_ShouldReturnSuccess_WhenValueHasValidModulus10_13CheckDigit()
   {
      // Arrange.
      var request = new Modulus10_13Request
      {
         UpcCode = "036000291452" // Valid UPC-A code
      };

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().BeEmpty();
   }

   [Fact]
   public void Modulus10_13CheckDigitAttribute_Validate_ShouldReturnSuccess_WhenNonRequiredValueIsNull()
   {
      // Arrange.
      var request = new Modulus10_13Request();

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      request.UpcCode.Should().BeNull();
      results.Should().BeEmpty();
   }

   [Fact]
   public void Modulus10_13CheckDigitAttribute_Validate_ShouldReturnSuccess_WhenNonRequiredValueIsEmpty()
   {
      // Arrange.
      var request = new Modulus10_13Request
      {
         UpcCode = String.Empty
      };

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().BeEmpty();
   }

   [Fact]
   public void Modulus10_13CheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueIsNotTypeString()
   {
      // Arrange.
      var item = new Modulus10_13RequestInvalidType
      {
         UpcCode = 123456
      };
      var expectedMessage = String.Format(Messages.InvalidPropertyType, nameof(item.UpcCode));

      // Act.
      var results = Utility.ValidateModel(item);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Fact]
   public void Modulus10_13CheckDigitAttribute_Validate_ShouldReturnFailure_WhenRequiredValueIsNull()
   {
      // Arrange.
      var request = new Modulus10_13RequestRequiredField();
      var expectedMessage = "The UpcCode field is required.";

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Fact]
   public void Modulus10_13CheckDigitAttribute_Validate_ShouldReturnFailure_WhenRequiredValueIsEmpty()
   {
      // Arrange.
      var request = new Modulus10_13RequestRequiredField
      {
         UpcCode = String.Empty
      };
      var expectedMessage = "The UpcCode field is required.";

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Fact]
   public void Modulus10_13CheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueHasInvalidModulus10_13CheckDigit()
   {
      // Arrange.
      var request = new Modulus10_13Request
      {
         UpcCode = "036000291455"
      };
      var expectedMessage = String.Format(Messages.SingleCheckDigitFailure, nameof(request.UpcCode));

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Fact]
   public void Modulus10_13CheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueHasInvalidModulus10_13CheckDigitAndCustomErrorMessageIsSupplied()
   {
      // Arrange.
      var request = new Modulus10_13RequestCustomMessage
      {
         UpcCode = "036000291455"
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
