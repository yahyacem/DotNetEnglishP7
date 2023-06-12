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
    public class TradeServiceIntegrationTests : IntegrationTests
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
                    new Trade() { Id = 1, Account = "Account Test 1", Type = "Type Test 1" },
                    new Trade() { Id = 2, Account = "Account Test 2", Type = "Type Test 2" },
                    new Trade() { Id = 3, Account = "Account Test 3", Type = "Type Test 3" }
                };
                context.Trades.AddRange(seedData);
                context.SaveChanges();

                // Instantiate repository, service and controller
                var tradeRepositoy = new TradeRepository(context);
                ITradeService tradeService = new TradeService(tradeRepositoy);

                // Act
                // Make the call and capture result
                var result = await tradeService.GetAllAsync();

                // Assert
                // Check response data
                Assert.Equal(seedData.Count, result.Count);
                Assert.Equal(seedData[0].Account, result.First(x => x.Id == 1).Account);
                Assert.Equal(seedData[0].Type, result.First(x => x.Id == 1).Type);
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

                // Act
                // Make the call and capture result
                var result = await tradeService.GetByIdAsync(idToReturn);

                // Assert
                // Check response data
                Assert.NotNull(result);
                Assert.Equal(seedData.Id, result.Id);
                Assert.Equal(seedData.Account, result.Account);
                Assert.Equal(seedData.Type, result.Type);
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

                // Act
                // Make the call and capture result
                var result = await tradeService.GetByIdAsync(idToReturn);

                // Assert
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

                // Act
                // Make the call and capture result
                var result = await tradeService.AddAsync(seedData);

                // Assert
                // Check response data
                Assert.NotNull(result);
                Assert.Equal(seedData.Id, result.Id);
                Assert.Equal(seedData.Account, result.Account);
                Assert.Equal(seedData.Type, result.Type);
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
                Trade seedData = new Trade() { Id = idToUpdate, Account = "Account Test 1", Type = "Type Test 1" };
                context.Trades.Add(seedData);
                context.SaveChanges();
                // Prepare new data to update
                Trade updatedData = new Trade() { Id = idToUpdate, Account = "Account Test 2", Type = "Type Test 2" };

                // Instantiate repository, service and controller
                var tradeRepositoy = new TradeRepository(context);
                ITradeService tradeService = new TradeService(tradeRepositoy);

                // Act
                // Make the call and capture result
                var result = await tradeService.UpdateAsync(updatedData);

                // Assert
                // Check response data
                Assert.NotNull(result);
                Assert.Equal(updatedData.Id, result.Id);
                Assert.Equal(updatedData.Account, result.Account);
                Assert.Equal(updatedData.Type, result.Type);
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
                    new Trade() { Id = 1, Account = "Account Test 1", Type = "Type Test 1" },
                    new Trade() { Id = 2, Account = "Account Test 2", Type = "Type Test 2" },
                    new Trade() { Id = 3, Account = "Account Test 3", Type = "Type Test 3" }
                };
                context.Trades.AddRange(seedData);
                context.SaveChanges();

                // Instantiate repository, service and controller
                var tradeRepositoy = new TradeRepository(context);
                ITradeService tradeService = new TradeService(tradeRepositoy);

                // Act
                // Make the call and capture result
                var result = await tradeService.DeleteAsync(idToDelete);

                // Assert
                // Check response data
                Assert.NotNull(result);
                Assert.Equal(seedData.First(x => x.Id == idToDelete).Id, result.Id);
                Assert.Equal(seedData.First(x => x.Id == idToDelete).Account, result.Account);
                Assert.Equal(seedData.First(x => x.Id == idToDelete).Type, result.Type);
                // Check data in database
                Assert.Null(await tradeService.GetByIdAsync(idToDelete));
                Assert.Equal(2, (await tradeService.GetAllAsync()).Count);
            }
        }

    }
}
