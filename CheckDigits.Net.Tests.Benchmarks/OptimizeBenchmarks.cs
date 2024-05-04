﻿namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class OptimizeBenchmarks
{
   private static readonly FigiAlgorithm _algorithm = new();

   [Params("BBG000B9Y5X2", "BBG111111160", "BBGZYXWVTSR7")]
   public String Value { get; set; } = String.Empty;

   [Benchmark]
   public void Startup() => _algorithm.Validate(Value);

   [Benchmark(Baseline = true)]
   public void Validate() => _ = _algorithm.Validate(Value);

   //[Benchmark]
   //public void Validate2() => _ = _algorithm.Validate2(Value);

   //[Benchmark]
   //public void Validate3() => _ = _algorithm.Validate3(Value);
}
