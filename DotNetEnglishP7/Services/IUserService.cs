using Dot.Net.WebApi.Domain;
using DotNetEnglishP7.Domain;
using DotNetEnglishP7.Identity;
using Microsoft.AspNetCore.Identity;

namespace DotNetEnglishP7.Services
{
    public interface IUserService
    {
        public Task<List<User>> GetAllAsync();
        public Task<User?> GetByIdAsync(int id);
        public Task<IdentityResult?> AddAsync(RegisterUser user);
        public Task<bool> UpdateAsync(RegisterUser user);
        public Task<bool> DeleteAsync(int id);
        public Task<bool> ExistAsync(int id);
    }
}
