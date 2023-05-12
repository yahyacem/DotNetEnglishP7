using Dot.Net.WebApi.Domain;
using DotNetEnglishP7.Repositories;
using DotNetEnglishP7.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetEnglishP7.Tests.Unit.Services
{
    public class RuleServiceUnitTests : UnitTests
    {
        [Fact]
        public async void GetAllAsync_ShouldReturnListRule()
        {
            // Arrange
            List<Rule> seedData = new List<Rule>()
            {
                new Rule() { Description = "Test Description 1" },
                new Rule() { Description = "Test Description 2" },
                new Rule() { Description = "Test Description 3" }
            };
            for (int i = 1; i <= seedData.Count; i++)
            {
                seedData[i - 1].SetId(i);
            }

            var ruleRepository = new Mock<IRuleRepository>();
            ruleRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(seedData);

            // Act
            IRuleService ruleService = new RuleService(ruleRepository.Object);
            var ruleResult = await ruleService.GetAllAsync();

            // Assert
            Assert.NotNull(ruleResult);
            Assert.Equal(3, ruleResult.Count);
            Assert.Equal("Test Description 2", ruleResult[1].Description);
        }
        [Fact]
        public async void GetByIdAsync_PassInt_ShouldReturnSingleRule()
        {
            // Arrange
            Rule seedData = new Rule() { Description = "Test Description 2" };
            seedData.SetId(1);

            var ruleRepository = new Mock<IRuleRepository>();
            ruleRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(seedData);

            // Act
            IRuleService ruleService = new RuleService(ruleRepository.Object);
            var ruleResult = await ruleService.GetByIdAsync(1);

            // Assert
            Assert.NotNull(ruleResult);
            Assert.Equal(1, ruleResult.Id);
            Assert.Equal("Test Description 2", ruleResult.Description);
        }
        [Fact]
        public async void GetByIdAsync_PassInt_ShouldReturnNull()
        {
            // Arrange
            var ruleRepository = new Mock<IRuleRepository>();

            // Act
            IRuleService ruleService = new RuleService(ruleRepository.Object);
            var ruleResult = await ruleService.GetByIdAsync(1);

            // Assert
            Assert.Null(ruleResult);
        }
        [Fact]
        public async void AddAsync_PassRule_ShouldReturnSameRule()
        {
            // Arrange
            Rule seedData = new Rule() { Description = "Test Description 2" };
            seedData.SetId(1);

            var ruleRepository = new Mock<IRuleRepository>();
            ruleRepository.Setup(x => x.AddAsync(seedData)).ReturnsAsync(seedData);

            // Act
            IRuleService ruleService = new RuleService(ruleRepository.Object);
            var ruleResult = await ruleService.AddAsync(seedData);

            // Assert
            Assert.NotNull(ruleResult);
            Assert.Equal(1, ruleResult.Id);
            Assert.Equal("Test Description 2", ruleResult.Description);
        }
        [Fact]
        public async void UpdateAsync_PassRule_ShouldReturnSameRule()
        {
            // Arrange
            int idToUpdate = 1;
            Rule seedData = new Rule() { Description = "Test Description 2" };
            seedData.SetId(idToUpdate);

            var ruleRepository = new Mock<IRuleRepository>();
            ruleRepository.Setup(x => x.GetByIdAsync(idToUpdate)).ReturnsAsync(seedData);
            ruleRepository.Setup(x => x.UpdateAsync(seedData)).ReturnsAsync(seedData);

            // Act
            IRuleService ruleService = new RuleService(ruleRepository.Object);
            var ruleResult = await ruleService.UpdateAsync(seedData);

            // Assert
            Assert.NotNull(ruleResult);
            Assert.Equal(1, ruleResult.Id);
            Assert.Equal("Test Description 2", ruleResult.Description);
        }
        [Fact]
        public async void DeleteAsync_PassRule_ShouldReturnSameRule()
        {
            // Arrange
            Rule seedData = new Rule() { Description = "Test Description 2" };
            seedData.SetId(1);

            var ruleRepository = new Mock<IRuleRepository>();
            ruleRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(seedData);
            ruleRepository.Setup(x => x.DeleteAsync(seedData)).ReturnsAsync(seedData);

            // Act
            IRuleService ruleService = new RuleService(ruleRepository.Object);
            var ruleResult = await ruleService.DeleteAsync(1);

            // Assert
            Assert.NotNull(ruleResult);
            Assert.Equal(1, ruleResult.Id);
            Assert.Equal("Test Description 2", ruleResult.Description);
        }
    }
}