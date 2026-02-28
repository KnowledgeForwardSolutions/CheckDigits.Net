// Ignore Spelling: Damm Quasigroup

namespace CheckDigits.Net.GeneralAlgorithms;

public sealed class DammCustomQuasigroupAlgorithm(IDammQuasigroup quasigroup) : ISingleCheckDigitAlgorithm, IMaskedCheckDigitAlgorithm
{
   private readonly Int32 _order = quasigroup.Order;
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
      var processLength = value.Length;
      for (var index = 0; index < processLength; index++)
      {
         var current = _quasigroup.MapCharacter(value[index]);
         if (current < 0 || current >= _order)
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
      var processLength = value.Length;
      for (var index = 0; index < processLength; index++)
      {
         var current = _quasigroup.MapCharacter(value[index]);
         if (current < 0 || current >= _order)
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
      if (String.IsNullOrEmpty(value) || value.Length < _validateMinLength)
      {
         return false;
      }

      var interim = 0;
      var processedDigits = 0;
      var processLength = value.Length - 1;
      for (var index = 0; index < processLength; index++)
      {
         if (mask.ExcludeCharacter(index))
         {
            continue;
         }
         var current = _quasigroup.MapCharacter(value[index]);
         if (current < 0 || current >= _order)
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

      var checkDigit = _quasigroup.MapCharacter(value[^1]);
      if (checkDigit < 0 || checkDigit >= _order)
      {
         return false;
      }
      
      interim = _quasigroup[interim, checkDigit];
      return interim == 0;
   }
}
