// Ignore Spelling: Icao Mrz

namespace CheckDigits.Net.Tests.Unit.ValueSpecificAlgorithms;

public class Icao9303SizeTD3AlgorithmTests
{
   private readonly Icao9303SizeTD3Algorithm _sut = new();

   // Example MRZ from Example from https://www.icao.int/publications/Documents/9303_p4_cons_en.pdf
   private const String _mrzFirstLine = "P<UTOERIKSSON<<ANNA<MARIA<<<<<<<<<<<<<<<<<<<";
   private const String _mrzSecondLine = "L898902C36UTO7408122F1204159ZE184226B<<<<<10";
   private const String _mrzLineSeparatorNone = "P<UTOERIKSSON<<ANNA<MARIA<<<<<<<<<<<<<<<<<<<L898902C36UTO7408122F1204159ZE184226B<<<<<10";
   private const String _mrzLineSeparatorCrlf = "P<UTOERIKSSON<<ANNA<MARIA<<<<<<<<<<<<<<<<<<<\r\nL898902C36UTO7408122F1204159ZE184226B<<<<<10";
   private const String _mrzLineSeparatorLf = "P<UTOERIKSSON<<ANNA<MARIA<<<<<<<<<<<<<<<<<<<\nL898902C36UTO7408122F1204159ZE184226B<<<<<10";

   private static String GetTestValue(
      LineSeparator lineSeparator = LineSeparator.None,
      String? secondLineValue = null)
   {
      var separatorChars = lineSeparator switch
      {
         LineSeparator.Crlf => "\r\n",
         LineSeparator.Lf => "\n",
         _ => String.Empty,
      };

      return _mrzFirstLine + separatorChars + (secondLineValue ?? _mrzSecondLine);
   }

   #region AlgorithmDescription Property Tests
      // ==========================================================================
      // ==========================================================================

   [Fact]
   public void Icao9303SizeTD3Algorithm_AlgorithmDescription_ShouldReturnExpectedValue()
      => _sut.AlgorithmDescription.Should().Be(Resources.Icao9303SizeTD3AlgorithmDescription);

   #endregion

   #region AlgorithmName Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Icao9303SizeTD3Algorithm_AlgorithmName_ShouldReturnExpectedValue()
      => _sut.AlgorithmName.Should().Be(Resources.Icao9303SizeTD3AlgorithmName);

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
   public void Icao9302TD3Algorithm_LineSeparator_ShouldNotThrow_ValueIsValid(LineSeparator lineSeparator)
   {
      // Arrange.
      var act = () => _sut.LineSeparator = lineSeparator;

      // Act/assert.
      act.Should().NotThrow();
   }

   [Fact]
   public void Icao9302TD3Algorithm_LineSeparator_ShouldThrowArgumentOutOfRangeException_ValueIsInvalid()
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
   public void Icao9303SizeTD3Algorithm_Validate_ShouldReturnFalse_WhenInputIsNull()
      => _sut.Validate(null!).Should().BeFalse();

   [Fact]
   public void Icao9303SizeTD3Algorithm_Validate_ShouldReturnFalse_WhenInputIsEmpty()
      => _sut.Validate(String.Empty).Should().BeFalse();

