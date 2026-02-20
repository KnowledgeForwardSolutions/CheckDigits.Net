# S0062: MakeIcao9393ClassesStateless

## Story
**As a** .NET developer  
**I want** the Icao9303MachineReadableVisaAlgorithm, Icao9303TD1Algorithm, Icao9303TD2Algorithm and Icao9303TD3Algorithm classes provided by **CheckDigits.Net** to have the same parameterless constructor, stateless-ness and thread-safety as other algorithms in **CheckDigits.Net**
**So that** I can use these algorithms in data annotations with line separators other than the default line separator (i.e. LineSeparator.None).

## Background
Icao9303MachineReadableVisaAlgorithm, Icao9303TD1Algorithm, Icao9303TD2Algorithm and Icao9303TD3Algorithm all support a LineSeparator property that makes the algorithm classes stateful (and worse, can cause the behavior of the laizly defined instances to change if LineSeparator is set for those instances).

LineSeparator is not necessary as the correct separator can be determined by the actual length of the input value.

## Acceptance Criteria
- Depreciate the LineSeparator property of Icao9303MachineReadableVisaAlgorithm, Icao9303TD1Algorithm, Icao9303TD2Algorithm and Icao9303TD3Algorithm
- Update Icao9303MachineReadableVisaAlgorithm, Icao9303TD1Algorithm, Icao9303TD2Algorithm and Icao9303TD3Algorithm to calculate the correct line separator from teh actual length of the input value.

## Definition of Done
- LineSeparator is depreciated and does nothing.
- Existing tests all pass
- Additional tests added as needed
- README updates
