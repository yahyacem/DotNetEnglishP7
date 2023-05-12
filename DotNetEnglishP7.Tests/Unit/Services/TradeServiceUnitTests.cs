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
                new Trade() { Account = "Test Account 1" },
                new Trade() { Account = "Test Account 2" },
                new Trade() { Account = "Test Account 3" }
            };
            for (int i = 1; i <= seedData.Count; i++)
            {
                seedData[i - 1].SetTradeId(i);
            }

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
            Trade seedData = new Trade() { Account = "Test Account 2" };
            seedData.SetTradeId(1);

            var tradeRepository = new Mock<ITradeRepository>();
            tradeRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(seedData);

            // Act
            ITradeService tradeService = new TradeService(tradeRepository.Object);
            var tradeResult = await tradeService.GetByIdAsync(1);

            // Assert
            Assert.NotNull(tradeResult);
            Assert.Equal(1, tradeResult.TradeId);
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
            Trade seedData = new Trade() { Account = "Test Account 2" };
            seedData.SetTradeId(1);

            var tradeRepository = new Mock<ITradeRepository>();
            tradeRepository.Setup(x => x.AddAsync(seedData)).ReturnsAsync(seedData);

            // Act
            ITradeService tradeService = new TradeService(tradeRepository.Object);
            var tradeResult = await tradeService.AddAsync(seedData);

            // Assert
            Assert.NotNull(tradeResult);
            Assert.Equal(1, tradeResult.TradeId);
            Assert.Equal("Test Account 2", tradeResult.Account);
        }
        [Fact]
        public async void UpdateAsync_PassTrade_ShouldReturnSameTrade()
        {
            // Arrange
            int idToUpdate = 1;
            Trade seedData = new Trade() { Account = "Test Account 2" };
            seedData.SetTradeId(1);

            var tradeRepository = new Mock<ITradeRepository>();
            tradeRepository.Setup(x => x.GetByIdAsync(idToUpdate)).ReturnsAsync(seedData);
            tradeRepository.Setup(x => x.UpdateAsync(seedData)).ReturnsAsync(seedData);

            // Act
            ITradeService tradeService = new TradeService(tradeRepository.Object);
            var tradeResult = await tradeService.UpdateAsync(seedData);

            // Assert
            Assert.NotNull(tradeResult);
            Assert.Equal(1, tradeResult.TradeId);
            Assert.Equal("Test Account 2", tradeResult.Account);
        }
        [Fact]
        public async void DeleteAsync_PassTrade_ShouldReturnSameTrade()
        {
            // Arrange
            Trade seedData = new Trade() { Account = "Test Account 2" };
            seedData.SetTradeId(1);

            var tradeRepository = new Mock<ITradeRepository>();
            tradeRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(seedData);
            tradeRepository.Setup(x => x.DeleteAsync(seedData)).ReturnsAsync(seedData);

            // Act
            ITradeService tradeService = new TradeService(tradeRepository.Object);
            var tradeResult = await tradeService.DeleteAsync(1);

            // Assert
            Assert.NotNull(tradeResult);
            Assert.Equal(1, tradeResult.TradeId);
            Assert.Equal("Test Account 2", tradeResult.Account);
        }
    }
}