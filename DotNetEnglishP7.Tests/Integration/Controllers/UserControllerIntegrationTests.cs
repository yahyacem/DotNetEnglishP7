using Dot.Net.WebApi.Controllers;
using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.Repositories;
using DotNetEnglishP7.Repositories;
using DotNetEnglishP7.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetEnglishP7.Tests.Integration.Controllers
{
    [Collection("Sequential")]
    public class UserControllerIntegrationTests : IntegrationTests
    {
        /// <summary>
        /// Test API call of type GET on endpoint /User. It should return the list of all User.
        /// </summary>
        [Fact]
        public async void GET_Home_ShouldReturnListUser()
        {
            using (var context = new LocalDbContext(GetOptions()))
            {
                // Arrange
                List<User> seedData = new List<User>()
                {
                    new User() { FullName = "FullName Test 1", UserName = "UserName Test 1", Password = "Password Test 1", Role = "Role Test 1" },
                    new User() { FullName = "FullName Test 2", UserName = "UserName Test 2", Password = "Password Test 2", Role = "Role Test 2" },
                    new User() { FullName = "FullName Test 3", UserName = "UserName Test 3", Password = "Password Test 3", Role = "Role Test 3" }
                };
                for (int i = 1; i <= seedData.Count; i++)
                {
                    seedData[i - 1].SetId(i);
                }
                context.Users.AddRange(seedData);
                context.SaveChanges();

                // Instantiate repository, service and controller
                var userRepositoy = new UserRepository(context);
                IUserService userService = new UserService(userRepositoy);
                var controller = new UserController(userService);

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
                Assert.Equal(seedData.Count, ((List<User>)okResult.Value).Count);
                Assert.Equal(seedData[0].FullName, ((List<User>)okResult.Value).First(x => x.Id == 1).FullName);
                Assert.Equal(seedData[0].UserName, ((List<User>)okResult.Value).First(x => x.Id == 1).UserName);
            }
        }
        /// <summary>
        /// Test API call of type GET on endpoint /User/{id}. It should return the requested User.
        /// </summary>
        [Fact]
        public async void GET_GetById_PassInt_ShouldReturnSameUser()
        {
            using (var context = new LocalDbContext(GetOptions()))
            {
                // Arrange
                int idToReturn = 1;
                User seedData = new User() { FullName = "FullName Test 1", UserName = "UserName Test 1", Password = "Password Test 1", Role = "Role Test 1" };
                context.Users.Add(seedData);
                context.SaveChanges();

                // Instantiate repository, service and controller
                var userRepositoy = new UserRepository(context);
                IUserService userService = new UserService(userRepositoy);
                var controller = new UserController(userService);

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
                Assert.Equal(seedData.Id, ((User)okResult.Value).Id);
                Assert.Equal(seedData.FullName, ((User)okResult.Value).FullName);
                Assert.Equal(seedData.UserName, ((User)okResult.Value).UserName);
            }
        }
        /// <summary>
        /// Test API call of type GET on endpoint /User/{id}. It should return NotFound.
        /// </summary>
        [Fact]
        public async void GET_GetById_PassInt_ShouldReturnNull()
        {
            using (var context = new LocalDbContext(GetOptions()))
            {
                // Arrange
                int idToReturn = 1;

                // Instantiate repository, service and controller
                var userRepositoy = new UserRepository(context);
                IUserService userService = new UserService(userRepositoy);
                var controller = new UserController(userService);

                // Act
                // Make the call and capture result
                var actionResult = await controller.GetById(idToReturn);
                var notFoundResult = actionResult as NotFoundResult;

                // Assert
                // Check response code
                Assert.NotNull(notFoundResult);
                Assert.Equal(404, notFoundResult.StatusCode);
                // Check data in database
                Assert.Empty(await userService.GetAllAsync());
                Assert.Null(await userService.GetByIdAsync(idToReturn));
            }
        }
        /// <summary>
        /// Test API call of type POST on endpoint /User/Add. It should return the created User.
        /// </summary>
        [Fact]
        public async void POST_Add_PassUser_ShouldReturnSameUser()
        {
            using (var context = new LocalDbContext(GetOptions()))
            {
                // Arrange
                int idToCreate = 1;
                User seedData = new User() { FullName = "FullName Test 1", UserName = "UserName Test 1", Password = "Password Test 1", Role = "Role Test 1" };

                // Instantiate repository, service and controller
                var userRepositoy = new UserRepository(context);
                IUserService userService = new UserService(userRepositoy);
                var controller = new UserController(userService);

                // Act
                // Make the call and capture result
                var actionResult = await controller.Add(seedData);
                var okResult = actionResult as CreatedAtActionResult;

                // Check response code
                Assert.NotNull(okResult);
                Assert.NotNull(okResult.Value);
                Assert.Equal(201, okResult.StatusCode);
                // Check response data
                Assert.Equal(seedData.Id, ((User)okResult.Value).Id);
                Assert.Equal(seedData.FullName, ((User)okResult.Value).FullName);
                Assert.Equal(seedData.UserName, ((User)okResult.Value).UserName);
                // Check data in database
                Assert.Single(await userService.GetAllAsync());
                Assert.NotNull(await userService.GetByIdAsync(idToCreate));
            }
        }
        /// <summary>
        /// Test API call of type POST on endpoint /User/Update. It should return the updated User.
        /// </summary>
        [Fact]
        public async void POST_Update_PassUser_ShouldReturnSameUser()
        {
            using (var context = new LocalDbContext(GetOptions()))
            {
                // Arrange
                int idToUpdate = 1;
                // Insert seed data
                User seedData = new User() { FullName = "FullName Test 1", UserName = "UserName Test 1", Password = "Password Test 1", Role = "Role Test 1" };
                context.Users.Add(seedData);
                context.SaveChanges();
                // Prepare new data to update
                User updatedData = new User() { FullName = "FullName Test 2", UserName = "UserName Test 2", Password = "Password Test 2", Role = "Role Test 2" };

                // Instantiate repository, service and controller
                var userRepositoy = new UserRepository(context);
                IUserService userService = new UserService(userRepositoy);
                var controller = new UserController(userService);

                // Act
                // Make the call and capture result
                var actionResult = await controller.Update(idToUpdate, updatedData);
                var okResult = actionResult as OkObjectResult;

                // Check response code
                Assert.NotNull(okResult);
                Assert.NotNull(okResult.Value);
                Assert.Equal(200, okResult.StatusCode);
                // Check response data
                Assert.Equal(updatedData.Id, ((User)okResult.Value).Id);
                Assert.Equal(updatedData.FullName, ((User)okResult.Value).FullName);
                Assert.Equal(updatedData.UserName, ((User)okResult.Value).UserName);
                // Check data in database
                Assert.NotNull(await userService.GetByIdAsync(idToUpdate));
            }
        }
        /// <summary>
        /// Test API call of type DELETE on endpoint /User/Delete. It should return the deleted User.
        /// </summary>
        [Fact]
        public async void DELETE_Delete_PassUser_ShouldReturnSameUser()
        {
            using (var context = new LocalDbContext(GetOptions()))
            {
                // Arrange
                int idToDelete = 1;
                // Insert seed data
                List<User> seedData = new List<User>()
                {
                    new User() { FullName = "FullName Test 1", UserName = "UserName Test 1", Password = "Password Test 1", Role = "Role Test 1" },
                    new User() { FullName = "FullName Test 2", UserName = "UserName Test 2", Password = "Password Test 2", Role = "Role Test 2" },
                    new User() { FullName = "FullName Test 3", UserName = "UserName Test 3", Password = "Password Test 3", Role = "Role Test 3" }
                };
                for (int i = 1; i <= seedData.Count; i++)
                {
                    seedData[i - 1].SetId(i);
                }
                context.Users.AddRange(seedData);
                context.SaveChanges();

                // Instantiate repository, service and controller
                var userRepositoy = new UserRepository(context);
                IUserService userService = new UserService(userRepositoy);
                var controller = new UserController(userService);

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
                Assert.Equal(seedData.First(x => x.Id == idToDelete).Id, ((User)okResult.Value).Id);
                Assert.Equal(seedData.First(x => x.Id == idToDelete).FullName, ((User)okResult.Value).FullName);
                Assert.Equal(seedData.First(x => x.Id == idToDelete).UserName, ((User)okResult.Value).UserName);
                // Check data in database
                Assert.Null(await userService.GetByIdAsync(idToDelete));
                Assert.Equal(2, (await userService.GetAllAsync()).Count);
            }
        }
    }
}
