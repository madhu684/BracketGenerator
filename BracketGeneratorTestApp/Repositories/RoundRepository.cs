using BracketGeneratorTestApp.Models;
using BracketGeneratorTestApp.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BracketGeneratorTestApp.Repositories
{
    /// <summary>
    /// Represents a round repository implementation.
    /// </summary>
    public class RoundRepository : IRoundRepository
    {
        private readonly List<Round> _rounds;

        public RoundRepository()
        {
            _rounds = new List<Round>();
        }

        /// <inheritdoc/>
        public void AddRound(Round round)
        {
            _rounds.Add(round);
        }

        /// <inheritdoc/>
        public List<Round> GetAllRounds()
        {
            return _rounds;
        }
    }
}
