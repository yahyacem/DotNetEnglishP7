using Dot.Net.WebApi.Domain;

namespace DotNetEnglishP7.Repositories
{
    public interface IUserRepository
    {
        Task<User?> FindByIdAsync(int id);
        Task<User?> FindByUserNameAsync(string userName);
        Task<User[]?> FindAllAsync();
        Task<User?> AddAsync(User user);
        Task<User?> UpdateAsync(User user);
        Task DeleteAsync(int id);
        Task<bool> ExistAsync(int id);
    }
}
