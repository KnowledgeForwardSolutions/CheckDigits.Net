// Ignore Spelling: Damm Quasigroup

#pragma warning disable CS0618 // Type or member is obsolete

namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class OptimizeBenchmarks
{
   //private static readonly Modulus11_27DecimalAlgorithm _algorithm = new();

   //[Benchmark(Baseline = true)]
   //[Arguments("1406")]
   //[Arguments("1406620")]
   //[Arguments("1406625385")]
   //[Arguments("1406625380421")]
   //[Arguments("1406625380425510")]
   //[Arguments("1406625380425510288")]
   //[Arguments("1406625380425510282650")]
   //public void Original(String value) => _ = _algorithm.Validate(value);

   //[Benchmark()]
   //[Arguments("1406")]
   //[Arguments("1406620")]
   //[Arguments("1406625385")]
   //[Arguments("1406625380421")]
   //[Arguments("1406625380425510")]
   //[Arguments("1406625380425510288")]
   //[Arguments("1406625380425510282650")]
   //public void Rollover(String value) => _ = _algorithm.Validate2(value);

   private static readonly AbaRtnAlgorithm _algorithm = new();

   [Benchmark(Baseline = true)]
   [Arguments("111000025")]
   [Arguments("122235821")]
   [Arguments("325081403")]
   [Arguments("325070760")]
   [Arguments("325272021")]
   public void Original(String value) => _ = _algorithm.Validate(value);

   [Benchmark()]
   [Arguments("111000025")]
   [Arguments("122235821")]
   [Arguments("325081403")]
   [Arguments("325070760")]
   [Arguments("325272021")]
   public void Rollover(String value) => _ = _algorithm.Validate2(value);

   //[Benchmark(Baseline = true)]
   //[Arguments("140")]
   //[Arguments("140662")]
   //[Arguments("140662538")]
   //[Arguments("140662538042")]
   //[Arguments("140662538042551")]
   //[Arguments("140662538042551028")]
   //[Arguments("140662538042551028265")]
   //public void MapFirst(String value)
   //{
   //   for (var index = 0; index < value.Length; index++)
   //   {
   //      var num = value[index].MapAsciiDigit();
   //      if (num.IsInvalidDigit())
   //      {
   //         return;
   //      }
   //   }
   //}

   //[Benchmark]
   //[Arguments("140")]
   //[Arguments("140662")]
   //[Arguments("140662538")]
   //[Arguments("140662538042")]
   //[Arguments("140662538042551")]
   //[Arguments("140662538042551028")]
   //[Arguments("140662538042551028265")]
   //public void ValidateFirst(String value)
   //{
   //   for (var index = 0; index < value.Length; index++)
   //   {
   //      var ch = value[index];
   //      if (!ch.IsAsciiDigit())
   //      {
   //         return;
   //      }

   //      var num = ch.MapAsciiDigit();
   //   }
   //}

   //[Benchmark]
   //[Arguments("140")]
   //[Arguments("140662")]
   //[Arguments("140662538")]
   //[Arguments("140662538042")]
   //[Arguments("140662538042551")]
   //[Arguments("140662538042551028")]
   //[Arguments("140662538042551028265")]
   //public void MapWithMinusOne(String value)
   //{
   //   for (var index = 0; index < value.Length; index++)
   //   {
   //      var num = value[index].MapAsciiDigit2();
   //      if (num == -1)
   //      {
   //         return;
   //      }
   //   }
   //}
}
