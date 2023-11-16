﻿// Ignore Spelling: Isan

namespace CheckDigits.Net.Tests.Unit.ValueSpecificAlgorithms;

public class IsanAlgorithmTests
{
   private readonly IsanAlgorithm _sut = new();
   private readonly ITestOutputHelper _outputHelper;

   public IsanAlgorithmTests(ITestOutputHelper outputHelper) => _outputHelper = outputHelper;


   #region AlgorithmDescription Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void IsanAlgorithm_AlgorithmDescription_ShouldReturnExpectedValue()
      => _sut.AlgorithmDescription.Should().Be(Resources.IsanAlgorithmDescription);

   #endregion

   #region AlgorithmName Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void IsanAlgorithm_AlgorithmName_ShouldReturnExpectedValue()
      => _sut.AlgorithmName.Should().Be(Resources.IsanAlgorithmName);

   #endregion

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void IsanAlgorithm_Validate_ShouldReturnFalse_WhenInputIsNull()
      => _sut.Validate(null!).Should().BeFalse();

   [Fact]
   public void IsanAlgorithm_Validate_ShouldReturnFalse_WhenInputIsEmpty()
      => _sut.Validate(String.Empty).Should().BeFalse();

   [Theory]
   [InlineData("0000ABCD1234FEDC700000008")]          // Valid MOD 37,36 value but incorrect length for unformatted ISAN
   [InlineData("0000ABCD1234FEDC7000000000W")]        // "
   public void IsanAlgorithm_Validate_ShouldReturnFalse_WhenInputIsNot17CharactersInLength(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Theory]
   [InlineData("0000000000000000900000000A")]
   [InlineData("00000000000000009000000018")]
   [InlineData("00000000000000009000000026")]
   [InlineData("00000000000000009000000034")]
   [InlineData("00000000000000009000000042")]
   [InlineData("0000000000000000900000005Z")]
   [InlineData("0000000000000000900000006X")]
   [InlineData("0000000000000000900000007V")]
   [InlineData("0000000000000000900000008T")]
   [InlineData("0000000000000000900000009R")]
   [InlineData("000000000000000090000000AP")]
   [InlineData("000000000000000090000000BN")]
   [InlineData("000000000000000090000000CL")]
   [InlineData("000000000000000090000000DJ")]
   [InlineData("000000000000000090000000EH")]
   [InlineData("000000000000000090000000FF")]
   public void IsanAlgorithm_Validate_ShouldCorrectlyMapCharacterValues(String value)
   => _sut.Validate(value).Should().BeTrue();


