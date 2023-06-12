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
        private readonly ILogger _logger;
        private readonly RoleManager<AppRole> _roleManager;
        public UserController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, ILogger<UserController> logger, RoleManager<AppRole> roleManager) : base(signInManager, userManager, logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _roleManager = roleManager;
        }
        [AllowAnonymous]
        [HttpPost("/user/login")]
        public async Task<IActionResult> Login([FromBody] LoginUser user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _signInManager.PasswordSignInAsync(user.UserName,
                           user.Password, false, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                AddLog("User logged successfully.");
                return Ok();    
            }

            if (result.IsLockedOut)
            {
                ModelState.AddModelError("Locked", "Account is currently locked.");
            }

            return Unauthorized("Username or password incorrect.");
        }
        [AllowAnonymous]
        [HttpPost("/user/register")]
        public async Task<IActionResult> Register([FromBody] RegisterUser user) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            user.Id = null;
            AppUser userToCreate = _mapper.Map<AppUser>(user);
            var result = await _userManager.CreateAsync(userToCreate, user.Password);

            if (result == null)
            {
                AddLog($"Error when trying to register new user.");
                return StatusCode(500);
            }

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            var roleToAdd = _roleManager.FindByNameAsync(user.Role).Result;

            if (roleToAdd == null)
            {
                roleToAdd = _roleManager.FindByNameAsync("Default").Result;
            }

            await _userManager.AddToRoleAsync(userToCreate, roleToAdd.Name);

            AddLog("User created successfully.");
            return Ok();
        }
        [Authorize]
        [HttpGet("/user/list")]
        public async Task<IActionResult> Home()
        {
            var listUsers = new List<User>();
            foreach(var user in await _userManager.Users.ToListAsync())
            {
                listUsers.Add(_mapper.Map<User>(user));
            }
            AddLog("List of users retrieved successfully.");
            return Ok(listUsers);
        }
        [Authorize]
        [HttpGet("/user/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }

            AddLog($"User {id} returned successfully.");
            return Ok(_mapper.Map<User>(user));
        }
        [Authorize]
        [HttpPut("/user/update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RegisterUser user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AppUser? userToUpdate = await _userManager.FindByIdAsync(id.ToString());

            if (userToUpdate == null)
            {
                return NotFound();
            }

            if (userToUpdate.Id.ToString() != _userManager.GetUserId(User) && !User.IsInRole("admin"))
            {
                return Unauthorized();
            }

            user.Id = id;
            _mapper.Map(user, userToUpdate);
            var result = await _userManager.UpdateAsync(userToUpdate);

            if (!result.Succeeded)
            {
                AddLog($"Error when trying to update user {id}.");
                return StatusCode(500, result.Errors);
            }

            AddLog($"User {id} updated successfully.");
            return Ok();
        }
        [Authorize]
        [HttpDelete("/user/delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AppUser? userToDelete = await _userManager.FindByIdAsync(id.ToString());

            if (userToDelete == null)
            {
                return NotFound();
            }

            if (userToDelete.Id.ToString() != _userManager.GetUserId(User) && !User.IsInRole("admin"))
            {
                return Unauthorized();
            }

            var result = await _userManager.DeleteAsync(userToDelete);

            if (!result.Succeeded)
            {
                AddLog($"Error when trying to delete user {id}.");
                return StatusCode(500, result.Errors);
            }

            AddLog($"User {id} deleted successfully.");
            return Ok();
        }
    }
}