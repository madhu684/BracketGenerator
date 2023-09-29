using BracketGeneratorTestApp.BusinessServices;
using BracketGeneratorTestApp.Models;
using BracketGeneratorTestApp.Repositories;
using BracketGeneratorTestApp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BracketGeneratorTestApp.Tests
{
    /// <summary>
    /// Test classes for <see cref="TournamentBracket"/>
    /// </summary>
    public class TournamentBracketTests
    {
        /// <summary>
        /// Verifies that <see cref="TournamentBracket.AdvanceTeam(string)"/> advances a team when given a valid seed.
        /// </summary>
        [Fact]
        public void AdvanceTeam_ShouldAdvanceTeam_WhenValidSeed()
        {
            // Arrange
            var teamRepository = new TeamRepository(new ConsoleLogger());
            var roundRepository = new RoundRepository();
            var logger = new ConsoleLogger();
            var bracket = new TournamentBracket(teamRepository, roundRepository, logger);
            bracket.SeedTeam("1A", "Netherlands");
            bracket.SeedTeam("2A", "Qatar");

            // Act
            bracket.AdvanceTeam("1A");

            // Assert
            var winners = bracket.GetWinners();
            Assert.Contains("1A", winners.Keys);
            Assert.Equal("Netherlands", winners["1A"].Name);
        }

        /// <summary>
        /// Verifies that <see cref="TournamentBracket.GetTournamentWinner"/> returns the winner when the tournament is completed.
        /// </summary>
        [Fact]
        public void GetTournamentWinner_ShouldReturnWinner_WhenTournamentCompleted()
        {
            // Arrange
            var teamRepository = new TeamRepository(new ConsoleLogger());
            var roundRepository = new RoundRepository();
            var logger = new ConsoleLogger();
            var bracket = new TournamentBracket(teamRepository, roundRepository, logger);

            // Manually simulate the tournament
            bracket.SeedTeam("1A", "Netherlands");
            bracket.SeedTeam("2A", "Qatar");
            bracket.AdvanceTeam("1A");

            // Add the rounds to the repository
            var rounds = bracket.GetWinners().Values.Select(winner => new Round
            {
                WinnerTeams = new List<Team> { winner },
            }).ToList();

            foreach (var round in rounds)
            {
                roundRepository.AddRound(round);
            }

            // Act
            var winner = bracket.GetTournamentWinner();

            // Assert
            Assert.NotNull(winner);
            Assert.Equal("Netherlands", winner.Name);
        }

        /// <summary>
        /// Verifies that <see cref="TournamentBracket.PathToVictory"/> logs the path to victory when called.
        /// </summary>
        [Fact]
        public void PathToVictory_ShouldLogPathToVictory_WhenCalled()
        {
            // Arrange
            var teamRepository = new TeamRepository(new ConsoleLogger());
            var roundRepository = new RoundRepository();
            var logger = new ConsoleLogger();
            var bracket = new TournamentBracket(teamRepository, roundRepository, logger);

            // Manually simulate the tournament
            bracket.SeedTeam("1A", "Netherlands");
            bracket.SeedTeam("2A", "Qatar");
            bracket.AdvanceTeam("1A");

            // Add the rounds to the repository
            var rounds = bracket.GetWinners().Values.Select(winner => new Round
            {
                Name = "Finals",
                Teams = teamRepository.GetAllTeams().Select(t => t.Value).ToList(),
                TeamCount = teamRepository.GetAllTeams().Select(t => t.Value).Count(),
                WinnerTeams = new List<Team> { winner },
                WinnerTeamCount = 1
            }).ToList();

            foreach (var round in rounds)
            {
                roundRepository.AddRound(round);
            }

            // Redirect console output
            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            try
            {
                // Act
                bracket.PathToVictory();
                // Print actual outtput for debugging
                var actualOutput = consoleOutput.ToString().Trim();

                // Assert
                Assert.Contains("[INFO] Path To Victory...!!!:", actualOutput);
                Assert.Contains("[INFO] Round: Finals", actualOutput);
                Assert.Contains("[INFO] Participated Teams Count: 2", actualOutput);
                Assert.Contains("[INFO] Participated Teams: Netherlands,Qatar", actualOutput);
                Assert.Contains("[INFO] Winner Teams Count: 1", actualOutput);
                Assert.Contains("[INFO] Winner Teams: Netherlands", actualOutput);
            }
            finally
            {
                // Clean up: Restore the original console output
                Console.SetOut(Console.Out);
            }
        }

        /// <summary>
        /// Verifies that <see cref="TournamentBracket.AdvanceTeam(string)"/> throws <see cref="ArgumentException"/> when given a null seed.
        /// </summary>
        [Fact]
        public void AdvanceTeam_ShouldThrowArgumentException_WhenNullSeed()
        {
            // Arrange
            var teamRepository = new TeamRepository(new ConsoleLogger());
            var roundRepository = new RoundRepository();
            var logger = new ConsoleLogger();
            var bracket = new TournamentBracket(teamRepository, roundRepository, logger);

            // Act and Assert
            Assert.Throws<ArgumentException>(() => bracket.AdvanceTeam(null));
        }

        /// <summary>
        /// Verifies that <see cref="TournamentBracket.AdvanceTeam(string)"/> throws <see cref="ArgumentException"/> when given an empty seed.
        /// </summary>
        [Fact]
        public void AdvanceTeam_ShouldThrowArgumentException_WhenEmptySeed()
        {
            // Arrange
            var teamRepository = new TeamRepository(new ConsoleLogger());
            var roundRepository = new RoundRepository();
            var logger = new ConsoleLogger();
            var bracket = new TournamentBracket(teamRepository, roundRepository, logger);

            // Act and Assert
            Assert.Throws<ArgumentException>(() => bracket.AdvanceTeam(""));
        }

        /// <summary>
        /// Verifies that <see cref="TournamentBracket.GetTournamentWinner"/> returns null when no rounds are added.
        /// </summary>
        [Fact]
        public void GetTournamentWinner_ShouldReturnNull_WhenNoRoundsAdded()
        {
            // Arrange
            var teamRepository = new TeamRepository(new ConsoleLogger());
            var roundRepository = new RoundRepository();
            var logger = new ConsoleLogger();
            var bracket = new TournamentBracket(teamRepository, roundRepository, logger);

            // Act
            var winner = bracket.GetTournamentWinner();

            // Assert
            Assert.Null(winner);
        }

        /// <summary>
        /// Verifies that <see cref="TournamentBracket.PathToVictory"/> throws <see cref="InvalidOperationException"/> when no rounds are added.
        /// </summary>
        [Fact]
        public void PathToVictory_ShouldThrowInvalidOperationException_WhenNoRoundsAdded()
        {
            // Arrange
            var teamRepository = new TeamRepository(new ConsoleLogger());
            var roundRepository = new RoundRepository();
            var logger = new ConsoleLogger();
            var bracket = new TournamentBracket(teamRepository, roundRepository, logger);

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => bracket.PathToVictory());
        }
    }
}
