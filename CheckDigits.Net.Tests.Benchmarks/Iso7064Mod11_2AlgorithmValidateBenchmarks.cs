namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class Iso7064Mod11_2AlgorithmValidateBenchmarks
{
   private static readonly Iso7064Mod11_2Algorithm _algorithm = new();

   [Params("07940", "000000012095650X", "0000000109302468", "999999999999999999999999999999999994")]
   public String Value { get; set; } = String.Empty;

   [Benchmark(Baseline = true)]
   public void Validate()
   {
      _ = _algorithm.Validate(Value);
   }

   //[Benchmark]
   //public void Validate2()
   //{
   //   _ = _algorithm.Validate2(Value);
   //}

   //[Benchmark]
   //public void Validate3()
   //{
   //   _ = _algorithm.Validate3(Value);
   //}

   //[Benchmark]
   //public void Validate4()
   //{
   //   _ = _algorithm.Validate4(Value);
   //}

   //[Benchmark]
   //public void Validate5()
   //{
   //   _ = _algorithm.Validate5(Value);
   //}

   //[Benchmark]
   //public void Validate6()
   //{
   //   _ = _algorithm.Validate6(Value);
   //}
}
