## Benchmarks (.Net 8)

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
  [Host]     : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2

### TryCalculateCheckDigit/TryCalculateCheckDigits Methods

#### General Numeric Algorithms

Note that the Modulus10_1, Modulus10_2 and Modulus11 algorithms have a maximum 
length of 10 (including the check digit) for values being validated so their
benchmarks do not cover lengths greater than 10.

| Algorithm Name    | Value                 | Mean      | Error     | StdDev    | Allocated |
|------------------ |---------------------- |----------:|----------:|----------:|----------:|
| Damm              | 140                   |  4.525 ns | 0.0571 ns | 0.0506 ns |         - |
| Damm              | 140662                |  5.477 ns | 0.0170 ns | 0.0142 ns |         - |
| Damm              | 140662538             |  8.315 ns | 0.0803 ns | 0.0712 ns |         - |
| Damm              | 140662538042          | 11.993 ns | 0.0355 ns | 0.0332 ns |         - |
| Damm              | 140662538042551       | 16.354 ns | 0.3389 ns | 0.3170 ns |         - |
| Damm              | 140662538042551028    | 19.487 ns | 0.0783 ns | 0.0654 ns |         - |
| Damm              | 140662538042551028265 | 23.192 ns | 0.0936 ns | 0.0781 ns |         - |
|                   |                       |           |           |           |           |                                           
| ISO/IEC 706 11,10 | 140                   |  6.355 ns | 0.0509 ns | 0.0476 ns |         - |
| ISO/IEC 706 11,10 | 140662                | 10.266 ns | 0.0631 ns | 0.0590 ns |         - |
| ISO/IEC 706 11,10 | 140662538             | 12.386 ns | 0.0982 ns | 0.0919 ns |         - |
| ISO/IEC 706 11,10 | 140662538042          | 15.053 ns | 0.1208 ns | 0.1130 ns |         - |
| ISO/IEC 706 11,10 | 140662538042551       | 18.437 ns | 0.1473 ns | 0.1378 ns |         - |
| ISO/IEC 706 11,10 | 140662538042551028    | 22.820 ns | 0.1971 ns | 0.1843 ns |         - |
| ISO/IEC 706 11,10 | 140662538042551028265 | 26.027 ns | 0.1479 ns | 0.1384 ns |         - |
|                   |                       |           |           |           |           |                                           
| ISO/IEC 706 11-2  | 140                   |  4.241 ns | 0.0141 ns | 0.0132 ns |         - |
| ISO/IEC 706 11-2  | 140662                |  8.603 ns | 0.0292 ns | 0.0259 ns |         - |
| ISO/IEC 706 11-2  | 140662538             | 11.325 ns | 0.0451 ns | 0.0400 ns |         - |
| ISO/IEC 706 11-2  | 140662538042          | 14.259 ns | 0.0477 ns | 0.0423 ns |         - |
| ISO/IEC 706 11-2  | 140662538042551       | 16.991 ns | 0.1129 ns | 0.1000 ns |         - |
| ISO/IEC 706 11-2  | 140662538042551028    | 14.592 ns | 0.0717 ns | 0.0636 ns |         - |
| ISO/IEC 706 11-2  | 140662538042551028265 | 22.463 ns | 0.1754 ns | 0.1555 ns |         - |
|                   |                       |           |           |           |           |                                           
| ISO/IEC 706 97-10 | 140                   |  6.887 ns | 0.0739 ns | 0.0692 ns |         - |
| ISO/IEC 706 97-10 | 140662                | 10.281 ns | 0.1422 ns | 0.1330 ns |         - |
| ISO/IEC 706 97-10 | 140662538             | 13.230 ns | 0.1022 ns | 0.0956 ns |         - |
| ISO/IEC 706 97-10 | 140662538042          | 16.044 ns | 0.1452 ns | 0.1358 ns |         - |
| ISO/IEC 706 97-10 | 140662538042551       | 18.855 ns | 0.1708 ns | 0.1426 ns |         - |
| ISO/IEC 706 97-10 | 140662538042551028    | 22.542 ns | 0.2155 ns | 0.2016 ns |         - |
| ISO/IEC 706 97-10 | 140662538042551028265 | 25.380 ns | 0.2038 ns | 0.1906 ns |         - |
|                   |                       |           |           |           |           |                                           
| Luhn              | 140                   |  6.674 ns | 0.1106 ns | 0.1035 ns |         - |
| Luhn              | 140662                |  9.396 ns | 0.0575 ns | 0.0538 ns |         - |
| Luhn              | 140662538             | 14.913 ns | 0.0464 ns | 0.0434 ns |         - |
| Luhn              | 140662538042          | 14.981 ns | 0.0720 ns | 0.0638 ns |         - |
| Luhn              | 140662538042551       | 20.813 ns | 0.1534 ns | 0.1435 ns |         - |
| Luhn              | 140662538042551028    | 22.434 ns | 0.1459 ns | 0.1365 ns |         - |
| Luhn              | 140662538042551028265 | 27.432 ns | 0.1217 ns | 0.1138 ns |         - |
|                   |                       |           |           |           |           |                                           
| Modulus10_13      | 140                   |  4.845 ns | 0.0532 ns | 0.0498 ns |         - |
| Modulus10_13      | 140662                |  8.806 ns | 0.1316 ns | 0.1167 ns |         - |
| Modulus10_13      | 140662538             | 11.743 ns | 0.1881 ns | 0.1760 ns |         - |
| Modulus10_13      | 140662538042          | 12.224 ns | 0.0869 ns | 0.0813 ns |         - |
| Modulus10_13      | 140662538042551       | 17.971 ns | 0.1486 ns | 0.1317 ns |         - |
| Modulus10_13      | 140662538042551028    | 21.347 ns | 0.1666 ns | 0.1558 ns |         - |
| Modulus10_13      | 140662538042551028265 | 24.085 ns | 0.1882 ns | 0.1761 ns |         - |
|                   |                       |           |           |           |           |                                           
| Modulus10_1       | 140                   |  3.865 ns | 0.0509 ns | 0.0476 ns |         - |
| Modulus10_1       | 140662                |  5.566 ns | 0.0775 ns | 0.0725 ns |         - |
| Modulus10_1       | 140662538             |  7.337 ns | 0.0871 ns | 0.0815 ns |         - |
|                   |                       |           |           |           |           |                                           
| Modulus10_2       | 140                   |  4.541 ns | 0.0420 ns | 0.0372 ns |         - |
| Modulus10_2       | 140662                |  6.142 ns | 0.0614 ns | 0.0513 ns |         - |
| Modulus10_2       | 140662538             |  7.874 ns | 0.0784 ns | 0.0733 ns |         - |
|                   |                       |           |           |           |           |                                           
| Modulus11         | 140                   |  6.740 ns | 0.0600 ns | 0.0562 ns |         - |
| Modulus11         | 140662                | 10.089 ns | 0.0851 ns | 0.0796 ns |         - |
| Modulus11         | 140662538             | 13.288 ns | 0.0696 ns | 0.0651 ns |         - |
|                   |                       |           |           |           |           |                                           
| Verhoeff          | 140                   |  8.358 ns | 0.1062 ns | 0.0941 ns |         - |
| Verhoeff          | 140662                | 12.916 ns | 0.0614 ns | 0.0544 ns |         - |
| Verhoeff          | 140662538             | 17.835 ns | 0.1126 ns | 0.0998 ns |         - |
| Verhoeff          | 140662538042          | 22.727 ns | 0.1362 ns | 0.1274 ns |         - |
| Verhoeff          | 140662538042551       | 27.473 ns | 0.1085 ns | 0.0961 ns |         - |
| Verhoeff          | 140662538042551028    | 32.246 ns | 0.1009 ns | 0.0842 ns |         - |
| Verhoeff          | 140662538042551028265 | 37.262 ns | 0.1306 ns | 0.1090 ns |         - |

