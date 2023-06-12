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
    [Collection("Sequential")]
    public class TradeServiceUnitTests : UnitTests
    {
        [Fact]
        public async void GetAllAsync_ShouldReturnListTrade()
        {
            // Arrange
            List<Trade> seedData = new List<Trade>()
            {
                new Trade() { Id = 1, Account = "Test Account 1" },
                new Trade() { Id = 2, Account = "Test Account 2" },
                new Trade() { Id = 3, Account = "Test Account 3" }
            };

            var tradeRepository = new Mock<ITradeRepository>();
            tradeRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(seedData);

            // Act
            ITradeService tradeService = new TradeService(tradeRepository.Object);
            var tradeResult = await tradeService.GetAllAsync();

            // Assert
            Assert.NotNull(tradeResult);
            Assert.Equal(3, tradeResult.Count);
            Assert.Equal("Test Account 2", tradeResult[1].Account);
        }
        [Fact]
        public async void GetByIdAsync_PassInt_ShouldReturnSingleTrade()
        {
            // Arrange
            int idToGet = 1;
            Trade seedData = new Trade() { Id = idToGet, Account = "Test Account 2" };

            var tradeRepository = new Mock<ITradeRepository>();
            tradeRepository.Setup(x => x.GetByIdAsync(idToGet)).ReturnsAsync(seedData);

            // Act
            ITradeService tradeService = new TradeService(tradeRepository.Object);
            var tradeResult = await tradeService.GetByIdAsync(idToGet);

            // Assert
            Assert.NotNull(tradeResult);
            Assert.Equal(idToGet, tradeResult.Id);
            Assert.Equal("Test Account 2", tradeResult.Account);
        }
        [Fact]
        public async void GetByIdAsync_PassInt_ShouldReturnNull()
        {
            // Arrange
            var tradeRepository = new Mock<ITradeRepository>();

            // Act
            ITradeService tradeService = new TradeService(tradeRepository.Object);
            var tradeResult = await tradeService.GetByIdAsync(1);

            // Assert
            Assert.Null(tradeResult);
        }
        [Fact]
        public async void AddAsync_PassTrade_ShouldReturnSameCurveTrade()
        {
            // Arrange
            int idToAdd = 1;
            Trade seedData = new Trade() { Id = idToAdd, Account = "Test Account 2" };

            var tradeRepository = new Mock<ITradeRepository>();
            tradeRepository.Setup(x => x.AddAsync(seedData)).ReturnsAsync(seedData);

            // Act
            ITradeService tradeService = new TradeService(tradeRepository.Object);
            var tradeResult = await tradeService.AddAsync(seedData);

            // Assert
            Assert.NotNull(tradeResult);
            Assert.Equal(idToAdd, tradeResult.Id);
            Assert.Equal("Test Account 2", tradeResult.Account);
        }
        [Fact]
        public async void UpdateAsync_PassTrade_ShouldReturnSameTrade()
        {
            // Arrange
            int idToUpdate = 1;
            Trade seedData = new Trade() { Id = idToUpdate, Account = "Test Account 2" };

            var tradeRepository = new Mock<ITradeRepository>();
            tradeRepository.Setup(x => x.GetByIdAsync(idToUpdate)).ReturnsAsync(seedData);
            tradeRepository.Setup(x => x.UpdateAsync(seedData)).ReturnsAsync(seedData);

            // Act
            ITradeService tradeService = new TradeService(tradeRepository.Object);
            var tradeResult = await tradeService.UpdateAsync(seedData);

            // Assert
            Assert.NotNull(tradeResult);
            Assert.Equal(1, tradeResult.Id);
            Assert.Equal("Test Account 2", tradeResult.Account);
        }
        [Fact]
        public async void DeleteAsync_PassTrade_ShouldReturnSameTrade()
        {
            // Arrange
            int idToDelete = 1;
            Trade seedData = new Trade() { Id = idToDelete, Account = "Test Account 2" };

            var tradeRepository = new Mock<ITradeRepository>();
            tradeRepository.Setup(x => x.GetByIdAsync(idToDelete)).ReturnsAsync(seedData);
            tradeRepository.Setup(x => x.DeleteAsync(seedData)).ReturnsAsync(seedData);

            // Act
            ITradeService tradeService = new TradeService(tradeRepository.Object);
            var tradeResult = await tradeService.DeleteAsync(idToDelete);

            // Assert
            Assert.NotNull(tradeResult);
            Assert.Equal(idToDelete, tradeResult.Id);
            Assert.Equal("Test Account 2", tradeResult.Account);
        }
    }
}