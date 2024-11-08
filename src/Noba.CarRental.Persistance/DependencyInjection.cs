using Microsoft.Extensions.DependencyInjection;
using Noba.CarRental.Domain.Entities;
using Noba.CarRental.Domain.Repositories;
using Noba.CarRental.Domain.UnitOfWork;
using Noba.CarRental.Persistance.Repositories;
using Noba.CarRental.Persistance.UnitOfWork;

namespace Noba.CarRental.Persistance
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistanceLayer(this IServiceCollection services) 
        {
            services.AddSingleton<IRepository<CarCategory>, InMemoryRepository<CarCategory>>();
            services.AddSingleton<ICarRepository, InMemoryCarRepository>();
            services.AddSingleton<IRentalRepository, InMemoryRentalRepository>();
            services.AddSingleton<IUnitOfWork, InMemoryUnitOfWork>();

            return services;
        }
    }
}
