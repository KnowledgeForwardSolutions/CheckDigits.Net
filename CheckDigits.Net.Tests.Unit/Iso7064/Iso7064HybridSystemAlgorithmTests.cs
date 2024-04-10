// Ignore Spelling: sut

namespace CheckDigits.Net.Tests.Unit.Iso7064;

public class Iso7064HybridSystemAlgorithmTests
{
   private const String _algorithmName = "name";
   private const String _algorithmDescription = "description";
   private const Int32 _modulus = 10;
   private static readonly IAlphabet _alphabet = new DigitsAlphabet();

   private static readonly Iso7064HybridSystemAlgorithm _alphabeticAlgorithm =
      new("Alphabetic", "Alphabetic, modulus = 26", 26, new LettersAlphabet());
   private static readonly Iso7064HybridSystemAlgorithm _alphanumericAlgorithm =
      new("Alphanumeric", "Alphanumeric, modulus = 36", 36, new AlphanumericAlphabet());
   private static readonly Iso7064HybridSystemAlgorithm _numericAlgorithm =
      new("Numeric", "Numeric, modulus = 10", 10, new DigitsAlphabet());

   #region Constructor Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso7064HybridAlgorithm_Constructor_ShouldCreateObject_WhenValidValuesSupplied()
   {
      // Act.
      var sut = new Iso7064HybridSystemAlgorithm(
         _algorithmName,
         _algorithmDescription,
         _modulus,
         _alphabet);

      // Assert.
      sut.Should().NotBeNull();
   }

   [Fact]
   public void Iso7064HybridAlgorithm_Constructor_ShouldThrowArgumentNullException_WhenAlgorithmNameIsNull()
   {
      // Arrange.
      String algorithmName = null!;
      var act = () => _ = new Iso7064HybridSystemAlgorithm(
         algorithmName,
         _algorithmDescription,
         _modulus,
         _alphabet);
      var expectedMessage = Resources.AlgorithmNameIsEmptyMessage;

      // Act/assert.
      act.Should().ThrowExactly<ArgumentNullException>()
         .WithParameterName(nameof(algorithmName))
         .WithMessage(expectedMessage + "*");
   }

   [Theory]
   [InlineData("")]
   [InlineData("\t")]
   public void Iso7064HybridAlgorithm_Constructor_ShouldThrowArgumentException_WhenAlgorithmNameIsEmpty(String algorithmName)
   {
      // Arrange.
      var act = () => _ = new Iso7064HybridSystemAlgorithm(
         algorithmName,
         _algorithmDescription,
         _modulus,
         _alphabet);
      var expectedMessage = Resources.AlgorithmNameIsEmptyMessage;

      // Act/assert.
      act.Should().ThrowExactly<ArgumentException>()
         .WithParameterName(nameof(algorithmName))
         .WithMessage(expectedMessage + "*");
   }

   [Fact]
   public void Iso7064HybridAlgorithm_Constructor_ShouldThrowArgumentNullException_WhenAlgorithmDescriptionIsNull()
   {
      // Arrange.
      String algorithmDescription = null!;
      var act = () => _ = new Iso7064HybridSystemAlgorithm(
         _algorithmName,
         algorithmDescription,
         _modulus,
         _alphabet);
      var expectedMessage = Resources.AlgorithmDescriptionIsEmptyMessage;

      // Act/assert.
      act.Should().ThrowExactly<ArgumentNullException>()
         .WithParameterName(nameof(algorithmDescription))
         .WithMessage(expectedMessage + "*");
   }

   [Theory]
   [InlineData("")]
   [InlineData("\t")]
   public void Iso7064HybridAlgorithm_Constructor_ShouldThrowArgumentException_WhenAlgorithmDescriptionIsEmpty(String algorithmDescription)
   {
      // Arrange.
      var act = () => _ = new Iso7064HybridSystemAlgorithm(
         _algorithmName,
         algorithmDescription,
         _modulus,
         _alphabet);
      var expectedMessage = Resources.AlgorithmDescriptionIsEmptyMessage;

      // Act/assert.
      act.Should().ThrowExactly<ArgumentException>()
         .WithParameterName(nameof(algorithmDescription))
         .WithMessage(expectedMessage + "*");
   }

   [Fact]
   public void Iso7064HybridAlgorithm_Constructor_ShouldThrowArgumentOutOfRangeException_WhenModulusIsLessThan2()
   {
      // Arrange.
      var modulus = 1;
      var act = () => _ = new Iso7064HybridSystemAlgorithm(
         _algorithmName,
         _algorithmDescription,
         modulus,
         _alphabet);
      var expectedMessage = Resources.Iso7064ModulusOutOfRange;

      // Act/assert.
      act.Should().ThrowExactly<ArgumentOutOfRangeException>()
         .WithParameterName(nameof(modulus))
         .WithMessage(expectedMessage + "*");
   }

   [Fact]
   public void Iso7064HybridAlgorithm_Constructor_ShouldThrowArgumentNullException_WhenAlphabetIsNull()
   {
      // Arrange.
      IAlphabet alphabet = null!;
      var act = () => _ = new Iso7064HybridSystemAlgorithm(
         _algorithmName,
         _algorithmDescription,
         _modulus,
         alphabet);
      var expectedMessage = Resources.Iso7046AlphabetIsNull;

      // Act/assert.
      act.Should().ThrowExactly<ArgumentNullException>()
         .WithParameterName(nameof(alphabet))
         .WithMessage(expectedMessage + "*");
   }
   #endregion

