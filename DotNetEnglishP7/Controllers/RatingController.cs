using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dot.Net.WebApi.Domain;
using DotNetEnglishP7.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
 
namespace Dot.Net.WebApi.Controllers
{
    [Route("[controller]")]
    public class RatingController : Controller
    {
        IRatingService _ratingService;
        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpGet("/rating/list")]
        public async Task<IActionResult> Home()
        {
            return Ok(await _ratingService.GetAllAsync());
        }
        [HttpGet("/rating/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Rating? rating = await _ratingService.GetByIdAsync(id);
            if (rating == null)
            {
                return NotFound();
            }
            return Ok(rating);
        }
        [HttpPost("/rating/add")]
        public async Task<IActionResult> Add([FromBody]Rating rating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Rating? createdRating = await _ratingService.AddAsync(rating);
            return createdRating != null ? CreatedAtAction(nameof(GetById), new { id = rating.Id }, createdRating) : StatusCode(500);
        }
        [HttpPost("/rating/update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Rating rating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            rating.SetId(id);
            Rating? updatedRating = await _ratingService.UpdateAsync(rating);
            return updatedRating == null ? NotFound() : Ok(updatedRating);
        }

        [HttpDelete("/rating/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Rating? deletedRating = await _ratingService.DeleteAsync(id);
            return deletedRating == null ? NotFound() : Ok(deletedRating);
        }
    }
}