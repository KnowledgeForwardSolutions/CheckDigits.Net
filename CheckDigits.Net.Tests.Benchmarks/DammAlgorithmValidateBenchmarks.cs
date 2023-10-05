// Ignore Spelling: Damm

namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class DammAlgorithmValidateBenchmarks
{
   private static readonly DammAlgorithm _dammAlgorithm = new();

   [Params("1234", "12345671", "123456789018", "1234567890123450")]
   public String Value { get; set; } = String.Empty;

   [Benchmark]
   public void Validate()
   {
      _ = _dammAlgorithm.Validate(Value);
   }
}
