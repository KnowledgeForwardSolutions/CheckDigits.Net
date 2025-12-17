// Ignore Spelling: Luhn Tharga Kor Dandago Slx

namespace CheckDigits.Net.Tests.Benchmarks.Comparisons;

using CcHelper = CreditCardHelper.Validator;

using CcvLuhn = CreditCardValidator.Luhn;

using DdLuhn = Dandago.Finance.LuhnAlgorithm;

using EkLuhn = EnKor.Luhn;

using LuChecker = LuhnChecker.LuhnValidator;

using LnDotNet = LuhnDotNet.Algorithm.Luhn.LuhnValidator;

using LnLuhn = LuhnNet.Luhn;

using SlxLuhn = SlxLuhnLibrary.ClsLuhnLibrary;

using ThargaLuhn = Tharga.Toolkit.Luhn;

using OvLuhn = wan24.ObjectValidation.LuhnChecksum;

[MemoryDiagnoser]
public class LuhnComparisons
{
   private static readonly ICheckDigitAlgorithm _baseline = Algorithms.Luhn;

   //[Params("1404", "1406628", "1406625382", "1406625380421", "1406625380425514", "1406625380425510285", "1406625380425510282651")]
   [Params("4012888888881881", "3056930009020004", "5555555555554444")]
   public String Value { get; set; } = default!;

   [Benchmark(Baseline = true)]
   public Boolean CheckDigitsDotNet() => _baseline.Validate(Value);

   [Benchmark]
   public Boolean CreditCardHelper() => CcHelper.ValidateLuhn(Value);

   [Benchmark]
   public Boolean CreditCardValidator() => CcvLuhn.CheckLuhn(Value);

   [Benchmark]
   public Boolean DandagoFinance() => DdLuhn.IsValid(Value);

   [Benchmark]
   public Boolean EnKor() => EkLuhn.IsValid(Value);

   [Benchmark]
   public Boolean LuhnChecker() => LuChecker.IsValid(Value);

   [Benchmark]
   public Boolean LuhnDotNet() => LnDotNet.IsValidLuhnNumber(Value);

   [Benchmark]
   public Boolean LuhnNet() => LnLuhn.IsValid(Value);

   [Benchmark]
   public Boolean ObjectValidation() => OvLuhn.Validate(Value);

   [Benchmark]
   public Boolean SlxLuhnLibrary() => SlxLuhn.CheckLuhn_Base10(Value);

   [Benchmark]
   public Boolean ThargaToolkit() => ThargaLuhn.HasValidCheckDigit(Value);
}
