
using FluentValidation;
using Catalog.Application.DTOs.Employee;
 

namespace Catalog.Application.BussinesRules.Employee;

public class AddEmployeeValidation : AbstractValidator<EmployeeDTO?>
{
    public AddEmployeeValidation()
    {
        RuleFor(x => x.Name)
           .NotEmpty()
           .WithMessage("Employee Name is required.")
            .MinimumLength(5)
            .WithMessage("Employee Name shoul have at least 5 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Employee Last name is required.")
             .MinimumLength(5)
            .WithMessage("Employee Name shoul have at least 5 characters.");

    }
}


