// Ignore Spelling: Luhn

namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class LuhnAlgorithmValidateBenchmarks
{
   private static readonly LuhnAlgorithm _luhnAlgorithm = new();

   [Params("1230", "12345674", "123456789015", "1234567890123452")]
   public String Value { get; set; } = String.Empty;

   [Benchmark]
   public void Validate()
   {
      _ = _luhnAlgorithm.Validate(Value);
   }
}
