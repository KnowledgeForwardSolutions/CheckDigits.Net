#pragma warning disable IDE0022 // Use expression body for method
#pragma warning disable IDE0059 // Unnecessary assignment of a value
#pragma warning disable IDE0060 // Remove unused parameter if it is not part of shipped public API

using CheckDigits.Net.ValueSpecificAlgorithms;

namespace CheckDigits.Net.Tests.Benchmarks;

//[SimpleJob(RuntimeMoniker.Net70, baseline: true)]
//[SimpleJob(RuntimeMoniker.Net80)]
[MemoryDiagnoser]
public class ValueSpecificAlgorithmBenchmarks
{
   public static IEnumerable<Object[]> TryCalculateCheckDigitArguments()
   {
      yield return new Object[] { Algorithms.Isin, Algorithms.Isin.AlgorithmName, "US037833100" };
      yield return new Object[] { Algorithms.Isin, Algorithms.Isin.AlgorithmName, "AU0000XVGZA" };
      yield return new Object[] { Algorithms.Isin, Algorithms.Isin.AlgorithmName, "GB000263494" };

      yield return new Object[] { Algorithms.Iso6346, Algorithms.Iso6346.AlgorithmName, "CSQU305438" };
      yield return new Object[] { Algorithms.Iso6346, Algorithms.Iso6346.AlgorithmName, "TOLU473478" };
      yield return new Object[] { Algorithms.Iso6346, Algorithms.Iso6346.AlgorithmName, "MSKU907032" };

      yield return new Object[] { Algorithms.Vin, Algorithms.Vin.AlgorithmName, "1M8GDM9A_KP042788" };
      yield return new Object[] { Algorithms.Vin, Algorithms.Vin.AlgorithmName, "1G8ZG127_WZ157259" };
      yield return new Object[] { Algorithms.Vin, Algorithms.Vin.AlgorithmName, "1HGEM212_2L047875" };
   }

   public static IEnumerable<Object[]> TryCalculateCheckDigitsArguments()
   {
      yield return new Object[] { Algorithms.Iban, Algorithms.Iban.AlgorithmName, "BE00096123456769" };
      yield return new Object[] { Algorithms.Iban, Algorithms.Iban.AlgorithmName, "GB00WEST12345698765432" };
      yield return new Object[] { Algorithms.Iban, Algorithms.Iban.AlgorithmName, "SC00MCBL01031234567890123456USD" };
   }

   public static IEnumerable<Object[]> ValidateArguments()
   {
      yield return new Object[] { Algorithms.AbaRtn, Algorithms.AbaRtn.AlgorithmName, "111000025" };
      yield return new Object[] { Algorithms.AbaRtn, Algorithms.AbaRtn.AlgorithmName, "122235821" };
      yield return new Object[] { Algorithms.AbaRtn, Algorithms.AbaRtn.AlgorithmName, "325081403" };

      yield return new Object[] { Algorithms.Cusip, Algorithms.Cusip.AlgorithmName, "037833100" };
      yield return new Object[] { Algorithms.Cusip, Algorithms.Cusip.AlgorithmName, "38143VAA7" };
      yield return new Object[] { Algorithms.Cusip, Algorithms.Cusip.AlgorithmName, "91282CJL6" };

      yield return new Object[] { Algorithms.Iban, Algorithms.Iban.AlgorithmName, "BE71096123456769" };
      yield return new Object[] { Algorithms.Iban, Algorithms.Iban.AlgorithmName, "GB82WEST12345698765432" };
      yield return new Object[] { Algorithms.Iban, Algorithms.Iban.AlgorithmName, "SC74MCBL01031234567890123456USD" };

      yield return new Object[] { Algorithms.Icao9303, Algorithms.Icao9303.AlgorithmName, "U7Y5" };
      yield return new Object[] { Algorithms.Icao9303, Algorithms.Icao9303.AlgorithmName, "U7Y8SX8" };
      yield return new Object[] { Algorithms.Icao9303, Algorithms.Icao9303.AlgorithmName, "U7Y8SXRC03" };
      yield return new Object[] { Algorithms.Icao9303, Algorithms.Icao9303.AlgorithmName, "U7Y8SXRC0O3S8" };
      yield return new Object[] { Algorithms.Icao9303, Algorithms.Icao9303.AlgorithmName, "U7Y8SXRC0O3SC4I2" };
      yield return new Object[] { Algorithms.Icao9303, Algorithms.Icao9303.AlgorithmName, "U7Y8SXRC0O3SC4IHYQ9" };
      yield return new Object[] { Algorithms.Icao9303, Algorithms.Icao9303.AlgorithmName, "U7Y8SXRC0O3SC4IHYQF4M8" };

      yield return new Object[] { Algorithms.Isan, Algorithms.Isan.AlgorithmName, "D02C42E954183EE2Q1291C8AEO" };
      yield return new Object[] { Algorithms.Isan, Algorithms.Isan.AlgorithmName, "C594660A8B2E5D22X6DDA3272E" };
      yield return new Object[] { Algorithms.Isan, Algorithms.Isan.AlgorithmName, "E9530C32BC0EE83B269867B20F" };

      yield return new Object[] { Algorithms.Iso6346, Algorithms.Iso6346.AlgorithmName, "CSQU3054383" };
      yield return new Object[] { Algorithms.Iso6346, Algorithms.Iso6346.AlgorithmName, "TOLU4734787" };
      yield return new Object[] { Algorithms.Iso6346, Algorithms.Iso6346.AlgorithmName, "MSKU9070323" };

      yield return new Object[] { Algorithms.Isin, Algorithms.Isin.AlgorithmName, "US0378331005" };
      yield return new Object[] { Algorithms.Isin, Algorithms.Isin.AlgorithmName, "AU0000XVGZA3" };
      yield return new Object[] { Algorithms.Isin, Algorithms.Isin.AlgorithmName, "GB0002634946" };

      yield return new Object[] { Algorithms.Nhs, Algorithms.Nhs.AlgorithmName, "9434765919" };
      yield return new Object[] { Algorithms.Nhs, Algorithms.Nhs.AlgorithmName, "4505577104" };
      yield return new Object[] { Algorithms.Nhs, Algorithms.Nhs.AlgorithmName, "5301194917" };

      yield return new Object[] { Algorithms.Npi, Algorithms.Npi.AlgorithmName, "1234567893" };
      yield return new Object[] { Algorithms.Npi, Algorithms.Npi.AlgorithmName, "1245319599" };
      yield return new Object[] { Algorithms.Npi, Algorithms.Npi.AlgorithmName, "1122337797" };

      yield return new Object[] { Algorithms.Sedol, Algorithms.Sedol.AlgorithmName, "3134865" };
      yield return new Object[] { Algorithms.Sedol, Algorithms.Sedol.AlgorithmName, "B0YQ5W0" };
      yield return new Object[] { Algorithms.Sedol, Algorithms.Sedol.AlgorithmName, "BRDVMH9" };

      yield return new Object[] { Algorithms.Vin, Algorithms.Vin.AlgorithmName, "1M8GDM9AXKP042788" };
      yield return new Object[] { Algorithms.Vin, Algorithms.Vin.AlgorithmName, "1G8ZG127XWZ157259" };
      yield return new Object[] { Algorithms.Vin, Algorithms.Vin.AlgorithmName, "1HGEM21292L047875" };
   }

