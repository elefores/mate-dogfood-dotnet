using FluentValidation;

namespace MateDogfood;

public class CustomerValidator : AbstractValidator<Customer>
{
    public CustomerValidator()
    {
        // `CascadeMode.StopOnFirstFailure` was removed in FluentValidation 11 and
        // replaced by `CascadeMode.Stop`. Bumping the package forces this line to change.
        CascadeMode = CascadeMode.StopOnFirstFailure;

        RuleFor(customer => customer.Name).NotEmpty().MaximumLength(100);
        RuleFor(customer => customer.Email).NotEmpty().EmailAddress();
        RuleFor(customer => customer.Age).InclusiveBetween(18, 120);
    }
}
