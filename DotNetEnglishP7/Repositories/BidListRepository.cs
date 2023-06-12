using AutoMapper;
using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using DotNetEnglishP7.Mappers;
using Microsoft.EntityFrameworkCore;

namespace DotNetEnglishP7.Repositories
{
    public class BidListRepository : IBidListRepository
    {
        private static IMapper _mapper = MappingProfile.InitializeAutoMapper().CreateMapper();
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
        public async Task<BidList?> GetByIdAsync(int? id)
        {
            return await DbContext.BidLists.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<BidList?> UpdateAsync(BidList bidList)
        {
            BidList? bidListToUpdate = await GetByIdAsync(bidList.Id);
            if (bidListToUpdate == null)
                return null;

            _mapper.Map(bidList, bidListToUpdate);
            DbContext.BidLists.Update(bidListToUpdate);
            await DbContext.SaveChangesAsync();

            return bidListToUpdate;
        }
        public async Task<bool> ExistAsync(int id)
        {
            return await DbContext.BidLists.AnyAsync(x => x.Id == id);
        }
    }
}