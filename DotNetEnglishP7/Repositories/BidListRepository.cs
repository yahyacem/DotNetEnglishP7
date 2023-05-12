using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace DotNetEnglishP7.Repositories
{
    public class BidListRepository : IBidListRepository
    {
        public LocalDbContext DbContext { get; }
        public BidListRepository(LocalDbContext dbContext)
        {
            DbContext = dbContext;
        }
        public async Task<BidList?> AddAsync(BidList bidList)
        {
            if (bidList != null)
            {
                await DbContext.BidLists.AddAsync(bidList);
                await DbContext.SaveChangesAsync();
            }
            return bidList;
        }
        public async Task<BidList?> DeleteAsync(BidList bidList)
        {
            if (bidList != null)
            {
                DbContext.BidLists.Remove(bidList);
                await DbContext.SaveChangesAsync();
            }
            return bidList;
        }
        public async Task<List<BidList>> GetAllAsync()
        {
            return await DbContext.BidLists.ToListAsync();
        }
        public async Task<BidList?> GetByIdAsync(int id)
        {
            return await DbContext.BidLists.FirstOrDefaultAsync(x => x.BidListId == id);
        }
        public async Task<BidList?> UpdateAsync(BidList bidList)
        {
            if (bidList != null)
            {
                BidList? existing = DbContext.BidLists.Local.SingleOrDefault(x => x.BidListId == bidList.BidListId);
                if (existing != null)
                    DbContext.Entry(existing).State = EntityState.Detached;

                DbContext.BidLists.Update(bidList);
                await DbContext.SaveChangesAsync();
            }
            return bidList;
        }
        public async Task<bool> ExistAsync(int id)
        {
            return await DbContext.BidLists.AnyAsync(x => x.BidListId == id);
        }
    }
}