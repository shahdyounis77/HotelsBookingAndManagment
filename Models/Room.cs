using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Models
{
   
    public class Room
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }

        public string RoomType { get; set; }

        public string Status { get; set; }

        public double PricePerNight { get; set; }

        [ForeignKey("Hotel")]
        public int HotelId { get; set; }
        public virtual Hotel Hotel { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }

        

    }

    
}
