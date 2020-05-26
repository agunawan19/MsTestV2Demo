using System;
using System.Collections.Generic;
using System.Linq;
using AG.FizzBuzzLib;
using AG.Tournaments;

namespace FizzBuzzApp
{
    class Program
    {
        private const string LineSeparator = "----------------------------";

        static void Main()
        {
            PrintFizzBuzz();

            Console.WriteLine(LineSeparator);

            PrintRoundRobinSchedule();
        }

        private static void PrintFizzBuzz()
        {
            var fizzBuzz = new FizzBuzz();

            Console.WriteLine(fizzBuzz.PrintFizzBuzz(30));
            Console.WriteLine(fizzBuzz.PrintFizzBuzzNumbers(10));
            Console.WriteLine(fizzBuzz.PrintFizzBuzzNumbers(15));

        }

        private static void PrintRoundRobinSchedule()
        {
            const int numberOfTeams = 14;
            var teams = Enumerable.Range(1, numberOfTeams).Select(n => $"{n:00}").ToList();
            IRoundRobin roundRobin = new RoundRobinFixedFirstColumnCircleMethod(teams);

            Console.WriteLine(roundRobin.GenerateSchedule());
            Console.WriteLine(LineSeparator);
            Console.WriteLine(roundRobin.GenerateScheduleRoundsTable());
        }
    }
}
