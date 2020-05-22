using System;
using System.Linq;
using AG.Extensions;

namespace AG.FizzBuzzLib
{
    /// <summary>
    /// The followings are the basic requirements:
    /// Write a program that prints the numbers from 1 to n and n max is 100
    /// But for multiples of three print "Fizz" instead of the number
    /// and for the multiples of five print "Buzz".
    /// For numbers that are multiples of both three and five print "FizzBuzz".
    /// </summary>
    public sealed class FizzBuzz
    {
        private static int MinNumber { get; } = 1;
        private static int MaxNumber { get; } = 100;
        private static string FizzText { get; }= "Fizz";
        private static string BuzzText { get; } = "Buzz";

        /// <summary>
        /// Print number, Fizz, Buzz, or FizzBuzz for each number separated by a space.
        /// e.g. PrintFizzBuzzNumbers(30)
        /// output: 1 2 Fizz 4 Buzz Fizz 7 8 Fizz Buzz 11 Fizz 13 14 FizzBuzz 16 17 Fizz 19 Buzz Fizz 22 23 Fizz Buzz 26 Fizz 28 29 FizzBuzz
        /// </summary>
        /// <param name="numberCount"></param>
        /// <returns>a string of number or word separated by a space.</returns>
        public string PrintFizzBuzzNumbers(int numberCount) =>
            string.Join(" ", Enumerable.Range(MinNumber, numberCount).Select(PrintFizzBuzz));

        public string PrintFizzBuzz(int number)
        {
            ThrowExceptionIfNumberIsNotWithinRange(number);

            string result = null;
            if (number % 3 == 0) result = FizzText;
            if (number % 5 == 0) result += BuzzText;

            return result ?? number.ToString();
        }

        private static void ThrowExceptionIfNumberIsNotWithinRange(int number)
        {
            if (number.IsBetween(MinNumber, MaxNumber)) return;

            throw new ArgumentException($"Entered number is {number}, entered number should be between {MinNumber} to {MaxNumber}");
        }
    }
}
