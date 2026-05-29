

namespace Hotel.Interfaces
{
    public interface IHotel
    {
        IEnumerable<Models.Hotel> GetAllHotels();


        void DeleteHotelById(int id);
        Models.Hotel GetHotelById(int id);

        void AddHotel(Dtos.AddHotel addHotel);


    }
}
