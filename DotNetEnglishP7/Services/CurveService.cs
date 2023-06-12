using Dot.Net.WebApi.Domain;
using DotNetEnglishP7.Repositories;

namespace DotNetEnglishP7.Services
{
    public class CurveService : ICurveService
    {
        private ICurveRepository _curveRepository;
        public CurveService(ICurveRepository curveRepository)
        {
            _curveRepository = curveRepository;
        }
        public async Task<CurvePoint?> AddAsync(CurvePoint curvePoint)
        {
            return await _curveRepository.AddAsync(curvePoint);
        }
        public async Task<CurvePoint?> DeleteAsync(int id)
        {
            CurvePoint? curvePointToDelete = await _curveRepository.GetByIdAsync(id);
            if (curvePointToDelete != null)
            {
                await _curveRepository.DeleteAsync(curvePointToDelete);
            }
            return curvePointToDelete;
        }
        public async Task<List<CurvePoint>> GetAllAsync()
        {
            return await _curveRepository.GetAllAsync();
        }
        public async Task<CurvePoint?> GetByIdAsync(int id)
        {
            return await _curveRepository.GetByIdAsync(id);
        }
        public async Task<CurvePoint?> UpdateAsync(CurvePoint curvePoint)
        {
            if (curvePoint != null)
            {
                await _curveRepository.UpdateAsync(curvePoint);
            }
            return curvePoint;
        }
        public async Task<bool> ExistAsync(int id)
        {
            return await _curveRepository.ExistAsync(id);
        }
    }
}