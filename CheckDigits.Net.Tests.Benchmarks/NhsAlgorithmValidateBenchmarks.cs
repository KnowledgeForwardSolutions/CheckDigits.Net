// Ignore Spelling: Nhs

namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class NhsAlgorithmValidateBenchmarks
{
   private static readonly NhsAlgorithm _algorithm = new();

   [Params("9434765919", "8514468243", "3967487881")]
   public String Value { get; set; } = String.Empty;

   [Benchmark]
   public void Validate()
   {
      _ = _algorithm.Validate(Value);
   }
}
