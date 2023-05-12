using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dot.Net.WebApi.Domain;
using DotNetEnglishP7.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
 
namespace Dot.Net.WebApi.Controllers
{
    [Route("[controller]")]
    public class TradeController : Controller
    {
        ITradeService _tradeService;
        public TradeController(ITradeService tradeService)
        {
            _tradeService = tradeService;
        }

        [HttpGet("/trade/list")]
        public async Task<IActionResult> Home()
        {
            return Ok(await _tradeService.GetAllAsync());
        }
        [HttpGet("/trade/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Trade? trade = await _tradeService.GetByIdAsync(id);
            return trade == null ? NotFound() : Ok(trade);
        }
        [HttpPost("/trade/add")]
        public async Task<IActionResult> Add([FromBody]Trade trade)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Trade? createdTrade = await _tradeService.AddAsync(trade);
            return createdTrade != null ? CreatedAtAction(nameof(GetById), new { id = trade.TradeId }, createdTrade) : StatusCode(500);
        }
        [HttpPost("/trade/update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Trade trade)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            trade.SetTradeId(id);
            Trade? updatedTrade = await _tradeService.UpdateAsync(trade);
            return updatedTrade == null ? NotFound() : Ok(updatedTrade);
        }
        [HttpDelete("/trade/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Trade? deletedTrade = await _tradeService.DeleteAsync(id);
            return deletedTrade == null ? NotFound() : Ok(deletedTrade);
        }
    }
}