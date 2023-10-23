namespace CheckDigits.Net.Tests.Unit.Iso7064;

public class Iso7064Mod37_36AlgorithmTests
{
   private readonly Iso7064Mod37_36Algorithm _sut = new();

   #region AlgorithmDescription Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso7064Mod37_36Algorithm_AlgorithmDescription_ShouldReturnExpectedValue()
      => _sut.AlgorithmDescription.Should().Be(Resources.Iso7064Mod37_36AlgorithmDescription);

   #endregion

   #region AlgorithmName Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso7064Mod37_36Algorithm_AlgorithmName_ShouldReturnExpectedValue()
      => _sut.AlgorithmName.Should().Be(Resources.Iso7064Mod37_36AlgorithmName);

   #endregion

   #region TryCalculateCheckDigit Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso7064Mod37_36Algorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsNull()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(null!, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Fact]
   public void Iso7064Mod37_36Algorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsEmpty()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(String.Empty, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Theory]
   [InlineData("0", '2')]
   [InlineData("1", 'Z')]
   [InlineData("2", 'X')]
   [InlineData("3", 'V')]
   [InlineData("4", 'T')]
   [InlineData("5", 'R')]
   [InlineData("6", 'P')]
   [InlineData("7", 'N')]
   [InlineData("8", 'L')]
   [InlineData("9", 'J')]
   [InlineData("A", 'H')]
   [InlineData("B", 'F')]
   [InlineData("C", 'D')]
   [InlineData("D", 'B')]
   [InlineData("E", '9')]
   [InlineData("F", '7')]
   [InlineData("G", '5')]
   [InlineData("H", '3')]
   [InlineData("I", '1')]
   [InlineData("J", '0')]
   [InlineData("K", 'Y')]
   [InlineData("L", 'W')]
   [InlineData("M", 'U')]
   [InlineData("N", 'S')]
   [InlineData("O", 'Q')]
   [InlineData("P", 'O')]
   [InlineData("Q", 'M')]
   [InlineData("R", 'K')]
   [InlineData("S", 'I')]
   [InlineData("T", 'G')]
   [InlineData("U", 'E')]
   [InlineData("V", 'C')]
   [InlineData("W", 'A')]
   [InlineData("X", '8')]
   [InlineData("Y", '6')]
   [InlineData("Z", '4')]
   public void Iso7064Mod37_36Algorithm_TryCalculateCheckDigit_ShouldCorrectlyMapCharacterValues(
      String value,
      Char expectedCheckDigit)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Theory]
   [InlineData("AEIOU", 'U')]
   [InlineData("QWERTYDVORAK", '1')]
   [InlineData("A1B2C3D4E5F6G7H8I9J0K", 'I')]
   [InlineData("THISISATESTTHISISONLYATEST", 'E')]
   [InlineData("1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ", 'T')]
   public void Iso7064Mod37_36AlgorithmAlgorithm_TryCalculateCheckDigit_ShouldCalculateExpectedCheckDigit(
      String value,
      Char expectedCheckDigit)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Theory]
   [InlineData("ABC!EFX")]
   [InlineData("ABC^EFX")]
   [InlineData("ABC=EFX")]
   public void Iso7064Mod37_36Algorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputContainsInvalidCharacter(String value)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   #endregion

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso7064Mod37_36Algorithm_Validate_ShouldReturnFalse_WhenInputIsNull()
      => _sut.Validate(null!).Should().BeFalse();

   [Fact]
   public void Iso7064Mod37_36Algorithm_Validate_ShouldReturnFalse_WhenInputIsEmpty()
      => _sut.Validate(String.Empty).Should().BeFalse();

   [Theory]
   [InlineData("0")]
   [InlineData("1")]
   [InlineData("A")]
   public void Iso7064Mod37_36Algorithm_Validate_ShouldReturnFalse_WhenInputIsOneCharacterInLength(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Theory]
   [InlineData("02")]
   [InlineData("1Z")]
   [InlineData("2X")]
   [InlineData("3V")]
   [InlineData("4T")]
   [InlineData("5R")]
   [InlineData("6P")]
   [InlineData("7N")]
   [InlineData("8L")]
   [InlineData("9J")]
   [InlineData("AH")]
   [InlineData("BF")]
   [InlineData("CD")]
   [InlineData("DB")]
   [InlineData("E9")]
   [InlineData("F7")]
   [InlineData("G5")]
   [InlineData("H3")]
   [InlineData("I1")]
   [InlineData("J0")]
   [InlineData("KY")]
   [InlineData("LW")]
   [InlineData("MU")]
   [InlineData("NS")]
   [InlineData("OQ")]
   [InlineData("PO")]
   [InlineData("QM")]
   [InlineData("RK")]
   [InlineData("SI")]
   [InlineData("TG")]
   [InlineData("UE")]
   [InlineData("VC")]
   [InlineData("WA")]
   [InlineData("X8")]
   [InlineData("Y6")]
   [InlineData("Z4")]
   public void Iso7064Mod37_36Algorithm_Validate_ShouldCorrectlyMapCharacterValues(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("AEIOUU")]
   [InlineData("QWERTYDVORAK1")]
   [InlineData("A1B2C3D4E5F6G7H8I9J0KI")]
   [InlineData("THISISATESTTHISISONLYATESTE")]
   [InlineData("1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZT")]
   public void Iso7064Mod37_36Algorithm_Validate_ShouldReturnTrue_WhenValueContainsValidCheckDigit(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("ACIOUU")]                       // AEIOUU with single char transcription error E -> C
   [InlineData("QWERTUDVORAK1")]                // QWERTYDVORAK1 with single char transcription error Y -> U
   [InlineData("A1B2C3D4E5F67GH8I9J0KI")]       // A1B2C3D4E5F6G7H8I9J0KI with two char transposition error G7 -> 7G 
   [InlineData("THISISATESUUHISISONLYATESTE")]  // THISISATESTTHISISONLYATESTE with two char twin error TT -> UU
   [InlineData("A1B2C3E4D5F6G7H8I9J0KI")]       // A1B2C3D4E5F6G7H8I9J0KI with jump transposition error D4E -> E4D
   [InlineData("QWERDYTVORAK1")]                // QWERTYDVORAK1 with jump transposition error TYD -> DYT
   [InlineData("UAEIOU")]                       // AEIOUU with circular shift error
   [InlineData("EIOUUA")]                       // AEIOUU with circular shift error
   public void Iso7064Mod37_36Algorithm_Validate_ShouldReturnFalse_WhenInputContainsDetectableError(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Fact]
   public void Iso7064Mod37_36Algorithm_Validate_ShouldReturnTrue_WhenCheckDigitIsCalculatesAsZero()
      => _sut.Validate("J0").Should().BeTrue();

   [Theory]
   [InlineData("ABC!EFX")]
   [InlineData("ABC^EFX")]
   [InlineData("ABC=EFX")]
   public void Iso7064Mod37_36Algorithm_Validate_ShouldReturnFalse_WhenValueContainsInvalidCharacter(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Theory]
   [InlineData("ABCDE!")]
   [InlineData("ABCDE^")]
   [InlineData("ABCDE=")]
   public void Iso7064Mod37_36Algorithm_Validate_ShouldReturnFalse_WhenValueContainsInvalidCheckDigitCharacter(String value)
      => _sut.Validate(value).Should().BeFalse();

   #endregion
}
