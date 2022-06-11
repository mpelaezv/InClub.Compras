using FluentValidation;
using OrdenCompra.Domain;

namespace OrdenCompra.Application.Commands
{
    public class CompraDetalleValidator : AbstractValidator<CompraDetalle>
    {
        public CompraDetalleValidator()
        {
            RuleFor(x => x.ProductoID).NotEmpty().WithMessage("Se REQUIERE del ID de Producto para ejecutar la operacion.");
            RuleFor(x => x.ItemCant).NotEmpty().WithMessage("Por favor, ingrese la cantidad de compra.");
            RuleFor(x => x.AuditUsuarioCreacion).NotEmpty().WithMessage("Se REQUIERE del ID de Usuario para ejecutar la operacion.");
        }
    }
}
