// Ignore Spelling: Aba Rtn

namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class AbaRtnAlgorithmValidateBenchmarks
{
   private static readonly AbaRtnAlgorithm _algorithm = new();

   [Params("111000025", "122235821")]
   public String Value { get; set; } = String.Empty;

   [Benchmark]
   public void Validate()
   {
      _ = _algorithm.Validate(Value);
   }
}