   [Theory]
   [InlineData(LineSeparator.None, "P<UTOERIKSSON<<ANNA<MARIA<<<<<<<<<<<<<<<<<<L898902C36UTO7408122F1204159ZE184226B<<<<<10")]
   [InlineData(LineSeparator.Crlf, "P<UTOERIKSSON<<ANNA<MARIA<<<<<<<<<<<<<<<<<<\r\nL898902C36UTO7408122F1204159ZE184226B<<<<<10")]
   [InlineData(LineSeparator.Lf, "P<UTOERIKSSON<<ANNA<MARIA<<<<<<<<<<<<<<<<<<\nL898902C36UTO7408122F1204159ZE184226B<<<<<10")]
   [InlineData(LineSeparator.None, "P<UTOERIKSSON<<ANNA<MARIA<<<<<<<<<<<<<<<<<<<<L898902C36UTO7408122F1204159ZE184226B<<<<<10")]
   [InlineData(LineSeparator.Crlf, "P<UTOERIKSSON<<ANNA<MARIA<<<<<<<<<<<<<<<<<<<<\r\nL898902C36UTO7408122F1204159ZE184226B<<<<<10")]
   [InlineData(LineSeparator.Lf, "P<UTOERIKSSON<<ANNA<MARIA<<<<<<<<<<<<<<<<<<<<\nL898902C36UTO7408122F1204159ZE184226B<<<<<10")]
   public void Icao9303SizeTD3Algorithm_Validate_ShouldReturnFalse_WhenValueIsInvalidLength(
      LineSeparator lineSeparator,
      String value)
   {
      // Arrange.
      var sut = new Icao9303SizeTD3Algorithm()
      {
         LineSeparator = lineSeparator,
      };

      // Act/assert.
      sut.Validate(value).Should().BeFalse();
   }

   public static TheoryData<LineSeparator, String> FieldCharacterMapData => new()
   {
      { LineSeparator.None, "0000000000UTO0000000F0000000<<<<<<<<<<<<<<<0" },
      { LineSeparator.None, "0010000001UTO0000000F0000000<<<<<<<<<<<<<<<8" },
      { LineSeparator.None, "0020000002UTO0000000F0000000<<<<<<<<<<<<<<<6" },
      { LineSeparator.None, "0030000003UTO0000000F0000000<<<<<<<<<<<<<<<4" },
      { LineSeparator.None, "0040000004UTO0000000F0000000<<<<<<<<<<<<<<<2" },
      { LineSeparator.None, "0050000005UTO0000000F0000000<<<<<<<<<<<<<<<0" },
      { LineSeparator.None, "0060000006UTO0000000F0000000<<<<<<<<<<<<<<<8" },
      { LineSeparator.None, "0070000007UTO0000000F0000000<<<<<<<<<<<<<<<6" },
      { LineSeparator.None, "0080000008UTO0000000F0000000<<<<<<<<<<<<<<<4" },
      { LineSeparator.None, "0090000009UTO0000000F0000000<<<<<<<<<<<<<<<2" },
      { LineSeparator.Crlf, "00A0000000UTO0000000F0000000<<<<<<<<<<<<<<<0" },
      { LineSeparator.Crlf, "00B0000001UTO0000000F0000000<<<<<<<<<<<<<<<8" },
      { LineSeparator.Crlf, "00C0000002UTO0000000F0000000<<<<<<<<<<<<<<<6" },
      { LineSeparator.Crlf, "00D0000003UTO0000000F0000000<<<<<<<<<<<<<<<4" },
      { LineSeparator.Crlf, "00E0000004UTO0000000F0000000<<<<<<<<<<<<<<<2" },
      { LineSeparator.Crlf, "00F0000005UTO0000000F0000000<<<<<<<<<<<<<<<0" },
      { LineSeparator.Crlf, "00G0000006UTO0000000F0000000<<<<<<<<<<<<<<<8" },
      { LineSeparator.Crlf, "00H0000007UTO0000000F0000000<<<<<<<<<<<<<<<6" },
      { LineSeparator.Crlf, "00I0000008UTO0000000F0000000<<<<<<<<<<<<<<<4" },
      { LineSeparator.Crlf, "00J0000009UTO0000000F0000000<<<<<<<<<<<<<<<2" },
      { LineSeparator.Lf, "00K0000000UTO0000000F0000000<<<<<<<<<<<<<<<0" },
      { LineSeparator.Lf, "00L0000001UTO0000000F0000000<<<<<<<<<<<<<<<8" },
      { LineSeparator.Lf, "00M0000002UTO0000000F0000000<<<<<<<<<<<<<<<6" },
      { LineSeparator.Lf, "00N0000003UTO0000000F0000000<<<<<<<<<<<<<<<4" },
      { LineSeparator.Lf, "00O0000004UTO0000000F0000000<<<<<<<<<<<<<<<2" },
      { LineSeparator.Lf, "00P0000005UTO0000000F0000000<<<<<<<<<<<<<<<0" },
      { LineSeparator.Lf, "00Q0000006UTO0000000F0000000<<<<<<<<<<<<<<<8" },
      { LineSeparator.Lf, "00R0000007UTO0000000F0000000<<<<<<<<<<<<<<<6" },
      { LineSeparator.Lf, "00S0000008UTO0000000F0000000<<<<<<<<<<<<<<<4" },
      { LineSeparator.Lf, "00T0000009UTO0000000F0000000<<<<<<<<<<<<<<<2" },
      { LineSeparator.None, "00U0000000UTO0000000F0000000<<<<<<<<<<<<<<<0" },
      { LineSeparator.None, "00V0000001UTO0000000F0000000<<<<<<<<<<<<<<<8" },
      { LineSeparator.None, "00W0000002UTO0000000F0000000<<<<<<<<<<<<<<<6" },
      { LineSeparator.None, "00X0000003UTO0000000F0000000<<<<<<<<<<<<<<<4" },
      { LineSeparator.None, "00Y0000004UTO0000000F0000000<<<<<<<<<<<<<<<2" },
      { LineSeparator.None, "00Z0000005UTO0000000F0000000<<<<<<<<<<<<<<<0" },
      { LineSeparator.None, "00<0000000UTO0000000F0000000<<<<<<<<<<<<<<<0" },
   };

