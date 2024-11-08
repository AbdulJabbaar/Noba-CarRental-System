namespace Noba.CarRental.Application.Features
{
    public record CarRentalResponse(string BookingNumber, string CustomerSSN, DateTime PickupDate, DateTime? ReturnDate, decimal Amount);
}
