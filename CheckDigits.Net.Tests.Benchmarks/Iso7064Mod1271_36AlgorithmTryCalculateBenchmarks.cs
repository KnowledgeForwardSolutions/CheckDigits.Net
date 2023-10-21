using Microsoft.VisualBasic;

namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class Iso7064Mod1271_36AlgorithmTryCalculateBenchmarks
{
   private static readonly Iso7064Mod1271_36Algorithm _algorithm = new();

   [Params("ISO79", "XS868977863229", "AEIOU1592430QWERTY", "ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ")]
   public String Value { get; set; } = String.Empty;

   [Benchmark]
   public void TryCalculateCheckDigit()
   {
      _ = _algorithm.TryCalculateCheckDigit(Value, out var first, out var second);
   }
}