   [Theory]
   [MemberData(nameof(FieldCharacterMapData))]
   public void Icao9303SizeTD3Algorithm_Validate_ShouldCorrectlyMapFieldCharacterValues(
      LineSeparator lineSeparator,
      String mrzSecondLine)
   {
      // Arrange.
      var sut = new Icao9303SizeTD3Algorithm()
      {
         LineSeparator = lineSeparator,
      };
      var value = GetTestValue(lineSeparator, mrzSecondLine);

      // Act/assert.
      sut.Validate(value).Should().BeTrue();
   }

   public static TheoryData<LineSeparator, String> WeightByCharacterPositionData => new()
   {
      { LineSeparator.None, "1000000007UTO0000000F0000000<<<<<<<<<<<<<<<6" },
      { LineSeparator.None, "0100000003UTO0000000F0000000<<<<<<<<<<<<<<<4" },
      { LineSeparator.None, "0010000001UTO0000000F0000000<<<<<<<<<<<<<<<8" },
      { LineSeparator.None, "0001000007UTO0000000F0000000<<<<<<<<<<<<<<<6" },
      { LineSeparator.None, "0000100003UTO0000000F0000000<<<<<<<<<<<<<<<4" },
      { LineSeparator.None, "0000010001UTO0000000F0000000<<<<<<<<<<<<<<<8" },
      { LineSeparator.None, "0000001007UTO0000000F0000000<<<<<<<<<<<<<<<6" },
      { LineSeparator.None, "0000000103UTO0000000F0000000<<<<<<<<<<<<<<<4" },
      { LineSeparator.None, "0000000011UTO0000000F0000000<<<<<<<<<<<<<<<8" },
      { LineSeparator.Crlf, "0000000000UTO1000007F0000000<<<<<<<<<<<<<<<4" },
      { LineSeparator.Crlf, "0000000000UTO0100003F0000000<<<<<<<<<<<<<<<0" },
      { LineSeparator.Crlf, "0000000000UTO0010001F0000000<<<<<<<<<<<<<<<0" },
      { LineSeparator.Crlf, "0000000000UTO0001007F0000000<<<<<<<<<<<<<<<4" },
      { LineSeparator.Crlf, "0000000000UTO0000103F0000000<<<<<<<<<<<<<<<0" },
      { LineSeparator.Crlf, "0000000000UTO0000011F0000000<<<<<<<<<<<<<<<0" },
      { LineSeparator.Lf, "0000000000UTO0000000F1000007<<<<<<<<<<<<<<<8" },
      { LineSeparator.Lf, "0000000000UTO0000000F0100003<<<<<<<<<<<<<<<0" },
      { LineSeparator.Lf, "0000000000UTO0000000F0010001<<<<<<<<<<<<<<<4" },
      { LineSeparator.Lf, "0000000000UTO0000000F0001007<<<<<<<<<<<<<<<8" },
      { LineSeparator.Lf, "0000000000UTO0000000F0000103<<<<<<<<<<<<<<<0" },
      { LineSeparator.Lf, "0000000000UTO0000000F0000011<<<<<<<<<<<<<<<4" },
      { LineSeparator.None, "0000000000UTO0000000F00000001000000000000074" },
      { LineSeparator.None, "0000000000UTO0000000F00000000100000000000036" },
      { LineSeparator.None, "0000000000UTO0000000F00000000010000000000012" },
      { LineSeparator.None, "0000000000UTO0000000F00000000001000000000074" },
      { LineSeparator.None, "0000000000UTO0000000F00000000000100000000036" },
      { LineSeparator.None, "0000000000UTO0000000F00000000000010000000012" },
      { LineSeparator.None, "0000000000UTO0000000F00000000000001000000074" },
      { LineSeparator.None, "0000000000UTO0000000F00000000000000100000036" },
      { LineSeparator.None, "0000000000UTO0000000F00000000000000010000012" },
      { LineSeparator.None, "0000000000UTO0000000F00000000000000001000074" },
      { LineSeparator.None, "0000000000UTO0000000F00000000000000000100036" },
      { LineSeparator.None, "0000000000UTO0000000F00000000000000000010012" },
      { LineSeparator.None, "0000000000UTO0000000F00000000000000000001074" },
      { LineSeparator.None, "0000000000UTO0000000F00000000000000000000136" },
   };

