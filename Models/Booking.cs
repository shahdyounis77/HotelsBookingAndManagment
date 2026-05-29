using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Models
{
    public class Booking
    {
        
        public int Id { get; set; }

        [ForeignKey("Hotel")]
        public int  HotelId { get; set; }
        [ForeignKey("Room")]
        public int  RoomId { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public double TotalPrice { get; set; }
        public Hotel Hotel { get; set; }
         public Room Room { get; set; }
         public User User { get; set; }
    }
}
