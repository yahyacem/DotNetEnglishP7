using Dot.Net.WebApi.Domain;

namespace DotNetEnglishP7.Repositories
{
    public interface IRuleRepository
    {
        public Task<Rule?> GetByIdAsync(int id);
        public Task<List<Rule>> GetAllAsync();
        public Task<Rule?> AddAsync(Rule rule);
        public Task<Rule?> UpdateAsync(Rule rule);
        public Task<Rule?> DeleteAsync(Rule rule);
        public Task<bool> ExistAsync(int id);
    }
}
