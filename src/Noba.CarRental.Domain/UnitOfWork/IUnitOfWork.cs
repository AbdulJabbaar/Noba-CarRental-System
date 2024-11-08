using Noba.CarRental.Domain.Entities;
using Noba.CarRental.Domain.Repositories;

namespace Noba.CarRental.Domain.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<CarCategory> CarCategoryRepository { get; }
        ICarRepository CarRepository { get; }
        IRentalRepository RentalRepository { get; }
        Task CommitAsync();
        Task RollbackAsync();
    }
}
