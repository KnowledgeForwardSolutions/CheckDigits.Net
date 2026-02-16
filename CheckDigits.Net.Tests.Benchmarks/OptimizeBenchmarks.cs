namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class OptimizeBenchmarks
{
   //private static readonly Iso7064Mod1271_36Algorithm _algorithm = new();

   private static readonly NhsAlgorithm _nhsAlgorithm = new();
   private static readonly Modulus11DecimalAlgorithm _modulus11DecimalAlgorithm = new();

   private static readonly ICheckDigitMask _groupsOfThreeMask = new GroupsOfThreeCheckDigitMask();

   //[Params("BE71096123456769", "GB82WEST12345698765432", "SC74MCBL01031234567890123456USD")]
   //public String Value { get; set; } = String.Empty;

   //[Benchmark]
   //public void Startup() => _algorithm.Validate(Value);

   //[Benchmark(Baseline = true)]
   //public void Validate() => _ = _algorithm.Validate(Value);

   //[Benchmark]
   //public void Validate2() => _ = _algorithm.Validate2(Value);

   //[Benchmark]
   //public void Validate3() => _ = _algorithm.Validate3(Value);

   //[Params("U7Y", "U7Y8SX", "U7Y8SXRC0", "U7Y8SXRC0O3S", "U7Y8SXRC0O3SC4I", "U7Y8SXRC0O3SC4IHYQ", "U7Y8SXRC0O3SC4IHYQF4M")]
   //public String TryValue { get; set; } = String.Empty;

   //[Benchmark]
   //public void Startup() => _algorithm.TryCalculateCheckDigits(TryValue, out var ch, out var ch2);

   //[Benchmark(Baseline = true)]
   //public void TryCalculate() => _ = _algorithm.TryCalculateCheckDigits(TryValue, out var ch, out var ch2);

   //[Benchmark]
   //public void TryCalculate2() => _ = _algorithm.TryCalculateCheckDigits2(TryValue, out var ch, out var ch2);

   //[Benchmark]
   //public void TryCalculate3() => _ = _algorithm.TryCalculateCheckDigits3(TryValue, out var ch, out var ch2);

   [Benchmark(Baseline = true)]
   [Arguments("9434765919")]
   [Arguments("4505577104")]
   [Arguments("5301194917")]
   public void NhsValidate(String value) => _ = _nhsAlgorithm.Validate(value);

   [Benchmark()]
   [Arguments("9434765919")]
   [Arguments("4505577104")]
   [Arguments("5301194917")]
   public void Modulus11DecimalValidate(String value) => _ = _modulus11DecimalAlgorithm.Validate(value);

   [Benchmark()]
   [Arguments("943 476 591 9")]
   [Arguments("450 557 710 4")]
   [Arguments("530 119 491 7")]
   public void Modulus11DecimalValidateMasked(String value) 
      => _ = _modulus11DecimalAlgorithm.Validate(value, _groupsOfThreeMask);

}