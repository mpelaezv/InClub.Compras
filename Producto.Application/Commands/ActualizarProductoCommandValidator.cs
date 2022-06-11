using FluentValidation;

namespace Producto.Application.Commands
{
    public class ActualizarProductoCommandValidator : AbstractValidator<ActualizarProductoCommand>
    {
        public ActualizarProductoCommandValidator()
        {
            RuleFor(r => r.ID).NotEmpty().WithMessage("Se REQUIERE del ID para ejecutar la operacion.");
            RuleFor(r => r.ProdNombre).NotEmpty().WithMessage("Por favor, ingrese el Nombre del Producto.").MaximumLength(400).WithMessage("La longitud maxima del Nombre a ingresar es de 400.");
            RuleFor(r => r.ProdUM).NotEmpty().WithMessage("Por favor, ingrese la Unidad de Medida.");
            RuleFor(r => r.ProdPrecio).NotEmpty().WithMessage("Por favor, ingrese el Precio del Producto.").ScalePrecision(2, 7).WithMessage("Por favor, ingrese un Monto Valido. Ejm: 10000.50");
            RuleFor(r => r.AuditUsuarioUltimaModif).NotEmpty().WithMessage("Se REQUIERE del ID de Usuario para ejecutar la operacion.");
        }
    }
}
