using Application.Common.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    /// <summary>
    /// Clase estática iniciadora para agregar/registrar los servicios de la capa de infraestructura en el contenedor de inyección de dependencias (DI Container).
    /// </summary>
    public static class ConfigureServices
    {
        /// <summary>
        /// Método que registra el DbContext.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAppDbContext(this IServiceCollection services)
        {
            // Usamos una BD en memoría.
            services.AddDbContext<IAppDbContext, AppDbContext>(options => options.UseInMemoryDatabase("DBCleanArchitecture"));
            return services;
        }
    }
}

