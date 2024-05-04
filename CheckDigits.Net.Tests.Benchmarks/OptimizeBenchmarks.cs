namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class OptimizeBenchmarks
{
   private static readonly IsinAlgorithm _algorithm = new();

   [Params("US0378331005", "AU0000XVGZA3", "GB0002634946")]
   public String Value { get; set; } = String.Empty;
   //yield return new Object[] { Algorithms.Isin, Algorithms.Isin.AlgorithmName, "US0378331005" };
   //yield return new Object[] { Algorithms.Isin, Algorithms.Isin.AlgorithmName, "AU0000XVGZA3" };
   //yield return new Object[] { Algorithms.Isin, Algorithms.Isin.AlgorithmName, "GB0002634946" };

   [Benchmark]
   public void Startup() => _algorithm.Validate(Value);

   [Benchmark(Baseline = true)]
   public void Validate() => _ = _algorithm.Validate(Value);

   //[Benchmark]
   //public void Validate2() => _ = _algorithm.Validate2(Value);

   //[Benchmark]
   //public void Validate3() => _ = _algorithm.Validate3(Value);
}
