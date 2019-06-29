using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.Fundamentals
{
    [TestFixture()]
    public class ReservationTests
    {
        private readonly User _user;
        private readonly User _madeby;
        private readonly Reservation _reservation;


        public ReservationTests()
        {
            _user = new User();
            _madeby = new User();
            _reservation = new Reservation(_madeby);
        }




        [Test()]
        public void CanBeCancelledByTest_UserIsMadeby_ReturnTrue()
        {
            // Arrange
            // Action
            var result = _reservation.CanBeCancelledBy(_madeby);

            // Assert
            Assert.That(result, Is.True);
        }


        [Test()]
        public void CanBeCancelledByTest_UserIsAdmin_ReturnTrue()
        {
            // Arrange
            _user.IsAdmin = true;
            // Action
            var result = _reservation.CanBeCancelledBy(_user);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test()]
        public void CanBeCancelledByTest_UserIsNotMadebyNotAdmin_ReturnFalse()
        {
            // Arrange
            _user.IsAdmin = false;
            // Action
            var result = _reservation.CanBeCancelledBy(_user);

            // Assert
            Assert.That(result, Is.False);
        }

    }
}