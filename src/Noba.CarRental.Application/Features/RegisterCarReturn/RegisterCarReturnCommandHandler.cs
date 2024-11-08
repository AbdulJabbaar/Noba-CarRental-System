using MediatR;
using Noba.CarRental.Application.Exceptions;
using Noba.CarRental.Domain.UnitOfWork;

namespace Noba.CarRental.Application.Features.RegisterCarReturn
{
    public class RegisterCarReturnCommandHandler : IRequestHandler<RegisterCarReturnCommand, CarRentalResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegisterCarReturnCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CarRentalResponse> Handle(RegisterCarReturnCommand request, CancellationToken cancellationToken)
        {
            // find the rented car by Booking Number
            var booking = await _unitOfWork.RentalRepository.GetRentalByBookingNumber(request.BookingNumber);
            if (booking == null)
            {
                throw new BookingNotFoundException($"Booking not found against specified booking number: {request.BookingNumber}");
            }
            // update the booking
            booking.ReturnCar(request.ReturnDateTime, request.MeterReading);
            await _unitOfWork.RentalRepository.UpdateAsync(booking);
            
            // update the car with the updated milage
            // TODO: move this part to an event as Mediator INotificationHandler 
            var car = await _unitOfWork.CarRepository.GetByIdAsync(booking.CarId);
            car.UpdateKm(request.MeterReading);
            await _unitOfWork.CarRepository.UpdateAsync(car);

            await _unitOfWork.CommitAsync();

            // calculate the price 
            var rentalPrice = booking.CalculateRent();

            return new CarRentalResponse(booking.BookingNumber, booking.CustomerSSN, booking.PickUpDate, booking.ReturnDate, rentalPrice);
        }
    }
}
