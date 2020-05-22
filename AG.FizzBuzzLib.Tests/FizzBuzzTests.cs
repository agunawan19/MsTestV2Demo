using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.CodeDom;
using System.Collections.Generic;

namespace AG.FizzBuzzLib.Tests
{
    [TestClass]
    public class FizzBuzzTests
    {
        private static readonly FizzBuzz FizzBuzz = new FizzBuzz();

        #region MsTest v1

        [TestCategory("PrintFizzBuzz Test")]
        [TestMethod]
        public void PrintFizzBuzz_Returns_ExpectedResult_Method1()
        {
            // Arrange
            var expected1 = "1";
            var expected2 = "Fizz";
            var expected3 = "Buzz";
            var expected4 = "FizzBuzz";
            var expected5 = "22";
            var expected6 = "FizzBuzz";

            // Act
            var actual1 = FizzBuzz.PrintFizzBuzz(1);
            var actual2 = FizzBuzz.PrintFizzBuzz(3);
            var actual3 = FizzBuzz.PrintFizzBuzz(5);
            var actual4 = FizzBuzz.PrintFizzBuzz(15);
            var actual5 = FizzBuzz.PrintFizzBuzz(22);
            var actual6 = FizzBuzz.PrintFizzBuzz(30);

            // Assert
            Assert.AreEqual(expected1, actual1);
            Assert.AreEqual(expected2, actual2);
            Assert.AreEqual(expected3, actual3);
            Assert.AreEqual(expected4, actual4);
            Assert.AreEqual(expected5, actual5);
            Assert.AreEqual(expected6, actual6);
        }

        [TestCategory("PrintFizzBuzz Test")]
        [TestMethod]
        public void PrintFizzBuzz_Returns_ExpectedResult_Method2()
        {
            // Arrange
            var expectedResults = new[]
            {
                "1",
                "Fizz",
                "Buzz",
                "FizzBuzz",
                "22"
            };

            // Act
            var actualResults = new[]
            {
                FizzBuzz.PrintFizzBuzz(1),
                FizzBuzz.PrintFizzBuzz(3),
                FizzBuzz.PrintFizzBuzz(5),
                FizzBuzz.PrintFizzBuzz(15),
                FizzBuzz.PrintFizzBuzz(22)
            };

            // Assert
            CollectionAssert.AreEqual(expectedResults, actualResults);
        }

        #endregion

        #region MsTest v2

        [TestCategory("PrintFizzBuzz Test MsTest V2")]
        [DataTestMethod]
        [DataRow(1, "1")]
        [DataRow(3, "Fizz")]
        [DataRow(5, "Buzz")]
        [DataRow(15, "FizzBuzz")]
        [DataRow(22, "22")]
        [DataRow(30, "FizzBuzz")]
        public void PrintFizzBuzz_Returns_ExpectedResult_Using_DataRow_1(int number, string expected) =>
            Assert.AreEqual(expected, FizzBuzz.PrintFizzBuzz(number));

        [TestCategory("PrintFizzBuzz Test MsTest V2")]
        [DataTestMethod]
        [DataRow("#1 1 Should Return 1", 1, "1")]
        [DataRow("#2 3 Should Return Fizz", 3, "Fizz")]
        [DataRow("#3 5 Should Return Buzz", 5, "Buzz")]
        [DataRow("#4 15 Should Return FizzBuzz", 15, "FizzBuzz")]
        [DataRow("#5 22 Should Return 22", 22, "22")]
        [DataRow("#6 30 Should Return FizzBuzz", 30, "FizzBuzz")]
        public void PrintFizzBuzz_Return_ExpectedResult_Using_DataRow_2(string message, int number, string expected)
        {
            // Act
            var actual = FizzBuzz.PrintFizzBuzz(number);

            // Assert
            Assert.AreEqual(expected, actual, message);
        }

        [TestCategory("PrintFizzBuzz Test MsTest V2")]
        [DataTestMethod]
        [DynamicData(nameof(GetFizzBuzzTestData), DynamicDataSourceType.Method)]
        public void PrintFizzBuzz_Returns_ExpectedResult_Using_DynamicData_Method(
             int number, string expected, string testNumber)
        {
            // Arrange
            const string message = "{0} Should Return {1}";

            // Act
            var actual = FizzBuzz.PrintFizzBuzz(number);

            // Assert
            Assert.AreEqual(expected, actual, $"{testNumber}. {message}", number, expected);
        }

        private static IEnumerable<object[]> GetFizzBuzzTestData()
        {
            yield return new object[] { 1, "1", "#1" };
            yield return new object[] { 3, "Fizz", "#2" };
            yield return new object[] { 5, "Buzz", "#3" };
            yield return new object[] { 15, "FizzBuzz", "#4" };
            yield return new object[] { 22, "22", "#5" };
            yield return new object[] { 30, "FizzBuzz", "#6" };
        }

        [TestCategory("PrintFizzBuzz Test MsTest V2")]
        [DataTestMethod]
        [DynamicData(nameof(FizzBuzzTestData))]
        public void PrintFizzBuzz_Returns_ExpectedResult_Using_DynamicData_Property(
            int number, string expected, string testNumber)
        {
            // Arrange
            const string message = "{0} Should Return {1}";

            // Act
            var actual = FizzBuzz.PrintFizzBuzz(number);

            // Assert
            Assert.AreEqual(expected, actual, $"{testNumber}. {message}", number, expected);
        }

        private static IEnumerable<object[]> FizzBuzzTestData
        {
            get
            {
                yield return new object[] { 1, "1", "#1" };
                yield return new object[] { 3, "Fizz", "#2" };
                yield return new object[] { 5, "Buzz", "#3" };
                yield return new object[] { 15, "FizzBuzz", "#4" };
                yield return new object[] { 22, "22", "#5" };
                yield return new object[] { 30, "FizzBuzz", "#6" };
            }
        }

        [TestCategory("PrintFizzBuzz Exception Test MsTest V2")]
        [DataTestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        [DataRow(101)]
        public void Can_Throw_Argument_Exception_When_Supplied_Number_Does_Not_Meet_Rule(int numberCount) =>
            Assert.ThrowsException<ArgumentException>(() => FizzBuzz.PrintFizzBuzz(numberCount));

        [TestCategory("PrintFizzBuzzNumber Test MsTest V2")]
        [DataTestMethod]
        [DataRow(10, @"1 2 Fizz 4 Buzz Fizz 7 8 Fizz Buzz")]
        [DataRow(15, @"1 2 Fizz 4 Buzz Fizz 7 8 Fizz Buzz 11 Fizz 13 14 FizzBuzz")]
        [DataRow(30, @"1 2 Fizz 4 Buzz Fizz 7 8 Fizz Buzz 11 Fizz 13 14 FizzBuzz 16 17 Fizz 19 Buzz Fizz 22 23 Fizz Buzz 26 Fizz 28 29 FizzBuzz")]
        public void PrintFizzBuzzNumbers_Returns_Correct_Result(int numberCount, string expected)
        {
            // Act
            var actual = FizzBuzz.PrintFizzBuzzNumbers(numberCount);

            // Assert
            Assert.AreEqual(expected, actual);
        }
        #endregion
    }
}
