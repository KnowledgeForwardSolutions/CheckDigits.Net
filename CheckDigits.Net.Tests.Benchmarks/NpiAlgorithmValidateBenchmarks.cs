// Ignore Spelling: Npi

using CheckDigits.Net.ValueSpecificAlgorithms;

namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class NpiAlgorithmValidateBenchmarks
{
   private static readonly NpiAlgorithm _algorithm = new();

   [Params("1234567893", "1245319599")]
   public String Value { get; set; } = String.Empty;

   [Benchmark]
   public void Validate()
   {
      _ = _algorithm.Validate(Value);
   }
}
