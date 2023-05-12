using Dot.Net.WebApi.Domain;

namespace DotNetEnglishP7.Services
{
    public interface IRuleService
    {
        public Task<List<Rule>> GetAllAsync();
        public Task<Rule?> GetByIdAsync(int id);
        public Task<Rule?> AddAsync(Rule rule);
        public Task<Rule?> UpdateAsync(Rule rule);
        public Task<Rule?> DeleteAsync(int id);
        public Task<bool> ExistAsync(int id);
    }
}
