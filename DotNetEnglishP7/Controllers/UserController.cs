using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.Repositories;
using DotNetEnglishP7.Repositories;
using DotNetEnglishP7.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
 
namespace Dot.Net.WebApi.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("/user/list")]
        public async Task<IActionResult> Home()
        {
            return Ok(await _userService.GetAllAsync());
        }
        [HttpGet("/trade/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            User? user = await _userService.GetByIdAsync(id);
            return user == null ? NotFound() : Ok(user);
        }
        [HttpPost("/user/add")]
        public async Task<IActionResult> Add([FromBody]User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            User? createdUser = await _userService.AddAsync(user);
            return createdUser != null ? CreatedAtAction(nameof(GetById), new { id = user.Id }, createdUser) : StatusCode(500);
        }
        [HttpPost("/user/update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            user.SetId(id);
            User? updatedUser = await _userService.UpdateAsync(user);
            return updatedUser == null ? NotFound() : Ok(updatedUser);
        }

        [HttpDelete("/user/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            User? deletedUser = await _userService.DeleteAsync(id);
            return deletedUser == null ? NotFound() : Ok(deletedUser);
        }
    }
}