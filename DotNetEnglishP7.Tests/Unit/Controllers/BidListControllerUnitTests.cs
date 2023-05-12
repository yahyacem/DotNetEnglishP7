using Dot.Net.WebApi.Controllers;
using Dot.Net.WebApi.Domain;
using DotNetEnglishP7.Repositories;
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
    [Collection("Sequential")]
    public class BidListControllerUnitTests : UnitTests
    {
        /// <summary>
        /// Test API call of type GET on endpoint /BidList. It should return the list of all BidList.
        /// </summary>
        [Fact]
        public async void Get_Home_ShouldReturnListBidList()
        {
            // Arrange
            List<BidList> seedData = new List<BidList>()
            {
                new BidList()
                {
                    Account = "Account Test 1",
                    Type = "Type Test 1"
                },
                new BidList()
                {
                    Account = "Account Test 2",
                    Type = "Type Test 2"
                },
                new BidList()
                {
                    Account = "Account Test 3",
                    Type = "Type Test 3"
                }
            };
            for (int i = 1; i <= seedData.Count; i++)
            {
                seedData[i - 1].SetBidListId(i);
            }

            var bidListService = new Mock<IBidListService>();
            bidListService.Setup(x => x.GetAllAsync()).ReturnsAsync(seedData);
            var controller = new BidListController(bidListService.Object);

            // Act
            var actionResult = await controller.Home();
            var okResult = actionResult as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.NotNull(okResult.Value);
            Assert.Equal(seedData.Count, ((List<BidList>)okResult.Value).Count);
            Assert.Equal(200, okResult.StatusCode);
        }
        /// <summary>
        /// Test API call of type GET on endpoint /BidList/{id}. It should return the requested BidList.
        /// </summary>
        [Fact]
        public async void Get_GetById_PassInt_ShouldReturnSingleBidList()
        {
            // Arrange
            int idToReturn = 1;
            BidList seedData = new BidList()
            {
                Account = "Account Test 1",
                Type = "Type Test 1"
            };
            seedData.SetBidListId(idToReturn);

            var bidListService = new Mock<IBidListService>();
            bidListService.Setup(x => x.GetByIdAsync(idToReturn)).ReturnsAsync(seedData);
            var controller = new BidListController(bidListService.Object);

            // Act
            var actionResult = await controller.GetById(idToReturn);
            var okResult = actionResult as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.NotNull(okResult.Value);
            Assert.Equal(seedData.Account, ((BidList)okResult.Value).Account);
            Assert.Equal(200, okResult.StatusCode);
        }
        /// <summary>
        /// Test API call of type GET on endpoint /BidList/{id}. It should return NotFound.
        /// </summary>
        [Fact]
        public async void Get_GetById_PassInt_ShouldReturnNotFound()
        {
            // Arrange
            var bidListService = new Mock<IBidListService>();
            var controller = new BidListController(bidListService.Object);

            // Act
            var actionResult = await controller.GetById(1);
            var notFoundResult = actionResult as NotFoundResult;

            // Assert
            Assert.NotNull(notFoundResult);
            Assert.Equal(404, notFoundResult.StatusCode);
        }
        /// <summary>
        /// Test API call of type POST on endpoint /BidList/Add. It should return the created BidList.
        /// </summary>
        [Fact]
        public async void Post_Add_PassBidList_ShouldReturnSameBidList()
        {
            // Arrange
            int idToCreate = 1;
            BidList seedData = new BidList()
            {
                Account = "Account Test 1",
                Type = "Type Test 1"
            };
            seedData.SetBidListId(idToCreate);

            var bidListService = new Mock<IBidListService>();
            bidListService.Setup(x => x.AddAsync(seedData)).ReturnsAsync(seedData);
            var controller = new BidListController(bidListService.Object);

            // Act
            var actionResult = await controller.Add(seedData);
            var okResult = actionResult as CreatedAtActionResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.NotNull(okResult.Value);
            Assert.Equal(seedData.Account, ((BidList)okResult.Value).Account);
            Assert.Equal(201, okResult.StatusCode);
        }
        /// <summary>
        /// Test API call of type POST on endpoint /BidList/Update. It should return the updated BidList.
        /// </summary>
        [Fact]
        public async void Post_Update_PassBidList_ShouldReturnSameBidList()
        {
            // Arrange
            int idToUpdate = 1;
            BidList seedData = new BidList()
            {
                Account = "Account Test 1",
                Type = "Type Test 1"
            };
            seedData.SetBidListId(idToUpdate);

            var bidListService = new Mock<IBidListService>();
            bidListService.Setup(x => x.UpdateAsync(seedData)).ReturnsAsync(seedData);
            var controller = new BidListController(bidListService.Object);

            // Act
            var actionResult = await controller.Update(idToUpdate, seedData);
            var okResult = actionResult as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.NotNull(okResult.Value);
            Assert.Equal(seedData.Account, ((BidList)okResult.Value).Account);
            Assert.Equal(200, okResult.StatusCode);
        }
        /// <summary>
        /// Test API call of type DELETE on endpoint /BidList/Delete. It should return the deleted BidList.
        /// </summary>
        [Fact]
        public async void Delete_Delete_PassBidList_ShouldReturnSameBidList()
        {
            // Arrange
            int idToDelete = 1;
            BidList seedData = new BidList()
            {
                Account = "Account Test 1",
                Type = "Type Test 1"
            };
            seedData.SetBidListId(idToDelete);

            var bidListService = new Mock<IBidListService>();
            bidListService.Setup(x => x.DeleteAsync(idToDelete)).ReturnsAsync(seedData);
            var controller = new BidListController(bidListService.Object);

            // Act
            var actionResult = await controller.Delete(idToDelete);
            var okResult = actionResult as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.NotNull(okResult.Value);
            Assert.Equal(seedData.Account, ((BidList)okResult.Value).Account);
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}