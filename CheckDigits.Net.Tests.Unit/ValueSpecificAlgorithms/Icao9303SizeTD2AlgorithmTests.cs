// Ignore Spelling: Icao Mrz

namespace CheckDigits.Net.Tests.Unit.ValueSpecificAlgorithms;

public class Icao9303SizeTD2AlgorithmTests
{
   private readonly Icao9303SizeTD2Algorithm _sut = new();

   // Example MRZ from https://www.icao.int/publications/Documents/9303_p6_cons_en.pdf
   private const String _mrzFirstLine = "I<UTOERIKSSON<<ANNA<MARIA<<<<<<<<<<<";
   private const String _mrzSecondLine = "D231458907UTO7408122F1204159<<<<<<<6";

   private const String _lineSeparatorNone = "";
   private const String _lineSeparatorCrlf = "\r\n";
   private const String _lineSeparatorLf = "\n";

   private static String GetTestValue(
      String documentCode = "I<",
      String issuingState = "UTO",
      String name = "ERIKSSON<<ANNA<MARIA<<<<<<<<<<<",
      String lineSeparator = _lineSeparatorNone,
      String documentNumber = "D231458907",              // 9 alphanumeric characters + 1 check digit
      String nationality = "UTO",
      String dateOfBirth = "7408122",                    // 6 digit characters + 1 check digit
      String sex = "F",
      String dateOfExpiry = "1204159",                   // 6 digit characters + 1 check digit
      String optionalData = "<<<<<<<",                   // possible extended document number
      String compositeCheckDigit = "6")
      => $"{documentCode}{issuingState}{name}" + 
         lineSeparator +
         $"{documentNumber}{nationality}{dateOfBirth}{sex}{dateOfExpiry}{optionalData}{compositeCheckDigit}";

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

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Icao9303SizeTD2Algorithm_Validate_ShouldReturnFalse_WhenInputIsNull()
      => _sut.Validate(null!).Should().BeFalse();

   [Fact]
   public void Icao9303SizeTD2Algorithm_Validate_ShouldReturnFalse_WhenInputIsEmpty()
      => _sut.Validate(String.Empty).Should().BeFalse();

