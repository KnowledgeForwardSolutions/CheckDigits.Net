﻿namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class AlphanumericAlgorithmBenchmarks
{
   public IEnumerable<Object[]> TryCalculateCheckDigitArguments()
   {
      yield return new Object[] { Algorithms.Iso7064Mod37_2, Algorithms.Iso7064Mod37_2.AlgorithmName, "EGR" };
      yield return new Object[] { Algorithms.Iso7064Mod37_2, Algorithms.Iso7064Mod37_2.AlgorithmName, "EGRNML" };
      yield return new Object[] { Algorithms.Iso7064Mod37_2, Algorithms.Iso7064Mod37_2.AlgorithmName, "EGRNMLJOC" };
      yield return new Object[] { Algorithms.Iso7064Mod37_2, Algorithms.Iso7064Mod37_2.AlgorithmName, "EGRNMLJOCECU" };
      yield return new Object[] { Algorithms.Iso7064Mod37_2, Algorithms.Iso7064Mod37_2.AlgorithmName, "EGRNMLJOCECUJIK" };
      yield return new Object[] { Algorithms.Iso7064Mod37_2, Algorithms.Iso7064Mod37_2.AlgorithmName, "EGRNMLJOCECUJIKNWW" };
      yield return new Object[] { Algorithms.Iso7064Mod37_2, Algorithms.Iso7064Mod37_2.AlgorithmName, "EGRNMLJOCECUJIKNWWVVO" };

      yield return new Object[] { Algorithms.Iso7064Mod37_36, Algorithms.Iso7064Mod37_36.AlgorithmName, "EGR" };
      yield return new Object[] { Algorithms.Iso7064Mod37_36, Algorithms.Iso7064Mod37_36.AlgorithmName, "EGRNML" };
      yield return new Object[] { Algorithms.Iso7064Mod37_36, Algorithms.Iso7064Mod37_36.AlgorithmName, "EGRNMLJOC" };
      yield return new Object[] { Algorithms.Iso7064Mod37_36, Algorithms.Iso7064Mod37_36.AlgorithmName, "EGRNMLJOCECU" };
      yield return new Object[] { Algorithms.Iso7064Mod37_36, Algorithms.Iso7064Mod37_36.AlgorithmName, "EGRNMLJOCECUJIK" };
      yield return new Object[] { Algorithms.Iso7064Mod37_36, Algorithms.Iso7064Mod37_36.AlgorithmName, "EGRNMLJOCECUJIKNWW" };
      yield return new Object[] { Algorithms.Iso7064Mod37_36, Algorithms.Iso7064Mod37_36.AlgorithmName, "EGRNMLJOCECUJIKNWWVVO" };
   }

   public IEnumerable<Object[]> TryCalculateCheckDigitsArguments()
   {
      yield return new Object[] { Algorithms.Iso7064Mod1271_36, Algorithms.Iso7064Mod1271_36.AlgorithmName, "EGR" };
      yield return new Object[] { Algorithms.Iso7064Mod1271_36, Algorithms.Iso7064Mod1271_36.AlgorithmName, "EGRNML" };
      yield return new Object[] { Algorithms.Iso7064Mod1271_36, Algorithms.Iso7064Mod1271_36.AlgorithmName, "EGRNMLJOC" };
      yield return new Object[] { Algorithms.Iso7064Mod1271_36, Algorithms.Iso7064Mod1271_36.AlgorithmName, "EGRNMLJOCECU" };
      yield return new Object[] { Algorithms.Iso7064Mod1271_36, Algorithms.Iso7064Mod1271_36.AlgorithmName, "EGRNMLJOCECUJIK" };
      yield return new Object[] { Algorithms.Iso7064Mod1271_36, Algorithms.Iso7064Mod1271_36.AlgorithmName, "EGRNMLJOCECUJIKNWW" };
      yield return new Object[] { Algorithms.Iso7064Mod1271_36, Algorithms.Iso7064Mod1271_36.AlgorithmName, "EGRNMLJOCECUJIKNWWVVO" };
   }

   public IEnumerable<Object[]> ValidateArguments()
   {
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

   //[Benchmark]
   //[ArgumentsSource(nameof(ValidateArguments))]
   //public void Validate(ICheckDigitAlgorithm algorithm, String name, String value)
   //{
   //   algorithm.Validate(value);
   //}
}