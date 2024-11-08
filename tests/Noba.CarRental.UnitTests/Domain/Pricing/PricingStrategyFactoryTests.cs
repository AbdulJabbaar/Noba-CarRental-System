using FluentAssertions;
using Noba.CarRental.Domain.Enums;
using Noba.CarRental.Domain.Pricing;

namespace Noba.CarRental.UnitTests.Domain.Pricing
{
    public class PricingStrategyFactoryTests
    {
        [Theory]
        [InlineData(CarCategoryType.Small, typeof(SmallCarPricingStrategy))]
        [InlineData(CarCategoryType.Combi, typeof(CombiPricingStrategy))]
        [InlineData(CarCategoryType.Truck, typeof(TruckPricingStrategy))]
        public void GetStrategy_ShouldReturnsCorrectPricingStrategy_ForValidCategoryType(CarCategoryType categoryType, Type expectedStrategyType)
        {
            // Arrange 
            var pricingStrategyFactory = new PricingStrategyFactory();

            // Act
            var pricingStrategy = pricingStrategyFactory.GetStrategy(categoryType);

            // Assert
            pricingStrategy.Should().BeOfType(expectedStrategyType);
        }

        [Fact]
        public void GetStrategy_ShouldThrowException_ForInvalidCategory()
        {
            // Arrange 
            var pricingStrategyFactory = new PricingStrategyFactory();

            // Act & Assert
            var act = () => pricingStrategyFactory.GetStrategy((CarCategoryType)999);
            act.Should().Throw<ArgumentException>();
        }
    }
}
