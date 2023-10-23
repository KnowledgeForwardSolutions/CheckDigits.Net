namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class Iso7064Mod27_26AlgorithmValidateBenchmarks
{
   private static readonly Iso7064Mod27_26Algorithm _algorithm = new();

   [Params("AEIOUI", "QWERTYDVORAKY", "ABCDEFGHIJKLMNOPQRO", "ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZB")]
   public String Value { get; set; } = String.Empty;

   [Benchmark]
   public void Validate()
   {
      _ = _algorithm.Validate(Value);
   }
}
