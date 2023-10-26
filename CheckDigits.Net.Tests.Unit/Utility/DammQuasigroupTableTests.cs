// Ignore Spelling: Damm, Quasigroup

namespace CheckDigits.Net.Tests.Unit.Utility;

public class DammQuasigroupTableTests
{
    #region Instance Property Tests
    // ==========================================================================
    // ==========================================================================

    [Fact]
    public void DammQuasigroupTable_Instance_ShouldReturnInstanceOfTable()
       => DammQuasigroupTable.Instance.Should().NotBeNull();

    [Fact]
    public void DammQuasigroupTable_Instance_ShouldBeSingleton()
    {
        // Act.
        var first = DammQuasigroupTable.Instance;
        var second = DammQuasigroupTable.Instance;

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
    [InlineData(0, 0, 3, 1, 7, 5, 9, 8, 6, 4, 2)]
    [InlineData(1, 7, 0, 9, 2, 1, 5, 4, 8, 6, 3)]
    [InlineData(2, 4, 2, 0, 6, 8, 7, 1, 3, 5, 9)]
    [InlineData(3, 1, 7, 5, 0, 9, 8, 3, 4, 2, 6)]
    [InlineData(4, 6, 1, 2, 3, 0, 4, 5, 9, 7, 8)]
    [InlineData(5, 3, 6, 7, 4, 2, 0, 9, 5, 8, 1)]
    [InlineData(6, 5, 8, 6, 9, 7, 2, 0, 1, 3, 4)]
    [InlineData(7, 8, 9, 4, 5, 3, 6, 2, 0, 1, 7)]
    [InlineData(8, 9, 4, 3, 8, 6, 1, 7, 2, 0, 5)]
    [InlineData(9, 2, 5, 8, 1, 4, 3, 6, 7, 9, 0)]


    public void DammQuasigroupTable_Indexer_ShouldReturnExpectedValues(
       int x,
       int y0,
       int y1,
       int y2,
       int y3,
       int y4,
       int y5,
       int y6,
       int y7,
       int y8,
       int y9)
    {
        // Arrange.
        var sut = DammQuasigroupTable.Instance;

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