   [Theory]
   [InlineData("00000000C36D002BK00000000E")]         // Full ISAN for Star Trek episode "Amok Time"
   [InlineData("00000000C36D004BC000000001")]         // Full ISAN for Star Trek episode "The Trouble With Tribbles"
   [InlineData("00000000D0A90011C000000001")]         // Full ISAN for Star Trek Next Gen episode "Yesterday's Enterprise"
   [InlineData("00000000D2D70009L00000000B")]         // Full ISAN for Star Trek DS9 episode "Trials And Tribble-ations"
   public void IsanAlgorithm_Validate_ShouldReturnTrue_WhenInputContainsValidCheckDigits(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("00000000C37D002BK00000000E")]         // 00000000C36D002BK00000000E with single digit transcription error 6 -> 7
   [InlineData("00000000B36D002BK00000000E")]         // 00000000C36D002BK00000000E with single char transcription error C -> B
   [InlineData("0000000042D70009L00000000B")]         // 00000000D2D70009L00000000B with single char transcription error D -> 4
   [InlineData("00000000C63D004BC000000001")]         // 00000000C36D004BC000000001 with two digit transposition error 36 -> 63
   [InlineData("0000ACBD1234FEDC700000000G")]         // 0000ABCD1234FEDC700000000G with two char transposition error BC -> CB
   [InlineData("0000ABC1D234FEDC700000000G")]         // 0000ABCD1234FEDC700000000G with two char transposition error D! -> 1D
   [InlineData("1155AABB3344CCDD500000000M")]         // 1122AABB3344CCDD500000000M with two digit twin error 22 -> 55
   [InlineData("1122AAEE3344CCDD500000000M")]         // 1122AABB3344CCDD500000000M with two char twin error BB -> EE
   [InlineData("2112AABB3344CCDD500000000M")]         // 1122AABB3344CCDD500000000M with jump transposition error 112 -> 211
   [InlineData("1122BAAB3344CCDD500000000M")]         // 1122AABB3344CCDD500000000M with jump transposition error AAB -> BAA
   public void IsanAlgorithm_Validate_ShouldReturnFalse_WhenInputContainsDetectableFirstCheckDigitError(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Theory]
   [InlineData("0000ABCD1234FEDC71634ABCD9")]         // 0000ABCD1234FEDC71234ABCD9 with single digit transcription error 2 -> 6
   [InlineData("0000ABCD1234FEDC71234EBCD9")]         // 0000ABCD1234FEDC71234ABCD9 with single char transcription error A -> E
   [InlineData("0000ABCD1234FEDC71234A9CD9")]         // 0000ABCD1234FEDC71234ABCD9 with single char transcription error B -> 9
   [InlineData("0000ABCD1234FEDC71243ABCD9")]         // 0000ABCD1234FEDC71234ABCD9 with two digit transposition error 34 -> 43
   [InlineData("0000ABCD1234FEDC71234ACBD9")]         // 0000ABCD1234FEDC71234ABCD9 with two char transposition error BC -> CB
   [InlineData("0000ABCD1234FEDC71155AABB6")]         // 0000ABCD1234FEDC71122AABB6 with two digit twin error 22 -> 55
   [InlineData("0000ABCD1234FEDC71122AAEE6")]         // 0000ABCD1234FEDC71122AABB6 with two char twin error BB -> EE
   [InlineData("0000ABCD1234FEDC72112AABB6")]         // 0000ABCD1234FEDC71122AABB6 with jump transposition error 112 -> 211
   [InlineData("0000ABCD1234FEDC71122BAAB6")]         // 0000ABCD1234FEDC71122AABB6 with jump transposition error AAB -> BAA
   public void IsanAlgorithm_Validate_ShouldReturnFalse_WhenInputContainsDetectableSecondCheckDigitError(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Theory]
   [InlineData("123456789ABCDEFGN000000005")]         // Valid MOD 37,36 value but not ISAN - G is out of range
   [InlineData("11112222333344447ABCDEFGHB")]         // Valid MOD 37,36 value but not ISAN - G,H are out of range
   [InlineData("123456789ABCDEF+N000000005")]
   [InlineData("123456789ABCDEF:N000000005")]
   [InlineData("123456789ABCDEF^N000000005")]
   [InlineData("11112222333344447ABCDEFA+B")]
   [InlineData("11112222333344447ABCDEFA:B")]
   [InlineData("11112222333344447ABCDEFA^B")]
   public void IsanAlgorithm_Validate_ShouldReturnFalse_WhenInputContainsInvalidCharacter(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Theory]
   [InlineData("00000000C36D002B+00000000E")]
   [InlineData("00000000C36D002B:00000000E")]
   [InlineData("00000000C36D002B^00000000E")]
   [InlineData("00000000C36D002BK00000000+")]
   [InlineData("00000000C36D002BK00000000:")]
   [InlineData("00000000C36D002BK00000000^")]
   public void IsanAlgorithm_Validate_ShouldReturnFalse_WhenInputContainsInvalidCheckCharacter(String value)
      => _sut.Validate(value).Should().BeFalse();

   //[Fact]
   //public void GenerateTestData()
   //{
   //   //r value = "0000ABCD1234FEDC";
   //   var value = "1111222233334444ABCDEFGH";
   //   Algorithms.Iso7064Mod37_36.TryCalculateCheckDigit(value, out var checkChar);

   //   _outputHelper.WriteLine($"{value}{checkChar}");

   //   //foreach (Char ch in "0123456789ABCDEF")
   //   //{
   //   //   //        12341234123412341234123
   //   //   value = $"00000000000000000000000{ch}";
   //   //   Algorithms.Iso7064Mod37_36.TryCalculateCheckDigit(value, out var checkChar);

   //   //   _outputHelper.WriteLine($"{value}{checkChar}");
   //   //}
   //}

   #endregion

   #region ValidateFormatted Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void IsanAlgorithm_ValidateFormatted_ShouldReturnFalse_WhenInputIsNull()
      => _sut.ValidateFormatted(null!).Should().BeFalse();

