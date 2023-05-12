using Dot.Net.WebApi.Controllers;
using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using DotNetEnglishP7.Repositories;
using DotNetEnglishP7.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetEnglishP7.Tests.Integration.Controllers
{
    [Collection("Sequential")]
    public class RuleControllerIntegrationTests : IntegrationTests
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
                    new Rule() { Name = "Name Test 1", Description = "Description Test 1" },
                    new Rule() { Name = "Name Test 2", Description = "Description Test 2" },
                    new Rule() { Name = "Name Test 3", Description = "Description Test 3" }
                };
                for (int i = 1; i <= seedData.Count; i++)
                {
                    seedData[i - 1].SetId(i);
                }
                context.Rules.AddRange(seedData);
                context.SaveChanges();

                // Instantiate repository, service and controller
                var ruleRepositoy = new RuleRepository(context);
                IRuleService ruleService = new RuleService(ruleRepositoy);
                var controller = new RuleController(ruleService);

                // Act
                // Make the call and capture result
                var actionResult = await controller.Home();
                var okResult = actionResult as OkObjectResult;

                // Assert
                // Check response code
                Assert.NotNull(okResult);
                Assert.NotNull(okResult.Value);
                Assert.Equal(200, okResult.StatusCode);
                // Check response data
                Assert.Equal(seedData.Count, ((List<Rule>)okResult.Value).Count);
                Assert.Equal(seedData[0].Name, ((List<Rule>)okResult.Value).First(x => x.Id == 1).Name);
                Assert.Equal(seedData[0].Description, ((List<Rule>)okResult.Value).First(x => x.Id == 1).Description);
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
                var controller = new RuleController(ruleService);

                // Act
                // Make the call and capture result
                var actionResult = await controller.GetById(idToReturn);
                var okResult = actionResult as OkObjectResult;

                // Assert
                // Check response code
                Assert.NotNull(okResult);
                Assert.NotNull(okResult.Value);
                Assert.Equal(200, okResult.StatusCode);
                // Check response data
                Assert.Equal(seedData.Id, ((Rule)okResult.Value).Id);
                Assert.Equal(seedData.Name, ((Rule)okResult.Value).Name);
                Assert.Equal(seedData.Description, ((Rule)okResult.Value).Description);
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
                var controller = new RuleController(ruleService);

                // Act
                // Make the call and capture result
                var actionResult = await controller.GetById(idToReturn);
                var notFoundResult = actionResult as NotFoundResult;

                // Assert
                // Check response code
                Assert.NotNull(notFoundResult);
                Assert.Equal(404, notFoundResult.StatusCode);
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
                var controller = new RuleController(ruleService);

                // Act
                // Make the call and capture result
                var actionResult = await controller.Add(seedData);
                var okResult = actionResult as CreatedAtActionResult;

                // Check response code
                Assert.NotNull(okResult);
                Assert.NotNull(okResult.Value);
                Assert.Equal(201, okResult.StatusCode);
                // Check response data
                Assert.Equal(seedData.Id, ((Rule)okResult.Value).Id);
                Assert.Equal(seedData.Name, ((Rule)okResult.Value).Name);
                Assert.Equal(seedData.Description, ((Rule)okResult.Value).Description);
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
                Rule seedData = new Rule() { Name = "Name Test 1", Description = "Description Test 1" };
                context.Rules.Add(seedData);
                context.SaveChanges();
                // Prepare new data to update
                Rule updatedData = new Rule() { Name = "Name Test 2", Description = "Description Test 2" };

                // Instantiate repository, service and controller
                var ruleRepositoy = new RuleRepository(context);
                IRuleService ruleService = new RuleService(ruleRepositoy);
                var controller = new RuleController(ruleService);

                // Act
                // Make the call and capture result
                var actionResult = await controller.Update(idToUpdate, updatedData);
                var okResult = actionResult as OkObjectResult;

                // Check response code
                Assert.NotNull(okResult);
                Assert.NotNull(okResult.Value);
                Assert.Equal(200, okResult.StatusCode);
                // Check response data
                Assert.Equal(updatedData.Id, ((Rule)okResult.Value).Id);
                Assert.Equal(updatedData.Name, ((Rule)okResult.Value).Name);
                Assert.Equal(updatedData.Description, ((Rule)okResult.Value).Description);
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
                    new Rule() { Name = "Name Test 1", Description = "Description Test 1" },
                    new Rule() { Name = "Name Test 2", Description = "Description Test 2" },
                    new Rule() { Name = "Name Test 3", Description = "Description Test 3" }
                };
                for (int i = 1; i <= seedData.Count; i++)
                {
                    seedData[i - 1].SetId(i);
                }
                context.Rules.AddRange(seedData);
                context.SaveChanges();

                // Instantiate repository, service and controller
                var ruleRepositoy = new RuleRepository(context);
                IRuleService ruleService = new RuleService(ruleRepositoy);
                var controller = new RuleController(ruleService);

                // Act
                // Make the call and capture result
                var actionResult = await controller.Delete(idToDelete);
                var okResult = actionResult as OkObjectResult;

                // Assert
                // Check response code
                Assert.NotNull(okResult);
                Assert.NotNull(okResult.Value);
                Assert.Equal(200, okResult.StatusCode);
                // Check response data
                Assert.Equal(seedData.First(x => x.Id == idToDelete).Id, ((Rule)okResult.Value).Id);
                Assert.Equal(seedData.First(x => x.Id == idToDelete).Name, ((Rule)okResult.Value).Name);
                Assert.Equal(seedData.First(x => x.Id == idToDelete).Description, ((Rule)okResult.Value).Description);
                // Check data in database
                Assert.Null(await ruleService.GetByIdAsync(idToDelete));
                Assert.Equal(2, (await ruleService.GetAllAsync()).Count);
            }
        }
    }
}
