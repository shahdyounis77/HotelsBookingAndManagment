using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookingController : ControllerBase
    {
        private readonly Services.BookingServices _bookingService;

        public BookingController(Services.BookingServices bookingServices)
        {
            _bookingService = bookingServices;
        }
        [Authorize(Roles = "User")]
        [HttpPost("{RoomId}")]

        public async Task<IActionResult> BookingRoom( int RoomId, [FromBody] Dtos.DateBooking dateBooking)
        {
            var userid = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            _bookingService.BookingRoom(RoomId, userid, dateBooking);

            return Ok(new { Message = "Room booked successfully" });
        }
        [Authorize(Roles = "User")]
        [HttpGet("User")]

        public async Task<IActionResult> GetAllBookingsUser()
        {
            var userid = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var bookings = _bookingService.GetAllBookingsOfUser(userid);
            return Ok(bookings);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Admin")]

        public async Task<IActionResult> GetAllBookingsAdmin()
        {
            
            var bookings = _bookingService.GetAllBookingforAdmins();
            return Ok(bookings);
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelBooking( int id)
        {

            _bookingService.removebooking(id);
            return Ok(new { Message = "Booking cancelled successfully" });




        }
    }
}
