using FluentValidation;
using Core.DTOs;
using Core.Entities;

namespace WebApi.Validators;

public class CustomerCreateDTOValidator : AbstractValidator<CustomerDTO>
{
    public CustomerCreateDTOValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("El nombre es obligatorio.")
            .MaximumLength(100).WithMessage("El nombre no puede tener más de 100 caracteres.");

        RuleFor(c => c.Address)
            .NotEmpty().WithMessage("La dirección es obligatoria.")
            .MaximumLength(200).WithMessage("La dirección no puede tener más de 200 caracteres.");

        RuleFor(c => c.City)
            .NotEmpty().WithMessage("La ciudad es obligatoria.");

        RuleFor(c => c.Email)
            .EmailAddress().WithMessage("El email no es válido.")
            .When(c => !string.IsNullOrEmpty(c.Email)); // Solo si el email es proporcionado

        RuleFor(c => c.Phone)
            .NotEmpty().WithMessage("El teléfono es obligatorio.")
            .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("El teléfono no es válido.");

        RuleFor(c => c.BirthDate)
            .LessThan(DateTime.Now).WithMessage("La fecha de nacimiento debe ser anterior a la fecha actual.");
    }
}
