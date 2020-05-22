using System;
using AG.FizzBuzzLib;

namespace FizzBuzzApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var fizzBuzz = new FizzBuzz();

            Console.WriteLine(fizzBuzz.PrintFizzBuzz(30));
            Console.WriteLine(fizzBuzz.PrintFizzBuzzNumbers(10));
            Console.WriteLine(fizzBuzz.PrintFizzBuzzNumbers(15));
        }
    }
}
