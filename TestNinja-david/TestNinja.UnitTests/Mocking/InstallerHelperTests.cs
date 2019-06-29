using System.Net;
using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture()]
    public class InstallerHelperTests
    {
        private InstallerHelper _helper;
        private Mock<IClient> _client;

        public InstallerHelperTests()
        {
            _client = new Mock<IClient>();
            _helper = new InstallerHelper(_client.Object);
        }

        [TestCase()]
        //[Ignore("To save time")]
        public void DownloadInstaller_WhenThrowException_ReturnFalse()
        {
            // Arrange
            _client.Setup(c => c.DownloadFile(It.IsAny<string>(), It.IsAny<string>())).Throws(new WebException());
            // Act
            var result = _helper.DownloadInstaller("customer", "installer");
            // Assert
            Assert.That(result, Is.False);
        }

        [TestCase()]
        //[Ignore("To save time")]
        public void DownloadInstaller_Completes_ReturnTrue()
        {
            // Arrange
           // Act
            var result = _helper.DownloadInstaller("http://local/", "p.txt");
            // Assert
            Assert.That(result, Is.True);
        }
    }
}