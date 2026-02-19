#pragma warning disable CS0618 // Type or member is obsolete

namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class OptimizeBenchmarks
{
   private static readonly Modulus11_27DecimalAlgorithm _algorithm = new();

   [Benchmark(Baseline = true)]
   [Arguments("1406")]
   //[Arguments("1406620")]
   //[Arguments("1406625385")]
   [Arguments("1406625380421")]
   //[Arguments("1406625380425510")]
   //[Arguments("1406625380425510288")]
   [Arguments("1406625380425510282650")]
   public void OriginalVersion(String value) => _ = _algorithm.Validate(value);

   //[Benchmark()]
   //[Arguments("1406")]
   //[Arguments("1406620")]
   //[Arguments("1406625385")]
   //[Arguments("1406625380421")]
   //[Arguments("1406625380425510")]
   //[Arguments("1406625380425510288")]
   //[Arguments("1406625380425510282650")]
   //public void UpdatedVersion(String value) => _ = _algorithm.Validate(value);

}