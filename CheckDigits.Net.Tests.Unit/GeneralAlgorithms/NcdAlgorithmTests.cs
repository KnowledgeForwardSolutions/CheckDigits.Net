// Ignore Spelling: Ncd

namespace CheckDigits.Net.Tests.Unit.GeneralAlgorithms;

public class NcdAlgorithmTests
{
   private readonly NcdAlgorithm _sut = new();

   #region AlgorithmDescription Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void NcdAlgorithm_AlgorithmDescription_ShouldReturnExpectedValue()
      => _sut.AlgorithmDescription.Should().Be(Resources.NcdAlgorithmDescription);

   #endregion

   #region AlgorithmName Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void NcdAlgorithm_AlgorithmName_ShouldReturnExpectedValue()
      => _sut.AlgorithmName.Should().Be(Resources.NcdAlgorithmName);

   #endregion

   #region MapCharacter Tests
   // ==========================================================================
   // ==========================================================================

   [Theory]
   [InlineData('\0', 0)]
   [InlineData('/', 0)]
   [InlineData('0', 0)]
   [InlineData('1', 1)]
   [InlineData('2', 2)]
   [InlineData('3', 3)]
   [InlineData('4', 4)]
   [InlineData('5', 5)]
   [InlineData('6', 6)]
   [InlineData('7', 7)]
   [InlineData('8', 8)]
   [InlineData('9', 9)]
   [InlineData(':', 0)]
   [InlineData(';', 0)]
   [InlineData('<', 0)]
   [InlineData('=', 0)]
   [InlineData('>', 0)]
   [InlineData('?', 0)]
   [InlineData('@', 0)]
   [InlineData('A', 0)]
   [InlineData('B', 0)]
   [InlineData('C', 0)]
   [InlineData('D', 0)]
   [InlineData('E', 0)]
   [InlineData('F', 0)]
   [InlineData('G', 0)]
   [InlineData('H', 0)]
   [InlineData('I', 0)]
   [InlineData('J', 0)]
   [InlineData('K', 0)]
   [InlineData('L', 0)]
   [InlineData('M', 0)]
   [InlineData('N', 0)]
   [InlineData('O', 0)]
   [InlineData('P', 0)]
   [InlineData('Q', 0)]
   [InlineData('R', 0)]
   [InlineData('S', 0)]
   [InlineData('T', 0)]
   [InlineData('U', 0)]
   [InlineData('V', 0)]
   [InlineData('W', 0)]
   [InlineData('X', 0)]
   [InlineData('Y', 0)]
   [InlineData('Z', 0)]
   [InlineData('[', 0)]
   [InlineData('\\', 0)]
   [InlineData(']', 0)]
   [InlineData('^', 0)]
   [InlineData('_', 0)]
   [InlineData('`', 0)]
   [InlineData('a', 0)]
   [InlineData('b', 10)]
   [InlineData('c', 11)]
   [InlineData('d', 12)]
   [InlineData('e', 0)]
   [InlineData('f', 13)]
   [InlineData('g', 14)]
   [InlineData('h', 15)]
   [InlineData('i', 0)]
   [InlineData('j', 16)]
   [InlineData('k', 17)]
   [InlineData('l', 0)]
   [InlineData('m', 18)]
   [InlineData('n', 19)]
   [InlineData('o', 0)]
   [InlineData('p', 20)]
   [InlineData('q', 21)]
   [InlineData('r', 22)]
   [InlineData('s', 23)]
   [InlineData('t', 24)]
   [InlineData('u', 0)]
   [InlineData('v', 25)]
   [InlineData('w', 26)]
   [InlineData('x', 27)]
   [InlineData('y', 0)]
   [InlineData('z', 28)]
   [InlineData('{', 0)]
   public void NcdAlgorithm_MapCharacter_ShouldReturnExpectedValue(
      Char ch,
      Int32 expected)
      => NcdAlgorithm.MapCharacter(ch).Should().Be(expected);

   #endregion

