using Dot.Net.WebApi.Domain;

namespace DotNetEnglishP7.Services
{
    public interface IBidListService
    {
        public Task<List<BidList>> GetAllAsync();
        public Task<BidList?> GetByIdAsync(int id);
        public Task<BidList?> AddAsync(BidList bidList);
        public Task<BidList?> UpdateAsync(BidList bidList);
        public Task<BidList?> DeleteAsync(int id);
        public Task<bool> ExistAsync(int id);
    }
}