using AutoMapper;
using Dot.Net.WebApi.Domain;
using DotNetEnglishP7.Mappers;
using DotNetEnglishP7.Repositories;

namespace DotNetEnglishP7.Services
{
    public class CurveService : ICurveService
    {
        private ICurveRepository _curveRepository;
        private static IMapper _mapper = MappingProfile.InitializeAutoMapper().CreateMapper();
        public CurveService(ICurveRepository curveRepository)
        {
            _curveRepository = curveRepository;
        }
        public async Task<CurvePoint?> AddAsync(CurvePoint curvePoint)
        {
            return await _curveRepository.AddAsync(curvePoint);
        }
        public async Task DeleteAsync(int id)
        {

            if (id != null)
            {
                await _curveRepository.DeleteAsync(id);
            }
        }
        public async Task<List<CurvePoint>> GetAllAsync()
        {
            var curveEntities = await _curveRepository.GetAllAsync();
            var curveViewModels = new List<CurvePoint>();

            foreach (var curveEntity in curveEntities)
            {
                curveViewModels.Add(_mapper.Map<CurvePoint>(curveEntity));
            }
            return curveViewModels;
        }
        public Task<CurvePoint?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        public Task<CurvePoint?> UpdateAsync(CurvePoint curvePointViewModel)
        {
            throw new NotImplementedException();
        }
    }
}
