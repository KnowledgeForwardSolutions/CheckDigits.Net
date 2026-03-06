// Ignore Spelling: Damm Quasigroup

namespace CheckDigits.Net.Tests.Unit.GeneralAlgorithms;

public class DammCustomQuasigroupTests
{
   public Char GetDecimalCheckCharacter(Int32 value) => (Char)(value + '0');

   public Char GetVowelCheckCharacter(Int32 value) => value switch
   {
      0 => 'A',
      1 => 'E',
      2 => 'I',
      3 => 'O',
      4 => 'U',
      _ => throw new ArgumentOutOfRangeException(nameof(value), "Value must be between 0 and 4.")
   }; 
   public static Int32 MapDecimalCharacter(Char c) => c - '0';

   public static Int32 MapVowelCharacter(Char c) => c switch
   {
      'A' => 0,
      'E' => 1,
      'I' => 2,
      'O' => 3,
      'U' => 4,
      _ => c - 'A' // This will return a value outside the valid range, which we want for testing purposes.
                   // See DammCustomQuasigroup_ConstructorCharOverload_ShouldThrowArgumentException_WhenQuasigroupTableHasValueLessThanZero
                   // and DammCustomQuasigroup_ConstructorCharOverload_ShouldThrowArgumentException_WhenQuasigroupTableHasValueGreaterThanOrder tests.
   };

   private static readonly Int32[,] _order2QuasigroupTable = 
   {
      { 0, 1 },
      { 1, 0 },
   };

   private static readonly Char[,] _vowelsQuasigroupTable = 
   {
      { 'A', 'E', 'I', 'O', 'U' },
      { 'E', 'A', 'O', 'U', 'I' },
      { 'I', 'U', 'A', 'E', 'O' },
      { 'O', 'I', 'U', 'A', 'E' },
      { 'U', 'O', 'E', 'I', 'A' },
   };

   #region Constructor Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void DammCustomQuasigroup_Constructor_ShouldCreateObject_WhenAllValuesAreValid()
   {
      // Act.
      var sut = new DammCustomQuasigroup(
         _order2QuasigroupTable,
         MapDecimalCharacter,
         GetDecimalCheckCharacter);

      // Assert.
      sut.Should().NotBeNull();
      sut.Order.Should().Be(_order2QuasigroupTable.GetLength(0));
   }

   [Fact]
   public void DammCustomQuasigroup_Constructor_ShouldThrowArgumentNullException_WhenQuasigroupTableIsNull()
   {
      // Arrange.
      Int32[,] quasigroupTable = null!;

      // Act/assert.
      FluentActions
         .Invoking(() => new DammCustomQuasigroup(
            quasigroupTable,
            MapDecimalCharacter,
            GetDecimalCheckCharacter))
         .Should()
         .ThrowExactly<ArgumentNullException>()
         .WithParameterName(nameof(quasigroupTable));
   }

   [Fact]
   public void DammCustomQuasigroup_Constructor_ShouldThrowArgumentNullException_WhenMapCharacterIsNull()
   {
      // Act/assert.
      FluentActions
         .Invoking(() =>  new DammCustomQuasigroup(
            _order2QuasigroupTable,
            null!,
            GetDecimalCheckCharacter))
         .Should()
         .ThrowExactly<ArgumentNullException>()
         .WithParameterName("mapCharacter");
   }

   [Fact]
   public void DammCustomQuasigroup_Constructor_ShouldThrowArgumentNullException_WhenGetCheckCharacterIsNull()
   {
      // Act/assert.
      FluentActions
         .Invoking(() =>  new DammCustomQuasigroup(
            _order2QuasigroupTable,
            MapDecimalCharacter,
            null!))
         .Should()
         .ThrowExactly<ArgumentNullException>()
         .WithParameterName("getCheckCharacter");
   }

