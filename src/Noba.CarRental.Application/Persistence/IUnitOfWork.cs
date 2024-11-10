using Noba.CarRental.Domain.Entities.CarCategory;

namespace Noba.CarRental.Application.Persistence
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
