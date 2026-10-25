// Ignore Spelling: Damm Quasigroup

#pragma warning disable CS0618 // Type or member is obsolete

namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class OptimizeBenchmarks
{
   //private static readonly AlphanumericMod97_10Algorithm _algorithm = new();

   //[Benchmark(Baseline = true)]
   //[Arguments("U7y46")]
   //[Arguments("U7y8SX89")]
   //[Arguments("U7y8SXrC087")]
   //[Arguments("U7y8SXrC0O3S38")]
   //[Arguments("U7y8SXrC0O3Sc4I27")]
   //[Arguments("U7y8SXrC0O3Sc4IHYQ54")]
   //[Arguments("U7y8SXrC0O3Sc4IHYQF4M21")]
   //public void Original(String value) => _ = _algorithm.Validate(value);

   //[Benchmark()]
   //[Arguments("U7y46")]
   //[Arguments("U7y8SX89")]
   //[Arguments("U7y8SXrC087")]
   //[Arguments("U7y8SXrC0O3S38")]
   //[Arguments("U7y8SXrC0O3Sc4I27")]
   //[Arguments("U7y8SXrC0O3Sc4IHYQ54")]
   //[Arguments("U7y8SXrC0O3Sc4IHYQF4M21")]
   //public void LongInteger(String value) => _ = _algorithm.Validate(value);

   [Benchmark(Baseline = true)]
   [Arguments("140")]
   [Arguments("140662")]
   [Arguments("140662538")]
   [Arguments("140662538042")]
   [Arguments("140662538042551")]
   [Arguments("140662538042551028")]
   [Arguments("140662538042551028265")]
   public void MapFirst(String value)
   {
      for (var index = 0; index < value.Length; index++)
      {
         var num = value[index].MapAsciiDigit();
         if (num.IsInvalidDigit())
         {
            return;
         }
      }
   }

   [Benchmark]
   [Arguments("140")]
   [Arguments("140662")]
   [Arguments("140662538")]
   [Arguments("140662538042")]
   [Arguments("140662538042551")]
   [Arguments("140662538042551028")]
   [Arguments("140662538042551028265")]
   public void ValidateFirst(String value)
   {
      for (var index = 0; index < value.Length; index++)
      {
         var ch = value[index];
         if (!ch.IsAsciiDigit())
         {
            return;
         }

         var num = ch.MapAsciiDigit();
      }
   }

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
