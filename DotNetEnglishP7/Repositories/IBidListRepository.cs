using Dot.Net.WebApi.Domain;

namespace DotNetEnglishP7.Repositories
{
    public interface IBidListRepository
    {
        public Task<BidList?> GetByIdAsync(int id);
        public Task<BidList[]?> GetAllAsync();
        public Task<BidList?> AddAsync(BidList bidList);
        public Task<BidList?> UpdateAsync(BidList bidList);
        public Task DeleteAsync(int id);
        public Task<bool> ExistAsync(int id);
    }
}
