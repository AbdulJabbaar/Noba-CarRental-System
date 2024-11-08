using FluentAssertions;
using Noba.CarRental.Domain.Pricing;

namespace Noba.CarRental.UnitTests.Domain.Pricing
{
    public class SmallCarPricingStrategyTests
    {
        [Fact]
        public void CalculatePrice_ShouldReturnZero_WhenNumberOfDaysIsZero()
        {
            // Arrange
            var pricingStrategy = new SmallCarPricingStrategy();

            // Act
            var price = pricingStrategy.CalculatePrice(100, 100, 0, 2000);

            // Assert
            price.Should().Be(0);
        }

        [Theory]
        [InlineData(100, 2, 200)]
        [InlineData(150, 5, 750)]
        [InlineData(50, 10, 500)]
        public void CalculatePrice_ShouldReturnCorrectPrice_ForGivenInputs(decimal baseDayRental, int numberOfDays, decimal expectedPrice)
        {
            // Arrange
            var pricingStrategy = new SmallCarPricingStrategy();

            // Act
            var price = pricingStrategy.CalculatePrice(baseDayRental, 0, numberOfDays, 0);

            // Assert
            price.Should().Be(expectedPrice);
        }
    }
}
