// Ignore Spelling: Npi

namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class NpiAlgorithmTryCalculateBenchmarks
{
   private static readonly NpiAlgorithm _algorithm = new();

   [Params("123456789", "124531959")]
   public String Value { get; set; } = String.Empty;

   [Benchmark]
   public void TryCalculateCheckDigit()
   {
      _ = _algorithm.TryCalculateCheckDigit(Value, out var checkDigit);
   }
}
