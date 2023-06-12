using Dot.Net.WebApi.Controllers;
using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using DotNetEnglishP7.Identity;
using DotNetEnglishP7.Repositories;
using DotNetEnglishP7.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetEnglishP7.Tests.Integration.Services
{
    [Collection("Sequential")]
    public class RuleServiceIntegrationTests : IntegrationTests
    {
        /// <summary>
        /// Test API call of type GET on endpoint /Rule. It should return the list of all Rule.
        /// </summary>
        [Fact]
        public async void GET_Home_ShouldReturnListRule()
        {
            using (var context = new LocalDbContext(GetOptions()))
            {
                // Arrange
                List<Rule> seedData = new List<Rule>()
                {
                    new Rule() { Id = 1, Name = "Name Test 1", Description = "Description Test 1" },
                    new Rule() { Id = 2, Name = "Name Test 2", Description = "Description Test 2" },
                    new Rule() { Id = 3, Name = "Name Test 3", Description = "Description Test 3" }
                };
                context.Rules.AddRange(seedData);
                context.SaveChanges();

                // Instantiate repository, service and controller
                var ruleRepositoy = new RuleRepository(context);
                IRuleService ruleService = new RuleService(ruleRepositoy);

                // Act
                // Make the call and capture result
                var result = await ruleService.GetAllAsync();

                // Assert
                // Check response data
                Assert.Equal(seedData.Count, result.Count);
                Assert.Equal(seedData[0].Name, result.First(x => x.Id == 1).Name);
                Assert.Equal(seedData[0].Description, result.First(x => x.Id == 1).Description);
            }
        }
        /// <summary>
        /// Test API call of type GET on endpoint /Rule/{id}. It should return the requested Rule.
        /// </summary>
        [Fact]
        public async void GET_GetById_PassInt_ShouldReturnSameRule()
        {
            using (var context = new LocalDbContext(GetOptions()))
            {
                // Arrange
                int idToReturn = 1;
                Rule seedData = new Rule() { Name = "Name Test 1", Description = "Description Test 1" };
                context.Rules.Add(seedData);
                context.SaveChanges();

                // Instantiate repository, service and controller
                var ruleRepositoy = new RuleRepository(context);
                IRuleService ruleService = new RuleService(ruleRepositoy);

                // Act
                // Make the call and capture result
                var result = await ruleService.GetByIdAsync(idToReturn);

                // Assert
                // Check response data
                Assert.NotNull(result);
                Assert.Equal(seedData.Id, result.Id);
                Assert.Equal(seedData.Name, result.Name);
                Assert.Equal(seedData.Description, result.Description);
            }
        }
        /// <summary>
        /// Test API call of type GET on endpoint /Rule/{id}. It should return NotFound.
        /// </summary>
        [Fact]
        public async void GET_GetById_PassInt_ShouldReturnNull()
        {
            using (var context = new LocalDbContext(GetOptions()))
            {
                // Arrange
                int idToReturn = 1;

                // Instantiate repository, service and controller
                var ruleRepositoy = new RuleRepository(context);
                IRuleService ruleService = new RuleService(ruleRepositoy);

                // Act
                // Make the call and capture result
                var result = await ruleService.GetByIdAsync(idToReturn);

                // Assert
                // Check data in database
                Assert.Empty(await ruleService.GetAllAsync());
                Assert.Null(await ruleService.GetByIdAsync(idToReturn));
            }
        }
        /// <summary>
        /// Test API call of type POST on endpoint /Rule/Add. It should return the created Rule.
        /// </summary>
        [Fact]
        public async void POST_Add_PassRule_ShouldReturnSameRule()
        {
            using (var context = new LocalDbContext(GetOptions()))
            {
                // Arrange
                int idToCreate = 1;
                Rule seedData = new Rule() { Name = "Name Test 1", Description = "Description Test 1" };

                // Instantiate repository, service and controller
                var ruleRepositoy = new RuleRepository(context);
                IRuleService ruleService = new RuleService(ruleRepositoy);

                // Act
                // Make the call and capture result
                var result = await ruleService.AddAsync(seedData);

                //Assert
                // Check response data
                Assert.NotNull(result);
                Assert.Equal(seedData.Id, result.Id);
                Assert.Equal(seedData.Name, result.Name);
                Assert.Equal(seedData.Description, result.Description);
                // Check data in database
                Assert.Single(await ruleService.GetAllAsync());
                Assert.NotNull(await ruleService.GetByIdAsync(idToCreate));
            }
        }
        /// <summary>
        /// Test API call of type POST on endpoint /Rule/Update. It should return the updated Rule.
        /// </summary>
        [Fact]
        public async void POST_Update_PassRule_ShouldReturnSameRule()
        {
            using (var context = new LocalDbContext(GetOptions()))
            {
                // Arrange
                int idToUpdate = 1;
                // Insert seed data
                Rule seedData = new Rule() { Id = idToUpdate, Name = "Name Test 1", Description = "Description Test 1" };
                context.Rules.Add(seedData);
                context.SaveChanges();
                // Prepare new data to update
                Rule updatedData = new Rule() { Id = idToUpdate, Name = "Name Test 2", Description = "Description Test 2" };

                // Instantiate repository, service and controller
                var ruleRepositoy = new RuleRepository(context);
                IRuleService ruleService = new RuleService(ruleRepositoy);

                // Act
                // Make the call and capture result
                var result = await ruleService.UpdateAsync(updatedData);

                // Assert
                // Check response data
                Assert.NotNull(result);
                Assert.Equal(updatedData.Id, result.Id);
                Assert.Equal(updatedData.Name, result.Name);
                Assert.Equal(updatedData.Description, result.Description);
                // Check data in database
                Assert.NotNull(await ruleService.GetByIdAsync(idToUpdate));
            }
        }
        /// <summary>
        /// Test API call of type DELETE on endpoint /Rule/Delete. It should return the deleted Rule.
        /// </summary>
        [Fact]
        public async void DELETE_Delete_PassRule_ShouldReturnSameRule()
        {
            using (var context = new LocalDbContext(GetOptions()))
            {
                // Arrange
                int idToDelete = 1;
                // Insert seed data
                List<Rule> seedData = new List<Rule>()
                {
                    new Rule() { Id = 1, Name = "Name Test 1", Description = "Description Test 1" },
                    new Rule() { Id = 2, Name = "Name Test 2", Description = "Description Test 2" },
                    new Rule() { Id = 3, Name = "Name Test 3", Description = "Description Test 3" }
                };
                context.Rules.AddRange(seedData);
                context.SaveChanges();

                // Instantiate repository, service and controller
                var ruleRepositoy = new RuleRepository(context);
                IRuleService ruleService = new RuleService(ruleRepositoy);

                // Act
                // Make the call and capture result
                var result = await ruleService.DeleteAsync(idToDelete);

                // Assert
                // Check response data
                Assert.NotNull(result);
                Assert.Equal(seedData.First(x => x.Id == idToDelete).Id, result.Id);
                Assert.Equal(seedData.First(x => x.Id == idToDelete).Name, result.Name);
                Assert.Equal(seedData.First(x => x.Id == idToDelete).Description, result.Description);
                // Check data in database
                Assert.Null(await ruleService.GetByIdAsync(idToDelete));
                Assert.Equal(2, (await ruleService.GetAllAsync()).Count);
            }
        }
    }
}
