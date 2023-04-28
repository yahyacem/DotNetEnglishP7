using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace DotNetEnglishP7.Repositories
{
    public class CurveRepository : ICurveRepository
    {
        private LocalDbContext DbContext;
        public CurveRepository(LocalDbContext _dbContext)
        {
            DbContext= _dbContext;
        }
        public async Task<CurvePoint> AddAsync(CurvePoint curvePoint)
        {
            if (curvePoint != null)
            {
                await DbContext.CurvePoints.AddAsync(curvePoint);
                await DbContext.SaveChangesAsync();
            }
            return curvePoint;
        }

        public async Task<CurvePoint?> DeleteAsync(CurvePoint curvePoint)
        {
            if (curvePoint != null)
            {
                DbContext.CurvePoints.Remove(curvePoint);
                await DbContext.SaveChangesAsync();
            }
            return curvePoint;
        }

        public async Task<bool> ExistAsync(int id)
        {
            return await DbContext.CurvePoints.AnyAsync(x => x.Id == id);
        }

        public async Task<List<CurvePoint>> GetAllAsync()
        {
            return await DbContext.CurvePoints.ToListAsync();
        }

        public async Task<CurvePoint?> GetByIdAsync(int id)
        {
            return await DbContext.CurvePoints.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<CurvePoint> UpdateAsync(CurvePoint curvePoint)
        {
            if (curvePoint != null)
            {
                DbContext.CurvePoints.Update(curvePoint);
                await DbContext.SaveChangesAsync();
            }
            return curvePoint;
        }
    }
}
