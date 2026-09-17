using MediatR;

namespace Application.Users.ConfirmEmail
{
    public class ConfirmEmailCommand : IRequest<bool>
    {
        public required int UserId { get; set; }
        public required string Token { get; set; }
    }
}
