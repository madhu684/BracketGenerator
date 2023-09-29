using BracketGeneratorTestApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BracketGeneratorTestApp.Repositories.Contract
{
    /// <summary>
    /// Represents a repository for tournament teams.
    /// </summary>
    public interface ITeamRepository
    {
        Dictionary<string, Team> GetAllTeams();
        Team GetTeam(string seed);
        void AddTeam(string seed, Team team);
    }
}
