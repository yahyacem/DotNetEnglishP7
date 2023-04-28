using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dot.Net.WebApi.Domain;
using DotNetEnglishP7.Models;
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
        public async Task<List<CurvePointViewModel>> Home()
        {
            return await _curveService.GetAllAsync();
        }

        [HttpPost("/curvePoint/add")]
        public async Task<ActionResult<CurvePointViewModel?>> AddCurvePoint([FromBody]CurvePointViewModel curvePoint)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return await _curveService.AddAsync(curvePoint);
        }

        [HttpGet("/curvePoint/validate")]
        public IActionResult Validate([FromBody]CurvePoint curvePoint)
        {
            // TODO: check data valid and save to db, after saving return bid list
            return View("curvePoint/add"    );
        }

        [HttpGet("/curvePoint/update/{id}")]
        public IActionResult ShowUpdateForm(int id)
        {
            // TODO: get CurvePoint by Id and to model then show to the form
            return View("curvepoint/update");
        }

        [HttpPost("/curvepoint/update/{id}")]
        public IActionResult UpdateCurvePoint(int id, [FromBody] CurvePoint curvePoint)
        {
            // TODO: check required fields, if valid call service to update Curve and return Curve list
            return Redirect("/curvepoint/list");
        }

        [HttpDelete("/curvepoint/{id}")]
        public IActionResult DeleteBid(int id)
        {
            // TODO: Find Curve by Id and delete the Curve, return to Curve list

            return Redirect("/curvePoint/list");
        }
    }
}