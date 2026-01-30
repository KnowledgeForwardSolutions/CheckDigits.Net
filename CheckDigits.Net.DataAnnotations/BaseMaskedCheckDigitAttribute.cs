namespace CheckDigits.Net.DataAnnotations;

/// <summary>
///   Abstract base class for masked check digit validation attributes.
/// </summary>
/// <typeparam name="TMask">
///   The concrete type of the <see cref="ICheckDigitMask"/> to use when 
///   validating values. Must have a parameterless constructor.
/// </typeparam>
/// <param name="checkDigitAlgorithm">
///   Instance of the check digit algorithm to use for validation.
/// </param>
/// <param name="errorMessage">
///   The error message to use when validation fails.
/// </param>
/// <exception cref="ArgumentNullException">
///   <paramref name="checkDigitAlgorithm"/> is <see langword="null"/>.
/// </exception>
public abstract class BaseMaskedCheckDigitAttribute<TMask>(
   IMaskedCheckDigitAlgorithm checkDigitAlgorithm,
   String errorMessage) : BaseCheckDigitAttribute(checkDigitAlgorithm, errorMessage)
   where TMask : ICheckDigitMask, new()
{
   private readonly IMaskedCheckDigitAlgorithm _checkDigitAlgorithm = checkDigitAlgorithm ?? throw new ArgumentNullException(nameof(checkDigitAlgorithm));
   private readonly ICheckDigitMask _mask = new TMask();
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

      return String.IsNullOrEmpty(str) || _checkDigitAlgorithm.Validate(str, _mask)
         ? ValidationResult.Success
         : new ValidationResult(FormatErrorMessage(validationContext.DisplayName),
            [validationContext.MemberName!]);
   }
}