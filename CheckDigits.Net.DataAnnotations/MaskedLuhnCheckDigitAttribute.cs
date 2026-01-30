// Ignore Spelling: Luhn

namespace CheckDigits.Net.DataAnnotations;

/// <summary>
///   Specifies that a string property or parameter must contain a valid Luhn 
///   check digit sequence for validation to succeed. Successful validation 
///   means that the value does not contain any transcription errors capable of
///   being detected by the Luhn algorithm.
/// </summary>
/// <typeparam name="TMask">
///   The concrete type of the <see cref="ICheckDigitMask"/> to use when 
///   validating values. Must have a parameterless constructor.
/// </typeparam>
/// <remarks>
///   <para>
///      Use this attribute to enforce that a value that contains formatting 
///      characters, conforms to the Luhn algorithm. The validation passes if 
///      the value is null or an empty string or if the value contains a valid 
///      Luhn check digit in the right-most character position.
///   </para>
///   <para>
///      If applied to a non-empty string property, validation will fail under 
///      the following conditions:
///      <list type="bullet">
///         <item>
///            The total number of characters in the value that are accepted by 
///            the check digit mask is less than two (this includes the check
///            digit itself which is always assumed to be in the right-most
///            character position). This is the minimum number of characters 
///            required for a valid Luhn check digit sequence.
///         </item>
///         <item>
///            The value contains non-ASCII digit characters that are not 
///            excluded by the check digit mask.
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
///   <para>
///      Note that the check digit mask is not used to determine the check 
///      digit location. The right-most character in the value is always assumed
///      to be the check digit.
///   </para>
/// </remarks>
public class MaskedLuhnCheckDigitAttribute<TMask>()
   : BaseMaskedCheckDigitAttribute<TMask>((IMaskedCheckDigitAlgorithm)Algorithms.Luhn, Messages.SingleCheckDigitFailure)
   where TMask : ICheckDigitMask, new()
{
}