#### General Alphabetic Algorithms

| Algorithm Name          | Value                 | Mean      | Error     | StdDev    | Allocated |
|------------------------ |---------------------- |----------:|----------:|----------:|----------:|
| ISO/IEC 7064 MOD 27,26  | EGR                   |  6.915 ns | 0.0760 ns | 0.0711 ns |         - |
| ISO/IEC 7064 MOD 27,26  | EGRNML                |  9.448 ns | 0.0751 ns | 0.0627 ns |         - |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOC             | 14.985 ns | 0.0798 ns | 0.0707 ns |         - |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOCECU          | 14.329 ns | 0.0699 ns | 0.0619 ns |         - |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOCECUJIK       | 17.069 ns | 0.0676 ns | 0.0599 ns |         - |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOCECUJIKNWW    | 19.291 ns | 0.0719 ns | 0.0600 ns |         - |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOCECUJIKNWWVVO | 26.171 ns | 0.0983 ns | 0.0871 ns |         - |
|                         |                       |           |           |           |           |                                           
| ISO/IEC 7064 MOD 661-26 | EGR                   |  6.994 ns | 0.0361 ns | 0.0282 ns |         - |
| ISO/IEC 7064 MOD 661-26 | EGRNML                | 10.143 ns | 0.0711 ns | 0.0594 ns |         - |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOC             | 13.182 ns | 0.0760 ns | 0.0674 ns |         - |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOCECU          | 16.399 ns | 0.0647 ns | 0.0605 ns |         - |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOCECUJIK       | 20.300 ns | 0.0949 ns | 0.0887 ns |         - |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOCECUJIKNWW    | 22.779 ns | 0.0896 ns | 0.0838 ns |         - |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOCECUJIKNWWVVO | 27.412 ns | 0.1551 ns | 0.1450 ns |         - |

#### General Alphanumeric Algorithms

Note that the values used for the NOID Check Digit algorithm do not include lengths
3 or 6 so that benchmarks are not run on purely numeric strings.

