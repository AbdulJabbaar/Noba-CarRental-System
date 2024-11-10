using Noba.CarRental.Domain.Entities;

namespace Noba.CarRental.Application.Persistence
{
    public interface IRentalRepository : IRepository<Rental>
    {
        Task<Rental> GetRentalByBookingNumber(string bookingNumber);
    }
}
