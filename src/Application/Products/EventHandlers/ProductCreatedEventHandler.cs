using Application.Common.Interfaces;
using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Products.EventHandlers
{
    /// <summary>
    /// Clase que contiene la logica de negocio para manejar el evento de dominio ProductCreatedEvent.
    /// </summary>
    public class ProductCreatedEventHandler : INotificationHandler<ProductCreatedEvent>
    {
        private readonly ILogger<ProductCreatedEventHandler> _logger;
        private readonly IEmailService _emailService;

        public ProductCreatedEventHandler(
            ILogger<ProductCreatedEventHandler> logger,
            IEmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }

        public async Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Nueva notificación: Nuevo producto {Product}", notification.Product);

            await _emailService.SendEmail("test@email.com", "Nuevo producto", $"Nuevo producto {notification.Product}");

            await Task.Delay(1000);
        }
    }

}
