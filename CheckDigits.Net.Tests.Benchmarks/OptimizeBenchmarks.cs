// Ignore Spelling: Damm Quasigroup

#pragma warning disable CS0618 // Type or member is obsolete

namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class OptimizeBenchmarks
{
   private static readonly DammAlgorithm _baselineAlgorithm = new();
   private static readonly DammAlgorithm _quasigroupAlgorithm = new(); //(new DefaultDammQuasigroupOrder10());

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
