// Ignore Spelling: Damm

namespace CheckDigits.Net.DataAnnotations.Tests.Unit;

public class DammCheckDigitAlgorithmTests
{
   private const String _customErrorMessage = "Need a valid Submission Identifier";

   public class DammRequest
   {
      [DammCheckDigitAlgorithm]
      public String SubmissionIdentifier { get; set; } = null!;
   }

   public class DammRequestCustomMessage
   {
      [DammCheckDigitAlgorithm(ErrorMessage = _customErrorMessage)]
      public String SubmissionIdentifier { get; set; } = null!;
   }

   public class DammRequestRequiredField
   {
      [Required, DammCheckDigitAlgorithm]
      public String SubmissionIdentifier { get; set; } = null!;
   }

   public class DammRequestInvalidType
   {
      [DammCheckDigitAlgorithm]
      public Int32 SubmissionIdentifier { get; set; }
   }

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Theory]
   [InlineData("5724")]                      // Worked example from Wikipedia
   [InlineData("112946")]                    // Test data from https://www.rosettacode.org/wiki/Damm_algorithm#C#
   [InlineData("123456789018")]              // Value calculated by https://jackanderson.me/2020/09/damm-algorithm-check-digit-tool/
   public void DammCheckDigitAttribute_Validate_ShouldReturnSuccess_WhenValueHasValidDammCheckDigit(String value)
   {
      // Arrange.
      var request = new DammRequest
      {
         SubmissionIdentifier = value
      };

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().BeEmpty();
   }

   [Fact]
   public void DammCheckDigitAttribute_Validate_ShouldReturnSuccess_WhenNonRequiredValueIsNull()
   {
      // Arrange.
      var request = new DammRequest();

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      request.SubmissionIdentifier.Should().BeNull();
      results.Should().BeEmpty();
   }

   [Fact]
   public void DammCheckDigitAttribute_Validate_ShouldReturnSuccess_WhenNonRequiredValueIsEmpty()
   {
      // Arrange.
      var request = new DammRequest
      {
         SubmissionIdentifier = String.Empty
      };

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().BeEmpty();
   }

   [Fact]
   public void DammCheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueIsNotTypeString()
   {
      // Arrange.
      var item = new DammRequestInvalidType
      {
         SubmissionIdentifier = 123456
      };
      var expectedMessage = String.Format(Messages.InvalidPropertyType, nameof(item.SubmissionIdentifier));

      // Act.
      var results = Utility.ValidateModel(item);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Fact]
   public void DammCheckDigitAttribute_Validate_ShouldReturnFailure_WhenRequiredValueIsNull()
   {
      // Arrange.
      var request = new DammRequestRequiredField();
      var expectedMessage = "The SubmissionIdentifier field is required.";

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Fact]
   public void DammCheckDigitAttribute_Validate_ShouldReturnFailure_WhenRequiredValueIsEmpty()
   {
      // Arrange.
      var request = new DammRequestRequiredField
      {
         SubmissionIdentifier = String.Empty
      };
      var expectedMessage = "The SubmissionIdentifier field is required.";

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Theory]
   [InlineData("112233445566778899016")]     // Single digit errors (using "112233445566778899006" as a valid value)
   [InlineData("1236547890123450")]          // Jump transposition error using "1234567890123450" as a valid value (456 -> 654)
   [InlineData("112255445566778899006")]     // Twin error using "112233445566778899006" as a valid value (33 -> 55)
   public void DammCheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueHasInvalidDammCheckDigit(String value)
   {
      // Arrange.
      var request = new DammRequest
      {
         SubmissionIdentifier = value
      };
      var expectedMessage = String.Format(Messages.SingleCheckDigitFailure, nameof(request.SubmissionIdentifier));

      // Act.
      var results = Utility.ValidateModel(request);

      // Assert.
      results.Should().HaveCount(1);
      results[0].ErrorMessage.Should().Be(expectedMessage);
   }

   [Theory]
   [InlineData("112233445566778899016")]     // Single digit errors (using "112233445566778899006" as a valid value)
   [InlineData("1236547890123450")]          // Jump transposition error using "1234567890123450" as a valid value (456 -> 654)
   [InlineData("112255445566778899006")]     // Twin error using "112233445566778899006" as a valid value (33 -> 55)
   public void DammCheckDigitAttribute_Validate_ShouldReturnFailure_WhenValueHasInvalidDammCheckDigitAndCustomErrorMessageIsSupplied(String value)
   {
      // Arrange.
      var request = new DammRequestCustomMessage
      {
         SubmissionIdentifier = value
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
