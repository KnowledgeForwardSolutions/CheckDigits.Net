// Ignore Spelling: Verhoeff

namespace CheckDigits.Net.Tests.Unit.GeneralAlgorithms;

public class VerhoeffAlgorithmTests
{
   private readonly VerhoeffAlgorithm _sut = new();
   private readonly ICheckDigitMask _acceptAllMask = new AcceptAllMask();
   private readonly ICheckDigitMask _groupsOfThreeMask = new GroupsOfThreeCheckDigitMask();
   private readonly ICheckDigitMask _rejectAllMask = new RejectAllMask();

   #region AlgorithmDescription Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void VerhoeffAlgorithm_AlgorithmDescription_ShouldReturnExpectedValue()
      => _sut.AlgorithmDescription.Should().Be(Resources.VerhoeffAlgorithmDescription);

   #endregion

   #region AlgorithmName Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void VerhoeffAlgorithm_AlgorithmName_ShouldReturnExpectedValue()
      => _sut.AlgorithmName.Should().Be(Resources.VerhoeffAlgorithmName);

   #endregion

   #region TryCalculateCheckDigit Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void VerhoeffAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsNull()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(null!, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Fact]
   public void VerhoeffAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsEmpty()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(String.Empty, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Theory]
   [InlineData("236", '3')]                      // Worked example from Wikipedia
   [InlineData("0", '4')]
   [InlineData("12345", '1')]                    // Test data from https://rosettacode.org/wiki/Verhoeff_algorithm
   [InlineData("75872", '2')]                    // Test data from https://codereview.stackexchange.com/questions/221229/verhoeff-check-digit-algorithm
   [InlineData("142857", '0')]                   // "
   public void VerhoeffAlgorithm_TryCalculateCheckDigit_ShouldCalculateExpectedCheckDigit(
      String value,
      Char expectedCheckDigit)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   // NOTE: algorithm applies mod 8 to the index when indexing into the permutation table
   [Theory]
   [InlineData("123456789012", '0')]             // Test data from https://rosettacode.org/wiki/Verhoeff_algorithm
   [InlineData("8473643095483728456789", '2')]   // Test data from https://codereview.stackexchange.com/questions/221229/verhoeff-check-digit-algorithm
   [InlineData("11223344556677889900", '9')]     // Value calculated by https://kik.amc.nl/home/rcornet/verhoeff.html
   public void VerhoeffAlgorithm_TryCalculateCheckDigit_ShouldCalculateExpectedCheckDigit_WhenValueHasLengthGreaterThanEight(
      String value,
      Char expectedCheckDigit)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Theory]
   [InlineData("12G45")]
   [InlineData("12)45")]
   public void VerhoeffAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputContainsNonDigitCharacter(String value)
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
   public void VerhoeffAlgorithm_TryCalculateValue_ShouldReturnTrue_ForBenchmarkValues(String value)
      => _sut.TryCalculateCheckDigit(value, out _).Should().BeTrue();

   #endregion

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void VerhoeffAlgorithm_Validate_ShouldReturnFalse_WhenInputIsNull()
      => _sut.Validate(null!).Should().BeFalse();

   [Fact]
   public void VerhoeffAlgorithm_Validate_ShouldReturnFalse_WhenInputIsEmpty()
      => _sut.Validate(String.Empty).Should().BeFalse();

   [Theory]
   [InlineData("0")]    // "0" is the only digit that would pass unless length is checked explicitly
   [InlineData("1")]
   public void VerhoeffAlgorithm_Validate_ShouldReturnFalse_WhenInputIsLengthOne(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Theory]
   [InlineData("2363")]                      // Worked example from Wikipedia
   [InlineData("04")]
   [InlineData("123451")]                    // Test data from https://rosettacode.org/wiki/Verhoeff_algorithm
   [InlineData("758722")]                    // Test data from https://codereview.stackexchange.com/questions/221229/verhoeff-check-digit-algorithm
   [InlineData("1428570")]                   // "
   public void VerhoeffAlgorithm_Validate_ShouldReturnTrue_WhenValueContainsValidCheckDigit(String value)
      => _sut.Validate(value).Should().BeTrue();

   // NOTE: algorithm applies mod 8 to the index when indexing into the permutation table
   [Theory]
   [InlineData("1234567890120")]             // Test data from https://rosettacode.org/wiki/Verhoeff_algorithm
   [InlineData("84736430954837284567892")]   // Test data from https://codereview.stackexchange.com/questions/221229/verhoeff-check-digit-algorithm
   [InlineData("112233445566778899009")]     // Value calculated by https://kik.amc.nl/home/rcornet/verhoeff.html
   public void VerhoeffAlgorithm_Validate_ShouldReturnTrue_WhenValueWithLengthGreaterThanEightContainsValidCheckDigit(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("112233445566778899019")]     // Single digit errors (using "112233445566778899009" as a valid value)
   [InlineData("112233445566778892009")]     // "
   [InlineData("112233445566778399009")]     // "
   [InlineData("112233445566748899009")]     // "
   [InlineData("112233445565778899009")]     // "
   [InlineData("112233445666778899009")]     // "
   [InlineData("112233475566778899009")]     // "
   [InlineData("112238445566778899009")]     // "
   [InlineData("112933445566778899009")]     // "
   [InlineData("102233445566778899009")]     // "
   [InlineData("121233445566778899009")]     // Transposition errors (using "112233445566778899009" as a valid value)
   [InlineData("112323445566778899009")]     // "
   [InlineData("112234345566778899009")]     // "
   [InlineData("112233454566778899009")]     // "
   [InlineData("112233445656778899009")]     // "
   [InlineData("112233445567678899009")]     // "
   [InlineData("112233445566787899009")]     // "
   [InlineData("112233445566778989009")]     // "
   [InlineData("112233445566778890909")]     // "
   [InlineData("84736430459837284567892")]   // Jump transposition error using "84736430954837284567892" as a valid value (954 -> 459)
   [InlineData("112255445566778899009")]     // Twin error using "112233445566778899009" as a valid value (33 -> 55)
   public void VerhoeffAlgorithm_Validate_ShouldReturnFalse_WhenInputContainsDetectableError(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Theory]
   [InlineData("12G455")]
   [InlineData("12)455")]
   public void VerhoeffAlgorithm_Validate_ShouldReturnFalse_WhenInputContainsNonDigitCharacter(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Theory]
   [InlineData("1401")]
   [InlineData("1406625")]
   [InlineData("1406625388")]
   [InlineData("1406625380426")]
   [InlineData("1406625380425512")]
   [InlineData("1406625380425510285")]
   [InlineData("1406625380425510282655")]
   public void VerhoeffAlgorithm_Validate_ShouldReturnTrue_ForBenchmarkValues(String value)
      => _sut.Validate(value).Should().BeTrue();

   #endregion

   #region Validate (ICheckDigitMask Overload) Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void VerhoeffAlgorithm_ValidateMasked_ShouldThrowArgumentNullException_WhenMaskIsNull()
      => _sut
      .Invoking(x => x.Validate("12345", null!))
      .Should()
      .ThrowExactly<ArgumentNullException>()
      .WithParameterName("mask")
      .WithMessage(Resources.NullMaskMessage + "*");

   [Fact]
   public void VerhoeffAlgorithm_ValidateMasked_ShouldReturnFalse_WhenInputIsNull()
      => _sut.Validate(null!, _acceptAllMask).Should().BeFalse();

   [Fact]
   public void VerhoeffAlgorithm_ValidateMasked_ShouldReturnFalse_WhenInputIsEmpty()
      => _sut.Validate(String.Empty, _acceptAllMask).Should().BeFalse();

   [Theory]
   [InlineData("0")]    // "0" is the only digit that would pass unless length is checked explicitly
   [InlineData("1")]
   public void VerhoeffAlgorithm_ValidateMasked_ShouldReturnFalse_WhenInsufficientUnmaskedCharactersToCalculateCheckDigit(String value)
      => _sut.Validate(value, _acceptAllMask).Should().BeFalse();

   [Fact]
   public void VerhoeffAlgorithm_ValidateMasked_ShouldReturnFalse_WhenAllNonCheckDigitCharactersAreMaskedOut()
      => _sut.Validate("000 000 0000 000 000", _rejectAllMask).Should().BeFalse();

   [Theory]
   [InlineData("236 3")]                     // Worked example from Wikipedia
   [InlineData("04")]
   [InlineData("123 451")]                   // Test data from https://rosettacode.org/wiki/Verhoeff_algorithm
   [InlineData("758 722")]                   // Test data from https://codereview.stackexchange.com/questions/221229/verhoeff-check-digit-algorithm
   [InlineData("142 857 0")]                 // "
   public void VerhoeffAlgorithm_ValidateMasked_ShouldReturnTrue_WhenValueContainsValidCheckDigit(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeTrue();

   [Theory]
   [InlineData("2363")]                      // Worked example from Wikipedia
   [InlineData("04")]
   [InlineData("123451")]                    // Test data from https://rosettacode.org/wiki/Verhoeff_algorithm
   [InlineData("758722")]                    // Test data from https://codereview.stackexchange.com/questions/221229/verhoeff-check-digit-algorithm
   [InlineData("1428570")]                   // "
   public void VerhoeffAlgorithm_ValidateMasked_ShouldReturnTrue_WhenValueContainsValidCheckDigitAndMaskAcceptsAllCharacters(String value)
      => _sut.Validate(value, _acceptAllMask).Should().BeTrue();

   // NOTE: algorithm applies mod 8 to the index when indexing into the permutation table
   [Theory]
   [InlineData("123 456 789 012 0")]               // Test data from https://rosettacode.org/wiki/Verhoeff_algorithm
   [InlineData("847 364 309 548 372 845 678 92")]  // Test data from https://codereview.stackexchange.com/questions/221229/verhoeff-check-digit-algorithm
   [InlineData("112 233 445 566 778 899 009")]     // Value calculated by https://kik.amc.nl/home/rcornet/verhoeff.html
   public void VerhoeffAlgorithm_ValidateMasked_ShouldReturnTrue_WhenValueWithLengthGreaterThanEightContainsValidCheckDigit(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeTrue();

   [Theory]
   [InlineData("112 233 445 566 778 899 019")]     // Single digit errors (using "112233445566778899009" as a valid value)
   [InlineData("112 233 445 566 778 892 009")]     // "
   [InlineData("112 233 445 566 778 399 009")]     // "
   [InlineData("112 233 445 566 748 899 009")]     // "
   [InlineData("112 233 445 565 778 899 009")]     // "
   [InlineData("112 233 445 666 778 899 009")]     // "
   [InlineData("112 233 475 566 778 899 009")]     // "
   [InlineData("112 238 445 566 778 899 009")]     // "
   [InlineData("112 933 445 566 778 899 009")]     // "
   [InlineData("102 233 445 566 778 899 009")]     // "
   [InlineData("121 233 445 566 778 899 009")]     // Transposition errors (using "112233445566778899009" as a valid value)
   [InlineData("112 323 445 566 778 899 009")]     // "
   [InlineData("112 234 345 566 778 899 009")]     // "
   [InlineData("112 233 454 566 778 899 009")]     // "
   [InlineData("112 233 445 656 778 899 009")]     // "
   [InlineData("112 233 445 567 678 899 009")]     // "
   [InlineData("112 233 445 566 787 899 009")]     // "
   [InlineData("112 233 445 566 778 989 009")]     // "
   [InlineData("112 233 445 566 778 890 909")]     // "
   [InlineData("847 364 304 598 372 845 678 92")]  // Jump transposition error using "84736430954837284567892" as a valid value (954 -> 459)
   [InlineData("112 255 445 566 778 899 009")]     // Twin error using "112233445566778899009" as a valid value (33 -> 55)
   public void VerhoeffAlgorithm_ValidateMasked_ShouldReturnFalse_WhenInputContainsDetectableError(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeFalse();

   [Theory]
   [InlineData("12G 455")]
   [InlineData("12) 455")]
   public void VerhoeffAlgorithm_ValidateMasked_ShouldReturnFalse_WhenInputContainsNonDigitCharacter(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeFalse();

   [Fact]
   public void VerhoeffAlgorithm_ValidateMasked_ShouldReturnFalse_WhenCheckDigitCharacterIsNonDigit()
      => _sut.Validate("123 45A", _groupsOfThreeMask).Should().BeFalse();

   [Theory]
   [InlineData("140 1")]
   [InlineData("140 662 5")]
   [InlineData("140 662 538 8")]
   [InlineData("140 662 538 042 6")]
   [InlineData("140 662 538 042 551 2")]
   [InlineData("140 662 538 042 551 028 5")]
   [InlineData("140 662 538 042 551 028 265 5")]
   public void VerhoeffAlgorithm_ValidateMasked_ShouldReturnTrue_ForBenchmarkValues(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeTrue();

   #endregion
}
