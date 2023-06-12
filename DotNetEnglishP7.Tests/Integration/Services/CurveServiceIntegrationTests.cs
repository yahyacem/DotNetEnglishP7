using Dot.Net.WebApi.Controllers;
using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using DotNetEnglishP7.Identity;
using DotNetEnglishP7.Repositories;
using DotNetEnglishP7.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetEnglishP7.Tests.Integration.Services
{
    [Collection("Sequential")]
    public class CurveServiceIntegrationTests : IntegrationTests
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
                    new CurvePoint() { Id = 1, CurvePointId = 5 },
                    new CurvePoint() { Id = 2, CurvePointId = 10 },
                    new CurvePoint() { Id = 3, CurvePointId = 3 }
                };
                context.CurvePoints.AddRange(seedData);
                context.SaveChanges();

                // Instantiate repository, service and controller
                ICurveRepository curveRepository = new CurveRepository(context);
                ICurveService curveService = new CurveService(curveRepository);

                // Act
                // Make the call and capture result
                var result = await curveService.GetAllAsync();

                // Assert
                // Check response data
                Assert.Equal(seedData.Count, result.Count);
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

                // Act
                // Make the call and capture result
                var result = await curveService.GetByIdAsync(idToReturn);

                // Assert
                // Check response data
                Assert.NotNull(result);
                Assert.Equal(seedData.CurvePointId, result.CurvePointId);
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

                // Act
                // Make the call and capture result
                var result = await curveService.GetByIdAsync(idToReturn);

                // Assert
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
                CurvePoint seedData = new CurvePoint() { Id = idToCreate, CurvePointId = 5 };

                // Instantiate repository, service and controller
                ICurveRepository curveRepositoy = new CurveRepository(context);
                ICurveService curveService = new CurveService(curveRepositoy);

                // Act
                // Make the call and capture result
                var result = await curveService.AddAsync(seedData);

                // Assert
                // Check response data
                Assert.NotNull(result);
                Assert.Equal(seedData.CurvePointId, result.CurvePointId);
                // Check data in database
                Assert.Single(await curveService.GetAllAsync());
                Assert.NotNull(await curveService.GetByIdAsync(idToCreate));
            }
        }
        /// <summary>
        /// Test API call of type POST on endpoint /CurvePoint/Update. It should return the updated CurvePoint.
        /// </summary>
        [Fact]
        public async void PUT_Update_PassCurvePoint_ShouldReturnSameCurvePoint()
        {
            using (var context = new LocalDbContext(GetOptions()))
            {
                // Arrange
                int idToUpdate = 1;
                // Insert seed data
                CurvePoint seedData = new CurvePoint() { Id = idToUpdate, CurvePointId = 10 };
                context.CurvePoints.Add(seedData);
                context.SaveChanges();
                // Prepare new data to update
                CurvePoint updatedSeedData = new CurvePoint() { Id = idToUpdate, CurvePointId = 5 };

                // Instantiate repository, service and controller
                ICurveRepository curveRepository = new CurveRepository(context);
                ICurveService curveService = new CurveService(curveRepository);

                // Act
                // Make the call and capture result
                var result = await curveService.UpdateAsync(updatedSeedData);

                // Assert
                // Check response data
                Assert.NotNull(result);
                Assert.Equal(updatedSeedData.CurvePointId, result.CurvePointId);
                // Check data in database
                var curvePointUpdated = await curveService.GetByIdAsync(idToUpdate);
                Assert.NotNull(curvePointUpdated);
                Assert.Equal(updatedSeedData.CurvePointId, curvePointUpdated.CurvePointId);
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
                    new CurvePoint() { Id = 1, CurvePointId = 5 },
                    new CurvePoint() { Id = 2, CurvePointId = 10 },
                    new CurvePoint() { Id = 3, CurvePointId = 15 }
                };
                context.CurvePoints.AddRange(seedData);
                context.SaveChanges();

                // Instantiate repository, service and controller
                ICurveRepository curveRepositoy = new CurveRepository(context);
                ICurveService curveService = new CurveService(curveRepositoy);

                // Act
                // Make the call and capture result
                var result = await curveService.DeleteAsync(idToDelete);

                // Assert
                // Check response data
                Assert.NotNull(result);
                Assert.Equal(seedData.First(x => x.Id == idToDelete).CurvePointId, result.CurvePointId);
                // Check data in database
                Assert.Null(await curveService.GetByIdAsync(idToDelete));
                Assert.Equal(2, (await curveService.GetAllAsync()).Count);
            }
        }

    }
}
