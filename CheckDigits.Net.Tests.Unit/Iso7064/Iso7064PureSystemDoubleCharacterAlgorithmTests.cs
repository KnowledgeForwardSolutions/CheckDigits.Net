// Ignore Spelling: sut

namespace CheckDigits.Net.Tests.Unit.Iso7064;

public class Iso7064PureSystemDoubleCharacterAlgorithmTests
{
   private const String _algorithmName = "name";
   private const String _algorithmDescription = "description";
   private const Int32 _modulus = 97;
   private const Int32 _radix = 10;
   private static readonly IAlphabet _alphabet = new DigitsAlphabet();

   private static readonly Iso7064PureSystemDoubleCharacterAlgorithm _alphabeticAlgorithm =
      new("Alphabetic", "Alphabetic, modulus = 661, radix = 26", 661, 26, new LettersAlphabet());
   private static readonly Iso7064PureSystemDoubleCharacterAlgorithm _alphanumericAlgorithm =
      new("Alphanumeric", "Alphanumeric, modulus = 1271, radix = 36", 1271, 36, new AlphanumericAlphabet());
   private static readonly Iso7064PureSystemDoubleCharacterAlgorithm _numericAlgorithm =
      new("Numeric", "Numeric, modulus = 97, radix = 10", 97, 10, new DigitsAlphabet());

   private static readonly Iso7064PureSystemDoubleCharacterAlgorithm _danishAlgorithm =
      new("Danish", "Danish, modulus = 29, radix = 2", 29, 2, new DanishAlphabet());

   private readonly ICheckDigitMask _acceptAllMask = new AcceptAllMask();
   private readonly ICheckDigitMask _groupsOfThreeMask = new GroupsOfThreeCheckDigitMask();
   private readonly ICheckDigitMask _rejectAllMask = new RejectAllMask();

   #region Constructor Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso7064PureSystemDoubleCharacterAlgorithm_Constructor_ShouldCreateObject_WhenValidValuesSupplied()
   {
      // Act.
      var sut = new Iso7064PureSystemDoubleCharacterAlgorithm(
         _algorithmName,
         _algorithmDescription,
         _modulus,
         _radix,
         _alphabet);

      // Assert.
      sut.Should().NotBeNull();
   }

