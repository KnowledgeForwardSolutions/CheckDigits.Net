# S0065: DammAlgorithmCustomQuasigroups

## Story
**As a** .NET developer  
**I want** to be able to use custom quasigroups with the Damm algorithm
**So that** I can extend the algorithm to symbol sets other than digits.

## Background
The existing implementation of the Damm algorithm uses the most common quasigroup that is limited to digits only. However the algorithm is not limited to that one quasigroup. The single common quasigroup was used in the existing implementation because creation of quasigroups is outside of the scope of CheckDigits.Net. But with the advent of AI, a developer can request that a tool such as ChatGPT create a quasigroup of any size desired. 

## Acceptance Criteria
- Definition of IDammQuasigroup interface. Includes indexer that supports 2-D lookup of values in the quasigroup, a CharacterToInteger method to convert a symbol in the value to its integer equivalent and an IntegerToCharacter method to convert an integer value to its equivalent character in the valid symbol set.
- Definition of DammCustomQuasigroupAlgorithm that requires an IDammQuasigroup object in the constructor and that implements ISingleCheckDigitAlgorithm

## Definition of Done
- New IDammQuasigroup interface
- New DammCustomQuasigroupAlgorithm implemented
- Unit tests for Validate and TryCalculateCheckDigit
- README updates, including example implementations of IDammQuasigroup and suggestions for best performance using a flattened two dimensional array flattened to a one dimensional array
