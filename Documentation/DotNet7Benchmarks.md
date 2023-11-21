## Benchmarks (DotNet 7)

The methodology for the general algorithms is to generate values for the benchmarks
by taking substrings of lengths 3, 6, 9, etc. from the same randomly generated 
source string. For the TryCalculateCheckDigit or TryCalculateCheckDigits methods 
the substring is used as is. For the Validate method benchmarks the substring is 
appended with the check character or characters that make the test value valid 
for the algorithm being benchmarked.

For value specific algorithms, three separate values that are valid for the 
algorithm being benchmarked are used.

#### Benchmark Details

BenchmarkDotNet v0.13.10, Windows 11 (10.0.22621.2715/22H2/2022Update/SunValley2)
Intel Core i7-8700K CPU 3.70GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET SDK 8.0.100
  [Host]     : .NET 7.0.14 (7.0.1423.51910), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.14 (7.0.1423.51910), X64 RyuJIT AVX2

### TryCalculateCheckDigit/TryCalculateCheckDigits Methods

#### General Numeric Algorithms

Note that the Modulus10_1, Modulus10_2 and Modulus11 algorithms have a maximum 
length of 10 (including the check digit) for values being validated so their
benchmarks do not cover lengths greater than 10.

| Algorithm Name    | Value                 | Mean      | Error     | StdDev    | Allocated |
|------------------ |---------------------- |----------:|----------:|----------:|----------:|
| Damm              | 140                   |  4.949 ns | 0.1024 ns | 0.0908 ns |         - |
| Damm              | 140662                |  8.825 ns | 0.0712 ns | 0.0666 ns |         - |
| Damm              | 140662538             | 14.690 ns | 0.2001 ns | 0.1774 ns |         - |
| Damm              | 140662538042          | 19.815 ns | 0.1659 ns | 0.1471 ns |         - |
| Damm              | 140662538042551       | 24.766 ns | 0.1932 ns | 0.1713 ns |         - |
| Damm              | 140662538042551028    | 29.666 ns | 0.1946 ns | 0.1725 ns |         - |
| Damm              | 140662538042551028265 | 35.442 ns | 0.5445 ns | 0.5093 ns |         - |
|                   |                       |           |           |           |           |                                           
| ISO/IEC 706 11,10 | 140                   |  7.461 ns | 0.0557 ns | 0.0465 ns |         - |
| ISO/IEC 706 11,10 | 140662                | 10.676 ns | 0.0782 ns | 0.0731 ns |         - |
| ISO/IEC 706 11,10 | 140662538             | 14.477 ns | 0.1290 ns | 0.1144 ns |         - |
| ISO/IEC 706 11,10 | 140662538042          | 18.263 ns | 0.2106 ns | 0.1970 ns |         - |
| ISO/IEC 706 11,10 | 140662538042551       | 21.087 ns | 0.1063 ns | 0.0942 ns |         - |
| ISO/IEC 706 11,10 | 140662538042551028    | 23.131 ns | 0.2303 ns | 0.2042 ns |         - |
| ISO/IEC 706 11,10 | 140662538042551028265 | 24.776 ns | 0.1663 ns | 0.1474 ns |         - |
|                   |                       |           |           |           |           |                                           
| ISO/IEC 706 11-2  | 140                   |  6.268 ns | 0.1064 ns | 0.0995 ns |         - |
| ISO/IEC 706 11-2  | 140662                | 10.061 ns | 0.1256 ns | 0.1175 ns |         - |
| ISO/IEC 706 11-2  | 140662538             | 13.671 ns | 0.2918 ns | 0.2730 ns |         - |
| ISO/IEC 706 11-2  | 140662538042          | 17.033 ns | 0.1795 ns | 0.1679 ns |         - |
| ISO/IEC 706 11-2  | 140662538042551       | 20.707 ns | 0.2469 ns | 0.2189 ns |         - |
| ISO/IEC 706 11-2  | 140662538042551028    | 24.420 ns | 0.1823 ns | 0.1616 ns |         - |
| ISO/IEC 706 11-2  | 140662538042551028265 | 27.453 ns | 0.1923 ns | 0.1799 ns |         - |
|                   |                       |           |           |           |           |                                           
| ISO/IEC 706 97-10 | 140                   |  6.853 ns | 0.1224 ns | 0.1085 ns |         - |
| ISO/IEC 706 97-10 | 140662                | 10.487 ns | 0.1345 ns | 0.1258 ns |         - |
| ISO/IEC 706 97-10 | 140662538             | 15.189 ns | 0.1493 ns | 0.1247 ns |         - |
| ISO/IEC 706 97-10 | 140662538042          | 18.234 ns | 0.1331 ns | 0.1180 ns |         - |
| ISO/IEC 706 97-10 | 140662538042551       | 21.893 ns | 0.3501 ns | 0.3275 ns |         - |
| ISO/IEC 706 97-10 | 140662538042551028    | 25.735 ns | 0.2219 ns | 0.2076 ns |         - |
| ISO/IEC 706 97-10 | 140662538042551028265 | 28.758 ns | 0.1250 ns | 0.0976 ns |         - |
|                   |                       |           |           |           |           |                                           
| Luhn              | 140                   |  7.013 ns | 0.1099 ns | 0.1028 ns |         - |
| Luhn              | 140662                | 10.537 ns | 0.1623 ns | 0.1518 ns |         - |
| Luhn              | 140662538             | 13.909 ns | 0.1060 ns | 0.0991 ns |         - |
| Luhn              | 140662538042          | 17.530 ns | 0.1428 ns | 0.1266 ns |         - |
| Luhn              | 140662538042551       | 21.001 ns | 0.2169 ns | 0.2029 ns |         - |
| Luhn              | 140662538042551028    | 24.310 ns | 0.2837 ns | 0.2654 ns |         - |
| Luhn              | 140662538042551028265 | 27.940 ns | 0.2464 ns | 0.2184 ns |         - |
|                   |                       |           |           |           |           |                                           
| Modulus10_13      | 140                   |  6.798 ns | 0.1453 ns | 0.1359 ns |         - |
| Modulus10_13      | 140662                | 10.110 ns | 0.2074 ns | 0.1940 ns |         - |
| Modulus10_13      | 140662538             | 12.569 ns | 0.1022 ns | 0.0853 ns |         - |
| Modulus10_13      | 140662538042          | 16.103 ns | 0.1472 ns | 0.1229 ns |         - |
| Modulus10_13      | 140662538042551       | 18.845 ns | 0.2321 ns | 0.2171 ns |         - |
| Modulus10_13      | 140662538042551028    | 22.300 ns | 0.1369 ns | 0.1214 ns |         - |
| Modulus10_13      | 140662538042551028265 | 25.200 ns | 0.2025 ns | 0.1795 ns |         - |
|                   |                       |           |           |           |           |                                           
| Modulus10_1       | 140                   |  4.139 ns | 0.0645 ns | 0.0603 ns |         - |
| Modulus10_1       | 140662                |  5.800 ns | 0.0984 ns | 0.0920 ns |         - |
| Modulus10_1       | 140662538             |  7.435 ns | 0.0742 ns | 0.0620 ns |         - |
|                   |                       |           |           |           |           |                                           
| Modulus10_2       | 140                   |  3.952 ns | 0.0486 ns | 0.0431 ns |         - |
| Modulus10_2       | 140662                |  5.621 ns | 0.1259 ns | 0.1178 ns |         - |
| Modulus10_2       | 140662538             |  7.305 ns | 0.0875 ns | 0.0776 ns |         - |
|                   |                       |           |           |           |           |                                           
| Modulus11         | 140                   |  4.415 ns | 0.0479 ns | 0.0400 ns |         - |
| Modulus11         | 140662                |  6.528 ns | 0.1075 ns | 0.1005 ns |         - |
| Modulus11         | 140662538             |  7.811 ns | 0.1584 ns | 0.1945 ns |         - |
|                   |                       |           |           |           |           |                                           
| Verhoeff          | 140                   | 10.665 ns | 0.1498 ns | 0.1402 ns |         - |
| Verhoeff          | 140662                | 18.160 ns | 0.0953 ns | 0.0796 ns |         - |
| Verhoeff          | 140662538             | 25.813 ns | 0.1021 ns | 0.0853 ns |         - |
| Verhoeff          | 140662538042          | 33.391 ns | 0.3761 ns | 0.2936 ns |         - |
| Verhoeff          | 140662538042551       | 40.823 ns | 0.2580 ns | 0.2413 ns |         - |
| Verhoeff          | 140662538042551028    | 48.616 ns | 0.4191 ns | 0.3921 ns |         - |
| Verhoeff          | 140662538042551028265 | 56.447 ns | 0.5107 ns | 0.4777 ns |         - |

