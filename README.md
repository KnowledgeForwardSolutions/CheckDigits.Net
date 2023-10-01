# CheckDigits.Net

CheckDigits.Net brings together in one library an extensive collection of optimized
implementations of check digit algorithms.

## Supported Algorithms

* [Luhn Algorithm](#luhn-algorithm)

### Luhn Algorithm

#### Description

The Luhn algorithm is a modulus 10 algorithm that was developed in 1960 by Hans
Peter Luhn. It can detect all single digit transcription errors and most two digit
transposition errors except 09 -> 90 and vice versa. It can also detect
most twin errors (i.e. 11 <-> 44) except 22 <-> 55,  33 <-> 66 and 44 <-> 77.

#### Common Applications

* Credit card numbers
* IMEI (International Mobile Equipment Identity) numbers
* A wide variety of government identifiers. See Wikipedia link for more info.

#### Links

Wikipedia: https://en.wikipedia.org/wiki/Luhn_algorithm
