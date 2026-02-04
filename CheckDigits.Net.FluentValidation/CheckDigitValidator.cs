// Ignore Spelling: Validator

namespace CheckDigits.Net.FluentValidation;

/// <summary>
///   Provides a property validator that verifies whether a string value 
///   contains valid check digits according to a specified check digit algorithm.
/// </summary>
/// <remarks>
///   Null or empty string values are considered valid by this validator. To 
///   enforce non-null or non-empty values, use a NotNull or NotEmpty validator 
///   in addition. The validator appends the algorithm name to the error
///   message for context when validation fails.
/// </remarks>
/// <typeparam name="T">
///   The type of the object being validated.
/// </typeparam>
/// <param name="algorithm">
///   The check digit algorithm used to validate the string value. Cannot be 
///   null. The algorithm's Validate method should be stateless and thread-safe.
/// </param>
/// <exception cref="ArgumentNullException">
///   <paramref name="algorithm"/> is <see langword="null"/>.
/// </exception>
public class CheckDigitValidator<T>(ICheckDigitAlgorithm algorithm) : PropertyValidator<T, String>
{
   private readonly ICheckDigitAlgorithm _algorithm = algorithm ?? throw new ArgumentNullException(nameof(algorithm));

   public override String Name => "CheckDigitValidator";
   
   public override Boolean IsValid(ValidationContext<T> context, String value)
   {
      // Consider null/empty as valid. Use NotNull validator to enforce non-null/non-empty.
      if (String.IsNullOrWhiteSpace(value) || _algorithm.Validate(value))
      {
         return true;
      } 

      context.MessageFormatter.AppendArgument("AlgorithmName", _algorithm.AlgorithmName);

      return false;
   }

   protected override String GetDefaultMessageTemplate(String errorCode) 
      => "{PropertyName} must have valid {AlgorithmName} check digit(s).";
}
