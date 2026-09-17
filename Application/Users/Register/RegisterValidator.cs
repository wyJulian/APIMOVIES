using Domain.Users;
using FluentValidation;

namespace Application.Users.Register
{
    public class RegisterValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterValidator(IUserRepository userRepository)
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .MaximumLength(30)
                .MustAsync(async (userName, cancellationToken) =>
                {
                    var existing = await userRepository.GetUserByNormalizedUserNameAsync(userName.ToUpperInvariant());
                    return existing == null;
                })
                .WithMessage(x => $"The username '{x.UserName}' is already taken.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(50)
                .MustAsync(async (email, cancellationToken) =>
                {
                    var existing = await userRepository.GetUserByNormalizedEmailAsync(email.ToUpperInvariant());
                    return existing == null;
                })
                .WithMessage(x => $"The email '{x.Email}' is already registered.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8)
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one number.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");

            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.MiddleName).MaximumLength(50);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);

            RuleFor(x => x.BirthDate)
                .Must(HaveMinimumAge)
                .WithMessage("You must be at least 14 years old to register.");

            RuleFor(x => x.PhoneNumber)
                .MaximumLength(20)
                .When(x => x.PhoneNumber != null);
        }

        private static bool HaveMinimumAge(DateTime birthDate)
        {
            var today = DateTime.UtcNow.Date;
            var age = today.Year - birthDate.Year;
            if (birthDate.Date > today.AddYears(-age))
            {
                age--;
            }

            return age >= 14;
        }
    }
}
