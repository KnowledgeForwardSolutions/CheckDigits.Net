namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class Modulus10_13AlgorithmTryCalculateBenchmarks
{
   private static readonly Modulus10_13Algorithm _modulus10_13Algorithm = new();

   [Params("42526", "7351353", "03600029145", "400638133393", "01234567800004567")]
   public String Value { get; set; } = String.Empty;

   [Benchmark]
   public void TryCalculateCheckDigit()
   {
      _ = _modulus10_13Algorithm.TryCalculateCheckDigit(Value, out var checkDigit);
   }
}
