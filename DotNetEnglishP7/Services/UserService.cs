using Dot.Net.WebApi.Domain;
using DotNetEnglishP7.Domain;
using DotNetEnglishP7.Identity;
using DotNetEnglishP7.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace DotNetEnglishP7.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            return await _userRepository.DeleteAsync(id);
        }
        public async Task<List<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }
        public async Task<User?> GetByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }
        public async Task<bool> UpdateAsync(RegisterUser user)
        {
            if (user == null)
            {
                return false;
            }

            return await _userRepository.UpdateAsync(user);
        }
        [AllowAnonymous]
        public async Task<IdentityResult?> AddAsync(RegisterUser user)
        {
            if (user == null)
            {
                return null;
            }
            return await _userRepository.AddAsync(user);
        }
        public async Task<bool> ExistAsync(int id)
        {
            return await _userRepository.ExistAsync(id);
        }
    }
}