#### General Alphabetic Algorithms

| Algorithm Name          | Value                 | Mean      | Error     | StdDev    | Allocated |
|------------------------ |---------------------- |----------:|----------:|----------:|----------:|
| ISO/IEC 7064 MOD 27,26  | EGR                   |  7.323 ns | 0.0744 ns | 0.0660 ns |         - |
| ISO/IEC 7064 MOD 27,26  | EGRNML                | 10.075 ns | 0.0852 ns | 0.0711 ns |         - |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOC             | 13.165 ns | 0.1768 ns | 0.1567 ns |         - |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOCECU          | 16.666 ns | 0.1215 ns | 0.1137 ns |         - |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOCECUJIK       | 20.371 ns | 0.1702 ns | 0.1508 ns |         - |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOCECUJIKNWW    | 22.317 ns | 0.1799 ns | 0.1682 ns |         - |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOCECUJIKNWWVVO | 25.042 ns | 0.1730 ns | 0.1533 ns |         - |
|                         |                       |           |           |           |           |                                           
| ISO/IEC 7064 MOD 661-26 | EGR                   |  6.285 ns | 0.1391 ns | 0.1161 ns |         - |
| ISO/IEC 7064 MOD 661-26 | EGRNML                |  9.172 ns | 0.1611 ns | 0.1507 ns |         - |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOC             | 11.706 ns | 0.0904 ns | 0.0755 ns |         - |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOCECU          | 16.000 ns | 0.1052 ns | 0.0932 ns |         - |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOCECUJIK       | 20.669 ns | 0.2951 ns | 0.2616 ns |         - |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOCECUJIKNWW    | 23.121 ns | 0.2003 ns | 0.1874 ns |         - |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOCECUJIKNWWVVO | 27.655 ns | 0.0923 ns | 0.0771 ns |         - |

