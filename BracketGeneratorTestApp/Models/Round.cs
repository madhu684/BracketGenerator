using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BracketGeneratorTestApp.Models
{
    /// <summary>
    /// Represents a round in a tournament.
    /// </summary>
    public class Round
    {
        public int RoundId { get; set; }
        public string Name { get; set; }
        public int TeamCount { get; set; }
        public int WinnerTeamCount { get; set; }
        public List<Team> Teams { get; set; }
        public List<Team> WinnerTeams { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Round"/> class.
        /// </summary>
        public Round()
        {
            Teams = new List<Team>();
            WinnerTeams = new List<Team>();
        }
    }
}
