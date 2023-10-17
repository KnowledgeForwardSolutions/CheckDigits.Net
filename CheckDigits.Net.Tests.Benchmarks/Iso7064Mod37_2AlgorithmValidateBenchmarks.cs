namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class Iso7064Mod37_2AlgorithmValidateBenchmarks
{
   private static readonly Iso7064Mod37_2Algorithm _algorithm = new();

   [Params("ZZZZO", "A999914123456N", "A999522123456*", "ABCDEFGHIJKLMNOPQRSTUVWX*")]
   public String Value { get; set; } = String.Empty;

   [Benchmark]
   public void Validate()
   {
      _ = _algorithm.Validate(Value);
   }
}
