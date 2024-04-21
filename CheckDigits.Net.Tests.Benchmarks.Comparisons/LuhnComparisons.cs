// Ignore Spelling: Luhn Tharga

namespace CheckDigits.Net.Tests.Benchmarks.Comparisons;

using CcvLuhn = CreditCardValidator.Luhn;

using LnLuhn = LuhnNet.Luhn;

using ThargaLuhn = Tharga.Toolkit.Luhn;

[MemoryDiagnoser]
public class LuhnComparisons
{
   private static readonly ICheckDigitAlgorithm _baseline = Algorithms.Luhn;

   [Params("1404", "1406628", "1406625382", "1406625380421", "1406625380425514", "1406625380425510285", "1406625380425510282651")]
   public String Value { get; set; } = default!;

   [Benchmark(Baseline = true)]
   public Boolean CheckDigitsDotNet() => _baseline.Validate(Value);

   [Benchmark]
   public Boolean CreditCardValidator() => CcvLuhn.CheckLuhn(Value);

   [Benchmark]
   public Boolean LuhnNet() => LnLuhn.IsValid(Value);

   [Benchmark]
   public Boolean ThargaToolkit() => ThargaLuhn.HasValidCheckDigit(Value);
}
