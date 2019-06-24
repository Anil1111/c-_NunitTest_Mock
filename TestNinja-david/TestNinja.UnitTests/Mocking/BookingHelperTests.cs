using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestNinja.Mocking.Tests
{
    [TestFixture()]
    public class BookingHelperTests
    {


        [TestCase]
        //[Ignore("To save time")]
        public void OverlappingBookingsExist_BookingIsCancelled_ReturnEmptyString()
        {
            // Arrange
            Booking booking = new Booking() { Status = "Cancelled" };
            // Act
            var result = BookingHelper.OverlappingBookingsExist(booking, null);
            // Assert
            Assert.That(result.Equals(String.Empty));
        }

        [TestCase(-15, -1, false)]  // After		
        [TestCase(-15, 0, false)]	// Start Touching
        [TestCase(-15, 1, true)]     	// Start Inside
        [TestCase(0, 15, true)]     	// Inside Start Touching
        [TestCase(0, 5, true)]		// Enclosing Start Touching
        [TestCase(1, 5, true)]     	// Enclosing	
        [TestCase(0, 10, true)]    	// Exact Match
        [TestCase(-1, 11, true)]     	// Inside
        [TestCase(-1, 10, true)]     	// Inside End Touching
        [TestCase(9, 15, true)]     	// End Inside
        [TestCase(10, 11, false)]     	// End Touching
        [TestCase(11, 15, false)]     	// Before
        //[Ignore("To save time")]
        public void OverlappingBookingsExist_WhenCalled_ReturnExpectedResult(int arriveDiff, int departureDiff, bool expectedResult)
        {
            // Arrange
            DateTime.TryParse("2019-02-23 14:00:00", out DateTime arriveDate);

            var mock = new Mock<IBookingRepository>();
            var list = new List<Booking>
            {
                new Booking{ Id = 101,Reference = "Start at January 23", Status = "Normal", ArrivalDate = arriveDate, DepartureDate = arriveDate.AddDays(10) },
            };
            mock.Setup(foo => foo.GetActiveBookings(It.IsAny<int>())).Returns(list.ToList().AsQueryable);

            // Act
            var result = BookingHelper.OverlappingBookingsExist(new Booking { Id = 201, Reference = "rrrrrr", Status = "Normal", ArrivalDate = arriveDate.AddDays(arriveDiff), DepartureDate = arriveDate.AddDays(departureDiff) }, mock.Object);
            // Assert
            Console.WriteLine(result);
            Assert.That(!result.Equals(String.Empty) == expectedResult);
        }
    }
}