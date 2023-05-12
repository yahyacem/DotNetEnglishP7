using Dot.Net.WebApi.Domain;

namespace DotNetEnglishP7.Repositories
{
    public interface ICurveRepository
    {
        public Task<CurvePoint?> GetByIdAsync(int id);
        public Task<List<CurvePoint>> GetAllAsync();
        public Task<CurvePoint?> AddAsync(CurvePoint curvePoint);
        public Task<CurvePoint?> UpdateAsync(CurvePoint curvePoint);
        public Task<CurvePoint?> DeleteAsync(CurvePoint curvePoint);
        public Task<bool> ExistAsync(int id);
    }
}