| Algorithm Name           | Value                 | Mean      | Error     | StdDev    | Allocated |
|------------------------- |---------------------- |----------:|----------:|----------:|----------:|
| AlphanumericMod97_10     | U7y                   |  8.867 ns | 0.1108 ns | 0.0982 ns |         - |
| AlphanumericMod97_10     | U7y8SX                | 15.700 ns | 0.2916 ns | 0.2727 ns |         - |
| AlphanumericMod97_10     | U7y8SXrC0             | 25.414 ns | 0.1013 ns | 0.0791 ns |         - |
| AlphanumericMod97_10     | U7y8SXrC0O3S          | 30.480 ns | 0.2305 ns | 0.2043 ns |         - |
| AlphanumericMod97_10     | U7y8SXrC0O3Sc4I       | 38.288 ns | 0.2435 ns | 0.2278 ns |         - |
| AlphanumericMod97_10     | U7y8SXrC0O3Sc4IHYQ    | 45.774 ns | 0.2720 ns | 0.2411 ns |         - |
| AlphanumericMod97_10     | U7y8SXrC0O3Sc4IHYQF4M | 54.707 ns | 0.5074 ns | 0.4746 ns |         - |
|                          |                       |           |           |           |           |                                           
| ISO/IEC 7064 MOD 1271-36 | U7Y                   |  7.648 ns | 0.0591 ns | 0.0553 ns |         - |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SX                | 12.700 ns | 0.0698 ns | 0.0653 ns |         - |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SXRC0             | 17.990 ns | 0.3459 ns | 0.3236 ns |         - |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SXRC0O3S          | 21.011 ns | 0.1960 ns | 0.1738 ns |         - |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SXRC0O3SC4I       | 25.927 ns | 0.3262 ns | 0.2547 ns |         - |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SXRC0O3SC4IHYQ    | 29.930 ns | 0.3608 ns | 0.3013 ns |         - |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SXRC0O3SC4IHYQF4M | 34.753 ns | 0.3039 ns | 0.2537 ns |         - |
|                          |                       |           |           |           |           |                                           
| ISO/IEC 7064 MOD 37-2    | U7Y                   |  7.391 ns | 0.0347 ns | 0.0324 ns |         - |
| ISO/IEC 7064 MOD 37-2    | U7Y8SX                | 11.461 ns | 0.0774 ns | 0.0724 ns |         - |
| ISO/IEC 7064 MOD 37-2    | U7Y8SXRC0             | 15.736 ns | 0.0734 ns | 0.0686 ns |         - |
| ISO/IEC 7064 MOD 37-2    | U7Y8SXRC0O3S          | 19.804 ns | 0.0717 ns | 0.0671 ns |         - |
| ISO/IEC 7064 MOD 37-2    | U7Y8SXRC0O3SC4I       | 23.759 ns | 0.0894 ns | 0.0836 ns |         - |
| ISO/IEC 7064 MOD 37-2    | U7Y8SXRC0O3SC4IHYQ    | 33.503 ns | 0.1415 ns | 0.1324 ns |         - |
| ISO/IEC 7064 MOD 37-2    | U7Y8SXRC0O3SC4IHYQF4M | 38.716 ns | 0.1444 ns | 0.1280 ns |         - |
|                          |                       |           |           |           |           |                                           
| ISO/IEC 7064 MOD 37,36   | U7Y                   |  7.937 ns | 0.0461 ns | 0.0385 ns |         - |
| ISO/IEC 7064 MOD 37,36   | U7Y8SX                | 12.423 ns | 0.0516 ns | 0.0482 ns |         - |
| ISO/IEC 7064 MOD 37,36   | U7Y8SXRC0             | 16.474 ns | 0.0991 ns | 0.0927 ns |         - |
| ISO/IEC 7064 MOD 37,36   | U7Y8SXRC0O3S          | 20.351 ns | 0.3404 ns | 0.2843 ns |         - |
| ISO/IEC 7064 MOD 37,36   | U7Y8SXRC0O3SC4I       | 24.541 ns | 0.1335 ns | 0.1183 ns |         - |
| ISO/IEC 7064 MOD 37,36   | U7Y8SXRC0O3SC4IHYQ    | 30.263 ns | 0.1079 ns | 0.0956 ns |         - |
| ISO/IEC 7064 MOD 37,36   | U7Y8SXRC0O3SC4IHYQF4M | 35.554 ns | 0.1783 ns | 0.1668 ns |         - |
|                          |                       |           |           |           |           |                                           
| NOID Check Digit         | 11404/2h9             | 10.900 ns | 0.1480 ns | 0.1390 ns |         - |
| NOID Check Digit         | 11404/2h9tqb          | 13.440 ns | 0.2840 ns | 0.2790 ns |         - |
| NOID Check Digit         | 11404/2h9tqbxk6       | 16.450 ns | 0.1590 ns | 0.1410 ns |         - |
| NOID Check Digit         | 11404/2h9tqbxk6rw7    | 20.090 ns | 0.2470 ns | 0.2190 ns |         - |
| NOID Check Digit         | 11404/2h9tqbxk6rw7dwm | 20.750 ns | 0.1330 ns | 0.1240 ns |         - |


#### Value Specific Algorithms

Note: ABA RTN, CUSIP, ICAO 9303 multi-field algorithms (Machine Readable Visa, Size TD1, 
TD2 and TD3), ISAN, NHS, NPI and SEDOL algorithms do not support calculation of check digits, 
only validation of values containing check digits.

| Algorithm Name | Value                           | Mean      | Error     | StdDev    | Allocated |
|--------------- |-------------------------------- |----------:|----------:|----------:|----------:|
| ICAO 9303      | U7Y                             |  7.615 ns | 0.0376 ns | 0.0333 ns |         - |
| ICAO 9303      | U7Y8SX                          | 13.264 ns | 0.0796 ns | 0.0745 ns |         - |
| ICAO 9303      | U7Y8SXRC0                       | 18.372 ns | 0.0931 ns | 0.0777 ns |         - |
| ICAO 9303      | U7Y8SXRC0O3S                    | 22.176 ns | 0.1815 ns | 0.1698 ns |         - |
| ICAO 9303      | U7Y8SXRC0O3SC4I                 | 27.067 ns | 0.1385 ns | 0.1228 ns |         - |
| ICAO 9303      | U7Y8SXRC0O3SC4IHYQ              | 32.181 ns | 0.2162 ns | 0.2022 ns |         - |
| ICAO 9303      | U7Y8SXRC0O3SC4IHYQF4M           | 36.435 ns | 0.2524 ns | 0.2237 ns |         - |
|                |                                 |           |           |           |           |                                           
| IBAN           | BE00096123456769                | 25.840 ns | 0.3480 ns | 0.3250 ns |         - |
| IBAN           | GB00WEST12345698765432          | 38.170 ns | 0.2620 ns | 0.2320 ns |         - |
| IBAN           | SC00MCBL01031234567890123456USD | 49.910 ns | 0.2270 ns | 0.2010 ns |         - |
|                |                                 |           |           |           |           |                                           
| ISIN           | AU0000XVGZA                     | 19.700 ns | 0.1580 ns | 0.1400 ns |         - |
| ISIN           | GB000263494                     | 18.310 ns | 0.2850 ns | 0.2660 ns |         - |
| ISIN           | US037833100                     | 18.060 ns | 0.1140 ns | 0.0960 ns |         - |
|                |                                 |           |           |           |           |                                           
| ISO 6346       | CSQU305438                      | 16.740 ns | 0.2160 ns | 0.2020 ns |         - |
| ISO 6346       | MSKU907032                      | 16.220 ns | 0.0780 ns | 0.0690 ns |         - |
| ISO 6346       | TOLU473478                      | 16.220 ns | 0.1350 ns | 0.1130 ns |         - |
|                |                                 |           |           |           |           |                                           
| VIN            | 1G8ZG127_WZ157259               | 21.460 ns | 0.0780 ns | 0.0730 ns |         - |
| VIN            | 1HGEM212_2L047875               | 20.740 ns | 0.1310 ns | 0.1230 ns |         - |
| VIN            | 1M8GDM9A_KP042788               | 20.890 ns | 0.0760 ns | 0.0710 ns |         - |

