using Dot.Net.WebApi.Data;
using System.Linq;
using Dot.Net.WebApi.Domain;
using System;
using System.Collections.ObjectModel;
using DotNetEnglishP7.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Dot.Net.WebApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        public LocalDbContext DbContext { get; }

        public UserRepository(LocalDbContext dbContext)
        {
            DbContext = dbContext;
        }
        public async Task<User?> FindByIdAsync(int id)
        {
            return await DbContext.Users.Where(user => user.Id == id)
                                  .FirstOrDefaultAsync();
        }
        public async Task<User?> FindByUserNameAsync(string userName)
        {
            return await DbContext.Users.Where(user => user.UserName == userName)
                                  .FirstOrDefaultAsync();
        }
        public async Task<User[]?> FindAllAsync()
        {
            return await DbContext.Users.ToArrayAsync();
        }
        public async Task<User?> AddAsync(User user)
        {
            if (user != null)
            {
                await DbContext.Users.AddAsync(user);
                await DbContext.SaveChangesAsync();
            }
            return user;
        }
        public async Task<User?> UpdateAsync(User user)
        {
            if (user != null)
            {
                DbContext.Users.Update(user);
                await DbContext.SaveChangesAsync();
            }
            return user;
        }
        public async Task DeleteAsync(int id)
        {
            User? userToDelete = await DbContext.Users.FirstAsync(x => x.Id == id);
            if (userToDelete != null)
            {
                DbContext.Users.Remove(userToDelete);
                await DbContext.SaveChangesAsync();
            }
        }
        public async Task<bool> ExistAsync(int id)
        {
            return await DbContext.Users.AnyAsync(x => x.Id == id);
        }
    }
}