using MediatR;
using Noba.CarRental.Application.Exceptions;
using Noba.CarRental.Application.Persistence;
using Noba.CarRental.Domain.Entities;

namespace Noba.CarRental.Application.Features.RegisterCarPickup
{
    public class RegisterCarPickupCommandHandler : IRequestHandler<RegisterCarPickupCommand, CarRentalResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        public RegisterCarPickupCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<CarRentalResponse> Handle(RegisterCarPickupCommand request, CancellationToken cancellationToken)
        {
            var car = await _unitOfWork.CarRepository.GetCarByRegistrationNumberAsync(request.RegistrationNumber);
            if (car == null)
            {
                throw new CarNotFoundException($"Car not found with specified registraion number:{request.RegistrationNumber}");
            }

            //TODO: this might not be needed because we are getting the car by the car registration number
            if (car.CarCategory.CategoryType != request.CarCategory)
            {
                throw new ArgumentException("Invalid car category");
            }

            var rental = Rental.Create(Guid.NewGuid(), request.BookingNumber, request.CustomerSSN, request.PickupDateTime, request.MeterReading, car);
            await _unitOfWork.RentalRepository.AddAsync(rental);
            await _unitOfWork.CommitAsync();

            return new CarRentalResponse(request.BookingNumber, request.CustomerSSN, request.PickupDateTime, null, 0);
        }
    }
}
