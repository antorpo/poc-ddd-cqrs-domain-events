using FluentValidation;

namespace Application.Products.Commands.CreateProduct
{
    /// <summary>
    /// Clase que valida el comando creación de producto.
    /// </summary>
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(r => r.Name)
                .NotEmpty();

            RuleFor(r => r.Description)
                .NotEmpty();

            RuleFor(r => r.Price)
                .NotEmpty();
        }
    }
}
