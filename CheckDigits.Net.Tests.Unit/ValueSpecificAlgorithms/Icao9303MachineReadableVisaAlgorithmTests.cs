// Ignore Spelling: Icao Mrz

namespace CheckDigits.Net.Tests.Unit.ValueSpecificAlgorithms;

public class Icao9303MachineReadableVisaAlgorithmTests
{
   private readonly Icao9303MachineReadableVisaAlgorithm _sut = new();

   // Example MRZ from https://www.icao.int/publications/Documents/9303_p7_cons_en.pdf
   private const String _fmtAMrzFirstLine = "V<UTOERIKSSON<<ANNA<MARIA<<<<<<<<<<<<<<<<<<<";
   private const String _fmtAMrzSecondLine = "L898902C<3UTO6908061F9406236ZE184226B<<<<<<<";
   private const String _fmtBMrzFirstLine = "V<UTOERIKSSON<<ANNA<MARIA<<<<<<<<<<<";
   private const String _fmtBMrzSecondLine = "L898902C<3UTO6908061F9406236ZE184226";

   public enum MrzFormat { A, B }

   private static String GetTestValue(
      MrzFormat format = MrzFormat.A,
      LineSeparator lineSeparator = LineSeparator.None,
#if !NET48
      String? firstLine = null,
      String? secondLine = null)
#else
      String firstLine = null,
      String secondLine = null)
