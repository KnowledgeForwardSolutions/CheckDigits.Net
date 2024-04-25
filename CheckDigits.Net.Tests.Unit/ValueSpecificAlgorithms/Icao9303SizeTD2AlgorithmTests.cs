// Ignore Spelling: Icao Mrz

namespace CheckDigits.Net.Tests.Unit.ValueSpecificAlgorithms;

public class Icao9303SizeTD2AlgorithmTests
{
   private readonly Icao9303SizeTD2Algorithm _sut = new();

   // Example MRZ from Example from https://www.icao.int/publications/Documents/9303_p6_cons_en.pdf
   private const String _mrzFirstLine = "I<UTOERIKSSON<<ANNA<MARIA<<<<<<<<<<<";
   private const String _mrzSecondLine = "D231458907UTO7408122F1204159<<<<<<<6";

   private static String GetTestValue(
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

      return (firstLine ?? _mrzFirstLine)
         + separatorChars + (secondLine ?? _mrzSecondLine);
   }

   #region AlgorithmDescription Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Icao9303SizeTD2Algorithm_AlgorithmDescription_ShouldReturnExpectedValue()
      => _sut.AlgorithmDescription.Should().Be(Resources.Icao9303SizeTD2AlgorithmDescription);

   #endregion

   #region AlgorithmName Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Icao9303SizeTD2Algorithm_AlgorithmName_ShouldReturnExpectedValue()
      => _sut.AlgorithmName.Should().Be(Resources.Icao9303SizeTD2AlgorithmName);

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
   public void Icao9302TD2Algorithm_LineSeparator_ShouldNotThrow_ValueIsValid(LineSeparator lineSeparator)
   {
      // Arrange.
      var act = () => _sut.LineSeparator = lineSeparator;

      // Act/assert.
      act.Should().NotThrow();
   }

   [Theory]
   [MemberData(nameof(LineSeparatorValues))]
   public void Icao9303TD2Algorithm_LineSeparator_ShouldReturnExpectedValueAfterSetting(LineSeparator lineSeparator)
   {
      // Arrange.
      var sut = new Icao9303SizeTD2Algorithm() { LineSeparator = lineSeparator };

      // Act/assert.
      sut.LineSeparator.Should().Be(lineSeparator);
   }

   [Fact]
   public void Icao9302TD2Algorithm_LineSeparator_ShouldThrowArgumentOutOfRangeException_ValueIsInvalid()
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
   public void Icao9303SizeTD2Algorithm_Validate_ShouldReturnFalse_WhenInputIsNull()
      => _sut.Validate(null!).Should().BeFalse();

   [Fact]
   public void Icao9303SizeTD2Algorithm_Validate_ShouldReturnFalse_WhenInputIsEmpty()
      => _sut.Validate(String.Empty).Should().BeFalse();

   public static TheoryData<LineSeparator, String> InvalidLengthData => new()
   {
      { LineSeparator.None, GetTestValue(LineSeparator.None, _mrzFirstLine[..35]) },
      { LineSeparator.Crlf, GetTestValue(LineSeparator.Crlf, _mrzFirstLine[..35]) },
      { LineSeparator.Lf,   GetTestValue(LineSeparator.Lf,   _mrzFirstLine[..35]) },
      { LineSeparator.None, GetTestValue(LineSeparator.None, _mrzFirstLine + "<<") },
      { LineSeparator.Crlf, GetTestValue(LineSeparator.Crlf, _mrzFirstLine + "<<") },
      { LineSeparator.Lf,   GetTestValue(LineSeparator.Lf,   _mrzFirstLine + "<<") },
   };

