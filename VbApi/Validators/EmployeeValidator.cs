using FluentValidation;
using VbApi.Controllers;

public class EmployeeValidator : AbstractValidator<Employee>
{
    public EmployeeValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name can not be empty.")
            .Length(3, 250).WithMessage("Name should have at least 3 characters.");

        RuleFor(x => x.DateOfBirth)
            .NotEmpty()
            .Must(BeAValidBirthDate).WithMessage("Birthdate is not valid.");

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Email address is not valid.");

        RuleFor(x => x.Phone)
            .Must(BeAValidPhoneNumber).WithMessage("Phone is not valid.");

        RuleFor(x => x.HourlySalary)
            .InclusiveBetween(50, 400).WithMessage("Hourly salary does not fall within valid range.");
    }

    private bool BeAValidBirthDate(DateTime date)
    {
        return date <= DateTime.Today.AddYears(-100) || date >= DateTime.Today.AddYears(-18);
    }

    private bool BeAValidPhoneNumber(string phone)
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
