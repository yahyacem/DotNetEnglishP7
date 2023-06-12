using Dot.Net.WebApi.Domain;
using DotNetEnglishP7.Repositories;

namespace DotNetEnglishP7.Services
{
    public class TradeService : ITradeService
    {
        private ITradeRepository _tradeRepository;
        public TradeService(ITradeRepository tradeRepository)
        {
            _tradeRepository = tradeRepository;
        }
        public async Task<Trade?> AddAsync(Trade trade)
        {
            return await _tradeRepository.AddAsync(trade);
        }
        public async Task<Trade?> DeleteAsync(int id)
        {
            Trade? tradeToDelete = await _tradeRepository.GetByIdAsync(id);
            if (tradeToDelete != null)
            {
                await _tradeRepository.DeleteAsync(tradeToDelete);
            }
            return tradeToDelete;
        }
        public async Task<List<Trade>> GetAllAsync()
        {
            return await _tradeRepository.GetAllAsync();
        }
        public async Task<Trade?> GetByIdAsync(int id)
        {
            return await _tradeRepository.GetByIdAsync(id);
        }
        public async Task<Trade?> UpdateAsync(Trade trade)
        {
            if (trade != null)
            {
                await _tradeRepository.UpdateAsync(trade);
            }
            return trade;
        }
        public async Task<bool> ExistAsync(int id)
        {
            return await _tradeRepository.ExistAsync(id);
        }
    }
}
