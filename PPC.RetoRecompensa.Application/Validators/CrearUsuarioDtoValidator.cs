using FluentValidation;
using PPC.RetoRecompensa.Application.Contracts;

namespace PPC.RetoRecompensa.Application.Validators;

public class CrearUsuarioDtoValidator : AbstractValidator<CrearUsuarioDto>
{
    public CrearUsuarioDtoValidator()
    {
        RuleFor(x => x.Correo)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(100)
            .WithMessage("El correo no tiene un formato correcto");

        RuleFor(x => x.Nombre)
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("Debe proporcionar un nombre");
        RuleFor(x => x.Avatar)
            .NotEmpty()
            .MaximumLength(250)
            .WithMessage("Debe seleccionar un avatar");
    }
}