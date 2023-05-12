using System;
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
    public class CurveController : Controller
    {
        private ICurveService _curveService;
        public CurveController(ICurveService curveService)
        {
            _curveService = curveService;
        }
        [HttpGet("/curvePoint/list")]
        public async Task<IActionResult> Home()
        {
            return Ok(await _curveService.GetAllAsync());
        }
        [HttpGet("/curvePoint/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            CurvePoint? curvePoint = await _curveService.GetByIdAsync(id);
            return curvePoint == null ? NotFound() : Ok(curvePoint);
        }
        [HttpPost("/curvePoint/add")]
        public async Task<IActionResult> Add([FromBody]CurvePoint curvePoint)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            CurvePoint? createdCurvePoint = await _curveService.AddAsync(curvePoint);
            return createdCurvePoint != null ? CreatedAtAction(nameof(GetById), new { id = curvePoint.Id }, createdCurvePoint) : StatusCode(500);
        }
        [HttpPost("/curvepoint/update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CurvePoint curvePoint)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            curvePoint.SetId(id);
            CurvePoint? updatedCurvePoint = await _curveService.UpdateAsync(curvePoint);
            return updatedCurvePoint == null ? NotFound() : Ok(updatedCurvePoint);
        }
        [HttpDelete("/curvepoint/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            CurvePoint? deletedCurvePoint = await _curveService.DeleteAsync(id);
            return deletedCurvePoint == null ? NotFound() : Ok(deletedCurvePoint);
        }
    }
}