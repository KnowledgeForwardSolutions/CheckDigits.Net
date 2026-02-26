// Ignore Spelling: Icao Mrz

namespace CheckDigits.Net.Tests.Unit.ValueSpecificAlgorithms;

public class Icao9303SizeTD3AlgorithmTests
{
   private readonly Icao9303SizeTD3Algorithm _sut = new();

   // Example MRZ from https://www.icao.int/publications/Documents/9303_p4_cons_en.pdf
   private const String _mrzFirstLine = "P<UTOERIKSSON<<ANNA<MARIA<<<<<<<<<<<<<<<<<<<";
   private const String _mrzSecondLine = "L898902C36UTO7408122F1204159ZE184226B<<<<<10";

   private const String _lineSeparatorNone = "";
   private const String _lineSeparatorCrlf = "\r\n";
   private const String _lineSeparatorLf = "\n";

   private static String GetTestValue(
      String documentCode = "P<",
      String issuingState = "UTO",
      String name = "ERIKSSON<<ANNA<MARIA<<<<<<<<<<<<<<<<<<<",
      String lineSeparator = _lineSeparatorNone,
      String passportNumber = "L898902C36",              // 9 alphanumeric characters + 1 check digit
      String nationality = "UTO",
      String dateOfBirth = "7408122",                    // 6 digit characters + 1 check digit
      String sex = "F",
      String dateOfExpiry = "1204159",                   // 6 digit characters + 1 check digit
      String personalNumber = "ZE184226B<<<<<1",         // 14 alphanumeric characters + 1 check digit
      String compositeCheckDigit = "0")
   => $"{documentCode}{issuingState}{name}" +
      lineSeparator +
      $"{passportNumber}{nationality}{dateOfBirth}{sex}{dateOfExpiry}{personalNumber}{compositeCheckDigit}";

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
   [InlineData("ERIKSSON<<ANNA<MARIA<<<<<<<<<<<<<<<<<<", _lineSeparatorNone)]      // Name -1 char and default line separator = total length 87
   [InlineData("ERIKSSON<<ANNA<MARIA<<<<<<<<<<<<<<<<<<<<", _lineSeparatorCrlf)]    // Name + 1 char and CRLF separator = total length 91
   public void Icao9303SizeTD3Algorithm_Validate_ShouldReturnFalse_WhenValueIsInvalidLength(
      String name,
      String lineSeparator)
   {
      // Arrange.
      var value = GetTestValue(name: name, lineSeparator: lineSeparator);

      // Act/assert.
      _sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   // Test data for edge cases where separator validation cannot detect certain 
   // issues due to length ambiguity. For example, when the first line is 
   // shortened by exactly one character, a CRLF separator's LF character falls 
   // at the position where an LF-only separator would be expected, making the 
   // error undetectable by length validation alone.
   [InlineData("ERIKSSON<<ANNA<MARIA<<<<<<<<<<<<<<<<<<", _lineSeparatorCrlf)]         // Name length -1 so total length indicates Lf only and Lf falls in correct position
   [InlineData("ERIKSSON<<ANNA<MARIA<<<<<<<<<<<<<<<<<<", _lineSeparatorLf)]           // Name length -1 so total length indicates no separator so separator chars not checked
   public void Icao9303SizeTD3Algorithm_Validate_ShouldReturnTrue_WhenUndetectableInvalidSeparator(
      String name,
      String lineSeparator)
   {
      // Arrange.
      var value = GetTestValue(name: name, lineSeparator: lineSeparator);

      // Act/assert.
      _sut.Validate(value).Should().BeTrue();
   }

   [Theory]
   [InlineData(_lineSeparatorNone)]
   [InlineData(_lineSeparatorCrlf)]
   [InlineData(_lineSeparatorLf)]
   public void Icao9303SizeTD3Algorithm_Validate_ShouldReturnTrue_WhenLineSeparatorIsValid(String lineSeparator)
   {
      // Arrange.
      var value = GetTestValue(lineSeparator: lineSeparator);

      // Act/assert.
      _sut.Validate(value).Should().BeTrue();
   }

   [Theory]
   [InlineData("X\n")]     // 'X' instead of \r
   [InlineData(" \n")]     // Space instead of \r
   [InlineData("\r ")]     // Space instead of \n
   [InlineData("\n\r")]    // \n\r instead of \r\n
   public void Icao9303SizeTD3Algorithm_Validate_ShouldReturnFalse_WhenLineSeparatorIsInvalid(String lineSeparator)
   {
      // Arrange.
      var value = GetTestValue(lineSeparator: lineSeparator);

      // Act/assert.
      _sut.Validate(value).Should().BeFalse();
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
   public void Icao9303SizeTD3Algorithm_Validate_ShouldCorrectlyMapCharactersToIntegerEquivalents(
      String passportNumber,
      String compositeCheckDigit)
   {
      // Arrange.
      var value = GetTestValue(
         lineSeparator: _lineSeparatorNone,
         passportNumber: passportNumber,
         dateOfBirth: "0000000",
         dateOfExpiry: "0000000",
         personalNumber: "<<<<<<<<<<<<<<<",
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
   public void Icao9303SizeTD3Algorithm_Validate_ShouldReturnFalse_WhenLowerCaseAlphabeticCharacterEncountered(
      String passportNumber,
      String compositeCheckDigit)
   {
      // Arrange.
      var value = GetTestValue(
         lineSeparator: _lineSeparatorCrlf,
         passportNumber: passportNumber,
         dateOfBirth: "0000000",
         dateOfExpiry: "0000000",
         personalNumber: "<<<<<<<<<<<<<<<",
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
   public void Icao9303SizeTD3Algorithm_Validate_ShouldReturnFalse_NumericFieldContainsAlphabeticCharacter(
      String dateOfBirth,
      String dateOfExpiry,
      String compositeCheckDigit)
   {
      // Arrange.
      var value = GetTestValue(
         passportNumber: "0000000000",
         dateOfBirth: dateOfBirth,
         dateOfExpiry: dateOfExpiry,
         personalNumber: "<<<<<<<<<<<<<<<",
         compositeCheckDigit: compositeCheckDigit);

      // Act/assert.
      _sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   // Passport number field
   [InlineData("1000000007", "0000000", "0000000", "<<<<<<<<<<<<<<<", "6")]
   [InlineData("0100000003", "0000000", "0000000", "<<<<<<<<<<<<<<<", "4")]
   [InlineData("0010000001", "0000000", "0000000", "<<<<<<<<<<<<<<<", "8")]
   [InlineData("0001000007", "0000000", "0000000", "<<<<<<<<<<<<<<<", "6")]
   [InlineData("0000100003", "0000000", "0000000", "<<<<<<<<<<<<<<<", "4")]
   [InlineData("0000010001", "0000000", "0000000", "<<<<<<<<<<<<<<<", "8")]
   [InlineData("0000001007", "0000000", "0000000", "<<<<<<<<<<<<<<<", "6")]
   [InlineData("0000000103", "0000000", "0000000", "<<<<<<<<<<<<<<<", "4")]
   [InlineData("0000000011", "0000000", "0000000", "<<<<<<<<<<<<<<<", "8")]
   // Date of birth field
   [InlineData("0000000000", "1000007", "0000000", "<<<<<<<<<<<<<<<", "4")]
   [InlineData("0000000000", "0100003", "0000000", "<<<<<<<<<<<<<<<", "0")]
   [InlineData("0000000000", "0010001", "0000000", "<<<<<<<<<<<<<<<", "0")]
   [InlineData("0000000000", "0001007", "0000000", "<<<<<<<<<<<<<<<", "4")]
   [InlineData("0000000000", "0000103", "0000000", "<<<<<<<<<<<<<<<", "0")]
   [InlineData("0000000000", "0000011", "0000000", "<<<<<<<<<<<<<<<", "0")]
   // Date of expiry field
   [InlineData("0000000000", "0000000", "1000007", "<<<<<<<<<<<<<<<", "8")]
   [InlineData("0000000000", "0000000", "0100003", "<<<<<<<<<<<<<<<", "0")]
   [InlineData("0000000000", "0000000", "0010001", "<<<<<<<<<<<<<<<", "4")]
   [InlineData("0000000000", "0000000", "0001007", "<<<<<<<<<<<<<<<", "8")]
   [InlineData("0000000000", "0000000", "0000103", "<<<<<<<<<<<<<<<", "0")]
   [InlineData("0000000000", "0000000", "0000011", "<<<<<<<<<<<<<<<", "4")]
   // Optional personal number field
   [InlineData("0000000000", "0000000", "0000000", "100000000000007", "4")]
   [InlineData("0000000000", "0000000", "0000000", "010000000000003", "6")]
   [InlineData("0000000000", "0000000", "0000000", "001000000000001", "2")]
   [InlineData("0000000000", "0000000", "0000000", "000100000000007", "4")]
   [InlineData("0000000000", "0000000", "0000000", "000010000000003", "6")]
   [InlineData("0000000000", "0000000", "0000000", "000001000000001", "2")]
   [InlineData("0000000000", "0000000", "0000000", "000000100000007", "4")]
   [InlineData("0000000000", "0000000", "0000000", "000000010000003", "6")]
   [InlineData("0000000000", "0000000", "0000000", "000000001000001", "2")]
   [InlineData("0000000000", "0000000", "0000000", "000000000100007", "4")]
   [InlineData("0000000000", "0000000", "0000000", "000000000010003", "6")]
   [InlineData("0000000000", "0000000", "0000000", "000000000001001", "2")]
   [InlineData("0000000000", "0000000", "0000000", "000000000000107", "4")]
   [InlineData("0000000000", "0000000", "0000000", "000000000000013", "6")]
   public void Icao9303SizeTD3Algorithm_Validate_ShouldCorrectlyWeightByCharacterPosition(
      String passportNumber,
      String dateOfBirth,
      String dateOfExpiry,
      String personalNumber,
      String compositeCheckDigit)
   {
      // Arrange.
      var value = GetTestValue(
         lineSeparator: _lineSeparatorLf,
         passportNumber: passportNumber,
         dateOfBirth: dateOfBirth,
         dateOfExpiry: dateOfExpiry,
         personalNumber: personalNumber,
         compositeCheckDigit: compositeCheckDigit);

      // Act/assert.
      _sut.Validate(value).Should().BeTrue();
   }

   [Theory]
   [InlineData("L898902C36", "7408122", "1204159", "ZE184226B<<<<<1", "0")]
   [InlineData("1000000007", "0000000", "0000000", "<<<<<<<<<<<<<<<", "6")]
   [InlineData("0000000000", "1000007", "0000000", "<<<<<<<<<<<<<<<", "4")]
   [InlineData("0000000000", "0000000", "1000007", "<<<<<<<<<<<<<<<", "8")]
   [InlineData("0000000000", "0000000", "0000000", "100000000000007", "4")]
   public void Icao9303SizeTD3Algorithm_Validate_ShouldReturnTrue_WhenValueContainsValidCheckDigits(
      String passportNumber,
      String dateOfBirth,
      String dateOfExpiry,
      String personalNumber,
      String compositeCheckDigit)
   {
      // Arrange.
      var value = GetTestValue(
         passportNumber: passportNumber,
         dateOfBirth: dateOfBirth,
         dateOfExpiry: dateOfExpiry,
         personalNumber: personalNumber,
         compositeCheckDigit: compositeCheckDigit);

      // Act/assert.
      _sut.Validate(value).Should().BeTrue();
   }

   [Theory]
   [InlineData("L8989A2C36", "7408122", "1204159", "ZE184226B<<<<<1", "0")]    // Passport number L898902C36 with single char transcription error (0 -> A) with delta 10
   [InlineData("L89890C236", "7408122", "1204159", "ZE184226B<<<<<1", "0")]    // Passport number L898902C36 with two char transposition error (2C -> C2) with delta 10
   [InlineData("0000000000", "8812728", "0000000", "<<<<<<<<<<<<<<<", "0")]    // Date of birth 8812278 with two char transposition error (27 -> 72) with delta 5
   [InlineData("0000000000", "0000000", "8812728", "<<<<<<<<<<<<<<<", "2")]    // Date of expiry 8812278 with two char transposition error (27 -> 72) with delta 5
   [InlineData("0000000000", "0000000", "0000000", "123456789A12345", "0")]    // Personal number 123456789012345 with single char transcription error (0 -> A) with delta 10
   public void Icao9303SizeTD3Algorithm_Validate_ShouldReturnTrue_WhenValueContainsUndetectableError(
      String passportNumber,
      String dateOfBirth,
      String dateOfExpiry,
      String personalNumber,
      String compositeCheckDigit)
   {
      // Arrange.
      var value = GetTestValue(
         passportNumber: passportNumber,
         dateOfBirth: dateOfBirth,
         dateOfExpiry: dateOfExpiry,
         personalNumber: personalNumber,
         compositeCheckDigit: compositeCheckDigit);

      // Act/assert.
      _sut.Validate(value).Should().BeTrue();
   }

   [Theory]
   [InlineData("L898902D36", "7408122", "1204159", "ZE184226B<<<<<1", "3")]    // Passport number L898902C36 with single char transcription error (C -> D)
   [InlineData("L898902C36", "7438122", "1204159", "ZE184226B<<<<<1", "1")]    // Date of birth 7408122 with single digit transcription error (0 -> 3)
   [InlineData("L898902C36", "7408122", "1201459", "ZE184226B<<<<<1", "9")]    // Date of expiry 1204159 with two digit transposition error (41 -> 14)
   [InlineData("L898902C36", "7408122", "1204159", "ZE184226B<<<<<1", "7")]    // Personal number ZE184226B<<<<<1 with two char transposition error (ZE -> EZ)
   public void Icao9303SizeTD3Algorithm_Validate_ShouldReturnFalse_WhenValueContainsDetectableError(
      String passportNumber,
      String dateOfBirth,
      String dateOfExpiry,
      String personalNumber,
      String compositeCheckDigit)
   {
      // Arrange.
      var value = GetTestValue(
         passportNumber: passportNumber,
         dateOfBirth: dateOfBirth,
         dateOfExpiry: dateOfExpiry,
         personalNumber: personalNumber,
         compositeCheckDigit: compositeCheckDigit);

      // Act/assert.
      _sut.Validate(value).Should().BeFalse();
   }

   [Fact]
   public void Icao9303SizeTD3Algorithm_Validate_ShouldReturnFalse_WhenFieldCheckDigitsAreValidButCompositeCheckDigitIsNotValid()
   {
      // Arrange.
      var value = GetTestValue(compositeCheckDigit: "7");   // Composite check digit should be '0' for the default test value

      // Act/assert.
      _sut.Validate(value).Should().BeFalse();
   }

   [Fact]
   public void Icao9303SizeTD3Algorithm_Validate_ShouldReturnTrue_WhenFieldsAreAllZeros()
   {
      // Arrange.
      var value = GetTestValue(
         passportNumber: "0000000000",
         dateOfBirth: "0000000",
         dateOfExpiry: "0000000",
         personalNumber: "<<<<<<<<<<<<<<<",
         compositeCheckDigit: "0");

      // Act/assert.
      _sut.Validate(value).Should().BeTrue();
   }

   [Fact]
   public void Icao9303SizeTD3Algorithm_Validate_ShouldReturnTrue_WhenFieldsAreAllFillerCharacters()
   {
      // Arrange.
      var value = GetTestValue(
         passportNumber: "<<<<<<<<<0",
         dateOfBirth: "<<<<<<0",
         dateOfExpiry: "<<<<<<0",
         personalNumber: "<<<<<<<<<<<<<<<",
         compositeCheckDigit: "0");

      // Act/assert.
      _sut.Validate(value).Should().BeTrue();
   }

   [Theory]
   [InlineData("<<<<<<<<<<<<<<<")]
   [InlineData("<<<<<<<<<<<<<<0")]
   public void Icao9303SizeTD3Algorithm_Validate_ShouldReturnTrue_WhenEmptyPersonalNumberUsesZeroOrFillerCharacterForCheckDigit(
      String personalNumber)
   {
      // Arrange.
      var value = GetTestValue(
         passportNumber: "0000000000",
         dateOfBirth: "0000000",
         dateOfExpiry: "0000000",
         personalNumber: personalNumber,
         compositeCheckDigit: "0");

      // Act/assert.
      _sut.Validate(value).Should().BeTrue();
   }

   [Fact]
   public void Icao9303SizeTD3Algorithm_Validate_ShouldReturnFalse_WhenAllZerosPersonalNumberUsesFillerCharacterForCheckDigit()
   {
      // Arrange.
      var value = GetTestValue(
         passportNumber: "0000000000",
         dateOfBirth: "0000000",
         dateOfExpiry: "0000000",
         personalNumber: "00000000000000<",
         compositeCheckDigit: "0");

      // Act/assert.
      _sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   [InlineData("`898902C36", "7408122", "1204159", "ZE184226B<<<<<1", "0")]    // Passport number L898902C36 with L replaced with character 20 positions later in ASCII table
   [InlineData("l898902C36", "7408122", "1204159", "ZE184226B<<<<<1", "0")]    // Passport number L898902C36 with L replaced with lowercase l
   [InlineData("L898902C36", "74A8122", "1204159", "ZE184226B<<<<<1", "0")]    // Date of birth 7408122 with 0 replaced with character A
   [InlineData("L898902C36", "74a8122", "1204159", "ZE184226B<<<<<1", "0")]    // Date of birth 7408122 with 0 replaced with character a
   [InlineData("L898902C36", "74&8122", "1204159", "ZE184226B<<<<<1", "0")]    // Date of birth 7408122 with 0 replaced with character 10 positions before in ASCII table
   [InlineData("L898902C36", "7408122", "12A4159", "ZE184226B<<<<<1", "0")]    // Date of expiry 1204159 with 0 replaced with character A
   [InlineData("L898902C36", "7408122", "12a4159", "ZE184226B<<<<<1", "0")]    // Date of expiry 1204159 with 0 replaced with character a
   [InlineData("L898902C36", "7408122", "12:4159", "ZE184226B<<<<<1", "0")]    // Date of expiry 1204159 with 0 replaced with character 10 positions later in ASCII table
   [InlineData("L898902C36", "7408122", "1204159", "ZE18>226B<<<<<1", "0")]    // Personal number ZE184226B<<<<<1 with 4 replaced with character 10 positions later in ASCII table
   [InlineData("L898902C36", "7408122", "1204159", "ZE184226b<<<<<1", "0")]    // Personal number ZE184226B<<<<<1 with B replaced with lowercase b
   public void Icao9303SizeTD3Algorithm_Validate_ShouldReturnFalse_WhenFieldContainsInvalidCharacter(
      String passportNumber,
      String dateOfBirth,
      String dateOfExpiry,
      String personalNumber,
      String compositeCheckDigit)
   {
      // Arrange.
      var value = GetTestValue(
         passportNumber: passportNumber,
         dateOfBirth: dateOfBirth,
         dateOfExpiry: dateOfExpiry,
         personalNumber: personalNumber,
         compositeCheckDigit: compositeCheckDigit);

      // Act/assert.
      _sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   [InlineData("L898902C3A", "7408122", "1204159", "ZE184226B<<<<<1", "0")]    // Document number check digit is invalid character
   [InlineData("L898902C3&", "7408122", "1204159", "ZE184226B<<<<<1", "0")]    // Document number check digit is invalid character
   [InlineData("L898902C3<", "7408122", "1204159", "ZE184226B<<<<<1", "0")]    // Document number check digit is invalid character
   [InlineData("L898902C3[", "7408122", "1204159", "ZE184226B<<<<<1", "0")]    // Document number check digit is invalid character
   [InlineData("L898902C36", "740812A", "1204159", "ZE184226B<<<<<1", "0")]    // Date of birth check digit is invalid character
   [InlineData("L898902C36", "740812&", "1204159", "ZE184226B<<<<<1", "0")]    // Date of birth check digit is invalid character
   [InlineData("L898902C36", "740812<", "1204159", "ZE184226B<<<<<1", "0")]    // Date of birth check digit is invalid character
   [InlineData("L898902C36", "740812[", "1204159", "ZE184226B<<<<<1", "0")]    // Date of birth check digit is invalid character
   [InlineData("L898902C36", "7408122", "120415A", "ZE184226B<<<<<1", "0")]    // Date of expiry check digit is invalid character
   [InlineData("L898902C36", "7408122", "120415&", "ZE184226B<<<<<1", "0")]    // Date of expiry check digit is invalid character
   [InlineData("L898902C36", "7408122", "120415<", "ZE184226B<<<<<1", "0")]    // Date of expiry check digit is invalid character
   [InlineData("L898902C36", "7408122", "120415[", "ZE184226B<<<<<1", "0")]    // Date of expiry check digit is invalid character
   public void Icao9303SizeTD3Algorithm_Validate_ShouldReturnFalse_WhenRequiredFieldCheckDigitContainsNonDigitCharacter(
      String passportNumber,
      String dateOfBirth,
      String dateOfExpiry,
      String personalNumber,
      String compositeCheckDigit)
   {
      // Arrange.
      var value = GetTestValue(
         passportNumber: passportNumber,
         dateOfBirth: dateOfBirth,
         dateOfExpiry: dateOfExpiry,
         personalNumber: personalNumber,
         compositeCheckDigit: compositeCheckDigit);

      // Act/assert.
      _sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   [InlineData("L898902C36", "7408122", "1204159", "ZE184226B<<<<<A", "0")]    // Personal number check digit is invalid character
   [InlineData("L898902C36", "7408122", "1204159", "ZE184226B<<<<<:", "0")]    // Personal number check digit is invalid character
   public void Icao9303SizeTD3Algorithm_Validate_ShouldReturnFalse_WhenOptionalFieldCheckDigitContainsInvalidCharacter(
      String passportNumber,
      String dateOfBirth,
      String dateOfExpiry,
      String personalNumber,
      String compositeCheckDigit)
   {
      // Arrange.
      var value = GetTestValue(
         passportNumber: passportNumber,
         dateOfBirth: dateOfBirth,
         dateOfExpiry: dateOfExpiry,
         personalNumber: personalNumber,
         compositeCheckDigit: compositeCheckDigit);

      // Act/assert.
      _sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   [InlineData("A")]
   [InlineData("a")]
   [InlineData("&")]
   [InlineData(":")]
   public void Icao9303SizeTD3Algorithm_Validate_ShouldReturnFalse_WhenCompositeCheckDigitContainsNonDigitCharacter(String compositeCheckDigit)
   {
      // Arrange.
      var value = GetTestValue(compositeCheckDigit: compositeCheckDigit);

      // Act/assert.
      _sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   [InlineData("P<UTOERIKSSON<<ANNA<MARIA<<<<<<<<<<<<<<<<<<<L898902C36UTO7408122F1204159ZE184226B<<<<<10")]
   [InlineData("P<UTOQWERTY<<ASDF<<<<<<<<<<<<<<<<<<<<<<<<<<<\r\nQ123987655UTO3311226F2010201<<<<<<<<<<<<<<06")]
   [InlineData("P<UTOSKYWALKER<<LUKE<<<<<<<<<<<<<<<<<<<<<<<<\nSTARWARS45UTO7705256M2405252HAN<SHOT<FIRST78")]
   public void Icao9303SizeTD3Algorithm_Validate_ShouldReturnTrue_ForBenchmarkValues(String value)
      => _sut.Validate(value).Should().BeTrue();

   #endregion
}
