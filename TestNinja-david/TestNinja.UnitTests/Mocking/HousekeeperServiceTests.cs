using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestNinja.Mocking.Tests
{
    [TestFixture()]
    public class HousekeeperServiceTests
    {
        private HousekeeperService _service;
        private DateTime _date;
        private Mock<IStatementGenerator> _statementGenerator;
        private Housekeeper _housekeeper;
        private Mock<IEmailSender> _emailSender;
        private Mock<IXtraMessageBox> _messageBox;
        private string _statementFileName;

        [OneTimeSetUp]
        public void Setup()
        {
            _date = new DateTime(2000, 1, 1);
            _statementFileName = "hi_there";
            _housekeeper = new Housekeeper
                {Email = "a@a", FullName = "faa", Oid = 123, StatementEmailBody = "body"};
            var list = new List<Housekeeper> {_housekeeper};

            var unitOfWork = new Mock<IUnitOfWork>();
            _statementGenerator = new Mock<IStatementGenerator>();
            _emailSender = new Mock<IEmailSender>();
            _messageBox = new Mock<IXtraMessageBox>();
            unitOfWork.Setup(u => u.Query<Housekeeper>()).Returns(list.AsQueryable);
            _statementGenerator.Setup(s => s.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _date))
                .Returns(() => _statementFileName);
            _service = new HousekeeperService(unitOfWork.Object, _statementGenerator.Object, _emailSender.Object,
                _messageBox.Object);
        }


        [TestCase()]
        public void HousekeeperService_WhenCalled_SaveStatement()
        {
            // Arrange
            // Action
            _service.SendStatementEmails(_date);

            // Assert
            _statementGenerator.Verify(s => s.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _date));
        }


        [TestCase()]
        public void HousekeeperService_EmailIsNull_ShouldNotSaveStatement()
        {
            // Arrange
            var str = _housekeeper.Email;
            _housekeeper.Email = null;
            // Action
            _service.SendStatementEmails(_date);
            _housekeeper.Email = str;

            // Assert
            _statementGenerator.Verify(s => s.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _date),
                Times.Never);
        }

        [TestCase()]
        public void HousekeeperService_EmailIsWhiteSpace_ShouldNotSaveStatement()
        {
            // Arrange
            var str = _housekeeper.Email;
            _housekeeper.Email = "                        ";
            ;
            // Action
            _service.SendStatementEmails(_date);

            _housekeeper.Email = str;

            // Assert
            _statementGenerator.Verify(s => s.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _date),
                Times.Never);
        }

        [TestCase()]
        public void HousekeeperService_StatementFilenameIsNull_ShouldNotSendEmail()
        {
            // Arrange
            string str = _statementFileName;
            _statementFileName = null;
            // Action
            _service.SendStatementEmails(_date);
            _statementFileName = str;

            // Assert
            _emailSender.Verify(s =>
                    s.EmailFile(_housekeeper.Email, _housekeeper.StatementEmailBody, _statementFileName, It.IsAny<string>()),
                Times.Never);
        }

        [TestCase()]
        public void HousekeeperService_WhenCalled_SendEmail()
        {
            // Arrange
            // Action
            _service.SendStatementEmails(_date);

            // Assert
            _emailSender.Verify(s =>
                s.EmailFile(_housekeeper.Email, _housekeeper.StatementEmailBody, _statementFileName, It.IsAny<string>()));
        }

        [TestCase()]
        public void HousekeeperService_ThrowException_MessageBoxWillShow()
        {
            // Arrange
            _emailSender.Setup(e => e.EmailFile(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>())).Throws(new Exception());

            // Act
            _service.SendStatementEmails(_date);

            // Assert
            // _messageBox.Show(e.Message, $"Email failure: {emailAddress}",
            // MessageBoxButtons.OK)

            _messageBox.Verify(s => s.Show(It.IsAny<string>(), It.IsAny<string>(), MessageBoxButtons.OK));
        }
    }
}