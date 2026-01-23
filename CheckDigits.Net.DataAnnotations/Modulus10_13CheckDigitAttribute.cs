namespace CheckDigits.Net.DataAnnotations;


/// <summary>
///   Specifies that a string property or parameter must contain a valid 
///   Modulus 10 (with weights 1 and 3) check digit sequence for validation to 
///   succeed.
/// </summary>
/// <remarks>
///   <para>
///      Use this attribute to enforce that a value, such as a GTIN code, EAN 
///      code UPC code or other identifier conforms to the Modulus 10 (with 
///      weights 1 and 3) algorithm. The validation passes if the value is null 
///      or an empty string or if the value contains a valid Modulus 10 (with 
///      weights 1 and 3) check digit in the right-most character position.
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
///   <para>
///      The Modulus 10 algorith with weights 1 and 3 is commonly used in 
///      product identification codes such as GTIN-13 (EAN-13) and UPC codes.
///   </para>
/// </remarks>
public class Modulus10_13CheckDigitAttribute()
   : ValidationAttribute(Messages.SingleCheckDigitFailure)
{
   protected override ValidationResult? IsValid(
      Object? value,
      ValidationContext validationContext)
   {
      if (value is null)
      {
         return ValidationResult.Success;
      }

      if (value is not String str)
      {
         return new ValidationResult(String.Format(Messages.InvalidPropertyType, validationContext.DisplayName));
      }

      return String.IsNullOrEmpty(str) || Algorithms.Modulus10_13.Validate(str)
         ? ValidationResult.Success
         : new ValidationResult(FormatErrorMessage(validationContext.DisplayName),
            [validationContext.MemberName!]);
   }
}