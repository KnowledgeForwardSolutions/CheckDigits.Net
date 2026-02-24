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

   private const String _lineSeparatorNone = "";
   private const String _lineSeparatorCrlf = "\r\n";
   private const String _lineSeparatorLf = "\n";

   private static String GetTestValueOld(
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

   private static String GetTestValue(
      String documentCode = "I<",
      String issuingState = "UTO",
      String documentNumber = "D231458907",              // 9 alphanumeric characters + 1 check digit
      String optionalData = "<<<<<<<<<<<<<<<",           // possible extended document number
      String lineSeparator = _lineSeparatorNone,
      String dateOfBirth = "7408122",                    // 6 digit characters + 1 check digit
      String sex = "F",
      String dateOfExpiry = "1204159",                   // 6 digit characters + 1 check digit
      String nationality = "UTO",
      String additionalOptionalData = "<<<<<<<<<<<",
      String compositeCheckDigit = "6",
      String? secondLineSeparator = null,
      String name = "ERIKSSON<<ANNA<MARIA<<<<<<<<<<")
      => $"{documentCode}{issuingState}{documentNumber}{optionalData}" +
         lineSeparator +
         $"{dateOfBirth}{sex}{dateOfExpiry}{nationality}{additionalOptionalData}{compositeCheckDigit}" +
         (secondLineSeparator ?? lineSeparator) +
         name;

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
   [Theory]
   [InlineData("<<<<<<<<<<<<<<", _lineSeparatorNone, "<<<<<<<<<<<", _lineSeparatorNone)]        // Optional data with 14 characters instead of 15 and empty line separator for total length of 59 instead of 60
   [InlineData("<<<<<<<<<<<<<<<<", _lineSeparatorCrlf, "<<<<<<<<<<<", _lineSeparatorCrlf)]      // Optional data with 16 characters instead of 15 and CRLF line separator for total length of 65 instead of 64
   [InlineData("<<<<<<<<<<<<<<", _lineSeparatorLf, "<<<<<<<<<<<", _lineSeparatorLf)]            // Optional data with 14 characters instead of 15 and LF line separator for total length of 61 instead of 62
   [InlineData("<<<<<<<<<<<<<<<<", _lineSeparatorLf, "<<<<<<<<<<<", _lineSeparatorLf)]          // Optional data with 16 characters instead of 15 and LF line separator for total length of 63 instead of 62
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnFalse_WhenValueIsInvalidLength(
      String optionalData,
      String lineSeparator,
      String additionalOptionalData,
      String secondLineSeparator)
   {
      // Arrange.
      var value = GetTestValue(
         optionalData: optionalData,
         lineSeparator: lineSeparator,
         additionalOptionalData: additionalOptionalData,
         secondLineSeparator: secondLineSeparator);

      // Act/assert.
      _sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   [InlineData("X\n" ,             _lineSeparatorCrlf)]        // 'X' instead of \r
   [InlineData(_lineSeparatorCrlf, "X\n" )]                    // 'X' instead of \r
   [InlineData(" \n" ,             _lineSeparatorCrlf)]        // Space instead of \r
   [InlineData(_lineSeparatorCrlf, " \n" )]                    // Space instead of \r
   [InlineData("\r " ,             _lineSeparatorCrlf)]        // Space instead of \n
   [InlineData(_lineSeparatorCrlf, "\r " )]                    // Space instead of \n
   [InlineData("\n\r",             _lineSeparatorCrlf)]        // \n\r instead of \r\n
   [InlineData(_lineSeparatorCrlf, "\n\r")]                    // \n\r instead of \r\n
   [InlineData(" " ,               _lineSeparatorLf)]          // Space instead of \n
   [InlineData(_lineSeparatorLf,   " ")]                       // Space instead of \n
   [InlineData(_lineSeparatorCrlf, _lineSeparatorLf)]          // Mixed line separators
   [InlineData(_lineSeparatorLf,   _lineSeparatorCrlf)]        // Mixed line separators
   [InlineData(_lineSeparatorNone, _lineSeparatorLf)]          // Mixed line separators
   [InlineData(_lineSeparatorLf,   _lineSeparatorNone)]        // Mixed line separators
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnFalse_WhenSeparatorCharactersAreInvalid(
      String lineSeparator,
      String secondLineSeparator)
   {
      // Arrange.
      var value = GetTestValue(
         lineSeparator: lineSeparator,
         secondLineSeparator: secondLineSeparator);

      // Act/assert.
      _sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   [InlineData(_lineSeparatorNone)]
   [InlineData(_lineSeparatorCrlf)]
   [InlineData(_lineSeparatorLf)]
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnTrue_WhenLineSeparatorIsValid(String lineSeparator)
   {
      // Arrange.
      var value = GetTestValue(lineSeparator: lineSeparator);

      // Act/assert.
      _sut.Validate(value).Should().BeTrue();
   }

   [Theory]
   // Third posiition has weight = 1, so easier to calculate field check digit and composite check digit
   [InlineData("0000000000", "0")]
   [InlineData("0010000001", "8")]
   [InlineData("0020000002", "6")]
   [InlineData("0030000003", "4")]
   [InlineData("0040000004", "2")]
   [InlineData("0050000005", "0")]
   [InlineData("0060000006", "8")]
   [InlineData("0070000007", "6")]
   [InlineData("0080000008", "4")]
   [InlineData("0090000009", "2")]
   [InlineData("00A0000000", "0")]
   [InlineData("00B0000001", "8")]
   [InlineData("00C0000002", "6")]
   [InlineData("00D0000003", "4")]
   [InlineData("00E0000004", "2")]
   [InlineData("00F0000005", "0")]
   [InlineData("00G0000006", "8")]
   [InlineData("00H0000007", "6")]
   [InlineData("00I0000008", "4")]
   [InlineData("00J0000009", "2")]
   [InlineData("00K0000000", "0")]
   [InlineData("00L0000001", "8")]
   [InlineData("00M0000002", "6")]
   [InlineData("00N0000003", "4")]
   [InlineData("00O0000004", "2")]
   [InlineData("00P0000005", "0")]
   [InlineData("00Q0000006", "8")]
   [InlineData("00R0000007", "6")]
   [InlineData("00S0000008", "4")]
   [InlineData("00T0000009", "2")]
   [InlineData("00U0000000", "0")]
   [InlineData("00V0000001", "8")]
   [InlineData("00W0000002", "6")]
   [InlineData("00X0000003", "4")]
   [InlineData("00Y0000004", "2")]
   [InlineData("00Z0000005", "0")]
   [InlineData("00<0000000", "0")]
   public void Icao9303SizeTD1Algorithm_Validate_ShouldCorrectlyMapCharactersToIntegerEquivalents(
      String documentNumber,
      String compositeCheckDigit)
   {
      // Arrange.
      var value = GetTestValue(
         documentNumber: documentNumber,
         dateOfBirth: "0000000",
         dateOfExpiry: "0000000",
         compositeCheckDigit: compositeCheckDigit);

      // Act/assert.
      _sut.Validate(value).Should().BeTrue();
   }

   [Theory]
   // Check digits are accurate, if equivalent uppercase character
   [InlineData("00a0000000", "0")]
   [InlineData("00b0000001", "8")]
   [InlineData("00c0000002", "6")]
   [InlineData("00d0000003", "4")]
   [InlineData("00e0000004", "2")]
   [InlineData("00f0000005", "0")]
   [InlineData("00g0000006", "8")]
   [InlineData("00h0000007", "6")]
   [InlineData("00i0000008", "4")]
   [InlineData("00j0000009", "2")]
   [InlineData("00k0000000", "0")]
   [InlineData("00l0000001", "8")]
   [InlineData("00m0000002", "6")]
   [InlineData("00n0000003", "4")]
   [InlineData("00o0000004", "2")]
   [InlineData("00p0000005", "0")]
   [InlineData("00q0000006", "8")]
   [InlineData("00r0000007", "6")]
   [InlineData("00s0000008", "4")]
   [InlineData("00t0000009", "2")]
   [InlineData("00u0000000", "0")]
   [InlineData("00v0000001", "8")]
   [InlineData("00w0000002", "6")]
   [InlineData("00x0000003", "4")]
   [InlineData("00y0000004", "2")]
   [InlineData("00z0000005", "0")]
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnFalse_WhenLowerCaseAlphabeticCharacterEncountered(
      String documentNumber,
      String compositeCheckDigit)
   {
      // Arrange.
      var value = GetTestValue(
         documentNumber: documentNumber,
         dateOfBirth: "0000000",
         dateOfExpiry: "0000000",
         compositeCheckDigit: compositeCheckDigit);

      // Act/assert.
      _sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   // Check digits would be correct if alpha characters allowed
   [InlineData("00A0000", "0000000", "0")]
   [InlineData("00a0000", "0000000", "0")]
   [InlineData("0000000", "00A0000", "0")]
   [InlineData("0000000", "00a0000", "0")]
   public void Icao9303SizeTD2Algorithm_Validate_ShouldReturnFalse_NumericFieldContainsAlphabeticCharacter(
      String dateOfBirth,
      String dateOfExpiry,
      String compositeCheckDigit)
   {
      // Arrange.
      var value = GetTestValue(
         documentNumber: "0000000000",
         dateOfBirth: dateOfBirth,
         dateOfExpiry: dateOfExpiry,
         compositeCheckDigit: compositeCheckDigit);

      // Act/assert.
      _sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   // Document number field
   [InlineData("1000000007", "<<<<<<<<<<<<<<<", "0000000", "0000000", "6")]
   [InlineData("0100000003", "<<<<<<<<<<<<<<<", "0000000", "0000000", "4")]
   [InlineData("0010000001", "<<<<<<<<<<<<<<<", "0000000", "0000000", "8")]
   [InlineData("0001000007", "<<<<<<<<<<<<<<<", "0000000", "0000000", "6")]
   [InlineData("0000100003", "<<<<<<<<<<<<<<<", "0000000", "0000000", "4")]
   [InlineData("0000010001", "<<<<<<<<<<<<<<<", "0000000", "0000000", "8")]
   [InlineData("0000001007", "<<<<<<<<<<<<<<<", "0000000", "0000000", "6")]
   [InlineData("0000000103", "<<<<<<<<<<<<<<<", "0000000", "0000000", "4")]
   [InlineData("0000000011", "<<<<<<<<<<<<<<<", "0000000", "0000000", "8")]
   // Extended document number
   [InlineData("000000000<", "10000000000007<", "0000000", "0000000", "8")]
   [InlineData("000000000<", "01000000000003<", "0000000", "0000000", "2")]
   [InlineData("000000000<", "00100000000001<", "0000000", "0000000", "4")]
   [InlineData("000000000<", "00010000000007<", "0000000", "0000000", "8")]
   [InlineData("000000000<", "00001000000003<", "0000000", "0000000", "2")]
   [InlineData("000000000<", "00000100000001<", "0000000", "0000000", "4")]
   [InlineData("000000000<", "00000010000007<", "0000000", "0000000", "8")]
   [InlineData("000000000<", "00000001000003<", "0000000", "0000000", "2")]
   [InlineData("000000000<", "00000000100001<", "0000000", "0000000", "4")]
   [InlineData("000000000<", "00000000010007<", "0000000", "0000000", "8")]
   [InlineData("000000000<", "00000000001003<", "0000000", "0000000", "2")]
   [InlineData("000000000<", "00000000000101<", "0000000", "0000000", "4")]
   [InlineData("000000000<", "00000000000017<", "0000000", "0000000", "8")]
   // Date of birth field
   [InlineData("0000000000", "<<<<<<<<<<<<<<<", "1000007", "0000000", "4")]
   [InlineData("0000000000", "<<<<<<<<<<<<<<<", "0100003", "0000000", "0")]
   [InlineData("0000000000", "<<<<<<<<<<<<<<<", "0010001", "0000000", "0")]
   [InlineData("0000000000", "<<<<<<<<<<<<<<<", "0001007", "0000000", "4")]
   [InlineData("0000000000", "<<<<<<<<<<<<<<<", "0000103", "0000000", "0")]
   [InlineData("0000000000", "<<<<<<<<<<<<<<<", "0000011", "0000000", "0")]
   // Date of expiry field
   [InlineData("0000000000", "<<<<<<<<<<<<<<<", "0000000", "1000007", "8")]
   [InlineData("0000000000", "<<<<<<<<<<<<<<<", "0000000", "0100003", "0")]
   [InlineData("0000000000", "<<<<<<<<<<<<<<<", "0000000", "0010001", "4")]
   [InlineData("0000000000", "<<<<<<<<<<<<<<<", "0000000", "0001007", "8")]
   [InlineData("0000000000", "<<<<<<<<<<<<<<<", "0000000", "0000103", "0")]
   [InlineData("0000000000", "<<<<<<<<<<<<<<<", "0000000", "0000011", "4")]
   public void Icao9303SizeTD1Algorithm_Validate_ShouldCorrectlyWeightByCharacterPosition(
      String documentNumber,
      String optionalData,
      String dateOfBirth,
      String dateOfExpiry,
      String compositeCheckDigit)
   {
      // Arrange.
      var value = GetTestValue(
         documentNumber: documentNumber,
         optionalData: optionalData,
         lineSeparator: _lineSeparatorNone,
         dateOfBirth: dateOfBirth,
         dateOfExpiry: dateOfExpiry,
         compositeCheckDigit: compositeCheckDigit);

      // Act/assert.
      _sut.Validate(value).Should().BeTrue();
   }

   [Theory]
   [InlineData("D231458907", "<<<<<<<<<<<<<<<", "7408122", "1204159", "6")]
   [InlineData("D23145890<", "AB112234566<<<<", "7408122", "1204159", "4")]
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnTrue_WhenValueContainsValidCheckDigits(
      String documentNumber,
      String optionalData,
      String dateOfBirth,
      String dateOfExpiry,
      String compositeCheckDigit)
   {
      // Arrange.
      var value = GetTestValue(
         documentNumber: documentNumber,
         optionalData: optionalData,
         lineSeparator: _lineSeparatorCrlf,
         dateOfBirth: dateOfBirth,
         dateOfExpiry: dateOfExpiry,
         compositeCheckDigit: compositeCheckDigit);

      // Act/assert.
      _sut.Validate(value).Should().BeTrue();
   }

   [Theory]
   [InlineData("D23145890<", "00000000000007<", "7408122", "1204159", "8")]
   [InlineData("D23145890<", "0000000000007<<", "7408122", "1204159", "6")]
   [InlineData("D23145890<", "000000000007<<<", "7408122", "1204159", "0")]
   [InlineData("D23145890<", "00000000007<<<<", "7408122", "1204159", "8")]
   [InlineData("D23145890<", "0000000007<<<<<", "7408122", "1204159", "6")]
   [InlineData("D23145890<", "000000007<<<<<<", "7408122", "1204159", "0")]
   [InlineData("D23145890<", "00000007<<<<<<<", "7408122", "1204159", "8")]
   [InlineData("D23145890<", "0000007<<<<<<<<", "7408122", "1204159", "6")]
   [InlineData("D23145890<", "000007<<<<<<<<<", "7408122", "1204159", "0")]
   [InlineData("D23145890<", "00007<<<<<<<<<<", "7408122", "1204159", "8")]
   [InlineData("D23145890<", "0007<<<<<<<<<<<", "7408122", "1204159", "6")]
   [InlineData("D23145890<", "007<<<<<<<<<<<<", "7408122", "1204159", "0")]
   [InlineData("D23145890<", "07<<<<<<<<<<<<<", "7408122", "1204159", "8")]
   [InlineData("D23145890<", "AB112234566<<<<", "7408122", "1204159", "4")]
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnTrue_WhenValueContainsExtendedDocumentNumberWithValidCheckDigits(
      String documentNumber,
      String optionalData,
      String dateOfBirth,
      String dateOfExpiry,
      String compositeCheckDigit)
   {
      // Arrange.
      var value = GetTestValue(
         documentNumber: documentNumber,
         optionalData: optionalData,
         lineSeparator: _lineSeparatorLf,
         dateOfBirth: dateOfBirth,
         dateOfExpiry: dateOfExpiry,
         compositeCheckDigit: compositeCheckDigit);

      // Act/assert.
      _sut.Validate(value).Should().BeTrue();
   }

   [Theory]
   [InlineData("D2314589A7", "<<<<<<<<<<<<<<<", "7408122", "1204159", "6")]   // Document number D231458907 with single char transcription error (0 -> A) with delta 10
   [InlineData("N2314589A7", "<<<<<<<<<<<<<<<", "7408122", "1204159", "6")]   // Document number D231458907 with single char transcription error (D -> N) with delta 10
   [InlineData("L89890C236", "<<<<<<<<<<<<<<<", "7408122", "1204159", "8")]   // Document number L898902C36 with two char transposition error (2C -> C2) with delta 10
   [InlineData("0000000000", "<<<<<<<<<<<<<<<", "8812728", "0000000", "0")]   // Date of birth 8812278 with two char transposition error (27 -> 72) with delta 5
   [InlineData("0000000000", "<<<<<<<<<<<<<<<", "0000000", "8812728", "2")]   // Date of expiry 8812278 with two char transposition error (27 -> 72) with delta 5
   [InlineData("D23145890<", "0B112234566<<<<", "7408122", "1204159", "4")]   // Extended document number AB112234566 with single char transcription error (A -> 0) with delta 10
   [InlineData("D23145890<", "AB112283568<<<<", "7408122", "1204159", "2")]   // Extended document number AB112238568 with two char transposition error (38 -> 83) with delta 5
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnTrue_WhenValueContainsUndetectableError(
      String documentNumber,
      String optionalData,
      String dateOfBirth,
      String dateOfExpiry,
      String compositeCheckDigit)
   {
      // Arrange.
      var value = GetTestValue(
         documentNumber: documentNumber,
         optionalData: optionalData,
         dateOfBirth: dateOfBirth,
         dateOfExpiry: dateOfExpiry,
         compositeCheckDigit: compositeCheckDigit);

      // Act/assert.
      _sut.Validate(value).Should().BeTrue();
   }

   [Theory]
   [InlineData("E231458907", "<<<<<<<<<<<<<<<", "7408122", "1204159", "6")]   // Document number D231458907 with single character transcription error (D -> E)
   [InlineData("D241458907", "<<<<<<<<<<<<<<<", "7408122", "1204159", "6")]   // Document number D231458907 with single digit transcription error (3 -> 4)
   [InlineData("D231458907", "<<<<<<<<<<<<<<<", "7409122", "1204159", "6")]   // Date of birth 7408122 with single digit transcription error (8 -> 9)
   [InlineData("D231458907", "<<<<<<<<<<<<<<<", "7409122", "1203159", "6")]   // Date of expiry 1204159 with single digit transcription error (4 -> 3)
   [InlineData("2D31458907", "<<<<<<<<<<<<<<<", "7408122", "1204159", "6")]   // Document number D231458907 with two character transposition error (D2 -> 2D)
   [InlineData("D231548907", "<<<<<<<<<<<<<<<", "7408122", "1204159", "6")]   // Document number D231458907 with two digit transposition error (45 -> 54)
   [InlineData("D231458907", "<<<<<<<<<<<<<<<", "7408212", "1204159", "6")]   // Date of birth 7408122 with two digit transposition error (12 -> 21)
   [InlineData("D231458907", "<<<<<<<<<<<<<<<", "7409122", "1201459", "6")]   // Date of expiry 1204159 with two digit transposition error (41 -> 14)
   [InlineData("D23145890<", "AC112234566<<<<", "7408122", "1204159", "4")]   // Extended document number AB112234566 with single character transcription error (B -> C)
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnFalse_WhenValueContainsDetectableError(
      String documentNumber,
      String optionalData,
      String dateOfBirth,
      String dateOfExpiry,
      String compositeCheckDigit)
   {
      // Arrange.
      var value = GetTestValue(
         documentNumber: documentNumber,
         optionalData: optionalData,
         dateOfBirth: dateOfBirth,
         dateOfExpiry: dateOfExpiry,
         compositeCheckDigit: compositeCheckDigit);

      // Act/assert.
      _sut.Validate(value).Should().BeFalse();
   }

   [Fact]
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnFalse_WhenFieldCheckDigitsAreValidButCompositeCheckDigitIsNotValid()
   {
      // Arrange.
      var value = GetTestValue(compositeCheckDigit: "7");   // Composite check digit should be 6 based on other field values

      // Act/assert.
      _sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   [InlineData("0000000000", "<<<<<<<<<<<<<<<", "0000000", "0000000", "0")]
   [InlineData("000000000<", "00000000000000<", "0000000", "0000000", "0")]   // Extended document number requires at least one trailing filler character
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnTrue_WhenFieldsAreAllZeros(
      String documentNumber,
      String optionalData,
      String dateOfBirth,
      String dateOfExpiry,
      String compositeCheckDigit)
   {
      // Arrange.
      var value = GetTestValue(
         documentNumber: documentNumber,
         optionalData: optionalData,
         dateOfBirth: dateOfBirth,
         dateOfExpiry: dateOfExpiry,
         compositeCheckDigit: compositeCheckDigit);

      // Act/assert.
      _sut.Validate(value).Should().BeTrue();
   }


   [Theory]
   [InlineData("<<<<<<<<<0", "<<<<<<<<<<<<<<<", "<<<<<<0", "<<<<<<0", "0")]
   [InlineData("<<<<<<<<<<", "0<<<<<<<<<<<<<<", "<<<<<<0", "<<<<<<0", "0")]   // If using extended document number then must have at least one digit (the check digit) before first filler char 
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnTrue_WhenFieldsAreAllFillerCharacters(
      String documentNumber,
      String optionalData,
      String dateOfBirth,
      String dateOfExpiry,
      String compositeCheckDigit)
   {
      // Arrange.
      var value = GetTestValue(
         documentNumber: documentNumber,
         optionalData: optionalData,
         dateOfBirth: dateOfBirth,
         dateOfExpiry: dateOfExpiry,
         compositeCheckDigit: compositeCheckDigit);

      // Act/assert.
      _sut.Validate(value).Should().BeTrue();
   }

   [Theory]
   [InlineData("b231458907", "<<<<<<<<<<<<<<<", "7408122", "1204159", "6")]   // Document number D231458907 with D replaced with character 30 positions later in ASCII table
   [InlineData("D2314589&7", "<<<<<<<<<<<<<<<", "7408122", "1204159", "6")]   // Document number D231458907 with 0 replaced with character 10 positions before in ASCII table
   [InlineData("D2314589:7", "<<<<<<<<<<<<<<<", "7408122", "1204159", "6")]   // Document number D231458907 with 0 replaced with character 10 positions later in ASCII table
   [InlineData("D231458907", "<<<<<<<<<<<<<<<", "7>08122", "1204159", "6")]   // Date of birth 7408122 with 4 replaced with character 10 positions later in ASCII table
   [InlineData("D231458907", "<<<<<<<<<<<<<<<", "7408122", "120>159", "6")]   // Date of expiry 1204159 with 4 replaced with character 10 positions later in ASCII table
   [InlineData("D23145890<", "A&112234566<<<<", "7408122", "1204159", "4")]   // Extended document number AB112234566 with B replaced by invalid character
   [InlineData("D23145890<", "A:112234566<<<<", "7408122", "1204159", "4")]   // Extended document number AB112234566 with B replaced by invalid character
   [InlineData("D23145890<", "A[112234566<<<<", "7408122", "1204159", "4")]   // Extended document number AB112234566 with B replaced by invalid character
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnFalse_WhenFieldContainsInvalidCharacter(
      String documentNumber,
      String optionalData,
      String dateOfBirth,
      String dateOfExpiry,
      String compositeCheckDigit)
   {
      // Arrange.
      var value = GetTestValue(
         documentNumber: documentNumber,
         optionalData: optionalData,
         dateOfBirth: dateOfBirth,
         dateOfExpiry: dateOfExpiry,
         compositeCheckDigit: compositeCheckDigit);

      // Act/assert.
      _sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   [InlineData("D23145890A", "<<<<<<<<<<<<<<<", "7408122", "1204159", "6")]   // Document number check digit is invalid character 
   [InlineData("D23145890&", "<<<<<<<<<<<<<<<", "7408122", "1204159", "6")]   // Document number check digit is invalid character
   [InlineData("D23145890:", "<<<<<<<<<<<<<<<", "7408122", "1204159", "6")]   // Document number check digit is invalid character
   [InlineData("D231458907", "<<<<<<<<<<<<<<<", "740812A", "1204159", "6")]   // Date of birth check digit is invalid character
   [InlineData("D231458907", "<<<<<<<<<<<<<<<", "740812&", "1204159", "6")]   // Date of birth check digit is invalid character
   [InlineData("D231458907", "<<<<<<<<<<<<<<<", "740812<", "1204159", "6")]   // Date of birth check digit is invalid character
   [InlineData("D231458907", "<<<<<<<<<<<<<<<", "740812[", "1204159", "6")]   // Date of birth check digit is invalid character
   [InlineData("D231458907", "<<<<<<<<<<<<<<<", "7408122", "120415A", "6")]   // Date of expiry check digit is invalid character
   [InlineData("D231458907", "<<<<<<<<<<<<<<<", "7408122", "120415&", "6")]   // Date of expiry check digit is invalid character
   [InlineData("D231458907", "<<<<<<<<<<<<<<<", "7408122", "120415<", "6")]   // Date of expiry check digit is invalid character
   [InlineData("D231458907", "<<<<<<<<<<<<<<<", "7408122", "120415[", "6")]   // Date of expiry check digit is invalid character
   [InlineData("D23145890<", "AB11223456A<<<<", "7408122", "1204159", "4")]   // Extended document number check digit with invalid character
   [InlineData("D23145890<", "AB11223456&<<<<", "7408122", "1204159", "4")]   // Extended document number check digit with invalid character
   [InlineData("D23145890<", "AB11223456<<<<<", "7408122", "1204159", "4")]   // Extended document number check digit with invalid character
   [InlineData("D23145890<", "AB11223456[<<<<", "7408122", "1204159", "4")]   // Extended document number check digit with invalid character
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnFalse_WhenRequiredFieldCheckDigitContainsNonDigitCharacter(
      String documentNumber,
      String optionalData,
      String dateOfBirth,
      String dateOfExpiry,
      String compositeCheckDigit)
   {
      // Arrange.
      var value = GetTestValue(
         documentNumber: documentNumber,
         optionalData: optionalData,
         dateOfBirth: dateOfBirth,
         dateOfExpiry: dateOfExpiry,
         compositeCheckDigit: compositeCheckDigit);

      // Act/assert.
      _sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   [InlineData("A")]
   [InlineData("&")]
   [InlineData("<")]
   [InlineData("[")]
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnFalse_WhenCompositeCheckDigitContainsNonDigitCharacter(String compositeCheckDigit)
   {
      // Arrange.
      var value = GetTestValue(compositeCheckDigit: compositeCheckDigit);

      // Act/assert.
      _sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   [InlineData("I<UTOD231458907<<<<<<<<<<<<<<<7408122F1204159UTO<<<<<<<<<<<6ERIKSSON<<ANNA<MARIA<<<<<<<<A<")]
   [InlineData("I<UTOD23145890<AB112234566<<<<\r\n7408122F1204159UTO<<<<<<<<<<<4\r\nERIKSSON<<ANNA<MARIA<<<<<<<<B<")]
   [InlineData("I<UTOSTARWARS45<<<<<<<<<<<<<<<\n7705256M2405252UTO<<<<<<<<<<<4\nSKYWALKER<<LUKE<<<<<<<<<<<<<C<")]
   public void Icao9303SizeTD2Algorithm_Validate_ShouldReturnTrue_ForBenchmarkValues(String value)
      => _sut.Validate(value).Should().BeTrue();

   #endregion
}
