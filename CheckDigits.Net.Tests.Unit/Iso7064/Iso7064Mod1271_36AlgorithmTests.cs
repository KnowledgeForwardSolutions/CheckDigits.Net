namespace CheckDigits.Net.Tests.Unit.Iso7064;

public class Iso7064Mod1271_36AlgorithmTests
{
   private readonly Iso7064Mod1271_36Algorithm _sut = new();

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

   #region TryCalculateCheckDigit Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso7064Mod1271_36Algorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsNull()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(null!, out var first, out var second).Should().BeFalse();
      first.Should().Be('\0');
      second.Should().Be('\0');
   }

   [Fact]
   public void Iso7064Mod1271_36Algorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsEmpty()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(null!, out var first, out var second).Should().BeFalse();
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
   public void Iso7064Mod1271_36Algorithm_TryCalculateCheckDigit_ShouldCorrectlyMapCharacterValues(
      String value,
      Char expectedFirst,
      Char expectedSecond)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var first, out var second).Should().BeTrue();
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
   public void Iso7064Mod1271_36AlgorithmAlgorithm_TryCalculateCheckDigit_ShouldCalculateExpectedCheckDigit(
      String value,
      Char expectedFirst,
      Char expectedSecond)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var first, out var second).Should().BeTrue();
      first.Should().Be(expectedFirst);
      second.Should().Be(expectedSecond);
   }

   [Theory]
   [InlineData("123!56")]
   [InlineData("123^56")]
   [InlineData("123=56X")]
   public void Iso7064Mod37_2Algorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputContainsInvalidCharacter(String value)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var first, out var second).Should().BeFalse();
      first.Should().Be('\0');
      second.Should().Be('\0');
   }

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
   public void Iso7064Mod1271_36Algorithm_Validate_ShouldReturnFalse_WhenInputIsLessThanTwoCharactersInLength(String value)
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
   public void Iso7064Mod1271_36Algorithm_Validate_ShouldReturnTrue_WhenValueContainsValidCheckDigit(String value)
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
   public void Iso7064Mod1271_36Algorithm_Validate_ShouldReturnFalse_WhenValueContainsInvalidCharacter(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Fact]
   public void Iso7064Mod1271_36Algorithm_Validate_ShouldReturnFalse_WhenValueContainsInvalidFirstCheckCharacter()
      => _sut.Validate("1234#0").Should().BeFalse();

   [Theory]
   [InlineData("12356X!")]
   [InlineData("12356X^")]
   [InlineData("12356X=")]
   public void Iso7064Mod1271_36Algorithm_Validate_ShouldReturnFalse_WhenValueContainsInvalidSecondCheckCharacter(String value)
      => _sut.Validate(value).Should().BeFalse();

   #endregion
}
