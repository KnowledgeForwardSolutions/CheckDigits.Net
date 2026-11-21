namespace CheckDigits.Net.Utility;

internal static class CharacterMap
{
   /// <summary>
   ///   Create a pre-computed map of characters to their integer equivalents in
   ///   a check digit algorithm.
   /// </summary>
   /// <param name="start">
   ///   The first character in the mapped range.
   /// </param>
   /// <param name="end">
   ///   The final character in the mapped range.
   /// </param>
   /// <param name="map">
   ///   Function that maps a single character to its integer equivalent.
   /// </param>
   /// <returns>
   ///   An array of mapped integer values.
   /// </returns>
   /// <remarks>
   ///   <paramref name="start"/> and <paramref name="end"/> are automatically
   ///   normalized if <paramref name="end"/> is less than 
   ///   <paramref name="start"/>.
   /// </remarks>
   public static Int32[] CreateCharacterMap(
      Char start,
      Char end,
      Func<Char, Int32> map)
   {
      if (map is null)
      {
         throw new ArgumentNullException(nameof(map));
      }

      if (end < start)
      {
         (end, start) = (start, end);
      }

      var result = new Int32[end - start + 1];

      // Using Int32 instead of Char suggested by Copilot AI review (Claude Haiku 4.5).
      for(Int32 i = start; i <= end; i++)
      {
         result[i - start] = map((Char)i);
      }

      return result;
   }
}
