namespace Hotel
{
    public class CalculateNumberOfDays
    {
        
        

        

        public static int GetNumberOfDays(DateTime checkInDate, DateTime checkOutDate)
        {
           var days = (checkOutDate - checkInDate).Days;
            return days;
        }
    }
}
