// Ignore Spelling: Damm Quasigroup

namespace CheckDigits.Net.GeneralAlgorithms;

/// <summary>
///   Generalized version of the Damm algorithm which allows the consumer to 
///   define a custom quasigroup instead of relying on the decimal quasigroup 
///   used by <see cref="DammAlgorithm"/>.
/// </summary>
/// <param name="quasigroup">
///   Custom quasigroup object to use in the check value calculations. The 
///   quasigroup also defines the supported character set and handles mapping 
///   characters to their integer equivalent and from calculated check values 
///   back to a character value.
/// </param>
/// <remarks>
///   <para>
///   Valid characters depend on supplied quasigroup.
///   </para>
///   <para>
///   Check character calculated by the algorithm will be one of the characters
///   defined by the supplied quasigroup.
///   </para>
///   <para>
///   Assumes that the check character (if present) is the right-most character 
///   in the input value.
///   </para>
///   <para>
///   Error-detection capabilities depend on the properties of the supplied
///   quasigroup. When used with a quasigroup that satisfies the standard Damm
///   algorithm constraints (such as the default decimal quasigroup), the
///   algorithm will detect all single-character transcription errors and all
///   two-character transpositions of adjacent characters.
///   </para>
/// </remarks>
public sealed class DammCustomQuasigroupAlgorithm(IDammQuasigroup quasigroup) : ISingleCheckDigitAlgorithm, IMaskedCheckDigitAlgorithm
{
   private readonly IDammQuasigroup _quasigroup = quasigroup ?? throw new ArgumentNullException(
         nameof(quasigroup),
         Resources.QuasigroupDefinitionRequiredMessage);
   private const Int32 _validateMinLength = 2;

   /// <inheritdoc/>
   public String AlgorithmDescription => Resources.DammCustomQuasigroupAlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.DammCustomQuasigroupAlgorithmName;

   /// <inheritdoc/>
   public Boolean TryCalculateCheckDigit(String value, out Char checkDigit) 
   {
      checkDigit = Chars.NUL;
      if (String.IsNullOrEmpty(value))
      {
         return false;
      }

      var interim = 0;
      var order = _quasigroup.Order;
      var processLength = value.Length;
      for (var index = 0; index < processLength; index++)
      {
         var current = _quasigroup.MapCharacter(value[index]);
         if (current < 0 || current >= order)
         {
            return false;
         }
         interim = _quasigroup[interim, current];
      }

      checkDigit = _quasigroup.GetCheckCharacter(interim);
      return true;
   }

   /// <inheritdoc/>
   public Boolean Validate(String value) 
   {
      if (String.IsNullOrEmpty(value) || value.Length < _validateMinLength)
      {
         return false;
      }

      var interim = 0;
      var order = _quasigroup.Order;
      var processLength = value.Length;
      for (var index = 0; index < processLength; index++)
      {
         var current = _quasigroup.MapCharacter(value[index]);
         if (current < 0 || current >= order)
         {
            return false;
         }
         interim = _quasigroup[interim, current];
      }

      return interim == 0;
   }

   /// <inheritdoc/>
   public Boolean Validate(String value, ICheckDigitMask mask)
   {
      if (mask is null)
      {
         throw new ArgumentNullException(nameof(mask), Resources.NullMaskMessage);
      }
      if (String.IsNullOrEmpty(value))
      {
         return false;
      }

      var interim = 0;
      var order = _quasigroup.Order;
      var processedDigits = 0;
      var processLength = value.Length - 1;
      for (var index = 0; index < processLength; index++)
      {
         if (mask.ExcludeCharacter(index))
         {
            continue;
         }
         var current = _quasigroup.MapCharacter(value[index]);
         if (current < 0 || current >= order)
         {
            return false;
         }
         interim = _quasigroup[interim, current];
         processedDigits++;
      }
      if (processedDigits == 0)
      {
         return false;
      }

      // Handle check character outside of loop to ensure that the mask doesn't
      // accidentally exclude it.
      var checkDigit = _quasigroup.MapCharacter(value[^1]);
      if (checkDigit < 0 || checkDigit >= order)
      {
         return false;
      }
      
      interim = _quasigroup[interim, checkDigit];
      return interim == 0;
   }
}
