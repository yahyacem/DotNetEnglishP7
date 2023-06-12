using AutoMapper;
using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using DotNetEnglishP7.Mappers;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DotNetEnglishP7.Repositories
{
    public class TradeRepository : ITradeRepository
    {
        private static IMapper _mapper = MappingProfile.InitializeAutoMapper().CreateMapper();
        private LocalDbContext DbContext;
        public TradeRepository(LocalDbContext _dbContext)
        {
            DbContext = _dbContext;
        }
        public async Task<Trade?> AddAsync(Trade trade)
        {
            if (trade != null)
            {
                await DbContext.Trades.AddAsync(trade);
                await DbContext.SaveChangesAsync();
            }
            return trade;
        }
        public async Task<Trade?> DeleteAsync(Trade trade)
        {
            if (trade != null)
            {
                DbContext.Trades.Remove(trade);
                await DbContext.SaveChangesAsync();
            }
            return trade;
        }
        public async Task<bool> ExistAsync(int id)
        {
            return await DbContext.Trades.AnyAsync(x => x.Id == id);
        }
        public async Task<List<Trade>> GetAllAsync()
        {
            return await DbContext.Trades.ToListAsync();
        }
        public async Task<Trade?> GetByIdAsync(int? id)
        {
            return await DbContext.Trades.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Trade?> UpdateAsync(Trade trade)
        {
            Trade? tradeToUpdate = await GetByIdAsync(trade.Id);
            if (tradeToUpdate == null)
                return null;

            _mapper.Map(trade, tradeToUpdate);
            DbContext.Trades.Update(tradeToUpdate);
            await DbContext.SaveChangesAsync();

            return tradeToUpdate;
        }
    }
}