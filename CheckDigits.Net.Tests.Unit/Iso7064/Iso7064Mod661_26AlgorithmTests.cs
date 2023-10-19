namespace CheckDigits.Net.Tests.Unit.Iso7064;

public class Iso7064Mod661_26AlgorithmTests
{
   private readonly Iso7064Mod661_26Algorithm _sut = new();

   #region AlgorithmDescription Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso7064Mod661_26Algorithm_AlgorithmDescription_ShouldReturnExpectedValue()
      => _sut.AlgorithmDescription.Should().Be(Resources.Iso7064Mod661_26AlgorithmDescription);

   #endregion

   #region AlgorithmName Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso7064Mod661_26Algorithm_AlgorithmName_ShouldReturnExpectedValue()
      => _sut.AlgorithmName.Should().Be(Resources.Iso7064Mod661_26AlgorithmName);

   #endregion

   #region TryCalculateCheckDigit Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso7064Mod661_26Algorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsNull()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(null!, out var first, out var second).Should().BeFalse();
      first.Should().Be('\0');
      second.Should().Be('\0');
   }

   [Fact]
   public void Iso7064Mod661_26Algorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsEmpty()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(null!, out var first, out var second).Should().BeFalse();
      first.Should().Be('\0');
      second.Should().Be('\0');
   }

   [Theory]
   [InlineData("A", 'Z', 'M')]
   [InlineData("B", 'Y', 'X')]
   [InlineData("C", 'Y', 'I')]
   [InlineData("D", 'X', 'T')]
   [InlineData("E", 'X', 'E')]
   [InlineData("F", 'W', 'P')]
   [InlineData("G", 'W', 'A')]
   [InlineData("H", 'V', 'L')]
   [InlineData("I", 'U', 'W')]
   [InlineData("J", 'U', 'H')]
   [InlineData("K", 'T', 'S')]
   [InlineData("L", 'T', 'D')]
   [InlineData("M", 'S', 'O')]
   [InlineData("N", 'R', 'Z')]
   [InlineData("O", 'R', 'K')]
   [InlineData("P", 'Q', 'V')]
   [InlineData("Q", 'Q', 'G')]
   [InlineData("R", 'P', 'R')]
   [InlineData("S", 'P', 'C')]
   [InlineData("T", 'O', 'N')]
   [InlineData("U", 'N', 'Y')]
   [InlineData("V", 'N', 'J')]
   [InlineData("W", 'M', 'U')]
   [InlineData("X", 'M', 'F')]
   [InlineData("Y", 'L', 'Q')]
   [InlineData("Z", 'L', 'B')]
   public void Iso7064Mod661_26Algorithm_TryCalculateCheckDigit_ShouldCorrectlyMapCharacterValues(
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
   [InlineData("ISOHJ", 'T', 'C')]
   [InlineData("ABCDEFGHIJKLMN", 'J', 'F')]
   [InlineData("ASDFQWERTYLKJH", 'L', 'R')]
   [InlineData("AAAEEEIIIOOOUUUBCDEF", 'J', 'Y')]
   [InlineData("ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ", 'N', 'S')]
   public void Iso7064Mod661_26AlgorithmAlgorithm_TryCalculateCheckDigit_ShouldCalculateExpectedCheckDigit(
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
   public void Iso7064Mod661_26Algorithm_Validate_ShouldReturnFalse_WhenInputIsNull()
      => _sut.Validate(null!).Should().BeFalse();

   [Fact]
   public void Iso7064Mod661_26Algorithm_Validate_ShouldReturnFalse_WhenInputIsEmpty()
      => _sut.Validate(String.Empty).Should().BeFalse();

   [Theory]
   [InlineData("A")]
   [InlineData("B")]
   [InlineData("AA")]
   [InlineData("AB")]
   public void Iso7064Mod661_26Algorithm_Validate_ShouldReturnFalse_WhenInputIsLessThanTwoCharactersInLength(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Theory]
   [InlineData("AZM")]
   [InlineData("BYX")]
   [InlineData("CYI")]
   [InlineData("DXT")]
   [InlineData("EXE")]
   [InlineData("FWP")]
   [InlineData("GWA")]
   [InlineData("HVL")]
   [InlineData("IUW")]
   [InlineData("JUH")]
   [InlineData("KTS")]
   [InlineData("LTD")]
   [InlineData("MSO")]
   [InlineData("NRZ")]
   [InlineData("ORK")]
   [InlineData("PQV")]
   [InlineData("QQG")]
   [InlineData("RPR")]
   [InlineData("SPC")]
   [InlineData("TON")]
   [InlineData("UNY")]
   [InlineData("VNJ")]
   [InlineData("WMU")]
   [InlineData("XMF")]
   [InlineData("YLQ")]
   [InlineData("ZLB")]
   public void Iso7064Mod661_26Algorithm_Validate_ShouldCorrectlyMapCharacterValues(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("ISOHJTC")]                
   [InlineData("ABCDEFGHIJKLMNJF")]
   [InlineData("ASDFQWERTYLKJHLR")]
   [InlineData("AAAEEEIIIOOOUUUBCDEFJY")]
   [InlineData("ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZNS")]
   public void Iso7064Mod661_26Algorithm_Validate_ShouldReturnTrue_WhenValueContainsValidCheckDigit(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("ABEDEFGHIJKLMNJF")]       // ABCDEFGHIJKLMNJF with single char transcription error C -> E
   [InlineData("ABCDEFGHLJKLMNJF")]       // ABCDEFGHIJKLMNJF with single char transcription error I -> L
   [InlineData("ASBAQWERTYLKJHLR")]       // ASDFQWERTYLKJHLR with two char transposition error DF -> BA 
   [InlineData("AAAEEEIIIOPPUUUBCDEFJY")] // AAAEEEIIIOOOUUUBCDEFJY with two char twin error OO -> PP
   [InlineData("ABCFEDGHIJKLMNJF")]       // ABCDEFGHIJKLMNJF with jump transposition error DEF -> FED
   [InlineData("ABCDEFGHKJILMNJF")]       // ABCDEFGHIJKLMNJF with jump transposition error IJK -> KJI
   [InlineData("SDFQWERTYLKJHLRA")]       // ASDFQWERTYLKJHLR with circular shift error
   [InlineData("RASDFQWERTYLKJHL")]       // ASDFQWERTYLKJHLR with circular shift error
   public void Iso7064Mod661_26Algorithm_Validate_ShouldReturnFalse_WhenInputContainsDetectableError(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Theory]
   [InlineData("123!56X")]
   [InlineData("123^56X")]
   public void Iso7064Mod661_26Algorithm_Validate_ShouldReturnFalse_WhenValueContainsInvalidCharacter(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Fact]
   public void Iso7064Mod661_26Algorithm_Validate_ShouldReturnFalse_WhenValueContainsInvalidFirstCheckCharacter()
      => _sut.Validate("1234#0").Should().BeFalse();

   [Fact]
   public void Iso7064Mod661_26Algorithm_Validate_ShouldReturnFalse_WhenValueContainsInvalidSecondCheckCharacter()
      => _sut.Validate("1234A#").Should().BeFalse();

   #endregion
}