   [Theory]
   [MemberData(nameof(WeightByCharacterPositionData))]
   public void Icao9303SizeTD3Algorithm_Validate_ShouldCorrectlyWeightByCharacterPosition(
      LineSeparator lineSeparator,
      String mrzSecondLine)
   {
      // Arrange.
      var sut = new Icao9303SizeTD3Algorithm()
      {
         LineSeparator = lineSeparator,
      };
      var value = GetTestValue(lineSeparator, mrzSecondLine);

      // Act/assert.
      sut.Validate(value).Should().BeTrue();
   }
   public static TheoryData<LineSeparator, String> ValidMrzData => new()
   {
      { LineSeparator.None, _mrzLineSeparatorNone },
      { LineSeparator.Crlf, _mrzLineSeparatorCrlf },
      { LineSeparator.Lf, _mrzLineSeparatorLf },
   };

   [Theory]
   [MemberData(nameof(ValidMrzData))]
   public void Icao9303SizeTD3Algorithm_Validate_ShouldReturnTrue_WhenValueContainsValidCheckDigits(
      LineSeparator lineSeparator,
      String value)
   {
      // Arrange.
      var sut = new Icao9303SizeTD3Algorithm()
      {
         LineSeparator = lineSeparator,
      };

      // Act/assert.
      sut.Validate(value).Should().BeTrue();
   }

