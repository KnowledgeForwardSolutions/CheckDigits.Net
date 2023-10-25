// Ignore Spelling: Luhn

namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class LuhnAlgorithmValidateBenchmarks
{
   private static readonly LuhnAlgorithm _algorithm = new();

   [Params("1404", "1406628", "1406625382", "1406625380421", "1406625380425514", "1406625380425510285", "1406625380425510282651")]
   public String Value { get; set; } = String.Empty;

   [Benchmark]
   public void Validate()
   {
      _ = _algorithm.Validate(Value);
   }
}
