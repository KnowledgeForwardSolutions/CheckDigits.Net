namespace CheckDigits.Net.Tests.Unit.Iso7064;

public class Iso7064Mod661_26AlgorithmTests
{
   private readonly Iso7064Mod661_26Algorithm _sut = new();
   private readonly ICheckDigitMask _acceptAllMask = new AcceptAllMask();
   private readonly ICheckDigitMask _groupsOfThreeMask = new GroupsOfThreeCheckDigitMask();
   private readonly ICheckDigitMask _rejectAllMask = new RejectAllMask();

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

   #region TryCalculateCheckDigits Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso7064Mod661_26Algorithm_TryCalculateCheckDigits_ShouldReturnFalse_WhenInputIsNull()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigits(null!, out var first, out var second).Should().BeFalse();
      first.Should().Be('\0');
      second.Should().Be('\0');
   }

   [Fact]
   public void Iso7064Mod661_26Algorithm_TryCalculateCheckDigits_ShouldReturnFalse_WhenInputIsEmpty()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigits(String.Empty, out var first, out var second).Should().BeFalse();
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
   public void Iso7064Mod661_26Algorithm_TryCalculateCheckDigits_ShouldCorrectlyMapCharacterValues(
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
   [InlineData("ISOHJ", 'T', 'C')]
   [InlineData("ABCDEFGHIJKLMN", 'J', 'F')]
   [InlineData("ASDFQWERTYLKJH", 'L', 'R')]
   [InlineData("AAAEEEIIIOOOUUUBCDEF", 'J', 'Y')]
   [InlineData("ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ", 'N', 'S')]
   public void Iso7064Mod661_26AlgorithmAlgorithm_TryCalculateCheckDigits_ShouldCalculateExpectedCheckDigit(
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
   [InlineData("^BCDE")]
   [InlineData("A!CDE")]
   [InlineData("AB0DE")]
   [InlineData("ABC9E")]
   [InlineData("ABCDa")]
   public void Iso7064Mod661_26Algorithm_TryCalculateCheckDigits_ShouldReturnFalse_WhenInputContainsInvalidCharacter(String value)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigits(value, out var first, out var second).Should().BeFalse();
      first.Should().Be('\0');
      second.Should().Be('\0');
   }

   [Theory]
   [InlineData("EGR")]
   [InlineData("EGRNML")]
   [InlineData("EGRNMLJOC")]
   [InlineData("EGRNMLJOCECU")]
   [InlineData("EGRNMLJOCECUJIK")]
   [InlineData("EGRNMLJOCECUJIKNWW")]
   [InlineData("EGRNMLJOCECUJIKNWWVVO")]
   public void Iso7064Mod661_26Algorithm_TryCalculateCheckDigit_ShouldReturnTrue_ForBenchmarkValues(String value)
      => _sut.TryCalculateCheckDigits(value, out _, out _).Should().BeTrue();

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
   [InlineData("^BCDEFX")]
   [InlineData("A!CDEFX")]
   [InlineData("AB0DEFX")]
   [InlineData("ABC9EFX")]
   [InlineData("ABCDaFX")]
   public void Iso7064Mod661_26Algorithm_Validate_ShouldReturnFalse_WhenValueContainsInvalidCharacter(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Fact]
   public void Iso7064Mod661_26Algorithm_Validate_ShouldReturnFalse_WhenValueContainsInvalidFirstCheckCharacter()
      => _sut.Validate("ABCD#F").Should().BeFalse();

   [Fact]
   public void Iso7064Mod661_26Algorithm_Validate_ShouldReturnFalse_WhenValueContainsInvalidSecondCheckCharacter()
      => _sut.Validate("ABCDE#").Should().BeFalse();

   [Theory]
   [InlineData("EGRSE")]
   [InlineData("EGRNMLDR")]
   [InlineData("EGRNMLJOCCK")]
   [InlineData("EGRNMLJOCECUZJ")]
   [InlineData("EGRNMLJOCECUJIKFQ")]
   [InlineData("EGRNMLJOCECUJIKNWWQN")]
   [InlineData("EGRNMLJOCECUJIKNWWVVORC")]
   public void Iso7064Mod661_26Algorithm_Validate_ShouldReturnTrue_ForBenchmarkValues(String value)
      => _sut.Validate(value).Should().BeTrue();

   #endregion

   #region Validate (ICheckDigitMask Overload) Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso7064Mod661_26Algorithm_ValidateMasked_ShouldThrowArgumentNullException_WhenMaskIsNull()
      => _sut
         .Invoking(x => x.Validate("07945", null!))
         .Should()
         .ThrowExactly<ArgumentNullException>()
         .WithParameterName("mask")
         .WithMessage(Resources.NullMaskMessage + "*");

   [Fact]
   public void Iso7064Mod661_26Algorithm_ValidateMasked_ShouldReturnFalse_WhenInputIsNull()
      => _sut.Validate(null!, _acceptAllMask).Should().BeFalse();

   [Fact]
   public void Iso7064Mod661_26Algorithm_ValidateMasked_ShouldReturnFalse_WhenInputIsEmpty()
      => _sut.Validate(String.Empty, _acceptAllMask).Should().BeFalse();

   [Theory]
   [InlineData("A")]
   [InlineData("B")]
   [InlineData("AA")]
   [InlineData("AB")]
   public void Iso7064Mod661_26Algorithm_ValidateMasked_ShouldReturnFalse_WhenInsufficientUnmaskedCharactersToCalculateCheckDigit(String value)
      => _sut.Validate(value, _acceptAllMask).Should().BeFalse();

   [Fact]
   public void Iso7064Mod661_26Algorithm_ValidateMasked_ShouldReturnFalse_WhenAllNonCheckDigitCharactersAreMaskedOut()
      => _sut.Validate("ISOHJTC", _rejectAllMask).Should().BeFalse();

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
   public void Iso7064Mod661_26Algorithm_ValidateMasked_ShouldCorrectlyMapCharacterValues(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeTrue();

   [Theory]
   [InlineData("ISO HJTC")]               // Note that mask is not applied to trailing two characters, so trailing 4 character group is valid
   [InlineData("ABC DEF GHI JKL MNJF")]
   [InlineData("ASD FQW ERT YLK JHLR")]
   [InlineData("AAA EEE III OOO UUU BCD EFJY")]
   [InlineData("ZZZ ZZZ ZZZ ZZZ ZZZ ZZZ ZZZ ZZZ ZZZ ZZZ NS")]
   public void Iso7064Mod661_26Algorithm_ValidateMasked_ShouldReturnTrue_WhenValueContainsValidCheckDigit(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeTrue();

   [Theory]
   [InlineData("ISOHJTC")]                
   [InlineData("ABCDEFGHIJKLMNJF")]
   [InlineData("ASDFQWERTYLKJHLR")]
   [InlineData("AAAEEEIIIOOOUUUBCDEFJY")]
   [InlineData("ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZNS")]
   public void Iso7064Mod661_26Algorithm_ValidateMasked_ShouldReturnTrue_WhenValueContainsValidCheckDigitAndMaskAcceptsAllCharacters(String value)
      => _sut.Validate(value, _acceptAllMask).Should().BeTrue();

   [Theory]
   [InlineData("ABE DEF GHI JKL MNJF")]         // ABCDEFGHIJKLMNJF with single char transcription error C -> E
   [InlineData("ABC DEF GHL JKL MNJF")]         // ABCDEFGHIJKLMNJF with single char transcription error I -> L
   [InlineData("ASB AQW ERT YLK JHLR")]         // ASDFQWERTYLKJHLR with two char transposition error DF -> BA 
   [InlineData("AAA EEE III OPP UUU BCD EFJY")] // AAAEEEIIIOOOUUUBCDEFJY with two char twin error OO -> PP
   [InlineData("ABC FED GHI JKL MNJF")]         // ABCDEFGHIJKLMNJF with jump transposition error DEF -> FED
   [InlineData("ABC DEF GHK JIL MNJF")]         // ABCDEFGHIJKLMNJF with jump transposition error IJK -> KJI
   [InlineData("SDF QWE RTY LKJ HLRA")]         // ASDFQWERTYLKJHLR with circular shift error
   [InlineData("RAS DFQ WER TYL KJHL")]         // ASDFQWERTYLKJHLR with circular shift error
   public void Iso7064Mod661_26Algorithm_ValidateMasked_ShouldReturnFalse_WhenInputContainsDetectableError(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeFalse();

   [Theory]
   [InlineData("^BC DEFX")]
   [InlineData("A!C DEFX")]
   [InlineData("AB0 DEFX")]
   [InlineData("ABC 9EFX")]
   [InlineData("ABC DaFX")]
   public void Iso7064Mod661_26Algorithm_ValidateMasked_ShouldReturnFalse_WhenValueContainsInvalidCharacter(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeFalse();

   [Theory]
   [InlineData("ISO HJ!C")]
   [InlineData("ISO HJ^C")]
   [InlineData("ISO HJ5C")]
   [InlineData("ISO HJaC")]
   public void Iso7064Mod661_26Algorithm_ValidateMasked_ShouldReturnFalse_WhenValueContainsInvalidFirstCheckCharacter(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeFalse();

   [Theory]
   [InlineData("ISO HJT!")]
   [InlineData("ISO HJT^")]
   [InlineData("ISO HJT5")]
   [InlineData("ISO HJTa")]
   public void Iso7064Mod661_26Algorithm_ValidateMasked_ShouldReturnFalse_WhenValueContainsInvalidSecondCheckCharacter(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeFalse();

   [Theory]
   [InlineData("EGR SE")]
   [InlineData("EGR NML DR")]
   [InlineData("EGR NML JOC CK")]
   [InlineData("EGR NML JOC ECU ZJ")]
   [InlineData("EGR NML JOC ECU JIK FQ")]
   [InlineData("EGR NML JOC ECU JIK NWW QN")]
   [InlineData("EGR NML JOC ECU JIK NWW VVO RC")]
   public void Iso7064Mod661_26Algorithm_ValidateMasked_ShouldReturnTrue_ForBenchmarkValues(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeTrue();

   #endregion
}
