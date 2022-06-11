using FluentValidation;

namespace Producto.Application.Commands
{
    public class EliminarProductoCommandValidator : AbstractValidator<EliminarProductoCommand>
    {
        public EliminarProductoCommandValidator()
        {
            RuleFor(r => r.ProductoID).NotEmpty().WithMessage("Se REQUIERE del ID de Producto para ejecutar la operacion.");
        }
    }
}
