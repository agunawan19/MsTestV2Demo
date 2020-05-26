using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using static System.Linq.Enumerable;

namespace AG.Tournaments.Tests
{
    [TestClass]
    public class RoundRobinBergerTableTests
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
03 01
04 02

Round: 3
02 01
04 03",
                GenerateScheduleRoundsTable(4)
            };

            yield return new object[]
            {
                @"Round: 1
01 02 03 04 05 06 07
14 13 12 11 10 09 08

Round: 2
08 09 10 11 12 13 01
14 07 06 05 04 03 02

Round: 3
02 03 04 05 06 07 08
14 01 13 12 11 10 09

Round: 4
09 10 11 12 13 01 02
14 08 07 06 05 04 03

Round: 5
03 04 05 06 07 08 09
14 02 01 13 12 11 10

Round: 6
10 11 12 13 01 02 03
14 09 08 07 06 05 04

Round: 7
04 05 06 07 08 09 10
14 03 02 01 13 12 11

Round: 8
11 12 13 01 02 03 04
14 10 09 08 07 06 05

Round: 9
05 06 07 08 09 10 11
14 04 03 02 01 13 12

Round: 10
12 13 01 02 03 04 05
14 11 10 09 08 07 06

Round: 11
06 07 08 09 10 11 12
14 05 04 03 02 01 13

Round: 12
13 01 02 03 04 05 06
14 12 11 10 09 08 07

Round: 13
07 08 09 10 11 12 13
14 06 05 04 03 02 01",
                GenerateScheduleRoundsTable(4)
            };
        }

        private static string GenerateScheduleRoundsTable(int numberOfTeams)
        {
            var teams = Range(1, numberOfTeams).Select(n => $"{n:00}").ToList();
            return new RoundRobinBergerTable(teams).GenerateScheduleRoundsTable();
        }
    }
}
