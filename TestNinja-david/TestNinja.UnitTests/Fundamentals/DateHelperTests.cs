using System;
using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.Fundamentals
{
    [TestFixture()]
    public class DateHelperTests
    {
        [Test()]
        public void FirstOfNextMonth_WhenMothIsNot12_ReturnNextMonth()
        {
            // Arrange
            var date = new DateTime(2018, 12, 1);

            // Action
            var result = DateHelper.FirstOfNextMonth(date);

            // Assert
            Assert.That(result.Month == 1);
            Assert.That(result.Year == 2018 + 1);
            Assert.That(result.Day == 1);
        }

        [Test()]
        public void FirstOfNextMonth_WhenMothIs12_ReturnNextYearFirstMonth()
        {
            // Arrange
            var date = new DateTime(2018, 1, 1);

            // Action
            var result = DateHelper.FirstOfNextMonth(date);

            // Assert
            Assert.That(result.Month == date.Month + 1);
            Assert.That(result.Year == 2018);
            Assert.That(result.Day == 1);
        }
    }
}