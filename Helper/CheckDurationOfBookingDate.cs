namespace Hotel.Helper
{
    public class CheckDurationOfBookingDate
    {

        public static bool IsVaildBookingDate(DateTime Checkin,DateTime checkout)
        {
            if (Checkin < checkout)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
