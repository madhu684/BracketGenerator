using BracketGeneratorTestApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BracketGeneratorTestApp.BusinessServices.Contract
{
    /// <summary>
    /// Represents a tournament bracket.
    /// </summary>
    public interface ITournamentBracket
    {
        /// <summary>
        /// Seeds a team in the tournament bracket.
        /// </summary>
        /// <param name="seed">The seed of the team.</param>
        /// <param name="teamName">The name of the team.</param>
        void SeedTeam(string seed, string teamName);

        /// <summary>
        /// Advances a team in the tournament bracket.
        /// </summary>
        /// <param name="seed">The seed of the team to advance.</param>
        void AdvanceTeam(string seed);

        /// <summary>
        /// Gets the tournament winner.
        /// </summary>
        Team GetTournamentWinner();

        /// <summary>
        /// Displays the path to victory in the tournament.
        /// </summary>
        void PathToVictory();

        /// <summary>
        /// Adds a tournament round to the bracket.
        /// </summary>
        /// <param name="round">The round to add.</param>
        void AddRound(Round round);

        /// <summary>
        /// Clears the dictionary of winners.
        /// </summary>
        void ClearWinners();

        /// <summary>
        /// Gets the dictionary of winners.
        /// </summary>
        /// <returns>The dictionary of winners where keys are team seeds and values are winning teams.</returns>
        Dictionary<string, Team> GetWinners();
    }
}
