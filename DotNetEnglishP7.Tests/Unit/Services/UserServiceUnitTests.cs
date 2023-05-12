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
    public class UserServiceUnitTests : UnitTests
    {
        [Fact]
        public async void GetAllAsync_ShouldReturnList()
        {
            // Arrange
            List<User> seedData = new List<User>()
            {
                new User() { FullName = "Test FullName 1", UserName = "Test UserName 1", Password = "Test Password 1", Role = "admin" },
                new User() { FullName = "Test FullName 2", UserName = "Test UserName 2", Password = "Test Password 2", Role = "superadmin" },
                new User() { FullName = "Test FullName 3", UserName = "Test UserName 3", Password = "Test Password 3", Role = "admin" }
            };
            for (int i = 1; i <= seedData.Count; i++)
            {
                seedData[i - 1].SetId(i);
            }

            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(seedData);

            // Act
            IUserService userService = new UserService(userRepository.Object);
            var userResult = await userService.GetAllAsync();

            // Assert
            Assert.NotNull(userResult);
            Assert.Equal(3, userResult.Count);
            Assert.Equal("Test UserName 2", userResult[1].UserName);
        }
        [Fact]
        public async void GetByIdAsync_PassInt_ShouldReturnSingleUser()
        {
            // Arrange
            User seedData = new User() { FullName = "Test FullName 2", UserName = "Test UserName 2", Password = "Test Password 2", Role = "superadmin" };
            seedData.SetId(1);

            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(seedData);

            // Act
            IUserService userService = new UserService(userRepository.Object);
            var userResult = await userService.GetByIdAsync(1);

            // Assert
            Assert.NotNull(userResult);
            Assert.Equal("Test UserName 2", userResult.UserName);
            Assert.Equal("superadmin", userResult.Role);
        }
        [Fact]
        public async void GetByIdAsync_PassInt_ShouldReturnNull()
        {
            // Arrange
            var userRepository = new Mock<IUserRepository>();

            // Act
            IUserService userService = new UserService(userRepository.Object);
            var userResult = await userService.GetByIdAsync(1);

            // Assert
            Assert.Null(userResult);
        }
        [Fact]
        public async void AddAsync_PassUser_ShouldReturnSameUser()
        {
            // Arrange
            User seedData = new User() { FullName = "Test FullName 2", UserName = "Test UserName 2", Password = "Test Password 2", Role = "superadmin" };
            seedData.SetId(1);

            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(x => x.AddAsync(seedData)).ReturnsAsync(seedData);

            // Act
            IUserService userService = new UserService(userRepository.Object);
            var userResult = await userService.AddAsync(seedData);

            // Assert
            Assert.NotNull(userResult);
            Assert.Equal("Test UserName 2", userResult.UserName);
            Assert.Equal("superadmin", userResult.Role);
        }
        [Fact]
        public async void UpdateAsync_PassUser_ShouldReturnSameUser()
        {
            // Arrange
            int idToUpdate = 1;
            User seedData = new User() { FullName = "Test FullName 2", UserName = "Test UserName 2", Password = "Test Password 2", Role = "superadmin" };
            seedData.SetId(idToUpdate);

            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(x => x.GetByIdAsync(idToUpdate)).ReturnsAsync(seedData);
            userRepository.Setup(x => x.UpdateAsync(seedData)).ReturnsAsync(seedData);

            // Act
            IUserService userService = new UserService(userRepository.Object);
            var userResult = await userService.UpdateAsync(seedData);

            // Assert
            Assert.NotNull(userResult);
            Assert.Equal("Test UserName 2", userResult.UserName);
            Assert.Equal("superadmin", userResult.Role);
        }
        [Fact]
        public async void DeleteAsync_PassUser_ShouldReturnSameUser()
        {
            // Arrange
            User seedData = new User() { FullName = "Test FullName 2", UserName = "Test UserName 2", Password = "Test Password 2", Role = "superadmin" };
            seedData.SetId(1);

            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(seedData);
            userRepository.Setup(x => x.DeleteAsync(seedData)).ReturnsAsync(seedData);

            // Act
            IUserService userService = new UserService(userRepository.Object);
            var userResult = await userService.DeleteAsync(1);

            // Assert
            Assert.NotNull(userResult);
            Assert.Equal("Test UserName 2", userResult.UserName);
            Assert.Equal("superadmin", userResult.Role);
        }
    }
}
