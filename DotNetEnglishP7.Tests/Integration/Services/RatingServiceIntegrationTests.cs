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
    public class RatingServiceIntegrationTests : IntegrationTests
    {
        /// <summary>
        /// Test API call of type GET on endpoint /Rating. It should return the list of all Ratings.
        /// </summary>
        [Fact]
        public async void GET_Home_ShouldReturnListRating()
        {
            using (var context = new LocalDbContext(GetOptions()))
            {
                // Arrange
                List<Rating> seedData = new List<Rating>()
                {
                    new Rating() { Id = 1, OrderNumber = 5 },
                    new Rating() { Id = 2, OrderNumber = 10 },
                    new Rating() { Id = 3, OrderNumber = 3 }
                };
                context.Ratings.AddRange(seedData);
                context.SaveChanges();

                // Instantiate repository, service and controller
                IRatingRepository ratingRepository = new RatingRepository(context);
                IRatingService ratingService = new RatingService(ratingRepository);

                // Act
                var result = await ratingService.GetAllAsync();

                // Assert
                // Check response data
                Assert.Equal(seedData.Count, result.Count);
            }
        }
        /// <summary>
        /// Test API call of type GET on endpoint /Rating/{id}. It should return the requested Rating.
        /// </summary>
        [Fact]
        public async void GET_GetById_PassInt_ShouldReturnSingleRating()
        {
            using (var context = new LocalDbContext(GetOptions()))
            {
                // Arrange
                int idToReturn = 1;
                Rating seedData = new Rating() { Id = idToReturn, OrderNumber = 5 };
                context.Ratings.Add(seedData);
                context.SaveChanges();

                // Instantiate repository, service and controller
                IRatingRepository ratingRepository = new RatingRepository(context);
                IRatingService ratingService = new RatingService(ratingRepository);

                // Act
                var result = await ratingService.GetByIdAsync(idToReturn);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(seedData.OrderNumber, result.OrderNumber);
            }
        }
        /// <summary>
        /// Test API call of type GET on endpoint /Rating/{id}. It should return NotFound.
        /// </summary>
        [Fact]
        public async void GET_GetById_PassInt_ShouldReturnNotFound()
        {
            using (var context = new LocalDbContext(GetOptions()))
            {
                // Arrange
                int idToReturn = 1;

                // Instantiate repository, service and controller
                IRatingRepository ratingRepository = new RatingRepository(context);
                IRatingService ratingService = new RatingService(ratingRepository);

                // Act
                var result = await ratingService.GetByIdAsync(idToReturn);

                // Assert
                Assert.Null(result);
            }
        }
        /// <summary>
        /// Test API call of type POST on endpoint /Rating/Add. It should return the created Rating.
        /// </summary>
        [Fact]
        public async void POST_Add_PassRating_ShouldReturnSameRating()
        {
            using (var context = new LocalDbContext(GetOptions()))
            {
                // Arrange
                int idToCreate = 1;
                Rating seedData = new Rating() { Id = idToCreate, OrderNumber = 5 };

                // Instantiate repository, service and controller
                IRatingRepository ratingRepository = new RatingRepository(context);
                IRatingService ratingService = new RatingService(ratingRepository);

                // Act
                var result = await ratingService.AddAsync(seedData);

                // Assert
                // Check response data
                Assert.NotNull(result);
                Assert.Equal(seedData.OrderNumber, result.OrderNumber);
                // Check data in database
                Assert.Single(await ratingService.GetAllAsync());
                Assert.NotNull(await ratingService.GetByIdAsync(idToCreate));
            }
        }
        /// <summary>
        /// Test API call of type POST on endpoint /Rating/Update. It should return the updated Rating.
        /// </summary>
        [Fact]
        public async void PUT_Update_PassRating_ShouldReturnSameRating()
        {
            using (var context = new LocalDbContext(GetOptions()))
            {
                // Arrange
                int idToUpdate = 1;
                Rating seedData = new Rating() { Id = idToUpdate, OrderNumber = 5 };
                context.Ratings.Add(seedData);
                context.SaveChanges();
                // Prepare new data to update
                Rating updatedSeedData = new Rating() { Id = idToUpdate, OrderNumber = 10 };

                // Instantiate repository, service and controller
                IRatingRepository ratingRepository = new RatingRepository(context);
                IRatingService ratingService = new RatingService(ratingRepository);

                // Act
                var result = await ratingRepository.UpdateAsync(updatedSeedData);

                // Assert
                // Check response data
                Assert.NotNull(result);
                Assert.Equal(seedData.OrderNumber, result.OrderNumber);
                // Check data in database
                var ratingUpdated = await ratingService.GetByIdAsync(idToUpdate);
                Assert.NotNull(ratingUpdated);
                Assert.Equal(updatedSeedData.OrderNumber, ratingUpdated.OrderNumber);
            }

        }
        /// <summary>
        /// Test API call of type DELETE on endpoint /Rating/Delete. It should return the deleted Rating.
        /// </summary>
        [Fact]
        public async void DELETE_Delete_PassRating_ShouldReturnSameRating()
        {
            using (var context = new LocalDbContext(GetOptions()))
            {
                // Arrange
                int idToDelete = 1;
                Rating seedData = new Rating() { Id = idToDelete, OrderNumber = 5 };
                context.Ratings.Add(seedData);
                context.SaveChanges();

                // Instantiate repository, service and controller
                IRatingRepository ratingRepository = new RatingRepository(context);
                IRatingService ratingService = new RatingService(ratingRepository);

                // Act
                var result = await ratingService.DeleteAsync(idToDelete);

                // Assert
                // Check response data
                Assert.NotNull(result);
                Assert.Equal(seedData.OrderNumber, result.OrderNumber);
                // Check data in database
                Assert.Empty(await ratingService.GetAllAsync());
                Assert.Null(await ratingService.GetByIdAsync(idToDelete));
            }
        }
    }
}
