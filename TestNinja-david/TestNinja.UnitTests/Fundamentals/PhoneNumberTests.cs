using System;
using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.Fundamentals
{
    [TestFixture()]
    public class PhoneNumberTests
    {
        private string _phoneNumberString;
        private PhoneNumber _phoneNumber;

  
        [Test()]
        public void Parse_PhoneNumberStringIsNull_ThrowException()
        {
            // Arrange
            // Action
            // Assert
            Assert.That(() => PhoneNumber.Parse(null),
                Throws
                    .TypeOf<ArgumentException>()
                    .With.Property("Message")
                    .EqualTo("Phone number cannot be blank."));
        }

        [Test()]
        public void Parse_PhoneNumberStringIsWhiteSpace_ThrowException()
        {
            // Arrange
            var _phoneNumberString = "                      ";
            // Action
            // Assert
            Assert.That(() => PhoneNumber.Parse(_phoneNumberString),
                Throws
                    .TypeOf<ArgumentException>()
                    .With.Property("Message")
                    .EqualTo("Phone number cannot be blank."));
        }

        [Test()]
        public void Parse_PhoneNumberStringLengthIsNot10_ThrowException()
        {
            // Arrange
            var _phoneNumberString = "123";
            // Action
            // Assert
            Assert.That(() => PhoneNumber.Parse(_phoneNumberString),
                Throws
                    .TypeOf<ArgumentException>()
                    .With.Property("Message")
                    .EqualTo("Phone number should be 10 digits long."));
        }

        [Test()]
        public void Parse_PhoneNumberStringIsCorrect_ReturnPhoneNumber()
        {
            // Arrange
            _phoneNumberString = "1234567890";
            // Action
            _phoneNumber = PhoneNumber.Parse(_phoneNumberString);
            // Assert
            Assert.That(_phoneNumber.Area, Is.Not.NaN);
            Assert.That(_phoneNumber.Major, Is.Not.NaN);
            Assert.That(_phoneNumber.Minor, Is.Not.NaN);
        }


        [Test()]
        public void ToString_WhenCalled_ReturnPhoneNumberString()
        {
            // Arrange
            // Action
            var result = _phoneNumber.ToString();
            // Assert
            Assert.That(result.Contains("-"));
            Assert.That(result.EndsWith(_phoneNumberString.Substring(6)));
        }
    }
}