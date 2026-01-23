namespace CheckDigits.Net.DataAnnotations;

/// <summary>
///   Specifies that a string property or parameter must contain a valid 
///   Modulus 11 check digit sequence for validation to succeed.
/// </summary>
/// <remarks>
///   <para>
///      Use this attribute to enforce that a value, such as an ISBN-10 or ISSN
///      conforms to the Modulus 11 algorithm. The validation passes if the 
///      value is null or an empty string or if the value contains a valid 
///      Modulus 11 check digit in the right-most character position.
///   </para>
///   <para>
///      If applied to a non-empty string property, validation will fail under 
///      the following conditions:
///      <list type="bullet">
///         <item>
///            The value is less than two characters in length, which is the 
///            minimum required for a valid Modulus11 check digit sequence.
///         </item>
///         <item>
///            The value is greater than ten characters in length, which is the 
///            maximum supported for a valid Modulus11 check digit sequence.
///         </item>
///         <item>
///            The value contains non-numeric characters in any position other
///            than the right-most check digit position or the value contains a
///            character other than '0'-'9' or 'X' in the right-most check digit
///            position.
///         </item>
///         <item>
///            The value does not contain a valid Modulus11 check digit in the 
///            right-most character position.
///         </item>
///      </list> 
///   </para>
///   <para>
///      Validation will also fail if the attribute is applied to a non-string
///      property.
///   </para>
/// </remarks>
public class Modulus11CheckDigitAttribute()
   : BaseCheckDigitAttribute(Algorithms.Modulus11, Messages.SingleCheckDigitFailure)
{
}
