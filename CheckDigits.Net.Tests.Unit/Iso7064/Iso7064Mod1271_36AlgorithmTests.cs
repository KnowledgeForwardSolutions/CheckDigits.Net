namespace CheckDigits.Net.Tests.Unit.Iso7064;

public class Iso7064Mod1271_36AlgorithmTests
{
   private readonly Iso7064Mod1271_36Algorithm _sut = new();
   private readonly ICheckDigitMask _acceptAllMask = new AcceptAllMask();
   private readonly ICheckDigitMask _groupsOfThreeMask = new GroupsOfThreeCheckDigitMask();
   private readonly ICheckDigitMask _rejectAllMask = new RejectAllMask();

   #region AlgorithmDescription Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso7064Mod1271_36Algorithm_AlgorithmDescription_ShouldReturnExpectedValue()
      => _sut.AlgorithmDescription.Should().Be(Resources.Iso7064Mod1271_36AlgorithmDescription);

   #endregion

   #region AlgorithmName Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso7064Mod1271_36Algorithm_AlgorithmName_ShouldReturnExpectedValue()
      => _sut.AlgorithmName.Should().Be(Resources.Iso7064Mod1271_36AlgorithmName);

   #endregion

   #region TryCalculateCheckDigits Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso7064Mod1271_36Algorithm_TryCalculateCheckDigits_ShouldReturnFalse_WhenInputIsNull()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigits(null!, out var first, out var second).Should().BeFalse();
      first.Should().Be('\0');
      second.Should().Be('\0');
   }

   [Fact]
   public void Iso7064Mod1271_36Algorithm_TryCalculateCheckDigits_ShouldReturnFalse_WhenInputIsEmpty()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigits(String.Empty, out var first, out var second).Should().BeFalse();
      first.Should().Be('\0');
      second.Should().Be('\0');
   }

   [Theory]
   [InlineData("0", 'Z', 'C')]
   [InlineData("1", 'Y', 'N')]
   [InlineData("2", 'X', 'Y')]
   [InlineData("3", 'X', '9')]
   [InlineData("4", 'W', 'K')]
   [InlineData("5", 'V', 'V')]
   [InlineData("6", 'V', '6')]
   [InlineData("7", 'U', 'H')]
   [InlineData("8", 'T', 'S')]
   [InlineData("9", 'T', '3')]
   [InlineData("A", 'S', 'E')]
   [InlineData("B", 'R', 'P')]
   [InlineData("C", 'R', '0')]
   [InlineData("D", 'Q', 'B')]
   [InlineData("E", 'P', 'M')]
   [InlineData("F", 'O', 'X')]
   [InlineData("G", 'O', '8')]
   [InlineData("H", 'N', 'J')]
   [InlineData("I", 'M', 'U')]
   [InlineData("J", 'M', '5')]
   [InlineData("K", 'L', 'G')]
   [InlineData("L", 'K', 'R')]
   [InlineData("M", 'K', '2')]
   [InlineData("N", 'J', 'D')]
   [InlineData("O", 'I', 'O')]
   [InlineData("P", 'H', 'Z')]
   [InlineData("Q", 'H', 'A')]
   [InlineData("R", 'G', 'L')]
   [InlineData("S", 'F', 'W')]
   [InlineData("T", 'F', '7')]
   [InlineData("U", 'E', 'I')]
   [InlineData("V", 'D', 'T')]
   [InlineData("W", 'D', '4')]
   [InlineData("X", 'C', 'F')]
   [InlineData("Y", 'B', 'Q')]
   [InlineData("Z", 'B', '1')]
   public void Iso7064Mod1271_36Algorithm_TryCalculateCheckDigits_ShouldCorrectlyMapCharacterValues(
      String value,
      Char expectedFirst,
      Char expectedSecond)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigits(value, out var first, out var second).Should().BeTrue();
      first.Should().Be(expectedFirst);
      second.Should().Be(expectedSecond);
   }

   [Theory]
   [InlineData("ISO79", '3', 'W')]           // Example from ISO/IEC 7064 specification
   [InlineData("XS868977863229", 'A', 'U')]  // Example Nigerian Virtual National Identification Number https://nin.mtn.ng/
   [InlineData("123456789ABCDE", 'H', '2')]
   [InlineData("AAAAA55555ZZZZZ", '0', 'S')]
   [InlineData("IS201309FR7531Q", 'A', 'X')]
   [InlineData("AEIOU1592430QWERTY", '0', 'Z')]
   [InlineData("ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ", '6', 'X')]
   public void Iso7064Mod1271_36AlgorithmAlgorithm_TryCalculateCheckDigits_ShouldCalculateExpectedCheckDigit(
      String value,
      Char expectedFirst,
      Char expectedSecond)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigits(value, out var first, out var second).Should().BeTrue();
      first.Should().Be(expectedFirst);
      second.Should().Be(expectedSecond);
   }

   [Theory]
   [InlineData("123!56")]
   [InlineData("123^56")]
   [InlineData("123=56X")]
   public void Iso7064Mod1271_36Algorithm_TryCalculateCheckDigits_ShouldReturnFalse_WhenInputContainsInvalidCharacter(String value)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigits(value, out var first, out var second).Should().BeFalse();
      first.Should().Be('\0');
      second.Should().Be('\0');
   }

   [Theory]
   [InlineData("U7Y")]
   [InlineData("U7Y8SX")]
   [InlineData("U7Y8SXRC0")]
   [InlineData("U7Y8SXRC0O3S")]
   [InlineData("U7Y8SXRC0O3SC4I")]
   [InlineData("U7Y8SXRC0O3SC4IHYQ")]
   [InlineData("U7Y8SXRC0O3SC4IHYQF4M")]
   public void Iso7064Mod1271_36Algorithm_TryCalculateCheckDigits_ShouldReturnTrue_ForBenchmarkValues(String value)
      => _sut.TryCalculateCheckDigits(value, out _, out _).Should().BeTrue();

   #endregion

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso7064Mod1271_36Algorithm_Validate_ShouldReturnFalse_WhenInputIsNull()
      => _sut.Validate(null!).Should().BeFalse();

   [Fact]
   public void Iso7064Mod1271_36Algorithm_Validate_ShouldReturnFalse_WhenInputIsEmpty()
      => _sut.Validate(String.Empty).Should().BeFalse();

   [Theory]
   [InlineData("0")]       // Length 1 will fail with index out of range exception if length not checked
   [InlineData("1")]
   [InlineData("00")]
   [InlineData("01")]      // 01 will pass validation if length is not checked
   public void Iso7064Mod1271_36Algorithm_Validate_ShouldReturnFalse_WhenInputIsLessThanThreeCharactersInLength(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Theory]
   [InlineData("0ZC")]
   [InlineData("1YN")]
   [InlineData("2XY")]
   [InlineData("3X9")]
   [InlineData("4WK")]
   [InlineData("5VV")]
   [InlineData("6V6")]
   [InlineData("7UH")]
   [InlineData("8TS")]
   [InlineData("9T3")]
   [InlineData("ASE")]
   [InlineData("BRP")]
   [InlineData("CR0")]
   [InlineData("DQB")]
   [InlineData("EPM")]
   [InlineData("FOX")]
   [InlineData("GO8")]
   [InlineData("HNJ")]
   [InlineData("IMU")]
   [InlineData("JM5")]
   [InlineData("KLG")]
   [InlineData("LKR")]
   [InlineData("MK2")]
   [InlineData("NJD")]
   [InlineData("OIO")]
   [InlineData("PHZ")]
   [InlineData("QHA")]
   [InlineData("RGL")]
   [InlineData("SFW")]
   [InlineData("TF7")]
   [InlineData("UEI")]
   [InlineData("VDT")]
   [InlineData("WD4")]
   [InlineData("XCF")]
   [InlineData("YBQ")]
   [InlineData("ZB1")]
   public void Iso7064Mod1271_36Algorithm_Validate_ShouldCorrectlyMapCharacterValues(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("ISO793W")]                // Example from ISO/IEC 7064 specification
   [InlineData("XS868977863229AU")]       // Example Nigerian Virtual National Identification Number https://nin.mtn.ng/
   [InlineData("123456789ABCDEH2")]
   [InlineData("AAAAA55555ZZZZZ0S")]
   [InlineData("IS201309FR7531QAX")]
   [InlineData("AEIOU1592430QWERTY0Z")]
   [InlineData("ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ6X")]
   public void Iso7064Mod1271_36Algorithm_Validate_ShouldReturnTrue_WhenValueContainsValidCheckDigits(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("XS868977864229AU")]    // XS868977863229AU with single digit transcription error 3 -> 4
   [InlineData("XS768977863229AU")]    // XS868977863229AU with single digit transcription error 8 -> 7
   [InlineData("XT868977863229AU")]    // XS868977863229AU with single char transcription error S -> T
   [InlineData("XS869877863229AU")]    // XS868977863229AU with two digit transposition error 89 -> 98 
   [InlineData("123456789BACDEH2")]    // 123456789ABCDEH2 with two char transposition error AB -> BA 
   [InlineData("XS868966863229AU")]    // XS868977863229AU with two digit twin error 77 -> 66
   [InlineData("XS868977863339AU")]    // XS868977863229AU two digit twin error 22 -> 33
   [InlineData("123456987ABCDEH2")]    // 123456789ABCDEH2 with jump transposition error 789 -> 987
   [InlineData("123456789ADCBEH2")]    // 123456789ABCDEH2 with jump transposition error BCD -> DCB
   [InlineData("UXS868977863229A")]    // XS868977863229AU with circular shift error
   [InlineData("S868977863229AUX")]    // XS868977863229AU with circular shift error
   public void Iso7064Mod1271_36Algorithm_Validate_ShouldReturnFalse_WhenInputContainsDetectableError(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Theory]
   [InlineData("123!56X")]
   [InlineData("123^56X")]
   [InlineData("123=56X")]
   [InlineData("123a56X")]
   public void Iso7064Mod1271_36Algorithm_Validate_ShouldReturnFalse_WhenValueContainsInvalidCharacter(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Theory]
   [InlineData("1234!0")]
   [InlineData("1234^0")]
   [InlineData("1234=0")]
   [InlineData("1234a0")]
   public void Iso7064Mod1271_36Algorithm_Validate_ShouldReturnFalse_WhenValueContainsInvalidFirstCheckCharacter(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Theory]
   [InlineData("12356X!")]
   [InlineData("12356X^")]
   [InlineData("12356X=")]
   [InlineData("12356Xa")]
   public void Iso7064Mod1271_36Algorithm_Validate_ShouldReturnFalse_WhenValueContainsInvalidSecondCheckCharacter(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Theory]
   [InlineData("U7YM0")]
   [InlineData("U7Y8SXOR")]
   [InlineData("U7Y8SXRC0FI")]
   [InlineData("U7Y8SXRC0O3SX4")]
   [InlineData("U7Y8SXRC0O3SC4I9D")]
   [InlineData("U7Y8SXRC0O3SC4IHYQYI")]
   [InlineData("U7Y8SXRC0O3SC4IHYQF4M44")]
   public void Iso7064Mod1271_36Algorithm_Validate_ShouldReturnTrue_ForBenchmarkValues(String value)
      => _sut.Validate(value).Should().BeTrue();

   #endregion

   #region Validate (ICheckDigitMask Overload) Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso7064Mod1271_36Algorithm_ValidateMasked_ShouldThrowArgumentNullException_WhenMaskIsNull()
      => _sut
         .Invoking(x => x.Validate("07945", null!))
         .Should()
         .ThrowExactly<ArgumentNullException>()
         .WithParameterName("mask")
         .WithMessage(Resources.NullMaskMessage + "*");

   [Fact]
   public void Iso7064Mod1271_36Algorithm_ValidateMasked_ShouldReturnFalse_WhenInputIsNull()
      => _sut.Validate(null!, _acceptAllMask).Should().BeFalse();

   [Fact]
   public void Iso7064Mod1271_36Algorithm_ValidateMasked_ShouldReturnFalse_WhenInputIsEmpty()
      => _sut.Validate(String.Empty, _acceptAllMask).Should().BeFalse();

   [Theory]
   [InlineData("0")]       // Length 1 will fail with index out of range exception if length not checked
   [InlineData("1")]
   [InlineData("00")]
   [InlineData("01")]      // 01 will pass validation if length is not checked
   public void Iso7064Mod1271_36Algorithm_ValidateMasked_ShouldReturnFalse_WhenInsufficientUnmaskedCharactersToCalculateCheckDigit(String value)
      => _sut.Validate(value, _acceptAllMask).Should().BeFalse();

   [Fact]
   public void Iso7064Mod1271_36Algorithm_ValidateMasked_ShouldReturnFalse_WhenAllNonCheckDigitCharactersAreMaskedOut()
      => _sut.Validate("ISO793W", _rejectAllMask).Should().BeFalse();

   [Theory]
   [InlineData("0ZC")]
   [InlineData("1YN")]
   [InlineData("2XY")]
   [InlineData("3X9")]
   [InlineData("4WK")]
   [InlineData("5VV")]
   [InlineData("6V6")]
   [InlineData("7UH")]
   [InlineData("8TS")]
   [InlineData("9T3")]
   [InlineData("ASE")]
   [InlineData("BRP")]
   [InlineData("CR0")]
   [InlineData("DQB")]
   [InlineData("EPM")]
   [InlineData("FOX")]
   [InlineData("GO8")]
   [InlineData("HNJ")]
   [InlineData("IMU")]
   [InlineData("JM5")]
   [InlineData("KLG")]
   [InlineData("LKR")]
   [InlineData("MK2")]
   [InlineData("NJD")]
   [InlineData("OIO")]
   [InlineData("PHZ")]
   [InlineData("QHA")]
   [InlineData("RGL")]
   [InlineData("SFW")]
   [InlineData("TF7")]
   [InlineData("UEI")]
   [InlineData("VDT")]
   [InlineData("WD4")]
   [InlineData("XCF")]
   [InlineData("YBQ")]
   [InlineData("ZB1")]
   public void Iso7064Mod1271_36Algorithm_ValidateMasked_ShouldCorrectlyMapCharacterValues(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeTrue();

   [Theory]                                  // Note possible trailing group of 4 characters to avoid splitting check characters with space
   [InlineData("ISO 793W")]                  // Example from ISO/IEC 7064 specification
   [InlineData("XS8 689 778 632 29AU")]      // Example Nigerian Virtual National Identification Number https://nin.mtn.ng/
   [InlineData("123 456 789 ABC DEH2")]
   [InlineData("AAA AA5 555 5ZZ ZZZ 0S")]
   [InlineData("IS2 013 09F R75 31Q AX")]
   [InlineData("AEI OU1 592 430 QWE RTY 0Z")]
   [InlineData("ZZZ ZZZ ZZZ ZZZ ZZZ ZZZ ZZZ ZZZ ZZZ ZZZ ZZZ ZZZ 6X")]
   public void Iso7064Mod1271_36Algorithm_ValidateMasked_ShouldReturnTrue_WhenValueContainsValidCheckDigits(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeTrue();

   [Theory]
   [InlineData("ISO793W")]                // Example from ISO/IEC 7064 specification
   [InlineData("XS868977863229AU")]       // Example Nigerian Virtual National Identification Number https://nin.mtn.ng/
   [InlineData("123456789ABCDEH2")]
   [InlineData("AAAAA55555ZZZZZ0S")]
   [InlineData("IS201309FR7531QAX")]
   [InlineData("AEIOU1592430QWERTY0Z")]
   [InlineData("ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ6X")]
   public void Iso7064Mod1271_36Algorithm_ValidateMasked_ShouldReturnTrue_WhenValueContainsValidCheckDigitsAndMaskAcceptsAllCharacters(String value)
      => _sut.Validate(value, _acceptAllMask).Should().BeTrue();

   [Theory]                               // Note trailing group of 4 characters to avoid splitting check characters with space
   [InlineData("XS8 689 778 642 29AU")]   // XS868977863229AU with single digit transcription error 3 -> 4
   [InlineData("XS7 689 778 632 29AU")]   // XS868977863229AU with single digit transcription error 8 -> 7
   [InlineData("XT8 689 778 632 29AU")]   // XS868977863229AU with single char transcription error S -> T
   [InlineData("XS8 698 778 632 29AU")]   // XS868977863229AU with two digit transposition error 89 -> 98 
   [InlineData("123 456 789 BAC DEH2")]   // 123456789ABCDEH2 with two char transposition error AB -> BA 
   [InlineData("XS8 689 668 632 29AU")]   // XS868977863229AU with two digit twin error 77 -> 66
   [InlineData("XS8 689 778 633 39AU")]   // XS868977863229AU two digit twin error 22 -> 33
   [InlineData("123 456 987 ABC DEH2")]   // 123456789ABCDEH2 with jump transposition error 789 -> 987
   [InlineData("123 456 789 ADC BEH2")]   // 123456789ABCDEH2 with jump transposition error BCD -> DCB
   [InlineData("UXS 868 977 863 229A")]   // XS868977863229AU with circular shift error
   [InlineData("S86 897 786 322 9AUX")]   // XS868977863229AU with circular shift error
   public void Iso7064Mod1271_36Algorithm_ValidateMasked_ShouldReturnFalse_WhenInputContainsDetectableError(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeFalse();

   [Theory]                               // Note trailing group of 4 characters to avoid splitting check characters with space
   [InlineData("123 !56X")]
   [InlineData("123 ^56X")]
   [InlineData("123 =56X")]
   [InlineData("123 a56X")]
   public void Iso7064Mod1271_36Algorithm_ValidateMasked_ShouldReturnFalse_WhenValueContainsInvalidCharacter(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeFalse();

   [Theory]                               // Note trailing group of 4 characters to avoid splitting check characters with space
   [InlineData("123 4!0")]
   [InlineData("123 4^0")]
   [InlineData("123 4=0")]
   [InlineData("123 4a0")]
   public void Iso7064Mod1271_36Algorithm_ValidateMasked_ShouldReturnFalse_WhenValueContainsInvalidFirstCheckCharacter(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeFalse();

   [Theory]                               // Note trailing group of 4 characters to avoid splitting check characters with space
   [InlineData("123 56X!")]
   [InlineData("123 56X^")]
   [InlineData("123 56X=")]
   [InlineData("123 56Xa")]
   public void Iso7064Mod1271_36Algorithm_ValidateMasked_ShouldReturnFalse_WhenValueContainsInvalidSecondCheckCharacter(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeFalse();

   [Theory]
   [InlineData("U7Y M0")]
   [InlineData("U7Y 8SX OR")]
   [InlineData("U7Y 8SX RC0 FI")]
   [InlineData("U7Y 8SX RC0 O3S X4")]
   [InlineData("U7Y 8SX RC0 O3S C4I 9D")]
   [InlineData("U7Y 8SX RC0 O3S C4I HYQ YI")]
   [InlineData("U7Y 8SX RC0 O3S C4I HYQ F4M 44")]
   public void Iso7064Mod1271_36Algorithm_ValidateMasked_ShouldReturnTrue_ForBenchmarkValues(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeTrue();

   #endregion
}