   #region AlgorithmDescription Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso7064HybridSystemAlgorithm_AlgorithmDescription_ShouldReturnExpectedValue()
   {
      // Arrange.
      var sut = new Iso7064HybridSystemAlgorithm(
         _algorithmName,
         _algorithmDescription,
         _modulus,
         _alphabet);

      // Act/assert.
      sut.AlgorithmDescription.Should().Be(_algorithmDescription);
   }

   #endregion

   #region AlgorithmName Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso7064HybridSystemAlgorithm_AlgorithmName_ShouldReturnExpectedValue()
   {
      // Arrange.
      var sut = new Iso7064HybridSystemAlgorithm(
         _algorithmName,
         _algorithmDescription,
         _modulus,
         _alphabet);

      // Act/assert.
      sut.AlgorithmName.Should().Be(_algorithmName);
   }

   #endregion

   #region TryCalculateCheckDigit Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso7064HybridSystemAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsNull()
   {
      // Arrange.
      var sut = _alphanumericAlgorithm;
      String value = null!;

      // Act/assert.
      sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Fact]
   public void Iso7064HybridSystemAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsEmpty()
   {
      // Arrange.
      var sut = _alphanumericAlgorithm;
      var value = String.Empty;

      // Act/assert.
      sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   public static TheoryData<Iso7064HybridSystemAlgorithm, String, Char> TryCalculateCheckDigitSuccessData => new()
   {
      { _numericAlgorithm, "0794", '5' },
      { _alphabeticAlgorithm, "QWERTYDVORAK", 'Y' },
      { _alphanumericAlgorithm, "A1B2C3D4E5F6G7H8I9J0K", 'I' }
   };

   [Theory]
   [MemberData(nameof(TryCalculateCheckDigitSuccessData))]
   public void Iso7064Mod11_2AlgorithmAlgorithm_TryCalculateCheckDigit_ShouldCalculateExpectedCheckDigit(
      Iso7064HybridSystemAlgorithm sut,
      String value,
      Char expectedCheckDigit)
   {
      // Act/assert.
      sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Theory]
   [InlineData("123!56")]
   [InlineData("123^56")]
   [InlineData("123=56")]
   public void Iso7064HybridSystemAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputContainsInvalidCharacter(String value)
   {
      // Arrange.
      var sut = _alphanumericAlgorithm;

      // Act/assert.
      sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   #endregion

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso7064HybridSystemAlgorithm_Validate_ShouldReturnFalse_WhenInputIsNull()
   {
      // Arrange.
      var sut = _numericAlgorithm;
      String value = null!;

      // Act/assert.
      sut.Validate(value).Should().BeFalse();
   }

   [Fact]
   public void Iso7064HybridSystemAlgorithm_Validate_ShouldReturnFalse_WhenInputIsEmpty()
   {
      // Arrange.
      var sut = _numericAlgorithm;
      var value = String.Empty;

      // Act/assert.
      sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   [InlineData("0")]
   [InlineData("1")]
   public void Iso7064HybridSystemAlgorithm_Validate_ShouldReturnFalse_WhenInputIsOneCharacterInLength(String value)
   {
      // Arrange.
      var sut = _numericAlgorithm;

      // Act/assert.
      sut.Validate(value).Should().BeFalse();
   }

   public static TheoryData<Iso7064HybridSystemAlgorithm, String> ValidateSuccessData => new()
   {
      { _numericAlgorithm, "07945" },
      { _alphabeticAlgorithm, "QWERTYDVORAKY" },
      { _alphanumericAlgorithm, "A1B2C3D4E5F6G7H8I9J0KI" }
   };

   [Theory]
   [MemberData(nameof(ValidateSuccessData))]
   public void Iso7064HybridSystemAlgorithm_Validate_ShouldReturnTrue_WhenValueContainsValidCheckDigit(
      Iso7064HybridSystemAlgorithm sut,
      String value)
      => sut.Validate(value).Should().BeTrue();

   public static TheoryData<Iso7064HybridSystemAlgorithm, String> ValidateFailureData => new()
   {
      { _numericAlgorithm, "123465788" },
      { _alphabeticAlgorithm, "QWERTUDVORAKY" },
      { _alphanumericAlgorithm, "A1B2C3D4E5F67GH8I9J0KI" }
   };

   [Theory]
   [MemberData(nameof(ValidateFailureData))]
   public void Iso7064HybridSystemAlgorithm_Validate_ShouldReturnFalse_WhenInputContainsDetectableError(
      Iso7064HybridSystemAlgorithm sut,
      String value)
      => sut.Validate(value).Should().BeFalse();

   [Fact]
   public void Iso7064HybridSystemAlgorithm_Validate_ShouldReturnFalse_WhenValueContainsSupplementalCharacterInOtherThanTrailingPosition()
   {
      // Arrange.
      var sut = _numericAlgorithm;
      var value = "079X8";

      // Act/assert.
      sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   [InlineData("123D56X")]
   [InlineData("123!56X")]
   [InlineData("123^56X")]
   public void Iso7064HybridSystemAlgorithm_Validate_ShouldReturnFalse_WhenValueContainsInvalidCharacter(String value)
   {
      // Arrange.
      var sut = _numericAlgorithm;

      // Act/assert.
      sut.Validate(value).Should().BeFalse();
   }

   [Fact]
   public void Iso7064HybridSystemAlgorithm_Validate_ShouldReturnFalse_WhenValueContainsInvalidCheckDigitCharacter()
   {
      // Arrange.
      var sut = _numericAlgorithm;
      var value = "12345Q";

      // Act/assert.
      sut.Validate(value).Should().BeFalse();
   }

   #endregion
}
