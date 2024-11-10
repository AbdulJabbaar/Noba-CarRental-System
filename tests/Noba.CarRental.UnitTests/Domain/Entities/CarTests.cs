using FluentAssertions;
using Noba.CarRental.Domain.Entities;
using Noba.CarRental.Domain.Entities.CarCategory;

namespace Noba.CarRental.UnitTests.Domain.Entities
{
    public class CarTests
    {
        private string CarRegistrationNumber => "ABC123";
        private decimal CurrentKM => 5000;


        [Fact]
        public void Create_ShouldReturn_Car_WithValidInputs()
        {
            // Arrange
            var carCategory = MockCarCategory();
            var id = Guid.NewGuid();

            // Act
            var car = Car.Create(id, CarRegistrationNumber, carCategory, CurrentKM);

            // Assert
            car.Should().NotBeNull();
            car.Id.Should().Be(id);
            car.RegistrationNumber.Should().Be(CarRegistrationNumber);
            car.CarCategoryId.Should().Be(carCategory.Id);
            car.CarCategory.Should().Be(carCategory);
            car.CurrentKm.Should().Be(CurrentKM);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Create_ShouldThrowException_WhenRegistrationNumberIsEmptyOrNull(string registrationNumber)
        {
            // Arrange
            var carCategory = MockCarCategory();
            var id = Guid.NewGuid();

            // Act & Assert
            var act = () => Car.Create(id, registrationNumber, carCategory, CurrentKM);
            act.Should().Throw<ArgumentException>().WithMessage("Registration number cannot be empty.");
        }

        [Fact]
        public void Create_ShouldThrowException_WhenCurrentKmIsNegative()
        {
            // Arrange
            var carCategory = MockCarCategory();
            var id = Guid.NewGuid();

            // Act & Assert
            var act = () => Car.Create(id, CarRegistrationNumber, carCategory, -1);
            act.Should().Throw<ArgumentException>().WithMessage("Current kilometers cannot be negative.");
        }

        [Fact]
        public void UpdateKm_ShouldUpdateCurrentKm_WhenNewKmIsGreaterThanCurrentKm()
        {
            // Arrange
            var carCategory = MockCarCategory();
            var id = Guid.NewGuid();
            var car = Car.Create(id, CarRegistrationNumber, carCategory, CurrentKM);

            decimal newKm = 6000;

            // Act
            car.UpdateKm(newKm);

            // Assert
            car.CurrentKm.Should().Be(newKm);
        }

        [Fact]
        public void UpdateKm_ShouldThrowException_WhenNewKmIsLessThanCurrentKm()
        {
            // Arrange
            var carCategory = MockCarCategory();
            var id = Guid.NewGuid();
            var car = Car.Create(id, CarRegistrationNumber, carCategory, CurrentKM);

            decimal newKm = 4000;

            // Act & Assert
            var act = () => car.UpdateKm(newKm);
            act.Should().Throw<ArgumentException>().WithMessage("New kilometers must be greater than current kilometers.");
        }

        private CarCategory MockCarCategory()
        {
            return CarCategory.Create(Guid.NewGuid(), CarRental.Domain.Enums.CarCategoryType.Small, new CarRental.Domain.Entities.CarCategory.Pricing(100, 100));
        }
    }
}
