namespace Hotel.Dtos
{
    public class GetRoom
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }

        public string RoomType { get; set; }

        public string Status { get; set; }

        public double PricePerNight { get; set; }
        
    }
}
