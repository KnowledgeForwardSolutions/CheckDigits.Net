// Ignore Spelling: Verhoeff

namespace CheckDigits.Net.Tests.Unit.Utility;

public class VerhoeffMultiplicationTableTests
{
    #region Instance Property Tests
    // ==========================================================================
    // ==========================================================================

    [Fact]
    public void VerhoeffMultiplicationTable_Instance_ShouldReturnInstanceOfTable()
       => VerhoeffMultiplicationTable.Instance.Should().NotBeNull();

    [Fact]
    public void VerhoeffMultiplicationTable_Instance_ShouldBeSingleton()
    {
        // Act.
        var first = VerhoeffMultiplicationTable.Instance;
        var second = VerhoeffMultiplicationTable.Instance;

        // Assert.
        first.Should().NotBeNull();
        second.Should().NotBeNull();
        first.Should().BeSameAs(second);
    }

    #endregion

    #region Indexer Tests
    // ==========================================================================
    // ==========================================================================

    [Theory]
    [InlineData(0, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9)]
    [InlineData(1, 1, 2, 3, 4, 0, 6, 7, 8, 9, 5)]
    [InlineData(2, 2, 3, 4, 0, 1, 7, 8, 9, 5, 6)]
    [InlineData(3, 3, 4, 0, 1, 2, 8, 9, 5, 6, 7)]
    [InlineData(4, 4, 0, 1, 2, 3, 9, 5, 6, 7, 8)]
    [InlineData(5, 5, 9, 8, 7, 6, 0, 4, 3, 2, 1)]
    [InlineData(6, 6, 5, 9, 8, 7, 1, 0, 4, 3, 2)]
    [InlineData(7, 7, 6, 5, 9, 8, 2, 1, 0, 4, 3)]
    [InlineData(8, 8, 7, 6, 5, 9, 3, 2, 1, 0, 4)]
    [InlineData(9, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0)]

    public void VerhoeffMultiplicationTable_Indexer_ShouldReturnExpectedValues(
       Int32 x,
       Int32 y0,
       Int32 y1,
       Int32 y2,
       Int32 y3,
       Int32 y4,
       Int32 y5,
       Int32 y6,
       Int32 y7,
       Int32 y8,
       Int32 y9)
    {
        // Arrange.
        var sut = VerhoeffMultiplicationTable.Instance;

        // Act/assert.
        sut[x, 0].Should().Be(y0);
        sut[x, 1].Should().Be(y1);
        sut[x, 2].Should().Be(y2);
        sut[x, 3].Should().Be(y3);
        sut[x, 4].Should().Be(y4);
        sut[x, 5].Should().Be(y5);
        sut[x, 6].Should().Be(y6);
        sut[x, 7].Should().Be(y7);
        sut[x, 8].Should().Be(y8);
        sut[x, 9].Should().Be(y9);
    }

    #endregion
}
