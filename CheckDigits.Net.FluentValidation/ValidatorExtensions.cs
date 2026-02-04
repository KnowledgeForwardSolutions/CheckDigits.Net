// Ignore Spelling: Validator

namespace CheckDigits.Net.FluentValidation;

/// <summary>
///   Extension methods that add check digit validation rules to the current
///   ruleset.
/// </summary>
public static class ValidatorExtensions
{
   /// <summary>
   ///   Adds a check digit validation rule to the current string property using 
   ///   the specified check digit algorithm.
   /// </summary>
   /// <remarks>
   ///   Use this method to ensure that a string property conforms to a specific 
   ///   check digit scheme, such as Luhn or Verhoeff. The validation will fail 
   ///   if the string does not satisfy the requirements of the provided 
   ///   algorithm.
   /// </remarks>
   /// <typeparam name="T">
   ///   The type of the object being validated.
   /// </typeparam>
   /// <param name="ruleBuilder">
   ///   The rule builder for the string property to which the check digit 
   ///   validation will be applied.
   /// </param>
   /// <param name="algorithm">
   ///   The check digit algorithm used to validate the string value. Cannot be 
   ///   null. The algorithm's Validate method should be stateless and thread-safe.
   /// </param>
   /// <returns>
   ///   A rule builder options object that can be used to configure additional 
   ///   validation rules for the string property.
   /// </returns>
   public static IRuleBuilderOptions<T, String> CheckDigit<T>(
      this IRuleBuilder<T, String> ruleBuilder,
      ICheckDigitAlgorithm algorithm)
      => ruleBuilder.SetValidator(new CheckDigitValidator<T>(algorithm));
}
