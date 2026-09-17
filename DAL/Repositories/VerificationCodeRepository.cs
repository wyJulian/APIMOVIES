using Domain.Users;
using Domain.VerificationCodes;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class VerificationCodeRepository : IVerificationCodeRepository
    {
        private readonly ApplicationDbContext _context;

        public VerificationCodeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<VerificationCode> AddVerificationCodeAsync(VerificationCode verificationCode)
        {
            var result = await _context.VerificationCodes.AddAsync(verificationCode);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<VerificationCode?> GetActiveCodeAsync(int userId, string code, string purpose) =>
            await _context.VerificationCodes.FirstOrDefaultAsync(v =>
                v.UserId == userId &&
                v.Code == code &&
                v.Purpose == purpose &&
                !v.IsUsed &&
                v.ExpiresAt > DateTime.UtcNow);

        public async Task InvalidatePendingCodesAsync(int userId, string purpose)
        {
            var pendingCodes = await _context.VerificationCodes
                .Where(v => v.UserId == userId && v.Purpose == purpose && !v.IsUsed)
                .ToListAsync();

            foreach (var pendingCode in pendingCodes)
            {
                pendingCode.IsUsed = true;
            }

            await _context.SaveChangesAsync();
        }

        public async Task<VerificationCode> MarkAsUsedAsync(VerificationCode verificationCode)
        {
            verificationCode.IsUsed = true;
            _context.VerificationCodes.Update(verificationCode);
            await _context.SaveChangesAsync();
            return verificationCode;
        }
    }
}