   #region TryCalculateCheckDigit Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void NcdAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsNull()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(null!, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Fact]
   public void NcdAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsEmpty()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(String.Empty, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Theory]
   [InlineData("0", '0')]
   [InlineData("1", '1')]
   [InlineData("2", '2')]
   [InlineData("3", '3')]
   [InlineData("4", '4')]
   [InlineData("5", '5')]
   [InlineData("6", '6')]
   [InlineData("7", '7')]
   [InlineData("8", '8')]
   [InlineData("9", '9')]
   [InlineData("b", 'b')]
   [InlineData("c", 'c')]
   [InlineData("d", 'd')]
   [InlineData("f", 'f')]
   [InlineData("g", 'g')]
   [InlineData("h", 'h')]
   [InlineData("j", 'j')]
   [InlineData("k", 'k')]
   [InlineData("m", 'm')]
   [InlineData("n", 'n')]
   [InlineData("p", 'p')]
   [InlineData("q", 'q')]
   [InlineData("r", 'r')]
   [InlineData("s", 's')]
   [InlineData("t", 't')]
   [InlineData("v", 'v')]
   [InlineData("w", 'w')]
   [InlineData("x", 'x')]
   [InlineData("z", 'z')]
   public void NcdAlgorithm_TryCalculateCheckDigit_ShouldCorrectlyMapCharacterValues(
      String value,
      Char expectedCheckDigit)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Theory]
   [InlineData("/", '0')]
   [InlineData(":", '0')]
   [InlineData("A", '0')]      // Uppercase is not valid
   [InlineData(@"\", '0')]
   [InlineData("{", '0')]
   public void NcdAlgorithm_TryCalculateCheckDigit_ShouldIgnoreNonCharacterValues(
      String value,
      Char expectedCheckDigit)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Theory]
   [InlineData("1", '1')]
   [InlineData("01", '2')]
   [InlineData("001", '3')]
   [InlineData("0001", '4')]
   [InlineData("00001", '5')]
   [InlineData("000001", '6')]
   [InlineData("0000001", '7')]
   [InlineData("00000001", '8')]
   [InlineData("000000001", '9')]
   [InlineData("0000000001", 'b')]
   [InlineData("00000000001", 'c')]
   [InlineData("000000000001", 'd')]
   [InlineData("0000000000001", 'f')]
   public void NcdAlgorithm_TryCalculateCheckDigit_ShouldProperlyWeightCharactersByPosition(
      String value,
      Char expectedCheckDigit)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Theory]
   [InlineData("13030/xf93gt2", 'q')]     // Worked example from https://metacpan.org/dist/Noid/view/noid#NOID-CHECK-DIGIT-ALGORITHM
   [InlineData("13030/tf5p30086", 'k')]   // Example from https://n2t.net/e/noid.html
   [InlineData("99999/fk4ck01k7", '7')]   // Demo value generated by https://ezid.cdlib.org/
   [InlineData("99999/fk44479c3", 'd')]   // "
   public void NcdAlgorithm_TryCalculateCheckDigit_ShouldCalculateExpectedCheckDigit(
      String value,
      Char expectedCheckDigit)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   #endregion

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void NcdAlgorithm_Validate_ShouldReturnFalse_WhenInputIsNull()
      => _sut.Validate(null!).Should().BeFalse();

   [Fact]
   public void NcdAlgorithm_Validate_ShouldReturnFalse_WhenInputIsEmpty()
      => _sut.Validate(String.Empty).Should().BeFalse();

   [Theory]
   [InlineData("0")]
   [InlineData("1")]
   public void NcdAlgorithm_Validate_ShouldReturnFalse_WhenInputIsOneCharacterInLength(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Theory]
   [InlineData("00")]
   [InlineData("11")]
   [InlineData("22")]
   [InlineData("33")]
   [InlineData("44")]
   [InlineData("55")]
   [InlineData("66")]
   [InlineData("77")]
   [InlineData("88")]
   [InlineData("99")]
   [InlineData("bb")]
   [InlineData("cc")]
   [InlineData("dd")]
   [InlineData("ff")]
   [InlineData("gg")]
   [InlineData("hh")]
   [InlineData("jj")]
   [InlineData("kk")]
   [InlineData("mm")]
   [InlineData("nn")]
   [InlineData("pp")]
   [InlineData("qq")]
   [InlineData("rr")]
   [InlineData("ss")]
   [InlineData("tt")]
   [InlineData("vv")]
   [InlineData("ww")]
   [InlineData("xx")]
   [InlineData("zz")]
   public void NcdAlgorithm_Validate_ShouldCorrectlyMapCharacterValues(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("/0")]
   [InlineData(":0")]
   [InlineData("A0")]      // Uppercase is not valid
   [InlineData(@"\0")]
   [InlineData("{0")]
   public void NcdAlgorithm_Validate_ShouldIgnoreNonCharacterValues(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("11")]
   [InlineData("012")]
   [InlineData("0013")]
   [InlineData("00014")]
   [InlineData("000015")]
   [InlineData("0000016")]
   [InlineData("00000017")]
   [InlineData("000000018")]
   [InlineData("0000000019")]
   [InlineData("0000000001b")]
   [InlineData("00000000001c")]
   [InlineData("000000000001d")]
   [InlineData("0000000000001f")]
   public void NcdAlgorithm_Validate_ShouldProperlyWeightCharactersByPosition(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("13030/xf93gt2q")]      // Worked example from https://metacpan.org/dist/Noid/view/noid#NOID-CHECK-DIGIT-ALGORITHM
   [InlineData("13030/tf5p30086k")]    // Example from https://n2t.net/e/noid.html
   [InlineData("99999/fk4ck01k77")]    // Demo value generated by https://ezid.cdlib.org/
   [InlineData("99999/fk44479c3d")]    // "
   public void NcdAlgorithm_Validate_ShouldReturnTrue_WhenValueContainsValidCheckDigit(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("13030/xf83gt2q")]      // 13030/xf93gt2q with single digit transcription error 9 -> 8
   [InlineData("13030/xd93gt2q")]      // 13030/xf93gt2q with single char transcription error f -> d
   [InlineData("13030/tf5p30806k")]    // 13030/tf5p30086k with two digit transposition error 08 -> 80 
   [InlineData("13030/ft5p30086k")]    // 13030/tf5p30086k with two char transposition error tf -> ft
   [InlineData("13030/tf53p0086k")]    // 13030/tf5p30086k with two char transposition error p3 -> 3p
   public void NcdAlgorithm_Validate_ShouldReturnFalse_WhenInputContainsDetectableError(String value)
      => _sut.Validate(value).Should().BeFalse();

   #endregion

   //[Theory]
   //[InlineData("13030/xf93gt2", 'q')]     // Worked example from https://metacpan.org/dist/Noid/view/noid#NOID-CHECK-DIGIT-ALGORITHM
   //[InlineData("13030/tf5p30086", 'k')]   // Example from https://n2t.net/e/noid.html
   //[InlineData("99999/fk4ck01k7", '7')]   // Demo value generated by https://ezid.cdlib.org/
   //[InlineData("99999/fk44479c3", 'd')]   // "
   //public void NcdAlgorithm_TryCalculateCheckDigit2_ShouldCalculateExpectedCheckDigit(
   //   String value,
   //   Char expectedCheckDigit)
   //{
   //   Act / assert.
   //  _sut.TryCalculateCheckDigit2(value, out var checkDigit).Should().BeTrue();
   //   checkDigit.Should().Be(expectedCheckDigit);
   //}

   //[Theory]
   //[InlineData("13030/xf93gt2q")]      // Worked example from https://metacpan.org/dist/Noid/view/noid#NOID-CHECK-DIGIT-ALGORITHM
   //[InlineData("13030/tf5p30086k")]    // Example from https://n2t.net/e/noid.html
   //[InlineData("99999/fk4ck01k77")]    // Demo value generated by https://ezid.cdlib.org/
   //[InlineData("99999/fk44479c3d")]    // "
   //public void NcdAlgorithm_Validate2_ShouldReturnTrue_WhenValueContainsValidCheckDigit(String value)
   //   => _sut.Validate2(value).Should().BeTrue();

   //[Theory]
   //[InlineData("13030/xf83gt2q")]      // 13030/xf93gt2q with single digit transcription error 9 -> 8
   //[InlineData("13030/xd93gt2q")]      // 13030/xf93gt2q with single char transcription error f -> d
   //[InlineData("13030/tf5p30806k")]    // 13030/tf5p30086k with two digit transposition error 08 -> 80 
   //[InlineData("13030/ft5p30086k")]    // 13030/tf5p30086k with two char transposition error tf -> ft
   //[InlineData("13030/tf53p0086k")]    // 13030/tf5p30086k with two char transposition error p3 -> 3p
   //public void NcdAlgorithm_Validate2_ShouldReturnFalse_WhenInputContainsDetectableError(String value)
   //   => _sut.Validate2(value).Should().BeFalse();

}
