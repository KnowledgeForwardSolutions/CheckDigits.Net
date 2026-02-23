// Ignore Spelling: Icao Mrz

namespace CheckDigits.Net.Tests.Unit.ValueSpecificAlgorithms;

public class Icao9303SizeTD1AlgorithmTests
{
   private readonly Icao9303SizeTD1Algorithm _sut = new();

   // Example MRZ from https://www.icao.int/publications/Documents/9303_p5_cons_en.pdf
   private const String _mrzFirstLine = "I<UTOD231458907<<<<<<<<<<<<<<<";
   private const String _mrzSecondLine = "7408122F1204159UTO<<<<<<<<<<<6";
   private const String _mrzThirdLine = "ERIKSSON<<ANNA<MARIA<<<<<<<<<<";
   private const String _emptySeparator = "";
   private const String _crlf = "\r\n";
   private const String _lf = "\n";

   private static String GetTestValue(
      String separatorChars = _emptySeparator,
      #if !NET48
      String? firstLine = null,
      String? secondLine = null,
      String? thirdLine = null)
      #else
      String firstLine = null,
      String secondLine = null,
      String thirdLine = null)
#endif
      => (firstLine ?? _mrzFirstLine) 
         + separatorChars + (secondLine ?? _mrzSecondLine)
         + separatorChars + (thirdLine ?? _mrzThirdLine);

   #region AlgorithmDescription Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Icao9303SizeTD1Algorithm_AlgorithmDescription_ShouldReturnExpectedValue()
      => _sut.AlgorithmDescription.Should().Be(Resources.Icao9303SizeTD1AlgorithmDescription);

   #endregion

   #region AlgorithmName Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Icao9303SizeTD1Algorithm_AlgorithmName_ShouldReturnExpectedValue()
      => _sut.AlgorithmName.Should().Be(Resources.Icao9303SizeTD1AlgorithmName);

   #endregion

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnFalse_WhenInputIsNull()
      => _sut.Validate(null!).Should().BeFalse();

   [Fact]
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnFalse_WhenInputIsEmpty()
      => _sut.Validate(String.Empty).Should().BeFalse();

   public static TheoryData<String> InvalidLengthData => new()
   {
      { GetTestValue(_emptySeparator, _mrzFirstLine[..29]) },
      { GetTestValue(_crlf, _mrzFirstLine[..29]) },
      { GetTestValue(_lf,   _mrzFirstLine[..29]) },
      { GetTestValue(_emptySeparator, _mrzFirstLine + "<<") },
      { GetTestValue(_crlf, _mrzFirstLine + "<<") },
      { GetTestValue(_lf,   _mrzFirstLine + "<<") },

   };

   [Theory]
   [MemberData(nameof(InvalidLengthData))]
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnFalse_WhenValueIsInvalidLength(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Theory]
   [InlineData(_mrzFirstLine + "X\n" + _mrzSecondLine + _crlf + _mrzThirdLine)]        // 'X' instead of \r
   [InlineData(_mrzFirstLine + _crlf + _mrzSecondLine + "X\n" + _mrzThirdLine)]        // 'X' instead of \r
   [InlineData(_mrzFirstLine + " \n" + _mrzSecondLine + _crlf + _mrzThirdLine)]        // Space instead of \r
   [InlineData(_mrzFirstLine + _crlf + _mrzSecondLine + " \n" + _mrzThirdLine)]        // Space instead of \r
   [InlineData(_mrzFirstLine + "\r " + _mrzSecondLine + _crlf + _mrzThirdLine)]        // Space instead of \n
   [InlineData(_mrzFirstLine + _crlf + _mrzSecondLine + "\r " + _mrzThirdLine)]        // Space instead of \n
   [InlineData(_mrzFirstLine + "\n\r" + _mrzSecondLine + _crlf + _mrzThirdLine)]       // \n\r instead of \r\n
   [InlineData(_mrzFirstLine + _crlf + _mrzSecondLine + "\n\r" + _mrzThirdLine)]       // \n\r instead of \r\n
   [InlineData(_mrzFirstLine + " " + _mrzSecondLine + _lf + _mrzThirdLine)]            // Space instead of \n
   [InlineData(_mrzFirstLine + _lf + _mrzSecondLine + " " + _mrzThirdLine)]            // Space instead of \n
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnFalse_WhenSeparatorCharactersAreInvalid(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Theory]
   [InlineData(_emptySeparator, "I<UTO0000000000<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<0")]
   [InlineData(_emptySeparator, "I<UTO0010000001<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<8")]
   [InlineData(_emptySeparator, "I<UTO0020000002<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<6")]
   [InlineData(_emptySeparator, "I<UTO0030000003<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<4")]
   [InlineData(_emptySeparator, "I<UTO0040000004<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<2")]
   [InlineData(_emptySeparator, "I<UTO0050000005<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<0")]
   [InlineData(_emptySeparator, "I<UTO0060000006<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<8")]
   [InlineData(_emptySeparator, "I<UTO0070000007<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<6")]
   [InlineData(_emptySeparator, "I<UTO0080000008<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<4")]
   [InlineData(_emptySeparator, "I<UTO0090000009<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<2")]
   [InlineData(_crlf, "I<UTO00A0000000<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<0")]
   [InlineData(_crlf, "I<UTO00B0000001<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<8")]
   [InlineData(_crlf, "I<UTO00C0000002<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<6")]
   [InlineData(_crlf, "I<UTO00D0000003<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<4")]
   [InlineData(_crlf, "I<UTO00E0000004<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<2")]
   [InlineData(_crlf, "I<UTO00F0000005<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<0")]
   [InlineData(_crlf, "I<UTO00G0000006<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<8")]
   [InlineData(_crlf, "I<UTO00H0000007<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<6")]
   [InlineData(_crlf, "I<UTO00I0000008<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<4")]
   [InlineData(_crlf, "I<UTO00J0000009<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<2")]
   [InlineData(_lf,   "I<UTO00K0000000<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<0")]
   [InlineData(_lf,   "I<UTO00L0000001<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<8")]
   [InlineData(_lf,   "I<UTO00M0000002<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<6")]
   [InlineData(_lf,   "I<UTO00N0000003<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<4")]
   [InlineData(_lf,   "I<UTO00O0000004<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<2")]
   [InlineData(_lf,   "I<UTO00P0000005<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<0")]
   [InlineData(_lf,   "I<UTO00Q0000006<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<8")]
   [InlineData(_lf,   "I<UTO00R0000007<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<6")]
   [InlineData(_lf,   "I<UTO00S0000008<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<4")]
   [InlineData(_lf,   "I<UTO00T0000009<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<2")]
   [InlineData(_emptySeparator, "I<UTO00U0000000<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<0")]
   [InlineData(_emptySeparator, "I<UTO00V0000001<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<8")]
   [InlineData(_emptySeparator, "I<UTO00W0000002<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<6")]
   [InlineData(_emptySeparator, "I<UTO00X0000003<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<4")]
   [InlineData(_emptySeparator, "I<UTO00Y0000004<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<2")]
   [InlineData(_emptySeparator, "I<UTO00Z0000005<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<0")]
   [InlineData(_emptySeparator, "I<UTO00<0000000<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<0")]
   public void Icao9303SizeTD1Algorithm_Validate_ShouldCorrectlyMapFieldCharacterValues(
      String lineSeparator,
      String mrzFirstLine,
      String mrzSecondLine)
   {
      // Arrange.
      var value = GetTestValue(lineSeparator, mrzFirstLine, mrzSecondLine);

      // Act/assert.
      _sut.Validate(value).Should().BeTrue();
   }

   [Theory]
   [InlineData(_crlf, "I<UTO00a0000000<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<0")]
   [InlineData(_crlf, "I<UTO00b0000001<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<8")]
   [InlineData(_crlf, "I<UTO00c0000002<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<6")]
   [InlineData(_crlf, "I<UTO00d0000003<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<4")]
   [InlineData(_crlf, "I<UTO00e0000004<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<2")]
   [InlineData(_crlf, "I<UTO00f0000005<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<0")]
   [InlineData(_crlf, "I<UTO00g0000006<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<8")]
   [InlineData(_crlf, "I<UTO00h0000007<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<6")]
   [InlineData(_crlf, "I<UTO00i0000008<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<4")]
   [InlineData(_crlf, "I<UTO00j0000009<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<2")]
   [InlineData(_lf, "I<UTO00k0000000<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<0")]
   [InlineData(_lf, "I<UTO00l0000001<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<8")]
   [InlineData(_lf, "I<UTO00m0000002<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<6")]
   [InlineData(_lf, "I<UTO00n0000003<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<4")]
   [InlineData(_lf, "I<UTO00o0000004<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<2")]
   [InlineData(_lf, "I<UTO00p0000005<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<0")]
   [InlineData(_lf, "I<UTO00q0000006<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<8")]
   [InlineData(_lf, "I<UTO00r0000007<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<6")]
   [InlineData(_lf, "I<UTO00s0000008<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<4")]
   [InlineData(_lf, "I<UTO00t0000009<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<2")]
   [InlineData(_emptySeparator, "I<UTO00u0000000<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<0")]
   [InlineData(_emptySeparator, "I<UTO00v0000001<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<8")]
   [InlineData(_emptySeparator, "I<UTO00w0000002<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<6")]
   [InlineData(_emptySeparator, "I<UTO00x0000003<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<4")]
   [InlineData(_emptySeparator, "I<UTO00y0000004<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<2")]
   [InlineData(_emptySeparator, "I<UTO00z0000005<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<0")]
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnFalse_WhenLowerCaseAlphabeticCharacterEncountered(
      String lineSeparator,
      String mrzFirstLine,
      String mrzSecondLine)
   {
      // Arrange.
      var value = GetTestValue(lineSeparator, mrzFirstLine, mrzSecondLine);

      // Act/assert.
      _sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   // Document number field
   [InlineData(_emptySeparator, "I<UTO1000000007<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<6")]
   [InlineData(_emptySeparator, "I<UTO0100000003<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<4")]
   [InlineData(_emptySeparator, "I<UTO0010000001<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<8")]
   [InlineData(_emptySeparator, "I<UTO0001000007<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<6")]
   [InlineData(_emptySeparator, "I<UTO0000100003<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<4")]
   [InlineData(_emptySeparator, "I<UTO0000010001<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<8")]
   [InlineData(_emptySeparator, "I<UTO0000001007<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<6")]
   [InlineData(_emptySeparator, "I<UTO0000000103<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<4")]
   [InlineData(_emptySeparator, "I<UTO0000000011<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<8")]
   // Extended document number
   [InlineData(_emptySeparator, "I<UTO000000000<10000000000007<", "0000000F0000000UTO<<<<<<<<<<<8")]
   [InlineData(_emptySeparator, "I<UTO000000000<01000000000003<", "0000000F0000000UTO<<<<<<<<<<<2")]
   [InlineData(_emptySeparator, "I<UTO000000000<00100000000001<", "0000000F0000000UTO<<<<<<<<<<<4")]
   [InlineData(_emptySeparator, "I<UTO000000000<00010000000007<", "0000000F0000000UTO<<<<<<<<<<<8")]
   [InlineData(_emptySeparator, "I<UTO000000000<00001000000003<", "0000000F0000000UTO<<<<<<<<<<<2")]
   [InlineData(_emptySeparator, "I<UTO000000000<00000100000001<", "0000000F0000000UTO<<<<<<<<<<<4")]
   [InlineData(_emptySeparator, "I<UTO000000000<00000010000007<", "0000000F0000000UTO<<<<<<<<<<<8")]
   [InlineData(_emptySeparator, "I<UTO000000000<00000001000003<", "0000000F0000000UTO<<<<<<<<<<<2")]
   [InlineData(_emptySeparator, "I<UTO000000000<00000000100001<", "0000000F0000000UTO<<<<<<<<<<<4")]
   [InlineData(_emptySeparator, "I<UTO000000000<00000000010007<", "0000000F0000000UTO<<<<<<<<<<<8")]
   [InlineData(_emptySeparator, "I<UTO000000000<00000000001003<", "0000000F0000000UTO<<<<<<<<<<<2")]
   [InlineData(_emptySeparator, "I<UTO000000000<00000000000101<", "0000000F0000000UTO<<<<<<<<<<<4")]
   [InlineData(_emptySeparator, "I<UTO000000000<00000000000017<", "0000000F0000000UTO<<<<<<<<<<<8")]
   // Date of birth field
   [InlineData(_crlf, "I<UTO0000000000<<<<<<<<<<<<<<<", "1000007F0000000UTO<<<<<<<<<<<4")]
   [InlineData(_crlf, "I<UTO0000000000<<<<<<<<<<<<<<<", "0100003F0000000UTO<<<<<<<<<<<0")]
   [InlineData(_crlf, "I<UTO0000000000<<<<<<<<<<<<<<<", "0010001F0000000UTO<<<<<<<<<<<0")]
   [InlineData(_crlf, "I<UTO0000000000<<<<<<<<<<<<<<<", "0001007F0000000UTO<<<<<<<<<<<4")]
   [InlineData(_crlf, "I<UTO0000000000<<<<<<<<<<<<<<<", "0000103F0000000UTO<<<<<<<<<<<0")]
   [InlineData(_crlf, "I<UTO0000000000<<<<<<<<<<<<<<<", "0000011F0000000UTO<<<<<<<<<<<0")]
   // Date of expiry field
   [InlineData(_lf,   "I<UTO0000000000<<<<<<<<<<<<<<<", "0000000F1000007UTO<<<<<<<<<<<8")]
   [InlineData(_lf,   "I<UTO0000000000<<<<<<<<<<<<<<<", "0000000F0100003UTO<<<<<<<<<<<0")]
   [InlineData(_lf,   "I<UTO0000000000<<<<<<<<<<<<<<<", "0000000F0010001UTO<<<<<<<<<<<4")]
   [InlineData(_lf,   "I<UTO0000000000<<<<<<<<<<<<<<<", "0000000F0001007UTO<<<<<<<<<<<8")]
   [InlineData(_lf,   "I<UTO0000000000<<<<<<<<<<<<<<<", "0000000F0000103UTO<<<<<<<<<<<0")]
   [InlineData(_lf,   "I<UTO0000000000<<<<<<<<<<<<<<<", "0000000F0000011UTO<<<<<<<<<<<4")]
   public void Icao9303SizeTD1Algorithm_Validate_ShouldCorrectlyWeightByCharacterPosition(
      String lineSeparator,
      String mrzFirstLine,
      String mrzSecondLine)
   {
      // Arrange.
      var value = GetTestValue(lineSeparator, mrzFirstLine, mrzSecondLine);

      // Act/assert.
      _sut.Validate(value).Should().BeTrue();
   }

   [Theory]
   [InlineData(_emptySeparator, "I<UTOD231458907<<<<<<<<<<<<<<<", "7408122F1204159UTO<<<<<<<<<<<6")]
   [InlineData(_crlf,           "I<UTOD231458907<<<<<<<<<<<<<<<", "7408122F1204159UTO<<<<<<<<<<<6")]
   [InlineData(_lf,             "I<UTOD231458907<<<<<<<<<<<<<<<", "7408122F1204159UTO<<<<<<<<<<<6")]
   [InlineData(_emptySeparator, "I<UTOD23145890<AB112234566<<<<", "7408122F1204159UTO<<<<<<<<<<<4")]
   [InlineData(_crlf,           "I<UTOD23145890<AB112234566<<<<", "7408122F1204159UTO<<<<<<<<<<<4")]
   [InlineData(_lf,             "I<UTOD23145890<AB112234566<<<<", "7408122F1204159UTO<<<<<<<<<<<4")]
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnTrue_WhenValueContainsValidCheckDigits(
      String lineSeparator,
      String mrzFirstLine,
      String mrzSecondLine)
   {
      // Arrange.
      var value = GetTestValue(lineSeparator, mrzFirstLine, mrzSecondLine);

      // Act/assert.
      _sut.Validate(value).Should().BeTrue();
   }

   [Theory]
   [InlineData(_emptySeparator, "I<UTOD23145890<00000000000007<", "7408122F1204159UTO<<<<<<<<<<<8")]
   [InlineData(_crlf,           "I<UTOD23145890<0000000000007<<", "7408122F1204159UTO<<<<<<<<<<<6")]
   [InlineData(_lf,             "I<UTOD23145890<000000000007<<<", "7408122F1204159UTO<<<<<<<<<<<0")]
   [InlineData(_emptySeparator, "I<UTOD23145890<00000000007<<<<", "7408122F1204159UTO<<<<<<<<<<<8")]
   [InlineData(_crlf,           "I<UTOD23145890<0000000007<<<<<", "7408122F1204159UTO<<<<<<<<<<<6")]
   [InlineData(_lf,             "I<UTOD23145890<000000007<<<<<<", "7408122F1204159UTO<<<<<<<<<<<0")]
   [InlineData(_emptySeparator, "I<UTOD23145890<00000007<<<<<<<", "7408122F1204159UTO<<<<<<<<<<<8")]
   [InlineData(_crlf,           "I<UTOD23145890<0000007<<<<<<<<", "7408122F1204159UTO<<<<<<<<<<<6")]
   [InlineData(_lf,             "I<UTOD23145890<000007<<<<<<<<<", "7408122F1204159UTO<<<<<<<<<<<0")]
   [InlineData(_emptySeparator, "I<UTOD23145890<00007<<<<<<<<<<", "7408122F1204159UTO<<<<<<<<<<<8")]
   [InlineData(_crlf,           "I<UTOD23145890<0007<<<<<<<<<<<", "7408122F1204159UTO<<<<<<<<<<<6")]
   [InlineData(_lf,             "I<UTOD23145890<007<<<<<<<<<<<<", "7408122F1204159UTO<<<<<<<<<<<0")]
   [InlineData(_emptySeparator, "I<UTOD23145890<07<<<<<<<<<<<<<", "7408122F1204159UTO<<<<<<<<<<<8")]
   [InlineData(_emptySeparator, "I<UTOD23145890<AB112234566<<<<", "7408122F1204159UTO<<<<<<<<<<<4")]
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnTrue_WhenValueContainsExtendedDocumentNumberWithValidCheckDigits(
      String lineSeparator,
      String mrzFirstLine,
      String mrzSecondLine)
   {
      // Arrange.
      var value = GetTestValue(lineSeparator, mrzFirstLine, mrzSecondLine);

      // Act/assert.
      _sut.Validate(value).Should().BeTrue();
   }

   [Theory]
   [InlineData(_emptySeparator, "I<UTOD2314589A7<<<<<<<<<<<<<<<", "7408122F1204159UTO<<<<<<<<<<<6")]  // D231458907 with single char transcription error (0 -> A) with delta 10
   [InlineData(_crlf, "I<UTON2314589A7<<<<<<<<<<<<<<<", "7408122F1204159UTO<<<<<<<<<<<6")]  // D231458907 with single char transcription error (D -> N) with delta 10
   [InlineData(_emptySeparator, "I<UTOL89890C236<<<<<<<<<<<<<<<", "7408122F1204159UTO<<<<<<<<<<<8")]  // L898902C36 with two char transposition error (2C -> C2) with delta 10
   [InlineData(_lf,   "I<UTO0000000000<<<<<<<<<<<<<<<", "8812728F0000000UTO<<<<<<<<<<<0")]  // 8812278 with two char transposition error (27 -> 72) with delta 5
   [InlineData(_emptySeparator, "I<UTO0000000000<<<<<<<<<<<<<<<", "0000000F8812728UTO<<<<<<<<<<<2")]  // 8812278 with two char transposition error (27 -> 72) with delta 5
   [InlineData(_crlf, "I<UTOD23145890<0B112234566<<<<", "7408122F1204159UTO<<<<<<<<<<<4")]  // Extended document number AB112234566 with single char transcription error (A -> 0) with delta 10
   [InlineData(_lf,   "I<UTOD23145890<AB112283568<<<<", "7408122F1204159UTO<<<<<<<<<<<2")]  // Extended document number AB112238568 with two char transposition error (38 -> 83) with delta 5
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnTrue_WhenValueContainsUndetectableError(
      String lineSeparator,
      String mrzFirstLine,
      String mrzSecondLine)
   {
      // Arrange.
      var value = GetTestValue(lineSeparator, mrzFirstLine, mrzSecondLine);

      // Act/assert.
      _sut.Validate(value).Should().BeTrue();
   }

   public static TheoryData<String, String, String> DetectableErrorData = new()
   {
      { _emptySeparator, "I<UTOE231458907<<<<<<<<<<<<<<<", _mrzSecondLine },     // D231458907 with single character transcription error (D -> E)
      { _emptySeparator, "I<UTOD241458907<<<<<<<<<<<<<<<", _mrzSecondLine },     // D231458907 with single digit transcription error (3 -> 4)
      { _crlf,           _mrzFirstLine, "7409122F1204159UTO<<<<<<<<<<<6" },      // 7408122 with single digit transcription error (8 -> 9)
      { _crlf,           _mrzFirstLine, "7409122F1203159UTO<<<<<<<<<<<6" },      // 1204159 with single digit transcription error (4 -> 3)
      { _emptySeparator, "I<UTO2D31458907<<<<<<<<<<<<<<<", _mrzSecondLine },     // D231458907 with two character transposition error (D2 -> 2D)
      { _emptySeparator, "I<UTOD231548907<<<<<<<<<<<<<<<", _mrzSecondLine },     // D231458907 with two digit transposition error (45 -> 54)
      { _crlf,           _mrzFirstLine, "7408212F1204159UTO<<<<<<<<<<<6" },      // 7408122 with two digit transposition error (12 -> 21)
      { _crlf,           _mrzFirstLine, "7409122F1201459UTO<<<<<<<<<<<6" },      // 1204159 with two digit transposition error (41 -> 14)
      { _emptySeparator, "I<UTOD23145890<AC112234566<<<<", "7408122F1204159UTO<<<<<<<<<<<4" },     // Extended document number AB112234566 with single character transcription error (B -> C)
   };

   [Theory]
   [MemberData(nameof(DetectableErrorData))]
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnFalse_WhenValueContainsDetectableError(
      String lineSeparator,
      String mrzFirstLine,
      String mrzSecondLine)
   {
      // Arrange.
      var value = GetTestValue(lineSeparator, mrzFirstLine, mrzSecondLine);

      // Act/assert.
      _sut.Validate(value).Should().BeFalse();
   }

   [Fact]
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnFalse_WhenFieldCheckDigitsAreValidButCompositeCheckDigitIsNotValid()
   {
      // Arrange.
      var value = _mrzThirdLine + _mrzSecondLine[..29] + "8" + _mrzThirdLine;

      // Act/assert.
      _sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   [InlineData(_emptySeparator, "I<UTO0000000000<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<0")]
   [InlineData(_crlf,           "I<UTO000000000<00000000000000<", "0000000F0000000UTO<<<<<<<<<<<0")]
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnTrue_WhenFieldsAreAllZeros(
      String lineSeparator,
      String mrzFirstLine,
      String mrzSecondLine)
   {
      // Arrange.
      var value = GetTestValue(lineSeparator, mrzFirstLine, mrzSecondLine);

      // Act/assert.
      _sut.Validate(value).Should().BeTrue();
   }

   [Fact]
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnTrue_WhenFieldsAreAllFillerCharacters()
   {
      // Arrange.
      var value = GetTestValue(
         _emptySeparator,
         "I<UTO<<<<<<<<<0<<<<<<<<<<<<<<<",
         "<<<<<<0F<<<<<<0UTO<<<<<<<<<<<0");

      // Act/assert.
      _sut.Validate(value).Should().BeTrue();
   }

   public static TheoryData<String, String, String> InvalidCharacterData = new()
   {
      { _emptySeparator, "I<UTOb231458907<<<<<<<<<<<<<<<", _mrzSecondLine },     // D231458907 with D replaced with character 30 positions later in ASCII table
      { _crlf,           "I<UTOD2314589&7<<<<<<<<<<<<<<<", _mrzSecondLine },     // D231458907 with 0 replaced with character 10 positions before in ASCII table
      { _lf,             "I<UTOD2314589:7<<<<<<<<<<<<<<<", _mrzSecondLine },     // D231458907 with 0 replaced with character 10 positions later in ASCII table
      { _emptySeparator, _mrzFirstLine, "7>08122F1204159UTO<<<<<<<<<<<6" },      // 7408122 with 4 replaced with character 10 positions later in ASCII table
      { _emptySeparator, "I<UTOD23145890<A&112234566<<<<", "7408122F1204159UTO<<<<<<<<<<<4" },     // Extended document number AB112234566 with B replaced by invalid character
      { _emptySeparator, "I<UTOD23145890<A:112234566<<<<", "7408122F1204159UTO<<<<<<<<<<<4" },     // Extended document number AB112234566 with B replaced by invalid character
      { _emptySeparator, "I<UTOD23145890<A[112234566<<<<", "7408122F1204159UTO<<<<<<<<<<<4" },     // Extended document number AB112234566 with B replaced by invalid character
   };

   [Theory]
   [MemberData(nameof(InvalidCharacterData))]
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnFalse_WhenFieldContainsInvalidCharacter(
      String lineSeparator,
      String mrzFirstLine,
      String mrzSecondLine)
   {
      // Arrange.
      var value = GetTestValue(lineSeparator, mrzFirstLine, mrzSecondLine);

      // Act/assert.
      _sut.Validate(value).Should().BeFalse();
   }

   public static TheoryData<String, String, String> InvalidCheckDigitCharacterData = new()
   {
      { _emptySeparator, "I<UTOD23145890A<<<<<<<<<<<<<<<", _mrzSecondLine },     // Document number check digit is invalid character 
      { _emptySeparator, "I<UTOD23145890&<<<<<<<<<<<<<<<", _mrzSecondLine },     // Document number check digit is invalid character
      { _emptySeparator, "I<UTOD23145890:<<<<<<<<<<<<<<<", _mrzSecondLine },     // Document number check digit is invalid character
      { _crlf,           _mrzFirstLine, "740812AF1204159UTO<<<<<<<<<<<6" },      // Date of birth check digit is invalid character
      { _crlf,           _mrzFirstLine, "740812&F1204159UTO<<<<<<<<<<<6" },      // Date of birth check digit is invalid character
      { _crlf,           _mrzFirstLine, "740812<F1204159UTO<<<<<<<<<<<6" },      // Date of birth check digit is invalid character
      { _crlf,           _mrzFirstLine, "740812[F1204159UTO<<<<<<<<<<<6" },      // Date of birth check digit is invalid character
      { _lf,             _mrzFirstLine, "7408122F120415AUTO<<<<<<<<<<<6" },      // Date of expiry check digit is invalid character
      { _lf,             _mrzFirstLine, "7408122F120415&UTO<<<<<<<<<<<6" },      // Date of expiry check digit is invalid character
      { _lf,             _mrzFirstLine, "7408122F120415<UTO<<<<<<<<<<<6" },      // Date of expiry check digit is invalid character
      { _lf,             _mrzFirstLine, "7408122F120415[UTO<<<<<<<<<<<6" },      // Date of expiry check digit is invalid character
      { _emptySeparator, "I<UTOD23145890<AB11223456A<<<<", "7408122F1204159UTO<<<<<<<<<<<4" },     // Extended document number check digit with invalid character
      { _emptySeparator, "I<UTOD23145890<AB11223456&<<<<", "7408122F1204159UTO<<<<<<<<<<<4" },     // Extended document number check digit with invalid character
      { _emptySeparator, "I<UTOD23145890<AB11223456<<<<<", "7408122F1204159UTO<<<<<<<<<<<4" },     // Extended document number check digit with invalid character
      { _emptySeparator, "I<UTOD23145890<AB11223456[<<<<", "7408122F1204159UTO<<<<<<<<<<<4" },     // Extended document number check digit with invalid character
   };

   [Theory]
   [MemberData(nameof(InvalidCheckDigitCharacterData))]
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnFalse_WhenRequiredFieldCheckDigitContainsNonDigitCharacter(
      String lineSeparator,
      String mrzFirstLine,
      String mrzSecondLine)
   {
      // Arrange.
      var value = GetTestValue(lineSeparator, mrzFirstLine, mrzSecondLine);

      // Act/assert.
      _sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   [InlineData("A")]
   [InlineData("&")]
   [InlineData("<")]
   [InlineData("[")]
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnFalse_WhenExtendedCheckDigitContainsNonDigitCharacter(String extendedCheckDigit)
   {
      // Arrange.
      var value = _mrzFirstLine + _mrzSecondLine[..29] + extendedCheckDigit + _mrzThirdLine;

      // Act/assert.
      _sut.Validate(value).Should().BeFalse();
   }

   #endregion
}
