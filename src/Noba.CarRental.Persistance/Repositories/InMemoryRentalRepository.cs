using Noba.CarRental.Domain.Entities;
using Noba.CarRental.Domain.Repositories;

namespace Noba.CarRental.Persistance.Repositories
{
    public class InMemoryRentalRepository : InMemoryRepository<Rental>, IRentalRepository
    {
        public Task<Rental> GetRentalByBookingNumber(string bookingNumber)
        {
            var rental = _entities.FirstOrDefault(r => r.BookingNumber == bookingNumber);
            return Task.FromResult(rental);
        }
    }
}
