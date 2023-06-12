using AutoMapper;
using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using DotNetEnglishP7.Mappers;
using Microsoft.EntityFrameworkCore;

namespace DotNetEnglishP7.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private static IMapper _mapper = MappingProfile.InitializeAutoMapper().CreateMapper();
        private LocalDbContext DbContext;
        public RatingRepository(LocalDbContext _dbContext)
        {
            DbContext = _dbContext;
        }
        public async Task<Rating?> AddAsync(Rating rating)
        {
            if (rating != null)
            {
                await DbContext.Ratings.AddAsync(rating);
                await DbContext.SaveChangesAsync();
            }
            return rating;
        }
        public async Task<Rating?> DeleteAsync(Rating rating)
        {
            if (rating != null)
            {
                DbContext.Ratings.Remove(rating);
                await DbContext.SaveChangesAsync();
            }
            return rating;
        }
        public async Task<bool> ExistAsync(int id)
        {
            return await DbContext.Ratings.AnyAsync(x => x.Id == id);
        }
        public async Task<List<Rating>> GetAllAsync()
        {
            return await DbContext.Ratings.ToListAsync();
        }
        public async Task<Rating?> GetByIdAsync(int? id)
        {
            return await DbContext.Ratings.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Rating?> UpdateAsync(Rating rating)
        {
            Rating? ratingToUpdate = await GetByIdAsync(rating.Id);
            if (ratingToUpdate == null)
                return null;

            _mapper.Map(rating, ratingToUpdate);
            DbContext.Ratings.Update(ratingToUpdate);
            await DbContext.SaveChangesAsync();

            return ratingToUpdate;
        }
    }
}