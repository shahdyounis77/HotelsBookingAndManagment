

using Microsoft.AspNetCore.Identity;

namespace Hotel.Models
{
    public class User:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
