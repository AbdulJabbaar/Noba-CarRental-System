namespace Noba.CarRental.Domain.Entities.CarCategory
{
    public class Pricing
    {
        public decimal BaseDayRental { get; }
        public decimal BaseKmPrice { get; }

        public Pricing(decimal baseDayRental, decimal baseKmPrice)
        {
            if (baseDayRental <= 0) throw new ArgumentException("Base day rental must be greater than zero.");
            if (baseKmPrice < 0) throw new ArgumentException("Base km price cannot be negative.");

            BaseDayRental = baseDayRental;
            BaseKmPrice = baseKmPrice;
        }
    }
}
