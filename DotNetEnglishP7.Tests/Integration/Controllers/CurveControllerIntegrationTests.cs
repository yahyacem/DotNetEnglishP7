using Dot.Net.WebApi.Controllers;
using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using DotNetEnglishP7.Repositories;
using DotNetEnglishP7.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetEnglishP7.Tests.Integration.Controllers
{
    [Collection("Sequential")]
    public class CurveControllerIntegrationTests : IntegrationTests
    {
        /// <summary>
        /// Test API call of type GET on endpoint /CurvePoint. It should return the list of all CurvePoint.
        /// </summary>
        [Fact]
        public async void GET_Home_ShouldReturnListCurvePoint()
        {
            using (var context = new LocalDbContext(GetOptions()))
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
                context.CurvePoints.AddRange(seedData);
                context.SaveChanges();

                // Instantiate repository, service and controller
                ICurveRepository curveRepository = new CurveRepository(context);
                ICurveService curveService = new CurveService(curveRepository);
                var controller = new CurveController(curveService);

                // Act
                // Make the call and capture result
                var actionResult = await controller.Home();
                var okResult = actionResult as OkObjectResult;

                // Assert
                // Check response code
                Assert.NotNull(okResult);
                Assert.NotNull(okResult.Value);
                Assert.Equal(200, okResult.StatusCode);
                // Check response data
                Assert.Equal(seedData.Count, ((List<CurvePoint>)okResult.Value).Count);
            }
        }
        /// <summary>
        /// Test API call of type GET on endpoint /CurvePoint/{id}. It should return the requested CurvePoint.
        /// </summary>
        [Fact]
        public async void GET_GetById_PassInt_ShouldReturnSameCurvePoint()
        {
            using (var context = new LocalDbContext(GetOptions()))
            {
                // Arrange
                int idToReturn = 1;
                CurvePoint seedData = new CurvePoint() { CurvePointId = 5 };
                context.CurvePoints.Add(seedData);
                context.SaveChanges();

                // Instantiate repository, service and controller
                ICurveRepository curveRepositoy = new CurveRepository(context);
                ICurveService curveService = new CurveService(curveRepositoy);
                var controller = new CurveController(curveService);

                // Act
                // Make the call and capture result
                var actionResult = await controller.GetById(idToReturn);
                var okResult = actionResult as OkObjectResult;

                // Assert
                // Check response code
                Assert.NotNull(okResult);
                Assert.NotNull(okResult.Value);
                Assert.Equal(200, okResult.StatusCode);
                // Check response data
                Assert.Equal(seedData.CurvePointId, ((CurvePoint)okResult.Value).CurvePointId);
            }
        }
        /// <summary>
        /// Test API call of type GET on endpoint /CurvePoint/{id}. It should return NotFound.
        /// </summary>
        [Fact]
        public async void GET_GetById_PassInt_ShouldReturnNull()
        {
            using (var context = new LocalDbContext(GetOptions()))
            {
                // Arrange
                int idToReturn = 1;

                // Instantiate repository, service and controller
                ICurveRepository curveRepository = new CurveRepository(context);
                ICurveService curveService = new CurveService(curveRepository);
                var controller = new CurveController(curveService);

                // Act
                // Make the call and capture result
                var actionResult = await controller.GetById(idToReturn);
                var notFoundResult = actionResult as NotFoundResult;

                // Assert
                // Check response code
                Assert.NotNull(notFoundResult);
                Assert.Equal(404, notFoundResult.StatusCode);
                // Check data in database
                Assert.Empty(await curveService.GetAllAsync());
                Assert.Null(await curveService.GetByIdAsync(idToReturn));
            }
        }
        /// <summary>
        /// Test API call of type POST on endpoint /CurvePoint/Add. It should return the created BiCurvePointdList.
        /// </summary>
        [Fact]
        public async void POST_Add_PassCurvePoint_ShouldReturnSameCurvePoint()
        {
            using (var context = new LocalDbContext(GetOptions()))
            {
                // Arrange
                int idToCreate = 1;
                CurvePoint seedData = new CurvePoint() { CurvePointId = 5 };
                seedData.SetId(idToCreate);

                // Instantiate repository, service and controller
                ICurveRepository curveRepositoy = new CurveRepository(context);
                ICurveService curveService = new CurveService(curveRepositoy);
                var controller = new CurveController(curveService);

                // Act
                // Make the call and capture result
                var actionResult = await controller.Add(seedData);
                var okResult = actionResult as CreatedAtActionResult;

                // Check response code
                Assert.NotNull(okResult);
                Assert.NotNull(okResult.Value);
                Assert.Equal(201, okResult.StatusCode);
                // Check response data
                Assert.Equal(seedData.CurvePointId, ((CurvePoint)okResult.Value).CurvePointId);
                // Check data in database
                Assert.Single(await curveService.GetAllAsync());
                Assert.NotNull(await curveService.GetByIdAsync(idToCreate));
            }
        }
        /// <summary>
        /// Test API call of type POST on endpoint /CurvePoint/Update. It should return the updated CurvePoint.
        /// </summary>
        [Fact]
        public async void POST_Update_PassCurvePoint_ShouldReturnSameCurvePoint()
        {
            using (var context = new LocalDbContext(GetOptions()))
            {
                // Arrange
                int idToUpdate = 1;
                // Insert seed data
                CurvePoint seedData = new CurvePoint() { CurvePointId = 5 };
                seedData.SetId(idToUpdate);
                context.CurvePoints.Add(seedData);
                context.SaveChanges();
                // Prepare new data to update
                CurvePoint updatedSeedData = new CurvePoint() { CurvePointId = 5 };

                // Instantiate repository, service and controller
                ICurveRepository curveRepository = new CurveRepository(context);
                ICurveService curveService = new CurveService(curveRepository);
                var controller = new CurveController(curveService);

                // Act
                // Make the call and capture result
                var actionResult = await controller.Update(idToUpdate, updatedSeedData);
                var okResult = actionResult as OkObjectResult;

                // Check response code
                Assert.NotNull(okResult);
                Assert.NotNull(okResult.Value);
                Assert.Equal(200, okResult.StatusCode);
                // Check response data
                Assert.Equal(updatedSeedData.CurvePointId, ((CurvePoint)okResult.Value).CurvePointId);
                // Check data in database
                Assert.NotNull(await curveService.GetByIdAsync(idToUpdate));
            }
        }
        /// <summary>
        /// Test API call of type DELETE on endpoint /CurvePoint/Delete. It should return the deleted CurvePoint.
        /// </summary>
        [Fact]
        public async void DELETE_Delete_PassCurvePoint_ShouldReturnSameCurvePoint()
        {
            using (var context = new LocalDbContext(GetOptions()))
            {
                // Arrange
                int idToDelete = 1;
                // Insert seed data
                List<CurvePoint> seedData = new List<CurvePoint>()
                {
                    new CurvePoint() { CurvePointId = 5 },
                    new CurvePoint() { CurvePointId = 10 },
                    new CurvePoint() { CurvePointId = 15 }
                };
                for (int i = 1; i <= seedData.Count; i++)
                {
                    seedData[i - 1].SetId(i);
                }
                context.CurvePoints.AddRange(seedData);
                context.SaveChanges();

                // Instantiate repository, service and controller
                ICurveRepository curveRepositoy = new CurveRepository(context);
                ICurveService curveService = new CurveService(curveRepositoy);
                var controller = new CurveController(curveService);

                // Act
                // Make the call and capture result
                var actionResult = await controller.Delete(idToDelete);
                var okResult = actionResult as OkObjectResult;

                // Assert
                // Check response code
                Assert.NotNull(okResult);
                Assert.NotNull(okResult.Value);
                Assert.Equal(200, okResult.StatusCode);
                // Check response data
                Assert.Equal(seedData.First(x => x.Id == idToDelete).CurvePointId, ((CurvePoint)okResult.Value).CurvePointId);
                // Check data in database
                Assert.Null(await curveService.GetByIdAsync(idToDelete));
                Assert.Equal(2, (await curveService.GetAllAsync()).Count);
            }
        }

    }
}
