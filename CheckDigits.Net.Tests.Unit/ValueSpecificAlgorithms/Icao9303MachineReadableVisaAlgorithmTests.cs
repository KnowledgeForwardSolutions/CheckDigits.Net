// Ignore Spelling: Icao Mrz Crlf

namespace CheckDigits.Net.Tests.Unit.ValueSpecificAlgorithms;

public class Icao9303MachineReadableVisaAlgorithmTests
{
   private readonly Icao9303MachineReadableVisaAlgorithm _sut = new();

   // Example MRZ from https://www.icao.int/publications/Documents/9303_p7_cons_en.pdf
   private const String _fmtAMrzFirstLine = "V<UTOERIKSSON<<ANNA<MARIA<<<<<<<<<<<<<<<<<<<";
   private const String _fmtAMrzSecondLine = "L898902C<3UTO6908061F9406236ZE184226B<<<<<<<";
   private const String _fmtBMrzFirstLine = "V<UTOERIKSSON<<ANNA<MARIA<<<<<<<<<<<";
   private const String _fmtBMrzSecondLine = "L898902C<3UTO6908061F9406236ZE184226";
   private const String _emptySeparator = "";
   private const String _crlf = "\r\n";
   private const String _lf = "\n";

   public enum MrzFormat { A, B }

   private static String GetTestValue(
      MrzFormat format = MrzFormat.A,
      String separatorChars = _emptySeparator,
#if !NET48
      String? firstLine = null,
      String? secondLine = null)
#else
      String firstLine = null,
      String secondLine = null)
