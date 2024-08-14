
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Infrastructure.Transversal.Utilitarios.IoC
{
    public static class MapperInitializer
    {
        public static void AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }   
    }

}
