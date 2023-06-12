using Dot.Net.WebApi.Domain;

namespace DotNetEnglishP7.Repositories
{
    public interface ITradeRepository
    {
        public Task<Trade?> GetByIdAsync(int? id);
        public Task<List<Trade>> GetAllAsync();
        public Task<Trade?> AddAsync(Trade trade);
        public Task<Trade?> UpdateAsync(Trade trade);
        public Task<Trade?> DeleteAsync(Trade trade);
        public Task<bool> ExistAsync(int id);
    }
}
