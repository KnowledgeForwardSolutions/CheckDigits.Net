
## Comparison Benchmarks, CheckDigits.Net, v2.1.0

### Algorithms

[Luhn Algorithm](#luhn-algorithm)
[Damm Algorithm](#damm-algorithm)
[Modulus11 Algorithm (for ISBN-10)](#modulus11-algorithm-for-isbn-10)
[Modulus10_13 Algorithm (for ISBN-13)](#modulus10_13-algorithm-for-isbn-13)

#### All benchmark details

BenchmarkDotNet v0.13.10, Windows 11 (10.0.22621.2715/22H2/2022Update/SunValley2)
Intel Core i7-8700K CPU 3.70GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET SDK 8.0.100
  [Host]     : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2

### Luhn Algorithm

CreditCardValidator, v3.0.1, 1.4M downloads
Tharga.Toolkit, V1.10.17, 233K downloads
LuhnNet, v2.0.0, 118K downloads

Download stats current as of 11/2023

| Library             | Value                  | Mean         | Error      | StdDev     | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|-------------------- |----------------------- |-------------:|-----------:|-----------:|------:|--------:|-------:|----------:|------------:|
| CheckDigits.Net     | 1404                   |     6.669 ns |  0.1020 ns |  0.0904 ns |  1.00 |    0.00 |      - |         - |          NA |
| LuhnNet             | 1404                   |    53.837 ns |  0.3644 ns |  0.3409 ns |  8.07 |    0.13 | 0.0153 |      96 B |          NA |
| Tharga.Toolkit      | 1404                   |   122.759 ns |  0.9389 ns |  0.7840 ns | 18.38 |    0.25 | 0.0648 |     408 B |          NA |
| CreditCardValidator | 1404                   |   326.582 ns |  2.4097 ns |  2.2541 ns | 48.96 |    0.76 | 0.1297 |     816 B |          NA |
|                     |                        |              |            |            |       |         |        |           | |
| CheckDigits.Net     | 1406628                |     8.484 ns |  0.0442 ns |  0.0392 ns |  1.00 |    0.00 |      - |         - |          NA |
| LuhnNet             | 1406628                |    79.575 ns |  0.6078 ns |  0.5686 ns |  9.37 |    0.07 | 0.0267 |     168 B |          NA |
| Tharga.Toolkit      | 1406628                |   151.827 ns |  1.5900 ns |  1.4873 ns | 17.89 |    0.20 | 0.0751 |     472 B |          NA |
| CreditCardValidator | 1406628                |   429.856 ns |  4.1286 ns |  3.6599 ns | 50.67 |    0.50 | 0.1655 |    1040 B |          NA |
|                     |                        |              |            |            |       |         |        |           | |
| CheckDigits.Net     | 1406625382             |    11.891 ns |  0.0770 ns |  0.0643 ns |  1.00 |    0.00 |      - |         - |          NA |
| LuhnNet             | 1406625382             |   106.920 ns |  1.8135 ns |  1.5144 ns |  8.99 |    0.09 | 0.0381 |     240 B |          NA |
| Tharga.Toolkit      | 1406625382             |   178.213 ns |  0.9346 ns |  0.7805 ns | 14.99 |    0.11 | 0.0918 |     576 B |          NA |
| CreditCardValidator | 1406625382             |   643.437 ns |  3.5987 ns |  3.3662 ns | 54.12 |    0.37 | 0.2279 |    1432 B |          NA |
|                     |                        |              |            |            |       |         |        |           | |
| CheckDigits.Net     | 1406625380421          |    13.599 ns |  0.1420 ns |  0.1328 ns |  1.00 |    0.00 |      - |         - |          NA |
| LuhnNet             | 1406625380421          |   134.946 ns |  1.5631 ns |  1.4621 ns |  9.92 |    0.18 | 0.0496 |     312 B |          NA |
| Tharga.Toolkit      | 1406625380421          |   196.947 ns |  1.4857 ns |  1.3170 ns | 14.49 |    0.18 | 0.0930 |     584 B |          NA |
| CreditCardValidator | 1406625380421          |   670.235 ns |  5.1838 ns |  4.8489 ns | 49.29 |    0.60 | 0.2508 |    1576 B |          NA |
|                     |                        |              |            |            |       |         |        |           | |
| CheckDigits.Net     | 1406625380425514       |    17.431 ns |  0.1086 ns |  0.1016 ns |  1.00 |    0.00 |      - |         - |          NA |
| LuhnNet             | 1406625380425514       |   163.974 ns |  1.1490 ns |  1.0186 ns |  9.41 |    0.08 | 0.0610 |     384 B |          NA |
| Tharga.Toolkit      | 1406625380425514       |   216.048 ns |  2.0956 ns |  1.9602 ns | 12.39 |    0.10 | 0.0956 |     600 B |          NA |
| CreditCardValidator | 1406625380425514       |   816.885 ns |  8.4729 ns |  7.5110 ns | 46.89 |    0.39 | 0.2804 |    1760 B |          NA |
|                     |                        |              |            |            |       |         |        |           | |
| CheckDigits.Net     | 1406625380425510285    |    19.423 ns |  0.1049 ns |  0.0930 ns |  1.00 |    0.00 |      - |         - |          NA |
| LuhnNet             | 1406625380425510285    |   188.793 ns |  0.9633 ns |  0.9011 ns |  9.72 |    0.05 | 0.0725 |     456 B |          NA |
| Tharga.Toolkit      | 1406625380425510285    |   246.072 ns |  1.6052 ns |  1.4229 ns | 12.67 |    0.10 | 0.1211 |     760 B |          NA |
| CreditCardValidator | 1406625380425510285    |   980.461 ns |  9.7749 ns |  8.6652 ns | 50.48 |    0.58 | 0.3338 |    2096 B |          NA |
|                     |                        |              |            |            |       |         |        |           | |
| CheckDigits.Net     | 1406625380425510282651 |    23.074 ns |  0.1953 ns |  0.1827 ns |  1.00 |    0.00 |      - |         - |          NA |
| LuhnNet             | 1406625380425510282651 |   241.914 ns |  3.9322 ns |  5.3824 ns | 10.45 |    0.25 | 0.0839 |     528 B |          NA |
| Tharga.Toolkit      | 1406625380425510282651 |   265.930 ns |  2.4183 ns |  2.2621 ns | 11.53 |    0.11 | 0.1235 |     776 B |          NA |
| CreditCardValidator | 1406625380425510282651 | 1,077.657 ns | 13.7246 ns | 12.1665 ns | 46.69 |    0.71 | 0.3719 |    2344 B |          NA |

### Damm Algorithm

Medo.Checksums, v1.2.0, 344 downloads as of 11/2023

| Library             | Value                  | Mean       | Error     | StdDev    | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|-------------------- |----------------------- |-----------:|----------:|----------:|------:|--------:|-------:|----------:|------------:|
| CheckDigits.Net     | 1402                   |   5.792 ns | 0.0419 ns | 0.0350 ns |  1.00 |    0.00 |      - |         - |          NA |
| Medo.Checksums      | 1402                   |  45.067 ns | 0.3087 ns | 0.2737 ns |  7.78 |    0.07 | 0.0140 |      88 B |          NA |
|                     |                        |            |           |           |       |         |        |           |             |
| CheckDigits.Net     | 1406622                |   6.149 ns | 0.0339 ns | 0.0300 ns |  1.00 |    0.00 |      - |         - |          NA |
| Medo.Checksums      | 1406622                |  51.984 ns | 0.5972 ns | 0.5586 ns |  8.46 |    0.11 | 0.0153 |      96 B |          NA |
|                     |                        |            |           |           |       |         |        |           |             |
| CheckDigits.Net     | 1406625388             |   9.424 ns | 0.1183 ns | 0.1107 ns |  1.00 |    0.00 |      - |         - |          NA |
| Medo.Checksums      | 1406625388             |  61.026 ns | 0.8882 ns | 0.7873 ns |  6.47 |    0.11 | 0.0166 |     104 B |          NA |
|                     |                        |            |           |           |       |         |        |           |             |
| CheckDigits.Net     | 1406625380422          |  14.346 ns | 0.0679 ns | 0.0602 ns |  1.00 |    0.00 |      - |         - |          NA |
| Medo.Checksums      | 1406625380422          |  69.178 ns | 0.7149 ns | 0.6687 ns |  4.83 |    0.04 | 0.0178 |     112 B |          NA |
|                     |                        |            |           |           |       |         |        |           |             |
| CheckDigits.Net     | 1406625380425518       |  17.368 ns | 0.1460 ns | 0.1295 ns |  1.00 |    0.00 |      - |         - |          NA |
| Medo.Checksums      | 1406625380425518       |  80.021 ns | 0.7367 ns | 0.6891 ns |  4.60 |    0.04 | 0.0191 |     120 B |          NA |
|                     |                        |            |           |           |       |         |        |           |             |
| CheckDigits.Net     | 1406625380425510280    |  20.804 ns | 0.1160 ns | 0.1085 ns |  1.00 |    0.00 |      - |         - |          NA |
| Medo.Checksums      | 1406625380425510280    |  91.580 ns | 0.9221 ns | 0.8174 ns |  4.40 |    0.04 | 0.0216 |     136 B |          NA |
|                     |                        |            |           |           |       |         |        |           |             |
| CheckDigits.Net     | 1406625380425510282654 |  24.329 ns | 0.1983 ns | 0.1758 ns |  1.00 |    0.00 |      - |         - |          NA |
| Medo.Checksums      | 1406625380425510282654 | 100.612 ns | 0.6115 ns | 0.5720 ns |  4.13 |    0.04 | 0.0216 |     136 B |          NA |

### Modulus11 Algorithm (for ISBN-10)

Nager.ArticleNumber, v1.0.7, 169K downloads as of 11/2023

| Library             | Value      | Mean      | Error     | StdDev    | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|-------------------- |----------- |----------:|----------:|----------:|------:|--------:|-------:|----------:|------------:|
| CheckDigits.Net     | 050027293X |  9.015 ns | 0.0998 ns | 0.0885 ns |  1.00 |    0.00 |      - |         - |          NA |
| Nager.ArticleNumber | 050027293X | 32.986 ns | 0.3550 ns | 0.3147 ns |  3.66 |    0.06 | 0.0063 |      40 B |          NA |
|                     |            |           |           |           |       |         |        |           |             |
| CheckDigits.Net     | 0714105449 |  8.562 ns | 0.0975 ns | 0.0912 ns |  1.00 |    0.00 |      - |         - |          NA |
| Nager.ArticleNumber | 0714105449 | 39.439 ns | 0.3110 ns | 0.2909 ns |  4.61 |    0.06 | 0.0102 |      64 B |          NA |
|                     |            |           |           |           |       |         |        |           |             |
| CheckDigits.Net     | 1568656521 |  8.524 ns | 0.0740 ns | 0.0656 ns |  1.00 |    0.00 |      - |         - |          NA |
| Nager.ArticleNumber | 1568656521 | 43.842 ns | 0.5060 ns | 0.4486 ns |  5.14 |    0.06 | 0.0102 |      64 B |          NA |

### Modulus10_13 Algorithm (for ISBN-13)

|                     | Value         | Mean     | Error    | StdDev   | Ratio | RatioSD | Allocated | Alloc Ratio |
|-------------------- |-------------- |---------:|---------:|---------:|------:|--------:|----------:|------------:|
| CheckDigits.Net     | 9780500516959 | 16.04 ns | 0.078 ns | 0.069 ns |  1.00 |    0.00 |         - |          NA |
| Nager.ArticleNumber | 9780500516959 | 50.63 ns | 0.160 ns | 0.150 ns |  3.16 |    0.02 |         - |          NA |
|                     |               |          |          |          |       |         |           |             |
| CheckDigits.Net     | 9780691161730 | 16.50 ns | 0.130 ns | 0.116 ns |  1.00 |    0.00 |         - |          NA |
| Nager.ArticleNumber | 9780691161730 | 48.05 ns | 0.285 ns | 0.253 ns |  2.91 |    0.03 |         - |          NA |
|                     |               |          |          |          |       |         |           |             |
| CheckDigits.Net     | 9781861978769 | 12.97 ns | 0.068 ns | 0.063 ns |  1.00 |    0.00 |         - |          NA |
| Nager.ArticleNumber | 9781861978769 | 49.03 ns | 0.420 ns | 0.393 ns |  3.78 |    0.04 |         - |          NA |
