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
                new BidList() { Account = "Account Test 1", Type = "Type Test 1" },
                new BidList() { Account = "Account Test 2", Type = "Type Test 2" },
                new BidList() { Account = "Account Test 3", Type = "Type Test 3" }
            };
            for (int i = 1; i <= seedData.Count; i++)
            {
                seedData[i - 1].SetBidListId(i);
            }

            var bidListRepositoy = new Mock<IBidListRepository>();
            bidListRepositoy.Setup(x => x.GetAllAsync()).ReturnsAsync(seedData);

            // Act
            IBidListService bidListService = new BidListService(bidListRepositoy.Object);
            var bidListResult = await bidListService.GetAllAsync();

            // Assert
            Assert.NotNull(bidListResult);
            Assert.Equal(3, bidListResult.Count);
            Assert.Equal(2, bidListResult[1].BidListId);
        }
        [Fact]
        public async void GetByIdAsync_PassInt_ShouldReturnsSingleBidList()
        {
            // Arrange
            BidList seedData = new BidList() { Account = "Account Test 1", Type = "Type Test 1" };
            seedData.SetBidListId(1);

            var bidListRepositoy = new Mock<IBidListRepository>();
            bidListRepositoy.Setup(x => x.GetByIdAsync(seedData.BidListId)).ReturnsAsync(seedData);

            // Act
            IBidListService bidListService = new BidListService(bidListRepositoy.Object);
            var bidListResult = await bidListService.GetByIdAsync(1);

            // Assert
            Assert.NotNull(bidListResult);
            Assert.Equal(1, bidListResult.BidListId);
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
            BidList seedData = new BidList() { Account = "Account Test 1", Type = "Type Test 1" };
            seedData.SetBidListId(1);
            var bidListRepositoy = new Mock<IBidListRepository>();
            bidListRepositoy.Setup(x => x.AddAsync(seedData)).ReturnsAsync(seedData);

            // Act
            IBidListService bidListService = new BidListService(bidListRepositoy.Object);
            var bidListResult = await bidListService.AddAsync(seedData);

            // Assert
            Assert.NotNull(bidListResult);
            Assert.Equal(seedData.BidListId, bidListResult.BidListId);
            Assert.Equal(seedData.Account, bidListResult.Account);
            Assert.Equal(seedData.Type, bidListResult.Type);
        }
        [Fact]
        public async void UpdateAsync_PassBidList_ShouldReturnSameBidList()
        {
            // Arrange
            int idToUpdate = 1;
            BidList seedData = new BidList() { Account = "Account Test 1", Type = "Type Test 1" };
            seedData.SetBidListId(idToUpdate);

            var bidListRepositoy = new Mock<IBidListRepository>();
            bidListRepositoy.Setup(x => x.GetByIdAsync(idToUpdate)).ReturnsAsync(seedData);
            bidListRepositoy.Setup(x => x.UpdateAsync(seedData)).ReturnsAsync(seedData);

            // Act
            IBidListService bidListService = new BidListService(bidListRepositoy.Object);
            var bidListResult = await bidListService.UpdateAsync(seedData);

            // Assert
            Assert.NotNull(bidListResult);
            Assert.Equal(seedData.BidListId, bidListResult.BidListId);
            Assert.Equal(seedData.Account, bidListResult.Account);
            Assert.Equal(seedData.Type, bidListResult.Type);
        }
        [Fact]
        public async void DeleteAsync_PassBidList_ShouldReturnSameBidList()
        {
            // Arrange
            BidList seedData = new BidList() { Account = "Account Test 1", Type = "Type Test 1" };
            seedData.SetBidListId(1);
            var bidListRepositoy = new Mock<IBidListRepository>();
            bidListRepositoy.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(seedData);
            bidListRepositoy.Setup(x => x.DeleteAsync(seedData)).ReturnsAsync(seedData);

            // Act
            IBidListService bidListService = new BidListService(bidListRepositoy.Object);
            var bidListResult = await bidListService.DeleteAsync(1);

            // Assert
            Assert.NotNull(bidListResult);
            Assert.Equal(seedData.BidListId, bidListResult.BidListId);
            Assert.Equal(seedData.Account, bidListResult.Account);
            Assert.Equal(seedData.Type, bidListResult.Type);
        }
    }
}