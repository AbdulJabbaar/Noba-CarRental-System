namespace Noba.CarRental.Domain.Pricing
{
    public class TruckPricingStrategy : IPricingStrategy
    {
        public decimal CalculatePrice(decimal baseDayRental, decimal baseKmPrice, int numberOfDays, decimal numberOfKm)
        {
            return baseDayRental * numberOfDays * 1.5M + baseKmPrice * numberOfKm * 1.5M;
        }
    }
}
