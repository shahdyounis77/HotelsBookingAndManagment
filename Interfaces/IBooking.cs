using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Interfaces
{
    public interface IBooking
    {
        void AddBooking(Dtos.AddBooking booking);

        void DeleteBooking(int id);
         IEnumerable<Models.Booking> GetAllBookings();
        Models.Booking GetBookingById(int id);
    }
}
