// Ignore Spelling: Icao Mrz

namespace CheckDigits.Net.Tests.Unit.ValueSpecificAlgorithms;

public class Icao9303SizeTD1AlgorithmTests
{
   private readonly Icao9303SizeTD1Algorithm _sut = new();

   // Example MRZ from Example from https://www.icao.int/publications/Documents/9303_p5_cons_en.pdf
   private const String _mrzFirstLine = "I<UTOD231458907<<<<<<<<<<<<<<<";
   private const String _mrzSecondLine = "7408122F1204159UTO<<<<<<<<<<<6";
   private const String _mrzThirdLine = "ERIKSSON<<ANNA<MARIA<<<<<<<<<<";

   private static String GetTestValue(
      LineSeparator lineSeparator = LineSeparator.None,
#if !NET48
      String? firstLine = null,
      String? secondLine = null,
      String? thirdLine = null)
      #else
      String firstLine = null,
      String secondLine = null,
      String thirdLine = null)
#endif
   {
      var separatorChars = lineSeparator switch
      {
         LineSeparator.Crlf => "\r\n",
         LineSeparator.Lf => "\n",
         _ => String.Empty,
      };

      return (firstLine ?? _mrzFirstLine) 
         + separatorChars + (secondLine ?? _mrzSecondLine)
         + separatorChars + (thirdLine ?? _mrzThirdLine);
   }

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
   public void Icao9302TD1Algorithm_LineSeparator_ShouldNotThrow_ValueIsValid(LineSeparator lineSeparator)
   {
      // Arrange.
      var act = () => _sut.LineSeparator = lineSeparator;

      // Act/assert.
      act.Should().NotThrow();
   }

   [Theory]
   [MemberData(nameof(LineSeparatorValues))]
   public void Icao9303TD1Algorithm_LineSeparator_ShouldReturnExpectedValueAfterSetting(LineSeparator lineSeparator)
   {
      // Arrange.
      var sut = new Icao9303SizeTD1Algorithm() { LineSeparator = lineSeparator };

      // Act/assert.
      sut.LineSeparator.Should().Be(lineSeparator);
   }

   [Fact]
   public void Icao9302TD1Algorithm_LineSeparator_ShouldThrowArgumentOutOfRangeException_ValueIsInvalid()
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
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnFalse_WhenInputIsNull()
      => _sut.Validate(null!).Should().BeFalse();

   [Fact]
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnFalse_WhenInputIsEmpty()
      => _sut.Validate(String.Empty).Should().BeFalse();

   public static TheoryData<LineSeparator, String> InvalidLengthData => new()
   {
      { LineSeparator.None, GetTestValue(LineSeparator.None, _mrzFirstLine[..29]) },
      { LineSeparator.Crlf, GetTestValue(LineSeparator.Crlf, _mrzFirstLine[..29]) },
      { LineSeparator.Lf,   GetTestValue(LineSeparator.Lf,   _mrzFirstLine[..29]) },
      { LineSeparator.None, GetTestValue(LineSeparator.None, _mrzFirstLine + "<<") },
      { LineSeparator.Crlf, GetTestValue(LineSeparator.Crlf, _mrzFirstLine + "<<") },
      { LineSeparator.Lf,   GetTestValue(LineSeparator.Lf,   _mrzFirstLine + "<<") },
   };

   [Theory]
   [MemberData(nameof(InvalidLengthData))]
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnFalse_WhenValueIsInvalidLength(
      LineSeparator lineSeparator,
      String value)
   {
      // Arrange.
      var sut = new Icao9303SizeTD1Algorithm()
      {
         LineSeparator = lineSeparator,
      };

      // Act/assert.
      sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   [InlineData(LineSeparator.None, "I<UTO0000000000<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<0")]
   [InlineData(LineSeparator.None, "I<UTO0010000001<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<8")]
   [InlineData(LineSeparator.None, "I<UTO0020000002<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<6")]
   [InlineData(LineSeparator.None, "I<UTO0030000003<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<4")]
   [InlineData(LineSeparator.None, "I<UTO0040000004<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<2")]
   [InlineData(LineSeparator.None, "I<UTO0050000005<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<0")]
   [InlineData(LineSeparator.None, "I<UTO0060000006<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<8")]
   [InlineData(LineSeparator.None, "I<UTO0070000007<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<6")]
   [InlineData(LineSeparator.None, "I<UTO0080000008<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<4")]
   [InlineData(LineSeparator.None, "I<UTO0090000009<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<2")]
   [InlineData(LineSeparator.Crlf, "I<UTO00A0000000<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<0")]
   [InlineData(LineSeparator.Crlf, "I<UTO00B0000001<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<8")]
   [InlineData(LineSeparator.Crlf, "I<UTO00C0000002<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<6")]
   [InlineData(LineSeparator.Crlf, "I<UTO00D0000003<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<4")]
   [InlineData(LineSeparator.Crlf, "I<UTO00E0000004<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<2")]
   [InlineData(LineSeparator.Crlf, "I<UTO00F0000005<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<0")]
   [InlineData(LineSeparator.Crlf, "I<UTO00G0000006<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<8")]
   [InlineData(LineSeparator.Crlf, "I<UTO00H0000007<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<6")]
   [InlineData(LineSeparator.Crlf, "I<UTO00I0000008<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<4")]
   [InlineData(LineSeparator.Crlf, "I<UTO00J0000009<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<2")]
   [InlineData(LineSeparator.Lf,   "I<UTO00K0000000<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<0")]
   [InlineData(LineSeparator.Lf,   "I<UTO00L0000001<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<8")]
   [InlineData(LineSeparator.Lf,   "I<UTO00M0000002<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<6")]
   [InlineData(LineSeparator.Lf,   "I<UTO00N0000003<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<4")]
   [InlineData(LineSeparator.Lf,   "I<UTO00O0000004<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<2")]
   [InlineData(LineSeparator.Lf,   "I<UTO00P0000005<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<0")]
   [InlineData(LineSeparator.Lf,   "I<UTO00Q0000006<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<8")]
   [InlineData(LineSeparator.Lf,   "I<UTO00R0000007<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<6")]
   [InlineData(LineSeparator.Lf,   "I<UTO00S0000008<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<4")]
   [InlineData(LineSeparator.Lf,   "I<UTO00T0000009<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<2")]
   [InlineData(LineSeparator.None, "I<UTO00U0000000<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<0")]
   [InlineData(LineSeparator.None, "I<UTO00V0000001<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<8")]
   [InlineData(LineSeparator.None, "I<UTO00W0000002<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<6")]
   [InlineData(LineSeparator.None, "I<UTO00X0000003<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<4")]
   [InlineData(LineSeparator.None, "I<UTO00Y0000004<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<2")]
   [InlineData(LineSeparator.None, "I<UTO00Z0000005<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<0")]
   [InlineData(LineSeparator.None, "I<UTO00<0000000<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<0")]
   public void Icao9303SizeTD1Algorithm_Validate_ShouldCorrectlyMapFieldCharacterValues(
      LineSeparator lineSeparator,
      String mrzFirstLine,
      String mrzSecondLine)
   {
      // Arrange.
      var sut = new Icao9303SizeTD1Algorithm()
      {
         LineSeparator = lineSeparator,
      };
      var value = GetTestValue(lineSeparator, mrzFirstLine, mrzSecondLine);

      // Act/assert.
      sut.Validate(value).Should().BeTrue();
   }

   [Theory]
   // Document number field
   [InlineData(LineSeparator.None, "I<UTO1000000007<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<6")]
   [InlineData(LineSeparator.None, "I<UTO0100000003<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<4")]
   [InlineData(LineSeparator.None, "I<UTO0010000001<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<8")]
   [InlineData(LineSeparator.None, "I<UTO0001000007<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<6")]
   [InlineData(LineSeparator.None, "I<UTO0000100003<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<4")]
   [InlineData(LineSeparator.None, "I<UTO0000010001<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<8")]
   [InlineData(LineSeparator.None, "I<UTO0000001007<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<6")]
   [InlineData(LineSeparator.None, "I<UTO0000000103<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<4")]
   [InlineData(LineSeparator.None, "I<UTO0000000011<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<8")]
   // Extended document number
   [InlineData(LineSeparator.None, "I<UTO000000000<10000000000007<", "0000000F0000000UTO<<<<<<<<<<<8")]
   [InlineData(LineSeparator.None, "I<UTO000000000<01000000000003<", "0000000F0000000UTO<<<<<<<<<<<2")]
   [InlineData(LineSeparator.None, "I<UTO000000000<00100000000001<", "0000000F0000000UTO<<<<<<<<<<<4")]
   [InlineData(LineSeparator.None, "I<UTO000000000<00010000000007<", "0000000F0000000UTO<<<<<<<<<<<8")]
   [InlineData(LineSeparator.None, "I<UTO000000000<00001000000003<", "0000000F0000000UTO<<<<<<<<<<<2")]
   [InlineData(LineSeparator.None, "I<UTO000000000<00000100000001<", "0000000F0000000UTO<<<<<<<<<<<4")]
   [InlineData(LineSeparator.None, "I<UTO000000000<00000010000007<", "0000000F0000000UTO<<<<<<<<<<<8")]
   [InlineData(LineSeparator.None, "I<UTO000000000<00000001000003<", "0000000F0000000UTO<<<<<<<<<<<2")]
   [InlineData(LineSeparator.None, "I<UTO000000000<00000000100001<", "0000000F0000000UTO<<<<<<<<<<<4")]
   [InlineData(LineSeparator.None, "I<UTO000000000<00000000010007<", "0000000F0000000UTO<<<<<<<<<<<8")]
   [InlineData(LineSeparator.None, "I<UTO000000000<00000000001003<", "0000000F0000000UTO<<<<<<<<<<<2")]
   [InlineData(LineSeparator.None, "I<UTO000000000<00000000000101<", "0000000F0000000UTO<<<<<<<<<<<4")]
   [InlineData(LineSeparator.None, "I<UTO000000000<00000000000017<", "0000000F0000000UTO<<<<<<<<<<<8")]
   // Date of birth field
   [InlineData(LineSeparator.Crlf, "I<UTO0000000000<<<<<<<<<<<<<<<", "1000007F0000000UTO<<<<<<<<<<<4")]
   [InlineData(LineSeparator.Crlf, "I<UTO0000000000<<<<<<<<<<<<<<<", "0100003F0000000UTO<<<<<<<<<<<0")]
   [InlineData(LineSeparator.Crlf, "I<UTO0000000000<<<<<<<<<<<<<<<", "0010001F0000000UTO<<<<<<<<<<<0")]
   [InlineData(LineSeparator.Crlf, "I<UTO0000000000<<<<<<<<<<<<<<<", "0001007F0000000UTO<<<<<<<<<<<4")]
   [InlineData(LineSeparator.Crlf, "I<UTO0000000000<<<<<<<<<<<<<<<", "0000103F0000000UTO<<<<<<<<<<<0")]
   [InlineData(LineSeparator.Crlf, "I<UTO0000000000<<<<<<<<<<<<<<<", "0000011F0000000UTO<<<<<<<<<<<0")]
   // Date of expiry field
   [InlineData(LineSeparator.Lf,   "I<UTO0000000000<<<<<<<<<<<<<<<", "0000000F1000007UTO<<<<<<<<<<<8")]
   [InlineData(LineSeparator.Lf,   "I<UTO0000000000<<<<<<<<<<<<<<<", "0000000F0100003UTO<<<<<<<<<<<0")]
   [InlineData(LineSeparator.Lf,   "I<UTO0000000000<<<<<<<<<<<<<<<", "0000000F0010001UTO<<<<<<<<<<<4")]
   [InlineData(LineSeparator.Lf,   "I<UTO0000000000<<<<<<<<<<<<<<<", "0000000F0001007UTO<<<<<<<<<<<8")]
   [InlineData(LineSeparator.Lf,   "I<UTO0000000000<<<<<<<<<<<<<<<", "0000000F0000103UTO<<<<<<<<<<<0")]
   [InlineData(LineSeparator.Lf,   "I<UTO0000000000<<<<<<<<<<<<<<<", "0000000F0000011UTO<<<<<<<<<<<4")]
   public void Icao9303SizeTD1Algorithm_Validate_ShouldCorrectlyWeightByCharacterPosition(
      LineSeparator lineSeparator,
      String mrzFirstLine,
      String mrzSecondLine)
   {
      // Arrange.
      var sut = new Icao9303SizeTD1Algorithm()
      {
         LineSeparator = lineSeparator,
      };
      var value = GetTestValue(lineSeparator, mrzFirstLine, mrzSecondLine);

      // Act/assert.
      sut.Validate(value).Should().BeTrue();
   }

   [Theory]
   [InlineData(LineSeparator.None, "I<UTOD231458907<<<<<<<<<<<<<<<", "7408122F1204159UTO<<<<<<<<<<<6")]
   [InlineData(LineSeparator.Crlf, "I<UTOD231458907<<<<<<<<<<<<<<<", "7408122F1204159UTO<<<<<<<<<<<6")]
   [InlineData(LineSeparator.Lf,   "I<UTOD231458907<<<<<<<<<<<<<<<", "7408122F1204159UTO<<<<<<<<<<<6")]
   [InlineData(LineSeparator.None, "I<UTOD23145890<AB112234566<<<<", "7408122F1204159UTO<<<<<<<<<<<4")]
   [InlineData(LineSeparator.Crlf, "I<UTOD23145890<AB112234566<<<<", "7408122F1204159UTO<<<<<<<<<<<4")]
   [InlineData(LineSeparator.Lf,   "I<UTOD23145890<AB112234566<<<<", "7408122F1204159UTO<<<<<<<<<<<4")]
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnTrue_WhenValueContainsValidCheckDigits(
      LineSeparator lineSeparator,
      String mrzFirstLine,
      String mrzSecondLine)
   {
      // Arrange.
      var sut = new Icao9303SizeTD1Algorithm()
      {
         LineSeparator = lineSeparator,
      };
      var value = GetTestValue(lineSeparator, mrzFirstLine, mrzSecondLine);

      // Act/assert.
      sut.Validate(value).Should().BeTrue();
   }

   [Theory]
   [InlineData(LineSeparator.None, "I<UTOD23145890<00000000000007<", "7408122F1204159UTO<<<<<<<<<<<8")]
   [InlineData(LineSeparator.Crlf, "I<UTOD23145890<0000000000007<<", "7408122F1204159UTO<<<<<<<<<<<6")]
   [InlineData(LineSeparator.Lf,   "I<UTOD23145890<000000000007<<<", "7408122F1204159UTO<<<<<<<<<<<0")]
   [InlineData(LineSeparator.None, "I<UTOD23145890<00000000007<<<<", "7408122F1204159UTO<<<<<<<<<<<8")]
   [InlineData(LineSeparator.Crlf, "I<UTOD23145890<0000000007<<<<<", "7408122F1204159UTO<<<<<<<<<<<6")]
   [InlineData(LineSeparator.Lf,   "I<UTOD23145890<000000007<<<<<<", "7408122F1204159UTO<<<<<<<<<<<0")]
   [InlineData(LineSeparator.None, "I<UTOD23145890<00000007<<<<<<<", "7408122F1204159UTO<<<<<<<<<<<8")]
   [InlineData(LineSeparator.Crlf, "I<UTOD23145890<0000007<<<<<<<<", "7408122F1204159UTO<<<<<<<<<<<6")]
   [InlineData(LineSeparator.Lf,   "I<UTOD23145890<000007<<<<<<<<<", "7408122F1204159UTO<<<<<<<<<<<0")]
   [InlineData(LineSeparator.None, "I<UTOD23145890<00007<<<<<<<<<<", "7408122F1204159UTO<<<<<<<<<<<8")]
   [InlineData(LineSeparator.Crlf, "I<UTOD23145890<0007<<<<<<<<<<<", "7408122F1204159UTO<<<<<<<<<<<6")]
   [InlineData(LineSeparator.Lf,   "I<UTOD23145890<007<<<<<<<<<<<<", "7408122F1204159UTO<<<<<<<<<<<0")]
   [InlineData(LineSeparator.None, "I<UTOD23145890<07<<<<<<<<<<<<<", "7408122F1204159UTO<<<<<<<<<<<8")]
   [InlineData(LineSeparator.None, "I<UTOD23145890<AB112234566<<<<", "7408122F1204159UTO<<<<<<<<<<<4")]
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnTrue_WhenValueContainsExtendedDocumentNumberWithValidCheckDigits(
      LineSeparator lineSeparator,
      String mrzFirstLine,
      String mrzSecondLine)
   {
      // Arrange.
      var sut = new Icao9303SizeTD1Algorithm()
      {
         LineSeparator = lineSeparator,
      };
      var value = GetTestValue(lineSeparator, mrzFirstLine, mrzSecondLine);

      // Act/assert.
      sut.Validate(value).Should().BeTrue();
   }

   [Theory]
   [InlineData(LineSeparator.None, "I<UTOD2314589A7<<<<<<<<<<<<<<<", "7408122F1204159UTO<<<<<<<<<<<6")]  // D231458907 with single char transcription error (0 -> A) with delta 10
   [InlineData(LineSeparator.Crlf, "I<UTON2314589A7<<<<<<<<<<<<<<<", "7408122F1204159UTO<<<<<<<<<<<6")]  // D231458907 with single char transcription error (D -> N) with delta 10
   [InlineData(LineSeparator.None, "I<UTOL89890C236<<<<<<<<<<<<<<<", "7408122F1204159UTO<<<<<<<<<<<8")]  // L898902C36 with two char transposition error (2C -> C2) with delta 10
   [InlineData(LineSeparator.Lf,   "I<UTO0000000000<<<<<<<<<<<<<<<", "8812728F0000000UTO<<<<<<<<<<<0")]  // 8812278 with two char transposition error (27 -> 72) with delta 5
   [InlineData(LineSeparator.None, "I<UTO0000000000<<<<<<<<<<<<<<<", "0000000F8812728UTO<<<<<<<<<<<2")]  // 8812278 with two char transposition error (27 -> 72) with delta 5
   [InlineData(LineSeparator.Crlf, "I<UTOD23145890<0B112234566<<<<", "7408122F1204159UTO<<<<<<<<<<<4")]  // Extended document number AB112234566 with single char transcription error (A -> 0) with delta 10
   [InlineData(LineSeparator.Lf,   "I<UTOD23145890<AB112283568<<<<", "7408122F1204159UTO<<<<<<<<<<<2")]  // Extended document number AB112238568 with two char transposition error (38 -> 83) with delta 5
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnTrue_WhenValueContainsUndetectableError(
      LineSeparator lineSeparator,
      String mrzFirstLine,
      String mrzSecondLine)
   {
      // Arrange.
      var sut = new Icao9303SizeTD1Algorithm()
      {
         LineSeparator = lineSeparator,
      };
      var value = GetTestValue(lineSeparator, mrzFirstLine, mrzSecondLine);

      // Act/assert.
      sut.Validate(value).Should().BeTrue();
   }

   public static TheoryData<LineSeparator, String, String> DetectableErrorData = new()
   {
      { LineSeparator.None, "I<UTOE231458907<<<<<<<<<<<<<<<", _mrzSecondLine },     // D231458907 with single character transcription error (D -> E)
      { LineSeparator.None, "I<UTOD241458907<<<<<<<<<<<<<<<", _mrzSecondLine },     // D231458907 with single digit transcription error (3 -> 4)
      { LineSeparator.Crlf, _mrzFirstLine, "7409122F1204159UTO<<<<<<<<<<<6" },      // 7408122 with single digit transcription error (8 -> 9)
      { LineSeparator.Crlf, _mrzFirstLine, "7409122F1203159UTO<<<<<<<<<<<6" },      // 1204159 with single digit transcription error (4 -> 3)
      { LineSeparator.None, "I<UTO2D31458907<<<<<<<<<<<<<<<", _mrzSecondLine },     // D231458907 with two character transposition error (D2 -> 2D)
      { LineSeparator.None, "I<UTOD231548907<<<<<<<<<<<<<<<", _mrzSecondLine },     // D231458907 with two digit transposition error (45 -> 54)
      { LineSeparator.Crlf, _mrzFirstLine, "7408212F1204159UTO<<<<<<<<<<<6" },      // 7408122 with two digit transposition error (12 -> 21)
      { LineSeparator.Crlf, _mrzFirstLine, "7409122F1201459UTO<<<<<<<<<<<6" },      // 1204159 with two digit transposition error (41 -> 14)
      { LineSeparator.None, "I<UTOD23145890<AC112234566<<<<", "7408122F1204159UTO<<<<<<<<<<<4" },     // Extended document number AB112234566 with single character transcription error (B -> C)
   };

   [Theory]
   [MemberData(nameof(DetectableErrorData))]
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnFalse_WhenValueContainsDetectableError(
      LineSeparator lineSeparator,
      String mrzFirstLine,
      String mrzSecondLine)
   {
      // Arrange.
      var sut = new Icao9303SizeTD1Algorithm()
      {
         LineSeparator = lineSeparator,
      };
      var value = GetTestValue(lineSeparator, mrzFirstLine, mrzSecondLine);

      // Act/assert.
      sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   [InlineData(LineSeparator.None, "I<UTO0000000000<<<<<<<<<<<<<<<", "0000000F0000000UTO<<<<<<<<<<<0")]
   [InlineData(LineSeparator.Crlf, "I<UTO000000000<00000000000000<", "0000000F0000000UTO<<<<<<<<<<<0")]
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnTrue_WhenFieldsAreAllZeros(
      LineSeparator lineSeparator,
      String mrzFirstLine,
      String mrzSecondLine)
   {
      // Arrange.
      var sut = new Icao9303SizeTD1Algorithm()
      {
         LineSeparator = lineSeparator,
      };
      var value = GetTestValue(lineSeparator, mrzFirstLine, mrzSecondLine);

      // Act/assert.
      sut.Validate(value).Should().BeTrue();
   }

   [Fact]
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnTrue_WhenFieldsAreAllFillerCharacters()
   {
      // Arrange.
      var value = GetTestValue(
         LineSeparator.None,
         "I<UTO<<<<<<<<<0<<<<<<<<<<<<<<<",
         "<<<<<<0F<<<<<<0UTO<<<<<<<<<<<0");

      // Act/assert.
      _sut.Validate(value).Should().BeTrue();
   }

   public static TheoryData<LineSeparator, String, String> InvalidCharacterData = new()
   {
      { LineSeparator.None, "I<UTOb231458907<<<<<<<<<<<<<<<", _mrzSecondLine },     // D231458907 with D replaced with character 30 positions later in ASCII table
      { LineSeparator.Crlf, "I<UTOD2314589&7<<<<<<<<<<<<<<<", _mrzSecondLine },     // D231458907 with 0 replaced with character 10 positions before in ASCII table
      { LineSeparator.Lf,   "I<UTOD2314589:7<<<<<<<<<<<<<<<", _mrzSecondLine },     // D231458907 with 0 replaced with character 10 positions later in ASCII table
      { LineSeparator.None, _mrzFirstLine, "7>08122F1204159UTO<<<<<<<<<<<6" },      // 7408122 with 4 replaced with character 10 positions later in ASCII table
      { LineSeparator.None, "I<UTOD23145890<A&112234566<<<<", "7408122F1204159UTO<<<<<<<<<<<4" },     // Extended document number AB112234566 with B replaced by invalid character
      { LineSeparator.None, "I<UTOD23145890<A:112234566<<<<", "7408122F1204159UTO<<<<<<<<<<<4" },     // Extended document number AB112234566 with B replaced by invalid character
      { LineSeparator.None, "I<UTOD23145890<A[112234566<<<<", "7408122F1204159UTO<<<<<<<<<<<4" },     // Extended document number AB112234566 with B replaced by invalid character
   };

   [Theory]
   [MemberData(nameof(InvalidCharacterData))]
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnFalse_WhenFieldContainsInvalidCharacter(
      LineSeparator lineSeparator,
      String mrzFirstLine,
      String mrzSecondLine)
   {
      // Arrange.
      var sut = new Icao9303SizeTD1Algorithm()
      {
         LineSeparator = lineSeparator,
      };
      var value = GetTestValue(lineSeparator, mrzFirstLine, mrzSecondLine);

      // Act/assert.
      sut.Validate(value).Should().BeFalse();
   }

   public static TheoryData<LineSeparator, String, String> InvalidCheckDigitCharacterData = new()
   {
      { LineSeparator.None, "I<UTOD23145890A<<<<<<<<<<<<<<<", _mrzSecondLine },     // Document number check digit is invalid character 
      { LineSeparator.None, "I<UTOD23145890&<<<<<<<<<<<<<<<", _mrzSecondLine },     // Document number check digit is invalid character
      { LineSeparator.None, "I<UTOD23145890:<<<<<<<<<<<<<<<", _mrzSecondLine },     // Document number check digit is invalid character
      { LineSeparator.Crlf, _mrzFirstLine, "740812AF1204159UTO<<<<<<<<<<<6" },      // Date of birth check digit is invalid character
      { LineSeparator.Crlf, _mrzFirstLine, "740812&F1204159UTO<<<<<<<<<<<6" },      // Date of birth check digit is invalid character
      { LineSeparator.Crlf, _mrzFirstLine, "740812<F1204159UTO<<<<<<<<<<<6" },      // Date of birth check digit is invalid character
      { LineSeparator.Crlf, _mrzFirstLine, "740812[F1204159UTO<<<<<<<<<<<6" },      // Date of birth check digit is invalid character
      { LineSeparator.Lf,   _mrzFirstLine, "7408122F120415AUTO<<<<<<<<<<<6" },      // Date of expiry check digit is invalid character
      { LineSeparator.Lf,   _mrzFirstLine, "7408122F120415&UTO<<<<<<<<<<<6" },      // Date of expiry check digit is invalid character
      { LineSeparator.Lf,   _mrzFirstLine, "7408122F120415<UTO<<<<<<<<<<<6" },      // Date of expiry check digit is invalid character
      { LineSeparator.Lf,   _mrzFirstLine, "7408122F120415[UTO<<<<<<<<<<<6" },      // Date of expiry check digit is invalid character
      { LineSeparator.None, "I<UTOD23145890<AB11223456A<<<<", "7408122F1204159UTO<<<<<<<<<<<4" },     // Extended document number check digit with invalid character
      { LineSeparator.None, "I<UTOD23145890<AB11223456&<<<<", "7408122F1204159UTO<<<<<<<<<<<4" },     // Extended document number check digit with invalid character
      { LineSeparator.None, "I<UTOD23145890<AB11223456<<<<<", "7408122F1204159UTO<<<<<<<<<<<4" },     // Extended document number check digit with invalid character
      { LineSeparator.None, "I<UTOD23145890<AB11223456[<<<<", "7408122F1204159UTO<<<<<<<<<<<4" },     // Extended document number check digit with invalid character
   };

   [Theory]
   [MemberData(nameof(InvalidCheckDigitCharacterData))]
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnFalse_WhenRequiredFieldCheckDigitContainsNonDigitCharacter(
      LineSeparator lineSeparator,
      String mrzFirstLine,
      String mrzSecondLine)
   {
      // Arrange.
      var sut = new Icao9303SizeTD1Algorithm()
      {
         LineSeparator = lineSeparator,
      };
      var value = GetTestValue(lineSeparator, mrzFirstLine, mrzSecondLine);

      // Act/assert.
      sut.Validate(value).Should().BeFalse();
   }

   #endregion
}
