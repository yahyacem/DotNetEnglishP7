using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dot.Net.WebApi.Domain;
using DotNetEnglishP7.Repositories;
using DotNetEnglishP7.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
 
namespace Dot.Net.WebApi.Controllers
{
    [Route("[controller]")]
    public class BidListController : Controller
    {
        private IBidListService _bidListService;
        public BidListController(IBidListService bidListService)
        {
            _bidListService = bidListService;
        }
        [HttpGet("/bidList/list")]
        public async Task<IActionResult> Home()
        {
            return Ok(await _bidListService.GetAllAsync());
        }
        [HttpGet("/bidList/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            BidList? bidList = await _bidListService.GetByIdAsync(id);
            return bidList == null ? NotFound() : Ok(bidList);
        }
        [HttpPost("/bidList/add")]
        public async Task<IActionResult> Add([FromBody] BidList bidList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            BidList? createdBidList = await _bidListService.AddAsync(bidList);
            return createdBidList != null ? CreatedAtAction(nameof(GetById), new { id = bidList.BidListId }, createdBidList) : StatusCode(500);
        }
        [HttpPost("/bidList/update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BidList bidList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            bidList.SetBidListId(id);
            BidList? updatedBidList = await _bidListService.UpdateAsync(bidList);
            return updatedBidList == null ? NotFound() : Ok(updatedBidList);
        }
        [HttpDelete("/bidList/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            BidList? deletedBidList = await _bidListService.DeleteAsync(id);
            return deletedBidList == null ? NotFound() : Ok(deletedBidList);
        }
    }
}