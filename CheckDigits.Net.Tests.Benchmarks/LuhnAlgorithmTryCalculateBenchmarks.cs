// Ignore Spelling: Luhn

namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class LuhnAlgorithmTryCalculateBenchmarks
{
   private static readonly LuhnAlgorithm _luhnAlgorithm = new();

   [Params("123", "1234567", "12345678901", "123456789012345")]
   public String Value { get; set; } = String.Empty;

   [Benchmark]
   public void TryCalculateCheckDigit()
   {
      _ = _luhnAlgorithm.TryCalculateCheckDigit(Value, out var checkDigit);
   }
}
