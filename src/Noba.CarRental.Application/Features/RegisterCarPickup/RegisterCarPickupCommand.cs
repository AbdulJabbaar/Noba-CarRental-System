using MediatR;
using Noba.CarRental.Domain.Enums;

namespace Noba.CarRental.Application.Features.RegisterCarPickup
{
    public record RegisterCarPickupCommand(
        string BookingNumber, 
        string RegistrationNumber, 
        string CustomerSSN, 
        CarCategoryType CarCategory, 
        DateTime PickupDateTime, 
        decimal MeterReading) : IRequest<CarRentalResponse>;
}
