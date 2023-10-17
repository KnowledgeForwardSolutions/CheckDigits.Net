namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class Iso7064Mod11_2AlgorithmTryCalculateBenchmarks
{
   private static readonly Iso7064Mod11_2Algorithm _algorithm = new();


   [Params("0794", "000000012095650", "000000010930246", "99999999999999999999999999999999999")]
   public String Value { get; set; } = String.Empty;

   [Benchmark]
   public void TryCalculateCheckDigit()
   {
      _ = _algorithm.TryCalculateCheckDigit(Value, out var checkDigit);
   }
}
