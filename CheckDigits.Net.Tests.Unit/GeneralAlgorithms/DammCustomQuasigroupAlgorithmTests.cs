// Ignore Spelling: Damm Quasigroup

namespace CheckDigits.Net.Tests.Unit.GeneralAlgorithms;

public class DammCustomQuasigroupAlgorithmTests
{
   private static readonly IDammQuasigroup _quasigroupOrder10 = new DammQuasigroupOrder10();
   private static readonly IDammQuasigroup _quasigroupOrder16 = DammQuasigroupOrder16.GetQuasigroup();
   private static readonly DammCustomQuasigroupAlgorithm _sutOrder10 = new(_quasigroupOrder10);
   private static readonly DammCustomQuasigroupAlgorithm _sutOrder16 = new(_quasigroupOrder16);
   private readonly ICheckDigitMask _acceptAllMask = new AcceptAllMask();
   private readonly ICheckDigitMask _groupsOfThreeMask = new GroupsOfThreeCheckDigitMask();
   private readonly ICheckDigitMask _rejectAllMask = new RejectAllMask();

   private static DammCustomQuasigroupAlgorithm GetAlgorithm(Int32 order)
      => order switch
      {
         10 => _sutOrder10,
         16 => _sutOrder16,
         _ => throw new ArgumentOutOfRangeException(nameof(order), $"No Damm quasigroup of order {order} is available.")
      };

   #region AlgorithmDescription Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void DammCustomQuasigroupAlgorithm_AlgorithmDescription_ShouldReturnExpectedValue()
      => _sutOrder10.AlgorithmDescription.Should().Be(Resources.DammCustomQuasigroupAlgorithmDescription);

   #endregion

   #region AlgorithmName Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void DammCustomQuasigroupAlgorithm_AlgorithmName_ShouldReturnExpectedValue()
      => _sutOrder10.AlgorithmName.Should().Be(Resources.DammCustomQuasigroupAlgorithmName);

   #endregion

   #region TryCalculateCheckDigit Tests
   // ==========================================================================
   // ==========================================================================

