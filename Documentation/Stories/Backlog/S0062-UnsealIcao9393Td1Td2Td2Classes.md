# S0062: UnsealIcao9393Td1Td2Td2Classes

## Story
**As a** .NET developer  
**I want** to unseal the Icao9303TD1Algorithm, Icao9303TD2Algorithm and Icao9303TD2Algorithm classes provided by **CheckDigits.Net**
**So that** I can use these algorithms in data annotations with line separators other than the default line separator (i.e. LineSeparator.None).

## Background
**CheckDigits.Net.Annotations** implements CheckDigitAttribute<TAlgorithm> which requires TAlgorithm to have a parameterless constructor. This means that to CheckDigitAttribute<Icao9303TD1Algorithm> will only use the default line separator as there is no opportunity to set a different separator. The preferred solution would be to derive a custom algorithm and set the line separator in the constructor, for example

```csharp
public class Icao9303TD1AlgorithmCrLf : Icao9303TD1Algorithm
{
    public Icao9303TD1AlgorithmCrlf()
    {
        LineSeparator = LineSeparator.CrLf
    }
}
```

However, Icao9303TD1Algorithm is sealed and it is not possible to derive a new version. Unsealing the class will allow new versions to be derived.

## Acceptance Criteria
- unseal Icao9303TD1Algorithm.
- unseal Icao9303TD2Algorithm.
- unseal Icao9303TD3Algorithm.

## Definition of Done
- Icao9303TD1Algorithm, Icao9303TD2Algorithm and Icao9303TD3Algorithm are unsealed. 
- Unit tests for CheckDigitAttribute updated to use derived versions of these classes with non default separators.
- README updates describing how to use these algorithms with CheckDigitAttribute and with non default separators.
