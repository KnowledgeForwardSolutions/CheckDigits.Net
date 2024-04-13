namespace CheckDigits.Net.Utility;

/// <summary>
///   A 2-dimensional table of static values.
/// </summary>
public interface ITwoDimensionalLookupTable
{
   /// <summary>
   ///   Gets the table entry at the specified x and y coordinates.
   /// </summary>
   Int32 this[Int32 x, Int32 y] { get; }
}