#### General Alphanumeric Algorithms

Note that the values used for the NOID Check Digit algorithm do not include lengths
3 or 6 so that benchmarks are not run on purely numeric strings.

| Algorithm Name           | Value                 | Mean      | Error     | StdDev    | Allocated |
|------------------------- |---------------------- |----------:|----------:|----------:|----------:|
| AlphanumericMod97_10     | U7y                   | 10.957 ns | 0.1313 ns | 0.1229 ns |         - |
| AlphanumericMod97_10     | U7y8SX                | 20.110 ns | 0.1744 ns | 0.1632 ns |         - |
| AlphanumericMod97_10     | U7y8SXrC0             | 27.565 ns | 0.4840 ns | 0.4528 ns |         - |
| AlphanumericMod97_10     | U7y8SXrC0O3S          | 36.799 ns | 0.2212 ns | 0.2069 ns |         - |
| AlphanumericMod97_10     | U7y8SXrC0O3Sc4I       | 43.957 ns | 0.3136 ns | 0.2933 ns |         - |
| AlphanumericMod97_10     | U7y8SXrC0O3Sc4IHYQ    | 53.902 ns | 0.3897 ns | 0.3645 ns |         - |
| AlphanumericMod97_10     | U7y8SXrC0O3Sc4IHYQF4M | 60.975 ns | 0.4534 ns | 0.4241 ns |         - |
|                          |                       |           |           |           |           |                                           
| ISO/IEC 7064 MOD 1271-36 | U7Y                   |  7.775 ns | 0.1203 ns | 0.1125 ns |         - |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SX                | 11.586 ns | 0.0878 ns | 0.0821 ns |         - |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SXRC0             | 16.392 ns | 0.1493 ns | 0.1324 ns |         - |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SXRC0O3S          | 19.021 ns | 0.1470 ns | 0.1375 ns |         - |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SXRC0O3SC4I       | 23.149 ns | 0.1528 ns | 0.1429 ns |         - |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SXRC0O3SC4IHYQ    | 27.458 ns | 0.1873 ns | 0.1752 ns |         - |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SXRC0O3SC4IHYQF4M | 32.253 ns | 0.3277 ns | 0.3066 ns |         - |
|                          |                       |           |           |           |           |                                           
| ISO/IEC 7064 MOD 37-2    | U7Y                   |  7.287 ns | 0.0465 ns | 0.0388 ns |         - |
| ISO/IEC 7064 MOD 37-2    | U7Y8SX                | 11.315 ns | 0.0584 ns | 0.0547 ns |         - |
| ISO/IEC 7064 MOD 37-2    | U7Y8SXRC0             | 15.624 ns | 0.1161 ns | 0.1086 ns |         - |
| ISO/IEC 7064 MOD 37-2    | U7Y8SXRC0O3S          | 20.231 ns | 0.1653 ns | 0.1546 ns |         - |
| ISO/IEC 7064 MOD 37-2    | U7Y8SXRC0O3SC4I       | 24.034 ns | 0.2066 ns | 0.1831 ns |         - |
| ISO/IEC 7064 MOD 37-2    | U7Y8SXRC0O3SC4IHYQ    | 28.953 ns | 0.1903 ns | 0.1589 ns |         - |
| ISO/IEC 7064 MOD 37-2    | U7Y8SXRC0O3SC4IHYQF4M | 32.639 ns | 0.4190 ns | 0.3919 ns |         - |
|                          |                       |           |           |           |           |                                           
| ISO/IEC 7064 MOD 37,36   | U7Y                   |  7.197 ns | 0.0649 ns | 0.0608 ns |         - |
| ISO/IEC 7064 MOD 37,36   | U7Y8SX                | 10.884 ns | 0.1312 ns | 0.1227 ns |         - |
| ISO/IEC 7064 MOD 37,36   | U7Y8SXRC0             | 15.222 ns | 0.1170 ns | 0.1038 ns |         - |
| ISO/IEC 7064 MOD 37,36   | U7Y8SXRC0O3S          | 19.103 ns | 0.1590 ns | 0.1488 ns |         - |
| ISO/IEC 7064 MOD 37,36   | U7Y8SXRC0O3SC4I       | 24.701 ns | 0.1302 ns | 0.1218 ns |         - |
| ISO/IEC 7064 MOD 37,36   | U7Y8SXRC0O3SC4IHYQ    | 28.522 ns | 0.3561 ns | 0.3157 ns |         - |
| ISO/IEC 7064 MOD 37,36   | U7Y8SXRC0O3SC4IHYQF4M | 33.024 ns | 0.1466 ns | 0.1300 ns |         - |
|                          |                       |           |           |           |           |                                           
| NOID Check Digit         | 11404/2h9             | 10.701 ns | 0.0572 ns | 0.0535 ns |         - |
| NOID Check Digit         | 11404/2h9tqb          | 16.175 ns | 0.1153 ns | 0.1022 ns |         - |
| NOID Check Digit         | 11404/2h9tqbxk6       | 19.212 ns | 0.1336 ns | 0.1250 ns |         - |
| NOID Check Digit         | 11404/2h9tqbxk6rw7    | 22.600 ns | 0.1498 ns | 0.1328 ns |         - |
| NOID Check Digit         | 11404/2h9tqbxk6rw7dwm | 28.120 ns | 0.1829 ns | 0.1711 ns |         - |


