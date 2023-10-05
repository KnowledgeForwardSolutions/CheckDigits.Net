namespace CheckDigits.Net;

/// <summary>
///   A 1-dimensional table of static values.
/// </summary>
public interface IOneDimensionalLookupTable
{
   /// <summary>
   ///   Gets the table entry at the specified index.
   /// </summary>
   Int32 this[Int32 index] { get; }
}
