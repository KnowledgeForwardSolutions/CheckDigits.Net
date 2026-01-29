namespace CheckDigits.Net.DataAnnotations;

/// <summary>
///   Specifies that a string property or parameter must contain a valid 
///   ISO/IEC 7064 MOD 97-10 check digit sequence for validation to succeed. 
///   Successful validation means that the value does not contain any 
///   transcription errors capable of being detected by the 
///   ISO/IEC 7064 MOD 97-10 algorithm.
/// </summary>
/// <remarks>
///   <para>
///      Use this attribute to enforce that a value conforms to the 
///      ISO/IEC 7064 MOD 97-10 algorithm. The validation passes if the value is 
///      null or an empty string or if the value contains valid 
///      ISO/IEC 7064 MOD 97-10 check digits in the two right-most character 
///      positions.
///   </para>
///   <para>
///      If applied to a non-empty string property, validation will fail under 
///      the following conditions:
///      <list type="bullet">
///         <item>
///            The value is less than three characters in length, which is the 
///            minimum required for a valid ISO/IEC 7064 MOD 97-10 check digit 
///            sequence.
///         </item>
///         <item>
///            The value contains characters other than ASCII digits (0-9).
///         </item>
///         <item>
///            The value does not contain two valid ISO/IEC 7064 MOD 97-10 
///            check digits in the right-most character positions.
///         </item>
///      </list> 
///   </para>
///   <para>
///      Validation will also fail if the attribute is applied to a non-string
///      property.
///   </para>
/// </remarks>
public class Iso7064Mod97_10CheckDigitAttribute()
   : BaseCheckDigitAttribute(Algorithms.Iso7064Mod97_10, Messages.MultiCheckDigitFailure)
{
}