#### Value Specific Algorithms

Note: ABA RTN, NHS and NPI algorithms do not support calculation of check digits, 
only validation of values containing check digits.

| Algorithm Name | Value                           | Mean     | Error    | StdDev   | Allocated |
|--------------- |-------------------------------- |---------:|---------:|---------:|----------:|
| IBAN           | BE00096123456769                | 35.28 ns | 0.211 ns | 0.197 ns |         - |
| IBAN           | GB00WEST12345698765432          | 52.85 ns | 0.645 ns | 0.571 ns |         - |
| IBAN           | SC00MCBL01031234567890123456USD | 74.39 ns | 0.522 ns | 0.463 ns |         - |
|                |                                 |          |          |          |           |                                           
| ISIN           | AU0000XVGZA                     | 29.73 ns | 0.588 ns | 0.550 ns |         - |
| ISIN           | GB000263494                     | 23.10 ns | 0.253 ns | 0.237 ns |         - |
| ISIN           | US037833100                     | 23.02 ns | 0.264 ns | 0.247 ns |         - |
|                |                                 |          |          |          |           |                                           
| VIN            | 1G8ZG127_WZ157259               | 41.17 ns | 0.607 ns | 0.568 ns |         - |
| VIN            | 1HGEM212_2L047875               | 40.46 ns | 0.332 ns | 0.277 ns |         - |
| VIN            | 1M8GDM9A_KP042788               | 41.28 ns | 0.769 ns | 0.719 ns |         - |

### Validate Method

#### General Numeric Algorithms

All algorithms use a single check digit except ISO/IEC 7064 MOD 97-10 which uses
two check digits.

Note that the Modulus10_1, Modulus10_2 and Modulus11 algorithms have a maximum 
length of 10 (including the check digit) for values being validated so their
benchmarks do not cover lengths greater than 10.

