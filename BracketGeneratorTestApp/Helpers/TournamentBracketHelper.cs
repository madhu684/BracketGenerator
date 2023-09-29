using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BracketGeneratorTestApp.BusinessServices;
using BracketGeneratorTestApp.BusinessServices.Contract;
using BracketGeneratorTestApp.Models;
using BracketGeneratorTestApp.Repositories;
using BracketGeneratorTestApp.Repositories.Contract;
using BracketGeneratorTestApp.Shared.Contract;
using Microsoft.Extensions.DependencyInjection;

namespace BracketGeneratorTestApp.Helpers
{
    public class TournamentBracketHelper
    {
        private const int LOG_BASE = 2;

        /// <summary>
        /// Seeds teams in the tournament bracket.
        /// </summary>
        /// <param name="bracket">The tournament bracket.</param>
        /// <param name="teamCount">The number of teams to seed.</param>
        public static void SeedTeams(ITournamentBracket bracket, ILogger logger, int teamCount)
        {
            try
            {
                for (int i = 1; i <= teamCount; i++)
                {
                    bracket.SeedTeam($"Team{i}", $"Team{i}");
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Error in SeedTeams: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Simulates a tournament.
        /// </summary>
        /// <param name="teamRepository">The team repository.</param>
        /// <param name="bracket">The tournament bracket.</param>
        /// <param name="teamCount">The number of teams in the tournament.</param>
        public static void SimulateTournament(ITeamRepository teamRepository, ITournamentBracket bracket, ILogger logger, int teamCount)
        {
            try
            {
                var rounds = GetRounds(teamCount);
                foreach (var round in rounds)
                {
                    var previousRound = rounds.IndexOf(round) > 0 ? rounds[rounds.IndexOf(round) - 1] : null;
                    SimulateRound(teamRepository, bracket, round, logger, previousRound);
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Error in SimulateTournament: {ex.Message}", ex);
            }
        }

        private static void SimulateRound(ITeamRepository teamRepository, ITournamentBracket bracket, Round round, ILogger logger, Round? previousRound)
        {
            if (round == null)
            {
                throw new ArgumentNullException(nameof(round), "Round cannot be null.");
            }

            try
            {
                AddTeamsToRound(teamRepository, round, previousRound);

                AdvanceRound(bracket, round);

                AddWinnerTeamsToRound(bracket, round);


                // Clear the round winners dictionary
                bracket.ClearWinners();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error in SimulateRound: {ex.Message}", ex);
            }
        }

        private static void AddWinnerTeamsToRound(ITournamentBracket bracket, Round round)
        {
            var roundWinners = bracket.GetWinners();
            round.WinnerTeams.AddRange(roundWinners.Values);
            round.WinnerTeamCount = roundWinners.Count();
            bracket.AddRound(round);
        }

        private static void AddTeamsToRound(ITeamRepository teamRepository, Round round, Round? previousRound)
        {
            if (previousRound != null)
            {
                // Add the winning teams from the previous round to the current round.
                round.Teams.AddRange(previousRound.WinnerTeams);
            }
            else
            {
                // If it's the first round, add all teams from the repository.
                round.Teams.AddRange(teamRepository.GetAllTeams().Values);
            }
        }

        private static List<Round> GetRounds(int teamCount)
        {
            var roundList = new List<Round>();
            int rounds = (int)Math.Ceiling(Math.Log(teamCount, LOG_BASE));
            string[] roundNames = new string[rounds];
            teamCount = GetRoundName(teamCount, roundList, rounds, roundNames);
            return roundList;
        }

        private static int GetRoundName(int teamCount, List<Round> roundList, int rounds, string[] roundNames)
        {
            for (int i = 0; i < rounds; i++)
            {
                var round = new Round()
                {
                    TeamCount = teamCount
                };

                // Assign round names based on the number of teams.
                AssignRoundNames(teamCount, i, round, roundNames);
                
                teamCount /= 2;

                round.RoundId = i;
                round.Name = roundNames[i];
                roundList.Add(round);
            }

            return teamCount;
        }

        private static void AssignRoundNames(int teamCount, int index, Round round, string[] roundNames)
        {
            // Assign round names based on the number of teams.
            if (teamCount == 8)
            {
                roundNames[index] = "Quarter Finals";
            }
            else if (teamCount == 4)
            {
                roundNames[index] = "Semi Finals";
            }
            else if (teamCount == 2)
            {
                roundNames[index] = "Finals";
            }
            else
            {
                roundNames[index] = $"Round of {teamCount}";
            }
        }

        private static void AdvanceRound(ITournamentBracket bracket, Round round)
        {
            // Simulate advancing teams for the given round
            for (int i = 1; i <= round.TeamCount; i += 2)
            {
                bracket.AdvanceTeam($"Team{i}");
            }
        }
    }
}
