using Domain.Users;
using MediatR;

namespace Application.Users.Register
{
    public class RegisterCommand : IRequest<UserDTO>
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
        public required string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public required string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
