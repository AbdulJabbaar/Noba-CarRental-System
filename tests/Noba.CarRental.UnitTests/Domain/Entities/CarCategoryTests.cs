using FluentAssertions;
using Noba.CarRental.Domain.Entities;
using Noba.CarRental.Domain.Enums;

namespace Noba.CarRental.UnitTests.Domain.Entities
{
    public class CarCategoryTests
    {
        [Fact]
        public void Create_ShouldReturn_CarCategory_WithValidInputs()
        {
            // Arrange
            var id = Guid.NewGuid();
            var categoryType = CarCategoryType.Small;
            decimal baseDayRental = 100;
            decimal baseKmPrice = 10;

            // Act
            var carCategory = CarCategory.Create(id, categoryType, baseDayRental, baseKmPrice);

            // Assert
            carCategory.Should().NotBeNull();
            carCategory.Id.Should().Be(id);
            carCategory.CategoryType.Should().Be(categoryType);
            carCategory.BaseDayRental.Should().Be(baseDayRental);
            carCategory.BaseKmPrice.Should().Be(baseKmPrice);
        }

        [Fact]
        public void Create_ShouldThrowException_WhenBaseDayRentalIsZeroOrNegative()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act & Assert
            var act = () => CarCategory.Create(id, CarCategoryType.Small, 0, 10);
            act.Should().Throw<ArgumentException>().WithMessage("Base day rental must be greater than zero.");

            act = () => CarCategory.Create(id, CarCategoryType.Small, -1, 10);
            act.Should().Throw<ArgumentException>().WithMessage("Base day rental must be greater than zero.");
        }

        [Fact]
        public void Create_ShouldThrowException_WhenBaseKmPriceIsNegative()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act & Assert
            var act = () => CarCategory.Create(id, CarCategoryType.Small, 100, -1);
            act.Should().Throw<ArgumentException>().WithMessage("Base km price cannot be negative.");
        }
    }
}
