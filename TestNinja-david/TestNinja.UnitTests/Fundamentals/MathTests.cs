using System.Linq;
using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.Fundamentals
{
    [TestFixture()]
    public class MathTests
    {
        private readonly Math _math;

        public MathTests()
        {
            _math = new Math();
        }
        [Test()]
        public void Add_WhenCalled_OK()
        {
            // Arrange
            // Action
            var result = _math.Add(2, 3);
            // Assert
            Assert.That(result == 2 + 3);
        }

        [TestCase(2, 2, 2)]
        [TestCase(2, 5, 5)]
        [TestCase(8, 3, 8)]
        public void Max_WhenCalled_OK(int a, int b, int expected)
        {
            // Arrange
            // Action
            var result = _math.Max(a, b);
            // Assert
            Assert.That(result == expected);
        }

        [Test()]
        public void GetOddNumbersTest()
        {
            // Arrange
            // Action
            var results = _math.GetOddNumbers(10);
            // Assert
            Assert.That(results.FirstOrDefault() == 1);
            Assert.That(results.ToArray()[1] == 3);
        }
    }
}