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
    public class RatingServiceUnitTests : UnitTests
    {
        [Fact]
        public async void GetAllAsync_ShouldReturnList()
        {
            // Arrange
            List<Rating> seedData = new List<Rating>() 
            { 
                new Rating() { Id = 1, OrderNumber = 5 },
                new Rating() { Id = 2, OrderNumber = 10 },
                new Rating() { Id = 3, OrderNumber = 7 }
            };

            var ratingRepository = new Mock<IRatingRepository>();
            ratingRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(seedData);

            // Act
            IRatingService ratingService = new RatingService(ratingRepository.Object);
            var ratingResult = await ratingService.GetAllAsync();

            // Assert
            Assert.NotNull(ratingResult);
            Assert.Equal(3, ratingResult.Count);
            Assert.Equal(10, ratingResult[1].OrderNumber);
        }
        [Fact]
        public async void GetByIdAsync_PassInt_ShouldReturnSingleRating()
        {
            // Arrange
            int idToGet = 1;
            Rating seedData = new Rating() { Id = idToGet, OrderNumber = 5 };

            var ratingRepository = new Mock<IRatingRepository>();
            ratingRepository.Setup(x => x.GetByIdAsync(idToGet)).ReturnsAsync(seedData);

            // Act
            IRatingService ratingService = new RatingService(ratingRepository.Object);
            var ratingResult = await ratingService.GetByIdAsync(idToGet);

            // Assert
            Assert.NotNull(ratingResult);
            Assert.Equal(5, ratingResult.OrderNumber);
        }
        [Fact]
        public async void GetByIdAsync_PassInt_ShouldReturnNull()
        {
            // Arrange
            var ratingRepository = new Mock<IRatingRepository>();

            // Act
            IRatingService ratingService = new RatingService(ratingRepository.Object);
            var ratingResult = await ratingService.GetByIdAsync(1);

            // Assert
            Assert.Null(ratingResult);
        }
        [Fact]
        public async void AddAsync_PassRating_ShouldReturnSameRating()
        {
            // Arrange
            Rating seedData = new Rating() { Id = 1, OrderNumber = 5 };

            var ratingRepository = new Mock<IRatingRepository>();
            ratingRepository.Setup(x => x.AddAsync(seedData)).ReturnsAsync(seedData);

            // Act
            IRatingService ratingService = new RatingService(ratingRepository.Object);
            var ratingResult = await ratingService.AddAsync(seedData);

            // Assert
            Assert.NotNull(ratingResult);
            Assert.Equal(seedData.Id, ratingResult.Id);
            Assert.Equal(seedData.OrderNumber, ratingResult.OrderNumber);
        }
        [Fact]
        public async void UpdateAsync_PassRating_ShouldReturnSameRating()
        {
            // Arrange
            int idToUpdate = 1;
            Rating seedData = new Rating() { Id = idToUpdate, OrderNumber = 5 };

            var ratingRepository = new Mock<IRatingRepository>();
            ratingRepository.Setup(x => x.GetByIdAsync(idToUpdate)).ReturnsAsync(seedData);
            ratingRepository.Setup(x => x.UpdateAsync(seedData)).ReturnsAsync(seedData);

            // Act
            IRatingService ratingService = new RatingService(ratingRepository.Object);
            var ratingResult = await ratingService.UpdateAsync(seedData);

            // Assert
            Assert.NotNull(ratingResult);
            Assert.Equal(seedData.Id, ratingResult.Id);
            Assert.Equal(seedData.OrderNumber, ratingResult.OrderNumber);
        }
        [Fact]
        public async void DeleteAsync_PassRating_ShouldReturnSameRating()
        {
            // Arrange
            int idToGet = 1;
            Rating seedData = new Rating() { Id = idToGet, OrderNumber = 5 };

            var ratingRepository = new Mock<IRatingRepository>();
            ratingRepository.Setup(x => x.GetByIdAsync(idToGet)).ReturnsAsync(seedData);
            ratingRepository.Setup(x => x.DeleteAsync(seedData)).ReturnsAsync(seedData);

            // Act
            IRatingService ratingService = new RatingService(ratingRepository.Object);
            var ratingResult = await ratingService.DeleteAsync(idToGet);

            // Assert
            Assert.NotNull(ratingResult);
            Assert.Equal(seedData.Id, ratingResult.Id);
            Assert.Equal(seedData.OrderNumber, ratingResult.OrderNumber);
        }
    }
}