using Dot.Net.WebApi.Domain;

namespace DotNetEnglishP7.Services
{
    public interface ICurveService
    {
        public Task<List<CurvePoint>> GetAllAsync();
        public Task<CurvePoint?> GetByIdAsync(int id);
        public Task<CurvePoint?> AddAsync(CurvePoint curvePoint);
        public Task<CurvePoint?> UpdateAsync(CurvePoint curvePoint);
        public Task<CurvePoint?> DeleteAsync(int id);
        public Task<bool> ExistAsync(int id);
    }
}