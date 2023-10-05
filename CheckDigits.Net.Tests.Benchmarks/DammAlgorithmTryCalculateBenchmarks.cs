// Ignore Spelling: Damm

namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class DammAlgorithmTryCalculateBenchmarks
{
   private static readonly DammAlgorithm _algorithm = new();

   [Params("123", "1234567", "12345678901", "123456789012345")]
   public String Value { get; set; } = String.Empty;

   [Benchmark]
   public void TryCalculateCheckDigit()
   {
      _ = _algorithm.TryCalculateCheckDigit(Value, out var checkDigit);
   }
}
