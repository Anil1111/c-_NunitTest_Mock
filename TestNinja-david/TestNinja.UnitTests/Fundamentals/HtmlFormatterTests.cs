using NUnit.Framework;
using TestNinja.Fundamentals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.Fundamentals.Tests
{
    [TestFixture]
    public class HtmlFormatterTests
    {
        [TestCase("aaa")]
        //[Ignore("To save time")]
        public void HtmlFormatter_InputIsCorrect_ReturnExpectedResult(string input)
        {
            // Arrange

            // Act
            var result = (new HtmlFormatter()).FormatAsBold(input);

            // Assert
            Assert.That(result.StartsWith("<strong>"), Is.True);
            Assert.That(result.EndsWith("</strong>"), Is.True);
            Assert.That(result.Contains(input), Is.True);

        }

    }


}