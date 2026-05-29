namespace Hotel.Helper
{
    public class CheckBookingDateForBookedRoom
    {

        public static bool IsBookingDateAvailable(DateTime checkin,DateTime checkout,List<IEnumerable<Dtos.DateBooking>> bookedDates)
        {
            foreach (var booked in bookedDates)
            {
                foreach (var date in booked)
                {
                    if (checkin < date.CheckOutDate && checkout > date.CheckInDate)
                    {
                        return false;
                    }
                }
               
            }
            return true; 
        }
    }
}
