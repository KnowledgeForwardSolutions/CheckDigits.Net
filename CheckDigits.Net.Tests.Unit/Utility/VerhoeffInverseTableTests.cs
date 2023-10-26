// Ignore Spelling: Verhoeff

namespace CheckDigits.Net.Tests.Unit.Utility;

public class VerhoeffInverseTableTests
{
    #region Instance Property Tests
    // ==========================================================================
    // ==========================================================================

    [Fact]
    public void VerhoeffInverseTable_Instance_ShouldReturnInstanceOfTable()
       => VerhoeffInverseTable.Instance.Should().NotBeNull();

    [Fact]
    public void VerhoeffInverseTable_Instance_ShouldBeSingleton()
    {
        // Act.
        var first = VerhoeffInverseTable.Instance;
        var second = VerhoeffInverseTable.Instance;

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
    [InlineData(0, 0)]
    [InlineData(1, 4)]
    [InlineData(2, 3)]
    [InlineData(3, 2)]
    [InlineData(4, 1)]
    [InlineData(5, 5)]
    [InlineData(6, 6)]
    [InlineData(7, 7)]
    [InlineData(8, 8)]
    [InlineData(9, 9)]
    public void VerhoeffInverseTable_Indexer_ShouldReturnExpectedValues(
       int index,
       int expected)
       => VerhoeffInverseTable.Instance[index].Should().Be(expected);

    #endregion
}
