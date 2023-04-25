using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.Repositories;
using DotNetEnglishP7.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
 
namespace Dot.Net.WebApi.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
        private IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("/user/list")]
        public async Task<User[]?> Home()
        {
            return await _userRepository.FindAllAsync();
        }

        [HttpPost("/user/add")]
        public async Task<ActionResult<User?>> AddUser([FromBody]User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newUser = await _userRepository.AddAsync(user);

            return newUser;
        }

        [HttpPost("/user/update/{id}")]
        public async Task<ActionResult<User?>> updateUser(int id, [FromBody] User user)
        {
            if (!await _userRepository.ExistAsync(id))
                ModelState.AddModelError("Id", "Invalid user Id: " + id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            user.Id = id;
            return await _userRepository.UpdateAsync(user);
        }

        [HttpDelete("/user/{id}")]
        public async Task<ActionResult<User[]?>> DeleteUser(int id)
        {
            if (!await _userRepository.ExistAsync(id))
                ModelState.AddModelError("Id", "Invalid user Id: " + id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _userRepository.DeleteAsync(id);
            return await _userRepository.FindAllAsync();
        }
    }
}