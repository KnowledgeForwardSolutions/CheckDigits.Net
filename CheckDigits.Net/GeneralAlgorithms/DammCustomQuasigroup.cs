// Ignore Spelling: Damm Quasigroup

namespace CheckDigits.Net.GeneralAlgorithms;

/// <summary>
///   Provides a custom implementation of the Damm quasigroup algorithm for use 
///   in check digit calculations.
/// </summary>
/// <remarks>
///   This class implements the IDammQuasigroup interface, enabling the use of a 
///   user-defined quasigroup table for Damm algorithm operations. It can be 
///   used to validate or generate check digits in scenarios where a custom
///   quasigroup is required.
/// </remarks>
public sealed class DammCustomQuasigroup : IDammQuasigroup
{
   private readonly Int32[] _quasigroupTable;
   private readonly Func<Char, Int32> _mapCharacter;
   private readonly Func<Int32, Char> _getCheckCharacter;

   /// <summary>
   ///   Initializes a new instance of the DammCustomQuasigroup class using the 
   ///   specified quasigroup table and character mapping functions.
   /// </summary>
   /// <param name="quasigroupTable">
   ///   A two-dimensional array that defines the quasigroup operation. This 
   ///   table must not be <see langword="null"/> and must represent a valid 
   ///   quasigroup structure.
   /// </param>
   /// <param name="mapCharacter">
   ///   A function that converts a character to its corresponding integer value. 
   ///   This function must not be <see langword="null"/>.
   ///   </param>
   /// <param name="getCheckCharacter">
   ///   A function that returns the character associated with a given integer 
   ///   value. This function must not be <see langword="null"/>.
   /// </param>
   /// <exception cref="ArgumentNullException">
   ///   <paramref name="quasigroupTable"/> is <see langword="null"/>.
   ///   -or- 
   ///   <paramref name="mapCharacter"/> is <see langword="null"/>.
   ///   -or- 
   ///   <paramref name="getCheckCharacter"/> is <see langword="null"/>.
   /// </exception>
   /// <exception cref="ArgumentException">
   ///   <paramref name="quasigroupTable"/> is not a valid quasigroup table 
   ///   (e.g., not square, contains duplicate values in rows/columns, or has 
   ///   diagonal elements that are not zero).
   /// </exception>
   public DammCustomQuasigroup(
      Int32[,] quasigroupTable,
      Func<Char, Int32> mapCharacter,
      Func<Int32, Char> getCheckCharacter)
   {
#if NET8_0_OR_GREATER
      ArgumentNullException.ThrowIfNull(quasigroupTable, nameof(quasigroupTable));
      ArgumentNullException.ThrowIfNull(mapCharacter, nameof(mapCharacter));
      ArgumentNullException.ThrowIfNull(getCheckCharacter, nameof(getCheckCharacter));
#else
      if (quasigroupTable == null)
      {
         throw new ArgumentNullException(nameof(quasigroupTable));
      }
      if (mapCharacter == null)
      {
         throw new ArgumentNullException(nameof(mapCharacter));
      }
      if (getCheckCharacter == null)
      {
         throw new ArgumentNullException(nameof(getCheckCharacter));
       }
      #endif
      ValidateQuasigroupTable(quasigroupTable);

      Order = quasigroupTable.GetLength(0);
      _quasigroupTable = FlattenQuasigroupTable(quasigroupTable);
      _mapCharacter = mapCharacter;
      _getCheckCharacter = getCheckCharacter;
   }

   /// <summary>
   ///   Initializes a new instance of the DammCustomQuasigroup class using the 
   ///   specified quasigroup table and character mapping functions.
   /// </summary>
   /// <param name="quasigroupTable">
   ///   A two-dimensional array that defines the quasigroup operation. This 
   ///   table must not be <see langword="null"/> and must represent a valid 
   ///   quasigroup structure.
   /// </param>
   /// <param name="mapCharacter">
   ///   A function that converts a character to its corresponding integer value. 
   ///   This function must not be <see langword="null"/>.
   ///   </param>
   /// <param name="getCheckCharacter">
   ///   A function that returns the character associated with a given integer 
   ///   value. This function must not be <see langword="null"/>.
   /// </param>
   /// <exception cref="ArgumentNullException">
   ///   <paramref name="quasigroupTable"/> is <see langword="null"/>.
   ///   -or- 
   ///   <paramref name="mapCharacter"/> is <see langword="null"/>.
   ///   -or- 
   ///   <paramref name="getCheckCharacter"/> is <see langword="null"/>.
   /// </exception>
   /// <exception cref="ArgumentException">
   ///   <paramref name="quasigroupTable"/> is not a valid quasigroup table 
   ///   (e.g., not square, contains duplicate values in rows/columns, or has 
   ///   diagonal elements that are not zero).
   /// </exception>
   public DammCustomQuasigroup(
      Char[,] quasigroupTable, 
      Func<Char, Int32> mapCharacter,
      Func<Int32, Char> getCheckCharacter)
      : this(ToIntegerTable(quasigroupTable, mapCharacter), mapCharacter, getCheckCharacter)
   {
   }