   [Theory]
   [MemberData(nameof(InvalidLengthData))]
   public void Icao9303SizeTD2Algorithm_Validate_ShouldReturnFalse_WhenValueIsInvalidLength(
      LineSeparator lineSeparator,
      String value)
   {
      // Arrange.
      var sut = new Icao9303SizeTD2Algorithm()
      {
         LineSeparator = lineSeparator,
      };

      // Act/assert.
      sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   [InlineData(LineSeparator.None, "0000000000UTO0000000F0000000<<<<<<<0")]
   [InlineData(LineSeparator.None, "0010000001UTO0000000F0000000<<<<<<<8")]
   [InlineData(LineSeparator.None, "0020000002UTO0000000F0000000<<<<<<<6")]
   [InlineData(LineSeparator.None, "0030000003UTO0000000F0000000<<<<<<<4")]
   [InlineData(LineSeparator.None, "0040000004UTO0000000F0000000<<<<<<<2")]
   [InlineData(LineSeparator.None, "0050000005UTO0000000F0000000<<<<<<<0")]
   [InlineData(LineSeparator.None, "0060000006UTO0000000F0000000<<<<<<<8")]
   [InlineData(LineSeparator.None, "0070000007UTO0000000F0000000<<<<<<<6")]
   [InlineData(LineSeparator.None, "0080000008UTO0000000F0000000<<<<<<<4")]
   [InlineData(LineSeparator.None, "0090000009UTO0000000F0000000<<<<<<<2")]
   [InlineData(LineSeparator.Crlf, "00A0000000UTO0000000F0000000<<<<<<<0")]
   [InlineData(LineSeparator.Crlf, "00B0000001UTO0000000F0000000<<<<<<<8")]
   [InlineData(LineSeparator.Crlf, "00C0000002UTO0000000F0000000<<<<<<<6")]
   [InlineData(LineSeparator.Crlf, "00D0000003UTO0000000F0000000<<<<<<<4")]
   [InlineData(LineSeparator.Crlf, "00E0000004UTO0000000F0000000<<<<<<<2")]
   [InlineData(LineSeparator.Crlf, "00F0000005UTO0000000F0000000<<<<<<<0")]
   [InlineData(LineSeparator.Crlf, "00G0000006UTO0000000F0000000<<<<<<<8")]
   [InlineData(LineSeparator.Crlf, "00H0000007UTO0000000F0000000<<<<<<<6")]
   [InlineData(LineSeparator.Crlf, "00I0000008UTO0000000F0000000<<<<<<<4")]
   [InlineData(LineSeparator.Crlf, "00J0000009UTO0000000F0000000<<<<<<<2")]
   [InlineData(LineSeparator.Lf,   "00K0000000UTO0000000F0000000<<<<<<<0")]
   [InlineData(LineSeparator.Lf,   "00L0000001UTO0000000F0000000<<<<<<<8")]
   [InlineData(LineSeparator.Lf,   "00M0000002UTO0000000F0000000<<<<<<<6")]
   [InlineData(LineSeparator.Lf,   "00N0000003UTO0000000F0000000<<<<<<<4")]
   [InlineData(LineSeparator.Lf,   "00O0000004UTO0000000F0000000<<<<<<<2")]
   [InlineData(LineSeparator.Lf,   "00P0000005UTO0000000F0000000<<<<<<<0")]
   [InlineData(LineSeparator.Lf,   "00Q0000006UTO0000000F0000000<<<<<<<8")]
   [InlineData(LineSeparator.Lf,   "00R0000007UTO0000000F0000000<<<<<<<6")]
   [InlineData(LineSeparator.Lf,   "00S0000008UTO0000000F0000000<<<<<<<4")]
   [InlineData(LineSeparator.Lf,   "00T0000009UTO0000000F0000000<<<<<<<2")]
   [InlineData(LineSeparator.None, "00U0000000UTO0000000F0000000<<<<<<<0")]
   [InlineData(LineSeparator.None, "00V0000001UTO0000000F0000000<<<<<<<8")]
   [InlineData(LineSeparator.None, "00W0000002UTO0000000F0000000<<<<<<<6")]
   [InlineData(LineSeparator.None, "00X0000003UTO0000000F0000000<<<<<<<4")]
   [InlineData(LineSeparator.None, "00Y0000004UTO0000000F0000000<<<<<<<2")]
   [InlineData(LineSeparator.None, "00Z0000005UTO0000000F0000000<<<<<<<0")]
   [InlineData(LineSeparator.None, "00<0000000UTO0000000F0000000<<<<<<<0")]
   public void Icao9303SizeTD2Algorithm_Validate_ShouldCorrectlyMapFieldCharacterValues(
      LineSeparator lineSeparator,
      String mrzSecondLine)
   {
      // Arrange.
      var sut = new Icao9303SizeTD2Algorithm()
      {
         LineSeparator = lineSeparator,
      };
      var value = GetTestValue(lineSeparator, _mrzFirstLine, mrzSecondLine);

      // Act/assert.
      sut.Validate(value).Should().BeTrue();
   }

   [Theory]
   // Document number field
   [InlineData(LineSeparator.None, "1000000007UTO0000000F0000000<<<<<<<6")]
   [InlineData(LineSeparator.None, "0100000003UTO0000000F0000000<<<<<<<4")]
   [InlineData(LineSeparator.None, "0010000001UTO0000000F0000000<<<<<<<8")]
   [InlineData(LineSeparator.None, "0001000007UTO0000000F0000000<<<<<<<6")]
   [InlineData(LineSeparator.None, "0000100003UTO0000000F0000000<<<<<<<4")]
   [InlineData(LineSeparator.None, "0000010001UTO0000000F0000000<<<<<<<8")]
   [InlineData(LineSeparator.None, "0000001007UTO0000000F0000000<<<<<<<6")]
   [InlineData(LineSeparator.None, "0000000103UTO0000000F0000000<<<<<<<4")]
   [InlineData(LineSeparator.None, "0000000011UTO0000000F0000000<<<<<<<8")]
   // Extended document number
   [InlineData(LineSeparator.None, "000000000<UTO0000000F0000000100007<4")]
   [InlineData(LineSeparator.None, "000000000<UTO0000000F0000000010003<6")]
   [InlineData(LineSeparator.None, "000000000<UTO0000000F0000000001001<2")]
   [InlineData(LineSeparator.None, "000000000<UTO0000000F0000000000107<4")]
   [InlineData(LineSeparator.None, "000000000<UTO0000000F0000000000013<6")]
   // Date of birth field
   [InlineData(LineSeparator.Crlf, "0000000000UTO1000007F0000000<<<<<<<4")]
   [InlineData(LineSeparator.Crlf, "0000000000UTO0100003F0000000<<<<<<<0")]
   [InlineData(LineSeparator.Crlf, "0000000000UTO0010001F0000000<<<<<<<0")]
   [InlineData(LineSeparator.Crlf, "0000000000UTO0001007F0000000<<<<<<<4")]
   [InlineData(LineSeparator.Crlf, "0000000000UTO0000103F0000000<<<<<<<0")]
   [InlineData(LineSeparator.Crlf, "0000000000UTO0000011F0000000<<<<<<<0")]
   // Date of expiry field
   [InlineData(LineSeparator.Lf,   "0000000000UTO0000000F1000007<<<<<<<8")]
   [InlineData(LineSeparator.Lf,   "0000000000UTO0000000F0100003<<<<<<<0")]
   [InlineData(LineSeparator.Lf,   "0000000000UTO0000000F0010001<<<<<<<4")]
   [InlineData(LineSeparator.Lf,   "0000000000UTO0000000F0001007<<<<<<<8")]
   [InlineData(LineSeparator.Lf,   "0000000000UTO0000000F0000103<<<<<<<0")]
   [InlineData(LineSeparator.Lf,   "0000000000UTO0000000F0000011<<<<<<<4")]
   public void Icao9303SizeTD2Algorithm_Validate_ShouldCorrectlyWeightByCharacterPosition(
      LineSeparator lineSeparator,
      String mrzSecondLine)
   {
      // Arrange.
      var sut = new Icao9303SizeTD2Algorithm()
      {
         LineSeparator = lineSeparator,
      };
      var value = GetTestValue(lineSeparator, _mrzFirstLine, mrzSecondLine);

      // Act/assert.
      sut.Validate(value).Should().BeTrue();
   }

   [Theory]
   [InlineData(LineSeparator.None, "D231458907UTO7408122F1204159<<<<<<<6")]
   [InlineData(LineSeparator.Crlf, "D231458907UTO7408122F1204159<<<<<<<6")]
   [InlineData(LineSeparator.Lf,   "D231458907UTO7408122F1204159<<<<<<<6")]
   [InlineData(LineSeparator.None, "D23145890<UTO7408122F1204159AB1124<4")]
   [InlineData(LineSeparator.Crlf, "D23145890<UTO7408122F1204159AB1124<4")]
   [InlineData(LineSeparator.Lf,   "D23145890<UTO7408122F1204159AB1124<4")]
   public void Icao9303SizeTD2Algorithm_Validate_ShouldReturnTrue_WhenValueContainsValidCheckDigits(
      LineSeparator lineSeparator,
      String mrzSecondLine)
   {
      // Arrange.
      var sut = new Icao9303SizeTD2Algorithm()
      {
         LineSeparator = lineSeparator,
      };
      var value = GetTestValue(lineSeparator, _mrzFirstLine, mrzSecondLine);

      // Act/assert.
      sut.Validate(value).Should().BeTrue();
   }

   [Theory]
   [InlineData(LineSeparator.None, "D2314589A7UTO7408122F1204159<<<<<<<6")]   // D231458907 with single char transcription error (0 -> A) with delta 10   
   [InlineData(LineSeparator.Crlf, "N231458907UTO7408122F1204159<<<<<<<6")]   // D231458907 with single char transcription error (D -> N) with delta 10
   [InlineData(LineSeparator.Lf,   "L89890C236UTO7408122F1204159<<<<<<<8")]   // L898902C36 with two char transposition error (2C -> C2) with delta 10
   [InlineData(LineSeparator.None, "0000000000UTO8812728F0000000<<<<<<<0")]   // 8812278 with two char transposition error (27 -> 72) with delta 5
   [InlineData(LineSeparator.Crlf, "0000000000UTO0000000F8812728<<<<<<<2")]   // 8812278 with two char transposition error (27 -> 72) with delta 5
   [InlineData(LineSeparator.Lf,   "D23145890<UTO7408122F12041590B1124<4")]   // Extended document number AB1124 with single char transcription error (A -> 0) with delta 10
   [InlineData(LineSeparator.None, "D23145890<UTO7408122F1204159AB8325<6")]   // Extended document number AB3824 with two char transposition error (38 -> 83) with delta 5
   public void Icao9303SizeTD2Algorithm_Validate_ShouldReturnTrue_WhenValueContainsUndetectableError(
      LineSeparator lineSeparator,
      String mrzSecondLine)
   {
      // Arrange.
      var sut = new Icao9303SizeTD2Algorithm()
      {
         LineSeparator = lineSeparator,
      };
      var value = GetTestValue(lineSeparator, _mrzFirstLine, mrzSecondLine);

      // Act/assert.
      sut.Validate(value).Should().BeTrue();
   }

   [Theory]
   [InlineData(LineSeparator.None, "E231458907UTO7408122F1204159<<<<<<<6")]   // D231458907 with single character transcription error (D -> E)
   [InlineData(LineSeparator.None, "D241458907UTO7408122F1204159<<<<<<<6")]   // D231458907 with single digit transcription error (3 -> 4)
   [InlineData(LineSeparator.None, "D231458907UTO7409122F1204159<<<<<<<6")]   // 7408122 with single digit transcription error (8 -> 9)
   [InlineData(LineSeparator.None, "D231458907UTO7408122F1203159<<<<<<<6")]   // 1204159 with single digit transcription error (4 -> 3)
   [InlineData(LineSeparator.None, "2D31458907UTO7408122F1204159<<<<<<<6")]   // D231458907 with two character transposition error (D2 -> 2D)
   [InlineData(LineSeparator.None, "D231548907UTO7408122F1204159<<<<<<<6")]   // D231458907 with two digit transposition error (45 -> 54)
   [InlineData(LineSeparator.None, "D231458907UTO7408212F1204159<<<<<<<6")]   // 7408122 with two digit transposition error (12 -> 21)
   [InlineData(LineSeparator.None, "D231458907UTO7409122F1201459<<<<<<<6")]   // 1204159 with two digit transposition error (41 -> 14)
   [InlineData(LineSeparator.None, "D23145890<UTO7408122F1204159AC1124<4")]   // Extended document number AB112234566 with single character transcription error (B -> C)
   public void Icao9303SizeTD2Algorithm_Validate_ShouldReturnFalse_WhenValueContainsDetectableError(
      LineSeparator lineSeparator,
      String mrzSecondLine)
   {
      // Arrange.
      var sut = new Icao9303SizeTD2Algorithm()
      {
         LineSeparator = lineSeparator,
      };
      var value = GetTestValue(lineSeparator, _mrzFirstLine, mrzSecondLine);

      // Act/assert.
      sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   [InlineData(LineSeparator.None, "0000000000UTO0000000F0000000<<<<<<<0")]
   [InlineData(LineSeparator.Crlf, "0000000000UTO0000000F0000000000000<0")]
   public void Icao9303SizeTD2Algorithm_Validate_ShouldReturnTrue_WhenFieldsAreAllZeros(
      LineSeparator lineSeparator,
      String mrzSecondLine)
   {
      // Arrange.
      var sut = new Icao9303SizeTD2Algorithm()
      {
         LineSeparator = lineSeparator,
      };
      var value = GetTestValue(lineSeparator, _mrzFirstLine, mrzSecondLine);

      // Act/assert.
      sut.Validate(value).Should().BeTrue();
   }

   [Fact]
   public void Icao9303SizeTD2Algorithm_Validate_ShouldReturnTrue_WhenFieldsAreAllFillerCharacters()
   {
      // Arrange.
      var value = GetTestValue(
         LineSeparator.None,
         "<<<<<<<<<0UTO<<<<<<0F<<<<<<0<<<<<<<0");

      // Act/assert.
      _sut.Validate(value).Should().BeTrue();
   }

   [Theory]
   [InlineData(LineSeparator.None, "b231458907UTO7408122F1204159<<<<<<<6")]   // D231458907 with D replaced with character 30 positions later in ASCII table
   [InlineData(LineSeparator.Crlf, "D2314589&7UTO7408122F1204159<<<<<<<6")]   // D231458907 with 0 replaced with character 10 positions before in ASCII table
   [InlineData(LineSeparator.Lf,   "D2314589:7UTO7408122F1204159<<<<<<<6")]   // D231458907 with 0 replaced with character 10 positions later in ASCII table
   [InlineData(LineSeparator.None, "D231458907UTO7>08122F1204159<<<<<<<6")]   // 7408122 with 4 replaced with character 10 positions later in ASCII table
   [InlineData(LineSeparator.Crlf, "D23145890<UTO7408122F1204159A&1124<4")]   // Extended document number AB112234 with B replaced by invalid character
   [InlineData(LineSeparator.Lf,   "D23145890<UTO7408122F1204159A:1124<4")]   // Extended document number AB112234 with B replaced by invalid character
   [InlineData(LineSeparator.None, "D23145890<UTO7408122F1204159A[1124<4")]   // Extended document number AB112234 with B replaced by invalid character
   public void Icao9303SizeTD2Algorithm_Validate_ShouldReturnFalse_WhenFieldContainsInvalidCharacter(
      LineSeparator lineSeparator,
      String mrzSecondLine)
   {
      // Arrange.
      var sut = new Icao9303SizeTD2Algorithm()
      {
         LineSeparator = lineSeparator,
      };
      var value = GetTestValue(lineSeparator, _mrzFirstLine, mrzSecondLine);

      // Act/assert.
      sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   [InlineData(LineSeparator.None, "D23145890AUTO7408122F1204159<<<<<<<6")]   // Document number check digit is invalid character
   [InlineData(LineSeparator.None, "D23145890&UTO7408122F1204159<<<<<<<6")]   // Document number check digit is invalid character
   [InlineData(LineSeparator.None, "D23145890:UTO7408122F1204159<<<<<<<6")]   // Document number check digit is invalid character
   [InlineData(LineSeparator.Crlf, "D231458907UTO740812AF1204159<<<<<<<6")]   // Date of birth check digit is invalid character
   [InlineData(LineSeparator.Crlf, "D231458907UTO740812&F1204159<<<<<<<6")]   // Date of birth check digit is invalid character
   [InlineData(LineSeparator.Crlf, "D231458907UTO740812<F1204159<<<<<<<6")]   // Date of birth check digit is invalid character
   [InlineData(LineSeparator.Crlf, "D231458907UTO740812[F1204159<<<<<<<6")]   // Date of birth check digit is invalid character
   [InlineData(LineSeparator.Crlf, "D231458907UTO7408122F120415A<<<<<<<6")]   // Date of expiry check digit is invalid character
   [InlineData(LineSeparator.Crlf, "D231458907UTO7408122F120415&<<<<<<<6")]   // Date of expiry check digit is invalid character
   [InlineData(LineSeparator.Crlf, "D231458907UTO7408122F120415<<<<<<<<6")]   // Date of expiry check digit is invalid character
   [InlineData(LineSeparator.Crlf, "D231458907UTO7408122F120415[<<<<<<<6")]   // Date of expiry check digit is invalid character
   [InlineData(LineSeparator.Crlf, "D23145890<UTO7408122F1204159AB112A<4")]   // Extended document number check digit with invalid character
   [InlineData(LineSeparator.Crlf, "D23145890<UTO7408122F1204159AB112&<4")]   // Extended document number check digit with invalid character
   [InlineData(LineSeparator.Crlf, "D23145890<UTO7408122F1204159AB112<<4")]   // Extended document number check digit with invalid character
   [InlineData(LineSeparator.Crlf, "D23145890<UTO7408122F1204159AB112[<4")]   // Extended document number check digit with invalid character
   public void Icao9303SizeTD2Algorithm_Validate_ShouldReturnFalse_WhenRequiredFieldCheckDigitContainsNonDigitCharacter(
      LineSeparator lineSeparator,
      String mrzSecondLine)
   {
      // Arrange.
      var sut = new Icao9303SizeTD2Algorithm()
      {
         LineSeparator = lineSeparator,
      };
      var value = GetTestValue(lineSeparator, _mrzFirstLine, mrzSecondLine);

      // Act/assert.
      sut.Validate(value).Should().BeFalse();
   }

   #endregion
}
