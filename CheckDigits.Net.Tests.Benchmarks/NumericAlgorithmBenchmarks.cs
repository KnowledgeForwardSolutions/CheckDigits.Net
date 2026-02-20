#pragma warning disable CA1822 // Mark members as static
#pragma warning disable IDE0022 // Use expression body for method
#pragma warning disable IDE0059 // Unnecessary assignment of a value
#pragma warning disable IDE0060 // Remove unused parameter if it is not part of shipped public API

namespace CheckDigits.Net.Tests.Benchmarks;

//[SimpleJob(RuntimeMoniker.Net80, baseline: true)]
//[SimpleJob(RuntimeMoniker.Net10_0)]
[MemoryDiagnoser]
public class NumericAlgorithmBenchmarks
{
   private static readonly ICheckDigitMask _groupsOfThreeMask = new GroupsOfThreeCheckDigitMask();

   public IEnumerable<Object[]> TryCalculateCheckDigitArguments()
   {
      //      yield return [Algorithms.Damm, Algorithms.Damm.AlgorithmName, "140"];
      //      yield return [Algorithms.Damm, Algorithms.Damm.AlgorithmName, "140662"];
      //      yield return [Algorithms.Damm, Algorithms.Damm.AlgorithmName, "140662538"];
      //      yield return [Algorithms.Damm, Algorithms.Damm.AlgorithmName, "140662538042"];
      //      yield return [Algorithms.Damm, Algorithms.Damm.AlgorithmName, "140662538042551"];
      //      yield return [Algorithms.Damm, Algorithms.Damm.AlgorithmName, "140662538042551028"];
      //      yield return [Algorithms.Damm, Algorithms.Damm.AlgorithmName, "140662538042551028265"];

      //      yield return [Algorithms.Iso7064Mod11_10, Algorithms.Iso7064Mod11_10.AlgorithmName, "140"];
      //      yield return [Algorithms.Iso7064Mod11_10, Algorithms.Iso7064Mod11_10.AlgorithmName, "140662"];
      //      yield return [Algorithms.Iso7064Mod11_10, Algorithms.Iso7064Mod11_10.AlgorithmName, "140662538"];
      //      yield return [Algorithms.Iso7064Mod11_10, Algorithms.Iso7064Mod11_10.AlgorithmName, "140662538042"];
      //      yield return [Algorithms.Iso7064Mod11_10, Algorithms.Iso7064Mod11_10.AlgorithmName, "140662538042551"];
      //      yield return [Algorithms.Iso7064Mod11_10, Algorithms.Iso7064Mod11_10.AlgorithmName, "140662538042551028"];
      //      yield return [Algorithms.Iso7064Mod11_10, Algorithms.Iso7064Mod11_10.AlgorithmName, "140662538042551028265"];

      //      yield return [Algorithms.Iso7064Mod11_2, Algorithms.Iso7064Mod11_2.AlgorithmName, "140"];
      //      yield return [Algorithms.Iso7064Mod11_2, Algorithms.Iso7064Mod11_2.AlgorithmName, "140662"];
      //      yield return [Algorithms.Iso7064Mod11_2, Algorithms.Iso7064Mod11_2.AlgorithmName, "140662538"];
      //      yield return [Algorithms.Iso7064Mod11_2, Algorithms.Iso7064Mod11_2.AlgorithmName, "140662538042"];
      //      yield return [Algorithms.Iso7064Mod11_2, Algorithms.Iso7064Mod11_2.AlgorithmName, "140662538042551"];
      //      yield return [Algorithms.Iso7064Mod11_2, Algorithms.Iso7064Mod11_2.AlgorithmName, "140662538042551028"];
      //      yield return [Algorithms.Iso7064Mod11_2, Algorithms.Iso7064Mod11_2.AlgorithmName, "140662538042551028265"];

      //      yield return [Algorithms.Luhn, Algorithms.Luhn.AlgorithmName, "140"];
      //      yield return [Algorithms.Luhn, Algorithms.Luhn.AlgorithmName, "140662"];
      //      yield return [Algorithms.Luhn, Algorithms.Luhn.AlgorithmName, "140662538"];
      //      yield return [Algorithms.Luhn, Algorithms.Luhn.AlgorithmName, "140662538042"];
      //      yield return [Algorithms.Luhn, Algorithms.Luhn.AlgorithmName, "140662538042551"];
      //      yield return [Algorithms.Luhn, Algorithms.Luhn.AlgorithmName, "140662538042551028"];
      //      yield return [Algorithms.Luhn, Algorithms.Luhn.AlgorithmName, "140662538042551028265"];

      //      yield return [Algorithms.Modulus10_1, Algorithms.Modulus10_1.AlgorithmName, "140"];
      //      yield return [Algorithms.Modulus10_1, Algorithms.Modulus10_1.AlgorithmName, "140662"];
      //      yield return [Algorithms.Modulus10_1, Algorithms.Modulus10_1.AlgorithmName, "140662538"];

      //      yield return [Algorithms.Modulus10_2, Algorithms.Modulus10_2.AlgorithmName, "140"];
      //      yield return [Algorithms.Modulus10_2, Algorithms.Modulus10_2.AlgorithmName, "140662"];
      //      yield return [Algorithms.Modulus10_2, Algorithms.Modulus10_2.AlgorithmName, "140662538"];

      //      yield return [Algorithms.Modulus10_13, Algorithms.Modulus10_13.AlgorithmName, "140"];
      //      yield return [Algorithms.Modulus10_13, Algorithms.Modulus10_13.AlgorithmName, "140662"];
      //      yield return [Algorithms.Modulus10_13, Algorithms.Modulus10_13.AlgorithmName, "140662538"];
      //      yield return [Algorithms.Modulus10_13, Algorithms.Modulus10_13.AlgorithmName, "140662538042"];
      //      yield return [Algorithms.Modulus10_13, Algorithms.Modulus10_13.AlgorithmName, "140662538042551"];
      //      yield return [Algorithms.Modulus10_13, Algorithms.Modulus10_13.AlgorithmName, "140662538042551028"];
      //      yield return [Algorithms.Modulus10_13, Algorithms.Modulus10_13.AlgorithmName, "140662538042551028265"];

      //#pragma warning disable CS0618 // Type or member is obsolete
      //      yield return [Algorithms.Modulus11, Algorithms.Modulus11.AlgorithmName, "140"];
      //      yield return [Algorithms.Modulus11, Algorithms.Modulus11.AlgorithmName, "140662"];
      //      yield return [Algorithms.Modulus11, Algorithms.Modulus11.AlgorithmName, "140662538"];
      //#pragma warning restore CS0618 // Type or member is obsolete

      //yield return [Algorithms.Modulus11_27Decimal, Algorithms.Modulus11_27Decimal.AlgorithmName, "140"];
      //yield return [Algorithms.Modulus11_27Decimal, Algorithms.Modulus11_27Decimal.AlgorithmName, "140662"];
      //yield return [Algorithms.Modulus11_27Decimal, Algorithms.Modulus11_27Decimal.AlgorithmName, "140662538"];
      //yield return [Algorithms.Modulus11_27Decimal, Algorithms.Modulus11_27Decimal.AlgorithmName, "140662538042"];
      //yield return [Algorithms.Modulus11_27Decimal, Algorithms.Modulus11_27Decimal.AlgorithmName, "140662538042551"];
      //yield return [Algorithms.Modulus11_27Decimal, Algorithms.Modulus11_27Decimal.AlgorithmName, "140662538042551028"];
      //yield return [Algorithms.Modulus11_27Decimal, Algorithms.Modulus11_27Decimal.AlgorithmName, "140662538042551028265"];

      yield return [Algorithms.Modulus11_27Extended, Algorithms.Modulus11_27Extended.AlgorithmName, "140"];
      yield return [Algorithms.Modulus11_27Extended, Algorithms.Modulus11_27Extended.AlgorithmName, "140662"];
      yield return [Algorithms.Modulus11_27Extended, Algorithms.Modulus11_27Extended.AlgorithmName, "140662538"];
      yield return [Algorithms.Modulus11_27Extended, Algorithms.Modulus11_27Extended.AlgorithmName, "140662538042"];
      yield return [Algorithms.Modulus11_27Extended, Algorithms.Modulus11_27Extended.AlgorithmName, "140662538042551"];
      yield return [Algorithms.Modulus11_27Extended, Algorithms.Modulus11_27Extended.AlgorithmName, "140662538042551028"];
      yield return [Algorithms.Modulus11_27Extended, Algorithms.Modulus11_27Extended.AlgorithmName, "140662538042551028265"];

      //yield return [Algorithms.Modulus11Decimal, Algorithms.Modulus11Decimal.AlgorithmName, "140"];
      //yield return [Algorithms.Modulus11Decimal, Algorithms.Modulus11Decimal.AlgorithmName, "140662"];
      //yield return [Algorithms.Modulus11Decimal, Algorithms.Modulus11Decimal.AlgorithmName, "140662538"];

      //yield return [Algorithms.Modulus11Extended, Algorithms.Modulus11Extended.AlgorithmName, "140"];
      //yield return [Algorithms.Modulus11Extended, Algorithms.Modulus11Extended.AlgorithmName, "140662"];
      //yield return [Algorithms.Modulus11Extended, Algorithms.Modulus11Extended.AlgorithmName, "140662538"];

      //yield return [Algorithms.Verhoeff, Algorithms.Verhoeff.AlgorithmName, "140"];
      //yield return [Algorithms.Verhoeff, Algorithms.Verhoeff.AlgorithmName, "140662"];
      //yield return [Algorithms.Verhoeff, Algorithms.Verhoeff.AlgorithmName, "140662538"];
      //yield return [Algorithms.Verhoeff, Algorithms.Verhoeff.AlgorithmName, "140662538042"];
      //yield return [Algorithms.Verhoeff, Algorithms.Verhoeff.AlgorithmName, "140662538042551"];
      //yield return [Algorithms.Verhoeff, Algorithms.Verhoeff.AlgorithmName, "140662538042551028"];
      //yield return [Algorithms.Verhoeff, Algorithms.Verhoeff.AlgorithmName, "140662538042551028265"];
   }

