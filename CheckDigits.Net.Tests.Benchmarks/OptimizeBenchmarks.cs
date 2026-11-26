// Ignore Spelling: Damm Quasigroup

#pragma warning disable CS0618 // Type or member is obsolete

namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class OptimizeBenchmarks
{
   private static readonly Icao9303SizeTD1Algorithm _algorithm = new();

   [Benchmark(Baseline = true)]
   [Arguments("I<UTOD231458907<<<<<<<<<<<<<<<7408122F1204159UTO<<<<<<<<<<<6ERIKSSON<<ANNA<MARIA<<<<<<<<A<")]
   [Arguments("I<UTOD23145890<AB112234566<<<<\r\n7408122F1204159UTO<<<<<<<<<<<4\r\nERIKSSON<<ANNA<MARIA<<<<<<<<B<")]
   [Arguments("I<UTOSTARWARS45<<<<<<<<<<<<<<<\n7705256M2405252UTO<<<<<<<<<<<4\nSKYWALKER<<LUKE<<<<<<<<<<<<<C<")]
   public void Original(String value) => _ = _algorithm.Validate(value);

   [Benchmark()]
   [Arguments("I<UTOD231458907<<<<<<<<<<<<<<<7408122F1204159UTO<<<<<<<<<<<6ERIKSSON<<ANNA<MARIA<<<<<<<<A<")]
   [Arguments("I<UTOD23145890<AB112234566<<<<\r\n7408122F1204159UTO<<<<<<<<<<<4\r\nERIKSSON<<ANNA<MARIA<<<<<<<<B<")]
   [Arguments("I<UTOSTARWARS45<<<<<<<<<<<<<<<\n7705256M2405252UTO<<<<<<<<<<<4\nSKYWALKER<<LUKE<<<<<<<<<<<<<C<")]
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
