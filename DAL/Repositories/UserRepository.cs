using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> AddUserAsync(User user)
        {
            var result = await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await GetUserByIdAsync(id);
            if (user == null)
            {
                return false;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<User>> GetAllUsersAsync() => await _context.Users.ToListAsync();

        public async Task<User?> GetUserByIdAsync(int id) => await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

        public async Task<User?> GetUserByNormalizedUserNameAsync(string normalizedUserName) =>
            await _context.Users.FirstOrDefaultAsync(u => u.NormalizedUserName == normalizedUserName);

        public async Task<User?> GetUserByNormalizedEmailAsync(string normalizedEmail) =>
            await _context.Users.FirstOrDefaultAsync(u => u.NormalizedEmail == normalizedEmail);

        public async Task<User> UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
