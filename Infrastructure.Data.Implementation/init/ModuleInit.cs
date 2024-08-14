using Domain.Contracts.Contracts;
using Infrastructure.Data.Core.Context;
using Infrastructure.Data.Core.Contracts;
using Infrastructure.Data.Implementation.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Data.Implementation.init
{
    public static class ModuleInit 
    {
        public static void AddInfraestructure2(this IServiceCollection services) 
        {
            services.AddScoped<IContextoUnidadDeTrabajo, ContextoPrincipal>();
            services.AddScoped<IHotelRepositorio,HotelRepositorio>();
            services.AddScoped<IHabitacionRepositorio, HabitacionRepositorio>();
            services.AddScoped<IReservaRepositorio, ReservaRepositorio>();
        }
    }
}
