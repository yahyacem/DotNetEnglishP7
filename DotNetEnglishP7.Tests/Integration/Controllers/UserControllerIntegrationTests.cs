using Dot.Net.WebApi.Controllers;
using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.Repositories;
using DotNetEnglishP7.Domain;
using DotNetEnglishP7.Identity;
using DotNetEnglishP7.Repositories;
using DotNetEnglishP7.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
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
        ///// <summary>
        ///// Test API call of type GET on endpoint /User. It should return the list of all User.
        ///// </summary>
        //[Fact]
        //public async void GET_Home_ShouldReturnListUser()
        //{
        //    using (var context = new LocalDbContext(GetOptions()))
        //    {
        //        // Arrange
        //        List<AppUser> seedData = new List<AppUser>()
        //        {
        //            new AppUser() { Id = 1, FullName = "FullName Test 1", UserName = "UserName Test 1", PasswordHash = "Password Test 1" },
        //            new AppUser() { Id = 2, FullName = "FullName Test 2", UserName = "UserName Test 2", PasswordHash = "Password Test 2" },
        //            new AppUser() { Id = 3, FullName = "FullName Test 3", UserName = "UserName Test 3", PasswordHash = "Password Test 3" }
        //        };
                
        //        context.Users.AddRange(seedData);
        //        context.SaveChanges();

        //        // Instantiate UserManager, UserSignIn and controller
        //        var store = new Mock<IUserStore<AppUser>>();
        //        var userManager = new UserManager<AppUser>(store.Object, null, null, null, null, null, null, null, null);
        //        var userSignIn = new SignInManager<AppUser>(userManager, new Mock<IHttpContextAccessor>().Object, 
        //            new Mock<IUserClaimsPrincipalFactory<AppUser>>().Object,
        //            new Mock<IOptions<IdentityOptions>>().Object, null,
        //            new Mock<IAuthenticationSchemeProvider>().Object, null);
        //        var controller = new UserController(userSignIn, userManager);

        //        // Act
        //        // Make the call and capture result
        //        var actionResult = await controller.Home();
        //        var okResult = actionResult as OkObjectResult;

        //        // Assert
        //        // Check response code
        //        Assert.NotNull(okResult);
        //        Assert.NotNull(okResult.Value);
        //        Assert.Equal(200, okResult.StatusCode);
        //        // Check response data
        //        Assert.Equal(seedData.Count, ((List<User>)okResult.Value).Count);
        //        Assert.Equal(seedData[0].FullName, ((List<User>)okResult.Value).First(x => x.Id == 1).FullName);
        //        Assert.Equal(seedData[0].UserName, ((List<User>)okResult.Value).First(x => x.Id == 1).UserName);
        //    }
        //}
        ///// <summary>
        ///// Test API call of type GET on endpoint /User/{id}. It should return the requested User.
        ///// </summary>
        //[Fact]
        //public async void GET_GetById_PassInt_ShouldReturnSameUser()
        //{
        //    using (var context = new LocalDbContext(GetOptions()))
        //    {
        //        // Arrange
        //        int idToReturn = 1;
        //        AppUser seedData = new AppUser() { Id = idToReturn, FullName = "FullName Test 1", UserName = "UserName Test 1", PasswordHash = "Password Test 1" };
        //        context.Users.Add(seedData);
        //        context.SaveChanges();

        //        // Instantiate UserManager, UserSignIn and controller
        //        var store = new Mock<IUserStore<AppUser>>();
        //        var userManager = new UserManager<AppUser>(store.Object, null, null, null, null, null, null, null, null);
        //        var userSignIn = new SignInManager<AppUser>(userManager, null, null, null, null, null, null);
        //        var controller = new UserController(userSignIn, userManager);

        //        // Act
        //        // Make the call and capture result
        //        var actionResult = await controller.GetById(idToReturn);
        //        var okResult = actionResult as OkObjectResult;

        //        // Assert
        //        // Check response code
        //        Assert.NotNull(okResult);
        //        Assert.NotNull(okResult.Value);
        //        Assert.Equal(200, okResult.StatusCode);
        //        // Check response data
        //        Assert.Equal(seedData.Id, ((User)okResult.Value).Id);
        //        Assert.Equal(seedData.FullName, ((User)okResult.Value).FullName);
        //        Assert.Equal(seedData.UserName, ((User)okResult.Value).UserName);
        //    }
        //}
        ///// <summary>
        ///// Test API call of type GET on endpoint /User/{id}. It should return NotFound.
        ///// </summary>
        //[Fact]
        //public async void GET_GetById_PassInt_ShouldReturnNull()
        //{
        //    using (var context = new LocalDbContext(GetOptions()))
        //    {
        //        // Arrange
        //        int idToReturn = 1;

        //        // Instantiate UserManager, UserSignIn and controller
        //        var store = new Mock<IUserStore<AppUser>>();
        //        var userManager = new UserManager<AppUser>(store.Object, null, null, null, null, null, null, null, null);
        //        var userSignIn = new SignInManager<AppUser>(userManager, null, null, null, null, null, null);
        //        var controller = new UserController(userSignIn, userManager);

        //        // Act
        //        // Make the call and capture result
        //        var actionResult = await controller.GetById(idToReturn);
        //        var notFoundResult = actionResult as NotFoundResult;

        //        // Assert
        //        // Check response code
        //        Assert.NotNull(notFoundResult);
        //        Assert.Equal(404, notFoundResult.StatusCode);
        //        // Check data in database
        //        Assert.Empty(context.Users);
        //    }
        //}
        ///// <summary>
        ///// Test API call of type POST on endpoint /User/Add. It should return the created User.
        ///// </summary>
        //[Fact]
        //public async void POST_Add_PassUser_ShouldReturnSameUser()
        //{
        //    using (var context = new LocalDbContext(GetOptions()))
        //    {
        //        // Arrange
        //        int idToCreate = 1;
        //        RegisterUser seedData = new RegisterUser() { FullName = "FullName Test 1", UserName = "UserName Test 1", Password = "Password Test 1", Role = "Role Test 1" };

        //        // Instantiate UserManager, UserSignIn and controller
        //        var store = new Mock<IUserStore<AppUser>>();
        //        var userManager = new UserManager<AppUser>(store.Object, null, null, null, null, null, null, null, null);
        //        var userSignIn = new SignInManager<AppUser>(userManager, null, null, null, null, null, null);
        //        var controller = new UserController(userSignIn, userManager);

        //        // Act
        //        // Make the call and capture result
        //        var actionResult = await controller.Register(seedData);
        //        var okResult = actionResult as CreatedAtActionResult;

        //        // Check response code
        //        Assert.NotNull(okResult);
        //        Assert.NotNull(okResult.Value);
        //        Assert.Equal(201, okResult.StatusCode);
        //        // Check data in database
        //        Assert.Single(context.Users);
        //        Assert.NotNull(await context.Users.FirstOrDefaultAsync(x => x.Id == idToCreate));
        //    }
        //}
        ///// <summary>
        ///// Test API call of type POST on endpoint /User/Update. It should return the updated User.
        ///// </summary>
        //[Fact]
        //public async void POST_Update_PassUser_ShouldReturnSameUser()
        //{
        //    using (var context = new LocalDbContext(GetOptions()))
        //    {
        //        // Arrange
        //        int idToUpdate = 1;
        //        // Insert seed data
        //        AppUser seedData = new AppUser() { Id = idToUpdate, FullName = "FullName Test 1", UserName = "UserName Test 1", PasswordHash = "Password Test 1" };
        //        context.Users.Add(seedData);
        //        context.SaveChanges();
        //        // Prepare new data to update
        //        RegisterUser updatedData = new RegisterUser() { FullName = "FullName Test 2", UserName = "UserName Test 2", Password = "Password Test 2", Role = "Role Test 2" };

        //        // Instantiate UserManager, UserSignIn and controller
        //        var store = new Mock<IUserStore<AppUser>>();
        //        var userManager = new UserManager<AppUser>(store.Object, null, null, null, null, null, null, null, null);
        //        var userSignIn = new SignInManager<AppUser>(userManager, null, null, null, null, null, null);
        //        var controller = new UserController(userSignIn, userManager);

        //        // Act
        //        // Make the call and capture result
        //        var actionResult = await controller.Update(idToUpdate, updatedData);
        //        var okResult = actionResult as OkObjectResult;

        //        // Check response code
        //        Assert.NotNull(okResult);
        //        Assert.NotNull(okResult.Value);
        //        Assert.Equal(200, okResult.StatusCode);
        //        // Check data in database
        //        Assert.NotNull(await context.Users.FirstOrDefaultAsync(x => x.Id == idToUpdate));
        //        Assert.Equal(updatedData.Id, (await context.Users.FirstAsync(x => x.Id == idToUpdate)).Id);
        //        Assert.Equal(updatedData.FullName, (await context.Users.FirstAsync(x => x.Id == idToUpdate)).FullName);
        //        Assert.Equal(updatedData.UserName, (await context.Users.FirstAsync(x => x.Id == idToUpdate)).UserName);
        //    }
        //}
        ///// <summary>
        ///// Test API call of type DELETE on endpoint /User/Delete. It should return the deleted User.
        ///// </summary>
        //[Fact]
        //public async void DELETE_Delete_PassUser_ShouldReturnSameUser()
        //{
        //    using (var context = new LocalDbContext(GetOptions()))
        //    {
        //        // Arrange
        //        int idToDelete = 1;
        //        // Insert seed data
        //        List<AppUser> seedData = new List<AppUser>()
        //        {
        //            new AppUser() { Id = 1, FullName = "FullName Test 1", UserName = "UserName Test 1", PasswordHash = "Password Test 1" },
        //            new AppUser() { Id = 2, FullName = "FullName Test 2", UserName = "UserName Test 2", PasswordHash = "Password Test 2" },
        //            new AppUser() { Id = 3, FullName = "FullName Test 3", UserName = "UserName Test 3", PasswordHash = "Password Test 3" }
        //        };
        //        context.Users.AddRange(seedData);
        //        context.SaveChanges();

        //        // Instantiate UserManager, UserSignIn and controller
        //        var store = new Mock<IUserStore<AppUser>>();
        //        var userManager = new UserManager<AppUser>(store.Object, null, null, null, null, null, null, null, null);
        //        var userSignIn = new SignInManager<AppUser>(userManager, null, null, null, null, null, null);
        //        var controller = new UserController(userSignIn, userManager);

        //        // Act
        //        // Make the call and capture result
        //        var actionResult = await controller.Delete(idToDelete);
        //        var okResult = actionResult as OkObjectResult;

        //        // Assert
        //        // Check response code
        //        Assert.NotNull(okResult);
        //        Assert.NotNull(okResult.Value);
        //        Assert.Equal(200, okResult.StatusCode);
        //        // Check data in database
        //        Assert.Null(await context.Users.FirstOrDefaultAsync(x => x.Id == idToDelete));
        //        Assert.Equal(2, context.Users.Count());
        //    }
        //}
    }
}
