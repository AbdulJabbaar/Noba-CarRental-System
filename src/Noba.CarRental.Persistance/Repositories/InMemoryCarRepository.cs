using Noba.CarRental.Domain.Entities;
using Noba.CarRental.Domain.Repositories;

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
