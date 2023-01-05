namespace Domain.Events
{
    /// <summary>
    /// Clase para el evento de dominio ProductCreatedEvent.
    /// </summary>
    public class ProductCreatedEvent : DomainEvent
    {
        public ProductCreatedEvent(Product product)
        {
            // Información de este evento: Producto creado.
            Product = product;
        }

        /// <summary>
        /// Producto que se creo.
        /// </summary>
        public Product Product { get; }
    }
}
