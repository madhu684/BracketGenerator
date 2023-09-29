using BracketGeneratorTestApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BracketGeneratorTestApp.Repositories.Contract
{
    /// <summary>
    /// Represents a repository for tournament rounds.
    /// </summary>
    public interface IRoundRepository
    {
        void AddRound(Round round);
        List<Round> GetAllRounds();
    }
}
