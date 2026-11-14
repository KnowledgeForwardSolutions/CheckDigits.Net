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

   private readonly ICheckDigitMask _acceptAllMask = new AcceptAllMask();
   private readonly ICheckDigitMask _groupsOfThreeMask = new GroupsOfThreeCheckDigitMask();
   private readonly ICheckDigitMask _rejectAllMask = new RejectAllMask();

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
   public void Iso7064HybridSystemAlgorithm_TryCalculateCheckDigit_ShouldCalculateExpectedCheckDigit(
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
      { _numericAlgorithm, "123556788" },                      // 123456788 with single digit transcription error 4 -> 5
      { _numericAlgorithm, "16341758183519103" },              // 16321758183519103 with single digit transcription error 2 -> 4
      { _numericAlgorithm, "123465788" },                      // 123456788 with two digit transposition error 56 -> 65 
      { _alphabeticAlgorithm, "THISISATESUUHISISONLYATESTT" }, // THISISATESTTHISISONLYATESTT with two char twin error TT -> UU
      { _alphabeticAlgorithm, "ABCFEDGHIJKLMNOPQRO" },         // ABCDEFGHIJKLMNOPQRO with jump transposition error DEF -> FED
      { _alphabeticAlgorithm, "QWERDYTVORAKY" },               // QWERTYDVORAKY with jump transposition error TYD -> DYT
      { _alphanumericAlgorithm, "UAEIOU" },                    // AEIOUU with circular shift error
      { _alphanumericAlgorithm, "EIOUUA" },                    // AEIOUU with circular shift error
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

   #region Validate (ICheckDigitMask Overload) Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso7064HybridSystemAlgorithm_ValidateMasked_ShouldThrowArgumentNullException_WhenMaskIsNull()
   {
      // Arrange.
      var sut = _numericAlgorithm;

      // Act/assert.
      sut
         .Invoking(x => x.Validate("07945", null!))
         .Should()
         .ThrowExactly<ArgumentNullException>()
         .WithParameterName("mask")
         .WithMessage(Resources.NullMaskMessage + "*");
   }

   [Fact]
   public void Iso7064HybridSystemAlgorithm_ValidateMasked_ShouldReturnFalse_WhenInputIsNull()
   {
      // Arrange.
      var sut = _alphanumericAlgorithm;

      // Act/assert.
      sut
         .Validate(null!, _acceptAllMask).Should().BeFalse();
   }

   [Fact]
   public void Iso7064HybridSystemAlgorithm_ValidateMasked_ShouldReturnFalse_WhenInputIsEmpty()
   {
      // Arrange.
      var sut = _alphanumericAlgorithm;

      // Act/assert.
      sut.Validate(String.Empty, _acceptAllMask).Should().BeFalse();
   }

   [Theory]
   [InlineData("0")]    
   [InlineData("1")]
   public void Iso7064HybridSystemAlgorithm_ValidateMasked_ShouldReturnFalse_WhenInsufficientUnmaskedCharactersToCalculateCheckDigit(String value)
   {
      // Arrange.
      var sut = _numericAlgorithm;

      // Act/assert.
      sut.Validate(value, _acceptAllMask).Should().BeFalse();
   }

   [Fact]
   public void Iso7064HybridSystemAlgorithm_ValidateMasked_ShouldReturnFalse_WhenAllNonCheckDigitCharactersAreMaskedOut()
   {
      // Arrange.
      var sut = _alphanumericAlgorithm;

      // Act/assert.
      sut.Validate("A1B2C3D4E5F6G7H8I9J0KI", _rejectAllMask).Should().BeFalse();
   }

   public static TheoryData<Iso7064HybridSystemAlgorithm, String> ValidateMaskedSuccessData => new()
   {
      { _numericAlgorithm, "079 45" },
      { _alphabeticAlgorithm, "QWE RTY DVO RAK Y" },
      { _alphanumericAlgorithm, "A1B 2C3 D4E 5F6 G7H 8I9 J0K I" }
   };

   [Theory]
   [MemberData(nameof(ValidateMaskedSuccessData))]
   public void Iso7064HybridSystemAlgorithm_ValidateMasked_ShouldReturnTrue_WhenValueContainsValidCheckDigit(
      Iso7064HybridSystemAlgorithm sut,
      String value)
      => sut.Validate(value, _groupsOfThreeMask).Should().BeTrue();

   public static TheoryData<Iso7064HybridSystemAlgorithm, String> ValidateMaskedFailureData => new()
   {
      { _numericAlgorithm, "123 556 788" },                             // 123456788 with single digit transcription error 4 -> 5
      { _numericAlgorithm, "163 417 581 835 191 03" },                  // 16321758183519103 with single digit transcription error 2 -> 4
      { _numericAlgorithm, "123 465 788" },                             // 123456788 with two digit transposition error 56 -> 65 
      { _alphabeticAlgorithm, "THI SIS ATE SUU HIS ISO NLY ATE STT" },  // THISISATESTTHISISONLYATESTT with two char twin error TT -> UU
      { _alphabeticAlgorithm, "ABC FED GHI JKL MN OPQ RO" },            // ABCDEFGHIJKLMNOPQRO with jump transposition error DEF -> FED
      { _alphabeticAlgorithm, "QWE RDY TVO RAK Y" },                    // QWERTYDVORAKY with jump transposition error TYD -> DYT
      { _alphanumericAlgorithm, "UAE IOU" },                            // AEIOUU with circular shift error
      { _alphanumericAlgorithm, "EIO UUA" },                            // AEIOUU with circular shift error
   };

   [Theory]
   [MemberData(nameof(ValidateMaskedFailureData))]
   public void Iso7064HybridSystemAlgorithm_ValidateMasked_ShouldReturnFalse_WhenInputContainsDetectableError(
      Iso7064HybridSystemAlgorithm sut,
      String value)
      => sut.Validate(value, _groupsOfThreeMask).Should().BeFalse();

   [Fact]
   public void Iso7064HybridSystemAlgorithm_ValidateMasked_ShouldReturnFalse_WhenValueContainsSupplementalCharacterInOtherThanTrailingPosition()
   {
      // Arrange.
      var sut = _numericAlgorithm;
      var value = "079 X8";

      // Act/assert.
      sut.Validate(value, _groupsOfThreeMask).Should().BeFalse();
   }

   [Theory]
   [InlineData("123 D56 X")]
   [InlineData("123 !56 X")]
   [InlineData("123 ^56 X")]
   public void Iso7064HybridSystemAlgorithm_ValidateMasked_ShouldReturnFalse_WhenValueContainsInvalidCharacter(String value)
   {
      // Arrange.
      var sut = _numericAlgorithm;

      // Act/assert.
      sut.Validate(value, _groupsOfThreeMask).Should().BeFalse();
   }

   [Fact]
   public void Iso7064HybridSystemAlgorithm_ValidateMasked_ShouldReturnFalse_WhenValueContainsInvalidCheckDigitCharacter()
   {
      // Arrange.
      var sut = _numericAlgorithm;
      var value = "123 45Q";

      // Act/assert.
      sut.Validate(value, _groupsOfThreeMask).Should().BeFalse();
   }

   #endregion
}