   public IEnumerable<Object[]> TryCalculateCheckDigitsArguments()
   {
      yield return [ Algorithms.Iso7064Mod97_10, Algorithms.Iso7064Mod97_10.AlgorithmName, "140" ];
      yield return [ Algorithms.Iso7064Mod97_10, Algorithms.Iso7064Mod97_10.AlgorithmName, "140662" ];
      yield return [ Algorithms.Iso7064Mod97_10, Algorithms.Iso7064Mod97_10.AlgorithmName, "140662538" ];
      yield return [ Algorithms.Iso7064Mod97_10, Algorithms.Iso7064Mod97_10.AlgorithmName, "140662538042" ];
      yield return [ Algorithms.Iso7064Mod97_10, Algorithms.Iso7064Mod97_10.AlgorithmName, "140662538042551" ];
      yield return [ Algorithms.Iso7064Mod97_10, Algorithms.Iso7064Mod97_10.AlgorithmName, "140662538042551028" ];
      yield return [ Algorithms.Iso7064Mod97_10, Algorithms.Iso7064Mod97_10.AlgorithmName, "140662538042551028265" ];
   }

   public IEnumerable<Object[]> ValidateArguments()
   {
      //      yield return [Algorithms.Damm, Algorithms.Damm.AlgorithmName, "1402"];
      //      yield return [Algorithms.Damm, Algorithms.Damm.AlgorithmName, "1406622"];
      //      yield return [Algorithms.Damm, Algorithms.Damm.AlgorithmName, "1406625388"];
      //      yield return [Algorithms.Damm, Algorithms.Damm.AlgorithmName, "1406625380422"];
      //      yield return [Algorithms.Damm, Algorithms.Damm.AlgorithmName, "1406625380425518"];
      //      yield return [Algorithms.Damm, Algorithms.Damm.AlgorithmName, "1406625380425510280"];
      //      yield return [Algorithms.Damm, Algorithms.Damm.AlgorithmName, "1406625380425510282654"];

      //      yield return [Algorithms.Iso7064Mod11_10, Algorithms.Iso7064Mod11_10.AlgorithmName, "1409"];
      //      yield return [Algorithms.Iso7064Mod11_10, Algorithms.Iso7064Mod11_10.AlgorithmName, "1406623"];
      //      yield return [Algorithms.Iso7064Mod11_10, Algorithms.Iso7064Mod11_10.AlgorithmName, "1406625381"];
      //      yield return [Algorithms.Iso7064Mod11_10, Algorithms.Iso7064Mod11_10.AlgorithmName, "1406625380426"];
      //      yield return [Algorithms.Iso7064Mod11_10, Algorithms.Iso7064Mod11_10.AlgorithmName, "1406625380425514"];
      //      yield return [Algorithms.Iso7064Mod11_10, Algorithms.Iso7064Mod11_10.AlgorithmName, "1406625380425510286"];
      //      yield return [Algorithms.Iso7064Mod11_10, Algorithms.Iso7064Mod11_10.AlgorithmName, "1406625380425510282657"];

      //      yield return [Algorithms.Iso7064Mod11_2, Algorithms.Iso7064Mod11_2.AlgorithmName, "140X"];
      //      yield return [Algorithms.Iso7064Mod11_2, Algorithms.Iso7064Mod11_2.AlgorithmName, "1406628"];
      //      yield return [Algorithms.Iso7064Mod11_2, Algorithms.Iso7064Mod11_2.AlgorithmName, "1406625380"];
      //      yield return [Algorithms.Iso7064Mod11_2, Algorithms.Iso7064Mod11_2.AlgorithmName, "1406625380426"];
      //      yield return [Algorithms.Iso7064Mod11_2, Algorithms.Iso7064Mod11_2.AlgorithmName, "1406625380425511"];
      //      yield return [Algorithms.Iso7064Mod11_2, Algorithms.Iso7064Mod11_2.AlgorithmName, "140662538042551028X"];
      //      yield return [Algorithms.Iso7064Mod11_2, Algorithms.Iso7064Mod11_2.AlgorithmName, "1406625380425510282651"];

      //      yield return [Algorithms.Iso7064Mod97_10, Algorithms.Iso7064Mod97_10.AlgorithmName, "14066"];
      //      yield return [Algorithms.Iso7064Mod97_10, Algorithms.Iso7064Mod97_10.AlgorithmName, "14066262"];
      //      yield return [Algorithms.Iso7064Mod97_10, Algorithms.Iso7064Mod97_10.AlgorithmName, "14066253823"];
      //      yield return [Algorithms.Iso7064Mod97_10, Algorithms.Iso7064Mod97_10.AlgorithmName, "14066253804250"];
      //      yield return [Algorithms.Iso7064Mod97_10, Algorithms.Iso7064Mod97_10.AlgorithmName, "14066253804255112"];
      //      yield return [Algorithms.Iso7064Mod97_10, Algorithms.Iso7064Mod97_10.AlgorithmName, "14066253804255102853"];
      //      yield return [Algorithms.Iso7064Mod97_10, Algorithms.Iso7064Mod97_10.AlgorithmName, "14066253804255102826587"];

      //      yield return [Algorithms.Luhn, Algorithms.Luhn.AlgorithmName, "1404"];
      //      yield return [Algorithms.Luhn, Algorithms.Luhn.AlgorithmName, "1406628"];
      //      yield return [Algorithms.Luhn, Algorithms.Luhn.AlgorithmName, "1406625382"];
      //      yield return [Algorithms.Luhn, Algorithms.Luhn.AlgorithmName, "1406625380421"];
      //      yield return [Algorithms.Luhn, Algorithms.Luhn.AlgorithmName, "1406625380425514"];
      //      yield return [Algorithms.Luhn, Algorithms.Luhn.AlgorithmName, "1406625380425510285"];
      //      yield return [Algorithms.Luhn, Algorithms.Luhn.AlgorithmName, "1406625380425510282651"];

      //      yield return [Algorithms.Modulus10_1, Algorithms.Modulus10_1.AlgorithmName, "1401"];
      //      yield return [Algorithms.Modulus10_1, Algorithms.Modulus10_1.AlgorithmName, "1406628"];
      //      yield return [Algorithms.Modulus10_1, Algorithms.Modulus10_1.AlgorithmName, "1406625384"];

      //      yield return [Algorithms.Modulus10_2, Algorithms.Modulus10_2.AlgorithmName, "1406"];
      //      yield return [Algorithms.Modulus10_2, Algorithms.Modulus10_2.AlgorithmName, "1406627"];
      //      yield return [Algorithms.Modulus10_2, Algorithms.Modulus10_2.AlgorithmName, "1406625389"];

      //      yield return [Algorithms.Modulus10_13, Algorithms.Modulus10_13.AlgorithmName, "1403"];
      //      yield return [Algorithms.Modulus10_13, Algorithms.Modulus10_13.AlgorithmName, "1406627"];
      //      yield return [Algorithms.Modulus10_13, Algorithms.Modulus10_13.AlgorithmName, "1406625385"];
      //      yield return [Algorithms.Modulus10_13, Algorithms.Modulus10_13.AlgorithmName, "1406625380425"];
      //      yield return [Algorithms.Modulus10_13, Algorithms.Modulus10_13.AlgorithmName, "1406625380425518"];
      //      yield return [Algorithms.Modulus10_13, Algorithms.Modulus10_13.AlgorithmName, "1406625380425510288"];
      //      yield return [Algorithms.Modulus10_13, Algorithms.Modulus10_13.AlgorithmName, "1406625380425510282657"];

      //#pragma warning disable CS0618 // Type or member is obsolete
      //      yield return [Algorithms.Modulus11, Algorithms.Modulus11.AlgorithmName, "1406"];
      //      yield return [Algorithms.Modulus11, Algorithms.Modulus11.AlgorithmName, "1406620"];
      //      yield return [Algorithms.Modulus11, Algorithms.Modulus11.AlgorithmName, "1406625388"];
      //#pragma warning restore CS0618 // Type or member is obsolete

      //yield return [Algorithms.Modulus11_27Decimal, Algorithms.Modulus11_27Decimal.AlgorithmName, "1406"];
      //yield return [Algorithms.Modulus11_27Decimal, Algorithms.Modulus11_27Decimal.AlgorithmName, "1406620"];
      //yield return [Algorithms.Modulus11_27Decimal, Algorithms.Modulus11_27Decimal.AlgorithmName, "1406625385"];
      //yield return [Algorithms.Modulus11_27Decimal, Algorithms.Modulus11_27Decimal.AlgorithmName, "1406625380421"];
      //yield return [Algorithms.Modulus11_27Decimal, Algorithms.Modulus11_27Decimal.AlgorithmName, "1406625380425510"];
      //yield return [Algorithms.Modulus11_27Decimal, Algorithms.Modulus11_27Decimal.AlgorithmName, "1406625380425510288"];
      //yield return [Algorithms.Modulus11_27Decimal, Algorithms.Modulus11_27Decimal.AlgorithmName, "1406625380425510282650"];

      yield return [Algorithms.Modulus11_27Extended, Algorithms.Modulus11_27Extended.AlgorithmName, "1406"];
      yield return [Algorithms.Modulus11_27Extended, Algorithms.Modulus11_27Extended.AlgorithmName, "1406620"];
      yield return [Algorithms.Modulus11_27Extended, Algorithms.Modulus11_27Extended.AlgorithmName, "1406625385"];
      yield return [Algorithms.Modulus11_27Extended, Algorithms.Modulus11_27Extended.AlgorithmName, "1406625380421"];
      yield return [Algorithms.Modulus11_27Extended, Algorithms.Modulus11_27Extended.AlgorithmName, "1406625380425510"];
      yield return [Algorithms.Modulus11_27Extended, Algorithms.Modulus11_27Extended.AlgorithmName, "1406625380425510288"];
      yield return [Algorithms.Modulus11_27Extended, Algorithms.Modulus11_27Extended.AlgorithmName, "1406625380425510282650"];

      //yield return [Algorithms.Modulus11Decimal, Algorithms.Modulus11Decimal.AlgorithmName, "1406"];
      //yield return [Algorithms.Modulus11Decimal, Algorithms.Modulus11Decimal.AlgorithmName, "1406620"];
      //yield return [Algorithms.Modulus11Decimal, Algorithms.Modulus11Decimal.AlgorithmName, "1406625388"];

      //yield return [Algorithms.Modulus11Extended, Algorithms.Modulus11Extended.AlgorithmName, "1406"];
      //yield return [Algorithms.Modulus11Extended, Algorithms.Modulus11Extended.AlgorithmName, "1406620"];
      //yield return [Algorithms.Modulus11Extended, Algorithms.Modulus11Extended.AlgorithmName, "1406625388"];

      //yield return [Algorithms.Verhoeff, Algorithms.Verhoeff.AlgorithmName, "1401"];
      //yield return [Algorithms.Verhoeff, Algorithms.Verhoeff.AlgorithmName, "1406620"];
      //yield return [Algorithms.Verhoeff, Algorithms.Verhoeff.AlgorithmName, "1406625388"];
      //yield return [Algorithms.Verhoeff, Algorithms.Verhoeff.AlgorithmName, "1406625380426"];
      //yield return [Algorithms.Verhoeff, Algorithms.Verhoeff.AlgorithmName, "1406625380425512"];
      //yield return [Algorithms.Verhoeff, Algorithms.Verhoeff.AlgorithmName, "1406625380425510285"];
      //yield return [Algorithms.Verhoeff, Algorithms.Verhoeff.AlgorithmName, "1406625380425510282655"];
   }

