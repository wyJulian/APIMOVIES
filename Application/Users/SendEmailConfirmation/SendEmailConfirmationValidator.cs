using FluentValidation;

namespace Application.Users.SendEmailConfirmation
{
    public class SendEmailConfirmationValidator : AbstractValidator<SendEmailConfirmationCommand>
    {
        public SendEmailConfirmationValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
