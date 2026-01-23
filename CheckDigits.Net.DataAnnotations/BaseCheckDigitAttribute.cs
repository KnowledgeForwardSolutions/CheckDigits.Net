namespace CheckDigits.Net.DataAnnotations;

/// <summary>
///   Abstract base class for check digit validation attributes.
/// </summary>
/// <param name="checkDigitAlgorithm">
///   Instance of the check digit algorithm to use for validation.
/// </param>
/// <param name="errorMessage">
///   The error message to use when validation fails.
/// </param>
/// <exception cref="ArgumentNullException">
///   <paramref name="checkDigitAlgorithm"/> is <see langword="null"/>.
/// </exception>
public abstract class BaseCheckDigitAttribute(
   ICheckDigitAlgorithm checkDigitAlgorithm,
   String errorMessage) : ValidationAttribute(errorMessage)
{
   private readonly ICheckDigitAlgorithm _checkDigitAlgorithm = checkDigitAlgorithm ?? throw new ArgumentNullException(nameof(checkDigitAlgorithm));

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

      return String.IsNullOrEmpty(str) || _checkDigitAlgorithm.Validate(str)
         ? ValidationResult.Success
         : new ValidationResult(FormatErrorMessage(validationContext.DisplayName),
            [validationContext.MemberName!]);
   }
}