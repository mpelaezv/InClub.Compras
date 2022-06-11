using FluentValidation;

namespace OrdenCompra.Application.Commands
{
    public class CrearOrdenCompraCommandValidator : AbstractValidator<CrearOrdenCompraCommand>
    {
        public CrearOrdenCompraCommandValidator()
        {
            RuleFor(x => x.CompraUsuarioID).NotEmpty().WithMessage("Se REQUIERE del ID de Usuario de Compra para ejecutar la operacion.");
            RuleFor(x => x.AuditUsuarioCreacion).NotEmpty().WithMessage("Se REQUIERE del ID de Usuario para ejecutar la operacion.");
            RuleForEach(x => x.CompraDetalles).SetValidator(new CompraDetalleValidator());
        }
    }
}
