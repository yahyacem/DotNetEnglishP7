using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dot.Net.WebApi.Domain;
using DotNetEnglishP7.Controllers;
using DotNetEnglishP7.Identity;
using DotNetEnglishP7.Repositories;
using DotNetEnglishP7.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.Extensions.Logging;
 
namespace Dot.Net.WebApi.Controllers
{
    [Route("[controller]")]
    public class BidListController : BaseController
    {
        private IBidListService _bidListService;
        private readonly UserManager<AppUser> _userManager;
        public BidListController(UserManager<AppUser> userManager, IBidListService bidListService)
            : base(userManager)
        {
            _userManager = userManager;
            _bidListService = bidListService;
        }
        [Authorize]
        [HttpGet("/bidList/list")]
        public async Task<IActionResult> Home()
        {
            await AddLogInformation("List of BidList retrieved successfully.");
            return Ok(await _bidListService.GetAllAsync());
        }
        [Authorize]
        [HttpGet("/bidList/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            BidList? bidList = await _bidListService.GetByIdAsync(id);
            if (bidList == null)
            {
                await AddLogError($"BidList {id} not found.");
                return NotFound();
            }

            await AddLogInformation($"BidList {id} returned successfully.");
            return Ok(bidList);
        }
        [Authorize]
        [HttpPost("/bidList/add")]
        public async Task<IActionResult> Add([FromBody] BidList bidList)
        {
            if (!ModelState.IsValid)
            {
                await AddLogError($"Bad request: {ModelState}");
                return BadRequest(ModelState);
            }
            bidList.Id = null;
            BidList? createdBidList = await _bidListService.AddAsync(bidList);

            if (createdBidList == null)
            {
                await AddLogError($"Error while creating BidList.");
                return StatusCode(500);
            }

            await AddLogInformation($"BidList {createdBidList.Id} created successfully.");
            return CreatedAtAction(nameof(GetById), new { id = createdBidList.Id }, createdBidList);
        }
        [Authorize]
        [HttpPut("/bidList/update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BidList bidList)
        {
            if (!ModelState.IsValid)
            {
                await AddLogError($"Bad request: {ModelState}");
                return BadRequest(ModelState);
            }
            bidList.Id = id;
            BidList? updatedBidList = await _bidListService.UpdateAsync(bidList);

            if (updatedBidList == null)
            {
                await AddLogError($"BidList {id} not found.");
                return NotFound();
            }

            await AddLogInformation($"BidList {id} updated successfully.");
            return Ok(updatedBidList);
        }
        [Authorize]
        [HttpDelete("/bidList/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                await AddLogError($"Bad request: {ModelState}");
                return BadRequest(ModelState);
            }
            BidList? deletedBidList = await _bidListService.DeleteAsync(id);

            if (deletedBidList == null)
            {
                await AddLogError($"BidList {id} not found.");
                return NotFound();
            }

            await AddLogInformation($"BidList {id} deleted successfully.");
            return Ok(deletedBidList);
        }
    }
}