using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using static System.Linq.Enumerable;

namespace AG.Tournaments.Tests
{
    [TestClass]
    public class RoundRobinFixedFirstColumnCircleMethodTests
    {
        [DataTestMethod]
        [DynamicData(nameof(GetRoundRobinMatchesTableData), DynamicDataSourceType.Method)]
        public void GenerateScheduleRoundsTable_Returns_Correct_Result(string expected, string actual) =>
            Assert.AreEqual(expected, actual);

        private static IEnumerable<object[]> GetRoundRobinMatchesTableData()
        {
            yield return new object[]
            {
                string.Empty,
                GenerateScheduleRoundsTable(0)
            };

            yield return new object[]
            {
                string.Empty,
                GenerateScheduleRoundsTable(1)
            };

            yield return new object[]
            {
                @"Round: 1
01
02",
                GenerateScheduleRoundsTable(2)
            };

            yield return new object[]
            {
                @"Round: 1
01 02
04 03

Round: 2
01 04
03 02

Round: 3
01 03
02 04",
                GenerateScheduleRoundsTable(4)
            };

            yield return new object[]
            {
                @"Round: 1
01 02 03 04 05 06 07
14 13 12 11 10 09 08

Round: 2
01 14 02 03 04 05 06
13 12 11 10 09 08 07

Round: 3
01 13 14 02 03 04 05
12 11 10 09 08 07 06

Round: 4
01 12 13 14 02 03 04
11 10 09 08 07 06 05

Round: 5
01 11 12 13 14 02 03
10 09 08 07 06 05 04

Round: 6
01 10 11 12 13 14 02
09 08 07 06 05 04 03

Round: 7
01 09 10 11 12 13 14
08 07 06 05 04 03 02

Round: 8
01 08 09 10 11 12 13
07 06 05 04 03 02 14

Round: 9
01 07 08 09 10 11 12
06 05 04 03 02 14 13

Round: 10
01 06 07 08 09 10 11
05 04 03 02 14 13 12

Round: 11
01 05 06 07 08 09 10
04 03 02 14 13 12 11

Round: 12
01 04 05 06 07 08 09
03 02 14 13 12 11 10

Round: 13
01 03 04 05 06 07 08
02 14 13 12 11 10 09",
                GenerateScheduleRoundsTable(14)
            };
        }

        private static string GenerateScheduleRoundsTable(int numberOfTeams)
        {
            var teams =  Range(1, numberOfTeams).Select(n => $"{n:00}").ToList();
            return new RoundRobinFixedFirstColumnCircleMethod(teams).GenerateScheduleRoundsTable();
        }
    }
}
