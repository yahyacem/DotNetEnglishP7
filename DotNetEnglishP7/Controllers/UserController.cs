using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.Repositories;
using DotNetEnglishP7.Controllers;
using DotNetEnglishP7.Domain;
using DotNetEnglishP7.Identity;
using DotNetEnglishP7.Mappers;
using DotNetEnglishP7.Repositories;
using DotNetEnglishP7.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
 
namespace Dot.Net.WebApi.Controllers
{
    [Route("[controller]")]
    public class UserController : BaseController
    {
        private static IMapper _mapper = MappingProfile.InitializeAutoMapper().CreateMapper();
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        public UserController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager,RoleManager<AppRole> roleManager) : base(userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [AllowAnonymous]
        [HttpPost("/user/login")]
        public async Task<IActionResult> Login([FromBody] LoginUser user)
        {
            if (!ModelState.IsValid)
            {
                await AddLogError($"Bad request: {ModelState}");
                return BadRequest(ModelState);
            }
            var result = await _signInManager.PasswordSignInAsync(user.UserName,
                           user.Password, false, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                await AddLogInformation("User logged successfully.");
                return Ok();    
            }

            if (result.IsLockedOut)
            {
                ModelState.AddModelError("Locked", "Account is currently locked.");
            }

            await AddLogError($"Username or password incorrect.");
            return Unauthorized("Username or password incorrect.");
        }
        [AllowAnonymous]
        [HttpPost("/user/register")]
        public async Task<IActionResult> Register([FromBody] RegisterUser user) 
        {
            if (!ModelState.IsValid)
            {
                await AddLogError($"Bad request: {ModelState}");
                return BadRequest(ModelState);
            }

            user.Id = null;
            AppUser userToCreate = _mapper.Map<AppUser>(user);
            var result = await _userManager.CreateAsync(userToCreate, user.Password);

            if (result == null)
            {
                await AddLogError($"Error when trying to register new user.");
                return StatusCode(500);
            }

            if (!result.Succeeded)
            {
                await AddLogError($"Bad request: {ModelState}");
                return BadRequest(result.Errors);
            }

            var roleToAdd = _roleManager.FindByNameAsync(user.Role).Result;

            if (roleToAdd == null)
            {
                roleToAdd = _roleManager.FindByNameAsync("User").Result;
            }

            await _userManager.AddToRoleAsync(userToCreate, roleToAdd.Name);

            await AddLogInformation("User created successfully.");
            return Ok();
        }
        [Authorize]
        [HttpGet("/user/list")]
        public async Task<IActionResult> Home()
        {
            var listUsers = new List<User>();
            foreach(var user in await _userManager.Users.ToListAsync())
            {
                var roles = await _userManager.GetRolesAsync(user);
                string role = roles.Count > 0 ? roles[0] : "";
                listUsers.Add(_mapper.Map<User>(user, opt => 
                opt.AfterMap((src, dest) => dest.Role = role)));
            }
            await AddLogInformation("List of users retrieved successfully.");
            return Ok(listUsers);
        }
        [Authorize]
        [HttpGet("/user/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                await AddLogError($"User {id} not found.");
                return NotFound();
            }

            var roles = await _userManager.GetRolesAsync(user);
            string role = roles.Count > 0 ? roles[0] : "";

            await AddLogInformation($"User {id} returned successfully.");
            return Ok(_mapper.Map<User>(user, opt =>
                opt.AfterMap((src, dest) => dest.Role = role)));
        }
        [Authorize]
        [HttpPut("/user/update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RegisterUser user)
        {
            if (!ModelState.IsValid)
            {
                await AddLogError($"Bad request: {ModelState}");
                return BadRequest(ModelState);
            }

            AppUser? userToUpdate = await _userManager.FindByIdAsync(id.ToString());

            if (userToUpdate == null)
            {
                await AddLogError($"User {id} not found.");
                return NotFound();
            }

            if (userToUpdate.Id.ToString() != _userManager.GetUserId(User) && !User.IsInRole("SuperAdmin"))
            {
                await AddLogError($"User unauthorized.");
                return Unauthorized();
            }


            user.Id = id;
            _mapper.Map(user, userToUpdate);
            var result = await _userManager.UpdateAsync(userToUpdate);

            if (!result.Succeeded)
            {
                await AddLogError($"Error when trying to update user {id}.");
                return StatusCode(500, result.Errors);
            }

            var currentRoles = await _userManager.GetRolesAsync(userToUpdate);
            var newRole = await _roleManager.FindByNameAsync(user.Role);
            if (newRole == null)
            {
                await AddLogError($"Error when trying to update user {id}.");
                return BadRequest($"Role {user.Role} doesn't exist.");
            }

            await _userManager.RemoveFromRolesAsync(userToUpdate, currentRoles);
            await _userManager.AddToRoleAsync(userToUpdate, user.Role);

            await AddLogInformation($"User {id} updated successfully.");
            return Ok();
        }
        [Authorize]
        [HttpDelete("/user/delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                await AddLogError($"Bad request: {ModelState}");
                return BadRequest(ModelState);
            }

            AppUser? userToDelete = await _userManager.FindByIdAsync(id.ToString());

            if (userToDelete == null)
            {
                await AddLogError($"User {id} not found.");
                return NotFound();
            }

            if (userToDelete.Id.ToString() != _userManager.GetUserId(User) && !User.IsInRole("SuperAdmin"))
            {
                await AddLogError($"User unauthorized.");
                return Unauthorized();
            }

            var result = await _userManager.DeleteAsync(userToDelete);

            if (!result.Succeeded)
            {
                await AddLogError($"Error when trying to delete user {id}.");
                return StatusCode(500, result.Errors);
            }

            await AddLogInformation($"User {id} deleted successfully.");
            return Ok();
        }
    }
}