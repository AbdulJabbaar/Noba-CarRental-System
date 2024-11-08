using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Noba.CarRental.Application;
using Noba.CarRental.Application.Features.RegisterCarPickup;
using Noba.CarRental.Application.Features.RegisterCarReturn;
using Noba.CarRental.Domain.Entities;
using Noba.CarRental.Domain.Enums;
using Noba.CarRental.Domain.UnitOfWork;
using Noba.CarRental.Persistance;

namespace Noba.CarRental.IntegrationTest
{
    public class CarRentalIntegrationTests
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMediator _mediator;

        private Guid SmallCarCategoryId = Guid.Parse("6e2cda91-6ef4-402c-8420-ef9059d17778");
        private readonly string SmallCarNumber = "SMALLCAR123";

        private Guid CombiCategoryId = Guid.Parse("57715a12-ff98-4cb9-8973-5ee3a319738e");
        private readonly string CombiCarNumber = "COMBI123";

        private Guid TruckCategoryId = Guid.Parse("acaed827-34c9-4ca4-bed9-a007e647c085");
        private readonly string TruckCarNumber = "TRUCK123";

        private readonly decimal InitialMeterReading = 1000;

        public CarRentalIntegrationTests()
        {
            var services = new ServiceCollection();

            services.AddApplicationLayer();
            services.AddPersistanceLayer();

            _serviceProvider = services.BuildServiceProvider();
            _mediator = _serviceProvider.GetRequiredService<IMediator>();            
        }

        [Theory]
        [InlineData("BRN#100000", "SMALLCAR123", 2, 1000, 20)]
        [InlineData("BRN#200000", "SMALLCAR123", 1, 1500, 10)]
        [InlineData("BRN#300000", "COMBI123", 2, 1500, 10052.0)]
        [InlineData("BRN#300000", "COMBI123", 1, 1500, 10026)]
        [InlineData("BRN#400000", "TRUCK123", 2, 1500, 22590)]
        [InlineData("BRN#400000", "TRUCK123", 1, 1500, 22545)]
        public async Task CompleteCarRentalFlow_PickupAndReturn_Success(string bookingNumber, string registrationNumber, int numberOfDays, decimal currentReadingKm, decimal amount)
        {
            // Arrange
            var unitOfWork = _serviceProvider.GetRequiredService<IUnitOfWork>();
            await AddMockCarAndCategoryData(unitOfWork);
            var carDetails = await unitOfWork.CarRepository.GetCarByRegistrationNumberAsync(registrationNumber);

            var carPickupCommand = new RegisterCarPickupCommand(
                bookingNumber, 
                carDetails.RegistrationNumber, 
                "19999999-0000", 
                carDetails.CarCategory.CategoryType, 
                DateTime.Now.Date, 
                carDetails.CurrentKm);
            var carReturnCommand = new RegisterCarReturnCommand(bookingNumber, DateTime.Now.Date.AddDays(numberOfDays), currentReadingKm);

            // Act
            await _mediator.Send(carPickupCommand);
            var carReturnCommandResponse = await _mediator.Send(carReturnCommand);

            // Assert
            carReturnCommandResponse.Should().NotBeNull();
            carReturnCommandResponse.Amount.Should().Be(amount);

            var rentalRecord = await unitOfWork.RentalRepository.GetAllAsync();
            rentalRecord.Should().NotBeNull(); 
            rentalRecord.Count().Should().Be(1);

            var car = await unitOfWork.CarRepository.GetCarByRegistrationNumberAsync(registrationNumber);
            car.Should().NotBeNull();
            car.CurrentKm.Should().Be(currentReadingKm);
        }

        [Fact]
        public async Task Test_SmallCarCategory_Added()
        {
            // Arrange
            var unitOfWork = _serviceProvider.GetRequiredService<IUnitOfWork>();
            await AddMockCarAndCategoryData(unitOfWork);
            // Act

            var carCategory = await unitOfWork.CarCategoryRepository.GetByIdAsync(SmallCarCategoryId);

            //Assert
            carCategory.Should().NotBeNull();
        }

        private async Task AddMockCarAndCategoryData(IUnitOfWork unitOfWork)
        {
            // Add Small Car
            var smallCategory = CarCategory.Create(SmallCarCategoryId, CarCategoryType.Small, 10, 10);
            await unitOfWork.CarCategoryRepository.AddAsync(smallCategory);

            var smallCar = Car.Create(Guid.NewGuid(), SmallCarNumber, smallCategory, InitialMeterReading);
            await unitOfWork.CarRepository.AddAsync(smallCar);


            // Add Combi Category
            var combiCategory = CarCategory.Create(CombiCategoryId, CarCategoryType.Combi, 20, 20);
            await unitOfWork.CarCategoryRepository.AddAsync(combiCategory);

            var combiCar = Car.Create(Guid.NewGuid(), CombiCarNumber, combiCategory, InitialMeterReading);
            await unitOfWork.CarRepository.AddAsync(combiCar);

            // Add Truck Category
            var truckCategory = CarCategory.Create(TruckCategoryId, CarCategoryType.Truck, 30, 30);
            await unitOfWork.CarCategoryRepository.AddAsync(truckCategory);

            var truckCar = Car.Create(Guid.NewGuid(), TruckCarNumber, truckCategory, InitialMeterReading);
            await unitOfWork.CarRepository.AddAsync(truckCar);

            await unitOfWork.CommitAsync();
        }
    }
}