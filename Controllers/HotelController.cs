using Hotel.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HotelController : ControllerBase
    {
        private readonly Services.HotelServices _hotelServices;
        public HotelController(Services.HotelServices hotelServices)
        {
            _hotelServices = hotelServices;
        }

        [Authorize(Roles = "User,Admin")]
        [HttpGet]

        public async Task<IActionResult> GetAllHotels()
        {
            var hotels = _hotelServices.GetAllHotels();

            return Ok(hotels);
        }
        
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHotelById( int id)
        {
            var hotel = _hotelServices.GetHotelById(id);
            if (hotel == null)
            {
                return NotFound();
            }
            return Ok(hotel);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddHotel([FromBody] AddHotel addHotel)
        {
            _hotelServices.AddHotel(addHotel);
            return Created();


        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotelsById( int id)
        {
            _hotelServices.DeleteHotelById(id);
            return NoContent();
        }
    }
}
