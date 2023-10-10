namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class Modulus10_2AlgorithmTryCalculateBenchmarks
{
   private static readonly Modulus10_2Algorithm _algorithm = new();

   [Params("907472", "970779", "101056")]
   public String Value { get; set; } = String.Empty;

   [Benchmark]
   public void TryCalculateCheckDigit()
   {
      _ = _algorithm.TryCalculateCheckDigit(Value, out var checkDigit);
   }
}
