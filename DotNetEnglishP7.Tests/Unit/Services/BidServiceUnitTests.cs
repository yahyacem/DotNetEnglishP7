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
    public class BidServiceUnitTests : UnitTests
    {
        [Fact]
        public async void GetAllAsync_ShouldReturnListBidList()
        {
            // Arrange
            List<BidList> seedData = new List<BidList>()
            {
                new BidList() { Id = 1, Account = "Account Test 1", Type = "Type Test 1" },
                new BidList() { Id = 2, Account = "Account Test 2", Type = "Type Test 2" },
                new BidList() { Id = 3, Account = "Account Test 3", Type = "Type Test 3" }
            };

            var bidListRepositoy = new Mock<IBidListRepository>();
            bidListRepositoy.Setup(x => x.GetAllAsync()).ReturnsAsync(seedData);

            // Act
            IBidListService bidListService = new BidListService(bidListRepositoy.Object);
            var bidListResult = await bidListService.GetAllAsync();

            // Assert
            Assert.NotNull(bidListResult);
            Assert.Equal(3, bidListResult.Count);
            Assert.Equal(2, bidListResult[1].Id);
        }
        [Fact]
        public async void GetByIdAsync_PassInt_ShouldReturnsSingleBidList()
        {
            // Arrange
            int idToGet = 1;
            BidList seedData = new BidList() { Id = idToGet, Account = "Account Test 1", Type = "Type Test 1" };

            var bidListRepositoy = new Mock<IBidListRepository>();
            bidListRepositoy.Setup(x => x.GetByIdAsync(seedData.Id)).ReturnsAsync(seedData);

            // Act
            IBidListService bidListService = new BidListService(bidListRepositoy.Object);
            var bidListResult = await bidListService.GetByIdAsync(idToGet);

            // Assert
            Assert.NotNull(bidListResult);
            Assert.Equal(idToGet, bidListResult.Id);
            Assert.Equal("Account Test 1", bidListResult.Account);
        }
        [Fact]
        public async void GetByIdAsync_PassInt_ShouldReturnNull()
        {
            // Arrange
            var bidListRepositoy = new Mock<IBidListRepository>();

            // Act
            IBidListService bidListService = new BidListService(bidListRepositoy.Object);
            var bidListResult = await bidListService.GetByIdAsync(1);

            // Assert
            Assert.Null(bidListResult);
        }
        [Fact]
        public async void AddAsync_PassBidList_ShouldReturnSameBidList()
        {
            // Arrange
            int idToAdd = 1;
            BidList seedData = new BidList() { Id = idToAdd, Account = "Account Test 1", Type = "Type Test 1" };

            var bidListRepositoy = new Mock<IBidListRepository>();
            bidListRepositoy.Setup(x => x.AddAsync(seedData)).ReturnsAsync(seedData);

            // Act
            IBidListService bidListService = new BidListService(bidListRepositoy.Object);
            var bidListResult = await bidListService.AddAsync(seedData);

            // Assert
            Assert.NotNull(bidListResult);
            Assert.Equal(seedData.Id, bidListResult.Id);
            Assert.Equal(seedData.Account, bidListResult.Account);
            Assert.Equal(seedData.Type, bidListResult.Type);
        }
        [Fact]
        public async void UpdateAsync_PassBidList_ShouldReturnSameBidList()
        {
            // Arrange
            int idToUpdate = 1;
            BidList seedData = new BidList() { Id = idToUpdate, Account = "Account Test 1", Type = "Type Test 1" };

            var bidListRepositoy = new Mock<IBidListRepository>();
            bidListRepositoy.Setup(x => x.GetByIdAsync(idToUpdate)).ReturnsAsync(seedData);
            bidListRepositoy.Setup(x => x.UpdateAsync(seedData)).ReturnsAsync(seedData);

            // Act
            IBidListService bidListService = new BidListService(bidListRepositoy.Object);
            var bidListResult = await bidListService.UpdateAsync(seedData);

            // Assert
            Assert.NotNull(bidListResult);
            Assert.Equal(seedData.Id, bidListResult.Id);
            Assert.Equal(seedData.Account, bidListResult.Account);
            Assert.Equal(seedData.Type, bidListResult.Type);
        }
        [Fact]
        public async void DeleteAsync_PassBidList_ShouldReturnSameBidList()
        {
            // Arrange
            int idToDelete = 1;
            BidList seedData = new BidList() { Id = idToDelete, Account = "Account Test 1", Type = "Type Test 1" };
            var bidListRepositoy = new Mock<IBidListRepository>();
            bidListRepositoy.Setup(x => x.GetByIdAsync(idToDelete)).ReturnsAsync(seedData);
            bidListRepositoy.Setup(x => x.DeleteAsync(seedData)).ReturnsAsync(seedData);

            // Act
            IBidListService bidListService = new BidListService(bidListRepositoy.Object);
            var bidListResult = await bidListService.DeleteAsync(idToDelete);

            // Assert
            Assert.NotNull(bidListResult);
            Assert.Equal(seedData.Id, bidListResult.Id);
            Assert.Equal(seedData.Account, bidListResult.Account);
            Assert.Equal(seedData.Type, bidListResult.Type);
        }
    }
}