| Algorithm Name         | Value                   | Mean      | Error     | StdDev    | Allocated |
|----------------------- | ----------------------- |----------:|----------:|----------:|----------:|
| Damm                   | 1402                    |  6.130 ns | 0.1318 ns | 0.1233 ns |         - |
| Damm                   | 1406622                 | 10.398 ns | 0.1068 ns | 0.0999 ns |         - |
| Damm                   | 1406625388              | 15.985 ns | 0.2725 ns | 0.2549 ns |         - |
| Damm                   | 1406625380422           | 21.212 ns | 0.2268 ns | 0.2122 ns |         - |
| Damm                   | 1406625380425518        | 26.353 ns | 0.1993 ns | 0.1864 ns |         - |
| Damm                   | 1406625380425510280     | 31.554 ns | 0.3449 ns | 0.3226 ns |         - |
| Damm                   | 1406625380425510282654  | 37.529 ns | 0.2162 ns | 0.1917 ns |         - |
|                        |                         |           |           |           |           |                                           
| ISO/IEC 7064 MOD 11,10 | 1409                    |  7.648 ns | 0.1277 ns | 0.1195 ns |         - |
| ISO/IEC 7064 MOD 11,10 | 1406623                 | 11.939 ns | 0.1648 ns | 0.1541 ns |         - |
| ISO/IEC 7064 MOD 11,10 | 1406625381              | 16.038 ns | 0.1963 ns | 0.1836 ns |         - |
| ISO/IEC 7064 MOD 11,10 | 1406625380426           | 20.212 ns | 0.2354 ns | 0.2202 ns |         - |
| ISO/IEC 7064 MOD 11,10 | 1406625380425514        | 24.405 ns | 0.2345 ns | 0.2194 ns |         - |
| ISO/IEC 7064 MOD 11,10 | 1406625380425510286     | 28.210 ns | 0.2072 ns | 0.1730 ns |         - |
| ISO/IEC 7064 MOD 11,10 | 1406625380425510282657  | 32.046 ns | 0.1599 ns | 0.1248 ns |         - |
|                        |                         |           |           |           |           |                                           
| ISO/IEC 7064 MOD 11-2  | 140X                    |  5.999 ns | 0.0537 ns | 0.0476 ns |         - |
| ISO/IEC 7064 MOD 11-2  | 1406628                 |  9.602 ns | 0.1235 ns | 0.1156 ns |         - |
| ISO/IEC 7064 MOD 11-2  | 1406625380              | 13.766 ns | 0.2956 ns | 0.3163 ns |         - |
| ISO/IEC 7064 MOD 11-2  | 1406625380426           | 16.767 ns | 0.3326 ns | 0.2949 ns |         - |
| ISO/IEC 7064 MOD 11-2  | 1406625380425511        | 20.032 ns | 0.2177 ns | 0.2037 ns |         - |
| ISO/IEC 7064 MOD 11-2  | 140662538042551028X     | 24.437 ns | 0.1978 ns | 0.1850 ns |         - |
| ISO/IEC 7064 MOD 11-2  | 1406625380425510282651  | 27.452 ns | 0.2728 ns | 0.2552 ns |         - |
|                        |                         |           |           |           |           |                                           
| ISO/IEC 7064 MOD 97-10 | 14066                   |  7.052 ns | 0.0363 ns | 0.0303 ns |         - |
| ISO/IEC 7064 MOD 97-10 | 14066262                | 10.809 ns | 0.1203 ns | 0.1125 ns |         - |
| ISO/IEC 7064 MOD 97-10 | 14066253823             | 15.062 ns | 0.2334 ns | 0.2184 ns |         - |
| ISO/IEC 7064 MOD 97-10 | 14066253804250          | 18.767 ns | 0.1066 ns | 0.0945 ns |         - |
| ISO/IEC 7064 MOD 97-10 | 14066253804255112       | 22.839 ns | 0.2047 ns | 0.1815 ns |         - |
| ISO/IEC 7064 MOD 97-10 | 14066253804255102853    | 25.885 ns | 0.1921 ns | 0.1703 ns |         - |
| ISO/IEC 7064 MOD 97-10 | 14066253804255102826587 | 29.362 ns | 0.3131 ns | 0.2928 ns |         - |
|                        |                         |           |           |           |           |                                           
| Luhn                   | 1404                    |  7.285 ns | 0.1008 ns | 0.0943 ns |         - |
| Luhn                   | 1406628                 | 11.476 ns | 0.2376 ns | 0.2106 ns |         - |
| Luhn                   | 1406625382              | 14.897 ns | 0.1170 ns | 0.1037 ns |         - |
| Luhn                   | 1406625380421           | 19.188 ns | 0.2519 ns | 0.2356 ns |         - |
| Luhn                   | 1406625380425514        | 22.925 ns | 0.2653 ns | 0.2482 ns |         - |
| Luhn                   | 1406625380425510285     | 27.039 ns | 0.3149 ns | 0.2945 ns |         - |
| Luhn                   | 1406625380425510282651  | 30.417 ns | 0.3339 ns | 0.3123 ns |         - |
|                        |                         |           |           |           |           |                                           
| Modulus10_13           | 1403                    |  7.743 ns | 0.0618 ns | 0.0548 ns |         - |
| Modulus10_13           | 1406627                 | 11.071 ns | 0.1356 ns | 0.1269 ns |         - |
| Modulus10_13           | 1406625385              | 14.656 ns | 0.1905 ns | 0.1689 ns |         - |
| Modulus10_13           | 1406625380425           | 19.068 ns | 0.2373 ns | 0.2103 ns |         - |
| Modulus10_13           | 1406625380425518        | 22.867 ns | 0.4330 ns | 0.3839 ns |         - |
| Modulus10_13           | 1406625380425510288     | 26.763 ns | 0.3102 ns | 0.2901 ns |         - |
| Modulus10_13           | 1406625380425510282657  | 29.478 ns | 0.2864 ns | 0.2391 ns |         - |
|                        |                         |           |           |           |           |                                           
| Modulus10_1            | 1401                    |  4.642 ns | 0.1087 ns | 0.1017 ns |         - |
| Modulus10_1            | 1406628                 |  6.595 ns | 0.1182 ns | 0.1048 ns |         - |
| Modulus10_1            | 1406625384              |  8.193 ns | 0.0861 ns | 0.0763 ns |         - |
|                        |                         |           |           |           |           |                                           
| Modulus10_2            | 1406                    |  5.222 ns | 0.0586 ns | 0.0489 ns |         - |
| Modulus10_2            | 1406627                 |  7.420 ns | 0.1605 ns | 0.1340 ns |         - |
| Modulus10_2            | 1406625389              |  9.537 ns | 0.1169 ns | 0.1093 ns |         - |
|                        |                         |           |           |           |           |                                           
| Modulus11              | 1406                    |  6.240 ns | 0.0799 ns | 0.0709 ns |         - |
| Modulus11              | 1406625                 |  8.356 ns | 0.0805 ns | 0.0753 ns |         - |
| Modulus11              | 1406625388              |  9.767 ns | 0.1164 ns | 0.1089 ns |         - |
|                        |                         |           |           |           |           |                                           
| Verhoeff               | 1401                    | 13.822 ns | 0.0920 ns | 0.0815 ns |         - |
| Verhoeff               | 1406625                 | 22.863 ns | 0.1531 ns | 0.1432 ns |         - |
| Verhoeff               | 1406625388              | 32.046 ns | 0.3094 ns | 0.2584 ns |         - |
| Verhoeff               | 1406625380426           | 41.280 ns | 0.4174 ns | 0.3700 ns |         - |
| Verhoeff               | 1406625380425512        | 50.391 ns | 0.5977 ns | 0.5591 ns |         - |
| Verhoeff               | 1406625380425510285     | 58.905 ns | 0.8780 ns | 0.7332 ns |         - |
| Verhoeff               | 1406625380425510282655  | 67.668 ns | 0.7107 ns | 0.6648 ns |         - |

