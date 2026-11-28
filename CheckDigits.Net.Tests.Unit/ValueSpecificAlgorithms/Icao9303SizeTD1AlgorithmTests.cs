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
      #if NET8_0_OR_GREATER
      String? secondLineSeparator = null,
      #else
      String secondLineSeparator = null,
      #endif
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
   [InlineData(_lineSeparatorNone)]
   [InlineData(_lineSeparatorCrlf)]
   [InlineData(_lineSeparatorLf)]
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnFalse_WhenValueIsTooLong(String lineSeparator)
   {
      // Arrange.
      var value = GetTestValue(lineSeparator: lineSeparator) + "X";

      // Act/assert.
      _sut.Validate(value).Should().BeFalse();
   }

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
   // Test data for edge cases where separator validation cannot detect certain 
   // issues due to length ambiguity. For example, when the first line is 
   // shortened by exactly one character, a CRLF separator's LF character falls 
   // at the position where an LF-only separator would be expected, making the 
   // error undetectable by length validation alone.
   [InlineData("<<<<<<<<<<<<<<", _lineSeparatorCrlf, "<<<<<<<<<<<", _lineSeparatorLf)]       // Length indicates Lf only and Lf falls in correct position
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnTrue_WhenUndetectableInvalidSeparator(
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
      _sut.Validate(value).Should().BeTrue();
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
   [InlineData("D23145890<", "0B112234566<<<<", "7408122", "1204159", "4")]   // Extended document number AB112234566 with single char transcription error (A -> 0) with delta 10
   [InlineData("D23145890<", "AB112283568<<<<", "7408122", "1204159", "2")]   // Extended document number AB112238568 with two char transposition error (38 -> 83) with delta 5
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnTrue_WhenDocumentNumberContainsUndetectableError(
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
   [InlineData("D2314589A7", "<<<<<<<<<<<<<<<", "7408122", "1204159", "6")]   // Document number D231458907 with single char transcription error (0 -> A) with delta 10
   [InlineData("N2314589A7", "<<<<<<<<<<<<<<<", "7408122", "1204159", "6")]   // Document number D231458907 with single char transcription error (D -> N) with delta 10
   [InlineData("L89890C236", "<<<<<<<<<<<<<<<", "7408122", "1204159", "8")]   // Document number L898902C36 with two char transposition error (2C -> C2) with delta 10
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnTrue_WhenExtendedDocumentNumberContainsUndetectableError(
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
   [InlineData("0000000000", "<<<<<<<<<<<<<<<", "8812728", "0000000", "0")]   // Date of birth 8812278 with two char transposition error (27 -> 72) with delta 5
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnTrue_WhenDateOfBirthContainsUndetectableError(
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
   [InlineData("0000000000", "<<<<<<<<<<<<<<<", "0000000", "8812728", "2")]   // Date of expiry 8812278 with two char transposition error (27 -> 72) with delta 5
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnTrue_WhenDateOfExpiryContainsUndetectableError(
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
   [InlineData("2D31458907", "<<<<<<<<<<<<<<<", "7408122", "1204159", "6")]   // Document number D231458907 with two character transposition error (D2 -> 2D)
   [InlineData("D231548907", "<<<<<<<<<<<<<<<", "7408122", "1204159", "6")]   // Document number D231458907 with two digit transposition error (45 -> 54)
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnFalse_WhenDocumentNumberContainsDetectableError(
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
   [InlineData("D23145890<", "AC112234566<<<<", "7408122", "1204159", "4")]   // Extended document number AB112234566 with single character transcription error (B -> C)
   [InlineData("D23145890<", "BA112234566<<<<", "7408122", "1204159", "4")]   // Extended document number AB112234566 with two character transcription error (AB -> BA)
   [InlineData("D23145890<", "AB112243566<<<<", "7408122", "1204159", "4")]   // Extended document number AB112234566 with two character transcription error (34 -> 43)
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnFalse_WhenExtendedDocumentNumberContainsDetectableError(
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
   [InlineData("D231458907", "<<<<<<<<<<<<<<<", "7409122", "1204159", "6")]   // Date of birth 7408122 with single digit transcription error (8 -> 9)
   [InlineData("D231458907", "<<<<<<<<<<<<<<<", "7408212", "1204159", "6")]   // Date of birth 7408122 with two digit transposition error (12 -> 21)
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnFalse_WhenDateOfBirthContainsDetectableError(
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
   [InlineData("D231458907", "<<<<<<<<<<<<<<<", "7409122", "1203159", "6")]   // Date of expiry 1204159 with single digit transcription error (4 -> 3)
   [InlineData("D231458907", "<<<<<<<<<<<<<<<", "7409122", "1201459", "6")]   // Date of expiry 1204159 with two digit transposition error (41 -> 14)
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnFalse_WhenDateOfExpiryContainsDetectableError(
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
   [InlineData("0")]
   [InlineData("1")]
   [InlineData("2")]
   [InlineData("3")]
   [InlineData("4")]
   [InlineData("5")]
   [InlineData("7")]
   [InlineData("8")]
   [InlineData("9")]
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnFalse_WhenFieldCheckDigitsAreValidButCompositeCheckDigitIsNotValid(String compositeCheckDigit)
   {
      // Arrange.
      var value = GetTestValue(compositeCheckDigit: compositeCheckDigit);  // Composite check digit should be 6 based on other field values

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
   [InlineData("!231458907")]             // ! - symbol preceeding digit block
   [InlineData("D?31458907")]             // ? - symbol preceeding uppercase letter block
   [InlineData("D2]1458907")]             // ] - symbol preceeding lowercase letter block
   [InlineData("D23a458907")]             // a - lowercase letter
   [InlineData("D231~58907")]             // ~ - symbol following lowercase letter block
   [InlineData("D2314\u21538907")]        // Unicode fraction 1/3
   [InlineData("D23145\u0BE6907")]        // Unicode Tamil digit 0
   [InlineData("D231458\u00D607")]        // Unicode uppercase O with umlaut
   [InlineData("D2314589\u00F67")]        // Unicode lowercase o with umlaut
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnFalse_WhenDocumentNumberContainsInvalidCharacter(String documentNumber)
   {
      // Arrange.
      var value = GetTestValue(documentNumber: documentNumber);

      // Act/assert.
      _sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   [InlineData("D23145890<", "!<<<<<<<<<<<<<<")]         // ! - symbol preceeding digit block
   [InlineData("D23145890<", "0?<<<<<<<<<<<<<")]         // ? - symbol preceeding uppercase letter block
   [InlineData("D23145890<", "00]<<<<<<<<<<<<")]         // ] - symbol preceeding lowercase letter block
   [InlineData("D23145890<", "000a<<<<<<<<<<<")]         // a - lowercase letter
   [InlineData("D23145890<", "0000~<<<<<<<<<<")]         // ~ - symbol following lowercase letter block
   [InlineData("D23145890<", "00000\u2153<<<<<<<<<")]    // Unicode fraction 1/3    
   [InlineData("D23145890<", "000000\u0BE6<<<<<<<<")]    // Unicode Tamil digit 0    
   [InlineData("D23145890<", "0000000\u00D6<<<<<<<")]    // Unicode uppercase O with umlaut    
   [InlineData("D23145890<", "00000000\u00F6<<<<<<")]    // Unicode lowercase o with umlaut    
   [InlineData("D23145890<", "000000000!<<<<<")]         // ! - symbol preceeding digit block
   [InlineData("D23145890<", "0000000000?<<<<")]         // ? - symbol preceeding uppercase letter block
   [InlineData("D23145890<", "00000000000]<<<")]         // ] - symbol preceeding lowercase letter block
   [InlineData("D23145890<", "000000000000z<<")]         // z - lowercase letter
   [InlineData("D23145890<", "0000000000000~<")]         // ~ - symbol following lowercase letter block
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnFalse_WhenExtendedDocumentNumberContainsInvalidCharacter(
      String documentNumber,
      String optionalData)
   {
      // Arrange.
      var value = GetTestValue(
         documentNumber: documentNumber,
         optionalData: optionalData);

      // Act/assert.
      _sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   [InlineData("!408122")]             // ! - symbol preceeding digit block
   [InlineData("7?08122")]             // ? - symbol preceeding uppercase letter block
   [InlineData("74A8122")]             // A - uppercase letter
   [InlineData("740]122")]             // ] - symbol preceeding lowercase letter block
   [InlineData("7408a22")]             // a - lowercase letter
   [InlineData("74081~2")]             // ~ - symbol following lowercase letter block
   [InlineData("74081\u21532")]        // Unicode fraction 1/3
   [InlineData("74081\u0BE62")]        // Unicode Tamil digit 0
   [InlineData("74081\u00D62")]        // Unicode uppercase O with umlaut
   [InlineData("74081\u00F62")]        // Unicode lowercase o with umlaut
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnFalse_WhenDateOfBirthContainsInvalidCharacter(String dateOfBirth)
   {
      // Arrange.
      var value = GetTestValue(dateOfBirth: dateOfBirth);

      // Act/assert.
      _sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   [InlineData("!204159")]             // ! - symbol preceeding digit block
   [InlineData("1?04159")]             // ? - symbol preceeding uppercase letter block
   [InlineData("12A4159")]             // A - uppercase letter
   [InlineData("120]159")]             // ] - symbol preceeding lowercase letter block
   [InlineData("1204a59")]             // a - lowercase letter
   [InlineData("12041~9")]             // ~ - symbol following lowercase letter block
   [InlineData("12041\u21539")]        // Unicode fraction 1/3
   [InlineData("12041\u0BE69")]        // Unicode Tamil digit 0
   [InlineData("12041\u00D69")]        // Unicode uppercase O with umlaut
   [InlineData("12041\u00F69")]        // Unicode lowercase o with umlaut
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnFalse_WhenDateOfExpiryContainsInvalidCharacter(String dateOfBirth)
   {
      // Arrange.
      var value = GetTestValue(dateOfBirth: dateOfBirth);

      // Act/assert.
      _sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   [InlineData("D23145890!")]             // ! - symbol preceeding digit block
   [InlineData("D23145890?")]             // ? - symbol preceeding uppercase letter block
   [InlineData("D23145890A")]             // A - uppercase letter
   [InlineData("D23145890]")]             // ] - symbol preceeding lowercase letter block
   [InlineData("D23145890a")]             // a - lowercase letter
   [InlineData("D23145890~")]             // ~ - symbol following lowercase letter block
   [InlineData("D23145890\u2153")]        // Unicode fraction 1/3
   [InlineData("D23145890\u0BE6")]        // Unicode Tamil digit 0
   [InlineData("D23145890\u00D6")]        // Unicode uppercase O with umlaut
   [InlineData("D23145890\u00F6")]        // Unicode lowercase o with umlaut
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnFalse_WhenDocumentNumberCheckDigitIsInvalidCharacter(String documentNumber)
   {
      // Arrange.
      var value = GetTestValue(documentNumber: documentNumber);

      // Act/assert.
      _sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   [InlineData("D23145890<", "!<<<<<<<<<<<<<<")]         // ! - symbol preceeding digit block
   [InlineData("D23145890<", "0?<<<<<<<<<<<<<")]         // ? - symbol preceeding uppercase letter block
   [InlineData("D23145890<", "00A<<<<<<<<<<<<")]         // A - uppercase letter
   [InlineData("D23145890<", "000]<<<<<<<<<<<")]         // ] - symbol preceeding lowercase letter block
   [InlineData("D23145890<", "0000a<<<<<<<<<<")]         // a - lowercase letter
   [InlineData("D23145890<", "00000~<<<<<<<<<")]         // ~ - symbol following lowercase letter block
   [InlineData("D23145890<", "000000\u2153<<<<<<<<")]    // Unicode fraction 1/3
   [InlineData("D23145890<", "0000000\u0BE6<<<<<<<")]    // Unicode Tamil digit 0
   [InlineData("D23145890<", "00000000\u00D6<<<<<<")]    // Unicode uppercase O with umlaut
   [InlineData("D23145890<", "000000000\u00F6<<<<<")]    // Unicode lowercase o with umlaut
   [InlineData("D23145890<", "0000000000!<<<<")]         // ! - symbol preceeding digit block
   [InlineData("D23145890<", "00000000000?<<<")]         // ? - symbol preceeding uppercase letter block
   [InlineData("D23145890<", "000000000000A<<")]         // A - uppercase letter
   [InlineData("D23145890<", "0000000000000]<")]         // ] - symbol preceeding lowercase letter block
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnFalse_WhenExtendedDocumentNumberCheckDigitInvalidCharacter(
      String documentNumber,
      String optionalData)
   {
      // Arrange.
      var value = GetTestValue(
         documentNumber: documentNumber,
         optionalData: optionalData);

      // Act/assert.
      _sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   [InlineData("740812!")]             // ! - symbol preceeding digit block
   [InlineData("740812?")]             // ? - symbol preceeding uppercase letter block
   [InlineData("740812A")]             // A - uppercase letter
   [InlineData("740812]")]             // ] - symbol preceeding lowercase letter block
   [InlineData("740812a")]             // a - lowercase letter
   [InlineData("740812~")]             // ~ - symbol following lowercase letter block
   [InlineData("740812\u2153")]        // Unicode fraction 1/3
   [InlineData("740812\u0BE6")]        // Unicode Tamil digit 0
   [InlineData("740812\u00D6")]        // Unicode uppercase O with umlaut
   [InlineData("740812\u00F6")]        // Unicode lowercase o with umlaut
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnFalse_WhenDateOfBirthCheckDigitInvalidCharacter(String dateOfBirth)
   {
      // Arrange.
      var value = GetTestValue(dateOfBirth: dateOfBirth);

      // Act/assert.
      _sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   [InlineData("120415!")]             // ! - symbol preceeding digit block
   [InlineData("120415?")]             // ? - symbol preceeding uppercase letter block
   [InlineData("120415A")]             // A - uppercase letter
   [InlineData("120415]")]             // ] - symbol preceeding lowercase letter block
   [InlineData("120415a")]             // a - lowercase letter
   [InlineData("120415~")]             // ~ - symbol following lowercase letter block
   [InlineData("120415\u2153")]        // Unicode fraction 1/3
   [InlineData("120415\u0BE6")]        // Unicode Tamil digit 0
   [InlineData("120415\u00D6")]        // Unicode uppercase O with umlaut
   [InlineData("120415\u00F6")]        // Unicode lowercase o with umlaut
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnFalse_WhenDateOfExpiryCheckDigitInvalidCharacter(String dateOfBirth)
   {
      // Arrange.
      var value = GetTestValue(dateOfBirth: dateOfBirth);

      // Act/assert.
      _sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   [InlineData("!")]             // ! - symbol preceeding digit block
   [InlineData("?")]             // ? - symbol preceeding uppercase letter block
   [InlineData("A")]             // A - uppercase letter
   [InlineData("]")]             // ] - symbol preceeding lowercase letter block
   [InlineData("a")]             // a - lowercase letter
   [InlineData("~")]             // ~ - symbol following lowercase letter block
   [InlineData("\u2153")]        // Unicode fraction 1/3
   [InlineData("\u0BE6")]        // Unicode Tamil digit 0
   [InlineData("\u00D6")]        // Unicode uppercase O with umlaut
   [InlineData("\u00F6")]        // Unicode lowercase o with umlaut
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
   public void Icao9303SizeTD1Algorithm_Validate_ShouldReturnTrue_ForBenchmarkValues(String value)
      => _sut.Validate(value).Should().BeTrue();

   #endregion
}
