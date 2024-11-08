using Noba.CarRental.Domain.Entities;

namespace Noba.CarRental.Domain.Repositories
{
    public interface ICarRepository: IRepository<Car>
    {
        Task<Car> GetCarByRegistrationNumberAsync(string registrationNumber);
    }
}
