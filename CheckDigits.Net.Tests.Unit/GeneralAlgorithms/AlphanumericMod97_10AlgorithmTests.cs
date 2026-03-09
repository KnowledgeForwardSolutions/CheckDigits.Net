namespace CheckDigits.Net.Tests.Unit.GeneralAlgorithms;

public class AlphanumericMod97_10AlgorithmTests
{
   private readonly AlphanumericMod97_10Algorithm _sut = new();
   private readonly ICheckDigitMask _acceptAllMask = new AcceptAllMask();
   private readonly ICheckDigitMask _groupsOfThreeMask = new GroupsOfThreeCheckDigitMask();
   private readonly ICheckDigitMask _rejectAllMask = new RejectAllMask();

   #region AlgorithmDescription Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void AlphanumericMod97_10Algorithm_AlgorithmDescription_ShouldReturnExpectedValue()
      => _sut.AlgorithmDescription.Should().Be(Resources.AlphanumericMod97_10AlgorithmDescription);

   #endregion

   #region AlgorithmName Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void AlphanumericMod97_10Algorithm_AlgorithmName_ShouldReturnExpectedValue()
      => _sut.AlgorithmName.Should().Be(Resources.AlphanumericMod97_10AlgorithmName);

   #endregion

   #region TryCalculateCheckDigits Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void AlphanumericMod97_10Algorithm_TryCalculateCheckDigits_ShouldReturnFalse_WhenInputIsNull()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigits(null!, out var first, out var second).Should().BeFalse();
      first.Should().Be('\0');
      second.Should().Be('\0');
   }

   [Fact]
   public void AlphanumericMod97_10Algorithm_TryCalculateCheckDigits_ShouldReturnFalse_WhenInputIsEmpty()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigits(String.Empty, out var first, out var second).Should().BeFalse();
      first.Should().Be('\0');
      second.Should().Be('\0');
   }

   [Theory]
   [InlineData("0", '9', '8')]
   [InlineData("1", '9', '5')]
   [InlineData("2", '9', '2')]
   [InlineData("3", '8', '9')]
   [InlineData("4", '8', '6')]
   [InlineData("5", '8', '3')]
   [InlineData("6", '8', '0')]
   [InlineData("7", '7', '7')]
   [InlineData("8", '7', '4')]
   [InlineData("9", '7', '1')]
   [InlineData("A", '6', '8')]
   [InlineData("B", '6', '5')]
   [InlineData("C", '6', '2')]
   [InlineData("D", '5', '9')]
   [InlineData("E", '5', '6')]
   [InlineData("F", '5', '3')]
   [InlineData("G", '5', '0')]
   [InlineData("H", '4', '7')]
   [InlineData("I", '4', '4')]
   [InlineData("J", '4', '1')]
   [InlineData("K", '3', '8')]
   [InlineData("L", '3', '5')]
   [InlineData("M", '3', '2')]
   [InlineData("N", '2', '9')]
   [InlineData("O", '2', '6')]
   [InlineData("P", '2', '3')]
   [InlineData("Q", '2', '0')]
   [InlineData("R", '1', '7')]
   [InlineData("S", '1', '4')]
   [InlineData("T", '1', '1')]
   [InlineData("U", '0', '8')]
   [InlineData("V", '0', '5')]
   [InlineData("W", '0', '2')]
   [InlineData("X", '9', '6')]
   [InlineData("Y", '9', '3')]
   [InlineData("Z", '9', '0')]
   [InlineData("a", '6', '8')]
   [InlineData("b", '6', '5')]
   [InlineData("c", '6', '2')]
   [InlineData("d", '5', '9')]
   [InlineData("e", '5', '6')]
   [InlineData("f", '5', '3')]
   [InlineData("g", '5', '0')]
   [InlineData("h", '4', '7')]
   [InlineData("i", '4', '4')]
   [InlineData("j", '4', '1')]
   [InlineData("k", '3', '8')]
   [InlineData("l", '3', '5')]
   [InlineData("m", '3', '2')]
   [InlineData("n", '2', '9')]
   [InlineData("o", '2', '6')]
   [InlineData("p", '2', '3')]
   [InlineData("q", '2', '0')]
   [InlineData("r", '1', '7')]
   [InlineData("s", '1', '4')]
   [InlineData("t", '1', '1')]
   [InlineData("u", '0', '8')]
   [InlineData("v", '0', '5')]
   [InlineData("w", '0', '2')]
   [InlineData("x", '9', '6')]
   [InlineData("y", '9', '3')]
   [InlineData("z", '9', '0')]
   public void AlphanumericMod97_10Algorithm_TryCalculateCheckDigits_ShouldCorrectlyMapCharacterValues(
      String value,
      Char expectedFirst,
      Char expectedSecond)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigits(value, out var first, out var second).Should().BeTrue();
      first.Should().Be(expectedFirst);
      second.Should().Be(expectedSecond);
   }

   [Theory]
   [InlineData("10Bx939c5543TqA1144M999143X", '3', '8')]         // Worked example from https://www.govinfo.gov/content/pkg/CFR-2016-title12-vol8/xml/CFR-2016-title12-vol8-part1003-appC.xml
   [InlineData("549300KM40FP4MSQU94112345QWERTY9876", '4', '8')] // Generated ULI from https://ffiec.cfpb.gov/tools/check-digit
   [InlineData("549300UDFJVWBIHXA0", '5', '8')]                  // LEI for Alphabet, from https://lei.info/
   [InlineData("549300NL8PIYPQDKDA", '7', '2')]                  // LEI for Apple
   [InlineData("967600DJA1Q8K13MZ8", '4', '5')]                  // LEI for Microsoft
   public void AlphanumericMod97_10AlgorithmAlgorithm_TryCalculateCheckDigits_ShouldCalculateExpectedCheckDigit(
      String value,
      Char expectedFirst,
      Char expectedSecond)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigits(value, out var first, out var second).Should().BeTrue();
      first.Should().Be(expectedFirst);
      second.Should().Be(expectedSecond);
   }

   [Theory]
   [InlineData("AA00123!56")]
   [InlineData("AA00123^56")]
   [InlineData("AA00123=56")]
   public void AlphanumericMod97_10Algorithm_TryCalculateCheckDigits_ShouldReturnFalse_WhenInputContainsInvalidCharacter(String value)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigits(value, out var first, out var second).Should().BeFalse();
      first.Should().Be('\0');
      second.Should().Be('\0');
   }

   [Theory]
   [InlineData("U7y")]
   [InlineData("U7y8SX")]
   [InlineData("U7y8SXrC0")]
   [InlineData("U7y8SXrC0O3S")]
   [InlineData("U7y8SXrC0O3Sc4I")]
   [InlineData("U7y8SXrC0O3Sc4IHYQ")]
   [InlineData("U7y8SXrC0O3Sc4IHYQF4M")]
   public void AlphanumericMod97_10Algorithm_TryCalculateCheckDigit_ShouldReturnTrue_ForBenchmarkValues(String value)
      => _sut.TryCalculateCheckDigits(value, out _, out _).Should().BeTrue();

   #endregion

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void AlphanumericMod97_10Algorithm_Validate_ShouldReturnFalse_WhenInputIsNull()
      => _sut.Validate(null!).Should().BeFalse();

   [Fact]
   public void AlphanumericMod97_10Algorithm_Validate_ShouldReturnFalse_WhenInputIsEmpty()
      => _sut.Validate(String.Empty).Should().BeFalse();

   [Theory]
   [InlineData("A")]
   [InlineData("AB")]
   [InlineData("98")]         // Would otherise be true if length requirement was not enforced
   public void AlphanumericMod97_10Algorithm_Validate_ShouldReturnFalse_WhenInputIsLessThanThreeCharactersInLength(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Theory]
   [InlineData("098")]
   [InlineData("195")]
   [InlineData("292")]
   [InlineData("389")]
   [InlineData("486")]
   [InlineData("583")]
   [InlineData("680")]
   [InlineData("777")]
   [InlineData("874")]
   [InlineData("971")]
   [InlineData("A68")]
   [InlineData("B65")]
   [InlineData("C62")]
   [InlineData("D59")]
   [InlineData("E56")]
   [InlineData("F53")]
   [InlineData("G50")]
   [InlineData("H47")]
   [InlineData("I44")]
   [InlineData("J41")]
   [InlineData("K38")]
   [InlineData("L35")]
   [InlineData("M32")]
   [InlineData("N29")]
   [InlineData("O26")]
   [InlineData("P23")]
   [InlineData("Q20")]
   [InlineData("R17")]
   [InlineData("S14")]
   [InlineData("T11")]
   [InlineData("U08")]
   [InlineData("V05")]
   [InlineData("W02")]
   [InlineData("X96")]
   [InlineData("Y93")]
   [InlineData("Z90")]
   [InlineData("a68")]
   [InlineData("b65")]
   [InlineData("c62")]
   [InlineData("d59")]
   [InlineData("e56")]
   [InlineData("f53")]
   [InlineData("g50")]
   [InlineData("h47")]
   [InlineData("i44")]
   [InlineData("j41")]
   [InlineData("k38")]
   [InlineData("l35")]
   [InlineData("m32")]
   [InlineData("n29")]
   [InlineData("o26")]
   [InlineData("p23")]
   [InlineData("q20")]
   [InlineData("r17")]
   [InlineData("s14")]
   [InlineData("t11")]
   [InlineData("u08")]
   [InlineData("v05")]
   [InlineData("w02")]
   [InlineData("x96")]
   [InlineData("y93")]
   [InlineData("z90")]
   public void AlphanumericMod97_10Algorithm_Validate_ShouldCorrectlyMapCharacterValues(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("10Bx939c5543TqA1144M999143X38")]         // Worked example from https://www.govinfo.gov/content/pkg/CFR-2016-title12-vol8/xml/CFR-2016-title12-vol8-part1003-appC.xml
   [InlineData("549300KM40FP4MSQU94112345QWERTY987648")] // Generated ULI from https://ffiec.cfpb.gov/tools/check-digit
   [InlineData("549300UDFJVWBIHXA058")]                  // LEI for Alphabet, from https://lei.info/
   [InlineData("549300NL8PIYPQDKDA72")]                  // LEI for Apple
   [InlineData("967600DJA1Q8K13MZ845")]                  // LEI for Microsoft
   public void AlphanumericMod97_10Algorithm_Validate_ShouldReturnTrue_WhenValueContainsValidCheckDigit(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("540300UDFJVWBIHXA058")]                  // 549300UDFJVWBIHXA058 with single digit transcription error 9 -> 0
   [InlineData("549300UDFJVEBIHXA058")]                  // 549300UDFJVWBIHXA058 with single char transcription error W -> E
   [InlineData("10Bx939c5453TqA1144M999143X38")]         // 10Bx939c5543TqA1144M999143X38 with two digit transposition error 54 -> 45
   [InlineData("549300NL8PIPYQDKDA72")]                  // 549300NL8PIYPQDKDA72 with two char transposition error YP -> PY 
   [InlineData("10Bx939c5543TqA2244M999143X38")]         // 10Bx939c5543TqA1144M999143X38 with two digit twin error 11 -> 22
   [InlineData("ASDF12345BBDDFF09876166")]               // ASDF12345BBDDEE09876166 with two char twin error EE -> FF
   [InlineData("10Bx939c5543TqA1144M994193X38")]         // SC74MCB10Bx939c5543TqA1144M999143X38L01031234567890123456USD with jump transposition error 914 -> 419
   [InlineData("SC74MC0LB1031234567890123456USD")]       // SC74MCBL01031234567890123456USD with jump transposition error BL0 -> 0LB
   [InlineData("967600AJD1Q8K13MZ845")]                  // 967600DJA1Q8K13MZ845 with jump transposition error DJA -> AJD
   [InlineData("54930LN08PIYPQDKDA72")]                  // 549300NL8PIYPQDKDA72 with jump transposition error 0NL -> LN0
   [InlineData("49300UDFJVWBIHXA0585")]                  // 549300UDFJVWBIHXA058 with circular shift error
   [InlineData("8549300UDFJVWBIHXA05")]                  // 549300UDFJVWBIHXA058 with circular shift error
   public void AlphanumericMod97_10Algorithm_Validate_ShouldReturnFalse_WhenInputContainsDetectableError(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Theory]
   [InlineData("AA13!56")]
   [InlineData("AA13^56")]
   [InlineData("AA13=56")]
   public void AlphanumericMod97_10Algorithm_Validate_ShouldReturnFalse_WhenValueContainsInvalidCharacter(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Fact]
   public void AlphanumericMod97_10Algorithm_Validate_ShouldReturnFalse_WhenValueContainsInvalidFirstCheckCharacter()
      => _sut.Validate("AA123#0").Should().BeFalse();

   [Fact]
   public void AlphanumericMod97_10Algorithm_Validate_ShouldReturnFalse_WhenValueContainsInvalidSecondCheckCharacter()
      => _sut.Validate("AA1230#").Should().BeFalse();

   [Theory]
   [InlineData("U7y46")]
   [InlineData("U7y8SX89")]
   [InlineData("U7y8SXrC087")]
   [InlineData("U7y8SXrC0O3S38")]
   [InlineData("U7y8SXrC0O3Sc4I27")]
   [InlineData("U7y8SXrC0O3Sc4IHYQ54")]
   [InlineData("U7y8SXrC0O3Sc4IHYQF4M21")]
   public void AlphanumericMod97_10Algorithm_Validate_ShouldReturnTrue_ForBenchmarkValues(String value)
      => _sut.Validate(value).Should().BeTrue();

   #endregion

   #region Validate (ICheckDigitMask Overload) Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void AlphanumericMod97_10Algorithm_ValidateMasked_ShouldThrowArgumentNullException_WhenMaskIsNull()
      => _sut
         .Invoking(x => x.Validate("12345", null!))
         .Should()
         .ThrowExactly<ArgumentNullException>()
         .WithParameterName("mask")
         .WithMessage(Resources.NullMaskMessage + "*");

   [Fact]
   public void AlphanumericMod97_10Algorithm_ValidateMasked_ShouldReturnFalse_WhenInputIsNull()
      => _sut.Validate(null!, _acceptAllMask).Should().BeFalse();

   [Fact]
   public void AlphanumericMod97_10Algorithm_ValidateMasked_ShouldReturnFalse_WhenInputIsEmpty()
      => _sut.Validate(String.Empty, _acceptAllMask).Should().BeFalse();

   [Fact]
   public void AlphanumericMod97_10Algorithm_ValidateMasked_ShouldReturnFalse_WhenAllNonCheckDigitCharactersAreMaskedOut()
      => _sut.Validate("U7y 8SX rC0 87", _rejectAllMask).Should().BeFalse();     // Would otherwise be valid if non-check characters were not masked out

   [Theory]
   [InlineData("A")]
   [InlineData("AB")]
   [InlineData("98")]         // Would otherise be true if length requirement was not enforced
   public void AlphanumericMod97Algorithm_ValidateMasked_ShouldReturnFalse_WhenInsufficientUnmaskedCharactersToCalculateCheckDigit(String value)
   {
      // Requires at least one unmasked character plus two check digit characters.
      // This should be rejected even if check digit would otherwise be valid.
      _sut.Validate(value, _acceptAllMask).Should().BeFalse();
   }

   [Theory]
   [InlineData("098")]
   [InlineData("195")]
   [InlineData("292")]
   [InlineData("389")]
   [InlineData("486")]
   [InlineData("583")]
   [InlineData("680")]
   [InlineData("777")]
   [InlineData("874")]
   [InlineData("971")]
   [InlineData("A68")]
   [InlineData("B65")]
   [InlineData("C62")]
   [InlineData("D59")]
   [InlineData("E56")]
   [InlineData("F53")]
   [InlineData("G50")]
   [InlineData("H47")]
   [InlineData("I44")]
   [InlineData("J41")]
   [InlineData("K38")]
   [InlineData("L35")]
   [InlineData("M32")]
   [InlineData("N29")]
   [InlineData("O26")]
   [InlineData("P23")]
   [InlineData("Q20")]
   [InlineData("R17")]
   [InlineData("S14")]
   [InlineData("T11")]
   [InlineData("U08")]
   [InlineData("V05")]
   [InlineData("W02")]
   [InlineData("X96")]
   [InlineData("Y93")]
   [InlineData("Z90")]
   [InlineData("a68")]
   [InlineData("b65")]
   [InlineData("c62")]
   [InlineData("d59")]
   [InlineData("e56")]
   [InlineData("f53")]
   [InlineData("g50")]
   [InlineData("h47")]
   [InlineData("i44")]
   [InlineData("j41")]
   [InlineData("k38")]
   [InlineData("l35")]
   [InlineData("m32")]
   [InlineData("n29")]
   [InlineData("o26")]
   [InlineData("p23")]
   [InlineData("q20")]
   [InlineData("r17")]
   [InlineData("s14")]
   [InlineData("t11")]
   [InlineData("u08")]
   [InlineData("v05")]
   [InlineData("w02")]
   [InlineData("x96")]
   [InlineData("y93")]
   [InlineData("z90")]
   public void AlphanumericMod97_10Algorithm_ValidateMasked_ShouldCorrectlyMapCharacterValues(String value)
      => _sut.Validate(value, _acceptAllMask).Should().BeTrue();

   // Both trailing characters are check characters and the mask is not allowed to split them, so example ending in 7648 is correct
   [Theory]
   [InlineData("10B x93 9c5 543 TqA 114 4M9 991 43X 38")]            // Worked example from https://www.govinfo.gov/content/pkg/CFR-2016-title12-vol8/xml/CFR-2016-title12-vol8-part1003-appC.xml
   [InlineData("549 300 KM4 0FP 4MS QU9 411 234 5QW ERT Y98 7648")]  // Generated ULI from https://ffiec.cfpb.gov/tools/check-digit
   [InlineData("549 300 UDF JVW BIH XA0 58")]                        // LEI for Alphabet, from https://lei.info/
   [InlineData("549 300 NL8 PIY PQD KDA 72")]                        // LEI for Apple
   [InlineData("967 600 DJA 1Q8 K13 MZ8 45")]                        // LEI for Microsoft
   public void AlphanumericMod97_10Algorithm_ValidateMasked_ShouldReturnTrue_WhenValueContainsValidCheckDigit(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeTrue();

   [Theory]
   [InlineData("10Bx939c5543TqA1144M999143X38")]         // Worked example from https://www.govinfo.gov/content/pkg/CFR-2016-title12-vol8/xml/CFR-2016-title12-vol8-part1003-appC.xml
   [InlineData("549300KM40FP4MSQU94112345QWERTY987648")] // Generated ULI from https://ffiec.cfpb.gov/tools/check-digit
   [InlineData("549300UDFJVWBIHXA058")]                  // LEI for Alphabet, from https://lei.info/
   [InlineData("549300NL8PIYPQDKDA72")]                  // LEI for Apple
   [InlineData("967600DJA1Q8K13MZ845")]                  // LEI for Microsoft
   public void AlphanumericMod97_10Algorithm_ValidateMasked_ShouldReturnTrue_WhenValueContainsValidCheckDigitAndMaskAcceptsAllCharacters(String value)
      => _sut.Validate(value, _acceptAllMask).Should().BeTrue();

   [Theory]
   [InlineData("540 300 UDF JVW BIH XA0 58")]                  // 549300UDFJVWBIHXA058 with single digit transcription error 9 -> 0
   [InlineData("549 300 UDF JVE BIH XA0 58")]                  // 549300UDFJVWBIHXA058 with single char transcription error W -> E
   [InlineData("10B x93 9c5 453 TqA 114 4M9 991 43X 38")]      // 10Bx939c5543TqA1144M999143X38 with two digit transposition error 54 -> 45
   [InlineData("549 300 NL8 PIP YQD KDA 72")]                  // 549300NL8PIYPQDKDA72 with two char transposition error YP -> PY 
   [InlineData("10B x93 9c5 543 TqA 224 4M9 991 43X 38")]      // 10Bx939c5543TqA1144M999143X38 with two digit twin error 11 -> 22
   [InlineData("ASD F12 345 BBD DFF 098 76166")]               // ASDF12345BBDDEE09876166 with two char twin error EE -> FF
   [InlineData("10B x93 9c5 543 TqA 114 4M9 941 93X 38")]      // SC74MCB10Bx939c5543TqA1144M999143X38L01031234567890123456USD with jump transposition error 914 -> 419
   [InlineData("SC7 4MC 0LB 103 123 456 789 012 345 6USD")]    // SC74MCBL01031234567890123456USD with jump transposition error BL0 -> 0LB
   [InlineData("967 600 AJD 1Q8 K13 MZ8 45")]                  // 967600DJA1Q8K13MZ845 with jump transposition error DJA -> AJD
   [InlineData("549 30L N08 PIY PQD KDA 72")]                  // 549300NL8PIYPQDKDA72 with jump transposition error 0NL -> LN0
   [InlineData("493 00U DFJ VWB IHX A05 85")]                  // 549300UDFJVWBIHXA058 with circular shift error
   [InlineData("854 930 0UD FJV WBI HXA 05")]                  // 549300UDFJVWBIHXA058 with circular shift error
   public void AlphanumericMod97_10Algorithm_ValidateMasked_ShouldReturnFalse_WhenInputContainsDetectableError(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeFalse();

   [Theory]
   [InlineData("U7y 8SX rC! 87")]
   [InlineData("U7y 8SX rC^ 87")]
   [InlineData("U7y 8SX rC= 87")]
   public void AlphanumericMod97_10Algorithm_ValidateMasked_ShouldReturnFalse_WhenValueContainsInvalidCharacter(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeFalse();

   [Theory]
   [InlineData("U7y 8SX rC0 B7")]         // B is 10 positions after 8 in ASCII table
   [InlineData("U7y 8SX rC0 j7")]         // j is 50 positions after 8 in ASCII table
   [InlineData("U7y 8SX rC0 .7")]         // . is 10 positions before 8 in ASCII table
   public void AlphanumericMod97_10Algorithm_ValidateMasked_ShouldReturnFalse_WhenValueContainsInvalidFirstCheckCharacter(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeFalse();

   [Theory]
   [InlineData("U7y 8SX rC0 8A")]         // A is 10 positions after 7 in ASCII table
   [InlineData("U7y 8SX rC0 8i")]         // i is 50 positions after 7 in ASCII table
   [InlineData("U7y 8SX rC0 8-")]         // - is 10 positions before 7 in ASCII table
   public void AlphanumericMod97_10Algorithm_ValidateMasked_ShouldReturnFalse_WhenValueContainsInvalidSecondCheckCharacter(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeFalse();

   [Theory]
   [InlineData("U7y 46")]
   [InlineData("U7y 8SX 89")]
   [InlineData("U7y 8SX rC0 87")]
   [InlineData("U7y 8SX rC0 O3S 38")]
   [InlineData("U7y 8SX rC0 O3S c4I 27")]
   [InlineData("U7y 8SX rC0 O3S c4I HYQ 54")]
   [InlineData("U7y 8SX rC0 O3S c4I HYQ F4M 21")]
   public void AlphanumericMod97_10Algorithm_ValidateMasked_ShouldReturnTrue_ForBenchmarkValues(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeTrue();

   #endregion
}
