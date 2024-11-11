using Core.DTOs;
using FluentValidation;

namespace WebApi.Validators;

public class CustomerUpdateDTOValidator : AbstractValidator<CustomerDTO>
{
    public CustomerUpdateDTOValidator()
    {
        RuleFor(c => c.Id)
            .GreaterThan(0).WithMessage("El ID debe ser mayor a 0.");

        Include(new CustomerCreateDTOValidator());
    }
}