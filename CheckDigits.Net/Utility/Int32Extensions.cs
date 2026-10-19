namespace CheckDigits.Net.Utility;

/// <summary>
///   Provides extension methods for System.Int32 (int) values.
/// </summary>
/// <remarks>
///   Contains static extension methods intended for internal use within the 
///   assembly; see individual members for details on behavior and thread 
///   safety.
/// </remarks>
internal static class Int32Extensions
{
   extension(Int32 num)
   {
      /// <summary>
      ///   Determines whether <paramref name="num"/> is within the inclusive 
      ///   range 0 to 9.
      /// </summary>
      /// <remarks>
      ///   <para>
      ///      The range 0 - 9 are treated as valid individual digits; negative 
      ///      values and values greater than 9 are invalid.
      ///   </para>
      ///   <para>
      ///      CheckDigits.Net optimizes conversion of characters to their 
      ///      integer equivalents by subtracting the constant '0' from the 
      ///      character. For '0'-'9', this operation results in the integer
      ///      equivalent. But for characters other than '0'-'9', the 
      ///      subtraction results in a negative value (for characters that 
      ///      preceed '0' in the ASCII table) or a value .gt. 9 (for characters
      ///      that follow '9' in the ASCII table). This method is used to 
      ///      ensure that the conversion of a character to an integer resulted
      ///      in a valid integer digit.
      ///   </para>
      /// </remarks>
      /// <returns>
      ///   <see langword="true"/> if <paramref name="num"/> between 0-9
      ///   (inclusive); otherwise, <see langword="false"/>.
      /// </returns>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public Boolean IsValidDigit() => num is >= 0 and <= 9;
   }
}
