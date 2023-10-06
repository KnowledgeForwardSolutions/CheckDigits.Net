// Ignore Spelling: Aba Rtn

namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class AbaRtnAlgorithmTryCalculateBenchmarks
{
   private static readonly AbaRtnAlgorithm _algorithm = new();

   [Params("11100002", "12223582")]
   public String Value { get; set; } = String.Empty;

   [Benchmark]
   public void TryCalculateCheckDigit()
   {
      _ = _algorithm.TryCalculateCheckDigit(Value, out var checkDigit);
   }
}
