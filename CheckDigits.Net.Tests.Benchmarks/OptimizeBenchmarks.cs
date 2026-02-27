// Ignore Spelling: Damm Quasigroup

#pragma warning disable CS0618 // Type or member is obsolete

using CheckDigits.Net.Utility;

namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class OptimizeBenchmarks
{
   private static readonly DammAlgorithm _baselineAlgorithm = new();
   private static readonly DammAlgorithm _quasigroupAlgorithm = new(new DefaultDammQuasigroupOrder10());

   [Benchmark(Baseline = true)]
   [Arguments("140")]
   [Arguments("140662")]
   [Arguments("140662538")]
   [Arguments("140662538042")]
   [Arguments("140662538042551")]
   [Arguments("140662538042551028")]
   [Arguments("140662538042551028265")]
   public void BaselineVersion(String value) => _ = _baselineAlgorithm.TryCalculateCheckDigit(value, out _);

   [Benchmark()]
   [Arguments("140")]
   [Arguments("140662")]
   [Arguments("140662538")]
   [Arguments("140662538042")]
   [Arguments("140662538042551")]
   [Arguments("140662538042551028")]
   [Arguments("140662538042551028265")]
   public void QuasigroupVersion(String value) => _ = _quasigroupAlgorithm.TryCalculateCheckDigit(value, out _);
}

public class DammQuasigroupOrder10 : IDammQuasigroup
{
   // 2D array flattened to 1D. Gives ~30% performance improvement vs 2D.
   private static readonly Int32[] _quasigroupTable =
   [
      0, 3, 1, 7, 5, 9, 8, 6, 4, 2,
      7, 0, 9, 2, 1, 5, 4, 8, 6, 3,
      4, 2, 0, 6, 8, 7, 1, 3, 5, 9,
      1, 7, 5, 0, 9, 8, 3, 4, 2, 6,
      6, 1, 2, 3, 0, 4, 5, 9, 7, 8,
      3, 6, 7, 4, 2, 0, 9, 5, 8, 1,
      5, 8, 6, 9, 7, 2, 0, 1, 3, 4,
      8, 9, 4, 5, 3, 6, 2, 0, 1, 7,
      9, 4, 3, 8, 6, 1, 7, 2, 0, 5,
      2, 5, 8, 1, 4, 3, 6, 7, 9, 0,
   ];

   public Int32 this[Int32 interim, Int32 next] => _quasigroupTable[(interim * 10) + next];

   public Int32 Order => 10;

   public Char GetCheckDigit(Int32 interim) => interim.ToDigitChar();

   public Int32 MapCharacter(Char ch) => ch.ToIntegerDigit();
}
