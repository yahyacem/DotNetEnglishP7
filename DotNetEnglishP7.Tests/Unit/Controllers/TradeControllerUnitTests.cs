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
    public class TradeControllerUnitTests : UnitTests
    {
        /// <summary>
        /// Test API call of type GET on endpoint /Trade. It should return the list of all Trade.
        /// </summary>
        [Fact]
        public async void Get_Home_ShouldReturnListTrade()
        {
            // Arrange
            List<Trade> seedData = new List<Trade>()
            {
                new Trade() { Account = "Account Test 1" },
                new Trade() { Account = "Account Test 2" },
                new Trade() { Account = "Account Test 3" }
            };
            for (int i = 1; i <= seedData.Count; i++)
            {
                seedData[i - 1].SetTradeId(i);
            }

            var tradeService = new Mock<ITradeService>();
            tradeService.Setup(x => x.GetAllAsync()).ReturnsAsync(seedData);
            var controller = new TradeController(tradeService.Object);

            // Act
            var actionResult = await controller.Home();
            var okResult = actionResult as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.NotNull(okResult.Value);
            Assert.Equal(seedData.Count, ((List<Trade>)okResult.Value).Count);
            Assert.Equal(200, okResult.StatusCode);
        }
        /// <summary>
        /// Test API call of type GET on endpoint /Trade/{id}. It should return the requested Trade.
        /// </summary>
        [Fact]
        public async void Get_GetById_PassInt_ShouldReturnSingleTrade()
        {
            // Arrange
            int idToReturn = 1;
            Trade seedData = new Trade() { Account = "Account Test 1" };
            seedData.SetTradeId(idToReturn);

            var tradeService = new Mock<ITradeService>();
            tradeService.Setup(x => x.GetByIdAsync(idToReturn)).ReturnsAsync(seedData);
            var controller = new TradeController(tradeService.Object);

            // Act
            var actionResult = await controller.GetById(idToReturn);
            var okResult = actionResult as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.NotNull(okResult.Value);
            Assert.Equal(seedData.Account, ((Trade)okResult.Value).Account);
            Assert.Equal(200, okResult.StatusCode);
        }
        /// <summary>
        /// Test API call of type GET on endpoint /Trade/{id}. It should return NotFound.
        /// </summary>
        [Fact]
        public async void Get_GetById_PassInt_ShouldReturnNotFound()
        {
            // Arrange
            var tradeService = new Mock<ITradeService>();
            var controller = new TradeController(tradeService.Object);

            // Act
            var actionResult = await controller.GetById(1);
            var notFoundResult = actionResult as NotFoundResult;

            // Assert
            Assert.NotNull(notFoundResult);
            Assert.Equal(404, notFoundResult.StatusCode);
        }
        /// <summary>
        /// Test API call of type POST on endpoint /Trade/Add. It should return the created Trade.
        /// </summary>
        [Fact]
        public async void Post_Add_PassTrade_ShouldReturnSameTrade()
        {
            // Arrange
            int idToCreate = 1;
            Trade seedData = new Trade() { Account = "Account Test 1" };
            seedData.SetTradeId(idToCreate);

            var tradeService = new Mock<ITradeService>();
            tradeService.Setup(x => x.AddAsync(seedData)).ReturnsAsync(seedData);
            var controller = new TradeController(tradeService.Object);

            // Act
            var actionResult = await controller.Add(seedData);
            var okResult = actionResult as CreatedAtActionResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.NotNull(okResult.Value);
            Assert.Equal(seedData.Account, ((Trade)okResult.Value).Account);
            Assert.Equal(201, okResult.StatusCode);
        }
        /// <summary>
        /// Test API call of type POST on endpoint /Trade/Update. It should return the updated Trade.
        /// </summary>
        [Fact]
        public async void Post_Update_PassTrade_ShouldReturnSameTrade()
        {
            // Arrange
            int idToUpdate = 1;
            Trade seedData = new Trade() { Account = "Account Test 1" };
            seedData.SetTradeId(idToUpdate);

            var tradeService = new Mock<ITradeService>();
            tradeService.Setup(x => x.UpdateAsync(seedData)).ReturnsAsync(seedData);
            tradeService.Setup(x => x.ExistAsync(idToUpdate)).ReturnsAsync(true);
            var controller = new TradeController(tradeService.Object);

            // Act
            var actionResult = await controller.Update(idToUpdate, seedData);
            var okResult = actionResult as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.NotNull(okResult.Value);
            Assert.Equal(seedData.Account, ((Trade)okResult.Value).Account);
            Assert.Equal(200, okResult.StatusCode);
        }
        /// <summary>
        /// Test API call of type DELETE on endpoint /Trade/Delete. It should return the deleted Trade.
        /// </summary>
        [Fact]
        public async void Delete_Delete_PassTrade_ShouldReturnSameTrade()
        {
            // Arrange
            int idToDelete = 1;
            Trade seedData = new Trade() { Account = "Account Test 1" };
            seedData.SetTradeId(idToDelete);

            var tradeService = new Mock<ITradeService>();
            tradeService.Setup(x => x.DeleteAsync(idToDelete)).ReturnsAsync(seedData);
            var controller = new TradeController(tradeService.Object);

            // Act
            var actionResult = await controller.Delete(idToDelete);
            var okResult = actionResult as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.NotNull(okResult.Value);
            Assert.Equal(seedData.Account, ((Trade)okResult.Value).Account);
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}
