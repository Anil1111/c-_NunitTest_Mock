using System;
using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.Fundamentals
{
    [TestFixture()]
    public class ErrorLoggerTests
    {
        private ErrorLogger _errorLogger;

        [SetUp]
        public void Setup()
        {
            _errorLogger = new ErrorLogger();
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("       ")]
        public void Log_WhenErrIsNullOrWhiteSpace_ThrowException(string errStr)
        {
            // Arrange 
            // Action
            // Assert
            Assert.That(() => _errorLogger.Log(errStr),
                Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void Log_WhenCalled_SetTheLastErrorProperty()
        {
            // Arrange

            // Act
            _errorLogger.Log("abc");

            // Assert
            Assert.That(_errorLogger.LastError, Is.EqualTo("abc"));
        }

        [Test]
        public void Log_ValidError_RaiseErrorLoggedEvent()
        {
            // Arrange
            var id = Guid.Empty;

            // Act
            _errorLogger.ErrorLogged += (sender, args) => { id = args; };
            _errorLogger.Log("abc");
            
            // Assert
            Assert.That(id, Is.Not.EqualTo(Guid.Empty));

        }

    }
}