#endif
   {
      var separatorChars = lineSeparator switch
      {
         LineSeparator.Crlf => "\r\n",
         LineSeparator.Lf => "\n",
         _ => String.Empty,
      };

      return (firstLine ?? MrzFirstLine(format))
         + separatorChars + (secondLine ?? MrzSecondLine(format));
   }

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

   #region LineSeparator Property Tests
   // ==========================================================================
   // ==========================================================================

   public static TheoryData<LineSeparator> LineSeparatorValues
   {
      get
      {
         var data = new TheoryData<LineSeparator>();
         foreach (var value in Enum.GetValues(typeof(LineSeparator)))
         {
            data.Add((LineSeparator)value);
         }

         return data;
      }
   }

   [Theory]
   [MemberData(nameof(LineSeparatorValues))]
   public void Icao9302MachineReadableVisaAlgorithm_LineSeparator_ShouldNotThrow_ValueIsValid(LineSeparator lineSeparator)
   {
      // Arrange.
      var act = () => _sut.LineSeparator = lineSeparator;

      // Act/assert.
      act.Should().NotThrow();
   }

   [Theory]
   [MemberData(nameof(LineSeparatorValues))]
   public void Icao9303MachineReadableVisaAlgorithm_LineSeparator_ShouldReturnExpectedValueAfterSetting(LineSeparator lineSeparator)
   {
      // Arrange.
      var sut = new Icao9303MachineReadableVisaAlgorithm() { LineSeparator = lineSeparator };

      // Act/assert.
      sut.LineSeparator.Should().Be(lineSeparator);
   }

   [Fact]
   public void Icao9302MachineReadableVisaAlgorithm_LineSeparator_ShouldThrowArgumentOutOfRangeException_ValueIsInvalid()
   {
      // Arrange.
      var value = (LineSeparator)(-1);
      var act = () => _sut.LineSeparator = value;
      var expectedMessage = Resources.LineSeparatorInvalidValueMessage;

      // Act/assert.
      act.Should().ThrowExactly<ArgumentOutOfRangeException>()
         .WithParameterName(nameof(value))
         .WithMessage(expectedMessage + "*")
         .And.ActualValue.Should().Be(value);
   }

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

   public static TheoryData<MrzFormat, LineSeparator, String, String> InvalidLengthData => new()
   {
      { MrzFormat.A, LineSeparator.None, _fmtAMrzFirstLine[..35], _fmtBMrzSecondLine },
      { MrzFormat.A, LineSeparator.Crlf, _fmtAMrzFirstLine[..35], _fmtBMrzSecondLine },
      { MrzFormat.A, LineSeparator.Lf,   _fmtAMrzFirstLine[..35], _fmtBMrzSecondLine },
      { MrzFormat.A, LineSeparator.None, _fmtAMrzFirstLine[..35], _fmtBMrzSecondLine },
      { MrzFormat.A, LineSeparator.Crlf, _fmtAMrzFirstLine[..35], _fmtBMrzSecondLine },
      { MrzFormat.A, LineSeparator.Lf,   _fmtAMrzFirstLine[..35], _fmtBMrzSecondLine },
      { MrzFormat.B, LineSeparator.None, _fmtBMrzFirstLine + "<<", _fmtBMrzSecondLine },
      { MrzFormat.B, LineSeparator.Crlf, _fmtBMrzFirstLine + "<<", _fmtBMrzSecondLine },
      { MrzFormat.B, LineSeparator.Lf,   _fmtBMrzFirstLine + "<<", _fmtBMrzSecondLine },
      { MrzFormat.B, LineSeparator.None, _fmtBMrzFirstLine + "<<", _fmtBMrzSecondLine },
      { MrzFormat.B, LineSeparator.Crlf, _fmtBMrzFirstLine + "<<", _fmtBMrzSecondLine },
      { MrzFormat.B, LineSeparator.Lf,   _fmtBMrzFirstLine + "<<", _fmtBMrzSecondLine },
   };

   [Theory]
   [MemberData(nameof(InvalidLengthData))]
   public void Icao9303MachineReadableVisaAlgorithm_Validate_ShouldReturnFalse_WhenValueIsInvalidLength(
      MrzFormat format,
      LineSeparator lineSeparator,
      String firstLine,
      String secondLine)
   {
      // Arrange.
      var sut = new Icao9303MachineReadableVisaAlgorithm()
      {
         LineSeparator = lineSeparator,
      };
      var value = GetTestValue(format, lineSeparator, firstLine, secondLine);

      // Act/assert.
      sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   [InlineData(MrzFormat.B, LineSeparator.None, "0000000000UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, LineSeparator.None, "0010000001UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, LineSeparator.None, "0020000002UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, LineSeparator.None, "0030000003UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, LineSeparator.None, "0040000004UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, LineSeparator.None, "0050000005UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, LineSeparator.None, "0060000006UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, LineSeparator.None, "0070000007UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, LineSeparator.None, "0080000008UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, LineSeparator.None, "0090000009UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, LineSeparator.Crlf, "00A0000000UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, LineSeparator.Crlf, "00B0000001UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, LineSeparator.Crlf, "00C0000002UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, LineSeparator.Crlf, "00D0000003UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, LineSeparator.Crlf, "00E0000004UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, LineSeparator.Crlf, "00F0000005UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, LineSeparator.Crlf, "00G0000006UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, LineSeparator.Crlf, "00H0000007UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, LineSeparator.Crlf, "00I0000008UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, LineSeparator.Crlf, "00J0000009UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, LineSeparator.Lf, "00K0000000UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, LineSeparator.Lf, "00L0000001UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, LineSeparator.Lf, "00M0000002UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, LineSeparator.Lf, "00N0000003UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, LineSeparator.Lf, "00O0000004UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, LineSeparator.Lf, "00P0000005UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, LineSeparator.Lf, "00Q0000006UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, LineSeparator.Lf, "00R0000007UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, LineSeparator.Lf, "00S0000008UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, LineSeparator.Lf, "00T0000009UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.A, LineSeparator.None, "00U0000000UTO0000000F0000000<<<<<<<<<<<<<<<<")]
   [InlineData(MrzFormat.A, LineSeparator.None, "00V0000001UTO0000000F0000000<<<<<<<<<<<<<<<<")]
   [InlineData(MrzFormat.A, LineSeparator.None, "00W0000002UTO0000000F0000000<<<<<<<<<<<<<<<<")]
   [InlineData(MrzFormat.A, LineSeparator.None, "00X0000003UTO0000000F0000000<<<<<<<<<<<<<<<<")]
   [InlineData(MrzFormat.A, LineSeparator.None, "00Y0000004UTO0000000F0000000<<<<<<<<<<<<<<<<")]
   [InlineData(MrzFormat.A, LineSeparator.None, "00Z0000005UTO0000000F0000000<<<<<<<<<<<<<<<<")]
   [InlineData(MrzFormat.A, LineSeparator.None, "00<0000000UTO0000000F0000000<<<<<<<<<<<<<<<<")]
   public void Icao9303MachineReadableVisaAlgorithm_Validate_ShouldCorrectlyMapFieldCharacterValues(
      MrzFormat format,
      LineSeparator lineSeparator,
      String mrzSecondLine)
   {
      // Arrange.
      var sut = new Icao9303MachineReadableVisaAlgorithm()
      {
         LineSeparator = lineSeparator,
      };
      var value = GetTestValue(format, lineSeparator, null, mrzSecondLine);

      // Act/assert.
      sut.Validate(value).Should().BeTrue();
   }

   [Theory]
   // Document number field
   [InlineData(MrzFormat.A, LineSeparator.None, "1000000007UTO0000000F0000000<<<<<<<<<<<<<<<<")]
   [InlineData(MrzFormat.A, LineSeparator.None, "0100000003UTO0000000F0000000<<<<<<<<<<<<<<<<")]
   [InlineData(MrzFormat.A, LineSeparator.None, "0010000001UTO0000000F0000000<<<<<<<<<<<<<<<<")]
   [InlineData(MrzFormat.A, LineSeparator.None, "0001000007UTO0000000F0000000<<<<<<<<<<<<<<<<")]
   [InlineData(MrzFormat.A, LineSeparator.None, "0000100003UTO0000000F0000000<<<<<<<<<<<<<<<<")]
   [InlineData(MrzFormat.B, LineSeparator.None, "0000010001UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, LineSeparator.None, "0000001007UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, LineSeparator.None, "0000000103UTO0000000F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, LineSeparator.None, "0000000011UTO0000000F0000000<<<<<<<<")]
   // Date of birth field
   [InlineData(MrzFormat.A, LineSeparator.Crlf, "0000000000UTO1000007F0000000<<<<<<<<<<<<<<<<")]
   [InlineData(MrzFormat.A, LineSeparator.Crlf, "0000000000UTO0100003F0000000<<<<<<<<<<<<<<<<")]
   [InlineData(MrzFormat.A, LineSeparator.Crlf, "0000000000UTO0010001F0000000<<<<<<<<<<<<<<<<")]
   [InlineData(MrzFormat.B, LineSeparator.Crlf, "0000000000UTO0001007F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, LineSeparator.Crlf, "0000000000UTO0000103F0000000<<<<<<<<")]
   [InlineData(MrzFormat.B, LineSeparator.Crlf, "0000000000UTO0000011F0000000<<<<<<<<")]
   // Date of expiry field
   [InlineData(MrzFormat.A, LineSeparator.Lf, "0000000000UTO0000000F1000007<<<<<<<<<<<<<<<<")]
   [InlineData(MrzFormat.A, LineSeparator.Lf, "0000000000UTO0000000F0100003<<<<<<<<<<<<<<<<")]
   [InlineData(MrzFormat.A, LineSeparator.Lf, "0000000000UTO0000000F0010001<<<<<<<<<<<<<<<<")]
   [InlineData(MrzFormat.B, LineSeparator.Lf, "0000000000UTO0000000F0001007<<<<<<<<")]
   [InlineData(MrzFormat.B, LineSeparator.Lf, "0000000000UTO0000000F0000103<<<<<<<<")]
   [InlineData(MrzFormat.B, LineSeparator.Lf, "0000000000UTO0000000F0000011<<<<<<<<")]
   public void Icao9303MachineReadableVisaAlgorithm_Validate_ShouldCorrectlyWeightByCharacterPosition(
      MrzFormat format,
      LineSeparator lineSeparator,
      String mrzSecondLine)
   {
      // Arrange.
      var sut = new Icao9303MachineReadableVisaAlgorithm()
      {
         LineSeparator = lineSeparator,
      };
      var value = GetTestValue(format, lineSeparator, null, mrzSecondLine);

      // Act/assert.
      sut.Validate(value).Should().BeTrue();
   }

   [Theory]
   [InlineData(MrzFormat.A, LineSeparator.None)]
   [InlineData(MrzFormat.A, LineSeparator.Crlf)]
   [InlineData(MrzFormat.A, LineSeparator.Lf)]
   [InlineData(MrzFormat.B, LineSeparator.None)]
   [InlineData(MrzFormat.B, LineSeparator.Crlf)]
   [InlineData(MrzFormat.B, LineSeparator.Lf)]
   public void Icao9303MachineReadableVisaAlgorithm_Validate_ShouldReturnTrue_WhenValueContainsValidCheckDigits(
      MrzFormat format,
      LineSeparator lineSeparator)
   {
      // Arrange.
      var sut = new Icao9303MachineReadableVisaAlgorithm()
      {
         LineSeparator = lineSeparator,
      };
      var value = GetTestValue(format, lineSeparator);

      // Act/assert.
      sut.Validate(value).Should().BeTrue();
   }

   [Theory]
   [InlineData(MrzFormat.A, LineSeparator.None, "D2314589A7UTO7408122F1204159<<<<<<<<<<<<<<<<")]   // D231458907 with single char transcription error (0 -> A) with delta 10   
   [InlineData(MrzFormat.B, LineSeparator.Crlf, "N231458907UTO7408122F1204159<<<<<<<<")]           // D231458907 with single char transcription error (D -> N) with delta 10
   [InlineData(MrzFormat.B, LineSeparator.Lf,   "L89890C236UTO7408122F1204159<<<<<<<<")]           // L898902C36 with two char transposition error (2C -> C2) with delta 10
   [InlineData(MrzFormat.B, LineSeparator.None, "0000000000UTO8812728F0000000<<<<<<<<")]           // 8812278 with two char transposition error (27 -> 72) with delta 5
   [InlineData(MrzFormat.A, LineSeparator.Crlf, "0000000000UTO0000000F8812728<<<<<<<<<<<<<<<<")]   // 8812278 with two char transposition error (27 -> 72) with delta 5
   public void Icao9303MachineReadableVisaAlgorithm_Validate_ShouldReturnTrue_WhenValueContainsUndetectableError(
      MrzFormat format,
      LineSeparator lineSeparator,
      String mrzSecondLine)
   {
      // Arrange.
      var sut = new Icao9303MachineReadableVisaAlgorithm()
      {
         LineSeparator = lineSeparator,
      };
      var value = GetTestValue(format, lineSeparator, null, mrzSecondLine);

      // Act/assert.
      sut.Validate(value).Should().BeTrue();
   }

   [Theory]
   [InlineData(MrzFormat.A, LineSeparator.None, "E231458907UTO7408122F1204159<<<<<<<<<<<<<<<<")]   // D231458907 with single character transcription error (D -> E)
   [InlineData(MrzFormat.A, LineSeparator.Crlf, "D241458907UTO7408122F1204159<<<<<<<<<<<<<<<<")]   // D231458907 with single digit transcription error (3 -> 4)
   [InlineData(MrzFormat.A, LineSeparator.Lf,   "D231458907UTO7409122F1204159<<<<<<<<<<<<<<<<")]   // 7408122 with single digit transcription error (8 -> 9)
   [InlineData(MrzFormat.A, LineSeparator.None, "D231458907UTO7408122F1203159<<<<<<<<<<<<<<<<")]   // 1204159 with single digit transcription error (4 -> 3)
   [InlineData(MrzFormat.B, LineSeparator.Crlf, "2D31458907UTO7408122F1204159<<<<<<<<")]           // D231458907 with two character transposition error (D2 -> 2D)
   [InlineData(MrzFormat.B, LineSeparator.Lf,   "D231548907UTO7408122F1204159<<<<<<<<")]           // D231458907 with two digit transposition error (45 -> 54)
   [InlineData(MrzFormat.B, LineSeparator.None, "D231458907UTO7408212F1204159<<<<<<<<")]           // 7408122 with two digit transposition error (12 -> 21)
   [InlineData(MrzFormat.B, LineSeparator.Crlf, "D231458907UTO7409122F1201459<<<<<<<<")]           // 1204159 with two digit transposition error (41 -> 14)
   public void Icao9303MachineReadableVisaAlgorithm_Validate_ShouldReturnFalse_WhenValueContainsDetectableError(
      MrzFormat format,
      LineSeparator lineSeparator,
      String mrzSecondLine)
   {
      // Arrange.
      var sut = new Icao9303MachineReadableVisaAlgorithm()
      {
         LineSeparator = lineSeparator,
      };
      var value = GetTestValue(format, lineSeparator, null, mrzSecondLine);

      // Act/assert.
      sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   [InlineData(MrzFormat.A, LineSeparator.None, "0000000000UTO0000000F0000000<<<<<<<<<<<<<<<<")]
   [InlineData(MrzFormat.B, LineSeparator.Crlf, "0000000000UTO0000000F0000000<<<<<<<<")]
   public void Icao9303MachineReadableVisaAlgorithm_Validate_ShouldReturnTrue_WhenFieldsAreAllZeros(
      MrzFormat format,
      LineSeparator lineSeparator,
      String mrzSecondLine)
   {
      // Arrange.
      var sut = new Icao9303MachineReadableVisaAlgorithm()
      {
         LineSeparator = lineSeparator,
      };
      var value = GetTestValue(format, lineSeparator, null, mrzSecondLine);

      // Act/assert.
      sut.Validate(value).Should().BeTrue();
   }

   [Theory]
   [InlineData(MrzFormat.A, LineSeparator.None, "<<<<<<<<<0UTO<<<<<<0F<<<<<<0<<<<<<<<<<<<<<<<")]
   [InlineData(MrzFormat.B, LineSeparator.Crlf, "<<<<<<<<<0UTO<<<<<<0F<<<<<<0<<<<<<<<")]
   public void Icao9303MachineReadableVisaAlgorithm_Validate_ShouldReturnTrue_WhenFieldsAreAllFillerCharacters(
      MrzFormat format,
      LineSeparator lineSeparator,
      String mrzSecondLine)
   {
      // Arrange.
      var sut = new Icao9303MachineReadableVisaAlgorithm()
      {
         LineSeparator = lineSeparator,
      };
      var value = GetTestValue(format, lineSeparator, null, mrzSecondLine);

      // Act/assert.
      sut.Validate(value).Should().BeTrue();
   }

   [Theory]
   [InlineData(MrzFormat.A, LineSeparator.None, "b231458907UTO7408122F1204159<<<<<<<<<<<<<<<<")]   // D231458907 with D replaced with character 30 positions later in ASCII table
   [InlineData(MrzFormat.A, LineSeparator.Crlf, "D2314589&7UTO7408122F1204159<<<<<<<<<<<<<<<<")]   // D231458907 with 0 replaced with character 10 positions before in ASCII table
   [InlineData(MrzFormat.B, LineSeparator.Lf,   "D2314589:7UTO7408122F1204159<<<<<<<<")]           // D231458907 with 0 replaced with character 10 positions later in ASCII table
   [InlineData(MrzFormat.B, LineSeparator.None, "D231458907UTO7>08122F1204159<<<<<<<<")]           // 7408122 with 4 replaced with character 10 positions later in ASCII table
   public void Icao9303MachineReadableVisaAlgorithm_Validate_ShouldReturnFalse_WhenFieldContainsInvalidCharacter(
      MrzFormat format,
      LineSeparator lineSeparator,
      String mrzSecondLine)
   {
      // Arrange.
      var sut = new Icao9303MachineReadableVisaAlgorithm()
      {
         LineSeparator = lineSeparator,
      };
      var value = GetTestValue(format, lineSeparator, null, mrzSecondLine);

      // Act/assert.
      sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   [InlineData(MrzFormat.A, LineSeparator.None, "D23145890AUTO7408122F1204159<<<<<<<<<<<<<<<<")]   // Document number check digit is invalid character
   [InlineData(MrzFormat.A, LineSeparator.None, "D23145890&UTO7408122F1204159<<<<<<<<<<<<<<<<")]   // Document number check digit is invalid character
   [InlineData(MrzFormat.A, LineSeparator.None, "D23145890:UTO7408122F1204159<<<<<<<<<<<<<<<<")]   // Document number check digit is invalid character
   [InlineData(MrzFormat.A, LineSeparator.Crlf, "D231458907UTO740812AF1204159<<<<<<<<<<<<<<<<")]   // Date of birth check digit is invalid character
   [InlineData(MrzFormat.A, LineSeparator.Crlf, "D231458907UTO740812&F1204159<<<<<<<<<<<<<<<<")]   // Date of birth check digit is invalid character
   [InlineData(MrzFormat.A, LineSeparator.Crlf, "D231458907UTO740812<F1204159<<<<<<<<<<<<<<<<")]   // Date of birth check digit is invalid character
   [InlineData(MrzFormat.A, LineSeparator.Crlf, "D231458907UTO740812[F1204159<<<<<<<<<<<<<<<<")]   // Date of birth check digit is invalid character
   [InlineData(MrzFormat.B, LineSeparator.Lf,   "D231458907UTO7408122F120415A<<<<<<<<")]           // Date of expiry check digit is invalid character
   [InlineData(MrzFormat.B, LineSeparator.Lf,   "D231458907UTO7408122F120415&<<<<<<<<")]           // Date of expiry check digit is invalid character
   [InlineData(MrzFormat.B, LineSeparator.Lf,   "D231458907UTO7408122F120415<<<<<<<<<")]           // Date of expiry check digit is invalid character
   [InlineData(MrzFormat.B, LineSeparator.Lf,   "D231458907UTO7408122F120415[<<<<<<<<")]           // Date of expiry check digit is invalid character
   public void Icao9303MachineReadableVisaAlgorithm_Validate_ShouldReturnFalse_WhenRequiredFieldCheckDigitContainsNonDigitCharacter(
      MrzFormat format,
      LineSeparator lineSeparator,
      String mrzSecondLine)
   {
      // Arrange.
      var sut = new Icao9303MachineReadableVisaAlgorithm()
      {
         LineSeparator = lineSeparator,
      };
      var value = GetTestValue(format, lineSeparator, null, mrzSecondLine);

      // Act/assert.
      sut.Validate(value).Should().BeFalse();
   }

   #endregion
}
