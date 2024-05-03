namespace CheckDigits.Net.Tests.Unit.ValueSpecificAlgorithms;

public class VinAlgorithmTests
{
   private readonly VinAlgorithm _sut = new();

   #region AlgorithmDescription Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void VinAlgorithm_AlgorithmDescription_ShouldReturnExpectedValue()
      => _sut.AlgorithmDescription.Should().Be(Resources.VinAlgorithmDescription);

   #endregion

   #region AlgorithmName Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void VinAlgorithm_AlgorithmName_ShouldReturnExpectedValue()
      => _sut.AlgorithmName.Should().Be(Resources.VinAlgorithmName);

   #endregion

   #region TryCalculateCheckDigit Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void VinAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsNull()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(null!, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Fact]
   public void VinAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsEmpty()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(String.Empty, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Fact]
   public void VinAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputHasLengthLessThan17()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit("12345678_0123456", out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Fact]
   public void VinAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputHasLengthGreaterThan17()
   {
      _sut.TryCalculateCheckDigit("12345678_012345678", out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Theory]
   [InlineData("10000000_00000000", '8')]
   [InlineData("01000000_00000000", '7')]
   [InlineData("00100000_00000000", '6')]
   [InlineData("00010000_00000000", '5')]
   [InlineData("00001000_00000000", '4')]
   [InlineData("00000100_00000000", '3')]
   [InlineData("00000010_00000000", '2')]
   [InlineData("00000001_00000000", 'X')]
   [InlineData("00000000_10000000", '9')]
   [InlineData("00000000_01000000", '8')]
   [InlineData("00000000_00100000", '7')]
   [InlineData("00000000_00010000", '6')]
   [InlineData("00000000_00001000", '5')]
   [InlineData("00000000_00000100", '4')]
   [InlineData("00000000_00000010", '3')]
   [InlineData("00000000_00000001", '2')]
   public void VinAlgorithm_TryCalculateCheckDigit_ShouldCorrectlyWeightCharactersByPosition(
      String value,
      Char expectedCheckDigit)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Fact]
   public void VinAlgorithm_TryCalculateCheckDigit_ShouldIgnoreCheckDigitPosition()
   {
      // Arrange.
      var value = "10000000?00000000";
      var expectedCheckDigit = '8';

      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Theory]
   [InlineData("1M8GDM9AXKP042788", 'X')]   // Worked example from Wikipedia (https://en.wikipedia.org/wiki/Vehicle_identification_number#Check-digit_calculation)
   [InlineData("11111111111111111", '1')]   // Test value as per Wikipedia 
   [InlineData("1G8ZG127XWZ157259", 'X')]   // Random VIN from https://vingenerator.org/
   [InlineData("1HGEM21292L047875", '9')]   // "
   public void VinAlgorithm_TryCalculateCheckDigit_ShouldCalculateExpectedCheckDigit(
      String value,
      Char expectedCheckDigit)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Fact]
   public void VinAlgorithm_TryCalculateCheckDigit_ShouldCalculateExpectedCheckDigit_WhenInputIsAllZeros()
   {
      // Arrange.
      var value = "00000000?00000000";
      var expectedCheckDigit = CharConstants.DigitZero;

      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Theory]
   [InlineData("0/000000?00000000")]
   [InlineData("0:000000?00000000")]
   [InlineData("0;000000?00000000")]
   [InlineData("0<000000?00000000")]
   [InlineData("0=000000?00000000")]
   [InlineData("0>000000?00000000")]
   [InlineData("0?000000?00000000")]
   [InlineData("0@000000?00000000")]
   [InlineData("0I000000?00000000")]
   [InlineData("0O000000?00000000")]
   [InlineData("0Q000000?00000000")]
   [InlineData("0[000000?00000000")]
   public void VinAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputContainsInvalidCharacter(String value)
   {
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   #endregion

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void VinAlgorithm_Validate_ShouldReturnFalse_WhenInputIsNull()
      => _sut.Validate(null!).Should().BeFalse();

   [Fact]
   public void VinAlgorithm_Validate_ShouldReturnFalse_WhenInputIsEmpty()
      => _sut.Validate(String.Empty).Should().BeFalse();

   [Fact]
   public void VinAlgorithm_Validate_ShouldReturnFalse_WhenInputHasLengthLessThan17()
      => _sut.Validate("0000000000000000").Should().BeFalse();

   [Fact]
   public void VinAlgorithm_Validate_ShouldReturnFalse_WhenInputHasLengthGreaterThan17()
      => _sut.Validate("000000000000000000").Should().BeFalse();

   [Theory]
   [InlineData("10000000800000000")]
   [InlineData("01000000700000000")]
   [InlineData("00100000600000000")]
   [InlineData("00010000500000000")]
   [InlineData("00001000400000000")]
   [InlineData("00000100300000000")]
   [InlineData("00000010200000000")]
   [InlineData("00000001X00000000")]
   [InlineData("00000000910000000")]
   [InlineData("00000000801000000")]
   [InlineData("00000000700100000")]
   [InlineData("00000000600010000")]
   [InlineData("00000000500001000")]
   [InlineData("00000000400000100")]
   [InlineData("00000000300000010")]
   [InlineData("00000000200000001")]
   public void VinAlgorithm_Validate_ShouldCorrectlyWeightCharactersByPosition(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("1M8GDM9AXKP042788")]   // Worked example from Wikipedia (https://en.wikipedia.org/wiki/Vehicle_identification_number#Check-digit_calculation)
                                       //
   [InlineData("11111111111111111")]   // Test value as per Wikipedia 
   [InlineData("1G8ZG127XWZ157259")]   // Random VIN from https://vingenerator.org/
   [InlineData("1HGEM21292L047875")]   // "
   public void VinAlgorithm_Validate_ShouldReturnTrue_WhenValueContainsValidCheckDigit(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("11111211111111111")]   // Single character transcription error 1 -> 2
   [InlineData("1M8FDM9AXKP042788")]   // Single character transcription error G -> F
   [InlineData("1G8ZG217XWZ157259")]   // Two character transposition 12 -> 21
   [InlineData("1HGME21292L047875")]   // Two character transposition EM -> ME
   [InlineData("1HGEM21W9WL047875")]   // Two character jump transposition 292 -> W9W 
   [InlineData("11100111111111111")]   // Twin error 11 -> 00
   public void VinAlgorithm_Validate_ShouldReturnFalse_WhenInputContainsDetectableError(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Theory]
   [InlineData("0/000000000000000")]
   [InlineData("0:000000000000000")]
   [InlineData("0;000000000000000")]
   [InlineData("0<000000000000000")]
   [InlineData("0=000000000000000")]
   [InlineData("0>000000000000000")]
   [InlineData("0?000000000000000")]
   [InlineData("0@000000000000000")]
   [InlineData("0I000000000000000")]
   [InlineData("0O000000000000000")]
   [InlineData("0Q000000000000000")]
   [InlineData("0[000000000000000")]
   public void VinAlgorithm_Validate_ShouldReturnFalse_WhenInputContainsInvalidCharacter(String value)
      => _sut.Validate(value).Should().BeFalse();

   #endregion
}
