// Ignore Spelling: Isin

namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class IsinAlgorithmValidateBenchmarks
{
   private static readonly IsinAlgorithm _algorithm = new();

   [Params("US0378331005", "AU0000XVGZA3", "US88160R1014")]
   public String Value { get; set; } = String.Empty;

   [Benchmark]
   public void Validate()
   {
      _ = _algorithm.Validate(Value);
   }
}
