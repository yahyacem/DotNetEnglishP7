using Dot.Net.WebApi.Controllers;
using Dot.Net.WebApi.Domain;
using DotNetEnglishP7.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetEnglishP7.Tests.Unit.Controllers
{
    public class RuleControllerUnitTests : UnitTests
    {
        /// <summary>
        /// Test API call of type GET on endpoint /Rule. It should return the list of all Rule.
        /// </summary>
        [Fact]
        public async void Get_Home_ShouldReturnListRule()
        {
            // Arrange
            List<Rule> seedData = new List<Rule>()
            {
                new Rule() { Name = "Rule Test 1" },
                new Rule() { Name = "Rule Test 2" },
                new Rule() { Name = "Rule Test 3" }
            };
            for (int i = 1; i <= seedData.Count; i++)
            {
                seedData[i - 1].SetId(i);
            }

            var ruleService = new Mock<IRuleService>();
            ruleService.Setup(x => x.GetAllAsync()).ReturnsAsync(seedData);
            var controller = new RuleController(ruleService.Object);

            // Act
            var actionResult = await controller.Home();
            var okResult = actionResult as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.NotNull(okResult.Value);
            Assert.Equal(seedData.Count, ((List<Rule>)okResult.Value).Count);
            Assert.Equal(200, okResult.StatusCode);
        }
        /// <summary>
        /// Test API call of type GET on endpoint /Rule/{id}. It should return the requested Rule.
        /// </summary>
        [Fact]
        public async void Get_GetById_PassInt_ShouldReturnSingleRule()
        {
            // Arrange
            int idToReturn = 1;
            Rule seedData = new Rule() { Name = "Rule Test 1" };
            seedData.SetId(idToReturn);

            var ruleService = new Mock<IRuleService>();
            ruleService.Setup(x => x.GetByIdAsync(idToReturn)).ReturnsAsync(seedData);
            var controller = new RuleController(ruleService.Object);

            // Act
            var actionResult = await controller.GetById(idToReturn);
            var okResult = actionResult as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.NotNull(okResult.Value);
            Assert.Equal(seedData.Name, ((Rule)okResult.Value).Name);
            Assert.Equal(200, okResult.StatusCode);
        }
        /// <summary>
        /// Test API call of type GET on endpoint /Rule/{id}. It should return NotFound.
        /// </summary>
        [Fact]
        public async void Get_GetById_PassInt_ShouldReturnNotFound()
        {
            // Arrange
            var ruleService = new Mock<IRuleService>();
            var controller = new RuleController(ruleService.Object);

            // Act
            var actionResult = await controller.GetById(1);
            var notFoundResult = actionResult as NotFoundResult;

            // Assert
            Assert.NotNull(notFoundResult);
            Assert.Equal(404, notFoundResult.StatusCode);
        }
        /// <summary>
        /// Test API call of type POST on endpoint /Rule/Add. It should return the created Rule.
        /// </summary>
        [Fact]
        public async void Post_Add_PassRule_ShouldReturnSameRule()
        {
            // Arrange
            int idToCreate = 1;
            Rule seedData = new Rule() { Name = "Rule Test 1" };
            seedData.SetId(idToCreate);

            var ruleService = new Mock<IRuleService>();
            ruleService.Setup(x => x.AddAsync(seedData)).ReturnsAsync(seedData);
            var controller = new RuleController(ruleService.Object);

            // Act
            var actionResult = await controller.Add(seedData);
            var okResult = actionResult as CreatedAtActionResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.NotNull(okResult.Value);
            Assert.Equal(seedData.Name, ((Rule)okResult.Value).Name);
            Assert.Equal(201, okResult.StatusCode);
        }
        /// <summary>
        /// Test API call of type POST on endpoint /Rule/Update. It should return the updated Rule.
        /// </summary>
        [Fact]
        public async void Post_Update_PassRule_ShouldReturnSameRule()
        {
            // Arrange
            int idToUpdate = 1;
            Rule seedData = new Rule() { Name = "Rule Test 1" };
            seedData.SetId(idToUpdate);

            var ruleService = new Mock<IRuleService>();
            ruleService.Setup(x => x.UpdateAsync(seedData)).ReturnsAsync(seedData);
            ruleService.Setup(x => x.ExistAsync(idToUpdate)).ReturnsAsync(true);
            var controller = new RuleController(ruleService.Object);

            // Act
            var actionResult = await controller.Update(idToUpdate, seedData);
            var okResult = actionResult as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.NotNull(okResult.Value);
            Assert.Equal(seedData.Name, ((Rule)okResult.Value).Name);
            Assert.Equal(200, okResult.StatusCode);
        }
        /// <summary>
        /// Test API call of type DELETE on endpoint /Rule/Delete. It should return the deleted Rule.
        /// </summary>
        [Fact]
        public async void Delete_Delete_PassRule_ShouldReturnSameRule()
        {
            // Arrange
            int idToDelete = 1;
            Rule seedData = new Rule() { Name = "Rule Test 1" };
            seedData.SetId(idToDelete);

            var ruleService = new Mock<IRuleService>();
            ruleService.Setup(x => x.DeleteAsync(idToDelete)).ReturnsAsync(seedData);
            var controller = new RuleController(ruleService.Object);

            // Act
            var actionResult = await controller.Delete(idToDelete);
            var okResult = actionResult as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.NotNull(okResult.Value);
            Assert.Equal(seedData.Name, ((Rule)okResult.Value).Name);
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}
