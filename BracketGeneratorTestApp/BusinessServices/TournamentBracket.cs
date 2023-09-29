using BracketGeneratorTestApp.BusinessServices.Contract;
using BracketGeneratorTestApp.Models;
using BracketGeneratorTestApp.Repositories.Contract;
using BracketGeneratorTestApp.Shared.Contract;

namespace BracketGeneratorTestApp.BusinessServices
{
    /// <summary>
    /// Represents a tournament bracket implementation.
    /// </summary>
    public class TournamentBracket : ITournamentBracket
    {
        protected readonly ITeamRepository _teamRepository;
        protected readonly IRoundRepository _roundRepository;
        protected readonly ILogger _logger;
        private Dictionary<string, Team> _winners;

        /// <summary>
        /// Initializes a new instance of the <see cref="TournamentBracket"/> class.
        /// </summary>
        /// <param name="teamRepository">The team repository.</param>
        /// <param name="roundRepository">The round repository.</param>
        public TournamentBracket(ITeamRepository teamRepository, IRoundRepository roundRepository, ILogger logger)
        {
            _teamRepository = teamRepository;
            _roundRepository = roundRepository;
            _logger = logger;
            _winners = new Dictionary<string, Team>();
        }

        /// <inheritdoc/>
        public Dictionary<string, Team> GetWinners()
        {
            return _winners;
        }

        /// <inheritdoc/>
        public void ClearWinners()
        {
            if (_winners == null)
            {
                throw new InvalidOperationException("Winners dictionary is not initialized.");
            }
            _winners.Clear();
        }

        /// <inheritdoc/>
        public virtual void SeedTeam(string seed, string teamName)
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

        /// <inheritdoc/>
        public void AdvanceTeam(string seed)
        {
            if (string.IsNullOrWhiteSpace(seed))
            {
                throw new ArgumentException("Seed cannot be null or empty.", nameof(seed));
            }

            try
            {
                var team = _teamRepository.GetTeam(seed);

                if (team != null)
                {
                    _winners[seed] = team;
                }
                else
                {
                    // Handle the case where the team is not found (throw an exception.)
                    throw new InvalidOperationException($"Team with seed '{seed}' not found.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in AdvanceTeam: {ex.Message}", ex);
            }
        }

        /// <inheritdoc/>
        public Team GetTournamentWinner()
        {
            try
            {
                return _roundRepository.GetAllRounds()?.LastOrDefault()?.WinnerTeams.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetTournamentWinner: {ex.Message}", ex);
                throw ex;
            }
        }

        /// <inheritdoc/>
        public void PathToVictory()
        {
            var rounds = _roundRepository.GetAllRounds();
            if (rounds == null || !rounds.Any())
            {
                throw new InvalidOperationException("Rounds list is not initialized.");
            }

            try
            {
                _logger.LogInformation("Path To Victory...!!!:");
                foreach (var round in rounds)
                {
                    _logger.LogInformation($"Round: {round.Name}");
                    _logger.LogInformation($"Participated Teams Count: {round.TeamCount}");
                    _logger.LogInformation($"Participated Teams: {string.Join(",", round.Teams.Select(t => t.Name))}");
                    _logger.LogInformation($"Winner Teams Count: {round.WinnerTeamCount}");
                    _logger.LogInformation($"Winner Teams: {string.Join(",", round.WinnerTeams.Select(t => t.Name))}\n");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetTournamentWinner: {ex.Message}", ex);
            }
        }

        public void AddRound(Round round)
        {
            _roundRepository.AddRound(round);
        }
    }
}
