// Main program
using BracketGeneratorTestApp.BusinessServices;
using BracketGeneratorTestApp.BusinessServices.Contract;
using BracketGeneratorTestApp.Helpers;
using BracketGeneratorTestApp.Repositories;
using BracketGeneratorTestApp.Repositories.Contract;
using BracketGeneratorTestApp.Shared;
using BracketGeneratorTestApp.Shared.Contract;
using Microsoft.Extensions.DependencyInjection;
public class Program
{
    static void Main()
    {
        // Setup DI container
        var serviceProvider = new ServiceCollection()
            .AddScoped<ITeamRepository, TeamRepository>()
            .AddScoped<IRoundRepository, RoundRepository>()
            .AddScoped<ITournamentBracket, SoccerTournamentBracket>()
            //.AddScoped<ITournamentBracket, NcaaSoccerTournamentBracket>()
            .AddScoped<ILogger, ConsoleLogger>()  // Add logging implementation
            .BuildServiceProvider();

       var tournamentBracket = serviceProvider.GetRequiredService<ITournamentBracket>();

        var teamRepository = serviceProvider.GetRequiredService<ITeamRepository>();
        var logger = serviceProvider.GetRequiredService<ILogger>();

        // Example for a 16-team soccer bracket
        RunTournament(tournamentBracket, teamRepository, logger, 16);

        // Example for a 64-team NCAA soccer bracket
        //RunTournament(tournamentBracket, teamRepository, logger, 64);
    }

    static void RunTournament(ITournamentBracket bracket, ITeamRepository teamRepository, ILogger logger, int teamCount)
    {
        try
        {

            // Seed teams
            TournamentBracketHelper.SeedTeams(bracket, logger, teamCount);

            // Simulate the tournament by advancing teams
            TournamentBracketHelper.SimulateTournament(teamRepository, bracket, logger, teamCount);

            // Get and print the winner
            logger.LogInformation($"Tournament Winner: {bracket.GetTournamentWinner()?.Name}\n\n");

            // Print the path to victory
            bracket.PathToVictory();

            logger.LogInformation("Tournament completed successfully!");
        }
        catch (Exception ex)
        {
            logger.LogError($"Error in RunTournament: {ex.Message}", ex);
        }
    }
}
