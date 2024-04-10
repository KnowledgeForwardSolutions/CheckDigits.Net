using CheckDigits.Net.Utility;

namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class PerformanceBenchmarks
{
   private readonly ModulusInt32 _counter = new(3) { Value = 1 };
   private readonly Int32[] _weights = [7, 3, 1];

   [Benchmark]
   public void Cast()
   {
      var weight = _weights[_counter];
   }

   [Benchmark]
   public void Property()
   {
      var weight = _weights[_counter.Value];
   }
}
