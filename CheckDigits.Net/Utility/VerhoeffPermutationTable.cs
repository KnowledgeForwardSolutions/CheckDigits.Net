// Ignore Spelling: Verhoeff

namespace CheckDigits.Net.Utility;

/// <summary>
///   Precomputed Verhoeff algorithm permutation table entries.
/// </summary>
public sealed class VerhoeffPermutationTable : ITwoDimensionalLookupTable
{
    private static readonly Int32[] _permutationTable =
    [
      0, 1, 2, 3, 4, 5, 6, 7, 8, 9,
      1, 5, 7, 6, 2, 8, 3, 0, 9, 4,
      5, 8, 0, 3, 7, 9, 6, 1, 4, 2,
      8, 9, 1, 6, 0, 4, 3, 5, 2, 7,
      9, 4, 5, 3, 1, 2, 6, 8, 7, 0,
      4, 2, 8, 6, 5, 7, 3, 9, 0, 1,
      2, 7, 9, 3, 8, 0, 6, 4, 1, 5,
      7, 0, 4, 6, 9, 1, 3, 2, 5, 8
    ];

    private static readonly Lazy<VerhoeffPermutationTable> _lazy
       = new(() => new VerhoeffPermutationTable());

    // Force singleton.
    private VerhoeffPermutationTable() { }

    /// <summary>
    ///   Reference to the single instance of the 
    ///   <see cref="VerhoeffPermutationTable"/>
    /// </summary>
    public static VerhoeffPermutationTable Instance => _lazy.Value;

    /// <inheritdoc/>
    public int this[int x, int y] => _permutationTable[(x * 10) + y];
}
