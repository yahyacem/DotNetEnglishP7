using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dot.Net.WebApi.Domain;
using DotNetEnglishP7.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
 
namespace Dot.Net.WebApi.Controllers
{
    [Route("[controller]")]
    public class BidListController : Controller
    {
        private IBidListRepository _bidListRepository;
        public BidListController(IBidListRepository bidListRepository)
        {
            _bidListRepository = bidListRepository;
        }
        [HttpGet("/bidList/list")]
        public async Task<BidList[]?> Home()
        {
            return await _bidListRepository.GetAllAsync();
        }
        [HttpGet("/bidList/{id}")]
        public async Task<BidList?> GetById(int id)
        {
            return await _bidListRepository.GetByIdAsync(id);
        }

        [HttpPost("/bidList/add")]
        public async Task<ActionResult<BidList?>> Add([FromBody]BidList bidList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return await _bidListRepository.AddAsync(bidList);
        }

        [HttpPost("/bidList/update/{id}")]
        public async Task<ActionResult<BidList?>> UpdateBid(int id, [FromBody] BidList bidList)
        {
            if (!await _bidListRepository.ExistAsync(id))
            {
                ModelState.AddModelError("Id", "No Bid List found with this id: " + id);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            bidList.BidListId = id;
            return await _bidListRepository.UpdateAsync(bidList);
        }

        [HttpDelete("/bidList/{id}")]
        public async Task<IActionResult> DeleteBid(int id)
        {
            if (!await _bidListRepository.ExistAsync(id))
            {
                ModelState.AddModelError("Id", "No Bid List found with this id: " + id);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _bidListRepository.DeleteAsync(id);
            return Ok();
        }
    }
}