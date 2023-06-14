using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
    public class TradeController : BaseController
    {
        ITradeService _tradeService;
        private readonly UserManager<AppUser> _userManager;
        public TradeController(UserManager<AppUser> userManager, ITradeService tradeService) 
            : base(userManager) 
        { 
            _userManager = userManager; 
            _tradeService = tradeService; 
        }
        [Authorize]
        [HttpGet("/trade/list")]
        public async Task<IActionResult> Home()
        {
            await AddLogInformation("List of Trades retrieved successfully.");
            return Ok(await _tradeService.GetAllAsync());
        }
        [Authorize]
        [HttpGet("/trade/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Trade? trade = await _tradeService.GetByIdAsync(id);
            if (trade == null)
            {
                await AddLogError($"Trade {id} not found.");
                return NotFound();
            }

            await AddLogInformation($"Trade {id} returned successfully.");
            return Ok(trade);
        }
        [Authorize]
        [HttpPost("/trade/add")]
        public async Task<IActionResult> Add([FromBody] Trade trade)
        {
            if (!ModelState.IsValid)
            {
                await AddLogError($"Bad request: {ModelState}");
                return BadRequest(ModelState);
            }
            trade.Id = null;
            Trade? createdTrade = await _tradeService.AddAsync(trade);
            
            if (createdTrade == null)
            {
                await AddLogError($"Trade while creating Rating.");
                return StatusCode(500);
            }

            await AddLogInformation($"Trade {createdTrade.Id} returned successfully.");
            return CreatedAtAction(nameof(GetById), new { id = createdTrade.Id }, createdTrade);
        }
        [Authorize]
        [HttpPut("/trade/update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Trade trade)
        {
            if (!ModelState.IsValid)
            {
                await AddLogError($"Bad request: {ModelState}");
                return BadRequest(ModelState);
            }
            trade.Id = id;
            Trade? updatedTrade = await _tradeService.UpdateAsync(trade);

            if (updatedTrade == null)
            {
                await AddLogError($"Trade {id} not found.");
                return NotFound();
            }

            await AddLogInformation($"Trade {id} updated successfully.");
            return Ok(updatedTrade);
        }
        [Authorize]
        [HttpDelete("/trade/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                await AddLogError($"Bad request: {ModelState}");
                return BadRequest(ModelState);
            }
            Trade? deletedTrade = await _tradeService.DeleteAsync(id);
            
            if (deletedTrade == null)
            {
                await AddLogError($"Trade {id} not found.");
                return NotFound();
            }
            
            await AddLogInformation($"Trade {id} deleted successfully.");
            return Ok(deletedTrade);
        }
    }
}