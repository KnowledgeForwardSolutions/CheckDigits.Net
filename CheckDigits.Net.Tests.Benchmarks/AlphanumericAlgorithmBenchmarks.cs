namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class AlphanumericAlgorithmBenchmarks
{
   public IEnumerable<Object[]> TryCalculateCheckDigitArguments()
   {
      yield return new Object[] { Algorithms.Iso7064Mod37_2, Algorithms.Iso7064Mod37_2.AlgorithmName, "U7Y" };
      yield return new Object[] { Algorithms.Iso7064Mod37_2, Algorithms.Iso7064Mod37_2.AlgorithmName, "U7Y8SX" };
      yield return new Object[] { Algorithms.Iso7064Mod37_2, Algorithms.Iso7064Mod37_2.AlgorithmName, "U7Y8SXRC0" };
      yield return new Object[] { Algorithms.Iso7064Mod37_2, Algorithms.Iso7064Mod37_2.AlgorithmName, "U7Y8SXRC0O3S" };
      yield return new Object[] { Algorithms.Iso7064Mod37_2, Algorithms.Iso7064Mod37_2.AlgorithmName, "U7Y8SXRC0O3SC4I" };
      yield return new Object[] { Algorithms.Iso7064Mod37_2, Algorithms.Iso7064Mod37_2.AlgorithmName, "U7Y8SXRC0O3SC4IHYQ" };
      yield return new Object[] { Algorithms.Iso7064Mod37_2, Algorithms.Iso7064Mod37_2.AlgorithmName, "U7Y8SXRC0O3SC4IHYQF4M" };

      yield return new Object[] { Algorithms.Iso7064Mod37_36, Algorithms.Iso7064Mod37_36.AlgorithmName, "U7Y" };
      yield return new Object[] { Algorithms.Iso7064Mod37_36, Algorithms.Iso7064Mod37_36.AlgorithmName, "U7Y8SX" };
      yield return new Object[] { Algorithms.Iso7064Mod37_36, Algorithms.Iso7064Mod37_36.AlgorithmName, "U7Y8SXRC0" };
      yield return new Object[] { Algorithms.Iso7064Mod37_36, Algorithms.Iso7064Mod37_36.AlgorithmName, "U7Y8SXRC0O3S" };
      yield return new Object[] { Algorithms.Iso7064Mod37_36, Algorithms.Iso7064Mod37_36.AlgorithmName, "U7Y8SXRC0O3SC4I" };
      yield return new Object[] { Algorithms.Iso7064Mod37_36, Algorithms.Iso7064Mod37_36.AlgorithmName, "U7Y8SXRC0O3SC4IHYQ" };
      yield return new Object[] { Algorithms.Iso7064Mod37_36, Algorithms.Iso7064Mod37_36.AlgorithmName, "U7Y8SXRC0O3SC4IHYQF4M" };

      yield return new Object[] { Algorithms.Ncd, Algorithms.Ncd.AlgorithmName, "11404/2h9" };
      yield return new Object[] { Algorithms.Ncd, Algorithms.Ncd.AlgorithmName, "11404/2h9tqb" };
      yield return new Object[] { Algorithms.Ncd, Algorithms.Ncd.AlgorithmName, "11404/2h9tqbxk6" };
      yield return new Object[] { Algorithms.Ncd, Algorithms.Ncd.AlgorithmName, "11404/2h9tqbxk6rw7" };
      yield return new Object[] { Algorithms.Ncd, Algorithms.Ncd.AlgorithmName, "11404/2h9tqbxk6rw7dwm" };
   }

   public IEnumerable<Object[]> TryCalculateCheckDigitsArguments()
   {
      yield return new Object[] { Algorithms.AlphanumericMod97_10, Algorithms.AlphanumericMod97_10.AlgorithmName, "U7y" };
      yield return new Object[] { Algorithms.AlphanumericMod97_10, Algorithms.AlphanumericMod97_10.AlgorithmName, "U7y8SX" };
      yield return new Object[] { Algorithms.AlphanumericMod97_10, Algorithms.AlphanumericMod97_10.AlgorithmName, "U7y8SXrC0" };
      yield return new Object[] { Algorithms.AlphanumericMod97_10, Algorithms.AlphanumericMod97_10.AlgorithmName, "U7y8SXrC0O3S" };
      yield return new Object[] { Algorithms.AlphanumericMod97_10, Algorithms.AlphanumericMod97_10.AlgorithmName, "U7y8SXrC0O3Sc4I" };
      yield return new Object[] { Algorithms.AlphanumericMod97_10, Algorithms.AlphanumericMod97_10.AlgorithmName, "U7y8SXrC0O3Sc4IHYQ" };
      yield return new Object[] { Algorithms.AlphanumericMod97_10, Algorithms.AlphanumericMod97_10.AlgorithmName, "U7y8SXrC0O3Sc4IHYQF4M" };

      yield return new Object[] { Algorithms.Iso7064Mod1271_36, Algorithms.Iso7064Mod1271_36.AlgorithmName, "U7Y" };
      yield return new Object[] { Algorithms.Iso7064Mod1271_36, Algorithms.Iso7064Mod1271_36.AlgorithmName, "U7Y8SX" };
      yield return new Object[] { Algorithms.Iso7064Mod1271_36, Algorithms.Iso7064Mod1271_36.AlgorithmName, "U7Y8SXRC0" };
      yield return new Object[] { Algorithms.Iso7064Mod1271_36, Algorithms.Iso7064Mod1271_36.AlgorithmName, "U7Y8SXRC0O3S" };
      yield return new Object[] { Algorithms.Iso7064Mod1271_36, Algorithms.Iso7064Mod1271_36.AlgorithmName, "U7Y8SXRC0O3SC4I" };
      yield return new Object[] { Algorithms.Iso7064Mod1271_36, Algorithms.Iso7064Mod1271_36.AlgorithmName, "U7Y8SXRC0O3SC4IHYQ" };
      yield return new Object[] { Algorithms.Iso7064Mod1271_36, Algorithms.Iso7064Mod1271_36.AlgorithmName, "U7Y8SXRC0O3SC4IHYQF4M" };
   }

   public IEnumerable<Object[]> ValidateArguments()
   {
      yield return new Object[] { Algorithms.AlphanumericMod97_10, Algorithms.AlphanumericMod97_10.AlgorithmName, "U7y46" };
      yield return new Object[] { Algorithms.AlphanumericMod97_10, Algorithms.AlphanumericMod97_10.AlgorithmName, "U7y8SX89" };
      yield return new Object[] { Algorithms.AlphanumericMod97_10, Algorithms.AlphanumericMod97_10.AlgorithmName, "U7y8SXrC087" };
      yield return new Object[] { Algorithms.AlphanumericMod97_10, Algorithms.AlphanumericMod97_10.AlgorithmName, "U7y8SXrC0O3S38" };
      yield return new Object[] { Algorithms.AlphanumericMod97_10, Algorithms.AlphanumericMod97_10.AlgorithmName, "U7y8SXrC0O3Sc4I27" };
      yield return new Object[] { Algorithms.AlphanumericMod97_10, Algorithms.AlphanumericMod97_10.AlgorithmName, "U7y8SXrC0O3Sc4IHYQ54" };
      yield return new Object[] { Algorithms.AlphanumericMod97_10, Algorithms.AlphanumericMod97_10.AlgorithmName, "U7y8SXrC0O3Sc4IHYQF4M21" };

      yield return new Object[] { Algorithms.Iso7064Mod1271_36, Algorithms.Iso7064Mod1271_36.AlgorithmName, "U7YM0" };
      yield return new Object[] { Algorithms.Iso7064Mod1271_36, Algorithms.Iso7064Mod1271_36.AlgorithmName, "U7Y8SXOR" };
      yield return new Object[] { Algorithms.Iso7064Mod1271_36, Algorithms.Iso7064Mod1271_36.AlgorithmName, "U7Y8SXRC0FI" };
      yield return new Object[] { Algorithms.Iso7064Mod1271_36, Algorithms.Iso7064Mod1271_36.AlgorithmName, "U7Y8SXRC0O3SX4" };
      yield return new Object[] { Algorithms.Iso7064Mod1271_36, Algorithms.Iso7064Mod1271_36.AlgorithmName, "U7Y8SXRC0O3SC4I9D" };
      yield return new Object[] { Algorithms.Iso7064Mod1271_36, Algorithms.Iso7064Mod1271_36.AlgorithmName, "U7Y8SXRC0O3SC4IHYQYI" };
      yield return new Object[] { Algorithms.Iso7064Mod1271_36, Algorithms.Iso7064Mod1271_36.AlgorithmName, "U7Y8SXRC0O3SC4IHYQF4M44" };

      yield return new Object[] { Algorithms.Iso7064Mod37_2, Algorithms.Iso7064Mod37_2.AlgorithmName, "U7YZ" };
      yield return new Object[] { Algorithms.Iso7064Mod37_2, Algorithms.Iso7064Mod37_2.AlgorithmName, "U7Y8SXV" };
      yield return new Object[] { Algorithms.Iso7064Mod37_2, Algorithms.Iso7064Mod37_2.AlgorithmName, "U7Y8SXRC0E" };
      yield return new Object[] { Algorithms.Iso7064Mod37_2, Algorithms.Iso7064Mod37_2.AlgorithmName, "U7Y8SXRC0O3SU" };
      yield return new Object[] { Algorithms.Iso7064Mod37_2, Algorithms.Iso7064Mod37_2.AlgorithmName, "U7Y8SXRC0O3SC4IB" };
      yield return new Object[] { Algorithms.Iso7064Mod37_2, Algorithms.Iso7064Mod37_2.AlgorithmName, "U7Y8SXRC0O3SC4IHYQG" };
      yield return new Object[] { Algorithms.Iso7064Mod37_2, Algorithms.Iso7064Mod37_2.AlgorithmName, "U7Y8SXRC0O3SC4IHYQF4MF" };

      yield return new Object[] { Algorithms.Iso7064Mod37_36, Algorithms.Iso7064Mod37_36.AlgorithmName, "U7YW" };
      yield return new Object[] { Algorithms.Iso7064Mod37_36, Algorithms.Iso7064Mod37_36.AlgorithmName, "U7Y8SX8" };
      yield return new Object[] { Algorithms.Iso7064Mod37_36, Algorithms.Iso7064Mod37_36.AlgorithmName, "U7Y8SXRC0E" };
      yield return new Object[] { Algorithms.Iso7064Mod37_36, Algorithms.Iso7064Mod37_36.AlgorithmName, "U7Y8SXRC0O3SR" };
      yield return new Object[] { Algorithms.Iso7064Mod37_36, Algorithms.Iso7064Mod37_36.AlgorithmName, "U7Y8SXRC0O3SC4IT" };
      yield return new Object[] { Algorithms.Iso7064Mod37_36, Algorithms.Iso7064Mod37_36.AlgorithmName, "U7Y8SXRC0O3SC4IHYQD" };
      yield return new Object[] { Algorithms.Iso7064Mod37_36, Algorithms.Iso7064Mod37_36.AlgorithmName, "U7Y8SXRC0O3SC4IHYQF4MP" };

      yield return new Object[] { Algorithms.Ncd, Algorithms.Ncd.AlgorithmName, "11404/2h9m" };
      yield return new Object[] { Algorithms.Ncd, Algorithms.Ncd.AlgorithmName, "11404/2h9tqb0" };
      yield return new Object[] { Algorithms.Ncd, Algorithms.Ncd.AlgorithmName, "11404/2h9tqbxk6d" };
      yield return new Object[] { Algorithms.Ncd, Algorithms.Ncd.AlgorithmName, "11404/2h9tqbxk6rw74" };
      yield return new Object[] { Algorithms.Ncd, Algorithms.Ncd.AlgorithmName, "11404/2h9tqbxk6rw7dwmz" };
   }

   [Benchmark]
   [ArgumentsSource(nameof(TryCalculateCheckDigitArguments))]
   public void TryCalculateCheckDigit(ISingleCheckDigitAlgorithm algorithm, String name, String value)
   {
      algorithm.TryCalculateCheckDigit(value, out var checkDigit);
   }

   [Benchmark]
   [ArgumentsSource(nameof(TryCalculateCheckDigitsArguments))]
   public void TryCalculateCheckDigits(IDoubleCheckDigitAlgorithm algorithm, String name, String value)
   {
      algorithm.TryCalculateCheckDigits(value, out var first, out var second);
   }

   [Benchmark]
   [ArgumentsSource(nameof(ValidateArguments))]
   public void Validate(ICheckDigitAlgorithm algorithm, String name, String value)
   {
      algorithm.Validate(value);
   }
}
