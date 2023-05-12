using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace DotNetEnglishP7.Repositories
{
    public class TradeRepository : ITradeRepository
    {
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
            return await DbContext.Trades.AnyAsync(x => x.TradeId == id);
        }
        public async Task<List<Trade>> GetAllAsync()
        {
            return await DbContext.Trades.ToListAsync();
        }
        public async Task<Trade?> GetByIdAsync(int id)
        {
            return await DbContext.Trades.FirstOrDefaultAsync(x => x.TradeId == id);
        }
        public async Task<Trade?> UpdateAsync(Trade trade)
        {
            if (trade != null)
            {
                Trade? existing = DbContext.Trades.Local.SingleOrDefault(x => x.TradeId == trade.TradeId);
                if (existing != null)
                    DbContext.Entry(existing).State = EntityState.Detached;

                DbContext.Trades.Update(trade);
                await DbContext.SaveChangesAsync();
            }
            return trade;
        }
    }
}