   /// <inheritdoc/>
   // Note that interm and next are not validated here because the consumer of
   // this class (DammCustomQuasigroupAlgorithm) is responsible for ensuring that
   // they are within the valid range based on the Order of the quasigroup.
   public Int32 this[Int32 interim, Int32 next] => _quasigroupTable[(interim * Order) + next];

   /// <inheritdoc/>
   #if NET8_0_OR_GREATER
   public Int32 Order { get; private init; }
   #else
   public Int32 Order { get; private set; }
   #endif

   /// <inheritdoc/>
   public Char GetCheckCharacter(Int32 interim) => _getCheckCharacter(interim);

   /// <inheritdoc/>
   public Int32 MapCharacter(Char ch) => _mapCharacter(ch);

   /// <summary>
   ///   Flattens a two-dimensional quasigroup table into a one-dimensional 
   ///   array in row-major order.
   /// </summary>
   /// <remarks>
   ///   Flattening the table into a one-dimensional array allows for more 
   ///   efficient indexing because of .NET's memory layout optimizations for 
   ///   single-dimensional arrays.
   /// </remarks>
   private static Int32[] FlattenQuasigroupTable(Int32[,] quasigroupTable)
   {
      var order = quasigroupTable.GetLength(0);
      var flattenedTable = new Int32[quasigroupTable.Length];

      var index = 0;
      for(var row = 0; row < order; row++)
      {
         for(var col = 0; col < order; col++)
         {
            flattenedTable[index] = quasigroupTable[row, col];
            index++;
         }
      }

      return flattenedTable;
   }

   private static Int32[,] ToIntegerTable(
      Char[,] quasigroupTable,
      Func<Char, Int32> mapCharacter)
   {
      if (quasigroupTable == null)
      {
         throw new ArgumentNullException(nameof(quasigroupTable));
      }
      if (mapCharacter == null)
      {
         throw new ArgumentNullException(nameof(mapCharacter));
      }

      var order = quasigroupTable.GetLength(0);
      var intTable = new Int32[order, order];
      for (var row = 0; row < order; row++)
      {
         for (var col = 0; col < order; col++)
         {
            intTable[row, col] = mapCharacter(quasigroupTable[row, col]);
         }
      }
      return intTable;
   }

   /// <summary>
   ///   Validates that the quasigroup table satisfies the required mathematical 
   ///   properties for a quasigroup structure.
   /// </summary>
   /// <remarks>
   ///   This method checks for conditions such as the presence of unique 
   ///   elements and closure within the table. It is intended for internal use 
   ///   and does not return a value.
   /// </remarks>
   private static void ValidateQuasigroupTable(Int32[,] quasigroupTable)
   {
      // Validate dimensions are same (square table).
      var rows = quasigroupTable.GetLength(0);
      var columns = quasigroupTable.GetLength(1);
      if (rows != columns)
      {
         throw new ArgumentException(Resources.SquareQuasigroupTableRequired, nameof(quasigroupTable));
      }

      // Validate diagonal elements are all zero.
      for (var index = 0; index < rows; index++)
      {
         if (quasigroupTable[index, index] != 0)
         {
            throw new ArgumentException(Resources.QuasigroupTableDiagonalElementsMustBeZero, nameof(quasigroupTable));
         }
      }

      // Validate is a latin square (each element appears exactly once in each
      // row and column) and that each entry is within the valid range of 0 to
      // order-1.
      for (var index = 0; index < rows; index++)
      {
         var rowElements = new HashSet<Int32>();
         var columnElements = new HashSet<Int32>();
         for (var colIndex = 0; colIndex < columns; colIndex++)
         {
            var rowValue = quasigroupTable[index, colIndex];
            var columnValue = quasigroupTable[colIndex, index];
            if (rowValue < 0 || rowValue >= rows || columnValue < 0 || columnValue >= columns)
            {
               throw new ArgumentException(Resources.QuasigroupTableValuesOutOfRange, nameof(quasigroupTable));
            }
            if (!rowElements.Add(rowValue))
            {
               throw new ArgumentException(Resources.QuasigroupTableRowContainsDuplicateValues, nameof(quasigroupTable));
            }
            if (!columnElements.Add(columnValue))
            {
               throw new ArgumentException(Resources.QuasigroupTableColumnContainsDuplicateValues, nameof(quasigroupTable));
            }
         }
      }
   }
}
