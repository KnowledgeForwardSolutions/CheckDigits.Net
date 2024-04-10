// Ignore Spelling: sut

namespace CheckDigits.Net.Tests.Unit.Iso7064;

public class Iso7064PureSystemSingleCharacterAlgorithmTests
{
   private const String _algorithmName = "name";
   private const String _algorithmDescription = "description";
   private const Int32 _modulus = 11;
   private const Int32 _radix = 2;
   private static readonly ISupplementalCharacterAlphabet _alphabet = new DigitsSupplementalAlphabet();

   private static readonly Iso7064PureSystemSingleCharacterAlgorithm _alphanumericAlgorithm =
      new("Alphanumeric", "Alphanumeric, modulus = 37, radix = 2", 37, 2, new AlphanumericSupplementalAlphabet());
   private static readonly Iso7064PureSystemSingleCharacterAlgorithm _numericAlgorithm =
      new("Numeric", "Numeric, modulus = 11, radix = 2", 11, 2, new DigitsSupplementalAlphabet());

   #region Constructor Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso7064PureSystemSingleCharacterAlgorithm_Constructor_ShouldCreateObject_WhenValidValuesSupplied()
   {
      // Act.
      var sut = new Iso7064PureSystemSingleCharacterAlgorithm(
         _algorithmName,
         _algorithmDescription,
         _modulus,
         _radix,
         _alphabet);

      // Assert.
      sut.Should().NotBeNull();
   }

