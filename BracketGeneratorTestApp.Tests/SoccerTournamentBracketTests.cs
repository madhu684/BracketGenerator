using BracketGeneratorTestApp.BusinessServices;
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
    /// Unit tests for the <see cref="SoccerTournamentBracket"/> class.
    /// </summary>
    public class SoccerTournamentBracketTests
    {
        /// <summary>
        /// Ensures that <see cref="SoccerTournamentBracket.SeedTeam(string, string)"/> properly seeds a team
        /// when given a valid seed and team name.
        /// </summary>
        [Fact]
        public void SeedTeam_ShouldSeedTeam_WhenValidSeedAndTeamName()
        {
            // Arrange
            var teamRepository = new TeamRepository(new ConsoleLogger());
            var roundRepository = new RoundRepository();
            var logger = new ConsoleLogger();
            var bracket = new SoccerTournamentBracket(teamRepository, roundRepository, logger);

            // Act
            bracket.SeedTeam("1A", "Netherlands");

            // Assert
            var teams = teamRepository.GetAllTeams();
            Assert.Contains("1A", teams.Keys);
            Assert.Equal("Netherlands", teams["1A"].Name);
        }

        /// <summary>
        /// Verifies that <see cref="SoccerTournamentBracket.SeedTeam(string, string)"/> throws an <see cref="ArgumentException"/>
        /// when provided with a null seed.
        /// </summary>
        [Fact]
        public void SeedTeam_ShouldThrowArgumentException_WhenNullSeed()
        {
            // Arrange
            var teamRepository = new TeamRepository(new ConsoleLogger());
            var roundRepository = new RoundRepository();
            var logger = new ConsoleLogger();
            var soccerBracket = new SoccerTournamentBracket(teamRepository, roundRepository, logger);

            // Act and Assert
            Assert.Throws<ArgumentException>(() => soccerBracket.SeedTeam(null, "TeamName"));
        }

        /// <summary>
        /// Verifies that <see cref="SoccerTournamentBracket.SeedTeam(string, string)"/> throws an <see cref="ArgumentException"/>
        /// when provided with an empty team name.
        /// </summary>
        [Fact]
        public void SeedTeam_ShouldThrowArgumentException_WhenEmptyTeamName()
        {
            // Arrange
            var teamRepository = new TeamRepository(new ConsoleLogger());
            var roundRepository = new RoundRepository();
            var logger = new ConsoleLogger();
            var soccerBracket = new SoccerTournamentBracket(teamRepository, roundRepository, logger);

            // Act and Assert
            Assert.Throws<ArgumentException>(() => soccerBracket.SeedTeam("Seed", ""));
        }
    }
}
