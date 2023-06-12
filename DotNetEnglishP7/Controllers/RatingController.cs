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
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger _logger;
        public RatingController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, ILogger<RatingController> logger, IRatingService ratingService)
            : base(signInManager, userManager, logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _ratingService = ratingService;
        }
        [Authorize]
        [HttpGet("/rating/list")]
        public async Task<IActionResult> Home()
        {
            AddLog("List of Ratings retrieved successfully.");
            return Ok(await _ratingService.GetAllAsync());
        }
        [Authorize]
        [HttpGet("/rating/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Rating? rating = await _ratingService.GetByIdAsync(id);
            if (rating == null)
            {
                return NotFound();
            }

            AddLog($"Curve {id} returned successfully.");
            return Ok(rating);
        }
        [Authorize]
        [HttpPost("/rating/add")]
        public async Task<IActionResult> Add([FromBody] Rating rating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            rating.Id = null;
            Rating? createdRating = await _ratingService.AddAsync(rating);
            
            if (createdRating == null)
            {
                return StatusCode(500);
            }

            AddLog($"Rating {createdRating.Id} created successfully.");
            return CreatedAtAction(nameof(GetById), new { id = createdRating.Id }, createdRating);
        }
        [Authorize]
        [HttpPut("/rating/update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Rating rating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            rating.Id = id;
            Rating? updatedRating = await _ratingService.UpdateAsync(rating);
            
            if (updatedRating == null)
            {
                return NotFound();
            }

            AddLog($"Rating {id} updated successfully.");
            return Ok(updatedRating);
        }
        [Authorize]
        [HttpDelete("/rating/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Rating? deletedRating = await _ratingService.DeleteAsync(id);
            
            if (deletedRating == null)
            {
                return NotFound();
            }

            AddLog($"Rating {id} deleted successfully.");
            return Ok(deletedRating);
        }
    }
}