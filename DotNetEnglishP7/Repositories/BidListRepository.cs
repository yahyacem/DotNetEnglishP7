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

        public async Task DeleteAsync(int id)
        {
            BidList? bidListToDelete = await GetByIdAsync(id);
            if (bidListToDelete != null)
            {
                DbContext.BidLists.Remove(bidListToDelete);
                await DbContext.SaveChangesAsync();
            }
        }

        public async Task<BidList[]?> GetAllAsync()
        {
            return await DbContext.BidLists.ToArrayAsync();
        }

        public async Task<BidList?> GetByIdAsync(int id)
        {
            return await DbContext.BidLists.FirstOrDefaultAsync(x => x.BidListId == id);
        }

        public async Task<BidList?> UpdateAsync(BidList bidList)
        {
            if (bidList != null && await ExistAsync(bidList.BidListId))
            {
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
