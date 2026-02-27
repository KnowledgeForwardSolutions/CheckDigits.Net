// Ignore Spelling: Damm Quasigroup

namespace CheckDigits.Net.GeneralAlgorithms;

public sealed class DammCustomQuasigroupAlgorithm(IDammQuasigroup quasigroup) : ISingleCheckDigitAlgorithm, IMaskedCheckDigitAlgorithm
{
   private readonly Int32 _order = quasigroup.Order;
   private readonly IDammQuasigroup _quasigroup = quasigroup ?? throw new ArgumentNullException(
         nameof(quasigroup),
         Resources.QuasigroupRequiredMessage);

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

      checkDigit = _quasigroup.GetCheckDigit(interim);
      return true;
   }

   /// <inheritdoc/>
   public Boolean Validate(String value) 
      => throw new NotImplementedException();

   /// <inheritdoc/>
   public Boolean Validate(String value, ICheckDigitMask mask) 
      => throw new NotImplementedException();
}
