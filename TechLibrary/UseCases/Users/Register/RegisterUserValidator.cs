using FluentValidation;
using TechLibrary.Communication.Requests;

namespace TechLibrary.UseCases.Users.Register;

public class RegisterUserValidator : AbstractValidator<RequestUserJson>
{
    public RegisterUserValidator()
    {
        RuleFor(request => request.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(request => request.Email).EmailAddress().WithMessage("Email is not valid");
        RuleFor(request => request.Password).NotEmpty().WithMessage("Password is required");
        When(request => string.IsNullOrEmpty(request.Password) == false, () =>
        {
            RuleFor(request => request.Password.Length).GreaterThanOrEqualTo(6).WithMessage("Password must be at least 6 characters");
        });
    }
}