#### General Alphabetic Algorithms

ISO/IEC 7064 MOD 27,26 uses a single check character. ISO/IEC 7064 MOD 661-26
uses two check characters.

| Algorithm Name          | Value                   | Mean      | Error     | StdDev    | Allocated |
|------------------------ |------------------------ |----------:|----------:|----------:|----------:|
| ISO/IEC 7064 MOD 27,26  | EGRS                    |  7.528 ns | 0.1329 ns | 0.1243 ns |         - |
| ISO/IEC 7064 MOD 27,26  | EGRNMLU                 | 11.776 ns | 0.1623 ns | 0.1439 ns |         - |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOCB              | 16.419 ns | 0.2130 ns | 0.1779 ns |         - |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOCECUA           | 20.097 ns | 0.1830 ns | 0.1712 ns |         - |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOCECUJIKA        | 24.309 ns | 0.2585 ns | 0.2291 ns |         - |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOCECUJIKNWWY     | 28.131 ns | 0.2375 ns | 0.2106 ns |         - |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOCECUJIKNWWVVOQ  | 32.378 ns | 0.2915 ns | 0.2727 ns |         - |
|                         |                         |           |           |           |           |                                           
| ISO/IEC 7064 MOD 661-26 | EGRSE                   |  7.655 ns | 0.0919 ns | 0.0860 ns |         - |
| ISO/IEC 7064 MOD 661-26 | EGRNMLDR                | 11.261 ns | 0.1051 ns | 0.0983 ns |         - |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOCCK             | 15.377 ns | 0.2277 ns | 0.2018 ns |         - |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOCECUZJ          | 18.679 ns | 0.2463 ns | 0.2304 ns |         - |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOCECUJIKFQ       | 22.296 ns | 0.1711 ns | 0.1429 ns |         - |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOCECUJIKNWWQN    | 26.118 ns | 0.2422 ns | 0.2147 ns |         - |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOCECUJIKNWWVVORC | 29.567 ns | 0.6012 ns | 0.6433 ns |         - |

#### General Alphanumeric Algorithms

AlphanumericMod97_10 algorithm and ISO/IEC 7064 MOD 1271-36 uses two check characters. 
ISO/IEC 7064 MOD 37-2, ISO/IEC 7064 MOD 37,36 and NOID Check Digit algorithms use a 
single check character.

Note also that the values used for the NOID Check Digit algorithm do not include lengths
3 or 6 so that benchmarks are not run on purely numeric strings.

