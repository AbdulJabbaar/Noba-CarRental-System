using Noba.CarRental.Domain.Entities;

namespace Noba.CarRental.Domain.Repositories
{
    public interface IRentalRepository : IRepository<Rental>
    {
        Task<Rental> GetRentalByBookingNumber(string bookingNumber);
    }
}
