// Ignore Spelling: Isin

using CheckDigits.Net.ValueSpecificAlgorithms;

namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class IsinAlgorithmTryCalculateBenchmarks
{
   private static readonly IsinAlgorithm _algorithm = new();

   [Params("US037833100", "AU0000XVGZA", "US88160R101")]
   public String Value { get; set; } = String.Empty;

   [Benchmark]
   public void TryCalculateCheckDigit()
   {
      _ = _algorithm.TryCalculateCheckDigit(Value, out var checkDigit);
   }
}
