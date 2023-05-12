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
        private LocalDbContext DbContext;
        public UserRepository(LocalDbContext _dbContext)
        {
            DbContext = _dbContext;
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
        public async Task<User?> DeleteAsync(User user)
        {
            if (user != null)
            {
                DbContext.Users.Remove(user);
                await DbContext.SaveChangesAsync();
            }
            return user;
        }
        public async Task<bool> ExistAsync(int id)
        {
            return await DbContext.Users.AnyAsync(x => x.Id == id);
        }
        public async Task<List<User>> GetAllAsync()
        {
            return await DbContext.Users.ToListAsync();
        }
        public async Task<User?> GetByIdAsync(int id)
        {
            return await DbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<User?> UpdateAsync(User user)
        {
            if (user != null)
            {
                User? existing = DbContext.Users.Local.SingleOrDefault(x => x.Id == user.Id);
                if (existing != null)
                    DbContext.Entry(existing).State = EntityState.Detached;

                DbContext.Users.Update(user);
                await DbContext.SaveChangesAsync();
            }
            return user;
        }
    }
}