   [Fact]
   public void IsanAlgorithm_ValidateFormatted_ShouldReturnFalse_WhenInputIsEmpty()
      => _sut.ValidateFormatted(String.Empty).Should().BeFalse();

   [Theory]
   [InlineData("ISAN 0000-0000-C36D-002B-")]
   [InlineData("ISAN 0000-0000-C36D-002B-K-")]
   [InlineData("ISAN 0000-0000-C36D-002B-K-0000-0000-")]
   [InlineData("ISAN 0000-0000-C36D-002B-K-0000-0000-E-")]
   public void IsanAlgorithm_ValidateFormatted_ShouldReturnFalse_WhenValueIsInvalidLength(String value)
      => _sut.ValidateFormatted(value).Should().BeFalse();

   [Theory]
   [InlineData("SAN 0000-0000-C36D-002B-K-0000-0000-E-")]
   [InlineData("AN 0000-0000-C36D-002B-K-0000-0000-E--")]
   [InlineData("N 0000-0000-C36D-002B-K-0000-0000-E---")]
   [InlineData(" 0000-0000-C36D-002B-K-0000-0000-E----")]
   [InlineData("0000-0000-C36D-002B-K-0000-0000-E-----")]
   [InlineData("I-AN 0000-0000-C36D-002B-K-0000-0000-E")]
   [InlineData("ISIN 0000-0000-C36D-002B-K-0000-0000-E")]
   public void IsanAlgorithm_ValidateFormatted_ShouldReturnFalse_WhenInputDoesNotStartWithIsanPrefix(String value)
      => _sut.ValidateFormatted(value).Should().BeFalse();

   [Theory]
   [InlineData("ISAN 00000-000-C36D-002B-K-0000-0000-E")]   // Would pass unless formatting explicitly checked
   [InlineData("ISAN 0000-0000C-36D-002B-K-0000-0000-E")]   // Valid digit in dash location
   [InlineData("ISAN 0000-0000-C36D0-02B-K-0000-0000-E")]
   [InlineData("ISAN 0000-0000-C36D-002B-K0-000-0000-E")]

   [InlineData("ISAN 000-00000-C36D-002B-K-0000-0000-E")]   // Dash in digit location
   [InlineData("ISAN 0000-000-0C36D-002B-K-0000-0000-E")]
   [InlineData("ISAN 0000-0000-C36-D002B-K-0000-0000-E")]
   [InlineData("ISAN 0000-0000-C36D-002-BK-0000-0000-E")]
   [InlineData("ISAN 0000-0000-C36D-002B-K-000-00000-E")]
   [InlineData("ISAN 0000-0000-C36D-002B-K-0000-000-0E")]
   public void IsanAlgorithm_ValidateFormatted_ShouldReturnFalse_WhenInputContainsOutOfPlaceGroupSeparator(String value)
      => _sut.ValidateFormatted(value).Should().BeFalse();

   [Theory]
   [InlineData("ISAN 0000-0001-8947-0000-8")]               // Example value from Wikipedia
   [InlineData("ISAN B159-D8FA-0124-0000-K")]               // Example from https://www.isan.org/docs/isan_check_digit_calculation_v2.0.pdf
   [InlineData("ISAN 0000-0000-C36D-002B-K-0000-0000-E")]   // Full ISAN for Star Trek episode "Amok Time"
   [InlineData("ISAN 0000-0000-C36D-004B-C-0000-0000-1")]   // Full ISAN for Star Trek episode "The Trouble With Tribbles"
   [InlineData("ISAN 0000-0000-D2D7-0009-L-0000-0000-B")]   // Full ISAN for Star Trek DS9 episode "Trials And Tribble-ations"
   public void IsanAlgorithm_ValidateFormatted_ShouldReturnTrue_WhenValueContainsValidCheckDigit(String value)
      => _sut.ValidateFormatted(value).Should().BeTrue();

   [Theory]
   [InlineData("ISAN 0000-0000-0000-000G-E")]               // Would pass unless explicitly checked for hexadecimal value
   [InlineData("ISAN 0000-0000-0000-000F-G-0000-000G-T")]
   public void IsanAlgorithm_ValidateFormatted_ShouldReturnFalse_WhenValueContainsNonHexadecimalDigit(String value)
      => _sut.ValidateFormatted(value).Should().BeFalse();

   #endregion
}
