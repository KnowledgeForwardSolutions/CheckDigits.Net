// Ignore Spelling: Verhoeff

namespace CheckDigits.Net.DataAnnotations;

/// <summary>
///   Specifies that a string property or parameter must contain a valid 
///   Verhoeff check digit sequence for validation to succeed. Successful 
///   validation means that the value does not contain any transcription errors 
///   capable of being detected by the Verhoeff algorithm.
/// </summary>
/// <remarks>
///   <para>
///      Use this attribute to enforce that a value conforms to the Verhoeff 
///      algorithm. The validation passes if the value is null or an empty 
///      string or if the value contains a valid Verhoeff check digit in the 
///      right-most character position.
///   </para>
///   <para>
///      If applied to a non-empty string property, validation will fail under 
///      the following conditions:
///      <list type="bullet">
///         <item>
///            The value is less than two characters in length, which is the 
///            minimum required for a valid Verhoeff check digit sequence.
///         </item>
///         <item>
///            The value contains non-numeric characters, as the Verhoeff 
///            algorithm only processes numeric digits.
///         </item>
///         <item>
///            The value does not contain a valid Verhoeff check digit in the 
///            right-most character position.
///         </item>
///      </list> 
///   </para>
///   <para>
///      Validation will also fail if the attribute is applied to a non-string
///      property.
///   </para>
/// </remarks>
public class VerhoeffCheckDigitAttribute()
   : BaseCheckDigitAttribute(Algorithms.Verhoeff, Messages.SingleCheckDigitFailure)
{
}
