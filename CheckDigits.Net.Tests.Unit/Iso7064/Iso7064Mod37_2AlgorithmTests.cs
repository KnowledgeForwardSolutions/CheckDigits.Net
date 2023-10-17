namespace CheckDigits.Net.Tests.Unit.Iso7064;

public class Iso7064Mod37_2AlgorithmTests
{
   private readonly Iso7064Mod37_2Algorithm _sut = new();

   #region AlgorithmDescription Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso7064Mod37_2Algorithm_AlgorithmDescription_ShouldReturnExpectedValue()
      => _sut.AlgorithmDescription.Should().Be(Resources.Iso7064Mod37_2AlgorithmDescription);

   #endregion

   #region AlgorithmName Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso7064Mod37_2Algorithm_AlgorithmName_ShouldReturnExpectedValue()
      => _sut.AlgorithmName.Should().Be(Resources.Iso7064Mod37_2AlgorithmName);

   #endregion

   #region TryCalculateCheckDigit Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso7064Mod37_2Algorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsNull()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(null!, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Fact]
   public void Iso7064Mod37_2Algorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsEmpty()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(String.Empty, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Theory]
   [InlineData("0", '1')]
   [InlineData("1", '*')]
   [InlineData("2", 'Y')]
   [InlineData("3", 'W')]
   [InlineData("4", 'U')]
   [InlineData("5", 'S')]
   [InlineData("6", 'Q')]
   [InlineData("7", 'O')]
   [InlineData("8", 'M')]
   [InlineData("9", 'K')]
   [InlineData("A", 'I')]
   [InlineData("B", 'G')]
   [InlineData("C", 'E')]
   [InlineData("D", 'C')]
   [InlineData("E", 'A')]
   [InlineData("F", '8')]
   [InlineData("G", '6')]
   [InlineData("H", '4')]
   [InlineData("I", '2')]
   [InlineData("J", '0')]
   [InlineData("K", 'Z')]
   [InlineData("L", 'X')]
   [InlineData("M", 'V')]
   [InlineData("N", 'T')]
   [InlineData("O", 'R')]
   [InlineData("P", 'P')]
   [InlineData("Q", 'N')]
   [InlineData("R", 'L')]
   [InlineData("S", 'J')]
   [InlineData("T", 'H')]
   [InlineData("U", 'F')]
   [InlineData("V", 'D')]
   [InlineData("W", 'B')]
   [InlineData("X", '9')]
   [InlineData("Y", '7')]
   [InlineData("Z", '5')]
   public void Iso7064Mod37_2Algorithm_TryCalculateCheckDigit_ShouldCorrectlyMapCharacterValues(
      String value,
      Char expectedCheckDigit)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Theory]
   [InlineData("A999914123456", 'N')]      // Example ISBT from https://www.isbt128.org/_files/ugd/83d6e1_9c7ba55fbdd44a80947bc310cdd92382.pdf
   [InlineData("C000307001466", '6')]      // Example ISBT from https://www.isbt128.org/_files/ugd/83d6e1_10edb0e64b234943abd9c100b925575c.pdf
   [InlineData("A999922123459", 'H')]      // Example ISBT from https://www.isbt128.org/_files/ugd/79eb0b_1a92d4e286af404183d03bf5bab9120f.pdf
   [InlineData("A999922654321", 'S')]      // "
   [InlineData("A999922123458", 'J')]      // "
   [InlineData("A999922012346", '*')]      // "
   [InlineData("A999522123456", '*')]      // "
   [InlineData("G123498654321", 'H')]      // Example from https://www.transfusionguidelines.org/red-book/annex-2-isbt-128-check-character-calculation
   [InlineData("ZZZZ", 'O')]
   [InlineData("ABCDEFGHIJKLMNOPQRSTUVWX", '*')]
   [InlineData("ZZZZZZZZZZZZZZZZZZZZZZZZZZ", '9')]
   public void Iso7064Mod37_2AlgorithmAlgorithm_TryCalculateCheckDigit_ShouldCalculateExpectedCheckDigit(
      String value,
      Char expectedCheckDigit)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Theory]
   [InlineData("123!56")]
   [InlineData("123^56")]
   public void Iso7064Mod37_2Algorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputContainsInvalidCharacter(String value)
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
   public void Iso7064Mod37_2Algorithm_Validate_ShouldReturnFalse_WhenInputIsNull()
      => _sut.Validate(null!).Should().BeFalse();

   [Fact]
   public void Iso7064Mod37_2Algorithm_Validate_ShouldReturnFalse_WhenInputIsEmpty()
      => _sut.Validate(String.Empty).Should().BeFalse();

   [Theory]
   [InlineData("0")]
   [InlineData("1")]          // "1" will fail if length check is not performed
   public void Iso7064Mod37_2Algorithm_Validate_ShouldReturnFalse_WhenInputIsOneCharacterInLength(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Theory]
   [InlineData("01")]
   [InlineData("1*")]
   [InlineData("2Y")]
   [InlineData("3W")]
   [InlineData("4U")]
   [InlineData("5S")]
   [InlineData("6Q")]
   [InlineData("7O")]
   [InlineData("8M")]
   [InlineData("9K")]
   [InlineData("AI")]
   [InlineData("BG")]
   [InlineData("CE")]
   [InlineData("DC")]
   [InlineData("EA")]
   [InlineData("F8")]
   [InlineData("G6")]
   [InlineData("H4")]
   [InlineData("I2")]
   [InlineData("J0")]
   [InlineData("KZ")]
   [InlineData("LX")]
   [InlineData("MV")]
   [InlineData("NT")]
   [InlineData("OR")]
   [InlineData("PP")]
   [InlineData("QN")]
   [InlineData("RL")]
   [InlineData("SJ")]
   [InlineData("TH")]
   [InlineData("UF")]
   [InlineData("VD")]
   [InlineData("WB")]
   [InlineData("X9")]
   [InlineData("Y7")]
   [InlineData("Z5")]
   public void Iso7064Mod37_2Algorithm_Validate_ShouldCorrectlyMapCharacterValues(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]                            // Example ISBT Donation Identification Numbers
   [InlineData("A999914123456N")]      // Example ISBT from https://www.isbt128.org/_files/ugd/83d6e1_9c7ba55fbdd44a80947bc310cdd92382.pdf
   [InlineData("C0003070014666")]      // Example ISBT from https://www.isbt128.org/_files/ugd/83d6e1_10edb0e64b234943abd9c100b925575c.pdf
   [InlineData("A999922123459H")]      // Example ISBT from https://www.isbt128.org/_files/ugd/79eb0b_1a92d4e286af404183d03bf5bab9120f.pdf
   [InlineData("A999922654321S")]      // "
   [InlineData("A999922123458J")]      // "
   [InlineData("A999922012346*")]      // "
   [InlineData("A999522123456*")]      // "
   [InlineData("G123498654321H")]      // Example from https://www.transfusionguidelines.org/red-book/annex-2-isbt-128-check-character-calculation
   [InlineData("ZZZZO")]
   [InlineData("ABCDEFGHIJKLMNOPQRSTUVWX*")]
   [InlineData("ZZZZZZZZZZZZZZZZZZZZZZZZZZ9")]
   public void Iso7064Mod37_2Algorithm_Validate_ShouldReturnTrue_WhenValueContainsValidCheckDigit(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("A999915123456N")]      // A999914123456N with single digit transcription error 4 -> 5
   [InlineData("F123498654321H")]      // G123498654321H with single char transcription error G -> F
   [InlineData("C0003070013666")]      // C0003070014666 with single digit transcription error 4 -> 3
   [InlineData("A999922564321S")]      // A999922654321S with two digit transposition error 65 -> 56 
   [InlineData("A999920212346*")]      // A999922012346* with two digit transposition error 20 -> 02
   [InlineData("A999933123458J")]      // A999922123458J with two digit twin error 22 -> 33
   [InlineData("C0003070014556")]      // C0003070014666 two digit twin error 66 -> 55
   [InlineData("G123468954321H")]      // G123498654321H with jump transposition error 986 -> 689
   [InlineData("6C000307001466")]      // C0003070014666 with circular shift error
   [InlineData("999914123456NA")]      // A999914123456N with circular shift error
   [InlineData("B999922123469H")]      // A999922123459H with two single transcription errors A -> B, 5 -> 6
   // NOTE A999914123456N with circular shift error to NA999914123456 is not detected
   public void Iso7064Mod37_2Algorithm_Validate_ShouldReturnFalse_WhenInputContainsDetectableError(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Fact]
   public void Iso7064Mod37_2Algorithm_Validate_ShouldReturnTrue_WhenCheckDigitIsCalculatesAsAsterisk()
      => _sut.Validate("A999522123456*").Should().BeTrue();

   [Fact]
   public void Iso7064Mod37_2Algorithm_Validate_ShouldReturnFalse_WhenValueContainsAsteriskOtherThanTrailingPosition()
      => _sut.Validate("ZZ*A1Q").Should().BeFalse();

   [Theory]
   [InlineData("123!56X")]
   [InlineData("123^56X")]
   public void Iso7064Mod37_2Algorithm_Validate_ShouldReturnFalse_WhenValueContainsInvalidCharacter(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Fact]
   public void Iso7064Mod37_2Algorithm_Validate_ShouldReturnFalse_WhenValueContainsInvalidCheckDigitCharacter()
      => _sut.Validate("12345!").Should().BeFalse();

   #endregion
}
