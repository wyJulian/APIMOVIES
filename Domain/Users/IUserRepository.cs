namespace Domain.Users
{
    public interface IUserRepository
    {
        public Task<List<User>> GetAllUsersAsync();
        public Task<User?> GetUserByIdAsync(int id);
        public Task<User?> GetUserByNormalizedUserNameAsync(string normalizedUserName);
        public Task<User?> GetUserByNormalizedEmailAsync(string normalizedEmail);
        public Task<User> AddUserAsync(User user);
        public Task<User> UpdateUserAsync(User user);
        public Task<bool> DeleteUserAsync(int id);
    }
}
