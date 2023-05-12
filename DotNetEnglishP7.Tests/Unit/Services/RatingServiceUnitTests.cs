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
                new Rating()
                {
                    OrderNumber = 5
                },
                new Rating()
                {
                    OrderNumber = 10
                },
                new Rating()
                {
                    OrderNumber = 7
                }
            };
            for (int i = 1; i <= seedData.Count; i++)
            {
                seedData[i - 1].SetId(i);
            }

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
            Rating seedData = new Rating()
            {
                OrderNumber = 5
            };
            seedData.SetId(1);

            var ratingRepository = new Mock<IRatingRepository>();
            ratingRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(seedData);

            // Act
            IRatingService ratingService = new RatingService(ratingRepository.Object);
            var ratingResult = await ratingService.GetByIdAsync(1);

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
            Rating seedData = new Rating()
            {
                OrderNumber = 5
            };
            seedData.SetId(1);

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
            Rating seedData = new Rating() { OrderNumber = 5 };
            seedData.SetId(idToUpdate);

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
            Rating seedData = new Rating()
            {
                OrderNumber = 5
            };
            seedData.SetId(1);

            var ratingRepository = new Mock<IRatingRepository>();
            ratingRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(seedData);
            ratingRepository.Setup(x => x.DeleteAsync(seedData)).ReturnsAsync(seedData);

            // Act
            IRatingService ratingService = new RatingService(ratingRepository.Object);
            var ratingResult = await ratingService.DeleteAsync(1);

            // Assert
            Assert.NotNull(ratingResult);
            Assert.Equal(seedData.Id, ratingResult.Id);
            Assert.Equal(seedData.OrderNumber, ratingResult.OrderNumber);
        }
    }
}