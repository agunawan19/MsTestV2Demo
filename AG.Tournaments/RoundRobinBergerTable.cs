using System.Collections.Generic;
using System.Linq;

namespace AG.Tournaments
{
    public class RoundRobinBergerTable : RoundRobinBase
    {
        private const int FirstTeamIndex = 0;

        public RoundRobinBergerTable(IEnumerable<string> teams) : base(teams)
        {
        }

        public override void CalculateTeamIndexMatchesRounds()
        {
            throw new System.NotImplementedException();
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
