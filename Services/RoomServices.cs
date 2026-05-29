using Hotel.Dtos;
using Hotel.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Hotel.Services
{
    public class RoomServices
    {
        private readonly IRoom room;


        public RoomServices(IRoom room)
        {
            this.room = room;
        }

        public IEnumerable<GetRoom> ViewAllRooms(int hotelId)
        {
            var rooms = room.GetAllRooms(hotelId);
            var result = rooms.Select(r => new GetRoom
            {
                Id = r.Id,
                RoomNumber = r.RoomNumber,
                RoomType = r.RoomType,
                Status = r.Status,
                PricePerNight = r.PricePerNight,

            });
            return result;


        }
        public IEnumerable<GetRoom> ViewAvailableRooms(int hotelid)
        {

            var rooms = room.GetAllRooms(hotelid);


            var availablerooms = rooms.Where(r => r.Status == "Available");
            var result = availablerooms.Select(r => new GetRoom
            {
                Id = r.Id,
                RoomNumber = r.RoomNumber,
                RoomType = r.RoomType,
                PricePerNight = r.PricePerNight,
            });

            return result;

        }
        public GetRoom ViewRoomById(int id)
        {
            var r = room.GetRoomById(id);
            if (r == null)
            {
                return null;
            }
            var result = new GetRoom
            {
                Id = r.Id,
                RoomNumber = r.RoomNumber,
                RoomType = r.RoomType,
                Status = r.Status,
                PricePerNight = r.PricePerNight,
            };
            return result;
        }

        public void AddRoom(int hotelId, AddRoom addRoom)
        {
            room.AddRoom(hotelId, addRoom);
        }
        public void RemoveRoomById(int id)
        {
            room.RemoveRoomById(id);
        }
        public void UpdatingRoom(int id, updatingStatus updatingStatus)
        {
            room.UpdatingRoom(id, updatingStatus);

        }
        public void UpdatingRoomPrice(int id, UpdatePrice price)
        {
            room.UpdatingRoomPrice(id, price);
        }
    }
}
