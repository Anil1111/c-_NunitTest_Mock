using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class FizzBuzzTests
    {

        [SetUp]
        public void SetUp()
        {
        }


        [Test]
        [TestCase(15, "FizzBuzz")]
        [TestCase(3, "Fizz")]
        [TestCase(6, "Fizz")]
        [TestCase(5, "Buzz")]
        [TestCase(0, "FizzBuzz")]
        [TestCase(-1, "-1")]
        //[Ignore("Ignore a text")]
        public void GetOutput_InputNumberIsValid_ReturnDifferentSound(int inputNumber, string resultString)
        {
            var result = FizzBuzz.GetOutput(inputNumber);
            Assert.That(result, Does.Contain(resultString).IgnoreCase);

        }
    }
}
