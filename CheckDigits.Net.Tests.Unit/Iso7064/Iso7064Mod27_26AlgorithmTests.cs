namespace CheckDigits.Net.Tests.Unit.Iso7064;

public class Iso7064Mod27_26AlgorithmTests
{
   private readonly Iso7064Mod27_26Algorithm _sut = new();
   private readonly ICheckDigitMask _acceptAllMask = new AcceptAllMask();
   private readonly ICheckDigitMask _groupsOfThreeMask = new GroupsOfThreeCheckDigitMask();
   private readonly ICheckDigitMask _rejectAllMask = new RejectAllMask();

   #region AlgorithmDescription Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso7064Mod27_26Algorithm_AlgorithmDescription_ShouldReturnExpectedValue()
      => _sut.AlgorithmDescription.Should().Be(Resources.Iso7064Mod27_26AlgorithmDescription);

   #endregion

   #region AlgorithmName Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso7064Mod27_26Algorithm_AlgorithmName_ShouldReturnExpectedValue()
      => _sut.AlgorithmName.Should().Be(Resources.Iso7064Mod27_26AlgorithmName);

   #endregion

   #region TryCalculateCheckDigit Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso7064Mod27_26Algorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsNull()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(null!, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Fact]
   public void Iso7064Mod27_26Algorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsEmpty()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(String.Empty, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Theory]
   [InlineData("A", 'C')]
   [InlineData("B", 'Z')]
   [InlineData("C", 'X')]
   [InlineData("D", 'V')]
   [InlineData("E", 'T')]
   [InlineData("F", 'R')]
   [InlineData("G", 'P')]
   [InlineData("H", 'N')]
   [InlineData("I", 'L')]
   [InlineData("J", 'J')]
   [InlineData("K", 'H')]
   [InlineData("L", 'F')]
   [InlineData("M", 'D')]
   [InlineData("N", 'B')]
   [InlineData("O", 'A')]
   [InlineData("P", 'Y')]
   [InlineData("Q", 'W')]
   [InlineData("R", 'U')]
   [InlineData("S", 'S')]
   [InlineData("T", 'Q')]
   [InlineData("U", 'O')]
   [InlineData("V", 'M')]
   [InlineData("W", 'K')]
   [InlineData("X", 'I')]
   [InlineData("Y", 'G')]
   [InlineData("Z", 'E')]
   public void Iso7064Mod27_26Algorithm_TryCalculateCheckDigit_ShouldCorrectlyMapCharacterValues(
      String value,
      Char expectedCheckDigit)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Theory]
   [InlineData("AEIOU", 'I')]
   [InlineData("QWERTYDVORAK", 'Y')]
   [InlineData("ABCDEFGHIJKLMNOPQR", 'O')]
   [InlineData("THISISATESTTHISISONLYATEST", 'T')]
   [InlineData("ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ", 'B')]
   public void Iso7064Mod27_26Algorithm_TryCalculateCheckDigit_ShouldCalculateExpectedCheckDigit(
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
   [InlineData("ABC5EFX")]
   public void Iso7064Mod27_26Algorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputContainsInvalidCharacter(String value)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Theory]
   [InlineData("EGR")]
   [InlineData("EGRNML")]
   [InlineData("EGRNMLJOC")]
   [InlineData("EGRNMLJOCECU")]
   [InlineData("EGRNMLJOCECUJIK")]
   [InlineData("EGRNMLJOCECUJIKNWW")]
   [InlineData("EGRNMLJOCECUJIKNWWVVO")]
   public void Iso7064Mod27_26Algorithm_TryCalculateCheckDigit_ShouldReturnTrue_ForBenchmarkValues(String value)
      => _sut.TryCalculateCheckDigit(value, out _).Should().BeTrue();

   #endregion

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso7064Mod27_26Algorithm_Validate_ShouldReturnFalse_WhenInputIsNull()
      => _sut.Validate(null!).Should().BeFalse();

   [Fact]
   public void Iso7064Mod27_26Algorithm_Validate_ShouldReturnFalse_WhenInputIsEmpty()
      => _sut.Validate(String.Empty).Should().BeFalse();

   [Theory]
   [InlineData("A")]
   [InlineData("B")]
   public void Iso7064Mod27_26Algorithm_Validate_ShouldReturnFalse_WhenInputIsOneCharacterInLength(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Theory]
   [InlineData("AC")]
   [InlineData("BZ")]
   [InlineData("CX")]
   [InlineData("DV")]
   [InlineData("ET")]
   [InlineData("FR")]
   [InlineData("GP")]
   [InlineData("HN")]
   [InlineData("IL")]
   [InlineData("JJ")]
   [InlineData("KH")]
   [InlineData("LF")]
   [InlineData("MD")]
   [InlineData("NB")]
   [InlineData("OA")]
   [InlineData("PY")]
   [InlineData("QW")]
   [InlineData("RU")]
   [InlineData("SS")]
   [InlineData("TQ")]
   [InlineData("UO")]
   [InlineData("VM")]
   [InlineData("WK")]
   [InlineData("XI")]
   [InlineData("YG")]
   [InlineData("ZE")]
   public void Iso7064Mod27_26Algorithm_Validate_ShouldCorrectlyMapCharacterValues(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("AEIOUI")]
   [InlineData("QWERTYDVORAKY")]
   [InlineData("ABCDEFGHIJKLMNOPQRO")]
   [InlineData("THISISATESTTHISISONLYATESTT")]
   [InlineData("ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZB")]
   public void Iso7064Mod27_26Algorithm_Validate_ShouldReturnTrue_WhenValueContainsValidCheckDigit(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("ACIOUI")]                       // AEIOUI with single char transcription error E -> C
   [InlineData("QWERTUDVORAKY")]                // QWERTYDVORAKY with single char transcription error Y -> U
   [InlineData("ABCDEFHGIJKLMNOPQRO")]          // ABCDEFGHIJKLMNOPQRO with two char transposition error GH -> HG 
   [InlineData("THISISATESUUHISISONLYATESTT")]  // THISISATESTTHISISONLYATESTT with two char twin error TT -> UU
   [InlineData("ABCFEDGHIJKLMNOPQRO")]          // ABCDEFGHIJKLMNOPQRO with jump transposition error DEF -> FED
   [InlineData("QWERDYTVORAKY")]                // QWERTYDVORAKY with jump transposition error TYD -> DYT
   [InlineData("IAEIOU")]                       // AEIOUI with circular shift error
   [InlineData("EIOUIA")]                       // AEIOUI with circular shift error
   public void Iso7064Mod27_26Algorithm_Validate_ShouldReturnFalse_WhenInputContainsDetectableError(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Fact]
   public void Iso7064Mod27_26Algorithm_Validate_ShouldReturnTrue_WhenCheckDigitIsCalculatesAsA()
      => _sut.Validate("ABCDEA").Should().BeTrue();

   [Theory]
   [InlineData("ABC!EFX")]
   [InlineData("ABC^EFX")]
   [InlineData("ABC5EFX")]
   public void Iso7064Mod27_26Algorithm_Validate_ShouldReturnFalse_WhenValueContainsInvalidCharacter(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Theory]
   [InlineData("ABCDE!")]
   [InlineData("ABCDE^")]
   [InlineData("ABCDE5")]
   public void Iso7064Mod27_26Algorithm_Validate_ShouldReturnFalse_WhenValueContainsInvalidCheckDigitCharacter(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Theory]
   [InlineData("EGRS")]
   [InlineData("EGRNMLU")]
   [InlineData("EGRNMLJOCB")]
   [InlineData("EGRNMLJOCECUA")]
   [InlineData("EGRNMLJOCECUJIKA")]
   [InlineData("EGRNMLJOCECUJIKNWWY")]
   [InlineData("EGRNMLJOCECUJIKNWWVVOQ")]
   public void Iso7064Mod27_26Algorithm_Validate_ShouldReturnTrue_ForBenchmarkValues(String value)
      => _sut.Validate(value).Should().BeTrue();

   #endregion

   #region Validate (ICheckDigitMask Overload) Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso7064Mod27_26Algorithm_ValidateMasked_ShouldThrowArgumentNullException_WhenMaskIsNull()
      => _sut
         .Invoking(x => x.Validate("AEIOUI", null!))
         .Should()
         .ThrowExactly<ArgumentNullException>()
         .WithParameterName("mask")
         .WithMessage(Resources.NullMaskMessage + "*");

   [Fact]
   public void Iso7064Mod27_26Algorithm_ValidateMasked_ShouldReturnFalse_WhenInputIsNull()
      => _sut.Validate(null!, _acceptAllMask).Should().BeFalse();

   [Fact]
   public void Iso7064Mod27_26Algorithm_ValidateMasked_ShouldReturnFalse_WhenInputIsEmpty()
      => _sut.Validate(String.Empty, _acceptAllMask).Should().BeFalse();

   [Theory]
   [InlineData("A")]
   [InlineData("B")]
   public void Iso7064Mod27_26Algorithm_ValidateMasked_ShouldReturnFalse_WhenInsufficientUnmaskedCharactersToCalculateCheckDigit(String value)
      => _sut.Validate(value, _acceptAllMask).Should().BeFalse();

   [Fact]
   public void Iso7064Mod27_26Algorithm_ValidateMasked_ShouldReturnFalse_WhenAllNonCheckDigitCharactersAreMaskedOut()
      => _sut.Validate("AEIOUI", _rejectAllMask).Should().BeFalse();

   [Theory]
   [InlineData("AC")]
   [InlineData("BZ")]
   [InlineData("CX")]
   [InlineData("DV")]
   [InlineData("ET")]
   [InlineData("FR")]
   [InlineData("GP")]
   [InlineData("HN")]
   [InlineData("IL")]
   [InlineData("JJ")]
   [InlineData("KH")]
   [InlineData("LF")]
   [InlineData("MD")]
   [InlineData("NB")]
   [InlineData("OA")]
   [InlineData("PY")]
   [InlineData("QW")]
   [InlineData("RU")]
   [InlineData("SS")]
   [InlineData("TQ")]
   [InlineData("UO")]
   [InlineData("VM")]
   [InlineData("WK")]
   [InlineData("XI")]
   [InlineData("YG")]
   [InlineData("ZE")]
   public void Iso7064Mod27_26Algorithm_ValidateMasked_ShouldCorrectlyMapCharacterValues(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeTrue();

   [Theory]
   [InlineData("AEI OUI")]
   [InlineData("QWE RTY DVO RAK Y")]
   [InlineData("ABC DEF GHI JKL MNO PQR O")]
   [InlineData("THI SIS ATE STT HIS ISO NLY ATE STT")]
   [InlineData("ZZZ ZZZ ZZZ ZZZ ZZZ ZZZ ZZZ ZZZ ZZZ ZZZ B")]
   public void Iso7064Mod27_26Algorithm_ValidateMasked_ShouldReturnTrue_WhenValueContainsValidCheckDigit(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeTrue();

   [Theory]
   [InlineData("AEIOUI")]
   [InlineData("QWERTYDVORAKY")]
   [InlineData("ABCDEFGHIJKLMNOPQRO")]
   [InlineData("THISISATESTTHISISONLYATESTT")]
   [InlineData("ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZB")]
   public void Iso7064Mod27_26Algorithm_ValidateMasked_ShouldReturnTrue_WhenValueContainsValidCheckDigitAndMaskAcceptsAllCharacters(String value)
      => _sut.Validate(value, _acceptAllMask).Should().BeTrue();

   [Theory]
   [InlineData("ACI OUI")]                               // AEIOUI with single char transcription error E -> C
   [InlineData("QWE RTU DVO RAK Y")]                     // QWERTYDVORAKY with single char transcription error Y -> U
   [InlineData("ABC DEF HGI JKL MNO PQR O")]             // ABCDEFGHIJKLMNOPQRO with two char transposition error GH -> HG 
   [InlineData("THI SIS ATE SUU HIS ISO NLY ATE STT")]   // THISISATESTTHISISONLYATESTT with two char twin error TT -> UU
   [InlineData("ABC FED GHI JKL MNO PQR O")]             // ABCDEFGHIJKLMNOPQRO with jump transposition error DEF -> FED
   [InlineData("QWE RDY TVO RAK Y")]                     // QWERTYDVORAKY with jump transposition error TYD -> DYT
   [InlineData("IAE IOU")]                               // AEIOUI with circular shift error
   [InlineData("EIO UIA")]                               // AEIOUI with circular shift error
   public void Iso7064Mod27_26Algorithm_ValidateMasked_ShouldReturnFalse_WhenInputContainsDetectableError(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeFalse();

   [Fact]
   public void Iso7064Mod27_26Algorithm_ValidateMasked_ShouldReturnTrue_WhenCheckDigitIsCalculatesAsA()
      => _sut.Validate("ABC DEA", _groupsOfThreeMask).Should().BeTrue();

   [Theory]
   [InlineData("ABC !EFX")]
   [InlineData("ABC ^EFX")]
   [InlineData("ABC 5EFX")]
   public void Iso7064Mod27_26Algorithm_ValidateMasked_ShouldReturnFalse_WhenValueContainsInvalidCharacter(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeFalse();

   [Theory]
   [InlineData("ABC DE!")]
   [InlineData("ABC DE^")]
   [InlineData("ABC DE5")]
   public void Iso7064Mod27_26Algorithm_ValidateMasked_ShouldReturnFalse_WhenValueContainsInvalidCheckDigitCharacter(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeFalse();

   [Theory]
   [InlineData("EGR S")]
   [InlineData("EGR NML U")]
   [InlineData("EGR NML JOC B")]
   [InlineData("EGR NML JOC ECU A")]
   [InlineData("EGR NML JOC ECU JIK A")]
   [InlineData("EGR NML JOC ECU JIK NWW Y")]
   [InlineData("EGR NML JOC ECU JIK NWW VVO Q")]
   public void Iso7064Mod27_26Algorithm_ValidateMasked_ShouldReturnTrue_ForBenchmarkValues(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeTrue();

   #endregion
}
