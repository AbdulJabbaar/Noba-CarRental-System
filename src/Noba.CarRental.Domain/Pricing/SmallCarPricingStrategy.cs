namespace Noba.CarRental.Domain.Pricing
{
    public class SmallCarPricingStrategy : IPricingStrategy
    {
        public decimal CalculatePrice(decimal baseDayRental, decimal baseKmPrice, int numberOfDays, decimal numberOfKm)
        {
            return baseDayRental * numberOfDays;
        }
    }
}
