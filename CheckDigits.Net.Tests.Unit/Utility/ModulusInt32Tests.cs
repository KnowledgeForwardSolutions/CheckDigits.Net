namespace CheckDigits.Net.Tests.Unit.Utility;

public class ModulusInt32Tests
{
   #region Constructor Tests
   // ==========================================================================
   // ==========================================================================

   [Theory]
   [InlineData(2)]
   [InlineData(3)]
   [InlineData(99)]
   public void ModulusInt32_Constructor_ShouldCreateStruct_WhenModulusIsValid(Int32 modulus)
   {
      // Act.
      var sut = new ModulusInt32(modulus);

      // Assert.
      sut.Should().NotBeNull();
      sut.Value.Should().Be(0);
      sut.Modulus.Should().Be(modulus);
      sut.MaxValue.Should().Be(modulus - 1);
   }

   [Theory]
   [InlineData(1)]
   [InlineData(0)]
   [InlineData(-1)]
   public void ModulusInt32_Constructor_ShouldThrowArgumentOutOfRangeException_WhenModulusIsLessThan2(Int32 modulus)
   {
      // Arrange.
      var act = () => _ = new ModulusInt32(modulus);

      // Act.
      act.Should().Throw<ArgumentOutOfRangeException>()
         .WithParameterName(nameof(modulus))
         .WithMessage(Resources.ModulusIntModulusOutOfRangeMessage + "*");
   }

   #endregion

   #region Value Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void ModulusInt32_Value_ShouldBeZero_WhenStructIsFirstCreated()
   {
      // Act.
      var sut = new ModulusInt32(17);

      // Assert.
      sut.Value.Should().Be(0);
   }

   [Fact]
   public void ModulusInt32_ValueGet_ShouldReturnExpectedValue()
   {
      // Arrange.
      var sut = new ModulusInt32(23);
      sut++;
      sut++;

      // Act/assert.
      sut.Value.Should().Be(2);
   }

   [Theory]
   [InlineData(0)]
   [InlineData(1)]
   [InlineData(12)]
   public void ModulusInt32_ValueSet_ShouldAssignValue_WhenValueIsInRange(Int32 value)
   {
      // Arrange.
#pragma warning disable IDE0017 // Simplify object initialization
      var sut = new ModulusInt32(13);
#pragma warning restore IDE0017 // Simplify object initialization

      // Act.
      sut.Value = value;

      // Assert
      sut.Value.Should().Be(value);
   }

   [Theory]
   [InlineData(-1)]
   [InlineData(13)]
   [InlineData(99)]
   public void ModulusInt32_ValueSet_ShouldThrowArgumentOutOfRangeException_WhenValueIsOutOfRange(Int32 value)
   {
      // Arrange.
      var sut = new ModulusInt32(13);
      var act = () => sut.Value = value;
      var message = String.Format(Resources.ModulusIntValueOutOfRangeMessageFormat, sut.Modulus);

      // Act/assert.
      act.Should().Throw<ArgumentOutOfRangeException>()
         .WithParameterName(nameof(value))
         .WithMessage(message + "*");
   }

   #endregion

   #region Implicit Int32 Cast Operator Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void ModulusInt32_ImplicitInt32CastOperator_ShouldReturnExpectedValue()
   {
      // Arrange.
      var expected = 3;
      var sut = new ModulusInt32(7)
      {
         Value = expected
      };

      // Act.
      Int32 intValue = sut;

      // Assert.
      intValue.Should().Be(expected);
   }

   #endregion

   #region Increment Operator Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void ModulusInt32_Increment_ShouldIncrementByOne()
   {
      // Arrange.
      var sut = new ModulusInt32(3);

      // Act.
      sut++;

      // Assert.
      sut.Value.Should().Be(1);
   }

   [Fact]
   public void ModulusInt32_Increment_ShouldRollOverToZero_WhenValueIsModulusMinusOne()
   {
      // Arrange.
      var modulus = 5;
      var sut = new ModulusInt32(modulus)
      {
         Value = modulus - 1
      };

      // Act.
      sut++;

      // Assert.
      sut.Value.Should().Be(0);
   }

   #endregion

   #region Decrement Operator Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void ModulusInt32_Decrement_ShouldDecrementByOne()
   {
      // Arrange.
      var sut = new ModulusInt32(31)
      {
         Value = 13
      };

      // Act.
      sut--;

      // Assert.
      sut.Value.Should().Be(12);
   }

   [Fact]
   public void ModulusInt32_Increment_ShouldRollOverToModulusMinusOne_WhenValueIsZero()
   {
      // Arrange.
      var modulus = 3;
      var sut = new ModulusInt32(modulus);
      var expected = modulus - 1;

      // Act.
      sut--;

      // Assert.
      sut.Value.Should().Be(expected);
   }

   #endregion
}