### Validate Method

#### General Numeric Algorithms

All algorithms use a single check digit except ISO/IEC 7064 MOD 97-10 which uses
two check digits.

Note that the Modulus10_1, Modulus10_2 and Modulus11 algorithms have a maximum 
length of 10 (including the check digit) for values being validated so their
benchmarks do not cover lengths greater than 10.

| Algorithm Name         | Value                   | Mean      | Error     | StdDev    | Allocated |
|----------------------- | ----------------------- |----------:|----------:|----------:|----------:|
| Damm                   | 1402                    |  3.825 ns | 0.0240 ns | 0.0213 ns |         - |
| Damm                   | 1406622                 |  6.032 ns | 0.0244 ns | 0.0216 ns |         - |
| Damm                   | 1406625388              |  9.435 ns | 0.0801 ns | 0.0750 ns |         - |
| Damm                   | 1406625380422           | 13.104 ns | 0.0813 ns | 0.0760 ns |         - |
| Damm                   | 1406625380425518        | 16.887 ns | 0.1718 ns | 0.1523 ns |         - |
| Damm                   | 1406625380425510280     | 20.558 ns | 0.1472 ns | 0.1377 ns |         - |
| Damm                   | 1406625380425510282654  | 24.074 ns | 0.1599 ns | 0.1495 ns |         - |
|                        |                         |           |           |           |           |                                           
| ISO/IEC 7064 MOD 11,10 | 1409                    |  6.567 ns | 0.0645 ns | 0.0572 ns |         - |
| ISO/IEC 7064 MOD 11,10 | 1406623                 | 11.212 ns | 0.0784 ns | 0.0695 ns |         - |
| ISO/IEC 7064 MOD 11,10 | 1406625381              | 14.277 ns | 0.0848 ns | 0.0708 ns |         - |
| ISO/IEC 7064 MOD 11,10 | 1406625380426           | 18.457 ns | 0.0729 ns | 0.0682 ns |         - |
| ISO/IEC 7064 MOD 11,10 | 1406625380425514        | 20.652 ns | 0.0946 ns | 0.0884 ns |         - |
| ISO/IEC 7064 MOD 11,10 | 1406625380425510286     | 25.927 ns | 0.0780 ns | 0.0730 ns |         - |
| ISO/IEC 7064 MOD 11,10 | 1406625380425510282657  | 29.294 ns | 0.1947 ns | 0.1822 ns |         - |
|                        |                         |           |           |           |           |                                           
| ISO/IEC 7064 MOD 11-2  | 140X                    |  5.319 ns | 0.0585 ns | 0.0488 ns |         - |
| ISO/IEC 7064 MOD 11-2  | 1406628                 |  9.415 ns | 0.1067 ns | 0.0998 ns |         - |
| ISO/IEC 7064 MOD 11-2  | 1406625380              | 11.886 ns | 0.0424 ns | 0.0397 ns |         - |
| ISO/IEC 7064 MOD 11-2  | 1406625380426           | 15.054 ns | 0.1373 ns | 0.1284 ns |         - |
| ISO/IEC 7064 MOD 11-2  | 1406625380425511        | 18.343 ns | 0.1774 ns | 0.1482 ns |         - |
| ISO/IEC 7064 MOD 11-2  | 140662538042551028X     | 15.708 ns | 0.1594 ns | 0.1491 ns |         - |
| ISO/IEC 7064 MOD 11-2  | 1406625380425510282651  | 22.860 ns | 0.0759 ns | 0.0634 ns |         - |
|                        |                         |           |           |           |           |                                           
| ISO/IEC 7064 MOD 97-10 | 14066                   |  6.683 ns | 0.0606 ns | 0.0537 ns |         - |
| ISO/IEC 7064 MOD 97-10 | 14066262                |  9.404 ns | 0.0898 ns | 0.0840 ns |         - |
| ISO/IEC 7064 MOD 97-10 | 14066253823             | 12.522 ns | 0.1327 ns | 0.1241 ns |         - |
| ISO/IEC 7064 MOD 97-10 | 14066253804250          | 15.676 ns | 0.1332 ns | 0.1246 ns |         - |
| ISO/IEC 7064 MOD 97-10 | 14066253804255112       | 18.960 ns | 0.2592 ns | 0.2298 ns |         - |
| ISO/IEC 7064 MOD 97-10 | 14066253804255102853    | 22.186 ns | 0.2068 ns | 0.1935 ns |         - |
| ISO/IEC 7064 MOD 97-10 | 14066253804255102826587 | 24.965 ns | 0.5259 ns | 0.4919 ns |         - |
|                        |                         |           |           |           |           |                                           
| Luhn                   | 1404                    |  6.671 ns | 0.0366 ns | 0.0305 ns |         - |
| Luhn                   | 1406628                 |  8.910 ns | 0.0483 ns | 0.0377 ns |         - |
| Luhn                   | 1406625382              | 12.273 ns | 0.0815 ns | 0.0763 ns |         - |
| Luhn                   | 1406625380421           | 14.002 ns | 0.0486 ns | 0.0431 ns |         - |
| Luhn                   | 1406625380425514        | 18.127 ns | 0.2873 ns | 0.2687 ns |         - |
| Luhn                   | 1406625380425510285     | 19.739 ns | 0.1725 ns | 0.1613 ns |         - |
| Luhn                   | 1406625380425510282651  | 23.388 ns | 0.1535 ns | 0.1435 ns |         - |
|                        |                         |           |           |           |           |                                           
| Modulus10_13           | 1403                    |  5.844 ns | 0.0538 ns | 0.0503 ns |         - |
| Modulus10_13           | 1406627                 |  9.622 ns | 0.1128 ns | 0.1055 ns |         - |
| Modulus10_13           | 1406625385              | 12.078 ns | 0.1033 ns | 0.0966 ns |         - |
| Modulus10_13           | 1406625380425           | 16.629 ns | 0.3109 ns | 0.2908 ns |         - |
| Modulus10_13           | 1406625380425518        | 19.143 ns | 0.1654 ns | 0.1547 ns |         - |
| Modulus10_13           | 1406625380425510288     | 18.478 ns | 0.1429 ns | 0.1336 ns |         - |
| Modulus10_13           | 1406625380425510282657  | 26.205 ns | 0.1747 ns | 0.1634 ns |         - |
|                        |                         |           |           |           |           |                                           
| Modulus10_1            | 1401                    |  3.168 ns | 0.0427 ns | 0.0399 ns |         - |
| Modulus10_1            | 1406628                 |  4.805 ns | 0.0789 ns | 0.0738 ns |         - |
| Modulus10_1            | 1406625384              |  7.102 ns | 0.1677 ns | 0.1647 ns |         - |
|                        |                         |           |           |           |           |                                           
| Modulus10_2            | 1406                    |  3.313 ns | 0.0394 ns | 0.0368 ns |         - |
| Modulus10_2            | 1406627                 |  4.892 ns | 0.0527 ns | 0.0493 ns |         - |
| Modulus10_2            | 1406625389              |  6.557 ns | 0.0890 ns | 0.0833 ns |         - |
|                        |                         |           |           |           |           |                                           
| Modulus11              | 1406                    |  5.127 ns | 0.0476 ns | 0.0422 ns |         - |
| Modulus11              | 1406625                 |  6.844 ns | 0.1128 ns | 0.1000 ns |         - |
| Modulus11              | 1406625388              |  8.112 ns | 0.0454 ns | 0.0425 ns |         - |
|                        |                         |           |           |           |           |                                           
| Verhoeff               | 1401                    |  9.365 ns | 0.0523 ns | 0.0489 ns |         - |
| Verhoeff               | 1406625                 | 14.769 ns | 0.0841 ns | 0.0656 ns |         - |
| Verhoeff               | 1406625388              | 20.334 ns | 0.1164 ns | 0.1089 ns |         - |
| Verhoeff               | 1406625380426           | 25.942 ns | 0.1319 ns | 0.1234 ns |         - |
| Verhoeff               | 1406625380425512        | 31.425 ns | 0.1170 ns | 0.0977 ns |         - |
| Verhoeff               | 1406625380425510285     | 36.982 ns | 0.1119 ns | 0.0935 ns |         - |
| Verhoeff               | 1406625380425510282655  | 42.288 ns | 0.1756 ns | 0.1642 ns |         - |


