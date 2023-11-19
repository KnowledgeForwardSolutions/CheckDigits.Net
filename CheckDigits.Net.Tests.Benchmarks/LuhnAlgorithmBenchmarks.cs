// Ignore Spelling: Luhn Inline

namespace CheckDigits.Net.Tests.Benchmarks;

[SimpleJob(RuntimeMoniker.Net70, baseline: true)]
[SimpleJob(RuntimeMoniker.Net80)]
[MemoryDiagnoser]
public class LuhnAlgorithmBenchmarks
{
   private readonly LuhnAlgorithm _algorithm = new();

   [Params("1404", "1406628", "1406625382", "1406625380421", "1406625380425514", "1406625380425510285", "1406625380425510282651")]
   public String Value { get; set; } = default!;

   [Benchmark]
   public void Validate()
   {
      _ = _algorithm.Validate(Value);
   }
}
