namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class Modulus11AlgorithmTryCalculateBenchmarks
{
   private static readonly Modulus11Algorithm _algorithm = new();

   [Params("123", "0317847", "050027293")]
   public String Value { get; set; } = String.Empty;

   [Benchmark]
   public void TryCalculateCheckDigit()
   {
      _ = _algorithm.TryCalculateCheckDigit(Value, out var checkDigit);
   }
}
