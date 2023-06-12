using Dot.Net.WebApi.Data;
using System.Linq;
using Dot.Net.WebApi.Domain;
using System;
using System.Collections.ObjectModel;
using DotNetEnglishP7.Repositories;
using Microsoft.EntityFrameworkCore;
using DotNetEnglishP7.Domain;
using Microsoft.AspNetCore.Identity;
using DotNetEnglishP7.Identity;
using AutoMapper;
using DotNetEnglishP7.Mappers;

namespace Dot.Net.WebApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private static IMapper _mapper = MappingProfile.InitializeAutoMapper().CreateMapper();
        private UserManager<AppUser> _userManager { get; set; }
        public UserRepository(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IdentityResult?> AddAsync(RegisterUser user)
        {
            if (user == null)
            {
                return null;
            }

            AppUser userToAdd = _mapper.Map<AppUser>(user);
            return await _userManager.CreateAsync(userToAdd, user.Password);
        }
        public async Task<bool> UpdateAsync(RegisterUser user)
        {
            AppUser? userToUpdate = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == user.Id);
            if (userToUpdate == null)
            {
                return false;
            }
            
            userToUpdate = _mapper.Map<AppUser>(user);
            var result = await _userManager.DeleteAsync(userToUpdate);
            return result.Succeeded;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            AppUser? userToDelete = await _userManager.FindByIdAsync(id.ToString());
            if (userToDelete == null)
            {
                return false;   
            }

            var result = await _userManager.DeleteAsync(userToDelete);
            return result.Succeeded;
        }
        public async Task<bool> ExistAsync(int id)
        {
            return await _userManager.Users.AnyAsync(x => x.Id == id);
        }
        public async Task<List<User>> GetAllAsync()
        {
            List<User> users = new List<User>();
            List<AppUser> appUsers = await _userManager.Users.ToListAsync();
            foreach (var user in appUsers)
            {
                users.Add(_mapper.Map<User>(user));
            }
            return users;
        }
        public async Task<User?> GetByIdAsync(int id)
        {
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            return user != null ? _mapper.Map<User>(user) : null;
        }
    }
}