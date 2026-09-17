using Application.IServices;
using Domain.Users;
using Domain.VerificationCodes;
using FluentValidation;
using MediatR;

namespace Application.Users.SendEmailConfirmation
{
    public class SendEmailConfirmationHandler : IRequestHandler<SendEmailConfirmationCommand, Unit>
    {
        private readonly IUserRepository _userRepository;
        private readonly IVerificationCodeRepository _verificationCodeRepository;
        private readonly IEmailService _emailService;
        private readonly IValidator<SendEmailConfirmationCommand> _validator;

        public SendEmailConfirmationHandler(
            IUserRepository userRepository,
            IVerificationCodeRepository verificationCodeRepository,
            IEmailService emailService,
            IValidator<SendEmailConfirmationCommand> validator)
        {
            _userRepository = userRepository;
            _verificationCodeRepository = verificationCodeRepository;
            _emailService = emailService;
            _validator = validator;
        }

        public async Task<Unit> Handle(SendEmailConfirmationCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var user = await _userRepository.GetUserByNormalizedEmailAsync(request.Email.ToUpperInvariant());
            if (user != null && !user.EmailConfirmed)
            {
                await _verificationCodeRepository.InvalidatePendingCodesAsync(user.Id, "EmailConfirmation");

                var token = Guid.NewGuid().ToString("N");

                var verificationCode = new VerificationCode
                {
                    UserId = user.Id,
                    Code = token,
                    Purpose = "EmailConfirmation",
                    Channel = "Email",
                    ExpiresAt = DateTime.UtcNow.AddHours(24),
                    IsUsed = false,
                    CreatedDate = DateTime.UtcNow
                };

                await _verificationCodeRepository.AddVerificationCodeAsync(verificationCode);

                await _emailService.SendEmailConfirmationAsync(user.Email, user.FirstName, user.Id, token);
            }

            return Unit.Value;
        }
    }
}