   [Fact]
   public void DammCustomQuasigroup_Constructor_ShouldThrowArgumentException_WhenQuasigroupTableIsEmpty()
   {
      // Arrange.
      var quasigroupTable = new Int32[0, 0];
      var expectedMessage = Resources.EmptyQuasigroupTableMessage + "*";

      // Act/assert.
      FluentActions
         .Invoking(() => new DammCustomQuasigroup(
            quasigroupTable,
            MapDecimalCharacter,
            GetDecimalCheckCharacter))
         .Should()
         .ThrowExactly<ArgumentException>()
         .WithParameterName(nameof(quasigroupTable))
         .WithMessage(expectedMessage);
   }

   [Fact]
   public void DammCustomQuasigroup_Constructor_ShouldThrowArgumentException_WhenQuasigroupTableIsOrder1()
   {
      // Arrange.
      var quasigroupTable = new Int32[,] { { 0 } };
      var expectedMessage = Resources.QuasigroupMinimumOrder2Message + "*";

      // Act/assert.
      FluentActions
         .Invoking(() => new DammCustomQuasigroup(
            quasigroupTable,
            MapDecimalCharacter,
            GetDecimalCheckCharacter))
         .Should()
         .ThrowExactly<ArgumentException>()
         .WithParameterName(nameof(quasigroupTable))
         .WithMessage(expectedMessage);
   }

   [Fact]
   public void DammCustomQuasigroup_Constructor_ShouldThrowArgumentException_WhenQuasigroupTableIsNotSquare()
   {
      // Arrange.
      var quasigroupTable = new Int32[,]
      {
         { 0, 1, 2 },
         { 1, 0, 2 }
      };
      var expectedMessage = Resources.SquareQuasigroupTableRequired + "*";

      // Act/assert.
      FluentActions
         .Invoking(() => new DammCustomQuasigroup(
            quasigroupTable,
            MapDecimalCharacter,
            GetDecimalCheckCharacter))
         .Should()
         .ThrowExactly<ArgumentException>()
         .WithParameterName(nameof(quasigroupTable))
         .WithMessage(expectedMessage);
   }

   [Fact]
   public void DammCustomQuasigroup_Constructor_ShouldThrowArgumentException_WhenQuasigroupTableHasNonZeroDiagonal()
   {
      // Arrange.
      var quasigroupTable = new Int32[,]
      {
         { 1, 0 },
         { 0, 1 }
      };
      var expectedMessage = Resources.QuasigroupTableDiagonalElementsMustBeZero + "*";

      // Act/assert.
      FluentActions
         .Invoking(() => new DammCustomQuasigroup(
            quasigroupTable,
            MapDecimalCharacter,
            GetDecimalCheckCharacter))
         .Should()
         .ThrowExactly<ArgumentException>()
         .WithParameterName(nameof(quasigroupTable))
         .WithMessage(expectedMessage);
   }

   [Fact]
   public void DammCustomQuasigroup_Constructor_ShouldThrowArgumentException_WhenQuasigroupTableDuplicateHorizontalEntries()
   {
      // Arrange.
      var quasigroupTable = new Int32[,]
      {
         { 0, 1, 2 },
         { 2, 0, 2 },
         { 1, 2, 0 }
      };
      var expectedMessage = Resources.QuasigroupTableRowContainsDuplicateValues + "*";

      // Act/assert.
      FluentActions
         .Invoking(() => new DammCustomQuasigroup(
            quasigroupTable,
            MapDecimalCharacter,
            GetDecimalCheckCharacter))
         .Should()
         .ThrowExactly<ArgumentException>()
         .WithParameterName(nameof(quasigroupTable))
         .WithMessage(expectedMessage);
   }

   [Fact]
   public void DammCustomQuasigroup_Constructor_ShouldThrowArgumentException_WhenQuasigroupTableDuplicateVerticalEntries()
   {
      // Arrange.
      var quasigroupTable = new Int32[,]
      {
         { 0, 2, 1 },
         { 2, 0, 1 },
         { 1, 2, 0 }
      };
      var expectedMessage = Resources.QuasigroupTableColumnContainsDuplicateValues + "*";

      // Act/assert.
      FluentActions
         .Invoking(() => new DammCustomQuasigroup(
            quasigroupTable,
            MapDecimalCharacter,
            GetDecimalCheckCharacter))
         .Should()
         .ThrowExactly<ArgumentException>()
         .WithParameterName(nameof(quasigroupTable))
         .WithMessage(expectedMessage);
   }

