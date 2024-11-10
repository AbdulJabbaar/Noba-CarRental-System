using Noba.CarRental.Domain.Entities;

namespace Noba.CarRental.Application.Persistence
{
    public interface ICarRepository : IRepository<Car>
    {
        Task<Car> GetCarByRegistrationNumberAsync(string registrationNumber);
    }
}
