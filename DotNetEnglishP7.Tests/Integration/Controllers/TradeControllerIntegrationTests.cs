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
    public class TradeControllerIntegrationTests : IntegrationTests
    {
        /// <summary>
        /// Test API call of type GET on endpoint /Trade. It should return the list of all Trade.
        /// </summary>
        [Fact]
        public async void GET_Home_ShouldReturnListTrade()
        {
            using (var context = new LocalDbContext(GetOptions()))
            {
                // Arrange
                List<Trade> seedData = new List<Trade>()
                {
                    new Trade() { Account = "Account Test 1", Type = "Type Test 1" },
                    new Trade() { Account = "Account Test 2", Type = "Type Test 2" },
                    new Trade() { Account = "Account Test 3", Type = "Type Test 3" }
                };
                for (int i = 1; i <= seedData.Count; i++)
                {
                    seedData[i - 1].SetTradeId(i);
                }
                context.Trades.AddRange(seedData);
                context.SaveChanges();

                // Instantiate repository, service and controller
                var tradeRepositoy = new TradeRepository(context);
                ITradeService tradeService = new TradeService(tradeRepositoy);
                var controller = new TradeController(tradeService);

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
                Assert.Equal(seedData.Count, ((List<Trade>)okResult.Value).Count);
                Assert.Equal(seedData[0].Account, ((List<Trade>)okResult.Value).First(x => x.TradeId == 1).Account);
                Assert.Equal(seedData[0].Type, ((List<Trade>)okResult.Value).First(x => x.TradeId == 1).Type);
            }
        }
        /// <summary>
        /// Test API call of type GET on endpoint /Trade/{id}. It should return the requested Trade.
        /// </summary>
        [Fact]
        public async void GET_GetById_PassInt_ShouldReturnSameTrade()
        {
            using (var context = new LocalDbContext(GetOptions()))
            {
                // Arrange
                int idToReturn = 1;
                Trade seedData = new Trade() { Account = "Account Test 1", Type = "Type Test 1" };
                context.Trades.Add(seedData);
                context.SaveChanges();

                // Instantiate repository, service and controller
                var tradeRepositoy = new TradeRepository(context);
                ITradeService tradeService = new TradeService(tradeRepositoy);
                var controller = new TradeController(tradeService);

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
                Assert.Equal(seedData.TradeId, ((Trade)okResult.Value).TradeId);
                Assert.Equal(seedData.Account, ((Trade)okResult.Value).Account);
                Assert.Equal(seedData.Type, ((Trade)okResult.Value).Type);
            }
        }
        /// <summary>
        /// Test API call of type GET on endpoint /Trade/{id}. It should return NotFound.
        /// </summary>
        [Fact]
        public async void GET_GetById_PassInt_ShouldReturnNull()
        {
            using (var context = new LocalDbContext(GetOptions()))
            {
                // Arrange
                int idToReturn = 1;

                // Instantiate repository, service and controller
                var tradeRepositoy = new TradeRepository(context);
                ITradeService tradeService = new TradeService(tradeRepositoy);
                var controller = new TradeController(tradeService);

                // Act
                // Make the call and capture result
                var actionResult = await controller.GetById(idToReturn);
                var notFoundResult = actionResult as NotFoundResult;

                // Assert
                // Check response code
                Assert.NotNull(notFoundResult);
                Assert.Equal(404, notFoundResult.StatusCode);
                // Check data in database
                Assert.Empty(await tradeService.GetAllAsync());
                Assert.Null(await tradeService.GetByIdAsync(idToReturn));
            }
        }
        /// <summary>
        /// Test API call of type POST on endpoint /Trade/Add. It should return the created Trade.
        /// </summary>
        [Fact]
        public async void POST_Add_PassTrade_ShouldReturnSameTrade()
        {
            using (var context = new LocalDbContext(GetOptions()))
            {
                // Arrange
                int idToCreate = 1;
                Trade seedData = new Trade() { Account = "Account Test 1", Type = "Type Test 1" };

                // Instantiate repository, service and controller
                var tradeRepositoy = new TradeRepository(context);
                ITradeService tradeService = new TradeService(tradeRepositoy);
                var controller = new TradeController(tradeService);

                // Act
                // Make the call and capture result
                var actionResult = await controller.Add(seedData);
                var okResult = actionResult as CreatedAtActionResult;

                // Check response code
                Assert.NotNull(okResult);
                Assert.NotNull(okResult.Value);
                Assert.Equal(201, okResult.StatusCode);
                // Check response data
                Assert.Equal(seedData.TradeId, ((Trade)okResult.Value).TradeId);
                Assert.Equal(seedData.Account, ((Trade)okResult.Value).Account);
                Assert.Equal(seedData.Type, ((Trade)okResult.Value).Type);
                // Check data in database
                Assert.Single(await tradeService.GetAllAsync());
                Assert.NotNull(await tradeService.GetByIdAsync(idToCreate));
            }
        }
        /// <summary>
        /// Test API call of type POST on endpoint /Trade/Update. It should return the updated Trade.
        /// </summary>
        [Fact]
        public async void POST_Update_PassTrade_ShouldReturnSameTrade()
        {
            using (var context = new LocalDbContext(GetOptions()))
            {
                // Arrange
                int idToUpdate = 1;
                // Insert seed data
                Trade seedData = new Trade() { Account = "Account Test 1", Type = "Type Test 1" };
                context.Trades.Add(seedData);
                context.SaveChanges();
                // Prepare new data to update
                Trade updatedData = new Trade() { Account = "Account Test 2", Type = "Type Test 2" };

                // Instantiate repository, service and controller
                var tradeRepositoy = new TradeRepository(context);
                ITradeService tradeService = new TradeService(tradeRepositoy);
                var controller = new TradeController(tradeService);

                // Act
                // Make the call and capture result
                var actionResult = await controller.Update(idToUpdate, updatedData);
                var okResult = actionResult as OkObjectResult;

                // Check response code
                Assert.NotNull(okResult);
                Assert.NotNull(okResult.Value);
                Assert.Equal(200, okResult.StatusCode);
                // Check response data
                Assert.Equal(updatedData.TradeId, ((Trade)okResult.Value).TradeId);
                Assert.Equal(updatedData.Account, ((Trade)okResult.Value).Account);
                Assert.Equal(updatedData.Type, ((Trade)okResult.Value).Type);
                // Check data in database
                Assert.NotNull(await tradeService.GetByIdAsync(idToUpdate));
            }
        }
        /// <summary>
        /// Test API call of type DELETE on endpoint /Trade/Delete. It should return the deleted Trade.
        /// </summary>
        [Fact]
        public async void DELETE_Delete_PassTrade_ShouldReturnSameTrade()
        {
            using (var context = new LocalDbContext(GetOptions()))
            {
                // Arrange
                int idToDelete = 1;
                // Insert seed data
                List<Trade> seedData = new List<Trade>()
                {
                    new Trade() { Account = "Account Test 1", Type = "Type Test 1" },
                    new Trade() { Account = "Account Test 2", Type = "Type Test 2" },
                    new Trade() { Account = "Account Test 3", Type = "Type Test 3" }
                };
                for (int i = 1; i <= seedData.Count; i++)
                {
                    seedData[i - 1].SetTradeId(i);
                }
                context.Trades.AddRange(seedData);
                context.SaveChanges();

                // Instantiate repository, service and controller
                var tradeRepositoy = new TradeRepository(context);
                ITradeService tradeService = new TradeService(tradeRepositoy);
                var controller = new TradeController(tradeService);

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
                Assert.Equal(seedData.First(x => x.TradeId == idToDelete).TradeId, ((Trade)okResult.Value).TradeId);
                Assert.Equal(seedData.First(x => x.TradeId == idToDelete).Account, ((Trade)okResult.Value).Account);
                Assert.Equal(seedData.First(x => x.TradeId == idToDelete).Type, ((Trade)okResult.Value).Type);
                // Check data in database
                Assert.Null(await tradeService.GetByIdAsync(idToDelete));
                Assert.Equal(2, (await tradeService.GetAllAsync()).Count);
            }
        }

    }
}
