using Application.Products.Commands.CreateProduct;
using Carter;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace API.Modules
{
    public class ProductModule : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("api/products", async (IMediator mediator, CreateProductCommand command) =>
            {
                return await mediator.Send(command);
            })
            .WithName(nameof(CreateProductCommand))
            .WithTags(nameof(Product))
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status201Created);
        }
    }
}
