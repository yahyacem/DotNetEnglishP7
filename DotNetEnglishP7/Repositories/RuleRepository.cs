﻿using AutoMapper;
using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using DotNetEnglishP7.Mappers;
using Microsoft.EntityFrameworkCore;

namespace DotNetEnglishP7.Repositories
{
    public class RuleRepository : IRuleRepository
    {
        private static IMapper _mapper = MappingProfile.InitializeAutoMapper().CreateMapper();
        private LocalDbContext DbContext;
        public RuleRepository(LocalDbContext _dbContext)
        {
            DbContext = _dbContext;
        }
        public async Task<Rule?> AddAsync(Rule rule)
        {
            if (rule != null)
            {
                await DbContext.Rules.AddAsync(rule);
                await DbContext.SaveChangesAsync();
            }
            return rule;
        }
        public async Task<Rule?> DeleteAsync(Rule rule)
        {
            if (rule != null)
            {
                DbContext.Rules.Remove(rule);
                await DbContext.SaveChangesAsync();
            }
            return rule;
        }
        public async Task<bool> ExistAsync(int id)
        {
            return await DbContext.Rules.AnyAsync(x => x.Id == id);
        }
        public async Task<List<Rule>> GetAllAsync()
        {
            return await DbContext.Rules.ToListAsync();
        }
        public async Task<Rule?> GetByIdAsync(int? id)
        {
            return await DbContext.Rules.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Rule?> UpdateAsync(Rule rule)
        {
            Rule? ruleToUpdate = await GetByIdAsync(rule.Id);
            if (ruleToUpdate == null)
                return null;

            _mapper.Map(rule, ruleToUpdate);
            DbContext.Rules.Update(ruleToUpdate);
            await DbContext.SaveChangesAsync();

            return ruleToUpdate;
        }
    }
}
