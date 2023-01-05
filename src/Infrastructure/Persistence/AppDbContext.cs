using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Infrastructure.Persistence
{
    /// <summary>
    /// Clase que representa el DbContext de la aplicación (Representa una sesión con la bd y posee todo el contexto de la misma).
    /// </summary>
    public class AppDbContext : DbContext, IAppDbContext
    {
        private readonly IPublisher _publisher;
        private readonly ILogger<AppDbContext> _logger;

        public AppDbContext(DbContextOptions<AppDbContext> options, IPublisher publisher, ILogger<AppDbContext> logger) : base(options)
        {
            _publisher = publisher;
            _logger = logger;

            _logger.LogDebug($"{nameof(AppDbContext)} created.");
        }

        public DbSet<Product> Products => Set<Product>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Aplicamos la configuración explicitamente.
            //builder.ApplyConfiguration(new ProductConfig());

            // Aplicamos la configuración a partir de la referencia del ensamblado donde se encuentran las clases de configuración.
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Deferred approach to raise and dispatch events
            // https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/domain-events-design-implementation

            int result = await base.SaveChangesAsync(cancellationToken);

            // Obtenemos los eventos que no han sido publicados por el Mediador de cada uno de los agregados.
            var events = ChangeTracker.Entries<IHasDomainEvent>()
               .Select(x => x.Entity.DomainEvents)
               .SelectMany(x => x)
               .Where(domainEvent => !domainEvent.IsPublished)
               .ToArray();

            // Publicamos/Despachamos los eventos a sus manejadores (EventHandlers) por medio de una notificación.
            foreach (var @event in events)
            {
                @event.IsPublished = true;

                _logger.LogInformation("New domain event {Event}", @event.GetType().Name);

                // Note: If an unhandled exception occurs, all the saved changes will be rolled back
                // by the TransactionBehavior. All the operations related to a domain event finish
                // successfully or none of them do.
                // Reference: https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/domain-events-design-implementation#what-is-a-domain-event
                await _publisher.Publish(@event);
            }

            return result;
        }
    }
}
