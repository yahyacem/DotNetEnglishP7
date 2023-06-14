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
    public class RuleController : BaseController
    {
        IRuleService _ruleService;
        private readonly UserManager<AppUser> _userManager;
        public RuleController(UserManager<AppUser> userManager, IRuleService ruleService)
            : base(userManager)
        {
            _userManager = userManager;
            _ruleService = ruleService;
        }
        [Authorize]
        [HttpGet("/ruleName/list")]
        public async Task<IActionResult> Home()
        {
            await AddLogInformation("List of Rules retrieved successfully.");
            return Ok(await _ruleService.GetAllAsync());
        }
        [Authorize]
        [HttpGet("/ruleName/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Rule? rule = await _ruleService.GetByIdAsync(id);
            if (rule == null)
            {
                await AddLogError($"Rule {id} not found.");
                return NotFound();
            }

            await AddLogInformation($"Rule {id} returned successfully.");
            return Ok(rule);
        }
        [Authorize]
        [HttpPost("/ruleName/add")]
        public async Task<IActionResult> Add([FromBody] Rule rule)
        {
            if (!ModelState.IsValid)
            {
                await AddLogError($"Bad request: {ModelState}");
                return BadRequest(ModelState);
            }
            rule.Id = null;
            Rule? createdRule = await _ruleService.AddAsync(rule);
            
            if (createdRule == null)
            {
                await AddLogError($"Rule while creating Rating.");
                return StatusCode(500);
            }

            await AddLogInformation($"Rule {createdRule.Id} created successfully.");
            return CreatedAtAction(nameof(GetById), new { id = createdRule.Id }, createdRule);
        }
        [Authorize]
        [HttpPut("/ruleName/update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Rule rule)
        {
            if (!ModelState.IsValid)
            {
                await AddLogError($"Bad request: {ModelState}");
                return BadRequest(ModelState);
            }
            rule.Id = id;
            Rule? updatedRule = await _ruleService.UpdateAsync(rule);

            if (updatedRule == null)
            {
                await AddLogError($"Rule {id} not found.");
                return NotFound();
            }

            await AddLogInformation($"Rule {id} updated successfully.");
            return Ok(updatedRule);
        }
        [Authorize]
        [HttpDelete("/ruleName/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                await AddLogError($"Bad request: {ModelState}");
                return BadRequest(ModelState);
            }
            Rule? deletedRule = await _ruleService.DeleteAsync(id);

            if (deletedRule == null)
            {
                await AddLogError($"Rule {id} not found.");
                return NotFound();
            }

            await AddLogInformation($"Rule {id} deleted successfully.");
            return Ok(deletedRule);
        }
    }
}