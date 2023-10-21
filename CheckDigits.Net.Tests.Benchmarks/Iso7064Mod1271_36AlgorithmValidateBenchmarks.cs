namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class Iso7064Mod1271_36AlgorithmValidateBenchmarks
{
   private static readonly Iso7064Mod1271_36Algorithm _algorithm = new();

   [Params("ISO793W", "XS868977863229AU", "AEIOU1592430QWERTY0Z", "ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ6X")]
   public String Value { get; set; } = String.Empty;

   [Benchmark]
   public void Validate()
   {
      _ = _algorithm.Validate(Value);
   }
}