using Dot.Net.WebApi.Controllers;
using Dot.Net.WebApi.Domain;
using DotNetEnglishP7.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetEnglishP7.Tests.Integration.Controllers
{
    [Collection("Sequential")]
    public class RatingControllerIntegrationTests : IntegrationTests
    {
        /// <summary>
        /// Test API call of type GET on endpoint /Rating. It should return the list of all Ratings.
        /// </summary>
        [Fact]
        public async void Get_Home_ShouldReturnListRating()
        {
            // Arrange
            List<Rating> seedData = new List<Rating>()
            {
                new Rating() { OrderNumber = 5 },
                new Rating() { OrderNumber = 10 },
                new Rating() { OrderNumber = 3 }
            };
            for (int i = 1; i <= seedData.Count; i++)
            {
                seedData[i - 1].SetId(i);
            }

            var ratingService = new Mock<IRatingService>();
            ratingService.Setup(x => x.GetAllAsync()).ReturnsAsync(seedData);
            var controller = new RatingController(ratingService.Object);

            // Act
            var actionResult = await controller.Home();
            var okResult = actionResult as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.NotNull(okResult.Value);
            Assert.Equal(seedData.Count, ((List<Rating>)okResult.Value).Count);
            Assert.Equal(200, okResult.StatusCode);
        }
        /// <summary>
        /// Test API call of type GET on endpoint /Rating/{id}. It should return the requested Rating.
        /// </summary>
        [Fact]
        public async void Get_GetById_PassInt_ShouldReturnSingleRating()
        {
            // Arrange
            int idToReturn = 1;
            Rating seedData = new Rating() { OrderNumber = 5 };
            seedData.SetId(idToReturn);

            var ratingService = new Mock<IRatingService>();
            ratingService.Setup(x => x.GetByIdAsync(idToReturn)).ReturnsAsync(seedData);
            var controller = new RatingController(ratingService.Object);

            // Act
            var actionResult = await controller.GetById(idToReturn);
            var okResult = actionResult as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.NotNull(okResult.Value);
            Assert.Equal(seedData.OrderNumber, ((Rating)okResult.Value).OrderNumber);
            Assert.Equal(200, okResult.StatusCode);
        }
        /// <summary>
        /// Test API call of type GET on endpoint /Rating/{id}. It should return NotFound.
        /// </summary>
        [Fact]
        public async void Get_GetById_PassInt_ShouldReturnNotFound()
        {
            // Arrange
            var ratingService = new Mock<IRatingService>();
            var controller = new RatingController(ratingService.Object);

            // Act
            var actionResult = await controller.GetById(1);
            var notFoundResult = actionResult as NotFoundResult;

            // Assert
            Assert.NotNull(notFoundResult);
            Assert.Equal(404, notFoundResult.StatusCode);
        }
        /// <summary>
        /// Test API call of type POST on endpoint /Rating/Add. It should return the created Rating.
        /// </summary>
        [Fact]
        public async void Post_Add_PassRating_ShouldReturnSameRating()
        {
            // Arrange
            int idToCreate = 1;
            Rating seedData = new Rating() { OrderNumber = 5 };
            seedData.SetId(idToCreate);

            var ratingService = new Mock<IRatingService>();
            ratingService.Setup(x => x.AddAsync(seedData)).ReturnsAsync(seedData);
            var controller = new RatingController(ratingService.Object);

            // Act
            var actionResult = await controller.Add(seedData);
            var okResult = actionResult as CreatedAtActionResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.NotNull(okResult.Value);
            Assert.Equal(seedData.OrderNumber, ((Rating)okResult.Value).OrderNumber);
            Assert.Equal(201, okResult.StatusCode);
        }
        /// <summary>
        /// Test API call of type POST on endpoint /Rating/Update. It should return the updated Rating.
        /// </summary>
        [Fact]
        public async void Post_Update_PassRating_ShouldReturnSameRating()
        {
            // Arrange
            int idToUpdate = 1;
            Rating seedData = new Rating() { OrderNumber = 5 };
            seedData.SetId(idToUpdate);

            var ratingService = new Mock<IRatingService>();
            ratingService.Setup(x => x.UpdateAsync(seedData)).ReturnsAsync(seedData);
            ratingService.Setup(x => x.ExistAsync(idToUpdate)).ReturnsAsync(true);
            var controller = new RatingController(ratingService.Object);

            // Act
            var actionResult = await controller.Update(idToUpdate, seedData);
            var okResult = actionResult as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.NotNull(okResult.Value);
            Assert.Equal(seedData.OrderNumber, ((Rating)okResult.Value).OrderNumber);
            Assert.Equal(200, okResult.StatusCode);
        }
        /// <summary>
        /// Test API call of type DELETE on endpoint /Rating/Delete. It should return the deleted Rating.
        /// </summary>
        [Fact]
        public async void Delete_Delete_PassRating_ShouldReturnSameRating()
        {
            // Arrange
            int idToDelete = 1;
            Rating seedData = new Rating() { OrderNumber = 5 };
            seedData.SetId(idToDelete);

            var ratingService = new Mock<IRatingService>();
            ratingService.Setup(x => x.DeleteAsync(idToDelete)).ReturnsAsync(seedData);
            var controller = new RatingController(ratingService.Object);

            // Act
            var actionResult = await controller.Delete(idToDelete);
            var okResult = actionResult as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.NotNull(okResult.Value);
            Assert.Equal(seedData.OrderNumber, ((Rating)okResult.Value).OrderNumber);
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}
