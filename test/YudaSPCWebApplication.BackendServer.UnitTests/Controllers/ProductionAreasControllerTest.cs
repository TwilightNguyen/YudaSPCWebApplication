using Microsoft.AspNetCore.Mvc;
using Moq;
using YudaSPCWebApplication.BackendServer.Controllers;
using YudaSPCWebApplication.BackendServer.Data;
using YudaSPCWebApplication.BackendServer.Data.Entities;
using YudaSPCWebApplication.ViewModels.System;

namespace YudaSPCWebApplication.BackendServer.UnitTest.Controllers
{
    public class ProductionAreasControllerTest
    {
        private readonly ApplicationDbContext _context;

        public ProductionAreasControllerTest()
        {
            _context = new InMemoryDbContextProductionArea().GetApplicationDbContext();
            _context.ProductionAreas.AddRange(new List<ProductionArea>
            {
                new() { IntID = 1, StrNameArea = "Tape" },
                new() { IntID = 2, StrNameArea = "Layout" },
                new() { IntID = 3, StrNameArea = "Block Vial" }
            });

            _context.SaveChangesAsync().ConfigureAwait(true);
        }


        [Fact]
        public void ShouldCreateInstance_NotNull_Success()
        {

            // Act
            var controller = new ProductionAreasController(_context);

            // Assert
            Assert.NotNull(controller);

        }

        [Fact]
        public async Task GetAll_HasData_Success()
        {
            var controller = new ProductionAreasController(_context);
             
            // Act
            var result = await controller.GetAll();
            // Assert
            Assert.NotNull(result);
            var okResult = result as OkObjectResult;
            var areasList = okResult?.Value as IEnumerable<ProductionAreaVm>;
            Assert.Equal(3, areasList?.Count());
            Assert.Contains(areasList!, a => a.Name == "Tape");
            Assert.Contains(areasList!, a => a.Name == "Layout");
            Assert.Contains(areasList!, a => a.Name == "Block Vial");
        }

        [Fact]
        public async Task GetById_HasData_Success()
        {
            var controller = new ProductionAreasController(_context);

            // Act
            var result = await controller.GetById(1);
            // Assert
            Assert.NotNull(result);
            var okResult = result as OkObjectResult;
            var areasList = okResult?.Value as ProductionAreaVm;
            Assert.Equal(1, areasList?.Id);
        }

        [Fact]
        public async Task GetById_NotFound_Failed()
        {
            // Act
            var controller = new ProductionAreasController(_context);
            

            var result = await controller.GetById(4);

            // Assert
            Assert.NotNull(result);

            var notFoundRequest = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Production Area not found.", notFoundRequest.Value);
        }
    }
}
