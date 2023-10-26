// Ignore Spelling: Verhoeff

namespace CheckDigits.Net.Tests.Unit.Utility;

public class VerhoeffPermutationTableTests
{
    #region Instance Property Tests
    // ==========================================================================
    // ==========================================================================

    [Fact]
    public void VerhoeffPermutationTable_Instance_ShouldReturnInstanceOfTable()
       => VerhoeffPermutationTable.Instance.Should().NotBeNull();

    [Fact]
    public void VerhoeffPermutationTable_Instance_ShouldBeSingleton()
    {
        // Act.
        var first = VerhoeffPermutationTable.Instance;
        var second = VerhoeffPermutationTable.Instance;

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
    [InlineData(1, 1, 5, 7, 6, 2, 8, 3, 0, 9, 4)]
    [InlineData(2, 5, 8, 0, 3, 7, 9, 6, 1, 4, 2)]
    [InlineData(3, 8, 9, 1, 6, 0, 4, 3, 5, 2, 7)]
    [InlineData(4, 9, 4, 5, 3, 1, 2, 6, 8, 7, 0)]
    [InlineData(5, 4, 2, 8, 6, 5, 7, 3, 9, 0, 1)]
    [InlineData(6, 2, 7, 9, 3, 8, 0, 6, 4, 1, 5)]
    [InlineData(7, 7, 0, 4, 6, 9, 1, 3, 2, 5, 8)]

    public void VerhoeffPermutationTable_Indexer_ShouldReturnExpectedValues(
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
        var sut = VerhoeffPermutationTable.Instance;

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
