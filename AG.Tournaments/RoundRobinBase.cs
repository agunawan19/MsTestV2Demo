using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Linq.Enumerable;

namespace AG.Tournaments
{
    public abstract class RoundRobinBase : IRoundRobin
    {
        public IList<string> Teams { get; set; }
        public IReadOnlyList<IReadOnlyList<(int FirstTeamIndex, int SecondTeamIndex)>> TeamIndexMatchesRounds { get; set; }
        public IReadOnlyList<(IReadOnlyList<int> firstTeamIndexes, IReadOnlyList<int> secondTeamIndexes)> TeamIndexRoundsTable { get; set; }
        protected int NumberOfTeams { get; }
        protected int NumberOfRounds { get; }
        protected int HalfTeamsCount { get; }
        protected LinkedList<int> TeamIndexesLinkedList { get;}

        protected RoundRobinBase(IEnumerable<string> teams)
        {
            Teams = teams.ToList();
            NumberOfTeams = Teams.Count;
            NumberOfRounds = NumberOfTeams - 1;
            HalfTeamsCount = NumberOfTeams / 2;
            TeamIndexesLinkedList = new LinkedList<int>(Range(0, NumberOfTeams).Select(n => n));
            TeamIndexMatchesRounds = new List<IReadOnlyList<(int FirstTeamIndex, int SecondTeamIndex)>>();
            TeamIndexRoundsTable = new List<(IReadOnlyList<int> FirstTeamIndexes, IReadOnlyList<int> SecondTeamIndexes)>();
        }

        public abstract void CalculateTeamIndexMatchesRounds();
        public abstract void CalculateTeamIndexRoundsTable();

        public virtual string GenerateSchedule()
        {
            if (NumberOfTeams < 2) return string.Empty;
            if ((TeamIndexMatchesRounds?.Count ?? 0) == 0) CalculateTeamIndexMatchesRounds();

            var roundCount = 0;
            var text = new StringBuilder();

            foreach (var round in TeamIndexMatchesRounds)
            {
                text.Append($"Round: {++roundCount}").AppendLine();

                foreach (var (firstTeamIndex, secondTeamIndex) in round)
                {
                    text.Append($"{Teams[firstTeamIndex]} - {Teams[secondTeamIndex]}").AppendLine();
                }

                text.AppendLine();
            }

            return text.ToString().TrimEnd();
        }

        public virtual string GenerateScheduleRoundsTable()
        {
            if (NumberOfTeams < 2) return string.Empty;
            if ((TeamIndexRoundsTable?.Count ?? 0) == 0) CalculateTeamIndexRoundsTable();

            const string separator = " ";
            var roundCount = 0;
            var text = new StringBuilder();

            foreach (var (firstTeamIndexes, secondTeamIndexes) in TeamIndexRoundsTable)
            {
                text.Append($"Round: {++roundCount}").AppendLine();

                var firstRow = new StringBuilder();
                var secondRow = new StringBuilder();
                var results = firstTeamIndexes.Zip(secondTeamIndexes,
                        (firstTeamIndex, secondTeamIndex) =>
                            (FirstTeam: Teams[firstTeamIndex], SecondTeam: Teams[secondTeamIndex]))
                    .ToList();

                foreach (var (firstTeamIndex, secondTeamIndex) in results)
                {
                    firstRow.Append(firstTeamIndex).Append(separator);
                    secondRow.Append(secondTeamIndex).Append(separator);
                }

                text.Append(firstRow.ToString().TrimEnd()).AppendLine()
                    .Append(secondRow.ToString().TrimEnd()).AppendLine().AppendLine();
            }

            return text.ToString().Trim();
        }

        public string GenerateScheduleRoundsTable(IEnumerable<string> teams)
        {
            Teams = teams.ToList();
            return GenerateScheduleRoundsTable();
        }
    }
}
