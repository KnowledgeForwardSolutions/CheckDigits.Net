namespace CheckDigits.Net.Tests.Unit.ValueSpecificAlgorithms;

public class Iso6346AlgorithmTests
{
   private readonly Iso6346Algorithm _sut = new();

   #region AlgorithmDescription Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso6346Algorithm_AlgorithmDescription_ShouldReturnExpectedValue()
      => _sut.AlgorithmDescription.Should().Be(Resources.Iso6346AlgorithmDescription);

   #endregion

   #region AlgorithmName Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso6346Algorithm_AlgorithmName_ShouldReturnExpectedValue()
      => _sut.AlgorithmName.Should().Be(Resources.Iso6346AlgorithmName);

   #endregion

   #region TryCalculateCheckDigit Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso6346Algorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsNull()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(null!, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Fact]
   public void Iso6346Algorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsEmpty()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(string.Empty, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Fact]
   public void Iso6346Algorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputHasLengthLessThanTenCharacters()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit("123456789", out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Fact]
   public void Iso6346Algorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputHasLengthGreaterThanTenCharacters()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit("12345678901", out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Theory]
   [InlineData("1000000000", '1')]      // Note: Owner prefix code and category char should be alphabetic - 0/1 used for this test
   [InlineData("0100000000", '2')]
   [InlineData("0010000000", '4')]
   [InlineData("0001000000", '8')]
   [InlineData("0000100000", '5')]
   [InlineData("0000010000", '0')]
   [InlineData("0000001000", '9')]
   [InlineData("0000000100", '7')]
   [InlineData("0000000010", '3')]
   [InlineData("0000000001", '6')]
   public void Iso6346Algorithm_TryCalculateCheckDigit_ShouldCorrectlyWeightByPosition(
      string value,
      char expectedCheckDigit)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Theory]
   [InlineData("0000000000", '0')]      // Note: Owner prefix code and category char should be alphabetic - 0/1 used for this test
   [InlineData("1000000000", '1')]
   [InlineData("2000000000", '2')]
   [InlineData("3000000000", '3')]
   [InlineData("4000000000", '4')]
   [InlineData("5000000000", '5')]
   [InlineData("6000000000", '6')]
   [InlineData("7000000000", '7')]
   [InlineData("8000000000", '8')]
   [InlineData("9000000000", '9')]
   [InlineData("A000000000", '0')]
   [InlineData("B000000000", '1')]
   [InlineData("C000000000", '2')]
   [InlineData("D000000000", '3')]
   [InlineData("E000000000", '4')]
   [InlineData("F000000000", '5')]
   [InlineData("G000000000", '6')]
   [InlineData("H000000000", '7')]
   [InlineData("I000000000", '8')]
   [InlineData("J000000000", '9')]
   [InlineData("K000000000", '0')]
   [InlineData("L000000000", '1')]
   [InlineData("M000000000", '2')]
   [InlineData("N000000000", '3')]
   [InlineData("O000000000", '4')]
   [InlineData("P000000000", '5')]
   [InlineData("Q000000000", '6')]
   [InlineData("R000000000", '7')]
   [InlineData("S000000000", '8')]
   [InlineData("T000000000", '9')]
   [InlineData("U000000000", '0')]
   [InlineData("V000000000", '1')]
   [InlineData("W000000000", '2')]
   [InlineData("X000000000", '3')]
   [InlineData("Y000000000", '4')]
   [InlineData("Z000000000", '5')]
   public void Iso6346Algorithm_TryCalculateCheckDigit_ShouldCorrectlyMapCharacter(
      string value,
      char expectedCheckDigit)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Theory]
   [InlineData("CSQU305438", '3')]      // Worked example from Wikipedia
   [InlineData("TOLU473478", '7')]      // Photo from Wikipedia
   [InlineData("HJCU828198", '8')]      // Photo from Wikipedia
   [InlineData("BICU123456", '5')]      // Example from https://www.bic-code.org/identification-number/
   [InlineData("MSKU907032", '3')]      // Example from https://www.letterofcredit.biz/index.php/2019/11/04/what-is-a-container-number-explanations-with-examples/
   [InlineData("MEDU870768", '8')]      // "
   [InlineData("HLAU123456", '7')]      // Calculated from https://www.bic-code.org/check-digit-calculator/
   public void Iso6346Algorithm_TryCalculateCheckDigit_ShouldCalculateExpectedCheckDigit(
      string value,
      char expectedCheckDigit)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Theory]
   [InlineData("CSQU3!5438")]
   [InlineData("CSQU3.5438")]
   [InlineData("CSQU3:5438")]
   [InlineData("CSQU3^5438")]
   public void Iso6346Algorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputContainsInvalidCharacter(string value)
   {
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   #endregion

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso6346Algorithm_Validate_ShouldReturnFalse_WhenInputIsNull()
      => _sut.Validate(null!).Should().BeFalse();

   [Fact]
   public void Iso6346Algorithm_Validate_ShouldReturnFalse_WhenInputIsEmpty()
      => _sut.Validate(String.Empty).Should().BeFalse();

   [Fact]
   public void Iso6346Algorithm_Validate_ShouldReturnFalse_WhenInputHasLengthLessThanElevenCharacters()
      => _sut.Validate("AAAU123456").Should().BeFalse();

   [Fact]
   public void Iso6346Algorithm_Validate_ShouldReturnFalse_WhenInputHasLengthGreaterThanElevenCharacters()
      => _sut.Validate("AAAU12345678").Should().BeFalse();
   [Theory]

   [InlineData("10000000001")]      // Note: Owner prefix code and category char should be alphabetic - 0/1 used for this test
   [InlineData("01000000002")]
   [InlineData("00100000004")]
   [InlineData("00010000008")]
   [InlineData("00001000005")]
   [InlineData("00000100000")]
   [InlineData("00000010009")]
   [InlineData("00000001007")]
   [InlineData("00000000103")]
   [InlineData("00000000016")]
   public void Iso6346Algorithm_Validate_ShouldCorrectlyWeightByPosition(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("00000000000")]      // Note: Owner prefix code and category char should be alphabetic - 0/1 used for this test
   [InlineData("10000000001")]
   [InlineData("20000000002")]
   [InlineData("30000000003")]
   [InlineData("40000000004")]
   [InlineData("50000000005")]
   [InlineData("60000000006")]
   [InlineData("70000000007")]
   [InlineData("80000000008")]
   [InlineData("90000000009")]
   [InlineData("A0000000000")]
   [InlineData("B0000000001")]
   [InlineData("C0000000002")]
   [InlineData("D0000000003")]
   [InlineData("E0000000004")]
   [InlineData("F0000000005")]
   [InlineData("G0000000006")]
   [InlineData("H0000000007")]
   [InlineData("I0000000008")]
   [InlineData("J0000000009")]
   [InlineData("K0000000000")]
   [InlineData("L0000000001")]
   [InlineData("M0000000002")]
   [InlineData("N0000000003")]
   [InlineData("O0000000004")]
   [InlineData("P0000000005")]
   [InlineData("Q0000000006")]
   [InlineData("R0000000007")]
   [InlineData("S0000000008")]
   [InlineData("T0000000009")]
   [InlineData("U0000000000")]
   [InlineData("V0000000001")]
   [InlineData("W0000000002")]
   [InlineData("X0000000003")]
   [InlineData("Y0000000004")]
   [InlineData("Z0000000005")]
   public void Iso6346Algorithm_Validate_ShouldCalculateCorrectlyMapCharacters(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("CSQU3054383")]      // Worked example from Wikipedia
   [InlineData("TOLU4734787")]      // Photo from Wikipedia
   [InlineData("HJCU8281988")]      // Photo from Wikipedia
   [InlineData("BICU1234565")]      // Example from https://www.bic-code.org/identification-number/
   [InlineData("MSKU9070323")]      // Example from https://www.letterofcredit.biz/index.php/2019/11/04/what-is-a-container-number-explanations-with-examples/
   [InlineData("MEDU8707688")]      // "
   [InlineData("HLAU1234567")]      // Calculated from https://www.bic-code.org/check-digit-calculator/
   public void Iso6346Algorithm_Validate_ShouldReturnTrue_WhenValueContainsValidCheckDigit(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("CSQU7054383")]      // CSQU3054383 with single digit transcription error 3 -> 7
   [InlineData("CTQU3054383")]      // CSQU3054383 with single character transcription error S -> T
   [InlineData("MEDU7807688")]      // MEDU8707688 with two digit transposition error 87 -> 78 
   [InlineData("MDEU8707688")]      // MEDU8707688 with two character transposition error ED -> DE
   [InlineData("HLAU1122445")]      // HLAU1122335 with two digit twin error 33 -> 44
   [InlineData("HMMU1122332")]      // HLLU1122332 with two letter twin error LL -> MM
   public void Iso6346Algorithm_Validate_ShouldReturnFalse_WhenInputContainsDetectableError(string value)
      => _sut.Validate(value).Should().BeFalse();

   [Fact]
   public void Iso6346Algorithm_Validate_ShouldReturnTrue_WhenCheckDigitIsCalculatesAsZero()
      => _sut.Validate("A0000000000").Should().BeTrue();

   [Theory]
   [InlineData("CSQU3!54383")]
   [InlineData("CSQU3.54383")]
   [InlineData("CSQU3:54383")]
   [InlineData("CSQU3^54383")]
   public void Iso6346Algorithm_Validate_ShouldReturnFalse_WhenInputContainsInvalidCharacter(string value)
      => _sut.Validate(value).Should().BeFalse();

   #endregion
}