   [Theory]
   [InlineData(LineSeparator.None, "L8989A2C36UTO7408122F1204159ZE184226B<<<<<10")]    // L898902C36 with single char transcription error (0 -> A) with delta 10
   [InlineData(LineSeparator.Crlf, "L89890C236UTO7408122F1204159ZE184226B<<<<<10")]    // L898902C36 with two char transposition error (2C -> C2) with delta 10
   [InlineData(LineSeparator.Lf,   "0000000000UTO8812728F0000000<<<<<<<<<<<<<<<0")]    // 8812278 with two char transposition error (27 -> 72) with delta 5
   [InlineData(LineSeparator.None, "0000000000UTO0000000F8812728<<<<<<<<<<<<<<<2")]    // 8812278 with two char transposition error (27 -> 72) with delta 5
   [InlineData(LineSeparator.Crlf, "0000000000UTO0000000F0000000123456789A123450")]    // 123456789012345 with single char transcription error (0 -> A) with delta 10
   public void Icao9303SizeTD3Algorithm_Validate_ShouldReturnTrue_WhenValueContainsUndetectableError(
      LineSeparator lineSeparator,
      String mrzSecondLine)
   {
      // Arrange.
      var sut = new Icao9303SizeTD3Algorithm()
      {
         LineSeparator = lineSeparator,
      };
      var value = GetTestValue(lineSeparator, mrzSecondLine);

      // Act/assert.
      sut.Validate(value).Should().BeTrue();
   }

   [Theory]
   [InlineData(LineSeparator.None, "L898902D36UTO7408122F1204159ZE184226B<<<<<13")]    // L898902C36 with single char transcription error (C -> D)
   [InlineData(LineSeparator.Crlf, "L898902C36UTO7438122F1204159ZE184226B<<<<<11")]    // 7408122 with single digit transcription error (0 -> 3)
   [InlineData(LineSeparator.Lf,   "L898902C36UTO7408122F1201459ZE184226B<<<<<19")]    // 1204159 with two digit transposition error (41 -> 14)
   [InlineData(LineSeparator.None, "L898902C36UTO7408122F1204159ZE184226B<<<<<17")]    // ZE184226B<<<<<1 with two char transposition error (ZE -> EZ)
   public void Icao9303SizeTD3Algorithm_Validate_ShouldReturnFalse_WhenValueContainsDetectableError(
      LineSeparator lineSeparator,
      String mrzSecondLine)
   {
      // Arrange.
      var sut = new Icao9303SizeTD3Algorithm()
      {
         LineSeparator = lineSeparator,
      };
      var value = GetTestValue(lineSeparator, mrzSecondLine);

      // Act/assert.
      sut.Validate(value).Should().BeFalse();
   }

   [Fact]
   public void Icao9303SizeTD2Algorithm_Validate_ShouldReturnTrue_WhenFieldsAreAllZeros()
      => _sut.Validate("P<UTOERIKSSON<<ANNA<MARIA<<<<<<<<<<<<<<<<<<<0000000000UTO0000000F0000000<<<<<<<<<<<<<<<0").Should().BeTrue();

   [Fact]
   public void Icao9303SizeTD2Algorithm_Validate_ShouldReturnTrue_WhenFieldsAreAllFillerCharacters()
      => _sut.Validate("P<UTOERIKSSON<<ANNA<MARIA<<<<<<<<<<<<<<<<<<<<<<<<<<<<0UTO<<<<<<0F<<<<<<0<<<<<<<<<<<<<<<0").Should().BeTrue();

