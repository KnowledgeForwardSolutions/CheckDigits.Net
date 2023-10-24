using CheckDigits.Net.ValueSpecificAlgorithms;

namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class VinAlgorithmTryCalculateBenchmarks
{
   private static readonly VinAlgorithm _algorithm = new();

   [Params("1M8GDM9AXKP042788", "1G8ZG127XWZ157259", "1HGEM21292L047875")]
   public String Value { get; set; } = String.Empty;

   [Benchmark]
   public void TryCalculateCheckDigit()
   {
      _ = _algorithm.TryCalculateCheckDigit(Value, out var checkDigit);
   }
}
