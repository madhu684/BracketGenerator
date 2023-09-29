using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BracketGeneratorTestApp.Models
{
    /// <summary>
    /// Represents a team in a tournament.
    /// </summary>
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Team"/> class.
        /// </summary>
        /// <param name="name">The name of the team.</param>
        public Team(string name)
        {
            Name = name;
        }
    }
}
