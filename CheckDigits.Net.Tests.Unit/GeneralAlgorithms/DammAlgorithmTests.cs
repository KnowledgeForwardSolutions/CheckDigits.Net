// Ignore Spelling: Damm

namespace CheckDigits.Net.Tests.Unit.GeneralAlgorithms;

public class DammAlgorithmTests
{
   private readonly DammAlgorithm _sut = new();
   private readonly ICheckDigitMask _acceptAllMask = new AcceptAllMask();
   private readonly ICheckDigitMask _groupsOfThreeMask = new GroupsOfThreeCheckDigitMask();
   private readonly ICheckDigitMask _rejectAllMask = new RejectAllMask();

   #region AlgorithmDescription Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void DammAlgorithm_AlgorithmDescription_ShouldReturnExpectedValue()
      => _sut.AlgorithmDescription.Should().Be(Resources.DammAlgorithmDescription);

   #endregion

   #region AlgorithmName Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void DammAlgorithm_AlgorithmName_ShouldReturnExpectedValue()
      => _sut.AlgorithmName.Should().Be(Resources.DammAlgorithmName);

   #endregion

   #region TryCalculateCheckDigit Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void DammAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsNull()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(null!, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Fact]
   public void DammAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsEmpty()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(String.Empty, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Theory]
   [InlineData("572", '4')]                      // Worked example from Wikipedia
   [InlineData("11294", '6')]                    // Test data from https://www.rosettacode.org/wiki/Damm_algorithm#C#
   [InlineData("12345678901", '8')]              // Value calculated by https://jackanderson.me/2020/09/damm-algorithm-check-digit-tool/
   [InlineData("123456789012345", '0')]          // "
   [InlineData("11223344556677889900", '6')]     // "
   public void DammAlgorithm_TryCalculateCheckDigit_ShouldCalculateExpectedCheckDigit(
      String value,
      Char expectedCheckDigit)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Fact]
   public void DamnAlgorithm_TryCalculateCheckDigit_ShouldCalculateExpectedCheckDigit_WhenInputIsAllZeros()
   {
      // Arrange.
      var value = "00000";
      var expectedCheckDigit = Chars.DigitZero;

      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Theory]
   [InlineData("12G45")]
   [InlineData("12)45")]
   public void DammAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputContainsNonDigitCharacter(String value)
   {
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Theory]
   [InlineData("140")]
   [InlineData("140662")]
   [InlineData("140662538")]
   [InlineData("140662538042")]
   [InlineData("140662538042551")]
   [InlineData("140662538042551028")]
   [InlineData("140662538042551028265")]
   public void DammAlgorithm_TryCalculateValue_ShouldReturnTrue_ForBenchmarkValues(String value)
      => _sut.TryCalculateCheckDigit(value, out _).Should().BeTrue();

   #endregion

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void DammAlgorithm_Validate_ShouldReturnFalse_WhenInputIsNull()
      => _sut.Validate(null!).Should().BeFalse();

   [Fact]
   public void DammAlgorithm_Validate_ShouldReturnFalse_WhenInputIsEmpty()
      => _sut.Validate(String.Empty).Should().BeFalse();

   [Theory]
   [InlineData("0")]    // "0" is the only digit that would pass unless length is checked explicitly
   [InlineData("1")]
   public void DammAlgorithm_Validate_ShouldReturnFalse_WhenInputIsLengthOne(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Theory]
   [InlineData("5724")]                      // Worked example from Wikipedia
   [InlineData("112946")]                    // Test data from https://www.rosettacode.org/wiki/Damm_algorithm#C#
   [InlineData("123456789018")]              // Value calculated by https://jackanderson.me/2020/09/damm-algorithm-check-digit-tool/
   [InlineData("1234567890123450")]          // "
   [InlineData("112233445566778899006")]     // "
   public void DammAlgorithm_Validate_ShouldReturnTrue_WhenInputContainsValidCheckDigit(String value)
         => _sut.Validate(value).Should().BeTrue();

   [Fact]
   public void DammAlgorithm_Validate_ShouldReturnTrue_WhenInputIsAllZeros()
      => _sut.Validate("0000000000000000").Should().BeTrue();

   [Theory]
   [InlineData("112233445566778899016")]     // Single digit errors (using "112233445566778899006" as a valid value)
   [InlineData("112233445566778892006")]     // "
   [InlineData("112233445566778399006")]     // "
   [InlineData("112233445566748899006")]     // "
   [InlineData("112233445565778899006")]     // "
   [InlineData("112233445666778899006")]     // "
   [InlineData("112233475566778899006")]     // "
   [InlineData("112238445566778899006")]     // "
   [InlineData("112933445566778899006")]     // "
   [InlineData("102233445566778899006")]     // "
   [InlineData("121233445566778899006")]     // Transposition errors (using "112233445566778899006" as a valid value)
   [InlineData("112323445566778899006")]     // "
   [InlineData("112234345566778899006")]     // "
   [InlineData("112233454566778899006")]     // "
   [InlineData("112233445656778899006")]     // "
   [InlineData("112233445567678899006")]     // "
   [InlineData("112233445566787899006")]     // "
   [InlineData("112233445566778989006")]     // "
   [InlineData("112233445566778890906")]     // "
   [InlineData("1236547890123450")]          // Jump transposition error using "1234567890123450" as a valid value (456 -> 654)
   [InlineData("112255445566778899006")]     // Twin error using "112233445566778899006" as a valid value (33 -> 55)
   public void DammAlgorithm_Validate_ShouldReturnFalse_WhenInputContainsDetectableError(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Theory]
   [InlineData("12G455")]
   [InlineData("12)455")]
   public void DammAlgorithm_Validate_ShouldReturnFalse_WhenInputContainsNonDigitCharacter(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Theory]
   [InlineData("1402")]
   [InlineData("1406622")]
   [InlineData("1406625388")]
   [InlineData("1406625380422")]
   [InlineData("1406625380425518")]
   [InlineData("1406625380425510280")]
   [InlineData("1406625380425510282654")]
   public void DammAlgorithm_Validate_ShouldReturnTrue_ForBenchmarkValues(String value)
      => _sut.Validate(value).Should().BeTrue();

   #endregion

   #region Validate (ICheckDigitMask Overload) Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void DammAlgorithm_ValidateMasked_ShouldReturnFalse_WhenInputIsNull()
      => _sut.Validate(null!, _acceptAllMask).Should().BeFalse();

   [Fact]
   public void DammAlgorithm_ValidateMasked_ShouldReturnFalse_WhenInputIsEmpty()
      => _sut.Validate(String.Empty, _acceptAllMask).Should().BeFalse();

   [Theory]
   [InlineData("0")]    // "0" is the only digit that would pass unless length is checked explicitly
   [InlineData("1")]
   public void DammAlgorithm_ValidateMasked_ShouldReturnFalse_WhenInsufficientUnmaskedCharactersToCalculateCheckDigit(String value)
      => _sut.Validate(value, _acceptAllMask).Should().BeFalse();

   [Fact]
   public void DammAlgorithm_ValidateMasked_ShouldReturnFalse_WhenAllNonCheckDigitCharactersAreMaskedOut()
      => _sut.Validate("0000 0000 0000 0000", _rejectAllMask).Should().BeFalse();

   [Theory]
   [InlineData("572 4")]                          // Worked example from Wikipedia
   [InlineData("112 946")]                        // Test data from https://www.rosettacode.org/wiki/Damm_algorithm#C#
   [InlineData("123 456 789 018")]                // Value calculated by https://jackanderson.me/2020/09/damm-algorithm-check-digit-tool/
   [InlineData("123 456 789 012 345 0")]          // "
   [InlineData("112 233 445 566 778 899 006")]    // "
   public void DammAlgorithm_ValidateMasked_ShouldReturnTrue_WhenInputContainsValidCheckDigit(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeTrue();

   [Fact]
   public void DammAlgorithm_ValidateMasked_ShouldReturnTrue_WhenInputIsAllZeros()
      => _sut.Validate("000 000 000 000 0000", _groupsOfThreeMask).Should().BeTrue();

   [Theory]
   [InlineData("112 233 445 566 778 899 016")]    // Single digit errors (using "112233445566778899006" as a valid value)
   [InlineData("112 233 445 566 778 892 006")]    // "
   [InlineData("112 233 445 566 778 399 006")]    // "
   [InlineData("112 233 445 566 748 899 006")]    // "
   [InlineData("112 233 445 565 778 899 006")]    // "
   [InlineData("112 233 445 666 778 899 006")]    // "
   [InlineData("112 233 475 566 778 899 006")]    // "
   [InlineData("112 238 445 566 778 899 006")]    // "
   [InlineData("112 933 445 566 778 899 006")]    // "
   [InlineData("102 233 445 566 778 899 006")]    // "
   [InlineData("121 233 445 566 778 899 006")]    // Transposition errors (using "112233445566778899006" as a valid value)
   [InlineData("112 323 445 566 778 899 006")]    // "
   [InlineData("112 234 345 566 778 899 006")]    // "
   [InlineData("112 233 454 566 778 899 006")]    // "
   [InlineData("112 233 445 656 778 899 006")]    // "
   [InlineData("112 233 445 567 678 899 006")]    // "
   [InlineData("112 233 445 566 787 899 006")]    // "
   [InlineData("112 233 445 566 778 989 006")]    // "
   [InlineData("112 233 445 566 778 890 906")]    // "
   [InlineData("123 654 789 012 345 0")]          // Jump transposition error using "1234567890123450" as a valid value (456 -> 654)
   [InlineData("112 255 445 566 778 899 006")]    // Twin error using "112233445566778899006" as a valid value (33 -> 55)
   public void DammAlgorithm_ValidateMasked_ShouldReturnFalse_WhenInputContainsDetectableError(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeFalse();

   [Theory]
   [InlineData("12G 455")]
   [InlineData("12) 455")]
   public void DammAlgorithm_ValidateMasked_ShouldReturnFalse_WhenInputContainsNonDigitCharacter(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeFalse();

   [Theory]
   [InlineData("140 2")]
   [InlineData("140 662 2")]
   [InlineData("140 662 538 8")]
   [InlineData("140 662 538 042 2")]
   [InlineData("140 662 538 042 551 8")]
   [InlineData("140 662 538 042 551 028 0")]
   [InlineData("140 662 538 042 551 028 265 4")]
   public void DammAlgorithm_ValidateMasked_ShouldReturnTrue_ForBenchmarkValues(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeTrue();

   #endregion
}
