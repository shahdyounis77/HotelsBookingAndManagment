namespace Hotel.Services
{
    public class HotelServices
    {
        private readonly Interfaces.IHotel hotel;
        public HotelServices(Interfaces.IHotel hotel)
        {
            this.hotel = hotel;
        }

        public IEnumerable<Dtos.GetHotel> GetAllHotels()
        {
            var hotels = hotel.GetAllHotels();
            var hotelDtos = hotels.Select(h => new Dtos.GetHotel
            {
                Id = h.Id,
                Name = h.Name,
                Address = h.Address,
                City = h.City
            });
            return hotelDtos;
        }
        public Dtos.GetHotel GetHotelById(int id)
        {
            var hotelEntity = hotel.GetHotelById(id);
            if (hotelEntity == null)
            {
                return null;
            }
            var hotelDto = new Dtos.GetHotel
            {
                Id = hotelEntity.Id,
                Name = hotelEntity.Name,
                Address = hotelEntity.Address,
                City = hotelEntity.City
            };
            return hotelDto;
        }

        public void AddHotel(Dtos.AddHotel addHotel)
        {
            hotel.AddHotel(addHotel);
        }

        public void DeleteHotelById(int id)
        {
            hotel.DeleteHotelById(id);
        }
    }
}
