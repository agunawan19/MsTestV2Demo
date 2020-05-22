using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace AG.Extensions.Tests
{
    [TestClass]
    public class ExtensionTests
    {
        [TestCategory("IsBetween<int> Test")]
        [DataTestMethod]
        [DataRow(2, new[] { 1, 3 }, true, "#1")]
        [DataRow(1, new[] { 1, 3 }, true, "#2")]
        [DataRow(3, new[] { 1, 3 }, true, "#3")]
        [DataRow(0, new[] { 1, 3 }, false, "#4")]
        [DataRow(4, new[] { 1, 3 }, false, "#5")]
        public void IsBetweenTest_Inclusive_Returns_Correct_Result_For_Integer_Type(int numberToTest, int[] range,
            bool expected, string message)
        {
            // Arrange
            var (lowerBound, upperBound) = (range[0], range[1]);

            // Act
            var actual = numberToTest.IsBetween(lowerBound, upperBound);

            // Assert
            Assert.AreEqual(expected, actual, message);
        }

        [TestCategory("IsBetween<int> Test")]
        [DataTestMethod]
        [DataRow(2, new[] { 1, 3 }, true, "#1")]
        [DataRow(1, new[] { 1, 3 }, false, "#2")]
        [DataRow(3, new[] { 1, 3 }, false, "#3")]
        [DataRow(0, new[] { 1, 3 }, false, "#4")]
        [DataRow(4, new[] { 1, 3 }, false, "#5")]
        public void IsBetweenTest_NotInclusive_Returns_Correct_Result_For_Integer_Type(int numberToTest, int[] range,
            bool expected, string message)
        {
            // Arrange
            var (lowerBound, upperBound) = (range[0], range[1]);

            // Act
            var actual = numberToTest.IsBetween(lowerBound, upperBound, false);

            // Assert
            Assert.AreEqual(expected, actual, message);
        }

        [TestCategory("IsBetween<DateTime> Test")]
        [DataTestMethod]
        [DynamicData(nameof(BetweenDateTimeInclusiveTestData))]
        public void IsBetweenTest_Inclusive_Returns_Correct_Result_For_DateTime_Type(string dateString,
            (string Start, string End) dateRange, bool expected, string message)
        {
            // Arrange
            const string dateFormat = "MM/dd/yyyy";
            var (startDate, endDate) = dateRange;
            var date = DateTime.ParseExact(dateString, dateFormat, null);
            var lowerBound = DateTime.ParseExact(startDate, dateFormat, null);
            var upperBound = DateTime.ParseExact(endDate, dateFormat, null);

            // Act
            var actual = date.IsBetween(lowerBound, upperBound);

            // Assert
            Assert.AreEqual(expected, actual, message);
        }

        private static IEnumerable<object[]> BetweenDateTimeInclusiveTestData
        {
            get
            {
                var range = (Start: "01/01/2000", End: "01/31/2000");

                yield return new object[] { "01/10/2000", range, true, "#1. 01/10/2000 is between 01/01/2000 - 01/31/2000" };
                yield return new object[] { "01/01/2000", range, true, "#2. 01/01/2000 is between 01/01/2000 - 01/31/2000" };
                yield return new object[] { "01/31/2000", range, true, "#3. 01/31/2000 is between 01/01/2000 - 01/31/2000" };
                yield return new object[] { "12/31/1999", range, false, "#4. 12/31/1999 is not between 01/01/2000 - 01/31/2000" };
                yield return new object[] { "02/01/2000", range, false, "#5. 02/01/2000 is not between 01/01/2000 - 01/31/2000" };
            }
        }

        [TestCategory("IsBetween<DateTime> Test")]
        [DataTestMethod]
        [DynamicData(nameof(BetweenDateTimeExclusiveTestData))]
        public void IsBetweenTest_NotInclusive_Returns_Correct_Result_For_DateTime_Type(string dateString,
            (string Start, string End) timeRange, bool expected, string message)
        {
            // Arrange
            const string hourFormat = "HH:mm";
            var (startDate, endDate) = timeRange;
            var date = DateTime.ParseExact(dateString, hourFormat, null);
            var lowerBound = DateTime.ParseExact(startDate, hourFormat, null);
            var upperBound = DateTime.ParseExact(endDate, hourFormat, null);

            // Act
            var actual = date.IsBetween(lowerBound, upperBound, false);

            // Assert
            Assert.AreEqual(expected, actual, message);
        }

        private static IEnumerable<object[]> BetweenDateTimeExclusiveTestData
        {
            get
            {
                var range = (Start: "08:00", End: "17:00");

                yield return new object[] { "08:01", range, true, "#1. 08:01 is between 08:00 - 17:00" };
                yield return new object[] { "08:00", range, false, "#2. 08:00 is not between 08:00 - 17:00 (not inclusive)" };
                yield return new object[] { "17:00", range, false, "#3. 17:00 is not between 08:00 - 17:00 (not inclusive)" };
                yield return new object[] { "07:59", range, false, "#4. 07:59 is not between 08:00 - 17:00" };
                yield return new object[] { "17:01", range, false, "#5. 17:01 is not between 08:00 - 17:00" };
            }
        }
    }
}
