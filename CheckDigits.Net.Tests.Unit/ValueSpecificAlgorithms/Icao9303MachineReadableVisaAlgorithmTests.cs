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

   private const String _fmtANameField = "ERIKSSON<<ANNA<MARIA<<<<<<<<<<<<<<<<<<<";
   private const String _fmtBNameField = "ERIKSSON<<ANNA<MARIA<<<<<<<<<<<";

   private const String _fmtAOptionalDataField = "ZE184226B<<<<<<<";
   private const String _fmtBOptionalDataField = "ZE184226";

   private const String _lineSeparatorNone = "";
   private const String _lineSeparatorCrlf = "\r\n";
   private const String _lineSeparatorLf = "\n";

   public enum MrzFormat { A, B }

#pragma warning disable CS8524 // The switch expression does not handle some values of its input type (it is not exhaustive) involving an unnamed enum value.
   private static String GetNameField(MrzFormat format)
      => format switch
      {
         MrzFormat.A => _fmtANameField,
         MrzFormat.B => _fmtBNameField,
      };

   private static String GetOptionalDataField(MrzFormat format)
      => format switch
      {
         MrzFormat.A => _fmtAOptionalDataField,
         MrzFormat.B => _fmtBOptionalDataField,
      };
#pragma warning restore CS8524 // The switch expression does not handle some values of its input type (it is not exhaustive) involving an unnamed enum value.

   private static String GetTestValue(
      String documentType = "V<",
      String issuingState = "UTO",
      String name = _fmtANameField,
      String lineSeparator = _lineSeparatorNone,
      String documentNumber = "D231458907",              // 9 alphanumeric characters + 1 check digit
      String nationality = "UTO",
      String dateOfBirth = "7408122",                    // 6 digit characters + 1 check digit
      String sex = "F",
      String dateOfExpiry = "1204159",                   // 6 digit characters + 1 check digit
      String optionalData = _fmtAOptionalDataField)
      => $"{documentType}{issuingState}{name}" +
         lineSeparator +
         $"{documentNumber}{nationality}{dateOfBirth}{sex}{dateOfExpiry}{optionalData}";

   private static String GetTestValue(
      MrzFormat format = MrzFormat.A,
      String documentType = "V<",
      String issuingState = "UTO",
      String lineSeparator = _lineSeparatorNone,
      String documentNumber = "D231458907",              // 9 alphanumeric characters + 1 check digit
      String nationality = "UTO",
      String dateOfBirth = "7408122",                    // 6 digit characters + 1 check digit
      String sex = "F",
      String dateOfExpiry = "1204159")                   // 6 digit characters + 1 check digit
      => $"{documentType}{issuingState}{GetNameField(format)}" +
         lineSeparator +
         $"{documentNumber}{nationality}{dateOfBirth}{sex}{dateOfExpiry}{GetOptionalDataField(format)}";

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

   [Theory]
   [InlineData("ERIKSSON<<ANNA<MARIA<<<<<<<<<<<<<<<<<<", _lineSeparatorNone, _fmtAOptionalDataField)]      // Name -1 char and default line separator = total length 87
   [InlineData("ERIKSSON<<ANNA<MARIA<<<<<<<<<<<<<<<<<<<<", _lineSeparatorNone, _fmtAOptionalDataField)]    // Name -1 char and default line separator = total length 91
   [InlineData("ERIKSSON<<ANNA<MARIA<<<<<<<<<<", _lineSeparatorNone, _fmtBOptionalDataField)]              // Name -1 char and default line separator = total length 71
   [InlineData("ERIKSSON<<ANNA<MARIA<<<<<<<<<<<<", _lineSeparatorNone, _fmtBOptionalDataField)]            // Name -1 char and default line separator = total length 95
   public void Icao9303MachineReadableVisaAlgorithm_Validate_ShouldReturnFalse_WhenValueIsInvalidLength(
      String name,
      String lineSeparator,
      String optionalData)
   {
      // Arrange.
      var value = GetTestValue(name: name, lineSeparator: lineSeparator, optionalData: optionalData);

      // Act/assert.
      _sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   [InlineData(MrzFormat.A, _lineSeparatorNone)]
   [InlineData(MrzFormat.A, _lineSeparatorCrlf)]
   [InlineData(MrzFormat.A, _lineSeparatorLf)]
   [InlineData(MrzFormat.B, _lineSeparatorNone)]
   [InlineData(MrzFormat.B, _lineSeparatorCrlf)]
   [InlineData(MrzFormat.B, _lineSeparatorLf)]
   public void Icao9303MachineReadableVisaAlgorithm_Validate_ShouldReturnTrue_WhenLineSeparatorIsValid(
      MrzFormat format,
      String lineSeparator)
   {
      // Arrange.
      var value = GetTestValue(format: format, lineSeparator: lineSeparator);

      // Act/assert.
      _sut.Validate(value).Should().BeTrue();
   }

   [Theory]
   [InlineData(MrzFormat.A, "X\n")]     // 'X' instead of \r
   [InlineData(MrzFormat.A, " \n")]     // Space instead of \r
   [InlineData(MrzFormat.A, "\r ")]     // Space instead of \n
   [InlineData(MrzFormat.A, "\n\r")]    // \n\r instead of \r\n
   [InlineData(MrzFormat.B, "X\n")]     // 'X' instead of \r
   [InlineData(MrzFormat.B, " \n")]     // Space instead of \r
   [InlineData(MrzFormat.B, "\r ")]     // Space instead of \n
   [InlineData(MrzFormat.B, "\n\r")]    // \n\r instead of \r\n
   public void Icao9303MachineReadableVisaAlgorithm_Validate_ShouldReturnFalse_WhenLineSeparatorIsInvalid(
      MrzFormat format,
      String lineSeparator)
   {
      // Arrange.
      var value = GetTestValue(format: format, lineSeparator: lineSeparator);

      // Act/assert.
      _sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   // Test data for edge cases where separator validation cannot detect certain 
   // issues due to length ambiguity. For example, when the first line is 
   // shortened by exactly one character, a CRLF separator's LF character falls 
   // at the position where an LF-only separator would be expected, making the 
   // error undetectable by length validation alone.
   [InlineData("ERIKSSON<<ANNA<MARIA<<<<<<<<<<<<<<<<<<", _lineSeparatorCrlf, _fmtAOptionalDataField)]      // Length indicates Lf only and Lf falls in correct position
   [InlineData("ERIKSSON<<ANNA<MARIA<<<<<<<<<<<<<<<<<<", _lineSeparatorLf, _fmtAOptionalDataField)]        // Length indicates no separator so separator chars not checked
   [InlineData("ERIKSSON<<ANNA<MARIA<<<<<<<<<<", _lineSeparatorCrlf, _fmtBOptionalDataField)]              // Length indicates Lf only and Lf falls in correct position
   [InlineData("ERIKSSON<<ANNA<MARIA<<<<<<<<<<", _lineSeparatorLf, _fmtBOptionalDataField)]                // Length indicates no separator so separator chars not checked
   public void Icao9303MachineReadableVisaAlgorithm_Validate_ShouldReturnTrue_WhenUndetectableInvalidSeparator(
      String name,
      String lineSeparator,
      String optionalData)
   {
      // Arrange.
      var value = GetTestValue(name: name, lineSeparator: lineSeparator, optionalData: optionalData);

      // Act/assert.
      _sut.Validate(value).Should().BeTrue();
   }

   [Theory]
   // Third posiition has weight = 1, so easier to calculate field check digit
   [InlineData(MrzFormat.A, "0000000000")]
   [InlineData(MrzFormat.A, "0010000001")]
   [InlineData(MrzFormat.A, "0020000002")]
   [InlineData(MrzFormat.A, "0030000003")]
   [InlineData(MrzFormat.A, "0040000004")]
   [InlineData(MrzFormat.A, "0050000005")]
   [InlineData(MrzFormat.A, "0060000006")]
   [InlineData(MrzFormat.A, "0070000007")]
   [InlineData(MrzFormat.A, "0080000008")]
   [InlineData(MrzFormat.A, "0090000009")]
   [InlineData(MrzFormat.A, "00A0000000")]
   [InlineData(MrzFormat.A, "00B0000001")]
   [InlineData(MrzFormat.A, "00C0000002")]
   [InlineData(MrzFormat.A, "00D0000003")]
   [InlineData(MrzFormat.A, "00E0000004")]
   [InlineData(MrzFormat.A, "00F0000005")]
   [InlineData(MrzFormat.A, "00G0000006")]
   [InlineData(MrzFormat.A, "00H0000007")]
   [InlineData(MrzFormat.A, "00I0000008")]
   [InlineData(MrzFormat.B, "00J0000009")]
   [InlineData(MrzFormat.B, "00K0000000")]
   [InlineData(MrzFormat.B, "00L0000001")]
   [InlineData(MrzFormat.B, "00M0000002")]
   [InlineData(MrzFormat.B, "00N0000003")]
   [InlineData(MrzFormat.B, "00O0000004")]
   [InlineData(MrzFormat.B, "00P0000005")]
   [InlineData(MrzFormat.B, "00Q0000006")]
   [InlineData(MrzFormat.B, "00R0000007")]
   [InlineData(MrzFormat.B, "00S0000008")]
   [InlineData(MrzFormat.B, "00T0000009")]
   [InlineData(MrzFormat.B, "00U0000000")]
   [InlineData(MrzFormat.B, "00V0000001")]
   [InlineData(MrzFormat.B, "00W0000002")]
   [InlineData(MrzFormat.B, "00X0000003")]
   [InlineData(MrzFormat.B, "00Y0000004")]
   [InlineData(MrzFormat.B, "00Z0000005")]
   [InlineData(MrzFormat.B, "00<0000000")]
   public void Icao9303MachineReadableVisaAlgorithm_Validate_ShouldCorrectlyMapCharactersToIntegerEquivalents(
      MrzFormat format,
      String documentNumber)
   {
      // Arrange.
      var value = GetTestValue(
         format: format,
         lineSeparator: _lineSeparatorCrlf,
         documentNumber: documentNumber);

      // Act/assert.
      _sut.Validate(value).Should().BeTrue();
   }

   [Theory]
   // Check digits are accurate, if equivalent uppercase character
   [InlineData(MrzFormat.A, "00a0000000")]
   [InlineData(MrzFormat.A, "00b0000001")]
   [InlineData(MrzFormat.A, "00c0000002")]
   [InlineData(MrzFormat.A, "00d0000003")]
   [InlineData(MrzFormat.A, "00e0000004")]
   [InlineData(MrzFormat.A, "00f0000005")]
   [InlineData(MrzFormat.A, "00g0000006")]
   [InlineData(MrzFormat.A, "00h0000007")]
   [InlineData(MrzFormat.A, "00i0000008")]
   [InlineData(MrzFormat.B, "00j0000009")]
   [InlineData(MrzFormat.B, "00k0000000")]
   [InlineData(MrzFormat.B, "00l0000001")]
   [InlineData(MrzFormat.B, "00m0000002")]
   [InlineData(MrzFormat.B, "00n0000003")]
   [InlineData(MrzFormat.B, "00o0000004")]
   [InlineData(MrzFormat.B, "00p0000005")]
   [InlineData(MrzFormat.B, "00q0000006")]
   [InlineData(MrzFormat.B, "00r0000007")]
   [InlineData(MrzFormat.B, "00s0000008")]
   [InlineData(MrzFormat.B, "00t0000009")]
   [InlineData(MrzFormat.B, "00u0000000")]
   [InlineData(MrzFormat.B, "00v0000001")]
   [InlineData(MrzFormat.B, "00w0000002")]
   [InlineData(MrzFormat.B, "00x0000003")]
   [InlineData(MrzFormat.B, "00y0000004")]
   [InlineData(MrzFormat.B, "00z0000005")]
   public void Icao9303MachineReadableVisaAlgorithm_Validate_ShouldReturnFalse_WhenLowerCaseAlphabeticCharacterEncountered(
      MrzFormat format,
      String documentNumber)
   {
      // Arrange.
      var value = GetTestValue(
         format: format,
         lineSeparator: _lineSeparatorCrlf,
         documentNumber: documentNumber);

      // Act/assert.
      _sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   // Check digits would be correct if alpha characters allowed
   [InlineData("00A0000", "0000000")]
   [InlineData("00a0000", "0000000")]
   [InlineData("0000000", "00A0000")]
   [InlineData("0000000", "00a0000")]
   public void Icao9303MachineReadableVisaAlgorithm_Validate_ShouldReturnFalse_NumericFieldContainsAlphabeticCharacter(
      String dateOfBirth,
      String dateOfExpiry)
   {
      // Arrange.
      var value = GetTestValue(
         format: MrzFormat.A,
         dateOfBirth: dateOfBirth,
         dateOfExpiry: dateOfExpiry);

      // Act/assert.
      _sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   // Document number field
   [InlineData(MrzFormat.A, _lineSeparatorNone, "1000000007", "0000000", "0000000")]
   [InlineData(MrzFormat.A, _lineSeparatorNone, "0100000003", "0000000", "0000000")]
   [InlineData(MrzFormat.A, _lineSeparatorNone, "0010000001", "0000000", "0000000")]
   [InlineData(MrzFormat.A, _lineSeparatorNone, "0001000007", "0000000", "0000000")]
   [InlineData(MrzFormat.A, _lineSeparatorNone, "0000100003", "0000000", "0000000")]
   [InlineData(MrzFormat.B, _lineSeparatorNone, "0000010001", "0000000", "0000000")]  
   [InlineData(MrzFormat.B, _lineSeparatorNone, "0000001007", "0000000", "0000000")]  
   [InlineData(MrzFormat.B, _lineSeparatorNone, "0000000103", "0000000", "0000000")]  
   [InlineData(MrzFormat.B, _lineSeparatorNone, "0000000011", "0000000", "0000000")]  
   // Date of birth field
   [InlineData(MrzFormat.A, _lineSeparatorCrlf, "0000000000", "1000007", "0000000")]
   [InlineData(MrzFormat.A, _lineSeparatorCrlf, "0000000000", "0100003", "0000000")]
   [InlineData(MrzFormat.A, _lineSeparatorCrlf, "0000000000", "0010001", "0000000")]
   [InlineData(MrzFormat.B, _lineSeparatorCrlf, "0000000000", "0001007", "0000000")]
   [InlineData(MrzFormat.B, _lineSeparatorCrlf, "0000000000", "0000103", "0000000")]
   [InlineData(MrzFormat.B, _lineSeparatorCrlf, "0000000000", "0000011", "0000000")]
   // Date of expiry field
   [InlineData(MrzFormat.A, _lineSeparatorLf,   "0000000000", "0000000", "1000007")]
   [InlineData(MrzFormat.A, _lineSeparatorLf,   "0000000000", "0000000", "0100003")]
   [InlineData(MrzFormat.A, _lineSeparatorLf,   "0000000000", "0000000", "0010001")]
   [InlineData(MrzFormat.B, _lineSeparatorLf,   "0000000000", "0000000", "0001007")]
   [InlineData(MrzFormat.B, _lineSeparatorLf,   "0000000000", "0000000", "0000103")]
   [InlineData(MrzFormat.B, _lineSeparatorLf,   "0000000000", "0000000", "0000011")]
   public void Icao9303MachineReadableVisaAlgorithm_Validate_ShouldCorrectlyWeightByCharacterPosition(
      MrzFormat format,
      String lineSeparator,
      String documentNumber,
      String dateOfBirth,
      String dateOfExpiry)
   {
      // Arrange.
      var value = GetTestValue(
         format: format, 
         lineSeparator: lineSeparator,
         documentNumber: documentNumber,
         dateOfBirth: dateOfBirth,
         dateOfExpiry: dateOfExpiry);

      // Act/assert.
      _sut.Validate(value).Should().BeTrue();
   }

   [Theory]
   [InlineData(MrzFormat.A, _lineSeparatorNone, "D231458907", "7408122", "1204159")]
   [InlineData(MrzFormat.A, _lineSeparatorCrlf, "L898902C<3", "6908061", "9406236")]
   [InlineData(MrzFormat.A, _lineSeparatorLf,   "STARWARS45", "7705256", "2405252")]
   [InlineData(MrzFormat.B, _lineSeparatorNone, "D231458907", "7408122", "1204159")]
   [InlineData(MrzFormat.B, _lineSeparatorCrlf, "L898902C<3", "6908061", "9406236")]
   [InlineData(MrzFormat.B, _lineSeparatorLf,   "STARWARS45", "7705256", "2405252")]
   public void Icao9303MachineReadableVisaAlgorithm_Validate_ShouldReturnTrue_WhenValueContainsValidCheckDigits(
      MrzFormat format,
      String lineSeparator,
      String documentNumber,
      String dateOfBirth,
      String dateOfExpiry)
   {
      // Arrange.
      var value = GetTestValue(
         format: format, 
         lineSeparator: lineSeparator,
         documentNumber: documentNumber,
         dateOfBirth: dateOfBirth,
         dateOfExpiry: dateOfExpiry);

      // Act/assert.
      _sut.Validate(value).Should().BeTrue();
   }

   [Theory]
   [InlineData(MrzFormat.A, _lineSeparatorNone, "D2314589A7", "7408122", "1204159")]   // Document number D231458907 with single char transcription error (0 -> A) with delta 10   
   [InlineData(MrzFormat.A, _lineSeparatorCrlf, "N231458907", "7408122", "1204159")]   // Document number D231458907 with single char transcription error (D -> N) with delta 10
   [InlineData(MrzFormat.A, _lineSeparatorLf,   "L89890C236", "7408122", "1204159")]   // Document number L898902C36 with two char transposition error (2C -> C2) with delta 10
   [InlineData(MrzFormat.A, _lineSeparatorNone, "0000000000", "8812728", "0000000")]   // Date of birth 8812278 with two char transposition error (27 -> 72) with delta 5
   [InlineData(MrzFormat.A, _lineSeparatorCrlf, "0000000000", "0000000", "8812728")]   // Date of expiry 8812278 with two char transposition error (27 -> 72) with delta 5
   [InlineData(MrzFormat.B, _lineSeparatorNone, "D2314589A7", "7408122", "1204159")]   // Document number D231458907 with single char transcription error (0 -> A) with delta 10   
   [InlineData(MrzFormat.B, _lineSeparatorCrlf, "N231458907", "7408122", "1204159")]   // Document number D231458907 with single char transcription error (D -> N) with delta 10
   [InlineData(MrzFormat.B, _lineSeparatorLf,   "L89890C236", "7408122", "1204159")]   // Document number L898902C36 with two char transposition error (2C -> C2) with delta 10
   [InlineData(MrzFormat.B, _lineSeparatorNone, "0000000000", "8812728", "0000000")]   // Date of birth 8812278 with two char transposition error (27 -> 72) with delta 5
   [InlineData(MrzFormat.B, _lineSeparatorCrlf, "0000000000", "0000000", "8812728")]   // Date of expiry 8812278 with two char transposition error (27 -> 72) with delta 5
   public void Icao9303MachineReadableVisaAlgorithm_Validate_ShouldReturnTrue_WhenValueContainsUndetectableError(
      MrzFormat format,
      String lineSeparator,
      String documentNumber,
      String dateOfBirth,
      String dateOfExpiry)
   {
      // Arrange.
      var value = GetTestValue(
         format: format, 
         lineSeparator: lineSeparator,
         documentNumber: documentNumber,
         dateOfBirth: dateOfBirth,
         dateOfExpiry: dateOfExpiry);

      // Act/assert.
      _sut.Validate(value).Should().BeTrue();
   }

   [Theory]
   [InlineData(MrzFormat.A, _lineSeparatorNone, "E231458907", "7408122", "1204159")]   // Document number D231458907 with single character transcription error (D -> E)
   [InlineData(MrzFormat.A, _lineSeparatorCrlf, "D241458907", "7408122", "1204159")]   // Document number D231458907 with single digit transcription error (3 -> 4)
   [InlineData(MrzFormat.A, _lineSeparatorLf,   "D231458907", "7409122", "1204159")]   // Date of birth 7408122 with single digit transcription error (8 -> 9)
   [InlineData(MrzFormat.A, _lineSeparatorNone, "D231458907", "7408122", "1203159")]   // Date of expiry 1204159 with single digit transcription error (4 -> 3)
   [InlineData(MrzFormat.B, _lineSeparatorCrlf, "2D31458907", "7408122", "1204159")]   // Document number D231458907 with two character transposition error (D2 -> 2D)
   [InlineData(MrzFormat.B, _lineSeparatorLf,   "D231548907", "7408122", "1204159")]   // Document number D231458907 with two digit transposition error (45 -> 54)
   [InlineData(MrzFormat.B, _lineSeparatorNone, "D231458907", "7408212", "1204159")]   // Date of birth 7408122 with two digit transposition error (12 -> 21)
   [InlineData(MrzFormat.B, _lineSeparatorCrlf, "D231458907", "7409122", "1201459")]   // Date of expiry 1204159 with two digit transposition error (41 -> 14)
   public void Icao9303MachineReadableVisaAlgorithm_Validate_ShouldReturnFalse_WhenValueContainsDetectableError(
      MrzFormat format,
      String lineSeparator,
      String documentNumber,
      String dateOfBirth,
      String dateOfExpiry)
   {
      // Arrange.
      var value = GetTestValue(
         format: format, 
         lineSeparator: lineSeparator,
         documentNumber: documentNumber,
         dateOfBirth: dateOfBirth,
         dateOfExpiry: dateOfExpiry);

      // Act/assert.
      _sut.Validate(value).Should().BeFalse();
   }

   [Fact]
   public void Icao9303MachineReadableVisaAlgorithm_Validate_ShouldReturnFalse_WhenMultipleFieldsContainInvalidCharacters()
   {
      // Arrange.
      var value = GetTestValue(
         MrzFormat.B,
         documentNumber: "D23145890!",
         dateOfBirth: "740812@",
         dateOfExpiry: "120415#");

      // Act/assert.
      _sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   [InlineData(MrzFormat.A, _lineSeparatorNone, "0000000000", "0000000", "0000000")]
   [InlineData(MrzFormat.B, _lineSeparatorCrlf, "0000000000", "0000000", "0000000")]
   public void Icao9303MachineReadableVisaAlgorithm_Validate_ShouldReturnTrue_WhenFieldsAreAllZeros(
      MrzFormat format,
      String lineSeparator,
      String documentNumber,
      String dateOfBirth,
      String dateOfExpiry)
   {
      // Arrange.
      var value = GetTestValue(
         format: format, 
         lineSeparator: lineSeparator,
         documentNumber: documentNumber,
         dateOfBirth: dateOfBirth,
         dateOfExpiry: dateOfExpiry);

      // Act/assert.
      _sut.Validate(value).Should().BeTrue();
   }

   [Theory]
   [InlineData(MrzFormat.A, _lineSeparatorLf,   "<<<<<<<<<0", "<<<<<<0", "<<<<<<0")]
   [InlineData(MrzFormat.B, _lineSeparatorNone, "<<<<<<<<<0", "<<<<<<0", "<<<<<<0")]
   public void Icao9303MachineReadableVisaAlgorithm_Validate_ShouldReturnTrue_WhenFieldsAreAllFillerCharacters(
      MrzFormat format,
      String lineSeparator,
      String documentNumber,
      String dateOfBirth,
      String dateOfExpiry)
   {
      // Arrange.
      var value = GetTestValue(
         format: format, 
         lineSeparator: lineSeparator,
         documentNumber: documentNumber,
         dateOfBirth: dateOfBirth,
         dateOfExpiry: dateOfExpiry);

      // Act/assert.
      _sut.Validate(value).Should().BeTrue();
   }

   [Theory]
   [InlineData(MrzFormat.A, _lineSeparatorNone, "b231458907", "7408122", "1204159")]   // Document number D231458907 with D replaced with character 30 positions later in ASCII table
   [InlineData(MrzFormat.A, _lineSeparatorCrlf, "D2314589&7", "7408122", "1204159")]   // Document number D231458907 with 0 replaced with character 10 positions before in ASCII table
   [InlineData(MrzFormat.B, _lineSeparatorLf,   "D2314589:7", "7408122", "1204159")]   // Document number D231458907 with 0 replaced with character 10 positions later in ASCII table
   [InlineData(MrzFormat.B, _lineSeparatorNone, "D231458907", "7>08122", "1204159")]   // Date of birth 7408122 with 4 replaced with character 10 positions later in ASCII table
   [InlineData(MrzFormat.B, _lineSeparatorNone, "D231458907", "7408122", "120>159")]   // Date of expiry 1204159 with 4 replaced with character 10 positions later in ASCII table
   public void Icao9303MachineReadableVisaAlgorithm_Validate_ShouldReturnFalse_WhenFieldContainsInvalidCharacter(
      MrzFormat format,
      String lineSeparator,
      String documentNumber,
      String dateOfBirth,
      String dateOfExpiry)
   {
      // Arrange.
      var value = GetTestValue(
         format: format, 
         lineSeparator: lineSeparator,
         documentNumber: documentNumber,
         dateOfBirth: dateOfBirth,
         dateOfExpiry: dateOfExpiry);

      // Act/assert.
      _sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   [InlineData(MrzFormat.A, _lineSeparatorNone, "D23145890A", "7408122", "1204159")]   // Document number check digit is invalid character
   [InlineData(MrzFormat.A, _lineSeparatorCrlf, "D23145890&", "7408122", "1204159")]   // Document number check digit is invalid character
   [InlineData(MrzFormat.A, _lineSeparatorLf,   "D23145890:", "7408122", "1204159")]   // Document number check digit is invalid character
   [InlineData(MrzFormat.A, _lineSeparatorNone, "D231458907", "740812A", "1204159")]   // Date of birth check digit is invalid character
   [InlineData(MrzFormat.A, _lineSeparatorCrlf, "D231458907", "740812&", "1204159")]   // Date of birth check digit is invalid character
   [InlineData(MrzFormat.A, _lineSeparatorLf,   "D231458907", "740812<", "1204159")]   // Date of birth check digit is invalid character
   [InlineData(MrzFormat.A, _lineSeparatorNone, "D231458907", "740812[", "1204159")]   // Date of birth check digit is invalid character
   [InlineData(MrzFormat.B, _lineSeparatorCrlf, "D231458907", "7408122", "120415A")]   // Date of expiry check digit is invalid character
   [InlineData(MrzFormat.B, _lineSeparatorLf,   "D231458907", "7408122", "120415&")]   // Date of expiry check digit is invalid character
   [InlineData(MrzFormat.B, _lineSeparatorNone, "D231458907", "7408122", "120415<")]   // Date of expiry check digit is invalid character
   [InlineData(MrzFormat.B, _lineSeparatorCrlf, "D231458907", "7408122", "120415[")]   // Date of expiry check digit is invalid character
   public void Icao9303MachineReadableVisaAlgorithm_Validate_ShouldReturnFalse_WhenRequiredFieldCheckDigitContainsNonDigitCharacter(
      MrzFormat format,
      String lineSeparator,
      String documentNumber,
      String dateOfBirth,
      String dateOfExpiry)
   {
      // Arrange.
      var value = GetTestValue(
         format: format, 
         lineSeparator: lineSeparator,
         documentNumber: documentNumber,
         dateOfBirth: dateOfBirth,
         dateOfExpiry: dateOfExpiry);

      // Act/assert.
      _sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   [InlineData("I<UTOERIKSSON<<ANNA<MARIA<<<<<<<<<<<D231458907UTO7408122F1204159<<<<<<A<")]
   [InlineData("V<UTOERIKSSON<<ANNA<MARIA<<<<<<<<<<<<<<<<<<<\r\nL898902C<3UTO6908061F9406236ZE184226B<<<<<<<")]
   [InlineData("I<UTOSKYWALKER<<LUKE<<<<<<<<<<<<<<<<\nSTARWARS45UTO7705256M2405252<<<<<<B<")]
   public void Icao9303MachineReadableVisaAlgorithm_Validate_ShouldReturnTrue_ForBenchmarkValues(String value)
      => _sut.Validate(value).Should().BeTrue();

   #endregion
}