| Algorithm Name           | Value                   | Mean      | Error     | StdDev    | Allocated |
|------------------------- |-------------------------|----------:|----------:|----------:|----------:|
| AlphanumericMod97_10     | U7y46                   | 12.354 ns | 0.0603 ns | 0.0534 ns |         - |
| AlphanumericMod97_10     | U7y8SX89                | 20.738 ns | 0.0957 ns | 0.0799 ns |         - |
| AlphanumericMod97_10     | U7y8SXrC087             | 28.463 ns | 0.2733 ns | 0.2423 ns |         - |
| AlphanumericMod97_10     | U7y8SXrC0O3S38          | 36.453 ns | 0.2528 ns | 0.2365 ns |         - |
| AlphanumericMod97_10     | U7y8SXrC0O3Sc4I27       | 44.678 ns | 0.2166 ns | 0.2026 ns |         - |
| AlphanumericMod97_10     | U7y8SXrC0O3Sc4IHYQ54    | 54.195 ns | 0.3931 ns | 0.3677 ns |         - |
| AlphanumericMod97_10     | U7y8SXrC0O3Sc4IHYQF4M21 | 61.407 ns | 0.3487 ns | 0.3262 ns |         - |
|                          |                         |           |           |           |           |                                           
| ISO/IEC 7064 MOD 1271-36 | U7YM0                   |  8.423 ns | 0.0587 ns | 0.0549 ns |         - |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SXOR                | 13.001 ns | 0.1098 ns | 0.1028 ns |         - |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SXRC0FI             | 17.476 ns | 0.1368 ns | 0.1279 ns |         - |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SXRC0O3SX4          | 21.074 ns | 0.1622 ns | 0.1517 ns |         - |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SXRC0O3SC4I9D       | 26.002 ns | 0.2291 ns | 0.2143 ns |         - |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SXRC0O3SC4IHYQYI    | 30.088 ns | 0.2033 ns | 0.1803 ns |         - |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SXRC0O3SC4IHYQF4M44 | 33.630 ns | 0.2810 ns | 0.2346 ns |         - |
|                          |                         |           |           |           |           |                                           
| ISO/IEC 7064 MOD 37-2    | U7YZ                    |  7.013 ns | 0.0817 ns | 0.0764 ns |         - |
| ISO/IEC 7064 MOD 37-2    | U7Y8SXV                 | 11.281 ns | 0.1002 ns | 0.0837 ns |         - |
| ISO/IEC 7064 MOD 37-2    | U7Y8SXRC0E              | 15.225 ns | 0.1731 ns | 0.1619 ns |         - |
| ISO/IEC 7064 MOD 37-2    | U7Y8SXRC0O3SU           | 19.720 ns | 0.1730 ns | 0.1618 ns |         - |
| ISO/IEC 7064 MOD 37-2    | U7Y8SXRC0O3SC4IB        | 24.069 ns | 0.2132 ns | 0.1994 ns |         - |
| ISO/IEC 7064 MOD 37-2    | U7Y8SXRC0O3SC4IHYQG     | 28.596 ns | 0.2424 ns | 0.2267 ns |         - |
| ISO/IEC 7064 MOD 37-2    | U7Y8SXRC0O3SC4IHYQF4MF  | 32.949 ns | 0.2376 ns | 0.2223 ns |         - |
|                          |                         |           |           |           |           |                                           
| ISO/IEC 7064 MOD 37,36   | U7YW                    |  8.342 ns | 0.0490 ns | 0.0434 ns |         - |
| ISO/IEC 7064 MOD 37,36   | U7Y8SX8                 | 12.235 ns | 0.1005 ns | 0.0940 ns |         - |
| ISO/IEC 7064 MOD 37,36   | U7Y8SXRC0E              | 16.822 ns | 0.1493 ns | 0.1323 ns |         - |
| ISO/IEC 7064 MOD 37,36   | U7Y8SXRC0O3SR           | 20.733 ns | 0.1190 ns | 0.0994 ns |         - |
| ISO/IEC 7064 MOD 37,36   | U7Y8SXRC0O3SC4IT        | 25.913 ns | 0.2772 ns | 0.2593 ns |         - |
| ISO/IEC 7064 MOD 37,36   | U7Y8SXRC0O3SC4IHYQD     | 30.133 ns | 0.3057 ns | 0.2860 ns |         - |
| ISO/IEC 7064 MOD 37,36   | U7Y8SXRC0O3SC4IHYQF4MP  | 35.250 ns | 0.2788 ns | 0.2608 ns |         - |
|                          |                         |           |           |           |           |                                           
| NOID Check Digit         | 11404/2h9m              | 13.725 ns | 0.0985 ns | 0.0873 ns |         - |
| NOID Check Digit         | 11404/2h9tqb0           | 19.307 ns | 0.1813 ns | 0.1607 ns |         - |
| NOID Check Digit         | 11404/2h9tqbxk6d        | 23.805 ns | 0.1584 ns | 0.1482 ns |         - |
| NOID Check Digit         | 11404/2h9tqbxk6rw74     | 28.676 ns | 0.3153 ns | 0.2795 ns |         - |
| NOID Check Digit         | 11404/2h9tqbxk6rw7dwmz  | 33.666 ns | 0.3178 ns | 0.2973 ns |         - |


