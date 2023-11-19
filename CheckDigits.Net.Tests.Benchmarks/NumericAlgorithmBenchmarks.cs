namespace CheckDigits.Net.Tests.Benchmarks;

[SimpleJob(RuntimeMoniker.Net70, baseline: true)]
[SimpleJob(RuntimeMoniker.Net80)]
[MemoryDiagnoser]
public class NumericAlgorithmBenchmarks
{
   public IEnumerable<Object[]> TryCalculateCheckDigitArguments()
   {
      yield return new Object[] { Algorithms.Damm, Algorithms.Damm.AlgorithmName, "140" };
      yield return new Object[] { Algorithms.Damm, Algorithms.Damm.AlgorithmName, "140662" };
      yield return new Object[] { Algorithms.Damm, Algorithms.Damm.AlgorithmName, "140662538" };
      yield return new Object[] { Algorithms.Damm, Algorithms.Damm.AlgorithmName, "140662538042" };
      yield return new Object[] { Algorithms.Damm, Algorithms.Damm.AlgorithmName, "140662538042551" };
      yield return new Object[] { Algorithms.Damm, Algorithms.Damm.AlgorithmName, "140662538042551028" };
      yield return new Object[] { Algorithms.Damm, Algorithms.Damm.AlgorithmName, "140662538042551028265" };

      yield return new Object[] { Algorithms.Iso7064Mod11_10, Algorithms.Iso7064Mod11_10.AlgorithmName, "140" };
      yield return new Object[] { Algorithms.Iso7064Mod11_10, Algorithms.Iso7064Mod11_10.AlgorithmName, "140662" };
      yield return new Object[] { Algorithms.Iso7064Mod11_10, Algorithms.Iso7064Mod11_10.AlgorithmName, "140662538" };
      yield return new Object[] { Algorithms.Iso7064Mod11_10, Algorithms.Iso7064Mod11_10.AlgorithmName, "140662538042" };
      yield return new Object[] { Algorithms.Iso7064Mod11_10, Algorithms.Iso7064Mod11_10.AlgorithmName, "140662538042551" };
      yield return new Object[] { Algorithms.Iso7064Mod11_10, Algorithms.Iso7064Mod11_10.AlgorithmName, "140662538042551028" };
      yield return new Object[] { Algorithms.Iso7064Mod11_10, Algorithms.Iso7064Mod11_10.AlgorithmName, "140662538042551028265" };

      yield return new Object[] { Algorithms.Iso7064Mod11_2, Algorithms.Iso7064Mod11_2.AlgorithmName, "140" };
      yield return new Object[] { Algorithms.Iso7064Mod11_2, Algorithms.Iso7064Mod11_2.AlgorithmName, "140662" };
      yield return new Object[] { Algorithms.Iso7064Mod11_2, Algorithms.Iso7064Mod11_2.AlgorithmName, "140662538" };
      yield return new Object[] { Algorithms.Iso7064Mod11_2, Algorithms.Iso7064Mod11_2.AlgorithmName, "140662538042" };
      yield return new Object[] { Algorithms.Iso7064Mod11_2, Algorithms.Iso7064Mod11_2.AlgorithmName, "140662538042551" };
      yield return new Object[] { Algorithms.Iso7064Mod11_2, Algorithms.Iso7064Mod11_2.AlgorithmName, "140662538042551028" };
      yield return new Object[] { Algorithms.Iso7064Mod11_2, Algorithms.Iso7064Mod11_2.AlgorithmName, "140662538042551028265" };

      yield return new Object[] { Algorithms.Luhn, Algorithms.Luhn.AlgorithmName, "140" };
      yield return new Object[] { Algorithms.Luhn, Algorithms.Luhn.AlgorithmName, "140662" };
      yield return new Object[] { Algorithms.Luhn, Algorithms.Luhn.AlgorithmName, "140662538" };
      yield return new Object[] { Algorithms.Luhn, Algorithms.Luhn.AlgorithmName, "140662538042" };
      yield return new Object[] { Algorithms.Luhn, Algorithms.Luhn.AlgorithmName, "140662538042551" };
      yield return new Object[] { Algorithms.Luhn, Algorithms.Luhn.AlgorithmName, "140662538042551028" };
      yield return new Object[] { Algorithms.Luhn, Algorithms.Luhn.AlgorithmName, "140662538042551028265" };

      yield return new Object[] { Algorithms.Modulus10_1, Algorithms.Modulus10_1.AlgorithmName, "140" };
      yield return new Object[] { Algorithms.Modulus10_1, Algorithms.Modulus10_1.AlgorithmName, "140662" };
      yield return new Object[] { Algorithms.Modulus10_1, Algorithms.Modulus10_1.AlgorithmName, "140662538" };

      yield return new Object[] { Algorithms.Modulus10_2, Algorithms.Modulus10_2.AlgorithmName, "140" };
      yield return new Object[] { Algorithms.Modulus10_2, Algorithms.Modulus10_2.AlgorithmName, "140662" };
      yield return new Object[] { Algorithms.Modulus10_2, Algorithms.Modulus10_2.AlgorithmName, "140662538" };

      yield return new Object[] { Algorithms.Modulus10_13, Algorithms.Modulus10_13.AlgorithmName, "140" };
      yield return new Object[] { Algorithms.Modulus10_13, Algorithms.Modulus10_13.AlgorithmName, "140662" };
      yield return new Object[] { Algorithms.Modulus10_13, Algorithms.Modulus10_13.AlgorithmName, "140662538" };
      yield return new Object[] { Algorithms.Modulus10_13, Algorithms.Modulus10_13.AlgorithmName, "140662538042" };
      yield return new Object[] { Algorithms.Modulus10_13, Algorithms.Modulus10_13.AlgorithmName, "140662538042551" };
      yield return new Object[] { Algorithms.Modulus10_13, Algorithms.Modulus10_13.AlgorithmName, "140662538042551028" };
      yield return new Object[] { Algorithms.Modulus10_13, Algorithms.Modulus10_13.AlgorithmName, "140662538042551028265" };

      yield return new Object[] { Algorithms.Modulus11, Algorithms.Modulus11.AlgorithmName, "140" };
      yield return new Object[] { Algorithms.Modulus11, Algorithms.Modulus11.AlgorithmName, "140662" };
      yield return new Object[] { Algorithms.Modulus11, Algorithms.Modulus11.AlgorithmName, "140662538" };

      yield return new Object[] { Algorithms.Verhoeff, Algorithms.Verhoeff.AlgorithmName, "140" };
      yield return new Object[] { Algorithms.Verhoeff, Algorithms.Verhoeff.AlgorithmName, "140662" };
      yield return new Object[] { Algorithms.Verhoeff, Algorithms.Verhoeff.AlgorithmName, "140662538" };
      yield return new Object[] { Algorithms.Verhoeff, Algorithms.Verhoeff.AlgorithmName, "140662538042" };
      yield return new Object[] { Algorithms.Verhoeff, Algorithms.Verhoeff.AlgorithmName, "140662538042551" };
      yield return new Object[] { Algorithms.Verhoeff, Algorithms.Verhoeff.AlgorithmName, "140662538042551028" };
      yield return new Object[] { Algorithms.Verhoeff, Algorithms.Verhoeff.AlgorithmName, "140662538042551028265" };
   }