   [Theory]
   [InlineData(10)]
   [InlineData(16)]
   public void DammCustomQuasigroupAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsNull(Int32 order)
   {
      // Arrange.
      var sut = GetAlgorithm(order);

      // Act/assert.
      sut.TryCalculateCheckDigit(null!, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Theory]
   [InlineData(10)]
   [InlineData(16)]
   public void DammCustomQuasigroupAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsEmpty(Int32 order)
   {
      // Arrange.
      var sut = GetAlgorithm(order);

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
   [InlineData(16, "2ED", '1')]            
   [InlineData(16, "2EDC15", '9')]         
   [InlineData(16, "2EDC15B3C", 'D')]      
   [InlineData(16, "2EDC15B3C1C3", '3')]   
   [InlineData(16, "2EDC15B3C1C34F4", 'C')]
   [InlineData(16, "2EDC15B3C1C34F4DA5", 'E')]
   [InlineData(16, "2EDC15B3C1C34F4DA55F3", '7')]
   [InlineData(16, "22446688AACCEE1155FF0", '0')]
   [InlineData(16, "123456789ABCDEF012345", '1')]
   public void DammCustomQuasigroupAlgorithm_TryCalculateCheckDigit_ShouldCalculateExpectedCheckDigit(
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

   [Theory]
   [InlineData(10, "00000")]
   [InlineData(16, "00000")]
   public void DamnCustomQuasigroupAlgorithm_TryCalculateCheckDigit_ShouldCalculateExpectedCheckDigit_WhenInputIsAllZeros(
      Int32 order,
      String value)
   {
      // Arrange.
      var sut = GetAlgorithm(order);
      var expectedCheckDigit = Chars.DigitZero;

      // Act/assert.
      sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Theory]
   [InlineData(10, "12G45")]
   [InlineData(10, "12)45")]
   [InlineData(16, "12G45")]
   [InlineData(16, "12)45")]
   public void DammCustomQuasigroupAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputContainsInvalidCharacter(
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
   [InlineData(16, "2ED")]            
   [InlineData(16, "2EDC15")]         
   [InlineData(16, "2EDC15B3C")]      
   [InlineData(16, "2EDC15B3C1C3")]   
   [InlineData(16, "2EDC15B3C1C34F4")]
   [InlineData(16, "2EDC15B3C1C34F4DA5")]
   [InlineData(16, "2EDC15B3C1C34F4DA55F3")]
   public void DammCustomQuasigroupAlgorithm_TryCalculateValue_ShouldReturnTrue_ForBenchmarkValues(
      Int32 order, 
      String value)
   {
      // Arrange.
      var sut = GetAlgorithm(order);

      // Act/assert.
      sut.TryCalculateCheckDigit(value, out _).Should().BeTrue();
   }

   #endregion

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Theory]
   [InlineData(10)]
   [InlineData(16)]
   public void DammCustomQuasigroupAlgorithm_Validate_ShouldReturnFalse_WhenInputIsNull(Int32 order)
   {
      // Arrange.
      var sut = GetAlgorithm(order);

      // Act/assert.
      sut.Validate(null!).Should().BeFalse();
   }

   [Theory]
   [InlineData(10)]
   [InlineData(16)]
   public void DammCustomQuasigroupAlgorithm_Validate_ShouldReturnFalse_WhenInputIsEmpty(Int32 order)
   {
      // Arrange.
      var sut = GetAlgorithm(order);

      // Act/assert.
      sut.Validate(String.Empty).Should().BeFalse();
   }

   [Theory]
   [InlineData(10, "0")]    // "0" is the only digit that would pass unless length is checked explicitly
   [InlineData(10, "1")]
   [InlineData(16, "0")]    // "0" is the only digit that would pass unless length is checked explicitly
   [InlineData(16, "1")]
   public void DammCustomQuasigroupAlgorithm_Validate_ShouldReturnFalse_WhenInputIsLengthOne(
      Int32 order,
      String value)
   {
      // Arrange.
      var sut = GetAlgorithm(order);

      // Act/assert.
      sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   [InlineData(10, "5724")]                      // Worked example from Wikipedia
   [InlineData(10, "112946")]                    // Test data from https://www.rosettacode.org/wiki/Damm_algorithm#C#
   [InlineData(10, "123456789018")]              // Value calculated by https://jackanderson.me/2020/09/damm-algorithm-check-digit-tool/
   [InlineData(10, "1234567890123450")]          // "
   [InlineData(10, "112233445566778899006")]     // "
   [InlineData(16, "2ED1")]            
   [InlineData(16, "2EDC159")]         
   [InlineData(16, "2EDC15B3CD")]      
   [InlineData(16, "2EDC15B3C1C33")]   
   [InlineData(16, "2EDC15B3C1C34F4C")]
   [InlineData(16, "2EDC15B3C1C34F4DA5E")]
   [InlineData(16, "2EDC15B3C1C34F4DA55F37")]
   [InlineData(16, "22446688AACCEE1155FF00")]
   [InlineData(16, "123456789ABCDEF0123451")]
   public void DammCustomQuasigroupAlgorithm_Validate_ShouldReturnTrue_WhenInputContainsValidCheckDigit(
      Int32 order,
      String value)
   {
      // Arrange.
      var sut = GetAlgorithm(order);

      // Act/assert.
      sut.Validate(value).Should().BeTrue();
   }

   [Theory]
   [InlineData(10)]
   [InlineData(16)]
   public void DammCustomQuasigroupAlgorithm_Validate_ShouldReturnTrue_WhenInputIsAllZeros(Int32 order)
   {
      // Arrange.
      var sut = GetAlgorithm(order);

      // Act/assert.
      sut.Validate("0000000000000000").Should().BeTrue();
   }

   [Theory]
   [InlineData(10, "112233445566778899016")]    // Single digit errors (using "112233445566778899006" as a valid value)
   [InlineData(10, "112233445566778892006")]    // "
   [InlineData(10, "112233445566778399006")]    // "
   [InlineData(10, "112233445566748899006")]    // "
   [InlineData(10, "112233445565778899006")]    // "
   [InlineData(10, "112233445666778899006")]    // "
   [InlineData(10, "112233475566778899006")]    // "
   [InlineData(10, "112238445566778899006")]    // "
   [InlineData(10, "112933445566778899006")]    // "
   [InlineData(10, "102233445566778899006")]    // "
   [InlineData(10, "121233445566778899006")]    // Transposition errors (using "112233445566778899006" as a valid value)
   [InlineData(10, "112323445566778899006")]    // "
   [InlineData(10, "112234345566778899006")]    // "
   [InlineData(10, "112233454566778899006")]    // "
   [InlineData(10, "112233445656778899006")]    // "
   [InlineData(10, "112233445567678899006")]    // "
   [InlineData(10, "112233445566787899006")]    // "
   [InlineData(10, "112233445566778989006")]    // "
   [InlineData(10, "112233445566778890906")]    // "
   [InlineData(10, "1236547890123450")]         // Jump transposition error using "1234567890123450" as a valid value (456 -> 654)
   [InlineData(10, "112255445566778899006")]    // Twin error using "112233445566778899006" as a valid value (33 -> 55)
   [InlineData(16, "23446688AACCEE1155FF00")]   // Single character errors (using "22446688AACCEE1155FF00" as a valid value)  
   [InlineData(10, "22445688AACCEE1155FF00")]   // "
   [InlineData(10, "22446680AACCEE1155FF00")]   // "
   [InlineData(10, "22446688AABCEE1155FF00")]   // "
   [InlineData(10, "22446688AACCEE11550F00")]   // "
   [InlineData(10, "24246688AACCEE1155FF00")]   // Transposition errors (using "22446688AACCEE1155FF00" as a valid value)
   [InlineData(10, "22464688AACCEE1155FF00")]   // "
   [InlineData(10, "22446868AACCEE1155FF00")]   // "
   [InlineData(10, "2244668A8ACCEE1155FF00")]   // "
   [InlineData(10, "22446688ACACEE1155FF00")]   // "
   [InlineData(10, "12345678BA9CDEF0123451")]   // Jump transposition error using "123456789ABCDEF0123451" as a valid value (9AB -> BA9)
   [InlineData(10, "22446688DDCCEE1155FF00")]    // Twin error using "22446688AACCEE1155FF00" as a valid value (AA -> DD)
   public void DammCustomQuasigroupAlgorithm_Validate_ShouldReturnFalse_WhenInputContainsDetectableError(
      Int32 order,
      String value)
   {
      // Arrange.
      var sut = GetAlgorithm(order);

      // Act/assert.
      sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   [InlineData(10, "12G455")]
   [InlineData(10, "12)455")]
   public void DammCustomQuasigroupAlgorithm_Validate_ShouldReturnFalse_WhenInputContainsNonDigitCharacter(
      Int32 order,
      String value)
   {
      // Arrange.
      var sut = GetAlgorithm(order);

      // Act/assert.
      sut.Validate(value).Should().BeFalse();
   }

   [Theory]
   [InlineData(10, "1402")]
   [InlineData(10, "1406622")]
   [InlineData(10, "1406625388")]
   [InlineData(10, "1406625380422")]
   [InlineData(10, "1406625380425518")]
   [InlineData(10, "1406625380425510280")]
   [InlineData(10, "1406625380425510282654")]
   [InlineData(16, "2ED1")]            
   [InlineData(16, "2EDC159")]         
   [InlineData(16, "2EDC15B3CD")]      
   [InlineData(16, "2EDC15B3C1C33")]   
   [InlineData(16, "2EDC15B3C1C34F4C")]
   [InlineData(16, "2EDC15B3C1C34F4DA5E")]
   [InlineData(16, "2EDC15B3C1C34F4DA55F37")]
   public void DammCustomQuasigroupAlgorithm_Validate_ShouldReturnTrue_ForBenchmarkValues(
      Int32 order, 
      String value)
   {
      // Arrange.
      var sut = GetAlgorithm(order);

      // Act/assert.
      sut.Validate(value).Should().BeTrue();
   }

   #endregion

   #region Validate (ICheckDigitMask Overload) Tests
   // ==========================================================================
   // ==========================================================================

   [Theory]
   [InlineData(10)]
   [InlineData(16)]
   public void DammCustomQuasigroupAlgorithm_ValidateMasked_ShouldReturnFalse_WhenInputIsNull(Int32 order)
   {
      // Arrange.
      var sut = GetAlgorithm(order);

      // Act/assert.
      sut.Validate(null!, _acceptAllMask).Should().BeFalse();
   }

   [Theory]
   [InlineData(10)]
   [InlineData(16)]
   public void DammCustomQuasigroupAlgorithm_ValidateMasked_ShouldReturnFalse_WhenInputIsEmpty(Int32 order)
   {
      // Arrange.
      var sut = GetAlgorithm(order);

      // Act/assert.
      sut.Validate(String.Empty, _acceptAllMask).Should().BeFalse();
   }

   [Theory]
   [InlineData(10, "0")]    // "0" is the only digit that would pass unless length is checked explicitly
   [InlineData(10, "1")]
   [InlineData(16, "0")]    // "0" is the only digit that would pass unless length is checked explicitly
   [InlineData(16, "1")]
   public void DammCustomQuasigroupAlgorithm_ValidateMasked_ShouldReturnFalse_WhenInsufficientUnmaskedCharactersToCalculateCheckDigit(
      Int32 order,
      String value)
   {
      // Arrange.
      var sut = GetAlgorithm(order);

      // Act/assert.
      sut.Validate(value, _acceptAllMask).Should().BeFalse();
   }

   [Theory]
   [InlineData(10)]
   [InlineData(16)]
   public void DammCustomQuasigroupAlgorithm_ValidateMasked_ShouldReturnFalse_WhenAllNonCheckDigitCharactersAreMaskedOut(Int32 order)
   {
      // Arrange.
      var sut = GetAlgorithm(order);

      // Act/assert.
      sut.Validate("0000 0000 0000 0000", _rejectAllMask).Should().BeFalse();
   }

   [Theory]
   [InlineData(10, "572 4")]                          // Worked example from Wikipedia
   [InlineData(10, "112 946")]                        // Test data from https://www.rosettacode.org/wiki/Damm_algorithm#C#
   [InlineData(10, "123 456 789 018")]                // Value calculated by https://jackanderson.me/2020/09/damm-algorithm-check-digit-tool/
   [InlineData(10, "123 456 789 012 345 0")]          // "
   [InlineData(10, "112 233 445 566 778 899 006")]    // "
   [InlineData(16, "2ED 1")]            
   [InlineData(16, "2ED C15 9")]         
   [InlineData(16, "2ED C15 B3C D")]      
   [InlineData(16, "2ED C15 B3C 1C3 3")]   
   [InlineData(16, "2ED C15 B3C 1C3 4F4 C")]
   [InlineData(16, "2ED C15 B3C 1C3 4F4 DA5 E")]
   [InlineData(16, "2ED C15 B3C 1C3 4F4 DA5 5F3 7")]
   [InlineData(16, "224 466 88A ACC EE1 155 FF0 0")]
   [InlineData(16, "123 456 789 ABC DEF 012 345 1")]
   public void DammCustomQuasigroupAlgorithm_ValidateMasked_ShouldReturnTrue_WhenInputContainsValidCheckDigit(
      Int32 order,
      String value)
   {
      // Arrange.
      var sut = GetAlgorithm(order);

      // Act/assert.
      sut.Validate(value, _groupsOfThreeMask).Should().BeTrue();
   }

   [Theory]
   [InlineData(10)]
   [InlineData(16)]
   public void DammCustomQuasigroupAlgorithm_ValidateMasked_ShouldReturnTrue_WhenInputIsAllZeros(Int32 order)
   {
      // Arrange.
      var sut = GetAlgorithm(order);

      // Act/assert.
      sut.Validate("000 000 000 000 000 0", _groupsOfThreeMask).Should().BeTrue();
   }

   [Theory]
   [InlineData(10, "112 233 445 566 778 899 016")]    // Single digit errors (using "112233445566778899006" as a valid value)
   [InlineData(10, "112 233 445 566 778 892 006")]    // "
   [InlineData(10, "112 233 445 566 778 399 006")]    // "
   [InlineData(10, "112 233 445 566 748 899 006")]    // "
   [InlineData(10, "112 233 445 565 778 899 006")]    // "
   [InlineData(10, "112 233 445 666 778 899 006")]    // "
   [InlineData(10, "112 233 475 566 778 899 006")]    // "
   [InlineData(10, "112 238 445 566 778 899 006")]    // "
   [InlineData(10, "112 933 445 566 778 899 006")]    // "
   [InlineData(10, "102 233 445 566 778 899 006")]    // "
   [InlineData(10, "121 233 445 566 778 899 006")]    // Transposition errors (using "112233445566778899006" as a valid value)
   [InlineData(10, "112 323 445 566 778 899 006")]    // "
   [InlineData(10, "112 234 345 566 778 899 006")]    // "
   [InlineData(10, "112 233 454 566 778 899 006")]    // "
   [InlineData(10, "112 233 445 656 778 899 006")]    // "
   [InlineData(10, "112 233 445 567 678 899 006")]    // "
   [InlineData(10, "112 233 445 566 787 899 006")]    // "
   [InlineData(10, "112 233 445 566 778 989 006")]    // "
   [InlineData(10, "112 233 445 566 778 890 906")]    // "
   [InlineData(10, "123 654 789 012 345 0")]          // Jump transposition error using "1234567890123450" as a valid value (456 -> 654)
   [InlineData(10, "112 255 445 566 778 899 006")]    // Twin error using "112233445566778899006" as a valid value (33 -> 55)
   [InlineData(16, "234 466 88A ACC EE1 155 FF0 0")]  // Single character errors (using "22446688AACCEE1155FF00" as a valid value)  
   [InlineData(10, "224 456 88A ACC EE1 155 FF0 0")]  // "
   [InlineData(10, "224 466 80A ACC EE1 155 FF0 0")]  // "
   [InlineData(10, "224 466 88A ABC EE1 155 FF0 0")]  // "
   [InlineData(10, "224 466 88A ACC EE1 155 0F0 0")]  // "
   [InlineData(10, "242 466 88A ACC EE1 155 FF0 0")]  // Transposition errors (using "22446688AACCEE1155FF00" as a valid value)
   [InlineData(10, "224 646 88A ACC EE1 155 FF0 0")]  // "
   [InlineData(10, "224 468 68A ACC EE1 155 FF0 0")]  // "
   [InlineData(10, "224 466 8A8 ACC EE1 155 FF0 0")]  // "
   [InlineData(10, "224 466 88A CAC EE1 155 FF0 0")]  // "
   [InlineData(10, "123 456 78B A9C DEF 012 345 1")]  // Jump transposition error using "123456789ABCDEF0123451" as a valid value (9AB -> BA9)
   [InlineData(10, "224 466 88D DCC EE1 155 FF0 0")]  // Twin error using "22446688AACCEE1155FF00" as a valid value (AA -> DD)
   public void DammCustomQuasigroupAlgorithm_ValidateMasked_ShouldReturnFalse_WhenInputContainsDetectableError(
      Int32 order,
      String value)
   {
      // Arrange.
      var sut = GetAlgorithm(order);

      // Act/assert.
      sut.Validate(value, _groupsOfThreeMask).Should().BeFalse();
   }

   [Theory]
   [InlineData(10, "12G 455")]
   [InlineData(10, "12) 455")]
   public void DammCustomQuasigroupAlgorithm_ValidateMasked_ShouldReturnFalse_WhenInputContainsNonDigitCharacter(
      Int32 order,
      String value)
   {
      // Arrange.
      var sut = GetAlgorithm(order);

      // Act/assert.
      sut.Validate(value, _groupsOfThreeMask).Should().BeFalse();
   }

   [Theory]
   [InlineData(10, "140 2")]
   [InlineData(10, "140 662 2")]
   [InlineData(10, "140 662 538 8")]
   [InlineData(10, "140 662 538 042 2")]
   [InlineData(10, "140 662 538 042 551 8")]
   [InlineData(10, "140 662 538 042 551 028 0")]
   [InlineData(10, "140 662 538 042 551 028 265 4")]
   [InlineData(16, "2ED 1")]            
   [InlineData(16, "2ED C15 9")]         
   [InlineData(16, "2ED C15 B3C D")]      
   [InlineData(16, "2ED C15 B3C 1C3 3")]   
   [InlineData(16, "2ED C15 B3C 1C3 4F4 C")]
   [InlineData(16, "2ED C15 B3C 1C3 4F4 DA5 E")]
   [InlineData(16, "2ED C15 B3C 1C3 4F4 DA5 5F3 7")]
   public void DammCustomQuasigroupAlgorithm_ValidateMasked_ShouldReturnTrue_ForBenchmarkValues(
      Int32 order, 
      String value)
   {
      // Arrange.
      var sut = GetAlgorithm(order);

      // Act/assert.
      sut.Validate(value, _groupsOfThreeMask).Should().BeTrue();
   }

   #endregion
}
