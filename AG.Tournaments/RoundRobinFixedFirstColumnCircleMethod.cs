using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using static System.Linq.Enumerable;

namespace AG.Tournaments
{
    /// <summary>
    /// Use case based on https://en.wikipedia.org/wiki/Round-robin_tournament#Circle_method
    /// </summary>
    public class RoundRobinFixedFirstColumnCircleMethod : RoundRobinBase
    {
        private const int FirstTeamIndex = 0;

        public RoundRobinFixedFirstColumnCircleMethod(IEnumerable<string> teams) : base(teams)
        {
        }

        public override void CalculateTeamIndexMatchesRounds()
        {
            if (TeamIndexMatchesRounds?.Count > 0) return;

            var teamIndexMatchesRounds = new List<IReadOnlyList<(int FirstTeamIndex, int SecondTeamIndex)>>(NumberOfRounds);

            for (var round = 0; round < NumberOfRounds; round++)
            {
                var teamIndexMatches = new List<(int FirstTeamIndex, int SecondTeamIndex)>(HalfTeamsCount);
                var teamIndexes = TeamIndexesLinkedList.ToList();

                for (var i = 0; i < HalfTeamsCount; i++)
                {
                    var firstIndex = teamIndexes[i];
                    var secondIndex = teamIndexes[NumberOfTeams - i - 1];
                    teamIndexMatches.Add((firstIndex, secondIndex));
                }

                teamIndexMatchesRounds.Add(teamIndexMatches);

                RotateList();
            }

            TeamIndexMatchesRounds = teamIndexMatchesRounds;
        }

        public override void CalculateTeamIndexRoundsTable()
        {
            if (TeamIndexRoundsTable?.Count > 0) return;

            var teamIndexRoundsTable = new List<(IReadOnlyList<int> FirstTeamIndexes, IReadOnlyList<int> SecondTeamIndexes)>(NumberOfRounds);

            for (var round = 0; round < NumberOfRounds; round++)
            {
                var teamIndexes = TeamIndexesLinkedList.ToList();
                var firstTeamIndexes = new List<int>(HalfTeamsCount);
                var secondTeamIndexes = new List<int>(HalfTeamsCount);

                for (var i = 0; i < HalfTeamsCount; i++)
                {
                    firstTeamIndexes.Add(teamIndexes[i]);
                    secondTeamIndexes.Add(teamIndexes[NumberOfTeams - i - 1]);
                }

                teamIndexRoundsTable.Add((firstTeamIndexes, secondTeamIndexes));

                RotateList();
            }

            TeamIndexRoundsTable = teamIndexRoundsTable;
        }

        private void RotateList()
        {
            TeamIndexesLinkedList.AddAfter(TeamIndexesLinkedList.Find(FirstTeamIndex), TeamIndexesLinkedList.Last());
            TeamIndexesLinkedList.RemoveLast();
        }
    }
}
