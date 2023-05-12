using Dot.Net.WebApi.Domain;

namespace DotNetEnglishP7.Services
{
    public interface IRatingService
    {
        public Task<List<Rating>> GetAllAsync();
        public Task<Rating?> GetByIdAsync(int id);
        public Task<Rating?> AddAsync(Rating rating);
        public Task<Rating?> UpdateAsync(Rating rating);
        public Task<Rating?> DeleteAsync(int id);
        public Task<bool> ExistAsync(int id);
    }
}
