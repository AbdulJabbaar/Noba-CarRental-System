using Noba.CarRental.Domain.Entities;
using Noba.CarRental.Domain.Repositories;

namespace Noba.CarRental.Persistance.Repositories
{
    public class InMemoryRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly List<T> _entities = new();

        public Task<T> GetByIdAsync(Guid id)
        {
            var entity = _entities.FirstOrDefault(e => e.Id == id);
            return Task.FromResult(entity);
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<T>>(_entities);
        }

        public Task AddAsync(T entity)
        {
            _entities.Add(entity);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(T entity)
        {
            var existingEntity = _entities.FirstOrDefault(e => e.Id == entity.Id);
            if (existingEntity != null)
            {
                _entities.Remove(existingEntity);
                _entities.Add(entity);
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id)
        {
            var entity = _entities.FirstOrDefault(e => e.Id == id);
            if (entity != null)
            {
                _entities.Remove(entity);
            }
            return Task.CompletedTask;
        }
    }
}