#### General Alphabetic Algorithms

ISO/IEC 7064 MOD 27,26 uses a single check character. ISO/IEC 7064 MOD 661-26
uses two check characters.

| Algorithm Name          | Value                   | Mean      | Error     | StdDev    | Allocated |
|------------------------ |------------------------ |----------:|----------:|----------:|----------:|
| ISO/IEC 7064 MOD 27,26  | EGRS                    |  7.274 ns | 0.0623 ns | 0.0552 ns |         - |
| ISO/IEC 7064 MOD 27,26  | EGRNMLU                 | 10.292 ns | 0.0467 ns | 0.0436 ns |         - |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOCB              | 14.444 ns | 0.0622 ns | 0.0582 ns |         - |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOCECUA           | 18.226 ns | 0.1115 ns | 0.0988 ns |         - |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOCECUJIKA        | 21.802 ns | 0.1181 ns | 0.1047 ns |         - |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOCECUJIKNWWY     | 25.724 ns | 0.0692 ns | 0.0647 ns |         - |
| ISO/IEC 7064 MOD 27,26  | EGRNMLJOCECUJIKNWWVVOQ  | 29.716 ns | 0.1309 ns | 0.1224 ns |         - |
|                         |                         |           |           |           |           |                                           
| ISO/IEC 7064 MOD 661-26 | EGRSE                   |  6.263 ns | 0.0179 ns | 0.0167 ns |         - |
| ISO/IEC 7064 MOD 661-26 | EGRNMLDR                | 10.339 ns | 0.0777 ns | 0.0726 ns |         - |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOCCK             | 13.633 ns | 0.0456 ns | 0.0427 ns |         - |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOCECUZJ          | 16.896 ns | 0.0641 ns | 0.0568 ns |         - |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOCECUJIKFQ       | 20.183 ns | 0.0823 ns | 0.0729 ns |         - |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOCECUJIKNWWQN    | 23.412 ns | 0.0979 ns | 0.0915 ns |         - |
| ISO/IEC 7064 MOD 661-26 | EGRNMLJOCECUJIKNWWVVORC | 26.679 ns | 0.1163 ns | 0.0971 ns |         - |

