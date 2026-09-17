using FluentValidation;

namespace Application.Users.ConfirmEmail
{
    public class ConfirmEmailValidator : AbstractValidator<ConfirmEmailCommand>
    {
        public ConfirmEmailValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0).WithMessage("User ID must be greater than 0.");
            RuleFor(x => x.Token).NotEmpty();
        }
    }
}