#endif
      => (firstLine ?? MrzFirstLine(format))
         + separatorChars + (secondLine ?? MrzSecondLine(format));

   private static String MrzFirstLine(MrzFormat format)
      => format switch
      {
         MrzFormat.A => _fmtAMrzFirstLine,
         MrzFormat.B => _fmtBMrzFirstLine,
         _ => throw new ArgumentOutOfRangeException(),
      };

   private static String MrzSecondLine(MrzFormat format)
      => format switch
      {
         MrzFormat.A => _fmtAMrzSecondLine,
         MrzFormat.B => _fmtBMrzSecondLine,
         _ => throw new ArgumentOutOfRangeException(),
      };

   #region AlgorithmDescription Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Icao9303MachineReadableVisaAlgorithm_AlgorithmDescription_ShouldReturnExpectedValue()
      => _sut.AlgorithmDescription.Should().Be(Resources.Icao9303MachineReadableVisaAlgorithmDescription);

   #endregion

   #region AlgorithmName Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Icao9303MachineReadableVisaAlgorithm_AlgorithmName_ShouldReturnExpectedValue()
      => _sut.AlgorithmName.Should().Be(Resources.Icao9303MachineReadableVisaAlgorithmName);

   #endregion

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Icao9303MachineReadableVisaAlgorithm_Validate_ShouldReturnFalse_WhenInputIsNull()
      => _sut.Validate(null!).Should().BeFalse();

   [Fact]
   public void Icao9303MachineReadableVisaAlgorithm_Validate_ShouldReturnFalse_WhenInputIsEmpty()
      => _sut.Validate(String.Empty).Should().BeFalse();

   public static TheoryData<MrzFormat, String, String, String> InvalidLengthData => new()
   {
      { MrzFormat.A, _emptySeparator, _fmtAMrzFirstLine[..35], _fmtBMrzSecondLine },
      { MrzFormat.A, _crlf,           _fmtAMrzFirstLine[..35], "<" + _fmtBMrzSecondLine },
      { MrzFormat.B, _emptySeparator, _fmtBMrzFirstLine + "<<", _fmtBMrzSecondLine },
      { MrzFormat.B, _crlf,           _fmtBMrzFirstLine + "<<", _fmtBMrzSecondLine },
      { MrzFormat.B, _lf,             _fmtBMrzFirstLine + "<<", _fmtBMrzSecondLine },
   };

   [Theory]
   [MemberData(nameof(InvalidLengthData))]
   public void Icao9303MachineReadableVisaAlgorithm_Validate_ShouldReturnFalse_WhenValueIsInvalidLength(
      MrzFormat format,
      String lineSeparator,
      String firstLine,
      String secondLine)
   {
      // Arrange.
      var value = GetTestValue(format, lineSeparator, firstLine, secondLine);

      // Act/assert.
      _sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   [InlineData(_fmtAMrzFirstLine + "X\n" + _fmtAMrzSecondLine)]  // 'X' instead of \r
   [InlineData(_fmtBMrzFirstLine + " \n" + _fmtBMrzSecondLine)]  // Space instead of \r
   [InlineData(_fmtAMrzFirstLine + "\r " + _fmtAMrzSecondLine)]  // Space instead of \n
   [InlineData(_fmtBMrzFirstLine + "\n\r" + _fmtBMrzSecondLine)] // \n\r instead of \r\n
   public void Icao9303MachineReadableVisaAlgorithm_Validate_ShouldReturnFalse_WhenSeparatorCharactersAreInvalid(String value)
      => _sut.Validate(value).Should().BeFalse();

   /// <summary>
   ///   Test data for edge cases where separator validation cannot detect certain 
   ///   issues due to length ambiguity. For example, when the first line is 
   ///   shortened by exactly one character, a CRLF separator's LF character falls 
   ///   at the position where an LF-only separator would be expected, making the 
   ///   error undetectable by length validation alone.
   /// </summary>
   public static TheoryData<MrzFormat, String, String, String> UndetectableSeparatorIssuesData => new()
   {
      { MrzFormat.A, _crlf,           _fmtAMrzFirstLine[..44], _fmtAMrzSecondLine },      // Length indicates Lf only and Lf falls in correct position
      { MrzFormat.A, _lf,             _fmtAMrzFirstLine[..44], _fmtAMrzSecondLine },      // Length indicates no separator so separator chars not checked
      { MrzFormat.A, _crlf,           _fmtBMrzFirstLine[..35], _fmtBMrzSecondLine },      // Length indicates Lf only and Lf falls in correct position
      { MrzFormat.B, _lf,             _fmtBMrzFirstLine[..35], _fmtBMrzSecondLine },      // Length indicates no separator so separator chars not checked
   };

   [Theory]
   [InlineData(71)]  // One character short of MRV-B with no separator
   [InlineData(73)]  // One character over MRV-B with no separator
   [InlineData(87)]  // One character short of MRV-A with no separator
   [InlineData(89)]  // One character over MRV-A with no separator
   public void Icao9303MachineReadableVisaAlgorithm_Validate_ShouldReturnFalse_WhenLengthIsNearButInvalid(Int32 length)
   {
      var value = new String('0', length);
      _sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   [MemberData(nameof(UndetectableSeparatorIssuesData))]
   public void Icao9303MachineReadableVisaAlgorithm_Validate_ShouldReturnTrue_WhenUndetectableInvalidSeparator(
      MrzFormat format,
      String lineSeparator,
      String firstLine,
      String secondLine)
   {
      // Arrange.
      var value = GetTestValue(format, lineSeparator, firstLine, secondLine);

      // Act/assert.
      _sut.Validate(value).Should().BeTrue();
   }

   [Theory]
   [InlineData(MrzFormat.B, _emptySeparator, "0000000000UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, _emptySeparator, "0010000001UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, _emptySeparator, "0020000002UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, _emptySeparator, "0030000003UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, _emptySeparator, "0040000004UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, _emptySeparator, "0050000005UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, _emptySeparator, "0060000006UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, _emptySeparator, "0070000007UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, _emptySeparator, "0080000008UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, _emptySeparator, "0090000009UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, _crlf, "00A0000000UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, _crlf, "00B0000001UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, _crlf, "00C0000002UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, _crlf, "00D0000003UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, _crlf, "00E0000004UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, _crlf, "00F0000005UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, _crlf, "00G0000006UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, _crlf, "00H0000007UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, _crlf, "00I0000008UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, _crlf, "00J0000009UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, _lf, "00K0000000UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, _lf, "00L0000001UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, _lf, "00M0000002UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, _lf, "00N0000003UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, _lf, "00O0000004UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, _lf, "00P0000005UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, _lf, "00Q0000006UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, _lf, "00R0000007UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, _lf, "00S0000008UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, _lf, "00T0000009UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.A, _emptySeparator, "00U0000000UTO0000000F0000000<<<<<<<<<<<<<<<<")]
   [InlineData(MrzFormat.A, _emptySeparator, "00V0000001UTO0000000F0000000<<<<<<<<<<<<<<<<")]
   [InlineData(MrzFormat.A, _emptySeparator, "00W0000002UTO0000000F0000000<<<<<<<<<<<<<<<<")]
   [InlineData(MrzFormat.A, _emptySeparator, "00X0000003UTO0000000F0000000<<<<<<<<<<<<<<<<")]
   [InlineData(MrzFormat.A, _emptySeparator, "00Y0000004UTO0000000F0000000<<<<<<<<<<<<<<<<")]
   [InlineData(MrzFormat.A, _emptySeparator, "00Z0000005UTO0000000F0000000<<<<<<<<<<<<<<<<")]
   [InlineData(MrzFormat.A, _emptySeparator, "00<0000000UTO0000000F0000000<<<<<<<<<<<<<<<<")]
   public void Icao9303MachineReadableVisaAlgorithm_Validate_ShouldCorrectlyMapFieldCharacterValues(
      MrzFormat format,
      String lineSeparator,
      String mrzSecondLine)
   {
      // Arrange.
      var value = GetTestValue(format, lineSeparator, null, mrzSecondLine);

      // Act/assert.
      _sut.Validate(value).Should().BeTrue();
   }

   [Theory]
   // Document number field
   [InlineData(MrzFormat.A, _emptySeparator, "1000000007UTO0000000F0000000<<<<<<<<<<<<<<<<")]
   [InlineData(MrzFormat.A, _emptySeparator, "0100000003UTO0000000F0000000<<<<<<<<<<<<<<<<")]
   [InlineData(MrzFormat.A, _emptySeparator, "0010000001UTO0000000F0000000<<<<<<<<<<<<<<<<")]
   [InlineData(MrzFormat.A, _emptySeparator, "0001000007UTO0000000F0000000<<<<<<<<<<<<<<<<")]
   [InlineData(MrzFormat.A, _emptySeparator, "0000100003UTO0000000F0000000<<<<<<<<<<<<<<<<")]
   [InlineData(MrzFormat.B, _emptySeparator, "0000010001UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, _emptySeparator, "0000001007UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, _emptySeparator, "0000000103UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, _emptySeparator, "0000000011UTO0000000F0000000<<<<<<<<")]
   // Date of birth field
   [InlineData(MrzFormat.A, _crlf, "0000000000UTO1000007F0000000<<<<<<<<<<<<<<<<")]
   [InlineData(MrzFormat.A, _crlf, "0000000000UTO0100003F0000000<<<<<<<<<<<<<<<<")]
   [InlineData(MrzFormat.A, _crlf, "0000000000UTO0010001F0000000<<<<<<<<<<<<<<<<")]
   [InlineData(MrzFormat.B, _crlf, "0000000000UTO0001007F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, _crlf, "0000000000UTO0000103F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, _crlf, "0000000000UTO0000011F0000000<<<<<<<<")]
   // Date of expiry field
   [InlineData(MrzFormat.A, _lf, "0000000000UTO0000000F1000007<<<<<<<<<<<<<<<<")]
   [InlineData(MrzFormat.A, _lf, "0000000000UTO0000000F0100003<<<<<<<<<<<<<<<<")]
   [InlineData(MrzFormat.A, _lf, "0000000000UTO0000000F0010001<<<<<<<<<<<<<<<<")]
   [InlineData(MrzFormat.B, _lf, "0000000000UTO0000000F0001007<<<<<<<<")]
   [InlineData(MrzFormat.B, _lf, "0000000000UTO0000000F0000103<<<<<<<<")]
   [InlineData(MrzFormat.B, _lf, "0000000000UTO0000000F0000011<<<<<<<<")]
   public void Icao9303MachineReadableVisaAlgorithm_Validate_ShouldCorrectlyWeightByCharacterPosition(
      MrzFormat format,
      String lineSeparator,
      String mrzSecondLine)
   {
      // Arrange.
      var value = GetTestValue(format, lineSeparator, null, mrzSecondLine);

      // Act/assert.
      _sut.Validate(value).Should().BeTrue();
   }

   [Theory]
   [InlineData(MrzFormat.A, _emptySeparator)]
   [InlineData(MrzFormat.A, _crlf)]
   [InlineData(MrzFormat.A, _lf)]
   [InlineData(MrzFormat.B, _emptySeparator)]
   [InlineData(MrzFormat.B, _crlf)]
   [InlineData(MrzFormat.B, _lf)]
   public void Icao9303MachineReadableVisaAlgorithm_Validate_ShouldReturnTrue_WhenValueContainsValidCheckDigits(
      MrzFormat format,
      String lineSeparator)
   {
      // Arrange.
      var value = GetTestValue(format, lineSeparator);

      // Act/assert.
      _sut.Validate(value).Should().BeTrue();
   }

   [Theory]
   [InlineData(MrzFormat.A, _emptySeparator, "D2314589A7UTO7408122F1204159<<<<<<<<<<<<<<<<")]   // D231458907 with single char transcription error (0 -> A) with delta 10   
   [InlineData(MrzFormat.B, _crlf, "N231458907UTO7408122F1204159<<<<<<<<")]           // D231458907 with single char transcription error (D -> N) with delta 10
   [InlineData(MrzFormat.B, _lf,   "L89890C236UTO7408122F1204159<<<<<<<<")]           // L898902C36 with two char transposition error (2C -> C2) with delta 10
   [InlineData(MrzFormat.B, _emptySeparator, "0000000000UTO8812728F0000000<<<<<<<<")]           // 8812278 with two char transposition error (27 -> 72) with delta 5
   [InlineData(MrzFormat.A, _crlf, "0000000000UTO0000000F8812728<<<<<<<<<<<<<<<<")]   // 8812278 with two char transposition error (27 -> 72) with delta 5
   public void Icao9303MachineReadableVisaAlgorithm_Validate_ShouldReturnTrue_WhenValueContainsUndetectableError(
      MrzFormat format,
      String lineSeparator,
      String mrzSecondLine)
   {
      // Arrange.
      var value = GetTestValue(format, lineSeparator, null, mrzSecondLine);

      // Act/assert.
      _sut.Validate(value).Should().BeTrue();
   }

   [Theory]
   [InlineData(MrzFormat.A, _emptySeparator, "E231458907UTO7408122F1204159<<<<<<<<<<<<<<<<")]   // D231458907 with single character transcription error (D -> E)
   [InlineData(MrzFormat.A, _crlf, "D241458907UTO7408122F1204159<<<<<<<<<<<<<<<<")]   // D231458907 with single digit transcription error (3 -> 4)
   [InlineData(MrzFormat.A, _lf,   "D231458907UTO7409122F1204159<<<<<<<<<<<<<<<<")]   // 7408122 with single digit transcription error (8 -> 9)
   [InlineData(MrzFormat.A, _emptySeparator, "D231458907UTO7408122F1203159<<<<<<<<<<<<<<<<")]   // 1204159 with single digit transcription error (4 -> 3)
   [InlineData(MrzFormat.B, _crlf, "2D31458907UTO7408122F1204159<<<<<<<<")]           // D231458907 with two character transposition error (D2 -> 2D)
   [InlineData(MrzFormat.B, _lf,   "D231548907UTO7408122F1204159<<<<<<<<")]           // D231458907 with two digit transposition error (45 -> 54)
   [InlineData(MrzFormat.B, _emptySeparator, "D231458907UTO7408212F1204159<<<<<<<<")]           // 7408122 with two digit transposition error (12 -> 21)
   [InlineData(MrzFormat.B, _crlf, "D231458907UTO7409122F1201459<<<<<<<<")]           // 1204159 with two digit transposition error (41 -> 14)
   public void Icao9303MachineReadableVisaAlgorithm_Validate_ShouldReturnFalse_WhenValueContainsDetectableError(
      MrzFormat format,
      String lineSeparator,
      String mrzSecondLine)
   {
      // Arrange.
      var value = GetTestValue(format, lineSeparator, null, mrzSecondLine);

      // Act/assert.
      _sut.Validate(value).Should().BeFalse();
   }

   [Fact]
   public void Icao9303MachineReadableVisaAlgorithm_Validate_ShouldReturnFalse_WhenMultipleFieldsContainInvalidCharacters()
   {
      // Arrange.
      var value = GetTestValue(
         MrzFormat.A,
         _emptySeparator,
         null,
         "D23145890!UTO740812@F120415#<<<<<<<<<<<<<<<<");

      // Act/assert.
      _sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   [InlineData(MrzFormat.A, _emptySeparator, "0000000000UTO0000000F0000000<<<<<<<<<<<<<<<<")]
   [InlineData(MrzFormat.B, _crlf, "0000000000UTO0000000F0000000<<<<<<<<")]
   public void Icao9303MachineReadableVisaAlgorithm_Validate_ShouldReturnTrue_WhenFieldsAreAllZeros(
      MrzFormat format,
      String lineSeparator,
      String mrzSecondLine)
   {
      // Arrange.
      var value = GetTestValue(format, lineSeparator, null, mrzSecondLine);

      // Act/assert.
      _sut.Validate(value).Should().BeTrue();
   }

   [Theory]
   [InlineData(MrzFormat.A, _emptySeparator, "<<<<<<<<<0UTO<<<<<<0F<<<<<<0<<<<<<<<<<<<<<<<")]
   [InlineData(MrzFormat.B, _crlf, "<<<<<<<<<0UTO<<<<<<0F<<<<<<0<<<<<<<<")]
   public void Icao9303MachineReadableVisaAlgorithm_Validate_ShouldReturnTrue_WhenFieldsAreAllFillerCharacters(
      MrzFormat format,
      String lineSeparator,
      String mrzSecondLine)
   {
      // Arrange.
      var value = GetTestValue(format, lineSeparator, null, mrzSecondLine);

      // Act/assert.
      _sut.Validate(value).Should().BeTrue();
   }

   [Theory]
   [InlineData(MrzFormat.A, _emptySeparator, "b231458907UTO7408122F1204159<<<<<<<<<<<<<<<<")]   // D231458907 with D replaced with character 30 positions later in ASCII table
   [InlineData(MrzFormat.A, _crlf, "D2314589&7UTO7408122F1204159<<<<<<<<<<<<<<<<")]   // D231458907 with 0 replaced with character 10 positions before in ASCII table
   [InlineData(MrzFormat.B, _lf,   "D2314589:7UTO7408122F1204159<<<<<<<<")]           // D231458907 with 0 replaced with character 10 positions later in ASCII table
   [InlineData(MrzFormat.B, _emptySeparator, "D231458907UTO7>08122F1204159<<<<<<<<")]           // 7408122 with 4 replaced with character 10 positions later in ASCII table
   public void Icao9303MachineReadableVisaAlgorithm_Validate_ShouldReturnFalse_WhenFieldContainsInvalidCharacter(
      MrzFormat format,
      String lineSeparator,
      String mrzSecondLine)
   {
      // Arrange.
      var value = GetTestValue(format, lineSeparator, null, mrzSecondLine);

      // Act/assert.
      _sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   [InlineData(MrzFormat.A, _emptySeparator, "D23145890AUTO7408122F1204159<<<<<<<<<<<<<<<<")]   // Document number check digit is invalid character
   [InlineData(MrzFormat.A, _emptySeparator, "D23145890&UTO7408122F1204159<<<<<<<<<<<<<<<<")]   // Document number check digit is invalid character
   [InlineData(MrzFormat.A, _emptySeparator, "D23145890:UTO7408122F1204159<<<<<<<<<<<<<<<<")]   // Document number check digit is invalid character
   [InlineData(MrzFormat.A, _crlf, "D231458907UTO740812AF1204159<<<<<<<<<<<<<<<<")]   // Date of birth check digit is invalid character
   [InlineData(MrzFormat.A, _crlf, "D231458907UTO740812&F1204159<<<<<<<<<<<<<<<<")]   // Date of birth check digit is invalid character
   [InlineData(MrzFormat.A, _crlf, "D231458907UTO740812<F1204159<<<<<<<<<<<<<<<<")]   // Date of birth check digit is invalid character
   [InlineData(MrzFormat.A, _crlf, "D231458907UTO740812[F1204159<<<<<<<<<<<<<<<<")]   // Date of birth check digit is invalid character
   [InlineData(MrzFormat.B, _lf,   "D231458907UTO7408122F120415A<<<<<<<<")]           // Date of expiry check digit is invalid character
   [InlineData(MrzFormat.B, _lf,   "D231458907UTO7408122F120415&<<<<<<<<")]           // Date of expiry check digit is invalid character
   [InlineData(MrzFormat.B, _lf,   "D231458907UTO7408122F120415<<<<<<<<<")]           // Date of expiry check digit is invalid character
   [InlineData(MrzFormat.B, _lf,   "D231458907UTO7408122F120415[<<<<<<<<")]           // Date of expiry check digit is invalid character
   public void Icao9303MachineReadableVisaAlgorithm_Validate_ShouldReturnFalse_WhenRequiredFieldCheckDigitContainsNonDigitCharacter(
      MrzFormat format,
      String lineSeparator,
      String mrzSecondLine)
   {
      // Arrange.
      var value = GetTestValue(format, lineSeparator, null, mrzSecondLine);

      // Act/assert.
      _sut.Validate(value).Should().BeFalse();
   }

   #endregion
}
