using Dot.Net.WebApi.Domain;

namespace DotNetEnglishP7.Repositories
{
    public interface IRatingRepository
    {
        public Task<Rating?> GetByIdAsync(int? id);
        public Task<List<Rating>> GetAllAsync();
        public Task<Rating?> AddAsync(Rating rating);
        public Task<Rating?> UpdateAsync(Rating rating);
        public Task<Rating?> DeleteAsync(Rating rating);
        public Task<bool> ExistAsync(int id);
    }
}
