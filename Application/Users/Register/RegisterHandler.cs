using Application.IServices;
using Domain.Users;
using Domain.VerificationCodes;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Register
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, UserDTO>
    {
        private readonly IUserRepository _userRepository;
        private readonly IVerificationCodeRepository _verificationCodeRepository;
        private readonly IEmailService _emailService;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IValidator<RegisterCommand> _validator;

        public RegisterHandler(
            IUserRepository userRepository,
            IVerificationCodeRepository verificationCodeRepository,
            IEmailService emailService,
            IPasswordHasher<User> passwordHasher,
            IValidator<RegisterCommand> validator)
        {
            _userRepository = userRepository;
            _verificationCodeRepository = verificationCodeRepository;
            _emailService = emailService;
            _passwordHasher = passwordHasher;
            _validator = validator;
        }

        public async Task<UserDTO> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var user = new User
            {
                UserName = request.UserName,
                NormalizedUserName = request.UserName.ToUpperInvariant(),
                PasswordHash = string.Empty,
                Email = request.Email,
                NormalizedEmail = request.Email.ToUpperInvariant(),
                FirstName = request.FirstName,
                NormalizedFirstName = request.FirstName.ToUpperInvariant(),
                MiddleName = request.MiddleName,
                NormalizedMiddleName = request.MiddleName?.ToUpperInvariant(),
                LastName = request.LastName,
                NormalizedLastName = request.LastName.ToUpperInvariant(),
                BirthDate = request.BirthDate,
                CreatedDate = DateTime.UtcNow,
                EmailConfirmed = false,
                PhoneNumber = request.PhoneNumber,
                NormalizedPhoneNumber = request.PhoneNumber?.ToUpperInvariant(),
                PhoneConfirmed = false,
                TwoFactorEnabled = false
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

            var created = await _userRepository.AddUserAsync(user);

            var token = Guid.NewGuid().ToString("N");

            var verificationCode = new VerificationCode
            {
                UserId = created.Id,
                Code = token,
                Purpose = "EmailConfirmation",
                Channel = "Email",
                ExpiresAt = DateTime.UtcNow.AddHours(24),
                IsUsed = false,
                CreatedDate = DateTime.UtcNow
            };

            await _verificationCodeRepository.AddVerificationCodeAsync(verificationCode);

            await _emailService.SendEmailConfirmationAsync(created.Email, created.FirstName, created.Id, token);

            return new UserDTO
            {
                Id = created.Id,
                UserName = created.UserName,
                Email = created.Email,
                FirstName = created.FirstName,
                MiddleName = created.MiddleName,
                LastName = created.LastName,
                BirthDate = created.BirthDate,
                CreatedDate = created.CreatedDate,
                ModifiedDate = created.ModifiedDate,
                EmailConfirmed = created.EmailConfirmed,
                PhoneNumber = created.PhoneNumber,
                PhoneConfirmed = created.PhoneConfirmed,
                TwoFactorEnabled = created.TwoFactorEnabled,
                TwoFactorChannel = created.TwoFactorChannel
            };
        }
    }
}
