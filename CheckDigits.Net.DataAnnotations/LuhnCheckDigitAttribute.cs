// Ignore Spelling: Luhn

namespace CheckDigits.Net.DataAnnotations;

public class LuhnCheckDigitAttribute() 
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

      return String.IsNullOrEmpty(str) || Algorithms.Luhn.Validate(str)
         ? ValidationResult.Success
         : new ValidationResult(FormatErrorMessage(validationContext.DisplayName),
            [validationContext.MemberName!]);
   }
}
