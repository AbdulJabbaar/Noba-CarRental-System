using MediatR;

namespace Noba.CarRental.Application.Features.RegisterCarReturn
{
    public record RegisterCarReturnCommand(
        string BookingNumber, 
        DateTime ReturnDateTime, 
        decimal MeterReading) :IRequest<CarRentalResponse>;
}
