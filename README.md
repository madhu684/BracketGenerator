# Soccer Tournament Bracket

This project simulates a soccer tournament bracket where teams compete against each other in a single-elimination format. The implementation includes classes for teams, rounds, and a tournament bracket.

## Table of Contents
- [Introduction](#introduction)
- [Instructions](#instructions)
  - [Building the Project](#building-the-project)
  - [Running Unit Tests](#running-unit-tests)
  - [Simulating a Tournament](#simulating-a-tournament)
- [Discussion of Technologies Used](#discussion-of-technologies-used)

## Introduction

The project is structured into classes representing tournament entities, such as teams and rounds. A `TournamentBracket` class handles the seeding, advancement, and simulation of the tournament.

## Instructions

### Building the Project

To build the project, follow these steps:

1. Clone the repository to your local machine.
2. Open the project in your preferred C# development environment.
3. Build the solution.

### Running Unit Tests

The project includes unit tests to verify the functionality of the `SoccerTournamentBracket` and `TournamentBracket` classes. To run the tests:

1. Open the test project in your C# development environment.
2. Run the tests.

### Simulating a Tournament

The `Program.cs` file in the main project contains an example of how to set up and run a tournament. Modify the `Main` method to configure the tournament as needed. The `RunTournament` method is responsible for seeding teams, simulating the tournament, and displaying the results.

```csharp
// Example for a 16-team soccer bracket
RunTournament(tournamentBracket, teamRepository, logger, 16);

// Example for a 64-team NCAA soccer bracket
// RunTournament(tournamentBracket, teamRepository, logger, 64);

## Discussion of Technologies Used

The project leverages the following technologies:

C#: The primary language used for coding the application logic.
.NET Core: Provides cross-platform compatibility and runtime support for the application.
xUnit: Utilized for writing and running unit tests to ensure the correctness of the implemented logic.
Dependency Injection: Used for managing class dependencies and promoting modularity within the application.