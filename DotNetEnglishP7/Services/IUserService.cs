using Dot.Net.WebApi.Domain;

namespace DotNetEnglishP7.Services
{
    public interface IUserService
    {
        public Task<List<User>> GetAllAsync();
        public Task<User?> GetByIdAsync(int id);
        public Task<User?> AddAsync(User user);
        public Task<User?> UpdateAsync(User user);
        public Task<User?> DeleteAsync(int id);
        public Task<bool> ExistAsync(int id);
    }
}
