using Hotel.Models;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Repos
{
    public class RoomRepo : Interfaces.IRoom
    {
        private readonly Models.Context context;
        public RoomRepo(Models.Context context)
        {
            this.context = context;
        }
        public IEnumerable<Room> GetAllRooms(int id)
        {
            var rooms = context.Rooms.AsNoTracking().Where(r => r.HotelId == id).ToList();
            return rooms;
        }


        public IEnumerable<Room> GetAllRoomsIncludeBookings()
        {
            var rooms = context.Rooms.AsNoTracking().Where(r => r.Status == "Booked").Include(r => r.Bookings).ToList();
            return rooms;
        }

        public Room GetRoomById(int id)
        {
            var room = context.Rooms.FirstOrDefault(r => r.Id == id);
            return room;
        }

        public void UpdatingRoom(int id, Dtos.updatingStatus updatingStatus)
        {
            context.Rooms.Find(id).Status = updatingStatus.Status;
            context.SaveChanges();

        }

        public void RemoveRoomById(int id)
        {
            var room = context.Rooms.Find(id);
            if (room != null)
            {
                context.Rooms.Remove(room);
                context.SaveChanges();
            }
        }

        public void AddRoom(int hotelId, Dtos.AddRoom addRoom)
        {
            var room = new Models.Room
            {
                HotelId = hotelId,
                RoomNumber = addRoom.RoomNumber,
                RoomType = addRoom.RoomType,
                PricePerNight = addRoom.PricePerNight,
                Status = "Available"
            };
            context.Rooms.Add(room);
            context.SaveChanges();
        }

        public void UpdatingRoomPrice(int id, Dtos.UpdatePrice price)
        {
            context.Rooms.Find(id).PricePerNight = price.PricePerNight;
            context.SaveChanges();
        }
    }
}