using FluentValidation;

namespace Usuario.Application.Commands
{
    public class EliminarUsuarioCommandValidator : AbstractValidator<EliminarUsuarioCommand>
    {
        public EliminarUsuarioCommandValidator()
        {
            RuleFor(r => r.UsuarioID).NotEmpty().WithMessage("Se REQUIERE del ID de Usuario para ejecutar la operacion.");
        }
    }
}
