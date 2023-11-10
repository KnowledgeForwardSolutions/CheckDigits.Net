namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class AlphanumericAlgorithmBenchmarks
{
   private static readonly Iso7064Mod1271_36Algorithm _sut = new();

   public IEnumerable<Object[]> TryCalculateCheckDigitArguments()
   {
      yield return new Object[] { Algorithms.Iso7064Mod37_2, Algorithms.Iso7064Mod37_2.AlgorithmName, "K1M" };
      yield return new Object[] { Algorithms.Iso7064Mod37_2, Algorithms.Iso7064Mod37_2.AlgorithmName, "K1MEL3" };
      yield return new Object[] { Algorithms.Iso7064Mod37_2, Algorithms.Iso7064Mod37_2.AlgorithmName, "K1MEL3765" };
      yield return new Object[] { Algorithms.Iso7064Mod37_2, Algorithms.Iso7064Mod37_2.AlgorithmName, "K1MEL37655H2" };
      yield return new Object[] { Algorithms.Iso7064Mod37_2, Algorithms.Iso7064Mod37_2.AlgorithmName, "K1MEL37655H24ED" };
      yield return new Object[] { Algorithms.Iso7064Mod37_2, Algorithms.Iso7064Mod37_2.AlgorithmName, "K1MEL37655H24EDKCA" };
      yield return new Object[] { Algorithms.Iso7064Mod37_2, Algorithms.Iso7064Mod37_2.AlgorithmName, "K1MEL37655H24EDKCA69I" };

      yield return new Object[] { Algorithms.Iso7064Mod37_36, Algorithms.Iso7064Mod37_36.AlgorithmName, "K1M" };
      yield return new Object[] { Algorithms.Iso7064Mod37_36, Algorithms.Iso7064Mod37_36.AlgorithmName, "K1MEL3" };
      yield return new Object[] { Algorithms.Iso7064Mod37_36, Algorithms.Iso7064Mod37_36.AlgorithmName, "K1MEL3765" };
      yield return new Object[] { Algorithms.Iso7064Mod37_36, Algorithms.Iso7064Mod37_36.AlgorithmName, "K1MEL37655H2" };
      yield return new Object[] { Algorithms.Iso7064Mod37_36, Algorithms.Iso7064Mod37_36.AlgorithmName, "K1MEL37655H24ED" };
      yield return new Object[] { Algorithms.Iso7064Mod37_36, Algorithms.Iso7064Mod37_36.AlgorithmName, "K1MEL37655H24EDKCA" };
      yield return new Object[] { Algorithms.Iso7064Mod37_36, Algorithms.Iso7064Mod37_36.AlgorithmName, "K1MEL37655H24EDKCA69I" };
   }

   public IEnumerable<Object[]> TryCalculateCheckDigitsArguments()
   {
      yield return new Object[] { Algorithms.AlphanumericMod97_10, Algorithms.AlphanumericMod97_10.AlgorithmName, "K1M" };
      yield return new Object[] { Algorithms.AlphanumericMod97_10, Algorithms.AlphanumericMod97_10.AlgorithmName, "K1MEL3" };
      yield return new Object[] { Algorithms.AlphanumericMod97_10, Algorithms.AlphanumericMod97_10.AlgorithmName, "K1MEL3765" };
      yield return new Object[] { Algorithms.AlphanumericMod97_10, Algorithms.AlphanumericMod97_10.AlgorithmName, "K1MEL37655H2" };
      yield return new Object[] { Algorithms.AlphanumericMod97_10, Algorithms.AlphanumericMod97_10.AlgorithmName, "K1MEL37655H24ED" };
      yield return new Object[] { Algorithms.AlphanumericMod97_10, Algorithms.AlphanumericMod97_10.AlgorithmName, "K1MEL37655H24EDKCA" };
      yield return new Object[] { Algorithms.AlphanumericMod97_10, Algorithms.AlphanumericMod97_10.AlgorithmName, "K1MEL37655H24EDKCA69I" };

      yield return new Object[] { Algorithms.Iso7064Mod1271_36, Algorithms.Iso7064Mod1271_36.AlgorithmName, "K1M" };
      yield return new Object[] { Algorithms.Iso7064Mod1271_36, Algorithms.Iso7064Mod1271_36.AlgorithmName, "K1MEL3" };
      yield return new Object[] { Algorithms.Iso7064Mod1271_36, Algorithms.Iso7064Mod1271_36.AlgorithmName, "K1MEL3765" };
      yield return new Object[] { Algorithms.Iso7064Mod1271_36, Algorithms.Iso7064Mod1271_36.AlgorithmName, "K1MEL37655H2" };
      yield return new Object[] { Algorithms.Iso7064Mod1271_36, Algorithms.Iso7064Mod1271_36.AlgorithmName, "K1MEL37655H24ED" };
      yield return new Object[] { Algorithms.Iso7064Mod1271_36, Algorithms.Iso7064Mod1271_36.AlgorithmName, "K1MEL37655H24EDKCA" };
      yield return new Object[] { Algorithms.Iso7064Mod1271_36, Algorithms.Iso7064Mod1271_36.AlgorithmName, "K1MEL37655H24EDKCA69I" };
   }

   public IEnumerable<Object[]> ValidateArguments()
   {
      yield return new Object[] { Algorithms.AlphanumericMod97_10, Algorithms.AlphanumericMod97_10.AlgorithmName, "K1M66" };
      yield return new Object[] { Algorithms.AlphanumericMod97_10, Algorithms.AlphanumericMod97_10.AlgorithmName, "K1MEL372" };
      yield return new Object[] { Algorithms.AlphanumericMod97_10, Algorithms.AlphanumericMod97_10.AlgorithmName, "K1MEL376530" };
      yield return new Object[] { Algorithms.AlphanumericMod97_10, Algorithms.AlphanumericMod97_10.AlgorithmName, "K1MEL37655H272" };
      yield return new Object[] { Algorithms.AlphanumericMod97_10, Algorithms.AlphanumericMod97_10.AlgorithmName, "K1MEL37655H24ED07" };
      yield return new Object[] { Algorithms.AlphanumericMod97_10, Algorithms.AlphanumericMod97_10.AlgorithmName, "K1MEL37655H24EDKCA67" };
      yield return new Object[] { Algorithms.AlphanumericMod97_10, Algorithms.AlphanumericMod97_10.AlgorithmName, "K1MEL37655H24EDKCA69I17" };

      yield return new Object[] { Algorithms.Iso7064Mod1271_36, Algorithms.Iso7064Mod1271_36.AlgorithmName, "K1M0W" };
      yield return new Object[] { Algorithms.Iso7064Mod1271_36, Algorithms.Iso7064Mod1271_36.AlgorithmName, "K1MEL34W" };
      yield return new Object[] { Algorithms.Iso7064Mod1271_36, Algorithms.Iso7064Mod1271_36.AlgorithmName, "K1MEL37654L" };
      yield return new Object[] { Algorithms.Iso7064Mod1271_36, Algorithms.Iso7064Mod1271_36.AlgorithmName, "K1MEL37655H2KZ" };
      yield return new Object[] { Algorithms.Iso7064Mod1271_36, Algorithms.Iso7064Mod1271_36.AlgorithmName, "K1MEL37655H24EDRD" };
      yield return new Object[] { Algorithms.Iso7064Mod1271_36, Algorithms.Iso7064Mod1271_36.AlgorithmName, "K1MEL37655H24EDKCA8P" };
      yield return new Object[] { Algorithms.Iso7064Mod1271_36, Algorithms.Iso7064Mod1271_36.AlgorithmName, "K1MEL37655H24EDKCA69I8W" };

      yield return new Object[] { Algorithms.Iso7064Mod37_2, Algorithms.Iso7064Mod37_2.AlgorithmName, "K1MF" };
      yield return new Object[] { Algorithms.Iso7064Mod37_2, Algorithms.Iso7064Mod37_2.AlgorithmName, "K1MEL3M" };
      yield return new Object[] { Algorithms.Iso7064Mod37_2, Algorithms.Iso7064Mod37_2.AlgorithmName, "K1MEL37655" };
      yield return new Object[] { Algorithms.Iso7064Mod37_2, Algorithms.Iso7064Mod37_2.AlgorithmName, "K1MEL37655H2W" };
      yield return new Object[] { Algorithms.Iso7064Mod37_2, Algorithms.Iso7064Mod37_2.AlgorithmName, "K1MEL37655H24EDO" };
      yield return new Object[] { Algorithms.Iso7064Mod37_2, Algorithms.Iso7064Mod37_2.AlgorithmName, "K1MEL37655H24EDKCAV" };
      yield return new Object[] { Algorithms.Iso7064Mod37_2, Algorithms.Iso7064Mod37_2.AlgorithmName, "K1MEL37655H24EDKCA69IA" };

      yield return new Object[] { Algorithms.Iso7064Mod37_36, Algorithms.Iso7064Mod37_36.AlgorithmName, "K1ME" };
      yield return new Object[] { Algorithms.Iso7064Mod37_36, Algorithms.Iso7064Mod37_36.AlgorithmName, "K1MEL3D" };
      yield return new Object[] { Algorithms.Iso7064Mod37_36, Algorithms.Iso7064Mod37_36.AlgorithmName, "K1MEL3765E" };
      yield return new Object[] { Algorithms.Iso7064Mod37_36, Algorithms.Iso7064Mod37_36.AlgorithmName, "K1MEL37655H2Z" };
      yield return new Object[] { Algorithms.Iso7064Mod37_36, Algorithms.Iso7064Mod37_36.AlgorithmName, "K1MEL37655H24EDI" };
      yield return new Object[] { Algorithms.Iso7064Mod37_36, Algorithms.Iso7064Mod37_36.AlgorithmName, "K1MEL37655H24EDKCAH" };
      yield return new Object[] { Algorithms.Iso7064Mod37_36, Algorithms.Iso7064Mod37_36.AlgorithmName, "K1MEL37655H24EDKCA69IG" };
   }

   //[Benchmark]
   //[ArgumentsSource(nameof(TryCalculateCheckDigitArguments))]
   //public void TryCalculateCheckDigit(ISingleCheckDigitAlgorithm algorithm, String name, String value)
   //{
   //   algorithm.TryCalculateCheckDigit(value, out var checkDigit);
   //}

   //[Benchmark]
   //[ArgumentsSource(nameof(TryCalculateCheckDigitsArguments))]
   //public void TryCalculateCheckDigits(IDoubleCheckDigitAlgorithm algorithm, String name, String value)
   //{
   //   algorithm.TryCalculateCheckDigits(value, out var first, out var second);
   //}

   //[Benchmark]
   //[ArgumentsSource(nameof(ValidateArguments))]
   //public void Validate(ICheckDigitAlgorithm algorithm, String name, String value)
   //{
   //   algorithm.Validate(value);
   //}

   [Params("K1M0W", "K1MEL34W", "K1MEL37654L", "K1MEL37655H2KZ", "K1MEL37655H24EDRD", "K1MEL37655H24EDKCA8P", "K1MEL37655H24EDKCA69I8W")]
   //[Params("K1M", "K1MEL3", "K1MEL3765", "K1MEL37655H2", "K1MEL37655H24ED", "K1MEL37655H24EDKCA", "K1MEL37655H24EDKCA69I")]
   public String Value { get; set; } = default!;

   //[Benchmark(Baseline = true)]
   //public void Baseline()
   //{
   //   _ = _sut.TryCalculateCheckDigits(Value, out var first, out var second);
   //}

   //[Benchmark]
   //public void TryCalculateCheckDigits2()
   //{
   //   _ = _sut.TryCalculateCheckDigits2(Value, out var first, out var second);
   //}

   //[Benchmark(Baseline = true)]
   //public void Baseline()
   //{
   //   _ = _sut.Validate(Value);
   //}

   //[Benchmark]
   //public void Validate2()
   //{
   //   _ = _sut.Validate2(Value);
   //}

   //[Benchmark]
   //public void Validate3()
   //{
   //   _ = _sut.Validate3(Value);
   //}
}
