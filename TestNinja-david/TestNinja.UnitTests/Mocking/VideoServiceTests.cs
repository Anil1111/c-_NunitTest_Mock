using System.Collections.Generic;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture()]
    public class VideoServiceTests
    {
        private VideoService _service;
        private Mock<IFileReader> _mock;
        private Mock<IVideoRepository> _mockVideosRep;

        public VideoServiceTests()
        {
            _mock = new Mock<IFileReader>();
            _mockVideosRep = new Mock<IVideoRepository>();
            _service = new VideoService(_mock.Object, _mockVideosRep.Object);
        }

        [TestCase("empty", "")]
        //[Ignore("to save resources")]
        public void ReadVideoTitle_VideoIsNull_ReturnExpectedResult(string path, string title)
        {
            // Arrange
            var str = JsonConvert.SerializeObject(null);
            _mock.Setup(m => m.FileRead(path)).Returns(str);
            // Act
            var result = _service.ReadVideoTitle(path);
            // Assert
            Assert.That(result.Contains("Error"));
        }

        [TestCase("1.txt")]
        //[Ignore("To save time")]
        public void ReadVideoTitle_WhenCalled_ReturnExpectedResult(string path, string title = "the lion king")
        {
            // Arrange
            var str = JsonConvert.SerializeObject(new Video() { Title = title });
            _mock.Setup(m => m.FileRead(path)).Returns(str);
            // Act
            var result = _service.ReadVideoTitle(path);
            // Assert
            Assert.That(result.Contains(title));
        }

        [TestCase()]
        //[Ignore("To save time")]
        public void GetUnprocessedVideosAsCsv_RepositoryIsNotEmpty_ReturnExpectedResult(int id = 2)
        {
            // Arrange
            var list = new List<Video>
            {
                new Video {Id = 111, Title = "Fake King", IsProcessed = true,},
                new Video {Id = id, Title = "Real Queen", IsProcessed = false,},
                new Video {Id = 3, Title = "Real Queen", IsProcessed = false,},
            };
            _mockVideosRep.Setup(m => m.Videos()).Returns(list);
            // Act
            var result = _service.GetUnprocessedVideosAsCsv();
            // Assert
            Assert.That(result.Contains("111"), Is.False);
            Assert.That(result.Contains(id.ToString()));
            Assert.That(result.Contains("3"));
        }


        [TestCase()]
        //[Ignore("To save time")]
        public void GetUnprocessedVideosAsCsv_RepositoryIsEmpty_ReturnExpectedResult()
        {
            // Arrange
            // Act
            var result = _service.GetUnprocessedVideosAsCsv();
            // Assert
            Assert.That(result == "");
        }

    }
}