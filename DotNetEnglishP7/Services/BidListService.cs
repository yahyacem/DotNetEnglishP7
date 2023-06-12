using Dot.Net.WebApi.Domain;
using DotNetEnglishP7.Repositories;

namespace DotNetEnglishP7.Services
{
    public class BidListService : IBidListService
    {
        private IBidListRepository _bidListRepository;
        public BidListService(IBidListRepository bidListRepository)
        {
            _bidListRepository = bidListRepository;
        }
        public async Task<BidList?> AddAsync(BidList curvePoint)
        {
            return await _bidListRepository.AddAsync(curvePoint);
        }
        public async Task<BidList?> DeleteAsync(int id)
        {
            BidList? bidListToDelete = await _bidListRepository.GetByIdAsync(id);
            if (bidListToDelete != null)
            {
                await _bidListRepository.DeleteAsync(bidListToDelete);
            }
            return bidListToDelete;
        }
        public async Task<List<BidList>> GetAllAsync()
        {
            return await _bidListRepository.GetAllAsync();
        }
        public async Task<BidList?> GetByIdAsync(int id)
        {
            return await _bidListRepository.GetByIdAsync(id);
        }
        public async Task<BidList?> UpdateAsync(BidList bidList)
        {
            if (bidList != null)
            {
                await _bidListRepository.UpdateAsync(bidList);
            }
            return bidList;
        }
        public async Task<bool> ExistAsync(int id)
        {
            return await _bidListRepository.ExistAsync(id);
        }
    }
}