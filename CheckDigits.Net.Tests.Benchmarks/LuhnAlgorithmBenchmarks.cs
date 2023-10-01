// Ignore Spelling: Luhn

namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class LuhnAlgorithmBenchmarks
{
   private const String _value = "305693000902000";
   private static readonly LuhnAlgorithm _luhnAlgorithm = new();

   [Benchmark(Baseline = true)]
   public void Baseline()
   {
      _ = _luhnAlgorithm.TryCalculateCheckDigit(_value, out var checkDigit);
   }

   [Benchmark]
   public void V0()
   {
      _ = _luhnAlgorithm.TryCalculateCheckDigit(_value, out var checkDigit);
   }
}
