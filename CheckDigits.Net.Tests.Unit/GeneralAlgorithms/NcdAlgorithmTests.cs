﻿// Ignore Spelling: Ncd

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

   #region TryCalculateCheckDigit Tests
   // ==========================================================================
   // ==========================================================================

   #endregion

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void NcdAlgorithm_Validate_ShouldReturnFalse_WhenInputIsNull()
      => _sut.Validate(null!).Should().BeFalse();

   [Fact]
   public void NcdAlgorithm_Validate_ShouldReturnFalse_WhenInputIsEmpty()
      => _sut.Validate(string.Empty).Should().BeFalse();

   [Theory]
   [InlineData("0")]
   [InlineData("1")]
   public void NcdAlgorithm_Validate_ShouldReturnFalse_WhenInputIsOneCharacterInLength(string value)
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
   public void NcdAlgorithm_Validate_CorrectlyMapCharacterValues(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("/0")]
   [InlineData(":0")]
   [InlineData("A0")]      // Uppercase is not valid
   [InlineData(@"\0")]
   [InlineData("{0")]
   public void NcdAlgorithm_Validate_IgnoreNonCharacterValues(String value)
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
   public void NcdAlgorithm_Validate_ShouldReturnTrue_WhenValueContainsValidCheckDigit(string value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("13030/xf83gt2q")]      // 13030/xf93gt2q with single digit transcription error 9 -> 8
   [InlineData("13030/xd93gt2q")]      // 13030/xf93gt2q with single char transcription error f -> d
   [InlineData("13030/tf5p30806k")]    // 13030/tf5p30086k with two digit transposition error 08 -> 80 
   [InlineData("13030/ft5p30086k")]    // 13030/tf5p30086k with two char transposition error tf -> ft
   [InlineData("13030/tf53p0086k")]    // 13030/tf5p30086k with two char transposition error p3 -> 3p
   public void NcdAlgorithm_Validate_ShouldReturnFalse_WhenInputContainsDetectableError(string value)
      => _sut.Validate(value).Should().BeFalse();

   #endregion
}