   [Fact]
   public void Iso7064PureSystemDoubleCharacterAlgorithm_Constructor_ShouldThrowArgumentNullException_WhenAlgorithmNameIsNull()
   {
      // Arrange.
      String algorithmName = null!;
      var act = () => _ = new Iso7064PureSystemDoubleCharacterAlgorithm(
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
   public void Iso7064PureSystemDoubleCharacterAlgorithm_Constructor_ShouldThrowArgumentException_WhenAlgorithmNameIsEmpty(String algorithmName)
   {
      // Arrange.
      var act = () => _ = new Iso7064PureSystemDoubleCharacterAlgorithm(
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
   public void Iso7064PureSystemDoubleCharacterAlgorithm_Constructor_ShouldThrowArgumentNullException_WhenAlgorithmDescriptionIsNull()
   {
      // Arrange.
      String algorithmDescription = null!;
      var act = () => _ = new Iso7064PureSystemDoubleCharacterAlgorithm(
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
   public void Iso7064PureSystemDoubleCharacterAlgorithm_Constructor_ShouldThrowArgumentException_WhenAlgorithmDescriptionIsEmpty(String algorithmDescription)
   {
      // Arrange.
      var act = () => _ = new Iso7064PureSystemDoubleCharacterAlgorithm(
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
   public void Iso7064PureSystemDoubleCharacterAlgorithm_Constructor_ShouldThrowArgumentOutOfRangeException_WhenModulusIsLessThan2()
   {
      // Arrange.
      var modulus = 1;
      var act = () => _ = new Iso7064PureSystemDoubleCharacterAlgorithm(
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
   public void Iso7064PureSystemDoubleCharacterAlgorithm_Constructor_ShouldThrowArgumentOutOfRangeException_WhenRadixIsLessThan2()
   {
      // Arrange.
      var radix = 1;
      var act = () => _ = new Iso7064PureSystemDoubleCharacterAlgorithm(
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
   public void Iso7064PureSystemDoubleCharacterAlgorithm_Constructor_ShouldThrowArgumentNullException_WhenAlphabetIsNull()
   {
      // Arrange.
      IAlphabet alphabet = null!;
      var act = () => _ = new Iso7064PureSystemDoubleCharacterAlgorithm(
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
   public void Iso7064PureSystemDoubleCharacterAlgorithm_AlgorithmDescription_ShouldReturnExpectedValue()
   {
      // Arrange.
      var sut = new Iso7064PureSystemDoubleCharacterAlgorithm(
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
   public void Iso7064PureSystemDoubleCharacterAlgorithm_AlgorithmName_ShouldReturnExpectedValue()
   {
      // Arrange.
      var sut = new Iso7064PureSystemDoubleCharacterAlgorithm(
         _algorithmName,
         _algorithmDescription,
         _modulus,
         _radix,
         _alphabet);

      // Act/assert.
      sut.AlgorithmName.Should().Be(_algorithmName);
   }

   #endregion

   #region TryCalculateCheckDigits Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso7064PureSystemDoubleCharacterAlgorithm_TryCalculateCheckDigits_ShouldReturnFalse_WhenInputIsNull()
   {
      // Arrange.
      var sut = _alphanumericAlgorithm;
      String value = null!;

      // Act/assert.
      sut.TryCalculateCheckDigits(value, out var first, out var second).Should().BeFalse();
      first.Should().Be('\0');
      second.Should().Be('\0');
   }

   [Fact]
   public void Iso7064PureSystemDoubleCharacterAlgorithm_TryCalculateCheckDigits_ShouldReturnFalse_WhenInputIsEmpty()
   {
      // Arrange.
      var sut = _alphanumericAlgorithm;
      var value = String.Empty;

      // Act/assert.
      sut.TryCalculateCheckDigits(value, out var first, out var second).Should().BeFalse();
      first.Should().Be('\0');
      second.Should().Be('\0');
   }

   public static TheoryData<Iso7064PureSystemDoubleCharacterAlgorithm, String, Char, Char> TryCalculateCheckDigitsSuccessData => new()
   {
      { _numericAlgorithm, "123456", '7', '6' },
      { _numericAlgorithm, "10113393912554329261011442299914333", '3', '8' },
      { _alphabeticAlgorithm, "ISOHJ", 'T', 'C' },
      { _alphanumericAlgorithm, "XS868977863229", 'A', 'U' },
      { _danishAlgorithm, "S\u00D8STER", 'D', 'A' }
   };

   [Theory]
   [MemberData(nameof(TryCalculateCheckDigitsSuccessData))]
   public void Iso7064PureSystemDoubleCharacterAlgorithm_TryCalculateCheckDigits_ShouldCalculateExpectedCheckDigit(
      Iso7064PureSystemDoubleCharacterAlgorithm sut,
      String value,
      Char expectedFirst,
      Char expectedSecond)
   {
      // Act/assert.
      sut.TryCalculateCheckDigits(value, out var first, out var second).Should().BeTrue();
      first.Should().Be(expectedFirst);
      second.Should().Be(expectedSecond);
   }

   [Theory]
   [InlineData("123!56")]
   [InlineData("123^56")]
   [InlineData("123=56")]
   public void Iso7064PureSystemDoubleCharacterAlgorithm_TryCalculateCheckDigits_ShouldReturnFalse_WhenInputContainsInvalidCharacter(String value)
   {
      // Arrange.
      var sut = _alphanumericAlgorithm;

      // Act/assert.
      sut.TryCalculateCheckDigits(value, out var first, out var second).Should().BeFalse();
      first.Should().Be('\0');
      second.Should().Be('\0');
   }

   #endregion

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso7064PureSystemDoubleCharacterAlgorithm_Validate_ShouldReturnFalse_WhenInputIsNull()
   {
      // Arrange.
      var sut = _numericAlgorithm;
      String value = null!;

      // Act/assert.
      sut.Validate(value).Should().BeFalse();
   }

   [Fact]
   public void Iso7064PureSystemDoubleCharacterAlgorithm_Validate_ShouldReturnFalse_WhenInputIsEmpty()
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
   [InlineData("00")]    
   [InlineData("01")]
   public void Iso7064PureSystemDoubleCharacterAlgorithm_Validate_ShouldReturnFalse_WhenInputIsLessThanThreeCharactersInLength(String value)
   {
      // Arrange.
      var sut = _numericAlgorithm;

      // Act/assert.
      sut.Validate(value).Should().BeFalse();
   }

   public static TheoryData<Iso7064PureSystemDoubleCharacterAlgorithm, String> ValidateSuccessData => new()
   {
      { _numericAlgorithm, "12345676" },
      { _numericAlgorithm, "1011339391255432926101144229991433338" },
      { _alphabeticAlgorithm, "ISOHJTC" },
      { _alphanumericAlgorithm, "XS868977863229AU" },
      { _danishAlgorithm, "S\u00D8STERDA" }
   };

   [Theory]
   [MemberData(nameof(ValidateSuccessData))]
   public void Iso7064PureSystemDoubleCharacterAlgorithm_Validate_ShouldReturnTrue_WhenValueContainsValidCheckDigit(
      Iso7064PureSystemDoubleCharacterAlgorithm sut,
      String value)
      => sut.Validate(value).Should().BeTrue();

   public static TheoryData<Iso7064PureSystemDoubleCharacterAlgorithm, String> ValidateFailureData => new()
   {
      { _numericAlgorithm, "163217541835191038" },          // 163217581835191038 with single char transcription error 8 -> 4
      { _numericAlgorithm, "163217581835191138" },          // 163217581835191038 with single char transcription error 0 -> 1
      { _numericAlgorithm, "12455676" },                    // 12345676 with two char transposition error 34 -> 45 
      { _alphabeticAlgorithm, "AAAEEEIIIOPPUUUBCDEFJY" },   // AAAEEEIIIOOOUUUBCDEFJY with two char twin error OO -> PP
      { _alphabeticAlgorithm, "ABCFEDGHIJKLMNJF" },         // ABCDEFGHIJKLMNJF with jump transposition error DEF -> FED
      { _alphanumericAlgorithm, "123456789ADCBEH2" },       // 123456789ABCDEH2 with jump transposition error BCD -> DCB
      { _alphanumericAlgorithm, "UXS868977863229A" },       // XS868977863229AU with circular shift error
      { _danishAlgorithm, "AS\u00D8STERD" },                // S\u00D8STERDA with circular shift error
   };

   [Theory]
   [MemberData(nameof(ValidateFailureData))]
   public void Iso7064PureSystemDoubleCharacterAlgorithm_Validate_ShouldReturnFalse_WhenInputContainsDetectableError(
      Iso7064PureSystemDoubleCharacterAlgorithm sut,
      String value)
      => sut.Validate(value).Should().BeFalse();

   [Fact]
   public void Iso7064PureSystemDoubleCharacterAlgorithm_Validate_ShouldReturnFalse_WhenValueContainsSupplementalCharacterInOtherThanTrailingPosition()
   {
      // Arrange.
      var sut = _numericAlgorithm;
      var value = "079X8";

      // Act/assert.
      sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   [InlineData("123!5676")]
   [InlineData("123^5676")]
   [InlineData("123=5676")]
   [InlineData("123a5676")]
   public void Iso7064PureSystemDoubleCharacterAlgorithm_Validate_ShouldReturnFalse_WhenValueContainsInvalidCharacter(String value)
   {
      // Arrange.
      var sut = _numericAlgorithm;

      // Act/assert.
      sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   [InlineData("123456!6")]
   [InlineData("123456^6")]
   [InlineData("123456=6")]
   [InlineData("123456a6")]
   public void Iso7064PureSystemDoubleCharacterAlgorithm_Validate_ShouldReturnFalse_WhenValueContainsInvalidFirstCheckCharacter(String value)
   {
      // Arrange.
      var sut = _numericAlgorithm;

      // Act/assert.
      sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   [InlineData("1234567!")]
   [InlineData("1234567^")]
   [InlineData("1234567=")]
   [InlineData("1234567a")]
   public void Iso7064PureSystemDoubleCharacterAlgorithm_Validate_ShouldReturnFalse_WhenValueContainsInvalidSecondCheckCharacter(String value)
   {
      // Arrange.
      var sut = _numericAlgorithm;

      // Act/assert.
      sut.Validate(value).Should().BeFalse();
   }

   #endregion

   #region Validate (ICheckDigitMask Overload) Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso7064PureSystemDoubleCharacterAlgorithm_ValidateMasked_ShouldThrowArgumentNullException_WhenMaskIsNull()
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
   public void Iso7064PureSystemDoubleCharacterAlgorithm_ValidateMasked_ShouldReturnFalse_WhenInputIsNull()
   {
      // Arrange.
      var sut = _alphanumericAlgorithm;

      // Act/assert.
      sut
         .Validate(null!, _acceptAllMask).Should().BeFalse();
   }

   [Fact]
   public void Iso7064PureSystemDoubleCharacterAlgorithm_ValidateMasked_ShouldReturnFalse_WhenInputIsEmpty()
   {
      // Arrange.
      var sut = _alphanumericAlgorithm;

      // Act/assert.
      sut.Validate(String.Empty, _acceptAllMask).Should().BeFalse();
   }

   [Theory]
   [InlineData("0")]    
   [InlineData("1")]    
   [InlineData("00")]    
   [InlineData("01")]
   public void Iso7064PureSystemDoubleCharacterAlgorithm_ValidateMasked_ShouldReturnFalse_WhenInsufficientUnmaskedCharactersToCalculateCheckDigit(String value)
   {
      // Arrange.
      var sut = _numericAlgorithm;

      // Act/assert.
      sut.Validate(value, _acceptAllMask).Should().BeFalse();
   }

   [Fact]
   public void Iso7064PureSystemDoubleCharacterAlgorithm_ValidateMasked_ShouldReturnFalse_WhenAllNonCheckDigitCharactersAreMaskedOut()
   {
      // Arrange.
      var sut = _alphanumericAlgorithm;

      // Act/assert.
      sut.Validate("XS868977863229AU", _rejectAllMask).Should().BeFalse();
   }

   public static TheoryData<Iso7064PureSystemDoubleCharacterAlgorithm, String> ValidateMaskedSuccessData => new()
   {
      { _numericAlgorithm, "123 456 76" },
      { _numericAlgorithm, "101 133 939 125 543 292 610 114 422 999 143 3338" },
      { _alphabeticAlgorithm, "ISO HJTC" },
      { _alphanumericAlgorithm, "XS8 689 778 632 29AU" },
      { _danishAlgorithm, "S\u00D8S TER DA" }
   };

   [Theory]
   [MemberData(nameof(ValidateMaskedSuccessData))]
   public void Iso7064PureSystemDoubleCharacterAlgorithm_ValidateMasked_ShouldReturnTrue_WhenValueContainsValidCheckDigit(
      Iso7064PureSystemDoubleCharacterAlgorithm sut,
      String value)
      => sut.Validate(value, _groupsOfThreeMask).Should().BeTrue();

   [Theory]
   [MemberData(nameof(ValidateSuccessData))]    // Note unmasked test data
   public void Iso7064PureSystemDoubleCharacterAlgorithm_ValidateMasked_ShouldReturnTrue_WhenValueContainsValidCheckDigitAndMaskAcceptsAllCharacters(
      Iso7064PureSystemDoubleCharacterAlgorithm sut,
      String value)
      => sut.Validate(value, _acceptAllMask).Should().BeTrue();

   public static TheoryData<Iso7064PureSystemDoubleCharacterAlgorithm, String> ValidateMaskedFailureData => new()
   {
      { _numericAlgorithm, "163 217 541 835 191 038" },           // 163217581835191038 with single char transcription error 8 -> 4
      { _numericAlgorithm, "163 217 581 835 191 138" },           // 163217581835191038 with single char transcription error 0 -> 1
      { _numericAlgorithm, "124 556 76" },                        // 12345676 with two char transposition error 34 -> 45 
      { _alphabeticAlgorithm, "AAA EEE III OPP UUU BCD EFJY" },   // AAAEEEIIIOOOUUUBCDEFJY with two char twin error OO -> PP
      { _alphabeticAlgorithm, "ABC FED GHI JKL MNJF" },           // ABCDEFGHIJKLMNJF with jump transposition error DEF -> FED
      { _alphanumericAlgorithm, "123 456 789 ADC BEH2" },         // 123456789ABCDEH2 with jump transposition error BCD -> DCB
      { _alphanumericAlgorithm, "UXS 868 977 863 229A" },         // XS868977863229AU with circular shift error
      { _danishAlgorithm, "AS\u00D8 STE RD" },                    // S\u00D8STERDA with circular shift error
   };

   [Theory]
   [MemberData(nameof(ValidateMaskedFailureData))]
   public void Iso7064PureSystemDoubleCharacterAlgorithm_ValidateMasked_ShouldReturnFalse_WhenInputContainsDetectableError(
      Iso7064PureSystemDoubleCharacterAlgorithm sut,
      String value)
      => sut.Validate(value, _groupsOfThreeMask).Should().BeFalse();

   [Fact]
   public void Iso7064PureSystemDoubleCharacterAlgorithm_ValidateMasked_ShouldReturnFalse_WhenValueContainsSupplementalCharacterInOtherThanTrailingPosition()
   {
      // Arrange.
      var sut = _numericAlgorithm;
      var value = "079 X8";

      // Act/assert.
      sut.Validate(value, _groupsOfThreeMask).Should().BeFalse();
   }

   [Theory]
   [InlineData("123 !56 76")]
   [InlineData("123 ^56 76")]
   [InlineData("123 =56 76")]
   [InlineData("123 a56 76")]
   public void Iso7064PureSystemDoubleCharacterAlgorithm_ValidateMasked_ShouldReturnFalse_WhenValueContainsInvalidCharacter(String value)
   {
      // Arrange.
      var sut = _numericAlgorithm;

      // Act/assert.
      sut.Validate(value, _groupsOfThreeMask).Should().BeFalse();
   }

   [Theory]
   [InlineData("123 456 !6")]
   [InlineData("123 456 ^6")]
   [InlineData("123 456 =6")]
   [InlineData("123 456 a6")]
   public void Iso7064PureSystemDoubleCharacterAlgorithm_ValidateMasked_ShouldReturnFalse_WhenValueContainsInvalidFirstCheckCharacter(String value)
   {
      // Arrange.
      var sut = _numericAlgorithm;

      // Act/assert.
      sut.Validate(value, _groupsOfThreeMask).Should().BeFalse();
   }

   [Theory]
   [InlineData("123 456 7!")]
   [InlineData("123 456 7^")]
   [InlineData("123 456 7=")]
   [InlineData("123 456 7a")]
   public void Iso7064PureSystemDoubleCharacterAlgorithm_ValidateMasked_ShouldReturnFalse_WhenValueContainsInvalidSecondCheckCharacter(String value)
   {
      // Arrange.
      var sut = _numericAlgorithm;

      // Act/assert.
      sut.Validate(value, _groupsOfThreeMask).Should().BeFalse();
   }

   #endregion
}