   [Fact]
   public void Iso7064PureSystemSingleCharacterAlgorithm_Constructor_ShouldThrowArgumentNullException_WhenAlgorithmNameIsNull()
   {
      // Arrange.
      String algorithmName = null!;
      var act = () => _ = new Iso7064PureSystemSingleCharacterAlgorithm(
         algorithmName,
         _algorithmDescription,
         _modulus,
         _radix,
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
   public void Iso7064PureSystemSingleCharacterAlgorithm_Constructor_ShouldThrowArgumentException_WhenAlgorithmNameIsEmpty(String algorithmName)
   {
      // Arrange.
      var act = () => _ = new Iso7064PureSystemSingleCharacterAlgorithm(
         algorithmName,
         _algorithmDescription,
         _modulus,
         _radix,
         _alphabet);
      var expectedMessage = Resources.AlgorithmNameIsEmptyMessage;

      // Act/assert.
      act.Should().ThrowExactly<ArgumentException>()
         .WithParameterName(nameof(algorithmName))
         .WithMessage(expectedMessage + "*");         
   }

   [Fact]
   public void Iso7064PureSystemSingleCharacterAlgorithm_Constructor_ShouldThrowArgumentNullException_WhenAlgorithmDescriptionIsNull()
   {
      // Arrange.
      String algorithmDescription = null!;
      var act = () => _ = new Iso7064PureSystemSingleCharacterAlgorithm(
         _algorithmName,
         algorithmDescription,
         _modulus,
         _radix,
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
   public void Iso7064PureSystemSingleCharacterAlgorithm_Constructor_ShouldThrowArgumentException_WhenAlgorithmDescriptionIsEmpty(String algorithmDescription)
   {
      // Arrange.
      var act = () => _ = new Iso7064PureSystemSingleCharacterAlgorithm(
         _algorithmName,
         algorithmDescription,
         _modulus,
         _radix,
         _alphabet);
      var expectedMessage = Resources.AlgorithmDescriptionIsEmptyMessage;

      // Act/assert.
      act.Should().ThrowExactly<ArgumentException>()
         .WithParameterName(nameof(algorithmDescription))
         .WithMessage(expectedMessage + "*");
   }

   [Fact]
   public void Iso7064PureSystemSingleCharacterAlgorithm_Constructor_ShouldThrowArgumentOutOfRangeException_WhenModulusIsLessThan2()
   {
      // Arrange.
      var modulus = 1;
      var act = () => _ = new Iso7064PureSystemSingleCharacterAlgorithm(
         _algorithmName,
         _algorithmDescription,
         modulus,
         _radix,
         _alphabet);
      var expectedMessage = Resources.Iso7064ModulusOutOfRange;

      // Act/assert.
      act.Should().ThrowExactly<ArgumentOutOfRangeException>()
         .WithParameterName(nameof(modulus))
         .WithMessage(expectedMessage + "*");
   }

   [Fact]
   public void Iso7064PureSystemSingleCharacterAlgorithm_Constructor_ShouldThrowArgumentOutOfRangeException_WhenRadixIsLessThan2()
   {
      // Arrange.
      var radix = 1;
      var act = () => _ = new Iso7064PureSystemSingleCharacterAlgorithm(
         _algorithmName,
         _algorithmDescription,
         _modulus,
         radix,
         _alphabet);
      var expectedMessage = Resources.Iso7064RadixOutOfRange;

      // Act/assert.
      act.Should().ThrowExactly<ArgumentOutOfRangeException>()
         .WithParameterName(nameof(radix))
         .WithMessage(expectedMessage + "*");
   }

   [Fact]
   public void Iso7064PureSystemSingleCharacterAlgorithm_Constructor_ShouldThrowArgumentNullException_WhenAlphabetIsNull()
   {
      // Arrange.
      ISupplementalCharacterAlphabet alphabet = null!;
      var act = () => _ = new Iso7064PureSystemSingleCharacterAlgorithm(
         _algorithmName,
         _algorithmDescription,
         _modulus,
         _radix,
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
   public void Iso7064PureSystemSingleCharacterAlgorithm_AlgorithmDescription_ShouldReturnExpectedValue()
   {
      // Arrange.
      var sut = new Iso7064PureSystemSingleCharacterAlgorithm(
         _algorithmName,
         _algorithmDescription,
         _modulus,
         _radix,
         _alphabet);

      // Act/assert.
      sut.AlgorithmDescription.Should().Be(_algorithmDescription);
   }

   #endregion

   #region AlgorithmName Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso7064PureSystemSingleCharacterAlgorithm_AlgorithmName_ShouldReturnExpectedValue()
   {
      // Arrange.
      var sut = new Iso7064PureSystemSingleCharacterAlgorithm(
         _algorithmName,
         _algorithmDescription,
         _modulus,
         _radix,
         _alphabet);

      // Act/assert.
      sut.AlgorithmName.Should().Be(_algorithmName);
   }

   #endregion

   #region TryCalculateCheckDigit Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso7064PureSystemSingleCharacterAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsNull()
   {
      // Arrange.
      var sut = _alphanumericAlgorithm;
      String value = null!;

      // Act/assert.
      sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Fact]
   public void Iso7064PureSystemSingleCharacterAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsEmpty()
   {
      // Arrange.
      var sut = _alphanumericAlgorithm;
      var value = String.Empty;

      // Act/assert.
      sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   public static TheoryData<Iso7064PureSystemSingleCharacterAlgorithm, String, Char> TryCalculateCheckDigitSuccessData => new()
   {
      { _numericAlgorithm, "0794", '0' },
      { _numericAlgorithm, "99999999999999999999999999999999999", '4' },
      { _alphanumericAlgorithm, "A999922123458", 'J' }
   };

   [Theory]
   [MemberData(nameof(TryCalculateCheckDigitSuccessData))]
   public void Iso7064Mod11_2AlgorithmAlgorithm_TryCalculateCheckDigit_ShouldCalculateExpectedCheckDigit(
      Iso7064PureSystemSingleCharacterAlgorithm sut,
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
   public void Iso7064PureSystemSingleCharacterAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputContainsInvalidCharacter(String value)
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
   public void Iso7064PureSystemSingleCharacterAlgorithm_Validate_ShouldReturnFalse_WhenInputIsNull()
   {
      // Arrange.
      var sut = _numericAlgorithm;
      String value = null!;

      // Act/assert.
      sut.Validate(value).Should().BeFalse();
   }

   [Fact]
   public void Iso7064PureSystemSingleCharacterAlgorithm_Validate_ShouldReturnFalse_WhenInputIsEmpty()
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
   public void Iso7064PureSystemSingleCharacterAlgorithm_Validate_ShouldReturnFalse_WhenInputIsOneCharacterInLength(String value)
   {
      // Arrange.
      var sut = _numericAlgorithm;

      // Act/assert.
      sut.Validate(value).Should().BeFalse();
   }

   public static TheoryData<Iso7064PureSystemSingleCharacterAlgorithm, String> ValidateSuccessData => new()
   {
      { _numericAlgorithm, "07940" },
      { _numericAlgorithm, "999999999999999999999999999999999994" },
      { _alphanumericAlgorithm, "A999922123458J" }
   };

   [Theory]
   [MemberData(nameof(ValidateSuccessData))]
   public void Iso7064PureSystemSingleCharacterAlgorithm_Validate_ShouldReturnTrue_WhenValueContainsValidCheckDigit(
      Iso7064PureSystemSingleCharacterAlgorithm sut,
      String value)
      => sut.Validate(value).Should().BeTrue();

   public static TheoryData<Iso7064PureSystemSingleCharacterAlgorithm, String> ValidateFailureData => new()
   {
      { _numericAlgorithm, "000000012156438X" },
      { _numericAlgorithm, "0000000444767411" },
      { _alphanumericAlgorithm, "G123468954321H" }
   };

   [Theory]
   [MemberData(nameof(ValidateFailureData))]
   public void Iso7064PureSystemSingleCharacterAlgorithm_Validate_ShouldReturnFalse_WhenInputContainsDetectableError(
      Iso7064PureSystemSingleCharacterAlgorithm sut,
      String value)
      => sut.Validate(value).Should().BeFalse();

   [Fact]
   public void Iso7064PureSystemSingleCharacterAlgorithm_Validate_ShouldReturnTrue_WhenCheckDigitIsCalculatesAsSupplementalCharacter()
   {
      // Arrange.
      var sut = _numericAlgorithm;
      var value = "079X";

      // Act/assert.
      sut.Validate(value).Should().BeTrue();
   }

   [Fact]
   public void Iso7064PureSystemSingleCharacterAlgorithm_Validate_ShouldReturnFalse_WhenValueContainsSupplementalCharacterInOtherThanTrailingPosition()
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
   public void Iso7064PureSystemSingleCharacterAlgorithm_Validate_ShouldReturnFalse_WhenValueContainsInvalidCharacter(String value)
   {
      // Arrange.
      var sut = _numericAlgorithm;

      // Act/assert.
      sut.Validate(value).Should().BeFalse();
   }

   [Fact]
   public void Iso7064PureSystemSingleCharacterAlgorithm_Validate_ShouldReturnFalse_WhenValueContainsInvalidCheckDigitCharacter()
   {
      // Arrange.
      var sut = _numericAlgorithm;
      var value = "12345Q";

      // Act/assert.
      sut.Validate(value).Should().BeFalse();
   }

   #endregion
}
