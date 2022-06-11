using FluentValidation;

namespace Producto.Application.Commands
{
    public class CrearProductoCommandValidator : AbstractValidator<CrearProductoCommand>
    {
        public CrearProductoCommandValidator()
        {
            RuleFor(r => r.ProdNombre).NotEmpty().WithMessage("Por favor, ingrese el Nombre del Producto.").MaximumLength(400).WithMessage("La longitud maxima del Nombre a ingresar es de 400.");
            RuleFor(r => r.ProdUM).NotEmpty().WithMessage("Por favor, ingrese la Unidad de Medida.");
            RuleFor(r => r.ProdPrecio).NotEmpty().WithMessage("Por favor, ingrese el Precio del Producto.").ScalePrecision(2,7).WithMessage("Por favor, ingrese un Monto Valido. Ejm: 10000.50");
            RuleFor(r => r.AuditUsuarioCreacion).NotEmpty().WithMessage("Se REQUIERE del ID de Usuario para ejecutar la operacion.");
        }
    }
}
