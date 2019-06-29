using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture()]
    public class FileReaderTests
    {
        private Mock<IFile> _file;
        private FileReader _fileReader;

        public FileReaderTests()
        {
            _file = new Mock<IFile>();
            _fileReader = new FileReader(_file.Object);
        }

        [TestCase]
        //[Ignore("To save time")]
        public void FileRead_WhenParameterIsNotCorrect_ThrowsException()
        {
            // Arrange
            // Act
            // Assert
            Assert.That(() => _fileReader.FileRead(""), Throws.ArgumentException);
        }

        [TestCase]
        //[Ignore("To save time")]
        public void FileRead_WhenCalled_ReturnExpectedResult()
        {
            // Arrange
            _file.Setup(m => m.ReadAllText("abc.txt")).Returns("abc");
            // Act
            var result = _fileReader.FileRead("abc.txt");
            // Assert
            Assert.That(result.Equals("abc"));
        }
    }
}