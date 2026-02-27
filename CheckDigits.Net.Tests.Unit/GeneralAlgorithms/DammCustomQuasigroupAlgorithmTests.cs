// Ignore Spelling: Damm Quasigroup

namespace CheckDigits.Net.Tests.Unit.GeneralAlgorithms;

public class DammCustomQuasigroupAlgorithmTests
{
   private static readonly IDammQuasigroup _quasigroupOrder10 = new DammQuasigroupOrder10();
   private static readonly DammCustomQuasigroupAlgorithm _sutOrder10 = new(_quasigroupOrder10);

   private static DammCustomQuasigroupAlgorithm GetAlgorithm(Int32 order)
      => order switch
      {
         10 => _sutOrder10,
         _ => throw new ArgumentOutOfRangeException(nameof(order), $"No Damm quasigroup of order {order} is available.")
      };

   #region AlgorithmDescription Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void DammAlgorithm_AlgorithmDescription_ShouldReturnExpectedValue()
      => _sutOrder10.AlgorithmDescription.Should().Be(Resources.DammCustomQuasigroupAlgorithmDescription);

   #endregion

   #region AlgorithmName Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void DammAlgorithm_AlgorithmName_ShouldReturnExpectedValue()
      => _sutOrder10.AlgorithmName.Should().Be(Resources.DammCustomQuasigroupAlgorithmName);

   #endregion

   #region TryCalculateCheckDigit Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void DammAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsNull()
   {
      // Act/assert.
      _sutOrder10.TryCalculateCheckDigit(null!, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Fact]
   public void DammAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsEmpty()
   {
      // Act/assert.
      _sutOrder10.TryCalculateCheckDigit(String.Empty, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Theory]
   [InlineData(10, "572", '4')]                      // Worked example from Wikipedia
   [InlineData(10, "11294", '6')]                    // Test data from https://www.rosettacode.org/wiki/Damm_algorithm#C#
   [InlineData(10, "12345678901", '8')]              // Value calculated by https://jackanderson.me/2020/09/damm-algorithm-check-digit-tool/
   [InlineData(10, "123456789012345", '0')]          // "
   [InlineData(10, "11223344556677889900", '6')]     // "
   public void DammAlgorithm_TryCalculateCheckDigit_ShouldCalculateExpectedCheckDigit(
      Int32 order,
      String value,
      Char expectedCheckDigit)
   {
      // Arrange.
      var sut = GetAlgorithm(order);

      // Act/assert.
      sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Fact]
   public void DamnAlgorithm_TryCalculateCheckDigit_ShouldCalculateExpectedCheckDigit_WhenInputIsAllZeros()
   {
      // Arrange.
      var value = "00000";
      var expectedCheckDigit = Chars.DigitZero;

      // Act/assert.
      _sutOrder10.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Theory]
   [InlineData(10, "12G45")]
   [InlineData(10, "12)45")]
   public void DammAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputContainsInvalidCharacter(
      Int32 order,
      String value)
   {
      // Arrange.
      var sut = GetAlgorithm(order);

      // Act/assert.
      sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Theory]
   [InlineData(10, "140")]
   [InlineData(10, "140662")]
   [InlineData(10, "140662538")]
   [InlineData(10, "140662538042")]
   [InlineData(10, "140662538042551")]
   [InlineData(10, "140662538042551028")]
   [InlineData(10, "140662538042551028265")]
   public void DammAlgorithm_TryCalculateValue_ShouldReturnTrue_ForBenchmarkValues(
      Int32 order, 
      String value)
   {
      // Arrange.
      var sut = GetAlgorithm(order);

      // Act/assert.
      sut.TryCalculateCheckDigit(value, out _).Should().BeTrue();
   }

   #endregion
}
