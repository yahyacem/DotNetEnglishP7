using Dot.Net.WebApi.Domain;
using DotNetEnglishP7.Repositories;

namespace DotNetEnglishP7.Services
{
    public class RatingService : IRatingService
    {
        private IRatingRepository _ratingRepository;
        public RatingService(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }
        public async Task<Rating?> AddAsync(Rating rating)
        {
            return await _ratingRepository.AddAsync(rating);
        }
        public async Task<Rating?> DeleteAsync(int id)
        {
            Rating? ratingToDelete = await _ratingRepository.GetByIdAsync(id);
            if (ratingToDelete != null)
            {
                await _ratingRepository.DeleteAsync(ratingToDelete);
            }
            return ratingToDelete;
        }
        public async Task<List<Rating>> GetAllAsync()
        {
            return await _ratingRepository.GetAllAsync();
        }
        public async Task<Rating?> GetByIdAsync(int id)
        {
            return await _ratingRepository.GetByIdAsync(id);
        }
        public async Task<Rating?> UpdateAsync(Rating rating)
        {
            Rating? ratingToUpdate = await _ratingRepository.GetByIdAsync(rating.Id);
            if (ratingToUpdate != null)
            {
                ratingToUpdate = rating;
                await _ratingRepository.UpdateAsync(ratingToUpdate);
            }
            return ratingToUpdate;
        }
        public async Task<bool> ExistAsync(int id)
        {
            return await _ratingRepository.ExistAsync(id);
        }
    }
}
