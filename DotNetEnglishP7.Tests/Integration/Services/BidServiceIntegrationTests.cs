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

                // Act
                // Make the call and capture result
                var result = await bidListService.GetAllAsync();

                // Assert
                // Check response data
                Assert.Equal(seedData.Count, result.Count);
                Assert.Equal(seedData[0].Account, result.First(x => x.Id == 1).Account);
                Assert.Equal(seedData[0].Type, result.First(x => x.Id == 1).Type);
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

                // Act
                // Make the call and capture result
                var result = await bidListService.GetByIdAsync(idToReturn);

                // Assert
                // Check response data
                Assert.NotNull(result);
                Assert.Equal(seedData.Id, result.Id);
                Assert.Equal(seedData.Account, result.Account);
                Assert.Equal(seedData.Type, result.Type);
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

                // Act
                // Make the call and capture result
                var result = await bidListService.GetByIdAsync(idToReturn);

                // Assert
                // Check data in database
                Assert.Null(result);
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

                // Act
                // Make the call and capture result
                var result = await bidListService.AddAsync(seedData);

                // Check response data
                Assert.NotNull(result);
                Assert.Equal(seedData.Id, result.Id);
                Assert.Equal(seedData.Account, result.Account);
                Assert.Equal(seedData.Type, result.Type);
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
                BidList updatedData = new BidList() { Id = idToUpdate, Account = "Account Test 2", Type = "Type Test 2" };

                // Instantiate repository, service and controller
                var bidListRepositoy = new BidListRepository(context);
                IBidListService bidListService = new BidListService(bidListRepositoy);

                // Act
                // Make the call and capture result
                var result = await bidListService.UpdateAsync(updatedData);

                // Check response data
                Assert.NotNull(result);
                Assert.Equal(updatedData.Id, result.Id);
                Assert.Equal(updatedData.Account, result.Account);
                Assert.Equal(updatedData.Type, result.Type);
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

                // Act
                // Make the call and capture result
                var result = await bidListService.DeleteAsync(idToDelete);

                // Assert
                // Check response data
                Assert.NotNull(result);
                Assert.Equal(seedData.First(x => x.Id == idToDelete).Id, result.Id);
                Assert.Equal(seedData.First(x => x.Id == idToDelete).Account, result.Account);
                Assert.Equal(seedData.First(x => x.Id == idToDelete).Type, result.Type);
                // Check data in database
                Assert.Null(await bidListService.GetByIdAsync(idToDelete));
                Assert.Equal(2, (await bidListService.GetAllAsync()).Count);
            }
        }
    }
}