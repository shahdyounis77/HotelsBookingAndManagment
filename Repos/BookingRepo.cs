using Microsoft.EntityFrameworkCore;

namespace Hotel.Repos
{
    public class BookingRepo : Interfaces.IBooking
    {
        private readonly Models.Context context;

        public BookingRepo(Models.Context context)
        {
            this.context = context;
        }

        public void AddBooking(Dtos.AddBooking booking)
        {
            var newBooking = new Models.Booking
            {
                Id = booking.Id,
                HotelId = booking.HotelId,
                RoomId = booking.RoomId,
                UserId = booking.UserId,
                CheckInDate = booking.CheckInDate,
                CheckOutDate = booking.CheckOutDate,
                TotalPrice = booking.TotalPrice

            };

            context.Bookings.Add(newBooking);
            context.SaveChanges();
        }

        public void DeleteBooking(int id)
        {
            var booking = context.Bookings.Find(id);
            context.Bookings.Remove(booking);
            context.SaveChanges();
        }

        
        public IEnumerable<Models.Booking> GetAllBookings()
        {
            var bookings = context.Bookings.AsNoTracking().Include(y=>y.User).Include(b => b.Room).ThenInclude(r => r.Hotel);

            return bookings;
        }

        public Models.Booking GetBookingById(int id)
        {
            var booking = context.Bookings.AsNoTracking().FirstOrDefault(b => b.Id == id);
            return booking;

        }
    }
}
