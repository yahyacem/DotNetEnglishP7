using Dot.Net.WebApi.Domain;
using DotNetEnglishP7.Repositories;

namespace DotNetEnglishP7.Services
{
    public class RuleService : IRuleService
    {
        private IRuleRepository _ruleRepository;
        public RuleService(IRuleRepository ruleRepository)
        {
            _ruleRepository = ruleRepository;
        }
        public async Task<Rule?> AddAsync(Rule rule)
        {
            return await _ruleRepository.AddAsync(rule);
        }
        public async Task<Rule?> DeleteAsync(int id)
        {
            Rule? ruleToDelete = await _ruleRepository.GetByIdAsync(id);
            if (ruleToDelete != null)
            {
                await _ruleRepository.DeleteAsync(ruleToDelete);
            }
            return ruleToDelete;
        }
        public async Task<List<Rule>> GetAllAsync()
        {
            return await _ruleRepository.GetAllAsync();
        }
        public async Task<Rule?> GetByIdAsync(int id)
        {
            return await _ruleRepository.GetByIdAsync(id);
        }
        public async Task<Rule?> UpdateAsync(Rule rule)
        {
            Rule? ruleToUpdate = await _ruleRepository.GetByIdAsync(rule.Id);
            if (ruleToUpdate != null)
            {
                ruleToUpdate = rule;
                await _ruleRepository.UpdateAsync(ruleToUpdate);
            }
            return ruleToUpdate;
        }
        public async Task<bool> ExistAsync(int id)
        {
            return await _ruleRepository.ExistAsync(id);
        }
    }
}
