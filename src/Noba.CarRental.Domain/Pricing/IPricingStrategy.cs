namespace Noba.CarRental.Domain.Pricing
{
    public interface IPricingStrategy
    {
        decimal CalculatePrice(decimal baseDayRental, decimal baseKmPrice, int numberOfDays, decimal numberOfKm);
    }
}
