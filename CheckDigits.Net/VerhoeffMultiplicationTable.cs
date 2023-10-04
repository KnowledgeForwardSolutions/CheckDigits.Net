// Ignore Spelling: Verhoeff

namespace CheckDigits.Net;

/// <summary>
///   Precomputed Verhoeff algorithm dihedral group multiplication table entries.
/// </summary>
public class VerhoeffMultiplicationTable : ITwoDimensionalLookupTable
{
   private static readonly Int32[,] _multiplicationTable = new Int32[10, 10]
   {
      { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 },
      { 1, 2, 3, 4, 0, 6, 7, 8, 9, 5 },
      { 2, 3, 4, 0, 1, 7, 8, 9, 5, 6 },
      { 3, 4, 0, 1, 2, 8, 9, 5, 6, 7 },
      { 4, 0, 1, 2, 3, 9, 5, 6, 7, 8 },
      { 5, 9, 8, 7, 6, 0, 4, 3, 2, 1 },
      { 6, 5, 9, 8, 7, 1, 0, 4, 3, 2 },
      { 7, 6, 5, 9, 8, 2, 1, 0, 4, 3 },
      { 8, 7, 6, 5, 9, 3, 2, 1, 0, 4 },
      { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 }
   };

   private static readonly Lazy<VerhoeffMultiplicationTable> _lazy
      = new(() => new VerhoeffMultiplicationTable());

   // Force singleton.
   private VerhoeffMultiplicationTable() { }

   /// <summary>
   ///   Reference to the single instance of the 
   ///   <see cref="VerhoeffMultiplicationTable"/>
   /// </summary>
   public static VerhoeffMultiplicationTable Instance => _lazy.Value;

   /// <inheritdoc/>
   public Int32 this[Int32 x, Int32 y] => _multiplicationTable[x, y];
}
