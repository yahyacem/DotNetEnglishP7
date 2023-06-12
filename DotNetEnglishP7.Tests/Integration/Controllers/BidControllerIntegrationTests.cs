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

namespace DotNetEnglishP7.Tests.Integration.Controllers
{
    [Collection("Sequential")]
    public class BidServiceIntegrationTests : IntegrationTests
    {
        /// <summary>
        /// Test API call of type GET on endpoint /BidList. It should return the list of all BidList.
        /// </summary>
        [Fact]
        public async void GET_Home_ShouldReturnListBidList()
        {
            using (var context = new LocalDbContext(GetOptions()))
            {
                // Arrange
                List<BidList> seedData = new List<BidList>()
                {
                    new BidList() { Id = 1, Account = "Account Test 1", Type = "Type Test 1" },
                    new BidList() { Id = 2, Account = "Account Test 2", Type = "Type Test 2" },
                    new BidList() { Id = 3, Account = "Account Test 3", Type = "Type Test 3" }
                };
                context.BidLists.AddRange(seedData);
                context.SaveChanges();

                // Instantiate repository, service and controller
                var bidListRepositoy = new BidListRepository(context);
                IBidListService bidListService = new BidListService(bidListRepositoy);
                var signInManager = new Mock<SignInManager<AppUser>>();
                var userManager = new Mock<UserManager<AppUser>>();
                var logger = new Mock<ILogger<BidListController>>();
                var controller = new BidListController(new FakeSignInManager(false), new FakeUserManager(), logger.Object, bidListService);

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
                Assert.Equal(seedData.Count, ((List<BidList>)okResult.Value).Count);
                Assert.Equal(seedData[0].Account, ((List<BidList>)okResult.Value).First(x => x.Id == 1).Account);
                Assert.Equal(seedData[0].Type, ((List<BidList>)okResult.Value).First(x => x.Id == 1).Type);
            }
        }
        /// <summary>
        /// Test API call of type GET on endpoint /BidList/{id}. It should return the requested BidList.
        /// </summary>
        [Fact]
        public async void GET_GetById_PassInt_ShouldReturnSameBidList()
        {
            using (var context = new LocalDbContext(GetOptions()))
            {
                // Arrange
                int idToReturn = 1;
                BidList seedData = new BidList() { Account = "Account Test 1", Type = "Type Test 1" };
                context.BidLists.Add(seedData);
                context.SaveChanges();

                // Instantiate repository, service and controller
                var bidListRepositoy = new BidListRepository(context);
                IBidListService bidListService = new BidListService(bidListRepositoy);
                var signInManager = new Mock<SignInManager<AppUser>>();
                var userManager = new Mock<UserManager<AppUser>>();
                var logger = new Mock<ILogger<BidListController>>();
                var controller = new BidListController(new FakeSignInManager(false), new FakeUserManager(), logger.Object, bidListService);

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
                Assert.Equal(seedData.Id, ((BidList)okResult.Value).Id);
                Assert.Equal(seedData.Account, ((BidList)okResult.Value).Account);
                Assert.Equal(seedData.Type, ((BidList)okResult.Value).Type);
            }
        }
        /// <summary>
        /// Test API call of type GET on endpoint /BidList/{id}. It should return NotFound.
        /// </summary>
        [Fact]
        public async void GET_GetById_PassInt_ShouldReturnNull()
        {
            using (var context = new LocalDbContext(GetOptions()))
            {
                // Arrange
                int idToReturn = 1;

                // Instantiate repository, service and controller
                var bidListRepositoy = new BidListRepository(context);
                IBidListService bidListService = new BidListService(bidListRepositoy);
                var signInManager = new Mock<SignInManager<AppUser>>();
                var userManager = new Mock<UserManager<AppUser>>();
                var logger = new Mock<ILogger<BidListController>>();
                var controller = new BidListController(new FakeSignInManager(false), new FakeUserManager(), logger.Object, bidListService);

                // Act
                // Make the call and capture result
                var actionResult = await controller.GetById(idToReturn);
                var notFoundResult = actionResult as NotFoundResult;

                // Assert
                // Check response code
                Assert.NotNull(notFoundResult);
                Assert.Equal(404, notFoundResult.StatusCode);
                // Check data in database
                Assert.Empty(await bidListService.GetAllAsync());
                Assert.Null(await bidListService.GetByIdAsync(idToReturn));
            }
        }
        /// <summary>
        /// Test API call of type POST on endpoint /BidList/Add. It should return the created BidList.
        /// </summary>
        [Fact]
        public async void POST_Add_PassBidList_ShouldReturnSameBidList()
        {
            using (var context = new LocalDbContext(GetOptions()))
            {
                // Arrange
                int idToCreate = 1;
                BidList seedData = new BidList() { Account = "Account Test 1", Type = "Type Test 1" };

                // Instantiate repository, service and controller
                var bidListRepositoy = new BidListRepository(context);
                IBidListService bidListService = new BidListService(bidListRepositoy);
                var signInManager = new Mock<SignInManager<AppUser>>();
                var userManager = new Mock<UserManager<AppUser>>();
                var logger = new Mock<ILogger<BidListController>>();
                var controller = new BidListController(new FakeSignInManager(false), new FakeUserManager(), logger.Object, bidListService);

                // Act
                // Make the call and capture result
                var actionResult = await controller.Add(seedData);
                var okResult = actionResult as CreatedAtActionResult;

                // Check response code
                Assert.NotNull(okResult);
                Assert.NotNull(okResult.Value);
                Assert.Equal(201, okResult.StatusCode);
                // Check response data
                Assert.Equal(seedData.Id, ((BidList)okResult.Value).Id);
                Assert.Equal(seedData.Account, ((BidList)okResult.Value).Account);
                Assert.Equal(seedData.Type, ((BidList)okResult.Value).Type);
                // Check data in database
                Assert.Single(await bidListService.GetAllAsync());
                Assert.NotNull(await bidListService.GetByIdAsync(idToCreate));
            }
        }
        /// <summary>
        /// Test API call of type POST on endpoint /BidList/Update. It should return the updated BidList.
        /// </summary>
        [Fact]
        public async void PUT_Update_PassBidList_ShouldReturnSameBidList()
        {
            using (var context = new LocalDbContext(GetOptions()))
            {
                // Arrange
                int idToUpdate = 1;
                // Insert seed data
                BidList seedData = new BidList() { Id = idToUpdate, Account = "Account Test 1", Type = "Type Test 1" };
                context.BidLists.Add(seedData);
                context.SaveChanges();
                // Prepare new data to update
                BidList updatedData = new BidList() { Account = "Account Test 2", Type = "Type Test 2" };

                // Instantiate repository, service and controller
                var bidListRepositoy = new BidListRepository(context);
                IBidListService bidListService = new BidListService(bidListRepositoy);
                var signInManager = new Mock<SignInManager<AppUser>>();
                var userManager = new Mock<UserManager<AppUser>>();
                var logger = new Mock<ILogger<BidListController>>();
                var controller = new BidListController(new FakeSignInManager(false), new FakeUserManager(), logger.Object, bidListService);

                // Act
                // Make the call and capture result
                var actionResult = await controller.Update(idToUpdate, updatedData);
                var okResult = actionResult as OkObjectResult;

                // Check response code
                Assert.NotNull(okResult);
                Assert.NotNull(okResult.Value);
                Assert.Equal(200, okResult.StatusCode);
                // Check response data
                Assert.Equal(updatedData.Id, ((BidList)okResult.Value).Id);
                Assert.Equal(updatedData.Account, ((BidList)okResult.Value).Account);
                Assert.Equal(updatedData.Type, ((BidList)okResult.Value).Type);
                // Check data in database
                var bidListUpdated = await bidListService.GetByIdAsync(idToUpdate);
                Assert.NotNull(bidListUpdated);
                Assert.Equal(updatedData.Account, bidListUpdated.Account);
            }
        }
        /// <summary>
        /// Test API call of type DELETE on endpoint /BidList/Delete. It should return the deleted BidList.
        /// </summary>
        [Fact]
        public async void DELETE_Delete_PassBidList_ShouldReturnSameBidList()
        {
            using (var context = new LocalDbContext(GetOptions()))
            {
                // Arrange
                int idToDelete = 1;
                // Insert seed data
                List<BidList> seedData = new List<BidList>()
                {
                    new BidList() { Id = 1, Account = "Account Test 1", Type = "Type Test 1" },
                    new BidList() { Id = 2, Account = "Account Test 2", Type = "Type Test 2" },
                    new BidList() { Id = 3, Account = "Account Test 3", Type = "Type Test 3" }
                };
                context.BidLists.AddRange(seedData);
                context.SaveChanges();

                // Instantiate repository, service and controller
                var bidListRepositoy = new BidListRepository(context);
                IBidListService bidListService = new BidListService(bidListRepositoy);
                var logger = new Mock<ILogger<BidListController>>();
                logger.Setup(x => x.Log(LogLevel.Information, It.IsAny<string>(), It.IsAny<string>())).Verifiable();
                //logger.Verify(logger => logger.Log(
                //    It.Is<LogLevel>(logLevel => logLevel == LogLevel.Information),
                //    It.Is<EventId>(eventId => eventId.Id == 0),
                //    It.Is<It.IsAnyType>((@object, @type) => @object.ToString() == "myMessage" && @type.Name == "FormattedLogValues"),
                //    It.IsAny<Exception>(),
                //    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                //Times.Once);
                //logger.Setup(x => x.LogInformation(It.IsAny<string>())).Verifiable();
                var controller = new BidListController(new FakeSignInManager(true), new FakeUserManager(), logger.Object, bidListService);

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
                Assert.Equal(seedData.First(x => x.Id == idToDelete).Id, ((BidList)okResult.Value).Id);
                Assert.Equal(seedData.First(x => x.Id == idToDelete).Account, ((BidList)okResult.Value).Account);
                Assert.Equal(seedData.First(x => x.Id == idToDelete).Type, ((BidList)okResult.Value).Type);
                // Check data in database
                Assert.Null(await bidListService.GetByIdAsync(idToDelete));
                Assert.Equal(2, (await bidListService.GetAllAsync()).Count);
            }
        }
    }
}