using Hotel.Helper;
using System.Diagnostics.Contracts;

namespace Hotel.Services
{
    public class BookingServices
    {
        private readonly Interfaces.IBooking booking;
        private readonly Interfaces.IRoom _room;
        public BookingServices(Interfaces.IBooking booking, Interfaces.IRoom _room)
        {
           this.booking = booking;
            this._room = _room;
        }


        public void BookingRoom(int id, string userid, Dtos.DateBooking dateBooking)
        {

            var room = _room.GetRoomById(id);
            var days = CalculateNumberOfDays.GetNumberOfDays(dateBooking.CheckInDate, dateBooking.CheckOutDate);
            var totalPrice = CalculateBookingTotalPrice.GetTotalPrice(room.PricePerNight, days);
            var result = CheckDurationOfBookingDate.IsVaildBookingDate(dateBooking.CheckInDate, dateBooking.CheckOutDate);
            


            var newbooking = new Dtos.AddBooking
            {
                RoomId = room.Id,
                HotelId = room.HotelId,
                UserId = userid,
                CheckInDate = dateBooking.CheckInDate,
                CheckOutDate = dateBooking.CheckOutDate,
                TotalPrice = totalPrice
            };

            if (room != null && room.Status == "Available")
            {
                _room.UpdatingRoom(id, new Dtos.updatingStatus { Status = "Booked" });

                if (result)
                {
                    booking.AddBooking(newbooking);
                }
                else
                {
                    throw new Exception("Invalid booking dates.");
                }

                
            }
            if (room != null && room.Status == "Booked")
            {
                var date = _room.GetAllRoomsIncludeBookings().Where(r => r.Id == id).Select(r => r.Bookings.Select(b => new Dtos.DateBooking { CheckInDate = b.CheckInDate, CheckOutDate = b.CheckOutDate })).ToList();

                var vailddate = CheckBookingDateForBookedRoom.IsBookingDateAvailable(dateBooking.CheckInDate, dateBooking.CheckOutDate, date);
                if (result && vailddate)
                {
                    booking.AddBooking(newbooking);

                }
                else
                {
                    throw new Exception("Invalid booking dates or the room is already booked for the selected dates.");
                }


               



            }
           
        }

        public IEnumerable<Dtos.GetAllBookingOfUser> GetAllBookingsOfUser(string userid)
        {
            var bookings = booking.GetAllBookings().Where(b => b.UserId == userid).ToList();
            List<Dtos.GetAllBookingOfUser> bookingDtos = new List<Dtos.GetAllBookingOfUser>();
            if (bookings.Count == 0)
            {
               throw new Exception("No bookings found for the user.");
            }
            else
            {
                foreach (var item in bookings)
                {
                    var bookingDto = new Dtos.GetAllBookingOfUser
                    {
                        BookingId = item.Id,
                        HotelName = item.Room.Hotel.Name,
                        HotelAddress = item.Room.Hotel.Address,
                        RoomType = item.Room.RoomType,
                        RoomNumber = item.Room.RoomNumber,
                        pricePerNight = item.Room.PricePerNight,
                        CheckInDate = item.CheckInDate,
                        CheckOutDate = item.CheckOutDate,
                        TotalPrice = item.TotalPrice
                    };
                    bookingDtos.Add(bookingDto);
                }
            }

            return bookingDtos;
        }

        public IEnumerable<Dtos.GetAllBookingforAdmin> GetAllBookingforAdmins() {

            var bookings = booking.GetAllBookings();
            List<Dtos.GetAllBookingforAdmin> allbookings = new List<Dtos.GetAllBookingforAdmin>();


            foreach (var item in bookings)
            {
                var allbooking = new Dtos.GetAllBookingforAdmin
                {

                    BookingId = item.Id,
                    UserName = item.User.UserName,
                    PhoneNumber = item.User.PhoneNumber,
                    Email = item.User.Email,
                    RoomNumber = item.Room.RoomNumber,
                    RoomType = item.Room.RoomType,
                    HotelName = item.Room.Hotel.Name,
                    HotelAddress = item.Room.Hotel.Address,
                    pricePerNight = item.Room.PricePerNight,
                    CheckInDate = item.CheckInDate,
                    CheckOutDate = item.CheckOutDate,
                    TotalPrice = item.TotalPrice




                };
                allbookings.Add(allbooking);

            }
            return allbookings;

            
           
        
        }

        public void removebooking(int id) {

            booking.DeleteBooking(id);
        
        }


                
        
        
        
    }
}
