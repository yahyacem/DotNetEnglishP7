using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dot.Net.WebApi.Domain;
using DotNetEnglishP7.Controllers;
using DotNetEnglishP7.Identity;
using DotNetEnglishP7.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
 
namespace Dot.Net.WebApi.Controllers
{
    [Route("[controller]")]
    public class RatingController : BaseController
    {
        IRatingService _ratingService;
        private readonly UserManager<AppUser> _userManager;
        public RatingController(UserManager<AppUser> userManager, IRatingService ratingService)
            : base(userManager)
        {
            _userManager = userManager;
            _ratingService = ratingService;
        }
        [Authorize]
        [HttpGet("/rating/list")]
        public async Task<IActionResult> Home()
        {
            await AddLogInformation("List of Ratings retrieved successfully.");
            return Ok(await _ratingService.GetAllAsync());
        }
        [Authorize]
        [HttpGet("/rating/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Rating? rating = await _ratingService.GetByIdAsync(id);
            if (rating == null)
            {
                await AddLogError($"Rating {id} not found.");
                return NotFound();
            }

            await AddLogInformation($"Rating {id} returned successfully.");
            return Ok(rating);
        }
        [Authorize]
        [HttpPost("/rating/add")]
        public async Task<IActionResult> Add([FromBody] Rating rating)
        {
            if (!ModelState.IsValid)
            {
                await AddLogError($"Bad request: {ModelState}");
                return BadRequest(ModelState);
            }
            rating.Id = null;
            Rating? createdRating = await _ratingService.AddAsync(rating);
            
            if (createdRating == null)
            {
                await AddLogError($"Error while creating Rating.");
                return StatusCode(500);
            }

            await AddLogInformation($"Rating {createdRating.Id} created successfully.");
            return CreatedAtAction(nameof(GetById), new { id = createdRating.Id }, createdRating);
        }
        [Authorize]
        [HttpPut("/rating/update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Rating rating)
        {
            if (!ModelState.IsValid)
            {
                await AddLogError($"Bad request: {ModelState}");
                return BadRequest(ModelState);
            }
            rating.Id = id;
            Rating? updatedRating = await _ratingService.UpdateAsync(rating);
            
            if (updatedRating == null)
            {
                await AddLogError($"Rating {id} not found.");
                return NotFound();
            }

            await AddLogInformation($"Rating {id} updated successfully.");
            return Ok(updatedRating);
        }
        [Authorize]
        [HttpDelete("/rating/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                await AddLogError($"Bad request: {ModelState}");
                return BadRequest(ModelState);
            }
            Rating? deletedRating = await _ratingService.DeleteAsync(id);
            
            if (deletedRating == null)
            {
                await AddLogError($"Rating {id} not found.");
                return NotFound();
            }

            await AddLogInformation($"Rating {id} deleted successfully.");
            return Ok(deletedRating);
        }
    }
}