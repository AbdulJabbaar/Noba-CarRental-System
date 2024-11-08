using Noba.CarRental.Domain.Enums;

namespace Noba.CarRental.Domain.Pricing
{
    public class PricingStrategyFactory
    {
        public IPricingStrategy GetStrategy(CarCategoryType categoryType)
        {
            return categoryType switch
            {
                CarCategoryType.Small => new SmallCarPricingStrategy(),
                CarCategoryType.Combi => new CombiPricingStrategy(),
                CarCategoryType.Truck => new TruckPricingStrategy(),
                _ => throw new ArgumentException("Invalid car category")
            };
        }
    }
}
