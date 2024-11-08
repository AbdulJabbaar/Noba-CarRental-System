using FluentAssertions;
using Noba.CarRental.Domain.Pricing;

namespace Noba.CarRental.UnitTests.Domain.Pricing
{
    public class TruckPricingStrategyTests
    {
        [Theory]
        [InlineData(100, 2, 0, 0, 0)]
        [InlineData(150, 5, 0, 0, 0)]
        [InlineData(50, 10, 0, 0, 0)]
        [InlineData(100, 0, 2, 0, 300)]
        [InlineData(150, 10, 5, 200, 4125)]  // baseDayRental = 150, baseKmPrice = 10, numberOfDays = 5, numberOfKm = 200, expected price = (150 * 5 * 1.5)+(10 * 200 * 1.5) = 4125
        public void CalculatePrice_ShouldReturnCorrectPrice_ForGivenInputs(decimal baseDayRental, decimal baseKmPrice, int numberOfDays, decimal numberOfKm, decimal expectedPrice)
        {
            // Arrange
            var pricingStrategy = new TruckPricingStrategy();

            // Act
            var price = pricingStrategy.CalculatePrice(baseDayRental, baseKmPrice, numberOfDays, numberOfKm);

            // Assert
            price.Should().Be(expectedPrice);
        }
    }
}
