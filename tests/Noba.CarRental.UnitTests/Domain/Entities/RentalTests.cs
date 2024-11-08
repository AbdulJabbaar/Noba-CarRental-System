using FluentAssertions;
using Noba.CarRental.Domain.Entities;
using Noba.CarRental.Domain.Enums;

namespace Noba.CarRental.UnitTests.Domain.Entities
{
    public class RentalTests
    {
        private readonly string _bookingNumber = "Booking123";
        private readonly string _customerSSN = "123456789";
        private readonly decimal _pickUpKm = 1000;
        private readonly DateTime _pickUpDate = DateTime.Now.Date;
        private readonly Car _car;

        public RentalTests()
        {
            _car = MockCar();
        }

        [Fact]
        public void Create_ShouldReturn_Rental_WithValidInputs()
        {
            // Act
            var id = Guid.NewGuid();
            var rental = Rental.Create(id, _bookingNumber, _customerSSN, _pickUpDate, _pickUpKm, _car);

            // Assert
            rental.Should().NotBeNull();
            rental.Id.Should().Be(id);
            rental.BookingNumber.Should().Be(_bookingNumber);
            rental.CustomerSSN.Should().Be(_customerSSN);
            rental.PickUpDate.Should().Be(_pickUpDate);
            rental.PickUpKm.Should().Be(_pickUpKm);
            rental.CarId.Should().Be(_car.Id);
            rental.Car.Should().Be(_car);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Create_ShouldThrowException_WhenInvalidBookingNumberOrCustomerSSNOrPickUpKm(string invalidValue)
        {
            // Act & Assert
            Action act = () => Rental.Create(Guid.NewGuid(), invalidValue, _customerSSN, _pickUpDate, _pickUpKm, _car);
            act.Should().Throw<ArgumentException>().WithMessage("Booking number cannot be empty.");

            act = () => Rental.Create(Guid.NewGuid(), _bookingNumber, invalidValue, _pickUpDate, _pickUpKm, _car);
            act.Should().Throw<ArgumentException>().WithMessage("Customer SSN cannot be empty.");

            act = () => Rental.Create(Guid.NewGuid(), _bookingNumber, _customerSSN, _pickUpDate, -1, _car);
            act.Should().Throw<ArgumentException>().WithMessage("Pick-up kilometers cannot be negative.");
        }

        [Fact]
        public void ReturnCar_ShouldUpdateReturnDateAndKm_WhenValidInputs()
        {
            // Arrange
            var rental = Rental.Create(Guid.NewGuid(), _bookingNumber, _customerSSN, _pickUpDate, _pickUpKm, _car);
            var returnDate = DateTime.Now.AddDays(3);
            var returnKm = 1200;

            // Act
            rental.ReturnCar(returnDate, returnKm);

            // Assert
            rental.ReturnDate.Should().Be(returnDate);
            rental.ReturnKm.Should().Be(returnKm);
        }

        [Fact]
        public void ReturnCar_ShouldThrowException_WhenReturnKmIsLessThanPickUpKm()
        {
            // Arrange
            var rental = Rental.Create(Guid.NewGuid(), _bookingNumber, _customerSSN, _pickUpDate, _pickUpKm, _car);
            var returnDate = DateTime.Now.AddDays(3);
            var returnKm = 800;

            // Act & Assert
            Action act = () => rental.ReturnCar(returnDate, returnKm);
            act.Should().Throw<ArgumentException>().WithMessage("Return kilometers cannot be less than pick-up kilometers.");
        }

        [Fact]
        public void CalculateRent_ShouldReturnZero_WhenNotReturned()
        {
            // Arrange
            var rental = Rental.Create(Guid.NewGuid(), _bookingNumber, _customerSSN, _pickUpDate, _pickUpKm, _car);

            // Act
            var rent = rental.CalculateRent();

            // Assert
            rent.Should().Be(0);
        }

        [Fact]
        public void CalculateRent_ShouldReturnCorrectAmount_WhenReturned()
        {
            // Arrange
            var rental = Rental.Create(Guid.NewGuid(), _bookingNumber, _customerSSN, _pickUpDate, _pickUpKm, _car);
            var returnDate = DateTime.Now.Date.AddDays(3);
            var returnKm = 1200;
            rental.ReturnCar(returnDate, returnKm);

            // Act
            var rent = rental.CalculateRent();

            // Assert
            rent.Should().Be(300);
        }

        private Car MockCar()
        {
            var carCategory = CarCategory.Create(Guid.NewGuid(), CarCategoryType.Small, 100, 50);
            return Car.Create(Guid.NewGuid(), "ABC123", carCategory, 1000);
        }
    }
}
