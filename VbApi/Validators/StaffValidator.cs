using FluentValidation;
using VbApi.Controllers;

public class StaffValidator : AbstractValidator<Staff>
{
    public StaffValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name can not be empty.")
            .Length(3, 250).WithMessage("Name should have at least 3 characters.");

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Email address is not valid.");

        RuleFor(x => x.Phone)
            .Must(BeAValidPhoneNumber).WithMessage("Phone is not valid.");

        RuleFor(x => x.HourlySalary)
            .InclusiveBetween(30, 400).WithMessage("Hourly salary does not fall within valid range.");
    }

    private bool BeAValidPhoneNumber(string? phone)
    {
        if (string.IsNullOrEmpty(phone))
        {
            return false;
        }
        if (phone.Length < 7 || phone.Length > 15)
        {
            return false;
        }
        return phone.All(char.IsDigit);
    }
}