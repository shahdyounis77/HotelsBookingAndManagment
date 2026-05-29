namespace Hotel.Helper
{
    public class CalculateBookingTotalPrice
    {
       
        public static double GetTotalPrice(double pricepernight,int days)
        {
            
            var totalprice = pricepernight * days;

            return totalprice;
        }
    }
}