#### General Alphanumeric Algorithms

AlphanumericMod97_10 algorithm and ISO/IEC 7064 MOD 1271-36 uses two check characters. 
ISO/IEC 7064 MOD 37-2, ISO/IEC 7064 MOD 37,36 and NOID Check Digit algorithms use a 
single check character.

Note also that the values used for the NOID Check Digit algorithm do not include lengths
3 or 6 so that benchmarks are not run on purely numeric strings.

| Algorithm Name           | Value                   | Mean      | Error     | StdDev    | Allocated |
|------------------------- |-------------------------|----------:|----------:|----------:|----------:|
| AlphanumericMod97_10     | U7y46                   | 10.601 ns | 0.0477 ns | 0.0423 ns |         - |
| AlphanumericMod97_10     | U7y8SX89                | 16.772 ns | 0.1559 ns | 0.1458 ns |         - |
| AlphanumericMod97_10     | U7y8SXrC087             | 23.859 ns | 0.2663 ns | 0.2491 ns |         - |
| AlphanumericMod97_10     | U7y8SXrC0O3S38          | 30.912 ns | 0.2579 ns | 0.2412 ns |         - |
| AlphanumericMod97_10     | U7y8SXrC0O3Sc4I27       | 37.383 ns | 0.5385 ns | 0.4774 ns |         - |
| AlphanumericMod97_10     | U7y8SXrC0O3Sc4IHYQ54    | 45.008 ns | 0.2819 ns | 0.2354 ns |         - |
| AlphanumericMod97_10     | U7y8SXrC0O3Sc4IHYQF4M21 | 49.544 ns | 0.2715 ns | 0.2539 ns |         - |
|                          |                         |           |           |           |           |                                           
| ISO/IEC 7064 MOD 1271-36 | U7YM0                   |  8.068 ns | 0.0306 ns | 0.0271 ns |         - |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SXOR                | 13.625 ns | 0.0857 ns | 0.0760 ns |         - |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SXRC0FI             | 17.588 ns | 0.0977 ns | 0.0914 ns |         - |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SXRC0O3SX4          | 20.950 ns | 0.0567 ns | 0.0503 ns |         - |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SXRC0O3SC4I9D       | 24.208 ns | 0.0451 ns | 0.0400 ns |         - |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SXRC0O3SC4IHYQYI    | 31.207 ns | 0.1850 ns | 0.1731 ns |         - |
| ISO/IEC 7064 MOD 1271-36 | U7Y8SXRC0O3SC4IHYQF4M44 | 33.292 ns | 0.0991 ns | 0.0828 ns |         - |
|                          |                         |           |           |           |           |                                           
| ISO/IEC 7064 MOD 37-2    | U7YZ                    |  6.713 ns | 0.0368 ns | 0.0326 ns |         - |
| ISO/IEC 7064 MOD 37-2    | U7Y8SXV                 | 10.742 ns | 0.0440 ns | 0.0412 ns |         - |
| ISO/IEC 7064 MOD 37-2    | U7Y8SXRC0E              | 14.103 ns | 0.0575 ns | 0.0538 ns |         - |
| ISO/IEC 7064 MOD 37-2    | U7Y8SXRC0O3SU           | 18.156 ns | 0.0843 ns | 0.0747 ns |         - |
| ISO/IEC 7064 MOD 37-2    | U7Y8SXRC0O3SC4IB        | 21.720 ns | 0.0729 ns | 0.0682 ns |         - |
| ISO/IEC 7064 MOD 37-2    | U7Y8SXRC0O3SC4IHYQG     | 26.040 ns | 0.1465 ns | 0.1370 ns |         - |
| ISO/IEC 7064 MOD 37-2    | U7Y8SXRC0O3SC4IHYQF4MF  | 29.839 ns | 0.0916 ns | 0.0812 ns |         - |
|                          |                         |           |           |           |           |                                           
| ISO/IEC 7064 MOD 37,36   | U7YW                    |  8.697 ns | 0.0715 ns | 0.0669 ns |         - |
| ISO/IEC 7064 MOD 37,36   | U7Y8SX8                 | 13.124 ns | 0.0772 ns | 0.0722 ns |         - |
| ISO/IEC 7064 MOD 37,36   | U7Y8SXRC0E              | 17.682 ns | 0.0633 ns | 0.0529 ns |         - |
| ISO/IEC 7064 MOD 37,36   | U7Y8SXRC0O3SR           | 23.219 ns | 0.2057 ns | 0.1924 ns |         - |
| ISO/IEC 7064 MOD 37,36   | U7Y8SXRC0O3SC4IT        | 24.299 ns | 0.1017 ns | 0.0902 ns |         - |
| ISO/IEC 7064 MOD 37,36   | U7Y8SXRC0O3SC4IHYQD     | 27.938 ns | 0.1244 ns | 0.0971 ns |         - |
| ISO/IEC 7064 MOD 37,36   | U7Y8SXRC0O3SC4IHYQF4MP  | 32.631 ns | 0.1183 ns | 0.1049 ns |         - |
|                          |                         |           |           |           |           |                                           
| NOID Check Digit         | 11404/2h9m              | 11.450 ns | 0.0710 ns | 0.0660 ns |         - |
| NOID Check Digit         | 11404/2h9tqb0           | 14.290 ns | 0.0960 ns | 0.0850 ns |         - |
| NOID Check Digit         | 11404/2h9tqbxk6d        | 17.010 ns | 0.0900 ns | 0.0790 ns |         - |
| NOID Check Digit         | 11404/2h9tqbxk6rw74     | 19.710 ns | 0.2040 ns | 0.1910 ns |         - |
| NOID Check Digit         | 11404/2h9tqbxk6rw7dwmz  | 22.590 ns | 0.1490 ns | 0.1250 ns |         - |


