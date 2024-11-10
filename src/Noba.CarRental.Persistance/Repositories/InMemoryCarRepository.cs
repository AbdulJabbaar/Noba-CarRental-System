using Noba.CarRental.Application.Persistence;
using Noba.CarRental.Domain.Entities;

namespace Noba.CarRental.Persistance.Repositories
{
    public class InMemoryCarRepository : InMemoryRepository<Car>, ICarRepository
    {
        public Task<Car> GetCarByRegistrationNumberAsync(string registrationNumber)
        {
            var car = _entities.FirstOrDefault(c => c.RegistrationNumber == registrationNumber);
            return Task.FromResult(car);
        }
    }
}
