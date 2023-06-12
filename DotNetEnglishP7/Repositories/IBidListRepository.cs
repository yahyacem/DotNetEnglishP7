using Dot.Net.WebApi.Domain;

namespace DotNetEnglishP7.Repositories
{
    public interface IBidListRepository
    {
        public Task<BidList?> GetByIdAsync(int? id);
        public Task<List<BidList>> GetAllAsync();
        public Task<BidList?> AddAsync(BidList bidList);
        public Task<BidList?> UpdateAsync(BidList bidList);
        public Task<BidList?> DeleteAsync(BidList bidList);
        public Task<bool> ExistAsync(int id);
    }
}