   public IEnumerable<Object[]> ValidateMaskedArguments()
   {
      //yield return [Algorithms.Luhn, Algorithms.Luhn.AlgorithmName, "140 4"];
      //yield return [Algorithms.Luhn, Algorithms.Luhn.AlgorithmName, "140 662 8"];
      //yield return [Algorithms.Luhn, Algorithms.Luhn.AlgorithmName, "140 662 538 2"];
      //yield return [Algorithms.Luhn, Algorithms.Luhn.AlgorithmName, "140 662 538 042 1"];
      //yield return [Algorithms.Luhn, Algorithms.Luhn.AlgorithmName, "140 662 538 042 551 4"];
      //yield return [Algorithms.Luhn, Algorithms.Luhn.AlgorithmName, "140 662 538 042 551 028 5"];
      //yield return [Algorithms.Luhn, Algorithms.Luhn.AlgorithmName, "140 662 538 042 551 028 265 1"];

      //yield return [Algorithms.Modulus10_13, Algorithms.Modulus10_13.AlgorithmName, "140 3"];
      //yield return [Algorithms.Modulus10_13, Algorithms.Modulus10_13.AlgorithmName, "140 662 7"];
      //yield return [Algorithms.Modulus10_13, Algorithms.Modulus10_13.AlgorithmName, "140 662 538 5"];
      //yield return [Algorithms.Modulus10_13, Algorithms.Modulus10_13.AlgorithmName, "140 662 538 042 5"];
      //yield return [Algorithms.Modulus10_13, Algorithms.Modulus10_13.AlgorithmName, "140 662 538 042 551 8"];
      //yield return [Algorithms.Modulus10_13, Algorithms.Modulus10_13.AlgorithmName, "140 662 538 042 551 028 8"];
      //yield return [Algorithms.Modulus10_13, Algorithms.Modulus10_13.AlgorithmName, "140 662 538 042 551 028 265 7"];

      //yield return [Algorithms.Modulus11_27Decimal, Algorithms.Modulus11_27Decimal.AlgorithmName, "140 6"];
      //yield return [Algorithms.Modulus11_27Decimal, Algorithms.Modulus11_27Decimal.AlgorithmName, "140 662 0"];
      //yield return [Algorithms.Modulus11_27Decimal, Algorithms.Modulus11_27Decimal.AlgorithmName, "140 662 538 5"];
      //yield return [Algorithms.Modulus11_27Decimal, Algorithms.Modulus11_27Decimal.AlgorithmName, "140 662 538 042 1"];
      //yield return [Algorithms.Modulus11_27Decimal, Algorithms.Modulus11_27Decimal.AlgorithmName, "140 662 538 042 551 0"];
      //yield return [Algorithms.Modulus11_27Decimal, Algorithms.Modulus11_27Decimal.AlgorithmName, "140 662 538 042 551 028 8"];
      //yield return [Algorithms.Modulus11_27Decimal, Algorithms.Modulus11_27Decimal.AlgorithmName, "140 662 538 042 551 028 265 0"];

      yield return [Algorithms.Modulus11_27Extended, Algorithms.Modulus11_27Extended.AlgorithmName, "140 6"];
      yield return [Algorithms.Modulus11_27Extended, Algorithms.Modulus11_27Extended.AlgorithmName, "140 662 0"];
      yield return [Algorithms.Modulus11_27Extended, Algorithms.Modulus11_27Extended.AlgorithmName, "140 662 538 5"];
      yield return [Algorithms.Modulus11_27Extended, Algorithms.Modulus11_27Extended.AlgorithmName, "140 662 538 042 1"];
      yield return [Algorithms.Modulus11_27Extended, Algorithms.Modulus11_27Extended.AlgorithmName, "140 662 538 042 551 0"];
      yield return [Algorithms.Modulus11_27Extended, Algorithms.Modulus11_27Extended.AlgorithmName, "140 662 538 042 551 028 8"];
      yield return [Algorithms.Modulus11_27Extended, Algorithms.Modulus11_27Extended.AlgorithmName, "140 662 538 042 551 028 265 0"];

      //yield return [Algorithms.Modulus11Decimal, Algorithms.Modulus11Decimal.AlgorithmName, "140 6"];
      //yield return [Algorithms.Modulus11Decimal, Algorithms.Modulus11Decimal.AlgorithmName, "140 662 0"];
      //yield return [Algorithms.Modulus11Decimal, Algorithms.Modulus11Decimal.AlgorithmName, "140 662 538 8"];

      //yield return [Algorithms.Modulus11Extended, Algorithms.Modulus11Extended.AlgorithmName, "140 6"];
      //yield return [Algorithms.Modulus11Extended, Algorithms.Modulus11Extended.AlgorithmName, "140 662 0"];
      //yield return [Algorithms.Modulus11Extended, Algorithms.Modulus11Extended.AlgorithmName, "140 662 538 8"];
   }

   [Benchmark]
   [ArgumentsSource(nameof(TryCalculateCheckDigitArguments))]
   public void TryCalculateCheckDigit(ISingleCheckDigitAlgorithm algorithm, String name, String value)
   {
      algorithm.TryCalculateCheckDigit(value, out var checkDigit);
   }

   //[Benchmark]
   //[ArgumentsSource(nameof(TryCalculateCheckDigitsArguments))]
   //public void TryCalculateCheckDigits(IDoubleCheckDigitAlgorithm algorithm, String name, String value)
   //{
   //   algorithm.TryCalculateCheckDigits(value, out var first, out var second);
   //}

   [Benchmark]
   [ArgumentsSource(nameof(ValidateArguments))]
   public void Validate(ICheckDigitAlgorithm algorithm, String name, String value)
   {
      algorithm.Validate(value);
   }

   [Benchmark]
   [ArgumentsSource(nameof(ValidateMaskedArguments))]
   public void ValidateMasked(IMaskedCheckDigitAlgorithm algorithm, String name, String value)
   {
      algorithm.Validate(value, _groupsOfThreeMask);
   }
}
