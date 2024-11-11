using Core.DTOs;
using Core.Entities;
using FluentValidation;

namespace WebApi.Validators;

public class CustomerValidator: AbstractValidator<Customer>
{
    protected bool BeAValidAge(DateTime date)
    {
        int currentYear = DateTime.Now.Year;
        int dobYear = date.Year;

        if (dobYear <= currentYear && dobYear > (currentYear - 120))
        {
            return true;
        }

        return false;
    }
    public CustomerValidator()
    {
        RuleFor(customer => customer.Name).NotNull().NotEmpty().MaximumLength(50).WithMessage("El texto solo debe tener como máximo 50 caracteres");
        RuleFor(customer => customer.Address).MaximumLength(100).WithMessage("El texto solo debe tener como máximo 100 caracteres");
        RuleFor(customer => customer.City).MaximumLength(50).WithMessage("El texto solo debe tener como máximo 50 caracteres"); ;
        RuleFor(customer => customer.Email).EmailAddress().WithMessage("Email inválido"); ;
        RuleFor(customer => customer.Phone).MaximumLength(15).WithMessage("El texto solo debe tener como máximo 15 caracteres"); ;
        RuleFor(customer => customer.BirthDate).Must(BeAValidAge).WithMessage("Invalid {PropertyName}");
    }
}
