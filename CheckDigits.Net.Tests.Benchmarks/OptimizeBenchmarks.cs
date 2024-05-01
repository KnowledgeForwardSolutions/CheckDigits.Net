using CheckDigits.Net.ValueSpecificAlgorithms;

namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class OptimizeBenchmarks
{
   private static readonly FigiAlgorithm _algorithm = (FigiAlgorithm)Algorithms.Figi;

   [Params("BBG000B9Y5X2", "BBG111111160", "BBGZYXWVTSR7")]
   public String Value { get; set; }

   [Benchmark(Baseline = true)]
   public void Validate() => _ = _algorithm.Validate(Value);

   //[Benchmark]
   //public void Validate2() => _ = _algorithm.Validate2(Value);

   //[Benchmark]
   //public void Validate3() => _ = _algorithm.Validate3(Value);
}