   [Fact]
   public void DammCustomQuasigroup_Constructor_ShouldThrowArgumentException_WhenQuasigroupTableHasValueLessThanZero()
   {
      // Arrange.
      var quasigroupTable = new Int32[,]
      {
         { 0, -1 },
         { 1, 0 },
      };
      var expectedMessage = Resources.QuasigroupTableValuesOutOfRange + "*";

      // Act/assert.
      FluentActions
         .Invoking(() => new DammCustomQuasigroup(
            quasigroupTable,
            MapDecimalCharacter,
            GetDecimalCheckCharacter))
         .Should()
         .ThrowExactly<ArgumentException>()
         .WithParameterName(nameof(quasigroupTable))
         .WithMessage(expectedMessage);
   }

   [Fact]
   public void DammCustomQuasigroup_Constructor_ShouldThrowArgumentException_WhenQuasigroupTableHasValueGreaterThanOrder()
   {
      // Arrange.
      var quasigroupTable = new Int32[,]
      {
         { 0, 9 },
         { 1, 0 },
      };
      var expectedMessage = Resources.QuasigroupTableValuesOutOfRange + "*";

      // Act/assert.
      FluentActions
         .Invoking(() => new DammCustomQuasigroup(
            quasigroupTable,
            MapDecimalCharacter,
            GetDecimalCheckCharacter))
         .Should()
         .ThrowExactly<ArgumentException>()
         .WithParameterName(nameof(quasigroupTable))
         .WithMessage(expectedMessage);
   }

   #endregion

   #region Constructor (Char Overload) Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void DammCustomQuasigroup_ConstructorCharOverload_ShouldCreateObject_WhenAllValuesAreValid()
   {
      // Act.
      var sut = new DammCustomQuasigroup(
         _vowelsQuasigroupTable,
         MapVowelCharacter,
         GetVowelCheckCharacter);

      // Assert.
      sut.Should().NotBeNull();
      sut.Order.Should().Be(_vowelsQuasigroupTable.GetLength(0));
   }

   [Fact]
   public void DammCustomQuasigroup_ConstructorCharOverload_ShouldThrowArgumentNullException_WhenQuasigroupTableIsNull()
   {
      // Arrange.
      Char[,] quasigroupTable = null!;

      // Act/assert.
      FluentActions
         .Invoking(() => new DammCustomQuasigroup(
            quasigroupTable,
            MapVowelCharacter,
            GetVowelCheckCharacter))
         .Should()
         .ThrowExactly<ArgumentNullException>()
         .WithParameterName(nameof(quasigroupTable));
   }

   [Fact]
   public void DammCustomQuasigroup_ConstructorCharOverload_ShouldThrowArgumentNullException_WhenMapCharacterIsNull()
   {
      // Act/assert.
      FluentActions
         .Invoking(() =>  new DammCustomQuasigroup(
            _vowelsQuasigroupTable,
            null!,
            GetVowelCheckCharacter))
         .Should()
         .ThrowExactly<ArgumentNullException>()
         .WithParameterName("mapCharacter");
   }

   [Fact]
   public void DammCustomQuasigroup_ConstructorCharOverload_ShouldThrowArgumentNullException_WhenGetCheckCharacterIsNull()
   {
      // Act/assert.
      FluentActions
         .Invoking(() =>  new DammCustomQuasigroup(
            _vowelsQuasigroupTable,
            MapVowelCharacter,
            null!))
         .Should()
         .ThrowExactly<ArgumentNullException>()
         .WithParameterName("getCheckCharacter");
   }

