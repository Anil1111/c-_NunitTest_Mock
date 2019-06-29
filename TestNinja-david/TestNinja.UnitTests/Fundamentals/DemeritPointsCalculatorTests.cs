using System;
using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.Fundamentals
{
    [TestFixture]
    public class DemeritPointsCalculatorTests
    {
        private readonly DemeritPointsCalculator _calculator;

        public DemeritPointsCalculatorTests()
        {
            _calculator = new DemeritPointsCalculator();
        }

        [TestCase(-1)]
        [TestCase(301)]
        //[Ignore("To save time")]
        public void MCalculateDemeritPoints_InputIsNotCorrect_ThrowException(int speed)
        {
            // Arrange
            // Act
            // Assert
            Assert.That(() => _calculator.CalculateDemeritPoints(speed),
                Throws
                    .TypeOf<ArgumentOutOfRangeException>()
                    .With.Property("Message")
                    .EqualTo("Specified argument was out of the range of valid values."));

        }

        [TestCase(64, 0)]
        [TestCase(65, 0)]
        [TestCase(69, 0)]
        [TestCase(70, 1)]
        [TestCase(71, 1)]
        [TestCase(75, 2)]
        [TestCase(76, 2)]
        //[Ignore("To save time")]
        public void MCalculateDemeritPoints_WhenCalled_ReturnExpectedResult(int inputSpeed, int expectedResult)
        {
            // Arrange

            // Act
            var result = _calculator.CalculateDemeritPoints(inputSpeed);
            // Assert
            Assert.That(result == expectedResult, Is.True);

        }

    }
}