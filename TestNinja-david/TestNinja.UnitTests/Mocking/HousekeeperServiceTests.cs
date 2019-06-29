using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture()]
    public class HousekeeperServiceTests
    {
        private DateTime _date;
        private string _statementFileName;
        private Housekeeper _housekeeper;
        private HousekeeperService _service;

        private Mock<IStatementGenerator> _statementGenerator;
        private Mock<IEmailSender> _emailSender;
        private Mock<IXtraMessageBox> _messageBox;

        [SetUp]
        public void Setup()
        {
            _date = new DateTime(2000, 1, 1);
            _statementFileName = "hi_there";
            _housekeeper = new Housekeeper
                {Email = "a@a", FullName = "faa", Oid = 123, StatementEmailBody = "body"};

            _statementGenerator = new Mock<IStatementGenerator>();
            _statementGenerator.Setup(s => s.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _date))
                .Returns(() => _statementFileName); // lazy evaluation

            var list = new List<Housekeeper> {_housekeeper};
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(u => u.Query<Housekeeper>()).Returns(list.AsQueryable);

            _emailSender = new Mock<IEmailSender>();
            _messageBox = new Mock<IXtraMessageBox>();
            _service = new HousekeeperService(unitOfWork.Object,
                _statementGenerator.Object,
                _emailSender.Object,
                _messageBox.Object);
        }


        [TestCase()]
        public void HousekeeperService_WhenCalled_SaveStatementSendEmail()
        {
            // Arrange
            // Action
            _service.SendStatementEmails(_date);

            // Assert
            VerifyStatementGenerated();
            VerifyEmailSend();
            VerifyMessageBoxNotShow();
        }


        [TestCase(null)]
        [TestCase("")]
        [TestCase("       ")]
        public void HousekeeperService_EmailIsNullOrWhiteSpace_ShouldNotSaveStatement(string emailStr)
        {
            // Arrange
            _housekeeper.Email = emailStr;
            // Action
            _service.SendStatementEmails(_date);

            // Assert
            VerifyStatementNotGenerated();
            VerifyEmailNotSend();
            VerifyMessageBoxNotShow();
        }




        [TestCase(null)]
        [TestCase("")]
        [TestCase("       ")]
        public void HousekeeperService_StatementFilenameIsNullOrWhiteSpace_ShouldNotSendEmail(string str)
        {
            // Arrange
            _statementFileName = str;
            // Action
            _service.SendStatementEmails(_date);

            // Assert
            VerifyEmailNotSend();
            VerifyMessageBoxNotShow();
        }


        [TestCase()]
        public void HousekeeperService_WhenThrowException_MessageBoxWillShow()
        {
            // Arrange
            _emailSender.Setup(e => e.EmailFile(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>())).Throws(new Exception());

            // Act
            _service.SendStatementEmails(_date);

            // Assert
            VerifyMessageBoxShow();
        }

        private void VerifyMessageBoxShow()
        {
            _messageBox.Verify(s => s.Show(It.IsAny<string>(),
                It.IsAny<string>(),
                MessageBoxButtons.OK), Times.Once);
        }

        private void VerifyMessageBoxNotShow()
        {
            _messageBox.Verify(s => s.Show(It.IsAny<string>(),
                It.IsAny<string>(),
                MessageBoxButtons.OK), Times.Never);
        }


        private void VerifyStatementGenerated()
        {
            _statementGenerator.Verify(s => s.SaveStatement(_housekeeper.Oid,
                    _housekeeper.FullName,
                    _date),
                Times.Once);
        }

        private void VerifyStatementNotGenerated()
        {
            _statementGenerator.Verify(s => s.SaveStatement(
                    _housekeeper.Oid,
                    _housekeeper.FullName,
                    _date),
                Times.Never);
        }

        private void VerifyEmailNotSend()
        {
            _emailSender.Verify(s =>
                    s.EmailFile(_housekeeper.Email,
                        _housekeeper.StatementEmailBody,
                        _statementFileName,
                        It.IsAny<string>()),
                Times.Never);
        }

        private void VerifyEmailSend()
        {
            _emailSender.Verify(s =>
                s.EmailFile(_housekeeper.Email,
                    _housekeeper.StatementEmailBody,
                    _statementFileName,
                    It.IsAny<string>()));
        }
    }
}