using BracketGeneratorTestApp.Models;
using BracketGeneratorTestApp.Repositories.Contract;
using BracketGeneratorTestApp.Shared.Contract;

namespace BracketGeneratorTestApp.BusinessServices
{
    /// <summary>
    /// Represents an NCAA soccer tournament bracket, inheriting from the base tournament bracket.
    /// </summary>
    public class NcaaSoccerTournamentBracket : TournamentBracket
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NcaaSoccerTournamentBracket"/> class.
        /// </summary>
        /// <param name="teamRepository">The team repository.</param>
        /// <param name="roundRepository">The round repository.</param>
        /// <param name="logger">The logger.</param>
        public NcaaSoccerTournamentBracket(ITeamRepository teamRepository, IRoundRepository roundRepository, ILogger logger) : base(teamRepository, roundRepository, logger)
        {

        }

        /// <summary>
        /// Seeds an NCAA soccer team in the tournament bracket.
        /// </summary>
        /// <param name="seed">The seed of the NCAA soccer team.</param>
        /// <param name="teamName">The name of the NCAA soccer team.</param>
        /// <exception cref="ArgumentException">Thrown when seed or teamName is null or empty.</exception>
        public override void SeedTeam(string seed, string teamName)
        {
            if (string.IsNullOrWhiteSpace(seed))
            {
                throw new ArgumentException("Seed cannot be null or empty.", nameof(seed));
            }

            if (string.IsNullOrWhiteSpace(teamName))
            {
                throw new ArgumentException("Team name cannot be null or empty.", nameof(teamName));
            }

            try
            {
                var team = new Team(teamName);
                _teamRepository.AddTeam(seed, team);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in SeedTeam: {ex.Message}", ex);
            }
        }
    }
}
