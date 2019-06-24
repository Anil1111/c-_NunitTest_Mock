using System;
using System.Collections.Generic;
using System.Linq;

namespace TestNinja.Mocking
{
    public static class BookingHelper
    {

        public static string OverlappingBookingsExist(Booking booking, IBookingRepository bookingRepository)
        {
            if (booking.Status == "Cancelled")
                return string.Empty;

            var bookings = bookingRepository.GetActiveBookings(booking.Id);

            var overlappingBooking =
                bookings.FirstOrDefault(
                    b =>
                        booking.ArrivalDate < b.DepartureDate
                        && booking.DepartureDate > b.ArrivalDate);

            return overlappingBooking == null ? string.Empty : overlappingBooking.Reference;
        }
    }

    public interface IBookingRepository
    {
        IQueryable<Booking> GetActiveBookings(int? excludeBookingId = null);
    }

    public class BookingRepository : IBookingRepository
    {
        public IQueryable<Booking> GetActiveBookings(int? excludeBookingId = null)
        {
            var unitOfWork = new UnitOfWork();
            var bookings = unitOfWork.Query<Booking>()
                .Where(b => b.Status != "Cancelled" && (excludeBookingId == null || excludeBookingId != b.Id));

            return bookings;
        }
    }

    public class UnitOfWork
    {
        public IQueryable<T> Query<T>()
        {
            return new List<T>().AsQueryable();
        }
    }

    public class Booking
    {
        public string Status { get; set; }
        public int Id { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public string Reference { get; set; }
    }
}