   [Fact]
   public void DammCustomQuasigroup_ConstructorCharOverload_ShouldThrowArgumentException_WhenQuasigroupTableIsEmpty()
   {
      // Arrange.
      var quasigroupTable = new Char[0, 0];
      var expectedMessage = Resources.EmptyQuasigroupTableMessage + "*";

      // Act/assert.
      FluentActions
         .Invoking(() => new DammCustomQuasigroup(
            quasigroupTable,
            MapVowelCharacter,
            GetVowelCheckCharacter))
         .Should()
         .ThrowExactly<ArgumentException>()
         .WithParameterName(nameof(quasigroupTable))
         .WithMessage(expectedMessage);
   }

   [Fact]
   public void DammCustomQuasigroup_ConstructorCharOverload_ShouldThrowArgumentException_WhenQuasigroupTableIsOrder1()
   {
      // Arrange.
      var quasigroupTable = new Char[,] { { 'A' } };
      var expectedMessage = Resources.QuasigroupMinimumOrder2Message + "*";

      // Act/assert.
      FluentActions
         .Invoking(() => new DammCustomQuasigroup(
            quasigroupTable,
            MapVowelCharacter,
            GetVowelCheckCharacter))
         .Should()
         .ThrowExactly<ArgumentException>()
         .WithParameterName(nameof(quasigroupTable))
         .WithMessage(expectedMessage);
   }

   [Fact]
   public void DammCustomQuasigroup_ConstructorCharOverload_ShouldThrowArgumentException_WhenQuasigroupTableIsNotSquare()
   {
      // Arrange.
      var quasigroupTable = new Char[,]
      {
         { 'A', 'E', 'I' },
         { 'I', 'A', 'E' }
      };
      var expectedMessage = Resources.SquareQuasigroupTableRequired + "*";

      // Act/assert.
      FluentActions
         .Invoking(() => new DammCustomQuasigroup(
            quasigroupTable,
            MapVowelCharacter,
            GetVowelCheckCharacter))
         .Should()
         .ThrowExactly<ArgumentException>()
         .WithParameterName(nameof(quasigroupTable))
         .WithMessage(expectedMessage);
   }

   [Fact]
   public void DammCustomQuasigroup_ConstructorCharOverload_ShouldThrowArgumentException_WhenQuasigroupTableHasNonZeroDiagonal()
   {
      // Arrange.
      var quasigroupTable = new Char[,]
      {
         { 'E', 'A' },
         { 'A', 'E' }
      };
      var expectedMessage = Resources.QuasigroupTableDiagonalElementsMustBeZero + "*";

      // Act/assert.
      FluentActions
         .Invoking(() => new DammCustomQuasigroup(
            quasigroupTable,
            MapVowelCharacter,
            GetVowelCheckCharacter))
         .Should()
         .ThrowExactly<ArgumentException>()
         .WithParameterName(nameof(quasigroupTable))
         .WithMessage(expectedMessage);
   }

   [Fact]
   public void DammCustomQuasigroup_ConstructorCharOverload_ShouldThrowArgumentException_WhenQuasigroupTableDuplicateHorizontalEntries()
   {
      // Arrange.
      var quasigroupTable = new Char[,]
      {
         { 'A', 'E', 'I' },
         { 'I', 'A', 'I' },
         { 'E', 'I', 'A' }
      };
      var expectedMessage = Resources.QuasigroupTableRowContainsDuplicateValues + "*";

      // Act/assert.
      FluentActions
         .Invoking(() => new DammCustomQuasigroup(
            quasigroupTable,
            MapVowelCharacter,
            GetVowelCheckCharacter))
         .Should()
         .ThrowExactly<ArgumentException>()
         .WithParameterName(nameof(quasigroupTable))
         .WithMessage(expectedMessage);
   }

   [Fact]
   public void DammCustomQuasigroup_ConstructorCharOverload_ShouldThrowArgumentException_WhenQuasigroupTableDuplicateVerticalEntries()
   {
      // Arrange.
      var quasigroupTable = new Char[,]
      {
         { 'A', 'I', 'E' },
         { 'I', 'A', 'E' },
         { 'E', 'I', 'A' }
      };
      var expectedMessage = Resources.QuasigroupTableColumnContainsDuplicateValues + "*";

      // Act/assert.
      FluentActions
         .Invoking(() => new DammCustomQuasigroup(
            quasigroupTable,
            MapVowelCharacter,
            GetVowelCheckCharacter))
         .Should()
         .ThrowExactly<ArgumentException>()
         .WithParameterName(nameof(quasigroupTable))
         .WithMessage(expectedMessage);
   }

