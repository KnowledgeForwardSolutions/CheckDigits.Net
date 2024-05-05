using CheckDigits.Net.GeneralAlgorithms;

namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class OptimizeBenchmarks
{
   private static readonly AlphanumericMod97_10Algorithm _algorithm = new();

   [Params("U7y46", "U7y8SX89", "U7y8SXrC087", "U7y8SXrC0O3S38", "U7y8SXrC0O3Sc4I27", "U7y8SXrC0O3Sc4IHYQ54", "U7y8SXrC0O3Sc4IHYQF4M21")]
   public String Value { get; set; } = String.Empty;

[Benchmark]
   public void Startup() => _algorithm.Validate(Value);

   [Benchmark(Baseline = true)]
   public void Validate() => _ = _algorithm.Validate(Value);

   [Benchmark]
   public void Validate2() => _ = _algorithm.Validate2(Value);

   //[Benchmark]
   //public void Validate3() => _ = _algorithm.Validate3(Value);
}