   [Theory]
   [InlineData("ERIKSSON<<ANNA<MARIA<<<<<<<<<<", _lineSeparatorNone)]      // Name -1 char and default line separator = total length 71
   [InlineData("ERIKSSON<<ANNA<MARIA<<<<<<<<<<<<", _lineSeparatorCrlf)]    // Name + 1 char and CRLF separator = total length 75
   public void Icao9303SizeTD2Algorithm_Validate_ShouldReturnFalse_WhenValueIsInvalidLength(
      String name,
      String lineSeparator)
   {
      // Arrange.
      var value = GetTestValue(name: name, lineSeparator: lineSeparator);

      // Act/assert.
      _sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   [InlineData(_lineSeparatorNone)]
   [InlineData(_lineSeparatorCrlf)]
   [InlineData(_lineSeparatorLf)]
   public void Icao9303SizeTD2Algorithm_Validate_ShouldReturnTrue_WhenLineSeparatorIsValid(String lineSeparator)
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
   public void Icao9303SizeTD2Algorithm_Validate_ShouldReturnFalse_WhenLineSeparatorIsValid(String lineSeparator)
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
   public void Icao9303SizeTD2Algorithm_Validate_ShouldCorrectlyMapCharactersToIntegerEquivalents(
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
   // Third posiition has weight = 1, so easier to calculate field check digit and composite check digit
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
   public void Icao9303SizeTD2Algorithm_Validate_ShouldReturnFalse_WhenLowerCaseAlphabeticCharacterEncountered(
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
   [InlineData("1000000007", "0000000", "0000000", "<<<<<<<", "6")]
   [InlineData("0100000003", "0000000", "0000000", "<<<<<<<", "4")]
   [InlineData("0010000001", "0000000", "0000000", "<<<<<<<", "8")]
   [InlineData("0001000007", "0000000", "0000000", "<<<<<<<", "6")]
   [InlineData("0000100003", "0000000", "0000000", "<<<<<<<", "4")]
   [InlineData("0000010001", "0000000", "0000000", "<<<<<<<", "8")]
   [InlineData("0000001007", "0000000", "0000000", "<<<<<<<", "6")]
   [InlineData("0000000103", "0000000", "0000000", "<<<<<<<", "4")]
   [InlineData("0000000011", "0000000", "0000000", "<<<<<<<", "8")]
   // Extended document number
   [InlineData("000000000<", "0000000", "0000000", "100007<", "4")]
   [InlineData("000000000<", "0000000", "0000000", "010003<", "6")]
   [InlineData("000000000<", "0000000", "0000000", "001001<", "2")]
   [InlineData("000000000<", "0000000", "0000000", "000107<", "4")]
   [InlineData("000000000<", "0000000", "0000000", "000013<", "6")]
   // Date of birth field
   [InlineData("0000000000", "1000007", "0000000", "<<<<<<<", "4")]
   [InlineData("0000000000", "0100003", "0000000", "<<<<<<<", "0")]
   [InlineData("0000000000", "0010001", "0000000", "<<<<<<<", "0")]
   [InlineData("0000000000", "0001007", "0000000", "<<<<<<<", "4")]
   [InlineData("0000000000", "0000103", "0000000", "<<<<<<<", "0")]
   [InlineData("0000000000", "0000011", "0000000", "<<<<<<<", "0")]
   // Date of expiry field
   [InlineData("0000000000", "0000000", "1000007", "<<<<<<<", "8")]
   [InlineData("0000000000", "0000000", "0100003", "<<<<<<<", "0")]
   [InlineData("0000000000", "0000000", "0010001", "<<<<<<<", "4")]
   [InlineData("0000000000", "0000000", "0001007", "<<<<<<<", "8")]
   [InlineData("0000000000", "0000000", "0000103", "<<<<<<<", "0")]
   [InlineData("0000000000", "0000000", "0000011", "<<<<<<<", "4")]
   public void Icao9303SizeTD2Algorithm_Validate_ShouldCorrectlyWeightByCharacterPosition(
      String documentNumber,
      String dateOfBirth,
      String dateOfExpiry,
      String optionalData,
      String compositeCheckDigit)
   {
      // Arrange.
      var value = GetTestValue(
         lineSeparator: _lineSeparatorNone,
         documentNumber: documentNumber,
         dateOfBirth: dateOfBirth,
         dateOfExpiry: dateOfExpiry,
         optionalData: optionalData,
         compositeCheckDigit: compositeCheckDigit);

      // Act/assert.
      _sut.Validate(value).Should().BeTrue();
   }

   [Theory]
   [InlineData("D231458907", "7408122", "1204159", "<<<<<<<", "6")]
   [InlineData("D23145890<", "7408122", "1204159", "AB1124<", "4")]     // Extended document number
   public void Icao9303SizeTD2Algorithm_Validate_ShouldReturnTrue_WhenValueContainsValidCheckDigits(
      String documentNumber,
      String dateOfBirth,
      String dateOfExpiry,
      String optionalData,
      String compositeCheckDigit)
   {
      // Arrange.
      var value = GetTestValue(
         lineSeparator: _lineSeparatorCrlf,
         documentNumber: documentNumber,
         dateOfBirth: dateOfBirth,
         dateOfExpiry: dateOfExpiry,
         optionalData: optionalData,
         compositeCheckDigit: compositeCheckDigit);

      // Act/assert.
      _sut.Validate(value).Should().BeTrue();
   }

   [Theory]
   [InlineData("D2314589A7", "7408122", "1204159", "<<<<<<<", "6")]   // Document number D231458907 with single char transcription error (0 -> A) with delta 10   
   [InlineData("N231458907", "7408122", "1204159", "<<<<<<<", "6")]   // Document number D231458907 with single char transcription error (D -> N) with delta 10
   [InlineData("L89890C236", "7408122", "1204159", "<<<<<<<", "8")]   // Document number L898902C36 with two char transposition error (2C -> C2) with delta 10
   [InlineData("0000000000", "8812728", "0000000", "<<<<<<<", "0")]   // Date of birth 8812278 with two char transposition error (27 -> 72) with delta 5
   [InlineData("0000000000", "0000000", "8812728", "<<<<<<<", "2")]   // Date of expiry 8812278 with two char transposition error (27 -> 72) with delta 5
   [InlineData("D23145890<", "7408122", "1204159", "0B1124<", "4")]   // Extended document number AB1124 with single char transcription error (A -> 0) with delta 10
   [InlineData("D23145890<", "7408122", "1204159", "AB8325<", "6")]   // Extended document number AB3824 with two char transposition error (38 -> 83) with delta 5
   public void Icao9303SizeTD2Algorithm_Validate_ShouldReturnTrue_WhenValueContainsUndetectableError(
      String documentNumber,
      String dateOfBirth,
      String dateOfExpiry,
      String optionalData,
      String compositeCheckDigit)
   {
      // Arrange.
      var value = GetTestValue(
         lineSeparator: _lineSeparatorLf,
         documentNumber: documentNumber,
         dateOfBirth: dateOfBirth,
         dateOfExpiry: dateOfExpiry,
         optionalData: optionalData,
         compositeCheckDigit: compositeCheckDigit);

      // Act/assert.
      _sut.Validate(value).Should().BeTrue();
   }

   [Theory]
   [InlineData("E231458907", "7408122", "1204159", "<<<<<<<", "6")]   // Document number D231458907 with single character transcription error (D -> E)
   [InlineData("D241458907", "7408122", "1204159", "<<<<<<<", "6")]   // Document number D231458907 with single digit transcription error (3 -> 4)
   [InlineData("D231458907", "7409122", "1204159", "<<<<<<<", "6")]   // Date of birth 7408122 with single digit transcription error (8 -> 9)
   [InlineData("D231458907", "7408122", "1203159", "<<<<<<<", "6")]   // Date of expiry 1204159 with single digit transcription error (4 -> 3)
   [InlineData("2D31458907", "7408122", "1204159", "<<<<<<<", "6")]   // Document number D231458907 with two character transposition error (D2 -> 2D)
   [InlineData("D231548907", "7408122", "1204159", "<<<<<<<", "6")]   // Document number D231458907 with two digit transposition error (45 -> 54)
   [InlineData("D231458907", "7408212", "1204159", "<<<<<<<", "6")]   // Date of birth 7408122 with two digit transposition error (12 -> 21)
   [InlineData("D231458907", "7409122", "1201459", "<<<<<<<", "6")]   // Date of expiry 1204159 with two digit transposition error (41 -> 14)
   [InlineData("D23145890<", "7408122", "1204159", "AC1124<", "4")]   // Extended document number AB1124 with single character transcription error (B -> C)
   public void Icao9303SizeTD2Algorithm_Validate_ShouldReturnFalse_WhenValueContainsDetectableError(
      String documentNumber,
      String dateOfBirth,
      String dateOfExpiry,
      String optionalData,
      String compositeCheckDigit)
   {
      // Arrange.
      var value = GetTestValue(
         documentNumber: documentNumber,
         dateOfBirth: dateOfBirth,
         dateOfExpiry: dateOfExpiry,
         optionalData: optionalData,
         compositeCheckDigit: compositeCheckDigit);

      // Act/assert.
      _sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   [InlineData("0000000000", "0000000", "0000000", "<<<<<<<", "0")]
   [InlineData("0000000000", "0000000", "0000000", "000000<", "0")]        // Extended document number requires at least one trailing filler character
   public void Icao9303SizeTD2Algorithm_Validate_ShouldReturnTrue_WhenFieldsAreAllZeros(
      String documentNumber,
      String dateOfBirth,
      String dateOfExpiry,
      String optionalData,
      String compositeCheckDigit)
   {
      // Arrange.
      var value = GetTestValue(
         documentNumber: documentNumber,
         dateOfBirth: dateOfBirth,
         dateOfExpiry: dateOfExpiry,
         optionalData: optionalData,
         compositeCheckDigit: compositeCheckDigit);

      // Act/assert.
      _sut.Validate(value).Should().BeTrue();
   }

   [Theory]
   [InlineData("<<<<<<<<<0", "<<<<<<0", "<<<<<<0", "<<<<<<<", "0")]
   [InlineData("<<<<<<<<<<", "<<<<<<0", "<<<<<<0", "0<<<<<<", "0")]        // If using extended document number then must have at least one digit (the check digit) before first filler char 
   public void Icao9303SizeTD2Algorithm_Validate_ShouldReturnTrue_WhenFieldsAreAllFillerCharacters(
      String documentNumber,
      String dateOfBirth,
      String dateOfExpiry,
      String optionalData,
      String compositeCheckDigit)
   {
      // Arrange.
      var value = GetTestValue(
         documentNumber: documentNumber,
         dateOfBirth: dateOfBirth,
         dateOfExpiry: dateOfExpiry,
         optionalData: optionalData,
         compositeCheckDigit: compositeCheckDigit);

      // Act/assert.
      _sut.Validate(value).Should().BeTrue();
   }

   [Theory]
   [InlineData("b231458907", "7408122", "1204159", "<<<<<<<", "6")]   // D231458907 with D replaced with character 30 positions later in ASCII table
   [InlineData("D2314589&7", "7408122", "1204159", "<<<<<<<", "6")]   // D231458907 with 0 replaced with character 10 positions before in ASCII table
   [InlineData("D2314589:7", "7408122", "1204159", "<<<<<<<", "6")]   // D231458907 with 0 replaced with character 10 positions later in ASCII table
   [InlineData("D231458907", "7>08122", "1204159", "<<<<<<<", "6")]   // 7408122 with 4 replaced with character 10 positions later in ASCII table
   [InlineData("D23145890<", "7408122", "1204159", "A&1124<", "4")]   // Extended document number AB112234 with B replaced by invalid character
   [InlineData("D23145890<", "7408122", "1204159", "A:1124<", "4")]   // Extended document number AB112234 with B replaced by invalid character
   [InlineData("D23145890<", "7408122", "1204159", "A[1124<", "4")]   // Extended document number AB112234 with B replaced by invalid character
   public void Icao9303SizeTD2Algorithm_Validate_ShouldReturnFalse_WhenFieldContainsInvalidCharacter(
      String documentNumber,
      String dateOfBirth,
      String dateOfExpiry,
      String optionalData,
      String compositeCheckDigit)
   {
      // Arrange.
      var value = GetTestValue(
         documentNumber: documentNumber,
         dateOfBirth: dateOfBirth,
         dateOfExpiry: dateOfExpiry,
         optionalData: optionalData,
         compositeCheckDigit: compositeCheckDigit);

      // Act/assert.
      _sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   [InlineData("D23145890A", "7408122", "1204159", "<<<<<<<", "6")]   // Document number check digit is invalid character
   [InlineData("D23145890&", "7408122", "1204159", "<<<<<<<", "6")]   // Document number check digit is invalid character
   [InlineData("D23145890:", "7408122", "1204159", "<<<<<<<", "6")]   // Document number check digit is invalid character
   [InlineData("D231458907", "740812A", "1204159", "<<<<<<<", "6")]   // Date of birth check digit is invalid character
   [InlineData("D231458907", "740812&", "1204159", "<<<<<<<", "6")]   // Date of birth check digit is invalid character
   [InlineData("D231458907", "740812<", "1204159", "<<<<<<<", "6")]   // Date of birth check digit is invalid character
   [InlineData("D231458907", "740812[", "1204159", "<<<<<<<", "6")]   // Date of birth check digit is invalid character
   [InlineData("D231458907", "7408122", "120415A", "<<<<<<<", "6")]   // Date of expiry check digit is invalid character
   [InlineData("D231458907", "7408122", "120415&", "<<<<<<<", "6")]   // Date of expiry check digit is invalid character
   [InlineData("D231458907", "7408122", "120415<", "<<<<<<<", "6")]   // Date of expiry check digit is invalid character
   [InlineData("D231458907", "7408122", "120415[", "<<<<<<<", "6")]   // Date of expiry check digit is invalid character
   [InlineData("D23145890<", "7408122", "1204159", "AB112A<", "4")]   // Extended document number check digit with invalid character
   [InlineData("D23145890<", "7408122", "1204159", "AB112&<", "4")]   // Extended document number check digit with invalid character
   [InlineData("D23145890<", "7408122", "1204159", "AB112<<", "4")]   // Extended document number check digit with invalid character
   [InlineData("D23145890<", "7408122", "1204159", "AB112[<", "4")]   // Extended document number check digit with invalid character
   public void Icao9303SizeTD2Algorithm_Validate_ShouldReturnFalse_WhenRequiredFieldCheckDigitContainsNonDigitCharacter(
      String documentNumber,
      String dateOfBirth,
      String dateOfExpiry,
      String optionalData,
      String compositeCheckDigit)
   {
      // Arrange.
      var value = GetTestValue(
         documentNumber: documentNumber,
         dateOfBirth: dateOfBirth,
         dateOfExpiry: dateOfExpiry,
         optionalData: optionalData,
         compositeCheckDigit: compositeCheckDigit);

      // Act/assert.
      _sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   [InlineData("A")]
   [InlineData("a")]
   [InlineData("&")]
   [InlineData(":")]
   public void Icao9303SizeTD2Algorithm_Validate_ShouldReturnFalse_WhenCompositeCheckDigitContainsNonDigitCharacter(String compositeCheckDigit)
   {
      // Arrange.
      var value = GetTestValue(compositeCheckDigit: compositeCheckDigit);

      // Act/assert.
      _sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   [InlineData("I<UTOERIKSSON<<ANNA<MARIA<<<<<<<<<<<D231458907UTO7408122F1204159<<<<<<<6")]
   [InlineData("I<UTOQWERTY<<ASDF<<<<<<<<<<<<<<<<<<<\r\nD23145890<UTO7408122F1204159AB1124<4")]
   [InlineData("I<UTOSKYWALKER<<LUKE<<<<<<<<<<<<<<<<\nSTARWARS45UTO7705256M2405252<<<<<<<4")]
   public void Icao9303SizeTD2Algorithm_Validate_ShouldReturnTrue_ForBenchmarkValues(String value)
      => _sut.Validate(value).Should().BeTrue();

   #endregion
}
