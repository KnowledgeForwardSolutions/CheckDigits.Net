namespace CheckDigits.Net.Utility;

/// <summary>
///   Integer that implements modulus arithmetic. Incrementing the value beyond
///   the modulus will wrap around to zero. Decrementing the value below zero
///   will wrap around to modulus - 1.
/// </summary>
public struct ModulusInt32
{
   private Int32 _value;
   [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0032:Use auto property", Justification = "<Pending>")]
   private readonly Int32 _modulus;

   /// <summary>
   ///   Initialize a new <see cref="ModulusInt32"/> struct.
   /// </summary>
   /// <param name="modulus">
   ///   The modulus used by this struct.
   /// </param>
   /// <exception cref="ArgumentOutOfRangeException">
   ///   <paramref name="modulus"/> is less than 2.
   /// </exception>
   public ModulusInt32(Int32 modulus)
   {
      if (modulus < 2)
      {
         throw new ArgumentOutOfRangeException(nameof(modulus), modulus, Resources.ModulusIntModulusOutOfRangeMessage);
      }

      _value = 0;
      _modulus = modulus;
   }

   /// <summary>
   ///   Gets the maximum integer value of this struct.
   /// </summary>
   public readonly Int32 MaxValue => _modulus - 1;

   /// <summary>
   ///   The modulus used by this struct.
   /// </summary>
   public readonly Int32 Modulus => _modulus;

   /// <summary>
   ///   The integer value of this struct.
   /// </summary>
   /// <remarks>
   ///   During assignment, if the assigned value equals or exceeds the modulus 
   ///   of this struct, the final value will be assigned value mod modulus. 
   /// </remarks>
   [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0251:Make member 'readonly'", Justification = "<Pending>")]
   public Int32 Value
   {
      get => _value;
      set
      {
         if (value < 0 || value >= _modulus)
         {
            var message = String.Format(Resources.ModulusIntValueOutOfRangeMessageFormat, _modulus);
            throw new ArgumentOutOfRangeException(nameof(value), value, message);
         }

         _value = value;
      }
   }

   public static implicit operator Int32(ModulusInt32 mi) => mi._value;

   /// <summary>
   ///   Increment the value of this struct by 1 and wrap around to zero if the
   ///   new value equals the modulus.
   /// </summary>
   /// <param name="mi">
   ///   The <see cref="ModulusInt32"/> struct to increment.
   /// </param>
   /// <returns>
   ///   The updated <see cref="ModulusInt32"/> struct.
   /// </returns>
   public static ModulusInt32 operator ++(ModulusInt32 mi)
   {
      mi._value++;
      if (mi._value >= mi._modulus)
      {
         mi._value = 0;
      }

      return mi;
   }

   /// <summary>
   ///   Decrement the value of this struct by 1 and wrap around to modulus - 1
   ///   if the new value is less than zero.
   /// </summary>
   /// <param name="mi">
   ///   The <see cref="ModulusInt32"/> struct to decrement.
   /// </param>
   /// <returns>
   ///   The updated <see cref="ModulusInt32"/> struct.
   /// </returns>
   public static ModulusInt32 operator --(ModulusInt32 mi)
   {
      mi._value--;
      if (mi._value < 0)
      {
         mi._value = mi._modulus - 1;
      }

      return mi;
   }
}
