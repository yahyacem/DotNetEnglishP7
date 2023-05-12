using Dot.Net.WebApi.Domain;

namespace DotNetEnglishP7.Services
{
    public interface ITradeService
    {
        public Task<List<Trade>> GetAllAsync();
        public Task<Trade?> GetByIdAsync(int id);
        public Task<Trade?> AddAsync(Trade trade);
        public Task<Trade?> UpdateAsync(Trade trade);
        public Task<Trade?> DeleteAsync(int id);
        public Task<bool> ExistAsync(int id);
    }
}
