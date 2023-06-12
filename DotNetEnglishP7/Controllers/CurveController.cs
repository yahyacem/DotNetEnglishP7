using System;
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
    public class CurveController : BaseController
    {
        private ICurveService _curveService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger _logger;
        public CurveController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, ILogger<CurveController> logger, ICurveService curveService)
            : base(signInManager, userManager, logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _curveService = curveService;
        }
        [Authorize]
        [HttpGet("/curvePoint/list")]
        public async Task<IActionResult> Home()
        {
            AddLog("List of Curves retrieved successfully.");
            return Ok(await _curveService.GetAllAsync());
        }
        [Authorize]
        [HttpGet("/curvePoint/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            CurvePoint? curvePoint = await _curveService.GetByIdAsync(id);
            if (curvePoint == null)
            {
                return NotFound();
            }

            AddLog($"BidList {id} returned successfully.");
            return Ok(curvePoint);
        }
        [Authorize]
        [HttpPost("/curvePoint/add")]
        public async Task<IActionResult> Add([FromBody] CurvePoint curvePoint)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            curvePoint.Id = null;
            CurvePoint? createdCurvePoint = await _curveService.AddAsync(curvePoint);

            if (createdCurvePoint == null)
            {
                return StatusCode(500);
            }

            AddLog($"Curve {createdCurvePoint.Id} updated successfully.");
            return CreatedAtAction(nameof(GetById), new { id = createdCurvePoint.Id }, createdCurvePoint);
        }
        [Authorize]
        [HttpPut("/curvepoint/update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CurvePoint curvePoint)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            curvePoint.Id = id;
            CurvePoint? updatedCurvePoint = await _curveService.UpdateAsync(curvePoint);
            
            if (updatedCurvePoint == null)
            {
                return NotFound();
            }

            AddLog($"Curve {id} updated successfully.");
            return Ok(updatedCurvePoint);
        }
        [Authorize]
        [HttpDelete("/curvepoint/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            CurvePoint? deletedCurvePoint = await _curveService.DeleteAsync(id);
            
            if (deletedCurvePoint == null)
            { 
                return NotFound();
            }

            AddLog($"Curve {id} deleted successfully.");
            return Ok(deletedCurvePoint);
        }
    }
}