   [Theory]
   [InlineData(LineSeparator.Crlf, "`898902C36UTO7408122F1204159ZE184226B<<<<<10")]    // L898902C36 with L replaced with character 20 positions later in ASCII table
   [InlineData(LineSeparator.Lf,   "L898902C36UTO74&8122F1204159ZE184226B<<<<<10")]    // 7408122 with 0 replaced with character 10 positions before in ASCII table
   [InlineData(LineSeparator.None, "L898902C36UTO7408122F12:4159ZE184226B<<<<<10")]    // 1204159 with 0 replaced with character 10 positions later in ASCII table
   [InlineData(LineSeparator.Crlf, "L898902C36UTO7408122F1204159ZE18>226B<<<<<10")]    // ZE184226B<<<<<1 with 4 replaced with character 10 positions later in ASCII table
   public void Icao9303SizeTD3Algorithm_Validate_ShouldReturnFalse_WhenFieldContainsInvalidCharacter(
      LineSeparator lineSeparator,
      String mrzSecondLine)
   {
      // Arrange.
      var sut = new Icao9303SizeTD3Algorithm()
      {
         LineSeparator = lineSeparator,
      };
      var value = GetTestValue(lineSeparator, mrzSecondLine);

      // Act/assert.
      sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   [InlineData(LineSeparator.None, "L898902C3AUTO7408122F1204159ZE184226B<<<<<10")]    // Document number check digit is invalid character
   [InlineData(LineSeparator.Crlf, "L898902C3<UTO7408122F1204159ZE184226B<<<<<10")]    // Document number check digit is invalid character
   [InlineData(LineSeparator.Lf,   "L898902C3:UTO7408122F1204159ZE184226B<<<<<10")]    // Document number check digit is invalid character
   [InlineData(LineSeparator.None, "L898902C36UTO740812AF1204159ZE184226B<<<<<10")]    // Date of birth check digit is invalid character
   [InlineData(LineSeparator.Crlf, "L898902C36UTO740812<F1204159ZE184226B<<<<<10")]    // Date of birth check digit is invalid character
   [InlineData(LineSeparator.Lf,   "L898902C36UTO740812:F1204159ZE184226B<<<<<10")]    // Date of birth check digit is invalid character
   [InlineData(LineSeparator.None, "L898902C36UTO7408122F120415AZE184226B<<<<<10")]    // Date of expiry check digit is invalid character
   [InlineData(LineSeparator.Crlf, "L898902C36UTO7408122F120415<ZE184226B<<<<<10")]    // Date of expiry check digit is invalid character
   [InlineData(LineSeparator.Lf,   "L898902C36UTO7408122F120415:ZE184226B<<<<<10")]    // Date of expiry check digit is invalid character
   public void Icao9303SizeTD3Algorithm_Validate_ShouldReturnFalse_WhenRequiredFieldCheckDigitContainsNonDigitCharacter(
      LineSeparator lineSeparator,
      String mrzSecondLine)
   {
      // Arrange.
      var sut = new Icao9303SizeTD3Algorithm()
      {
         LineSeparator = lineSeparator,
      };
      var value = GetTestValue(lineSeparator, mrzSecondLine);

      // Act/assert.
      sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   [InlineData(LineSeparator.Crlf, "L898902C36UTO7408122F1204159ZE184226B<<<<<A0")]    // Optional data check digit is invalid character
   [InlineData(LineSeparator.Lf,   "L898902C36UTO7408122F1204159ZE184226B<<<<<:0")]    // Optional data check digit is invalid character
   public void Icao9303SizeTD3Algorithm_Validate_ShouldReturnFalse_WhenOptionalFieldCheckDigitContainsInvalidCharacter(
      LineSeparator lineSeparator,
      String mrzSecondLine)
   {
      // Arrange.
      var sut = new Icao9303SizeTD3Algorithm()
      {
         LineSeparator = lineSeparator,
      };
      var value = GetTestValue(lineSeparator, mrzSecondLine);

      // Act/assert.
      sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   [InlineData(LineSeparator.Crlf, "0000000000UTO0000000F0000000<<<<<<<<<<<<<<00")]
   [InlineData(LineSeparator.Lf,   "0000000000UTO0000000F0000000<<<<<<<<<<<<<<<0")]
   public void Icao9303SizeTD3Algorithm_Validate_ShouldReturnTrue_WhenEmptyOptionalFieldCheckDigitIsZeroOrFillerCharacter(
      LineSeparator lineSeparator,
      String mrzSecondLine)
   {
      // Arrange.
      var sut = new Icao9303SizeTD3Algorithm()
      {
         LineSeparator = lineSeparator,
      };
      var value = GetTestValue(lineSeparator, mrzSecondLine);

      // Act/assert.
      sut.Validate(value).Should().BeTrue();
   }

   #endregion
}
