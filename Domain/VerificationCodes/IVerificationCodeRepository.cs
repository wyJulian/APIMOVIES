using Domain.Users;

namespace Domain.VerificationCodes
{
    public interface IVerificationCodeRepository
    {
        public Task<VerificationCode> AddVerificationCodeAsync(VerificationCode verificationCode);
        public Task<VerificationCode?> GetActiveCodeAsync(int userId, string code, string purpose);
        public Task InvalidatePendingCodesAsync(int userId, string purpose);
        public Task<VerificationCode> MarkAsUsedAsync(VerificationCode verificationCode);
    }
}
