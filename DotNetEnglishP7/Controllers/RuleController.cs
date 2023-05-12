using System;
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
    public class RuleController : Controller
    {
        IRuleService _ruleService;
        public RuleController(IRuleService ruleService)
        {
            _ruleService = ruleService;
        }
        [HttpGet("/ruleName/list")]
        public async Task<IActionResult> Home()
        {
            return Ok(await _ruleService.GetAllAsync());
        }
        [HttpGet("/ruleName/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Rule? rule = await _ruleService.GetByIdAsync(id);
            return rule == null ? NotFound() : Ok(rule);
        }
        [HttpPost("/ruleName/add")]
        public async Task<IActionResult> Add([FromBody]Rule rule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Rule? createdRule = await _ruleService.AddAsync(rule);
            return createdRule != null ? CreatedAtAction(nameof(GetById), new { id = rule.Id }, createdRule) : StatusCode(500);
        }
        [HttpPost("/ruleName/update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Rule rule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            rule.SetId(id);
            Rule? updatedRule = await _ruleService.UpdateAsync(rule);
            return updatedRule == null ? NotFound() : Ok(updatedRule);
        }
        [HttpDelete("/ruleName/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Rule? deletedRule = await _ruleService.DeleteAsync(id);
            return deletedRule == null ? NotFound() : Ok(deletedRule);
        }
    }
}