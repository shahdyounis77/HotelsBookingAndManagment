namespace Hotel.Dtos
{
    public class GetAllBookingOfUser
    {
        public int BookingId { get; set; }
        public string HotelName { get; set; }

        public string HotelAddress { get; set; }

        public string RoomType { get; set; }

        public string RoomNumber { get; set; }

        public double pricePerNight { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public double TotalPrice { get; set; }
    }
}
