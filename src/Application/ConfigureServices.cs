using Application.Common.Behaviours;
using Application.Common.Interfaces;
using Application.Services;
using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

/*
 
IMPORTANTE: 
    Este archivo se crea para registrar los servicios de la capa de aplicación. 
    
    No podemos usar el mismo archivo que la capa de infraestructura ya que tenemos dependencias de la capa de aplicación las cuales
    para ser registradas no admiten el uso de tipos (TService e TImplementation) sino que requieren la referencia al ensamblado.

    Para hacerlo desde la capa de infraestructura deberiamos tener como referenciar el ensamblado de la capa de aplicación (es decir el [.dll, .exe] en ejecución [runtime]).
    Ejm: 
        services.AddMediatR(typeof(Application)); 
        (Donde {Application} es una clase dentro del ensamblado de la capa de aplicación).
    
    En este caso decidimos separarlo para evitar tener clases vacias solo para el escaneo de ensamblados y poder registrar correctamente las dependencias usadas en esta capa.
    
        services.AddMediatR(Assembly.GetExecutingAssembly()); 
        (Donde {Assembly.GetExecutingAssembly()} es el ensamblado actual que contiene el codigo que se esta ejecutando).

*/

namespace Application
{
    /// <summary>
    /// Clase estática iniciadora para agregar/registrar los servicios de la capa de aplicación en el contenedor de inyección de dependencias (DI Container).
    /// </summary>
    public static class ConfigureServices
    {
        /// <summary>
        /// Método que registra los validadores de FluentValidation.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            // Opcion 1: Usando la referencia del ensamblado que se esta ejecutando donde se definieron los validadores.
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // Opcion 2: Usando la referencia del ensamblado de la clase que contiene el validador.
            // services.AddValidatorsFromAssemblyContaining(typeof(CreateProductCommandValidator));

            // Usando los tipos (TService e TImplementation) de los validadores.
            // services.AddScoped<IValidator<CreateProductCommand>, CreateProductCommandValidator>();

            return services;
        }

        /// <summary>
        /// Metodo que registra el mediador y sus comportamientos.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMediator(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IRequestPreProcessor<>), typeof(LoggingBehaviour<>));
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IEmailService, EmailService>();
            return services;
        }
    }
}

