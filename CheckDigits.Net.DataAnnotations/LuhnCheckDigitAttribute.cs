// Ignore Spelling: Luhn

namespace CheckDigits.Net.DataAnnotations;

/// <summary>
///   Specifies that a string property or parameter must contain a valid Luhn 
///   check digit sequence for validation to succeed. Successful validation 
///   means that the value does not contain any transcription errors capable of
///   being detected by the Luhn algorithm.
/// </summary>
/// <remarks>
///   <para>
///      Use this attribute to enforce that a value, such as a credit card 
///      number or other identifier, conforms to the Luhn algorithm. The 
///      validation passes if the value is null or an empty string or if the
///      value contains a valid Luhn check digit in the right-most character 
///      position.
///   </para>
///   <para>
///      If applied to a non-empty string property, validation will fail under 
///      the following conditions:
///      <list type="bullet">
///         <item>
///            The value is less than two characters in length, which is the 
///            minimum required for a valid Luhn check digit sequence.
///         </item>
///         <item>
///            The value contains non-numeric characters, as the Luhn algorithm 
///            only processes numeric digits.
///         </item>
///         <item>
///            The value does not contain a valid Luhn check digit in the 
///            right-most character position.
///         </item>
///      </list> 
///   </para>
///   <para>
///      Validation will also fail if the attribute is applied to a non-string
///      property.
///   </para>
/// </remarks>
public class LuhnCheckDigitAttribute()
   : BaseCheckDigitAttribute(Algorithms.Luhn, Messages.SingleCheckDigitFailure)
{
}
