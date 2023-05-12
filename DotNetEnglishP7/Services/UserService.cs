using Dot.Net.WebApi.Domain;
using DotNetEnglishP7.Repositories;

namespace DotNetEnglishP7.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User?> AddAsync(User user)
        {
            return await _userRepository.AddAsync(user);
        }
        public async Task<User?> DeleteAsync(int id)
        {
            User? userToDelete = await _userRepository.GetByIdAsync(id);
            if (userToDelete != null)
            {
                await _userRepository.DeleteAsync(userToDelete);
            }
            return userToDelete;
        }
        public async Task<List<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }
        public async Task<User?> GetByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }
        public async Task<User?> UpdateAsync(User user)
        {
            User? userToUpdate = await _userRepository.GetByIdAsync(user.Id);
            if (userToUpdate != null)
            {
                userToUpdate = user;
                await _userRepository.UpdateAsync(userToUpdate);
            }
            return userToUpdate;
        }
        public async Task<bool> ExistAsync(int id)
        {
            return await _userRepository.ExistAsync(id);
        }
    }
}
