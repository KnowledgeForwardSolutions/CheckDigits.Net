namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class Iso7064Mod37_2AlgorithmTryCalculateBenchmarks
{
   private static readonly Iso7064Mod37_2Algorithm _algorithm = new();

   [Params("ZZZZ", "A999914123456", "A999522123456", "ABCDEFGHIJKLMNOPQRSTUVWX")]
   public String Value { get; set; } = String.Empty;

   [Benchmark]
   public void TryCalculateCheckDigit()
   {
      _ = _algorithm.TryCalculateCheckDigit(Value, out var checkDigit);
   }
}
