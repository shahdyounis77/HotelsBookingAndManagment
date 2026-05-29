using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Hotel.Repos
{
    public class HotelRepo: Interfaces.IHotel
    {
        private readonly Models.Context context;

        public HotelRepo(Models.Context context)
        {
            this.context = context;
        }

        public void DeleteHotelById(int id)
        {
            var hotel = context.Hotels.Find(id);
            if (hotel != null)
            {
                context.Hotels.Remove(hotel);
                context.SaveChanges();
            }
        }

        public IEnumerable<Models.Hotel> GetAllHotels()
        {
            var hotels = context.Hotels.AsNoTracking().ToList();
           

            



            return hotels;
        }
        public Models.Hotel GetHotelById(int id)
        {
            var hotel = context.Hotels.AsNoTracking().FirstOrDefault(h => h.Id == id);
            return hotel;
        }
           public void AddHotel(Dtos.AddHotel addHotel)
        {
            var hotel = new Models.Hotel
            {
                Name = addHotel.Name,
                Address = addHotel.Address,
                City = addHotel.City,

            };
            context.Hotels.Add(hotel);
            context.SaveChanges();
        }

    }
}
