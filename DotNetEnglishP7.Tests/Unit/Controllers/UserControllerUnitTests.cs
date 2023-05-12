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

namespace DotNetEnglishP7.Tests.Unit.Controllers
{
    public class UserControllerUnitTests : UnitTests
    {
        /// <summary>
        /// Test API call of type GET on endpoint /User. It should return the list of all User.
        /// </summary>
        [Fact]
        public async void Get_Home_ShouldReturnListTrade()
        {
            // Arrange
            List<User> seedData = new List<User>()
            {
                new User() { FullName = "FullName 1" , UserName = "UserName 1", Password = "Password 1", Role = "admin"},
                new User() { FullName = "FullName 2" , UserName = "UserName 2", Password = "Password 2", Role = "superadmin"},
                new User() { FullName = "FullName 3" , UserName = "UserName 3", Password = "Password 3", Role = "admin"}
            };
            for (int i = 1; i <= seedData.Count; i++)
            {
                seedData[i - 1].SetId(i);
            }

            var userService = new Mock<IUserService>();
            userService.Setup(x => x.GetAllAsync()).ReturnsAsync(seedData);
            var controller = new UserController(userService.Object);

            // Act
            var actionResult = await controller.Home();
            var okResult = actionResult as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.NotNull(okResult.Value);
            Assert.Equal(seedData.Count, ((List<User>)okResult.Value).Count);
            Assert.Equal(200, okResult.StatusCode);
        }
        /// <summary>
        /// Test API call of type GET on endpoint /User/{id}. It should return the requested User.
        /// </summary>
        [Fact]
        public async void Get_GetById_PassInt_ShouldReturnSingleUser()
        {
            // Arrange
            int idToReturn = 1;
            User seedData = new User() { FullName = "FullName 1", UserName = "UserName 1", Password = "Password 1", Role = "admin" };
            seedData.SetId(idToReturn);

            var userService = new Mock<IUserService>();
            userService.Setup(x => x.GetByIdAsync(idToReturn)).ReturnsAsync(seedData);
            var controller = new UserController(userService.Object);

            // Act
            var actionResult = await controller.GetById(idToReturn);
            var okResult = actionResult as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.NotNull(okResult.Value);
            Assert.Equal(seedData.UserName, ((User)okResult.Value).UserName);
            Assert.Equal(200, okResult.StatusCode);
        }
        /// <summary>
        /// Test API call of type GET on endpoint /User/{id}. It should return NotFound.
        /// </summary>
        [Fact]
        public async void Get_GetById_PassInt_ShouldReturnNotFound()
        {
            // Arrange
            var userService = new Mock<IUserService>();
            var controller = new UserController(userService.Object);

            // Act
            var actionResult = await controller.GetById(1);
            var notFoundResult = actionResult as NotFoundResult;

            // Assert
            Assert.NotNull(notFoundResult);
            Assert.Equal(404, notFoundResult.StatusCode);
        }
        /// <summary>
        /// Test API call of type POST on endpoint /User/Add. It should return the created User.
        /// </summary>
        [Fact]
        public async void Post_Add_PassUser_ShouldReturnSameUser()
        {
            // Arrange
            int idToCreate = 1;
            User seedData = new User() { FullName = "FullName 1", UserName = "UserName 1", Password = "Password 1", Role = "admin" };
            seedData.SetId(idToCreate);

            var userService = new Mock<IUserService>();
            userService.Setup(x => x.AddAsync(seedData)).ReturnsAsync(seedData);
            var controller = new UserController(userService.Object);

            // Act
            var actionResult = await controller.Add(seedData);
            var okResult = actionResult as CreatedAtActionResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.NotNull(okResult.Value);
            Assert.Equal(seedData.UserName, ((User)okResult.Value).UserName);
            Assert.Equal(201, okResult.StatusCode);
        }
        /// <summary>
        /// Test API call of type POST on endpoint /User/Update. It should return the updated User.
        /// </summary>
        [Fact]
        public async void Post_Update_PassUser_ShouldReturnSameUser()
        {
            // Arrange
            int idToUpdate = 1;
            User seedData = new User() { FullName = "FullName 1", UserName = "UserName 1", Password = "Password 1", Role = "admin" };
            seedData.SetId(idToUpdate);

            var userService = new Mock<IUserService>();
            userService.Setup(x => x.UpdateAsync(seedData)).ReturnsAsync(seedData);
            userService.Setup(x => x.ExistAsync(idToUpdate)).ReturnsAsync(true);
            var controller = new UserController(userService.Object);

            // Act
            var actionResult = await controller.Update(idToUpdate, seedData);
            var okResult = actionResult as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.NotNull(okResult.Value);
            Assert.Equal(seedData.UserName, ((User)okResult.Value).UserName);
            Assert.Equal(200, okResult.StatusCode);
        }
        /// <summary>
        /// Test API call of type DELETE on endpoint /User/Delete. It should return the deleted User.
        /// </summary>
        [Fact]
        public async void Delete_Delete_PassUser_ShouldReturnSameUser()
        {
            // Arrange
            int idToDelete = 1;
            User seedData = new User() { FullName = "FullName 1", UserName = "UserName 1", Password = "Password 1", Role = "admin" };
            seedData.SetId(idToDelete);

            var userService = new Mock<IUserService>();
            userService.Setup(x => x.DeleteAsync(idToDelete)).ReturnsAsync(seedData);
            var controller = new UserController(userService.Object);

            // Act
            var actionResult = await controller.Delete(idToDelete);
            var okResult = actionResult as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.NotNull(okResult.Value);
            Assert.Equal(seedData.UserName, ((User)okResult.Value).UserName);
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}
