using Dot.Net.WebApi.Domain;
using DotNetEnglishP7.Repositories;
using DotNetEnglishP7.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetEnglishP7.Tests.Unit.Services
{
    [Collection("Sequential")]
    public class CurveServiceUnitTests : UnitTests
    {
        [Fact]
        public async void GetAllAsync_ShouldReturnListCurve()
        {
            // Arrange
            List<CurvePoint> seedData = new List<CurvePoint>()
            {
                new CurvePoint() { CurvePointId = 5, Term = 4.34 },
                new CurvePoint() { CurvePointId = 10, Term = 5.321 },
                new CurvePoint() { CurvePointId = 7, Term = 9.00 }
            };
            for (int i = 1; i <= seedData.Count; i++)
            {
                seedData[i - 1].SetId(i);
            }

            var curveRepository = new Mock<ICurveRepository>();
            curveRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(seedData);

            // Act
            ICurveService curveService = new CurveService(curveRepository.Object);
            var curveResult = await curveService.GetAllAsync();

            // Assert
            Assert.NotNull(curveResult);
            Assert.Equal(3, curveResult.Count);
            Assert.Equal(10, curveResult[1].CurvePointId);
        }
        [Fact]
        public async void GetByIdAsync_PassInt_ShouldReturnSingleCurve()
        {
            // Arrange
            CurvePoint seedData = new CurvePoint() { CurvePointId = 5, Term = 4.34 };
            seedData.SetId(1);

            var curveRepository = new Mock<ICurveRepository>();
            curveRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(seedData);

            // Act
            ICurveService curveService = new CurveService(curveRepository.Object);
            var curveResult = await curveService.GetByIdAsync(1);

            // Assert
            Assert.NotNull(curveResult);
            Assert.Equal(5, curveResult.CurvePointId);
            Assert.Equal(4.34, curveResult.Term);
        }
        [Fact]
        public async void GetByIdAsync_PassInt_ShouldReturnNull()
        {
            // Arrange
            var curveRepository = new Mock<ICurveRepository>();

            // Act
            ICurveService curveService = new CurveService(curveRepository.Object);
            var curveResult = await curveService.GetByIdAsync(1);

            // Assert
            Assert.Null(curveResult);
        }
        [Fact]
        public async void AddAsync_PassCurve_ShouldReturnSameCurve()
        {
            // Arrange
            CurvePoint seedData = new CurvePoint() { CurvePointId = 5, Term = 4.34 };
            seedData.SetId(1);

            var curveRepository = new Mock<ICurveRepository>();
            curveRepository.Setup(x => x.AddAsync(seedData)).ReturnsAsync(seedData);

            // Act
            ICurveService curveService = new CurveService(curveRepository.Object);
            var curveResult = await curveService.AddAsync(seedData);

            // Assert
            Assert.NotNull(curveResult);
            Assert.Equal(seedData.Id, curveResult.Id);
            Assert.Equal(seedData.CurvePointId, curveResult.CurvePointId);
            Assert.Equal(seedData.Term, curveResult.Term);
        }
        [Fact]
        public async void UpdateAsync_PassCurve_ShouldReturnSameCurve()
        {
            // Arrange
            int idToUpdate = 1;
            CurvePoint seedData = new CurvePoint() { CurvePointId = 5, Term = 4.34 };
            seedData.SetId(idToUpdate);

            var curveRepository = new Mock<ICurveRepository>();
            curveRepository.Setup(x => x.GetByIdAsync(idToUpdate)).ReturnsAsync(seedData);
            curveRepository.Setup(x => x.UpdateAsync(seedData)).ReturnsAsync(seedData);

            // Act
            ICurveService curveService = new CurveService(curveRepository.Object);
            var curveResult = await curveService.UpdateAsync(seedData);

            // Assert
            Assert.NotNull(curveResult);
            Assert.Equal(seedData.Id, curveResult.Id);
            Assert.Equal(seedData.CurvePointId, curveResult.CurvePointId);
            Assert.Equal(seedData.Term, curveResult.Term);
        }
        [Fact]
        public async void DeleteAsync_PassCurve_ShouldReturnSameCurve()
        {
            // Arrange
            CurvePoint seedData = new CurvePoint() { CurvePointId = 5, Term = 4.34 };
            seedData.SetId(1);

            var curveRepository = new Mock<ICurveRepository>();
            curveRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(seedData);
            curveRepository.Setup(x => x.DeleteAsync(seedData)).ReturnsAsync(seedData);

            // Act
            ICurveService curveService = new CurveService(curveRepository.Object);
            var curveResult = await curveService.DeleteAsync(1);

            // Assert
            Assert.NotNull(curveResult);
            Assert.Equal(seedData.Id, curveResult.Id);
            Assert.Equal(seedData.CurvePointId, curveResult.CurvePointId);
            Assert.Equal(seedData.Term, curveResult.Term);
        }
    }
}