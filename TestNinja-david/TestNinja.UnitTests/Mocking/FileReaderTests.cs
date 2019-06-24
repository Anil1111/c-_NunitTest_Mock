using NUnit.Framework;
using TestNinja.Mocking;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace TestNinja.Mocking.Tests
{
    [TestFixture()]
    public class FileReaderTests
    {
        [Test()]
        public void FileReadTest()
        {

        }

        [TestCase]
        //[Ignore("To save time")]
        public void FileRead_WhenCalled_ReturnExpectedResult()
        {
            // Arrange
            var mock = new Mock<IFile>();
            var fileReader = new FileReader(mock.Object);
            mock.Setup(m => m.ReadAllText("abc.txt")).Returns("abc");
            // Act
            var result = fileReader.FileRead("abc.txt");
            // Assert
            Assert.That(result.Equals("abc"));
        }
    }
}