namespace Noba.CarRental.Domain.Pricing
{
    public class CombiPricingStrategy : IPricingStrategy
    {
        public decimal CalculatePrice(decimal baseDayRental, decimal baseKmPrice, int numberOfDays, decimal numberOfKm)
        {
            return baseDayRental * numberOfDays * 1.3M + baseKmPrice * numberOfKm;
        }
    }
}
