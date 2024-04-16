namespace CheckDigits.Net;

/// <summary>
///   Enum that specifies what line separator character is used in strings that
///   contain multiple lines of data.
/// </summary>
public enum LineSeparator
{
   /// <summary>
   ///   No separator characters are used.
   /// </summary>
   None,

   /// <summary>
   ///   The Windows line separator characters, carriage return ('\r') followed
   ///   by line feed ('\n').
   /// </summary>
   Crlf,

   /// <summary>
   /// The Unix line separator character, line feed ('\n').
   /// </summary>
   Lf
}

/// <summary>
///   Additional methods for type <see cref="LineSeparator"/>.
/// </summary>
public static partial class TypeExtensions
{
   /// <summary>
   ///   Checks if <paramref name="value"/> is a valid member of the
   ///   <see cref="LineSeparator"/> enumeration.
   /// </summary>
   /// <param name="value">
   ///   The value to check.
   /// </param>
   /// <returns>
   ///   <see langword="true"/> if <paramref name="value"/> is a valid member
   ///   of the <see cref="LineSeparator"/> enumeration; otherwise 
   ///   <see langword="false"/>.
   /// </returns>
   public static Boolean IsDefined(this LineSeparator value)
   {
      return value == LineSeparator.None
         || value == LineSeparator.Crlf
         || value == LineSeparator.Lf;

   }  // IsDefined

}  // TypeExtensions

