using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Dtos
{
    public class AddBooking
    {
        public int Id { get; set; }

        
        public int HotelId { get; set; }
     
        public int RoomId { get; set; }
    
        public string UserId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public double TotalPrice { get; set; }
    }
}