#### Value Specific Algorithms

| Algorithm Name   | Value                                  | Mean      | Error     | StdDev    | Allocated |
|----------------- |--------------------------------------- |----------:|----------:|----------:|----------:|
| ABA RTN          | 111000025                              |  8.862 ns | 0.1623 ns | 0.1518 ns |         - |
| ABA RTN          | 122235821                              |  8.692 ns | 0.1737 ns | 0.1624 ns |         - |
| ABA RTN          | 325081403                              |  8.684 ns | 0.1237 ns | 0.1157 ns |         - |
|                  |                                        |           |           |           |           |                                           
| IBAN             | BE71096123456769                       | 22.310 ns | 0.2240 ns | 0.1980 ns |         - |
| IBAN             | GB82WEST12345698765432                 | 34.930 ns | 0.3060 ns | 0.2870 ns |         - |
| IBAN             | SC74MCBL01031234567890123456USD        | 51.930 ns | 0.8720 ns | 0.7730 ns |         - |
|                  |                                        |           |           |           |           |                                           
| ISAN             | C594660A8B2E5D22X6DDA3272E             | 57.740 ns | 0.7840 ns | 0.6950 ns |         - |
| ISAN             | D02C42E954183EE2Q1291C8AEO             | 54.680 ns | 0.7400 ns | 0.6560 ns |         - |
| ISAN             | E9530C32BC0EE83B269867B20F             | 54.830 ns | 0.6520 ns | 0.5780 ns |         - |
|                  |                                        |           |           |           |           |                                           
| ISAN (Formatted) | ISAN C594-660A-8B2E-5D22-X             | 49.680 ns | 0.5080 ns | 0.4750 ns |         - |
| ISAN (Formatted) | ISAN D02C-42E9-5418-3EE2-Q             | 49.640 ns | 0.3700 ns | 0.3460 ns |         - |
| ISAN (Formatted) | ISAN E953-0C32-BC0E-E83B-2             | 47.750 ns | 0.4370 ns | 0.4090 ns |         - |
| ISAN (Formatted) | ISAN C594-660A-8B2E-5D22-X-6DDA-3272-E | 71.010 ns | 0.9400 ns | 0.8790 ns |         - |
| ISAN (Formatted) | ISAN D02C-42E9-5418-3EE2-Q-1291-C8AE-O | 69.900 ns | 0.9790 ns | 0.8680 ns |         - |
| ISAN (Formatted) | ISAN E953-0C32-BC0E-E83B-2-6986-7B20-F | 71.490 ns | 0.9130 ns | 0.8540 ns |         - |
|                  |                                        |           |           |           |           |                                           
| ISIN             | AU0000XVGZA3                           | 25.624 ns | 0.2618 ns | 0.2449 ns |         - |
| ISIN             | GB0002634946                           | 21.148 ns | 0.2497 ns | 0.2335 ns |         - |
| ISIN             | US0378331005                           | 21.139 ns | 0.3062 ns | 0.2865 ns |         - |
|                  |                                        |           |           |           |           |                                           
| NHS              | 4505577104                             | 11.933 ns | 0.1477 ns | 0.1309 ns |         - |
| NHS              | 5301194917                             | 11.898 ns | 0.1416 ns | 0.1324 ns |         - |
| NHS              | 9434765919                             | 11.917 ns | 0.1627 ns | 0.1522 ns |         - |
|                  |                                        |           |           |           |           |                                           
| NPI              | 1122337797                             | 15.106 ns | 0.2468 ns | 0.2309 ns |         - |
| NPI              | 1234567893                             | 14.986 ns | 0.0968 ns | 0.0808 ns |         - |
| NPI              | 1245319599                             | 15.067 ns | 0.2008 ns | 0.1878 ns |         - |
|                  |                                        |           |           |           |           |                                           
| VIN              | 1G8ZG127XWZ157259                      | 40.107 ns | 0.3094 ns | 0.2743 ns |         - |
| VIN              | 1HGEM21292L047875                      | 40.206 ns | 0.2919 ns | 0.2438 ns |         - |
| VIN              | 1M8GDM9AXKP042788                      | 40.266 ns | 0.5329 ns | 0.4985 ns |         - |
