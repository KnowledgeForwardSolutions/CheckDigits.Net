namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class Modulus10_1AlgorithmTryCalculateBenchmarks
{
   private static readonly Modulus10_1Algorithm _algorithm = new();

   [Params("5808", "773218", "2872855")]
   public String Value { get; set; } = String.Empty;

   [Benchmark]
   public void TryCalculateCheckDigit()
   {
      _ = _algorithm.TryCalculateCheckDigit(Value, out var checkDigit);
   }
}