   [Fact]
   public void DammCustomQuasigroup_ConstructorCharOverload_ShouldThrowArgumentException_WhenQuasigroupTableHasValueLessThanZero()
   {
      // Arrange.
      var quasigroupTable = new Char[,]
      {
         { 'A', '0' },
         { 'E', 'A' },
      };
      var expectedMessage = Resources.QuasigroupTableValuesOutOfRange + "*";

      // Act/assert.
      FluentActions
         .Invoking(() => new DammCustomQuasigroup(
            quasigroupTable,
            MapVowelCharacter,
            GetVowelCheckCharacter))
         .Should()
         .ThrowExactly<ArgumentException>()
         .WithParameterName(nameof(quasigroupTable))
         .WithMessage(expectedMessage);
   }

   [Fact]
   public void DammCustomQuasigroup_ConstructorCharOverload_ShouldThrowArgumentException_WhenQuasigroupTableHasValueGreaterThanOrder()
   {
      // Arrange.
      var quasigroupTable = new Char[,]
      {
         { 'A', 'Z' },
         { 'E', 'A' },
      };
      var expectedMessage = Resources.QuasigroupTableValuesOutOfRange + "*";

      // Act/assert.
      FluentActions
         .Invoking(() => new DammCustomQuasigroup(
            quasigroupTable,
            MapVowelCharacter,
            GetVowelCheckCharacter))
         .Should()
         .ThrowExactly<ArgumentException>()
         .WithParameterName(nameof(quasigroupTable))
         .WithMessage(expectedMessage);
   }

   #endregion

   #region Indexer Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void DammCustomQuasigroup_Indexer_ShouldReturnCorrectValue_ForValidIndices()
   {
      // Arrange.
      var sut = new DammCustomQuasigroup(
         _order2QuasigroupTable,
         MapDecimalCharacter,
         GetDecimalCheckCharacter);

      // Act/assert.
      sut[0, 0].Should().Be(0);
      sut[0, 1].Should().Be(1);
      sut[1, 0].Should().Be(1);
      sut[1, 1].Should().Be(0);
   }

   #endregion

   #region GetCheckCharacter Method Tests
   // ==========================================================================
   // ==========================================================================

   [Theory]
   [InlineData(0, 'A')]
   [InlineData(1, 'E')]
   [InlineData(2, 'I')]
   [InlineData(3, 'O')]
   [InlineData(4, 'U')]
   public void DammCustomQuasigroup_GetCheckCharacter_ShouldReturnCorrectValue_ForValidCheckValues(
      Int32 value,
      Char expected)
   {
      // Arrange.
      var sut = new DammCustomQuasigroup(
         _vowelsQuasigroupTable,
         MapVowelCharacter,
         GetVowelCheckCharacter);

      // Act/assert.
      sut.GetCheckCharacter(value).Should().Be(expected);
      GetVowelCheckCharacter(value).Should().Be(expected);
   }

   #endregion

   #region MapCharacter Method Tests
   // ==========================================================================
   // ==========================================================================

   [Theory]
   [InlineData('A', 0)]
   [InlineData('E', 1)]
   [InlineData('I', 2)]
   [InlineData('O', 3)]
   [InlineData('U', 4)]
   public void DammCustomQuasigroup_MapCharacter_ShouldReturnCorrectValue_ForCharacters(
      Char ch,
      Int32 expected)
   {
      // Arrange.
      var sut = new DammCustomQuasigroup(
         _vowelsQuasigroupTable,
         MapVowelCharacter,
         GetVowelCheckCharacter);

      // Act/assert.
      sut.MapCharacter(ch).Should().Be(expected);
      MapVowelCharacter(ch).Should().Be(expected);
   }

   #endregion
}
