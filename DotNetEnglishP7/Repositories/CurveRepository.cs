﻿using AutoMapper;
using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using DotNetEnglishP7.Mappers;
using Microsoft.EntityFrameworkCore;

namespace DotNetEnglishP7.Repositories
{
    public class CurveRepository : ICurveRepository
    {
        private static IMapper _mapper = MappingProfile.InitializeAutoMapper().CreateMapper();
        private LocalDbContext DbContext;
        public CurveRepository(LocalDbContext _dbContext)
        {
            DbContext= _dbContext;
        }
        public async Task<CurvePoint?> AddAsync(CurvePoint curvePoint)
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
        public async Task<CurvePoint?> GetByIdAsync(int? id)
        {
            return await DbContext.CurvePoints.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<CurvePoint?> UpdateAsync(CurvePoint curvePoint)
        {
            CurvePoint? curvePointToUpdate = await GetByIdAsync(curvePoint.Id);
            if (curvePointToUpdate == null)
                return null;

            _mapper.Map(curvePoint, curvePointToUpdate);
            DbContext.CurvePoints.Update(curvePointToUpdate);
            await DbContext.SaveChangesAsync();

            return curvePointToUpdate;
        }
    }
}
