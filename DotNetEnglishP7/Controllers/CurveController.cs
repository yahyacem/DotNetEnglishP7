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
        public async Task<ActionResult<List<CurvePoint>>> Home()
        {
            return Ok(await _curveService.GetAllAsync());
        }
        [HttpGet("/curvePoint/{id}")]
        public async Task<ActionResult<CurvePoint>> GetById(int id)
        {
            CurvePoint? curvePoint = await _curveService.GetByIdAsync(id);
            return curvePoint == null ? NotFound() : Ok(curvePoint);
        }
        [HttpPost("/curvePoint/add")]
        public async Task<ActionResult<CurvePoint>> AddCurvePoint([FromBody]CurvePoint curvePoint)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CurvePoint createdCurvePoint = await _curveService.AddAsync(curvePoint);
            return CreatedAtAction(nameof(GetById), new { id = curvePoint.Id }, createdCurvePoint);
        }
        [HttpPost("/curvepoint/update/{id}")]
        public async Task<ActionResult<CurvePoint>> UpdateCurvePoint(int id, [FromBody] CurvePoint curvePoint)
        {
            if (!await _curveService.ExistAsync(id))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            curvePoint.SetId(id);
            CurvePoint? curvePointUpdated = await _curveService.UpdateAsync(curvePoint);
            return curvePointUpdated == null ? NotFound() : Ok(curvePointUpdated);
        }
        [HttpDelete("/curvepoint/{id}")]
        public async Task<ActionResult> DeleteBid(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CurvePoint? curvePointDeleted = await _curveService.DeleteAsync(id);
            return curvePointDeleted == null ? NotFound() : Ok(curvePointDeleted);
        }
    }
}