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
        private readonly UserManager<AppUser> _userManager;
        public CurveController(UserManager<AppUser> userManager, ICurveService curveService)
            : base(userManager)
        {
            _userManager = userManager;
            _curveService = curveService;
        }
        [Authorize]
        [HttpGet("/curvePoint/list")]
        public async Task<IActionResult> Home()
        {
            await AddLogInformation("List of Curves retrieved successfully.");
            return Ok(await _curveService.GetAllAsync());
        }
        [Authorize]
        [HttpGet("/curvePoint/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            CurvePoint? curvePoint = await _curveService.GetByIdAsync(id);
            if (curvePoint == null)
            {
                await AddLogError($"Curve {id} not found.");
                return NotFound();
            }

            await AddLogInformation($"Curve {id} returned successfully.");
            return Ok(curvePoint);
        }
        [Authorize]
        [HttpPost("/curvePoint/add")]
        public async Task<IActionResult> Add([FromBody] CurvePoint curvePoint)
        {
            if (!ModelState.IsValid)
            {
                await AddLogError($"Bad request: {ModelState}");
                return BadRequest(ModelState);
            }
            curvePoint.Id = null;
            CurvePoint? createdCurvePoint = await _curveService.AddAsync(curvePoint);

            if (createdCurvePoint == null)
            {
                await AddLogError($"Error while creating Curve.");
                return StatusCode(500);
            }

            await AddLogInformation($"Curve {createdCurvePoint.Id} updated successfully.");
            return CreatedAtAction(nameof(GetById), new { id = createdCurvePoint.Id }, createdCurvePoint);
        }
        [Authorize]
        [HttpPut("/curvepoint/update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CurvePoint curvePoint)
        {
            if (!ModelState.IsValid)
            {
                await AddLogError($"Bad request: {ModelState}");
                return BadRequest(ModelState);
            }

            curvePoint.Id = id;
            CurvePoint? updatedCurvePoint = await _curveService.UpdateAsync(curvePoint);
            
            if (updatedCurvePoint == null)
            {
                await AddLogError($"Curve {id} not found.");
                return NotFound();
            }

            await AddLogInformation($"Curve {id} updated successfully.");
            return Ok(updatedCurvePoint);
        }
        [Authorize]
        [HttpDelete("/curvepoint/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                await AddLogError($"Bad request: {ModelState}");
                return BadRequest(ModelState);
            }
            CurvePoint? deletedCurvePoint = await _curveService.DeleteAsync(id);
            
            if (deletedCurvePoint == null)
            {
                await AddLogError($"Curve {id} not found.");
                return NotFound();
            }

            await AddLogInformation($"Curve {id} deleted successfully.");
            return Ok(deletedCurvePoint);
        }
    }
}