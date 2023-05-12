using Dot.Net.WebApi.Domain;

namespace DotNetEnglishP7.Repositories
{
    public interface IUserRepository
    {
        public Task<User?> GetByIdAsync(int id);
        public Task<List<User>> GetAllAsync();
        public Task<User?> AddAsync(User user);
        public Task<User?> UpdateAsync(User user);
        public Task<User?> DeleteAsync(User user);
        public Task<bool> ExistAsync(int id);
    }
}
