namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class Iso7064Mod27_26AlgorithmTryCalculateBenchmarks
{
   private static readonly Iso7064Mod27_26Algorithm _algorithm = new();

   [Params("AEIOU", "QWERTYDVORAK", "ABCDEFGHIJKLMNOPQR", "ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ")]
   public String Value { get; set; } = String.Empty;

   [Benchmark]
   public void TryCalculateCheckDigit()
   {
      _ = _algorithm.TryCalculateCheckDigit(Value, out var checkDigit);
   }
}