#### Value Specific Algorithms

| Algorithm Name                  | Value                                  | Mean      | Error     | StdDev    | Allocated |
|-------------------------------- |--------------------------------------- |----------:|----------:|----------:|----------:|
| ABA RTN                         | 111000025                              | 10.830 ns | 0.0650 ns | 0.0580 ns |         - |
| ABA RTN                         | 122235821                              | 10.400 ns | 0.1880 ns | 0.1570 ns |         - |
| ABA RTN                         | 325081403                              | 10.310 ns | 0.0610 ns | 0.0570 ns |         - |
|                                 |                                        |           |           |           |           |                                           
| CUSIP                           | 037833100                              | 16.500 ns | 0.1990 ns | 0.1760 ns |         - |
| CUSIP                           | 38143VAA7                              | 13.020 ns | 0.0830 ns | 0.0770 ns |         - |
| CUSIP                           | 91282CJL6                              | 12.850 ns | 0.0630 ns | 0.0530 ns |         - |
|                                 |                                        |           |           |           |           |                                           
| FIGI                            | BBG000B9Y5X2                           | 19.980 ns | 0.3870 ns | 0.3430 ns |         - |
| FIGI                            | BBG111111160                           | 19.540 ns | 0.1640 ns | 0.1540 ns |         - |
| FIGI                            | BBGZYXWVTSR7                           | 19.650 ns | 0.2670 ns | 0.2230 ns |         - |
|                                 |                                        |           |           |           |           |                                           
| IBAN                            | BE71096123456769                       | 20.090 ns | 0.1330 ns | 0.1180 ns |         - |
| IBAN                            | GB82WEST12345698765432                 | 33.800 ns | 0.2340 ns | 0.2080 ns |         - |
| IBAN                            | SC74MCBL01031234567890123456USD        | 48.680 ns | 0.1980 ns | 0.1850 ns |         - |
|                                 |                                        |           |           |           |           |
| ICAO 9303                       | U7Y5                                   |  7.529 ns | 0.0629 ns | 0.0589 ns |         - |
| ICAO 9303                       | U7Y8SX8                                | 13.597 ns | 0.0780 ns | 0.0730 ns |         - |
| ICAO 9303                       | U7Y8SXRC03                             | 19.148 ns | 0.1083 ns | 0.1013 ns |         - |
| ICAO 9303                       | U7Y8SXRC0O3S8                          | 24.398 ns | 0.1694 ns | 0.1502 ns |         - |
| ICAO 9303                       | U7Y8SXRC0O3SC4I2                       | 25.424 ns | 0.1976 ns | 0.1751 ns |         - |
| ICAO 9303                       | U7Y8SXRC0O3SC4IHYQ9                    | 29.443 ns | 0.1393 ns | 0.1235 ns |         - |
| ICAO 9303                       | U7Y8SXRC0O3SC4IHYQF4M8                 | 33.964 ns | 0.1729 ns | 0.1444 ns |         - |
|                                 |                                        |           |           |           |           |
| ICAO 9303 Machine Readable Visa | I<UTOERIKSSON<<ANNA<MARIA<<<<<<<<<<<<br>D231458907UTO7408122F1204159<<<<<<<< | 59.49 ns | 1.208 ns | 1.770 ns |         - |
| ICAO 9303 Machine Readable Visa | I<UTOSKYWALKER<<LUKE<<<<<<<<<<<<<<<<<br>STARWARS45UTO7705256M2405252<<<<<<<< | 53.47 ns | 0.739 ns | 0.655 ns |         - |
| ICAO 9303 Machine Readable Visa | V<UTOERIKSSON<<ANNA<MARIA<<<<<<<<<<<<<<<<<<<<br>L898902C<3UTO6908061F9406236ZE184226B<<<<<<< | 52.94 ns | 0.527 ns | 0.493 ns |         - |
|                                 |                                        |           |           |           |           |
| ICAO 9303 Size TD1              | I<UTOD231458907<<<<<<<<<<<<<<<<br>7408122F1204159UTO<<<<<<<<<<<6<br>ERIKSSON<<ANNA<MARIA<<<<<<<<<< | 84.945 ns | 1.6663 ns | 1.5586 ns |         - |
| ICAO 9303 Size TD1              | I<UTOSTARWARS45<<<<<<<<<<<<<<<<br>7705256M2405252UTO<<<<<<<<<<<4<br>SKYWALKER<<LUKE<<<<<<<<<<<<<<< | 97.953 ns | 1.0370 ns | 0.9700 ns |         - |
| ICAO 9303 Size TD1              | I<UTOD23145890<AB112234566<<<<<br>7408122F1204159UTO<<<<<<<<<<<4<br>ERIKSSON<<ANNA<MARIA<<<<<<<<<< | 97.953 ns | 1.0370 ns | 0.9700 ns |         - |
|                                 |                                        |           |           |           |           |
| ICAO 9303 Size TD2              | I<UTOERIKSSON<<ANNA<MARIA<<<<<<<<<<<<br>D231458907UTO7408122F1204159<<<<<<<6 | 86.78 ns | 0.816 ns | 0.763 ns |         - |
| ICAO 9303 Size TD2              | I<UTOQWERTY<<ASDF<<<<<<<<<<<<<<<<<<<<br>D23145890<UTO7408122F1204159AB1124<4 | 95.22 ns | 0.852 ns | 0.797 ns |         - |
| ICAO 9303 Size TD2              | I<UTOSKYWALKER<<LUKE<<<<<<<<<<<<<<<<<br>STARWARS45UTO7705256M2405252<<<<<<<8 | 87.34 ns | 0.704 ns | 0.658 ns |         - |
|                                 |                                        |           |           |           |           |
| ICAO 9303 Size TD3              | P<UTOERIKSSON<<ANNA<MARIA<<<<<<<<<<<<<<<<<<<<br>L898902C36UTO7408122F1204159ZE184226B<<<<<10 | 85.675 ns | 0.5136 ns | 0.4804 ns |         - |
| ICAO 9303 Size TD3              | P<UTOQWERTY<<ASDF<<<<<<<<<<<<<<<<<<<<<<<<<<<<br>Q123987655UTO3311226F2010201<<<<<<<<<<<<<<06 | 85.188 ns | 0.2958 ns | 0.2310 ns |         - |
| ICAO 9303 Size TD3              | P<UTOSKYWALKER<<LUKE<<<<<<<<<<<<<<<<<<<<<<<<<br>STARWARS45UTO7705256M2405252HAN<SHOT<FIRST78 | 85.401 ns | 0.5888 ns | 0.5507 ns |         - |
|                                 |                                        |           |           |           |           |                                           
| ISAN                            | C594660A8B2E5D22X6DDA3272E             | 54.400 ns | 0.1940 ns | 0.1810 ns |         - |
| ISAN                            | D02C42E954183EE2Q1291C8AEO             | 51.210 ns | 0.2820 ns | 0.2640 ns |         - |
| ISAN                            | E9530C32BC0EE83B269867B20F             | 46.700 ns | 0.1390 ns | 0.1300 ns |         - |
|                                 |                                        |           |           |           |           |                                           
| ISAN (Formatted)                | ISAN C594-660A-8B2E-5D22-X             | 45.420 ns | 0.1530 ns | 0.1360 ns |         - |
| ISAN (Formatted)                | ISAN D02C-42E9-5418-3EE2-Q             | 44.310 ns | 0.2520 ns | 0.2360 ns |         - |
| ISAN (Formatted)                | ISAN E953-0C32-BC0E-E83B-2             | 50.080 ns | 0.2070 ns | 0.1840 ns |         - |
| ISAN (Formatted)                | ISAN C594-660A-8B2E-5D22-X-6DDA-3272-E | 64.650 ns | 0.3200 ns | 0.3000 ns |         - |
| ISAN (Formatted)                | ISAN D02C-42E9-5418-3EE2-Q-1291-C8AE-O | 65.820 ns | 0.3030 ns | 0.2840 ns |         - |
| ISAN (Formatted)                | ISAN E953-0C32-BC0E-E83B-2-6986-7B20-F | 64.220 ns | 0.3640 ns | 0.3400 ns |         - |
|                                 |                                        |           |           |           |           |                                           
| ISIN                            | AU0000XVGZA3                           | 18.710 ns | 0.1680 ns | 0.1400 ns |         - |
| ISIN                            | GB0002634946                           | 17.430 ns | 0.1080 ns | 0.0950 ns |         - |
| ISIN                            | US0378331005                           | 17.390 ns | 0.1190 ns | 0.1050 ns |         - |
|                                 |                                        |           |           |           |           |                                           
| ISO 6346                        | CSQU3054383                            | 14.970 ns | 0.0350 ns | 0.0280 ns |         - |
| ISO 6346                        | MSKU9070323                            | 14.890 ns | 0.0930 ns | 0.0870 ns |         - |
| ISO 6346                        | TOLU4734787                            | 14.840 ns | 0.0980 ns | 0.0870 ns |         - |
|                                 |                                        |           |           |           |           |                                           
| NHS                             | 4505577104                             | 11.280 ns | 0.0360 ns | 0.0340 ns |         - |
| NHS                             | 5301194917                             | 11.270 ns | 0.0400 ns | 0.0360 ns |         - |
| NHS                             | 9434765919                             | 11.270 ns | 0.0450 ns | 0.0430 ns |         - |
|                                 |                                        |           |           |           |           |                                           
| NPI                             | 1122337797                             | 12.790 ns | 0.1540 ns | 0.1440 ns |         - |
| NPI                             | 1234567893                             | 12.400 ns | 0.0670 ns | 0.0590 ns |         - |
| NPI                             | 1245319599                             | 12.610 ns | 0.0700 ns | 0.0660 ns |         - |
|                                 |                                        |           |           |           |           |                                           
| SEDOL                           | 3134865                                | 12.290 ns | 0.1440 ns | 0.1200 ns |         - |
| SEDOL                           | B0YQ5W0                                | 12.180 ns | 0.0630 ns | 0.0560 ns |         - |
| SEDOL                           | BRDVMH9                                | 12.220 ns | 0.0800 ns | 0.0710 ns |         - |
|                                 |                                        |           |           |           |           |                                           
| VIN                             | 1G8ZG127XWZ157259                      | 21.120 ns | 0.1160 ns | 0.1080 ns |         - |
| VIN                             | 1HGEM21292L047875                      | 20.920 ns | 0.0770 ns | 0.0690 ns |         - |
| VIN                             | 1M8GDM9AXKP042788                      | 21.050 ns | 0.0940 ns | 0.0830 ns |         - |
