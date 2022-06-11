using FluentValidation;

namespace Usuario.Application.Commands
{
    public class CrearUsuarioCommandValidator : AbstractValidator<CrearUsuarioCommand>
    {
        public CrearUsuarioCommandValidator()
        {
            RuleFor(r => r.UsCorreo).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Por favor, ingrese su correo electronico.").EmailAddress().WithMessage("Por favor, ingrese un correo electronico valido.").MaximumLength(140).WithMessage("La longitud maxima del Correo Eletronico a ingresar es de 140.");
            RuleFor(r => r.UsPass).NotEmpty().WithMessage("Por favor, ingrese una contraseña.");
            RuleFor(r => r.UsNombre).NotEmpty().WithMessage("Por favor, ingrese su Nombre.").MaximumLength(200).WithMessage("La longitud maxima del Nombre a ingresar es de 200.");
            RuleFor(r => r.UsApellidos).NotEmpty().WithMessage("Por favor, ingrese sus Apellidos.").MaximumLength(200).WithMessage("La longitud maxima de los Apellidos a ingresar es de 200.");
        }
    }
}
