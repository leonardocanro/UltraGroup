using Application.Contracts.Contracts;
using Application.Implementation.Class;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Implementation.init
{
    public static class Inicializador
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddScoped<IHotelServicio, HotelServicio>();
            services.AddScoped<IHabitacionServicio, HabitacionServicio>();
            services.AddScoped<IReservaServicio, ReservaServicio>();
        }
    }
}