   public static IEnumerable<Object[]> ValidateEmbeddedArguments()
   {
      yield return new Object[] { Algorithms.Icao9303, Algorithms.Icao9303.AlgorithmName, "+U7Y5+", 4 };
      yield return new Object[] { Algorithms.Icao9303, Algorithms.Icao9303.AlgorithmName, "+U7Y8SX8+", 8 };
      yield return new Object[] { Algorithms.Icao9303, Algorithms.Icao9303.AlgorithmName, "+U7Y8SXRC03+", 10 };
      yield return new Object[] { Algorithms.Icao9303, Algorithms.Icao9303.AlgorithmName, "+U7Y8SXRC0O3S8+", 13 };
      yield return new Object[] { Algorithms.Icao9303, Algorithms.Icao9303.AlgorithmName, "+U7Y8SXRC0O3SC4I2+", 16 };
      yield return new Object[] { Algorithms.Icao9303, Algorithms.Icao9303.AlgorithmName, "+U7Y8SXRC0O3SC4IHYQ9+", 19 };
      yield return new Object[] { Algorithms.Icao9303, Algorithms.Icao9303.AlgorithmName, "+U7Y8SXRC0O3SC4IHYQF4M8+", 22 };
   }

   public static IEnumerable<Object[]> ValidateFormattedArguments()
   {
      yield return new Object[] { Algorithms.Isan, Algorithms.Isan.AlgorithmName, "ISAN D02C-42E9-5418-3EE2-Q" };
      yield return new Object[] { Algorithms.Isan, Algorithms.Isan.AlgorithmName, "ISAN C594-660A-8B2E-5D22-X" };
      yield return new Object[] { Algorithms.Isan, Algorithms.Isan.AlgorithmName, "ISAN E953-0C32-BC0E-E83B-2" };
      yield return new Object[] { Algorithms.Isan, Algorithms.Isan.AlgorithmName, "ISAN D02C-42E9-5418-3EE2-Q-1291-C8AE-O" };
      yield return new Object[] { Algorithms.Isan, Algorithms.Isan.AlgorithmName, "ISAN C594-660A-8B2E-5D22-X-6DDA-3272-E" };
      yield return new Object[] { Algorithms.Isan, Algorithms.Isan.AlgorithmName, "ISAN E953-0C32-BC0E-E83B-2-6986-7B20-F" };
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

   [Benchmark]
   [ArgumentsSource(nameof(ValidateEmbeddedArguments))]
   public void ValidateEmbedded(IEmbeddedCheckDigitAlgorithm algorithm, String name, String value, Int32 length)
   {
      algorithm.Validate(value, 1, length);
   }

   [Benchmark]
   [ArgumentsSource(nameof(ValidateFormattedArguments))]
   public void ValidateFormatted(IsanAlgorithm algorithm, String name, String value)
   {
      algorithm.ValidateFormatted(value);
   }
}
