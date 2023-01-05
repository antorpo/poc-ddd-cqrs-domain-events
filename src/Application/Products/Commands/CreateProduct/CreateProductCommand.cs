using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Products.Commands.CreateProduct
{
    /// <summary>
    /// Registro para el comando creación de producto.
    /// </summary>
    public record CreateProductCommand : IRequest<int>
    {
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public double Price { get; init; }
    }

    /// <summary>
    /// Clase que maneja el comando creación de producto.
    /// </summary>
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IAppDbContext _context;

        public CreateProductCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        // https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/microservice-application-layer-implementation-web-api
        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var newProduct = new Product(0, request.Name, request.Description, request.Price);

            _context.Products.Add(newProduct);

            await _context.SaveChangesAsync(cancellationToken);

            return newProduct.ProductId;
        }
    }
}
