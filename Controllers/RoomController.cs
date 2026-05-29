using Hotel.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoomController : ControllerBase
    {
        private readonly RoomServices _roomService;
        public RoomController(RoomServices roomService)
        {
            _roomService = roomService;
        }
        [Authorize(Roles = "Admin,User")]
        [HttpGet("Hotel/{Hotelid}")]
        public async Task<IActionResult> GetAllRooms(int Hotelid )
        {
            var rooms = _roomService.ViewAllRooms(Hotelid);
            return Ok(rooms);
        }
        [Authorize(Roles = "Admin,User")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoomById( int id)
        {
            var room = _roomService.ViewRoomById(id);
            if (room == null)
            {
                return NotFound();
            }
            return Ok(room);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("{hotelId}")]
        public async Task<IActionResult> AddRoom( int hotelId, Dtos.AddRoom addRoom)
        {
            _roomService.AddRoom(hotelId, addRoom);
            return Created();
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveRoomById( int id)
        {
            _roomService.RemoveRoomById(id);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/Status")]
        public async Task<IActionResult> UpdatingRoom( int id, Dtos.updatingStatus updatingStatus)
        {
            _roomService.UpdatingRoom(id, updatingStatus);
            return Ok(new { message = "update successfully" });

        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/Price")]
        public async Task<IActionResult> UpdatingRoomPrice( int id, Dtos.UpdatePrice updatingPrice)
        {
            _roomService.UpdatingRoomPrice(id, updatingPrice);
            return Ok(new { message = "update successfully" });
        }
    }
}
