using Hotel.Models;

namespace Hotel.Interfaces
{
    public interface IRoom
    {
        IEnumerable<Room> GetAllRooms(int id);
        Room GetRoomById(int id);
        void UpdatingRoom(int id,  Dtos.updatingStatus updatingStatus);
        void UpdatingRoomPrice(int id, Dtos.UpdatePrice price);
        void RemoveRoomById(int id);
        void AddRoom(int hotelId, Dtos.AddRoom addRoom);
        IEnumerable<Room> GetAllRoomsIncludeBookings();





    }
}
