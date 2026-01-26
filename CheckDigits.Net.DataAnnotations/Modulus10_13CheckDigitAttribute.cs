namespace CheckDigits.Net.DataAnnotations;

/// <summary>
///   Specifies that a string property or parameter must contain a valid 
///   Modulus 10 check digit calculated with alternating weights of 1 and 3 for 
///   validation to succeed. Successful validation means that the value does not 
///   contain any transcription errors capable of being detected by the 
///   Modulus10_13 algorithm.
/// </summary>
/// <remarks>
///   <para>
///      Use this attribute to enforce that a value, such as a GTIN code, EAN 
///      code UPC code or other identifier has a valid Modulus 10 (weights 1 and 
///      3) check digit. The validation passes if the value is null or an empty 
///      string or if the value contains a valid check digit in the right-most 
///      character position.
///   </para>
///   <para>
///      If applied to a non-empty string property, validation will fail under 
///      the following conditions:
///      <list type="bullet">
///         <item>
///            The value is less than two characters in length, which is the 
///            minimum required for a valid Modulus10_13 check digit sequence.
///         </item>
///         <item>
///            The value contains non-numeric characters, as the Modulus10_13 
///            algorithm only processes numeric digits.
///         </item>
///         <item>
///            The value does not contain a valid Modulus10_13 check digit in 
///            the right-most character position.
///         </item>
///      </list> 
///   </para>
///   <para>
///      Validation will also fail if the attribute is applied to a non-string
///      property.
///   </para>
/// </remarks>
public class Modulus10_13CheckDigitAttribute()
   : BaseCheckDigitAttribute(Algorithms.Modulus10_13, Messages.SingleCheckDigitFailure)
{
}