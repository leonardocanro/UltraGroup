using Application.Implementation.init;
using Infrastructure.Data.Implementation.init;
using Infrastructure.Transversal.Utilitarios.IoC;
namespace UltraGroup
{
    public static class ConfigureServiceExtension
    {
        public static void InitConfigurationAPI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfrastructureLayer();
            services.AddInfraestructure2();
            services.AddApplicationLayer();
        }  
    }
}   
