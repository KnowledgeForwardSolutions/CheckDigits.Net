namespace CheckDigits.Net.Utility;

/// <summary>
///   A 1-dimensional table of static values.
/// </summary>
public interface IOneDimensionalLookupTable
{
    /// <summary>
    ///   Gets the table entry at the specified index.
    /// </summary>
    int this[int index] { get; }
}
