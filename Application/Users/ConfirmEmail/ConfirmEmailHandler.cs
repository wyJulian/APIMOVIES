using Domain.Users;
using Domain.VerificationCodes;
using FluentValidation;
using MediatR;

namespace Application.Users.ConfirmEmail
{
    public class ConfirmEmailHandler : IRequestHandler<ConfirmEmailCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IVerificationCodeRepository _verificationCodeRepository;
        private readonly IValidator<ConfirmEmailCommand> _validator;

        public ConfirmEmailHandler(
            IUserRepository userRepository,
            IVerificationCodeRepository verificationCodeRepository,
            IValidator<ConfirmEmailCommand> validator)
        {
            _userRepository = userRepository;
            _verificationCodeRepository = verificationCodeRepository;
            _validator = validator;
        }

        public async Task<bool> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var user = await _userRepository.GetUserByIdAsync(request.UserId);
            if (user == null)
            {
                return false;
            }

            var verificationCode = await _verificationCodeRepository.GetActiveCodeAsync(request.UserId, request.Token, "EmailConfirmation");
            if (verificationCode == null)
            {
                return false;
            }

            await _verificationCodeRepository.MarkAsUsedAsync(verificationCode);

            user.EmailConfirmed = true;
            user.ModifiedDate = DateTime.UtcNow;
            await _userRepository.UpdateUserAsync(user);

            return true;
        }
    }
}
