// Ignore Spelling: Verhoeff

namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class VerhoeffAlgorithmValidateBenchmarks
{
   private static readonly VerhoeffAlgorithm _verhoeffAlgorithm = new();

   [Params("1233", "12345679", "123456789010", "1234567890123455")]
   public String Value { get; set; } = String.Empty;

   [Benchmark]
   public void Validate()
   {
      _ = _verhoeffAlgorithm.Validate(Value);
   }
}
