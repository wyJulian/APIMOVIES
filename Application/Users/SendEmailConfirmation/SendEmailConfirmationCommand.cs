using MediatR;

namespace Application.Users.SendEmailConfirmation
{
    public class SendEmailConfirmationCommand : IRequest<Unit>
    {
        public required string Email { get; set; }
    }
}