   public IEnumerable<Object[]> TryCalculateCheckDigitsArguments()
   {
      yield return new Object[] { Algorithms.Iso7064Mod97_10, Algorithms.Iso7064Mod97_10.AlgorithmName, "140" };
      yield return new Object[] { Algorithms.Iso7064Mod97_10, Algorithms.Iso7064Mod97_10.AlgorithmName, "140662" };
      yield return new Object[] { Algorithms.Iso7064Mod97_10, Algorithms.Iso7064Mod97_10.AlgorithmName, "140662538" };
      yield return new Object[] { Algorithms.Iso7064Mod97_10, Algorithms.Iso7064Mod97_10.AlgorithmName, "140662538042" };
      yield return new Object[] { Algorithms.Iso7064Mod97_10, Algorithms.Iso7064Mod97_10.AlgorithmName, "140662538042551" };
      yield return new Object[] { Algorithms.Iso7064Mod97_10, Algorithms.Iso7064Mod97_10.AlgorithmName, "140662538042551028" };
      yield return new Object[] { Algorithms.Iso7064Mod97_10, Algorithms.Iso7064Mod97_10.AlgorithmName, "140662538042551028265" };
   }

   public IEnumerable<Object[]> ValidateArguments()
   {
      yield return new Object[] { Algorithms.Damm, Algorithms.Damm.AlgorithmName, "1402" };
      yield return new Object[] { Algorithms.Damm, Algorithms.Damm.AlgorithmName, "1406622" };
      yield return new Object[] { Algorithms.Damm, Algorithms.Damm.AlgorithmName, "1406625388" };
      yield return new Object[] { Algorithms.Damm, Algorithms.Damm.AlgorithmName, "1406625380422" };
      yield return new Object[] { Algorithms.Damm, Algorithms.Damm.AlgorithmName, "1406625380425518" };
      yield return new Object[] { Algorithms.Damm, Algorithms.Damm.AlgorithmName, "1406625380425510280" };
      yield return new Object[] { Algorithms.Damm, Algorithms.Damm.AlgorithmName, "1406625380425510282654" };

      yield return new Object[] { Algorithms.Iso7064Mod11_10, Algorithms.Iso7064Mod11_10.AlgorithmName, "1409" };
      yield return new Object[] { Algorithms.Iso7064Mod11_10, Algorithms.Iso7064Mod11_10.AlgorithmName, "1406623" };
      yield return new Object[] { Algorithms.Iso7064Mod11_10, Algorithms.Iso7064Mod11_10.AlgorithmName, "1406625381" };
      yield return new Object[] { Algorithms.Iso7064Mod11_10, Algorithms.Iso7064Mod11_10.AlgorithmName, "1406625380426" };
      yield return new Object[] { Algorithms.Iso7064Mod11_10, Algorithms.Iso7064Mod11_10.AlgorithmName, "1406625380425514" };
      yield return new Object[] { Algorithms.Iso7064Mod11_10, Algorithms.Iso7064Mod11_10.AlgorithmName, "1406625380425510286" };
      yield return new Object[] { Algorithms.Iso7064Mod11_10, Algorithms.Iso7064Mod11_10.AlgorithmName, "1406625380425510282657" };

      yield return new Object[] { Algorithms.Iso7064Mod11_2, Algorithms.Iso7064Mod11_2.AlgorithmName, "140X" };
      yield return new Object[] { Algorithms.Iso7064Mod11_2, Algorithms.Iso7064Mod11_2.AlgorithmName, "1406628" };
      yield return new Object[] { Algorithms.Iso7064Mod11_2, Algorithms.Iso7064Mod11_2.AlgorithmName, "1406625380" };
      yield return new Object[] { Algorithms.Iso7064Mod11_2, Algorithms.Iso7064Mod11_2.AlgorithmName, "1406625380426" };
      yield return new Object[] { Algorithms.Iso7064Mod11_2, Algorithms.Iso7064Mod11_2.AlgorithmName, "1406625380425511" };
      yield return new Object[] { Algorithms.Iso7064Mod11_2, Algorithms.Iso7064Mod11_2.AlgorithmName, "140662538042551028X" };
      yield return new Object[] { Algorithms.Iso7064Mod11_2, Algorithms.Iso7064Mod11_2.AlgorithmName, "1406625380425510282651" };

      yield return new Object[] { Algorithms.Iso7064Mod97_10, Algorithms.Iso7064Mod97_10.AlgorithmName, "14066" };
      yield return new Object[] { Algorithms.Iso7064Mod97_10, Algorithms.Iso7064Mod97_10.AlgorithmName, "14066262" };
      yield return new Object[] { Algorithms.Iso7064Mod97_10, Algorithms.Iso7064Mod97_10.AlgorithmName, "14066253823" };
      yield return new Object[] { Algorithms.Iso7064Mod97_10, Algorithms.Iso7064Mod97_10.AlgorithmName, "14066253804250" };
      yield return new Object[] { Algorithms.Iso7064Mod97_10, Algorithms.Iso7064Mod97_10.AlgorithmName, "14066253804255112" };
      yield return new Object[] { Algorithms.Iso7064Mod97_10, Algorithms.Iso7064Mod97_10.AlgorithmName, "14066253804255102853" };
      yield return new Object[] { Algorithms.Iso7064Mod97_10, Algorithms.Iso7064Mod97_10.AlgorithmName, "14066253804255102826587" };

      yield return new Object[] { Algorithms.Luhn, Algorithms.Luhn.AlgorithmName, "1404" };
      yield return new Object[] { Algorithms.Luhn, Algorithms.Luhn.AlgorithmName, "1406628" };
      yield return new Object[] { Algorithms.Luhn, Algorithms.Luhn.AlgorithmName, "1406625382" };
      yield return new Object[] { Algorithms.Luhn, Algorithms.Luhn.AlgorithmName, "1406625380421" };
      yield return new Object[] { Algorithms.Luhn, Algorithms.Luhn.AlgorithmName, "1406625380425514" };
      yield return new Object[] { Algorithms.Luhn, Algorithms.Luhn.AlgorithmName, "1406625380425510285" };
      yield return new Object[] { Algorithms.Luhn, Algorithms.Luhn.AlgorithmName, "1406625380425510282651" };

      yield return new Object[] { Algorithms.Modulus10_1, Algorithms.Modulus10_1.AlgorithmName, "1401" };
      yield return new Object[] { Algorithms.Modulus10_1, Algorithms.Modulus10_1.AlgorithmName, "1406628" };
      yield return new Object[] { Algorithms.Modulus10_1, Algorithms.Modulus10_1.AlgorithmName, "1406625384" };

      yield return new Object[] { Algorithms.Modulus10_2, Algorithms.Modulus10_2.AlgorithmName, "1406" };
      yield return new Object[] { Algorithms.Modulus10_2, Algorithms.Modulus10_2.AlgorithmName, "1406627" };
      yield return new Object[] { Algorithms.Modulus10_2, Algorithms.Modulus10_2.AlgorithmName, "1406625389" };

      yield return new Object[] { Algorithms.Modulus10_13, Algorithms.Modulus10_13.AlgorithmName, "1403" };
      yield return new Object[] { Algorithms.Modulus10_13, Algorithms.Modulus10_13.AlgorithmName, "1406627" };
      yield return new Object[] { Algorithms.Modulus10_13, Algorithms.Modulus10_13.AlgorithmName, "1406625385" };
      yield return new Object[] { Algorithms.Modulus10_13, Algorithms.Modulus10_13.AlgorithmName, "1406625380425" };
      yield return new Object[] { Algorithms.Modulus10_13, Algorithms.Modulus10_13.AlgorithmName, "1406625380425518" };
      yield return new Object[] { Algorithms.Modulus10_13, Algorithms.Modulus10_13.AlgorithmName, "1406625380425510288" };
      yield return new Object[] { Algorithms.Modulus10_13, Algorithms.Modulus10_13.AlgorithmName, "1406625380425510282657" };

      yield return new Object[] { Algorithms.Modulus11, Algorithms.Modulus11.AlgorithmName, "1406" };
      yield return new Object[] { Algorithms.Modulus11, Algorithms.Modulus11.AlgorithmName, "1406625" };
      yield return new Object[] { Algorithms.Modulus11, Algorithms.Modulus11.AlgorithmName, "1406625388" };

      yield return new Object[] { Algorithms.Verhoeff, Algorithms.Verhoeff.AlgorithmName, "1401" };
      yield return new Object[] { Algorithms.Verhoeff, Algorithms.Verhoeff.AlgorithmName, "1406625" };
      yield return new Object[] { Algorithms.Verhoeff, Algorithms.Verhoeff.AlgorithmName, "1406625388" };
      yield return new Object[] { Algorithms.Verhoeff, Algorithms.Verhoeff.AlgorithmName, "1406625380426" };
      yield return new Object[] { Algorithms.Verhoeff, Algorithms.Verhoeff.AlgorithmName, "1406625380425512" };
      yield return new Object[] { Algorithms.Verhoeff, Algorithms.Verhoeff.AlgorithmName, "1406625380425510285" };
      yield return new Object[] { Algorithms.Verhoeff, Algorithms.Verhoeff.AlgorithmName, "1406625380425510282655" };
   }

   [Benchmark]
   [ArgumentsSource(nameof(TryCalculateCheckDigitArguments))]
   public void TryCalculateCheckDigit(ISingleCheckDigitAlgorithm algorithm, String name, String value)
   {
      algorithm.TryCalculateCheckDigit(value, out var checkDigit);
   }

   [Benchmark]
   [ArgumentsSource(nameof(TryCalculateCheckDigitsArguments))]
   public void TryCalculateCheckDigits(IDoubleCheckDigitAlgorithm algorithm, String name, String value)
   {
      algorithm.TryCalculateCheckDigits(value, out var first, out var second);
   }

   [Benchmark]
   [ArgumentsSource(nameof(ValidateArguments))]
   public void Validate(ICheckDigitAlgorithm algorithm, String name, String value)
   {
      algorithm.Validate(value);
   }
}
