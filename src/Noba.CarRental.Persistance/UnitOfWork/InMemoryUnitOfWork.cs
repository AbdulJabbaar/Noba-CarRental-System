using Noba.CarRental.Domain.Entities;
using Noba.CarRental.Domain.Repositories;
using Noba.CarRental.Domain.UnitOfWork;

namespace Noba.CarRental.Persistance.UnitOfWork
{
    public class InMemoryUnitOfWork : IUnitOfWork
    {
        public IRepository<CarCategory> CarCategoryRepository { get; }
        public ICarRepository CarRepository { get; }
        public IRentalRepository RentalRepository { get; }

        public InMemoryUnitOfWork(IRepository<CarCategory> carCategoryRepository, ICarRepository carRepository, IRentalRepository rentalRepository)
        {
            CarCategoryRepository = carCategoryRepository;
            CarRepository = carRepository;
            RentalRepository = rentalRepository;
        }

        public Task CommitAsync()
        {
            return Task.CompletedTask;
        }

        public Task RollbackAsync()
        {
            return Task.CompletedTask;
        }
    }
}
