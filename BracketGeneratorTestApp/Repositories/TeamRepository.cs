using BracketGeneratorTestApp.Models;
using BracketGeneratorTestApp.Repositories.Contract;
using BracketGeneratorTestApp.Shared.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BracketGeneratorTestApp.Repositories
{
    /// <summary>
    /// Represents a team repository implementation.
    /// </summary>
    public class TeamRepository : ITeamRepository
    {
        private readonly ILogger _logger;
        private readonly Dictionary<string, Team> _teams;

        public TeamRepository(ILogger logger)
        {
            _logger = logger;
            _teams = new Dictionary<string, Team>();
        }

        /// <inheritdoc/>
        public void AddTeam(string seed, Team team)
        {
            if (string.IsNullOrWhiteSpace(seed))
            {
                throw new ArgumentException("Seed cannot be null or empty.", nameof(seed));
            }

            if (team == null)
            {
                throw new ArgumentNullException(nameof(team), "Team cannot be null.");
            }
            _teams[seed] = team;
        }

        /// <inheritdoc/>
        public Dictionary<string, Team> GetAllTeams()
        {
            return _teams;
        }

        /// <inheritdoc/>
        public Team GetTeam(string seed)
        {
            try
            {
                if (_teams.TryGetValue(seed, out var team))
                {
                    return team;
                }
                else
                {
                    // Handle the case where the key is not found (return null, throw an exception, etc.)
                    throw new KeyNotFoundException($"Team with seed '{seed}' not found.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception or take appropriate action based on your application's error handling strategy.
                _logger.LogError($"Error in GetTeam: {ex.Message}", ex);
                return null;
            }
        }
    }
}
