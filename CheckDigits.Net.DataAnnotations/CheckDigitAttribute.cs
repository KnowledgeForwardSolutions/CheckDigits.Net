namespace CheckDigits.Net.DataAnnotations;

/// <summary>
///   Specifies that a string property or field must contain a valid check
///   digit sequence as defined by the specified 
///   <typeparamref name="TAlgorithm"/>.
/// </summary>
/// <typeparam name="TAlgorithm">
///   The algorithm used to validate the check digit sequence. TAlgorithm must
///   implement <see cref="ICheckDigitAlgorithm"/> and have a parameterless
///   constructor. The <see cref="ICheckDigitAlgorithm.Validate(String)"/>
///   method must be stateless and thread-safe.
/// </typeparam>
/// <remarks>
///   <para>
///      Validation passes if the value is <see langword="null"/> or
///      <see cref="String.Empty"/> or if the value contains a valid check digit
///      sequence according to the specified algorithm.
///   </para>
///   <para>
///      Validation fails if the non-null, non-empty string value does not 
///      contain a valid check digit sequence according to the specified
///      algorithm.
///   </para>
///   <para>
///      Validation fails if the value is not of type <see cref="String"/>.
///   </para>
/// </remarks>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field , AllowMultiple = false)] 
public class CheckDigitAttribute<TAlgorithm> : ValidationAttribute
   where TAlgorithm : ICheckDigitAlgorithm, new()
{
   private readonly TAlgorithm _algorithm = new();

   protected override ValidationResult IsValid(Object? value, ValidationContext validationContext)
   {
      if (value is null)
      {
         return ValidationResult.Success;
      }

      if (value is not String str)
      {
         return new ValidationResult(String.Format(Messages.InvalidPropertyType, validationContext?.DisplayName ?? String.Empty));
      }

      return String.IsNullOrEmpty(str) || _algorithm.Validate(str)
         ? ValidationResult.Success
         : new ValidationResult(FormatErrorMessage(validationContext?.DisplayName ?? String.Empty),
            [validationContext?.MemberName ?? String.Empty]);
   }
}
