using System.Collections.Generic;

namespace AG.Tournaments
{
    public interface IRoundRobin
    {
        IList<string> Teams { get; set; }
        IReadOnlyList<IReadOnlyList<(int FirstTeamIndex, int SecondTeamIndex)>> TeamIndexMatchesRounds { get; set; }
        IReadOnlyList<(IReadOnlyList<int> firstTeamIndexes, IReadOnlyList<int> secondTeamIndexes)> TeamIndexRoundsTable { get; set; }
        void CalculateTeamIndexMatchesRounds();
        void CalculateTeamIndexRoundsTable();
        string GenerateSchedule();
        string GenerateScheduleRoundsTable();
        string GenerateScheduleRoundsTable(IEnumerable<string> teams);
    }
}
