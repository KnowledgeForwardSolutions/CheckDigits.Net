// Ignore Spelling: Damm Quasigroup

#pragma warning disable CS0618 // Type or member is obsolete

namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class OptimizeBenchmarks
{
   private static readonly AlphanumericMod97_10Algorithm _algorithm = new();

   [Benchmark(Baseline = true)]
   [Arguments("U7y46")]
   [Arguments("U7y8SX89")]
   [Arguments("U7y8SXrC087")]
   [Arguments("U7y8SXrC0O3S38")]
   [Arguments("U7y8SXrC0O3Sc4I27")]
   [Arguments("U7y8SXrC0O3Sc4IHYQ54")]
   [Arguments("U7y8SXrC0O3Sc4IHYQF4M21")]
   public void Original(String value) => _ = _algorithm.Validate(value);

   //[Benchmark()]
   //[Arguments("U7y46")]
   //[Arguments("U7y8SX89")]
   //[Arguments("U7y8SXrC087")]
   //[Arguments("U7y8SXrC0O3S38")]
   //[Arguments("U7y8SXrC0O3Sc4I27")]
   //[Arguments("U7y8SXrC0O3Sc4IHYQ54")]
   //[Arguments("U7y8SXrC0O3Sc4IHYQF4M21")]
   //public void LongInteger(String value) => _ = _algorithm.Validate(value);
}
