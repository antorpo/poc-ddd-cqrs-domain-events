namespace Domain.Entities
{
    /// <summary>
    /// Aggregate Root (Product) - Clase para el producto.
    /// </summary>
    public class Product : IHasDomainEvent
    {
        public Product(int productId, string name, string description, double price)
        {
            ProductId = productId;
            Name = name;
            Description = description;
            Price = price;


            // Obligamos a que esta entidad tenga este evento desde su creación.
            DomainEvents.Add(new ProductCreatedEvent(this));
        }

        public int ProductId { get; set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public double Price { get; private set; }

        /// <summary>
        /// Lista de eventos de dominio.
        /// </summary>
        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}
