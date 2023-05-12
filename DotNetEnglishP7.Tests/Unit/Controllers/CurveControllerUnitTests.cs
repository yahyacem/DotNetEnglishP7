using Dot.Net.WebApi.Controllers;
using Dot.Net.WebApi.Domain;
using DotNetEnglishP7.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DotNetEnglishP7.Tests.Unit.Controllers
{
    [Collection("Sequential")]
    public class CurveControllerUnitTests : UnitTests
    {
        /// <summary>
        /// Test API call of type GET on endpoint /Curve. It should return the list of all Curves.
        /// </summary>
        [Fact]
        public async void Get_Home_ShouldReturnListCurve()
        {
            // Arrange
            List<CurvePoint> seedData = new List<CurvePoint>()
            {
                new CurvePoint() { CurvePointId = 5 },
                new CurvePoint() { CurvePointId = 10 },
                new CurvePoint() { CurvePointId = 3 }
            };
            for (int i = 1; i <= seedData.Count; i++)
            {
                seedData[i - 1].SetId(i);
            }

            var curveService = new Mock<ICurveService>();
            curveService.Setup(x => x.GetAllAsync()).ReturnsAsync(seedData);
            var controller = new CurveController(curveService.Object);

            // Act
            var actionResult = await controller.Home();
            var okResult = actionResult as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.NotNull(okResult.Value);
            Assert.Equal(seedData.Count, ((List<CurvePoint>)okResult.Value).Count);
            Assert.Equal(200, okResult.StatusCode);
        }
        /// <summary>
        /// Test API call of type GET on endpoint /Curve/{id}. It should return the requested Curve.
        /// </summary>
        [Fact]
        public async void Get_GetById_PassInt_ShouldReturnSingleCurve()
        {
            // Arrange
            int idToReturn = 1;
            CurvePoint seedData = new CurvePoint() { CurvePointId = 5 };
            seedData.SetId(idToReturn);

            var curveService = new Mock<ICurveService>();
            curveService.Setup(x => x.GetByIdAsync(idToReturn)).ReturnsAsync(seedData);
            var controller = new CurveController(curveService.Object);

            // Act
            var actionResult = await controller.GetById(idToReturn);
            var okResult = actionResult as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.NotNull(okResult.Value);
            Assert.Equal(seedData.CurvePointId, ((CurvePoint)okResult.Value).CurvePointId);
            Assert.Equal(200, okResult.StatusCode);
        }
        /// <summary>
        /// Test API call of type GET on endpoint /Curve/{id}. It should return NotFound.
        /// </summary>
        [Fact]
        public async void Get_GetById_PassInt_ShouldReturnNotFound()
        {
            // Arrange
            var curveService = new Mock<ICurveService>();
            var controller = new CurveController(curveService.Object);

            // Act
            var actionResult = await controller.GetById(1);
            var notFoundResult = actionResult as NotFoundResult;

            // Assert
            Assert.NotNull(notFoundResult);
            Assert.Equal(404, notFoundResult.StatusCode);
        }
        /// <summary>
        /// Test API call of type POST on endpoint /Curve/Add. It should return the created Curve.
        /// </summary>
        [Fact]
        public async void Post_Add_PassCurve_ShouldReturnSameCurve()
        {
            // Arrange
            int idToCreate = 1;
            CurvePoint seedData = new CurvePoint() { CurvePointId = 5 };
            seedData.SetId(idToCreate);

            var curveService = new Mock<ICurveService>();
            curveService.Setup(x => x.AddAsync(seedData)).ReturnsAsync(seedData);
            var controller = new CurveController(curveService.Object);

            // Act
            var actionResult = await controller.Add(seedData);
            var okResult = actionResult as CreatedAtActionResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.NotNull(okResult.Value);
            Assert.Equal(seedData.CurvePointId, ((CurvePoint)okResult.Value).CurvePointId);
            Assert.Equal(201, okResult.StatusCode);
        }
        /// <summary>
        /// Test API call of type POST on endpoint /Curve/Update. It should return the updated Curve.
        /// </summary>
        [Fact]
        public async void Post_Update_PassCurve_ShouldReturnSameCurve()
        {
            // Arrange
            int idToUpdate = 1;
            CurvePoint seedData = new CurvePoint() { CurvePointId = 5 };
            seedData.SetId(idToUpdate);

            var curveService = new Mock<ICurveService>();
            curveService.Setup(x => x.UpdateAsync(seedData)).ReturnsAsync(seedData);
            curveService.Setup(x => x.ExistAsync(idToUpdate)).ReturnsAsync(true);
            var controller = new CurveController(curveService.Object);

            // Act
            var actionResult = await controller.Update(idToUpdate, seedData);
            var okResult = actionResult as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.NotNull(okResult.Value);
            Assert.Equal(seedData.CurvePointId, ((CurvePoint)okResult.Value).CurvePointId);
            Assert.Equal(200, okResult.StatusCode);
        }
        /// <summary>
        /// Test API call of type DELETE on endpoint /Curve/Delete. It should return the deleted Curve.
        /// </summary>
        [Fact]
        public async void Delete_Delete_PassCurve_ShouldReturnSameCurve()
        {
            // Arrange
            int idToDelete = 1;
            CurvePoint seedData = new CurvePoint() { CurvePointId = 5 };
            seedData.SetId(idToDelete);

            var curveService = new Mock<ICurveService>();
            curveService.Setup(x => x.DeleteAsync(idToDelete)).ReturnsAsync(seedData);
            var controller = new CurveController(curveService.Object);

            // Act
            var actionResult = await controller.Delete(idToDelete);
            var okResult = actionResult as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.NotNull(okResult.Value);
            Assert.Equal(seedData.CurvePointId, ((CurvePoint)okResult.Value).CurvePointId);
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}