using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using YudaSPCWebApplication.BackendServer.Controllers;
using YudaSPCWebApplication.BackendServer.Data;
using YudaSPCWebApplication.BackendServer.Data.Entities;
using YudaSPCWebApplication.ViewModels;
using YudaSPCWebApplication.ViewModels.System.Product;
using YudaSPCWebApplication.ViewModels.System.Production;
using static Azure.Core.HttpHeader;

namespace YudaSPCWebApplication.BackendServer.UnitTest.Controllers
{
    public class ProductionsControllerTest : IAsyncLifetime
    {
        public required ApplicationDbContext _context; 
        public async Task InitializeAsync()
        {
            _context = InMemoryDbContext.GetApplicationDbContext();

            InMemoryDbContext.SeedUsers(_context);
            InMemoryDbContext.SeedRoles(_context);
            InMemoryDbContext.SeedProductionAreas(_context);
            InMemoryDbContext.SeedProcesses(_context);
            InMemoryDbContext.SeedProcessLines(_context);
            InMemoryDbContext.SeedCharacteristics(_context);
            InMemoryDbContext.SeedEventLogs(_context);
            InMemoryDbContext.SeedInspPlanTypes(_context);
            InMemoryDbContext.SeedInspectionPlans(_context);
            InMemoryDbContext.SeedInspectionPlanSubs(_context);
            InMemoryDbContext.SeedInspectionPlanDatas(_context);
            InMemoryDbContext.SeedProducts(_context);
            InMemoryDbContext.SeedJobDecisions(_context);
            InMemoryDbContext.SeedJobs(_context);
            InMemoryDbContext.SeedProductions(_context);

            await _context.SaveChangesAsync();
        }

        public async Task DisposeAsync()
        {
            try { await _context.Database.EnsureDeletedAsync(); } catch { }
            await _context.DisposeAsync();
        }

        [Fact]
        public async Task ShouldCreateInstance_NotNull_Success()
        {
            // Acct
            var controller = new ProductionsController(_context);

            // Assert
            Assert.NotNull(controller);
        }

        [Fact]
        public async Task GetAll_ValidData_Success()
        {
            var controller = new ProductionsController(_context);

            // Act
            var result = await controller.GetAll();

            // Assert
            Assert.NotNull(result);
            var okResult = result as OkObjectResult;
            var productions = okResult?.Value as IEnumerable<ProductionVm>;
            Assert.NotNull(productions);
            Assert.Equal(4, productions?.Count());
        }

        [Fact]
        public async Task GetById_ValidData_Success()
        {
            var controller = new ProductionsController(_context);

            // Act
            var result = await controller.GetById(1);

            // Assert
            Assert.NotNull(result);
            var okResult = result as OkObjectResult;
            var production = okResult?.Value as ProductionVm;
            Assert.NotNull(production);
            Assert.Equal(1, production.Id);
        }

        [Fact]
        public async Task GetById_NotFound_Failure()
        {
            var controller = new ProductionsController(_context);

            // Act
            var result = await controller.GetById(999);

            // Assert
            Assert.NotNull(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundResult);
            Assert.Equal(404, notFoundResult?.StatusCode);
            Assert.Equal("Production not found.", notFoundResult?.Value);
        }

        [Fact]
        public async Task GetPaging_ValidData_Success()
        {
            var controller = new ProductionsController (_context);

            // Act
            var result = await controller.GetPaging(1, 2);

            // Assert
            Assert.NotNull(result);
            var okResult = result as OkObjectResult;
            var pagedResult = okResult?.Value as Pagination<ProductionVm>;
            Assert.NotNull(pagedResult);
            Assert.Equal(4, pagedResult?.TotalRecords);
            Assert.Equal(2, pagedResult?.Items.Count);
        }

        [Fact]
        public async Task CreateProduction_ValidData_Success()
        {
            var controller = new ProductionsController(_context);
 
            // Act
            var productiondate = DateTime.Now.AddDays(1);
            var result = await controller.CreateProduction(new ProductionCreateRequest
            {
                JobId = 2,
                LineId = 1,
                LotInform = "Lot Inform 1 new",
                MaterialInform = "Material Inform 1 new",
                Notes = "Notes 1 new",
                ProductionDate = productiondate,
                ProductionQty = 400,
            });

            // Assert
            Assert.NotNull(result);

            var createResult = result as CreatedAtActionResult;
            Assert.NotNull(createResult);
            var production = createResult?.Value as ProductionVm;
            Assert.NotNull(production);
            Assert.Equal(2, production.JobId);
            Assert.Equal(1, production.LineId);
            Assert.Equal("Lot Inform 1 new", production.LotInform);
            Assert.Equal("Material Inform 1 new", production.MaterialInform);
            Assert.Equal("Notes 1 new", production.Notes);
            Assert.Equal(productiondate, production.ProductionDate);
            Assert.Equal(400, production.ProductionQty);
        }

        [Fact]
        public async Task CreateProduction_InvalidJob_Failure()
        {
            var controller = new ProductionsController(_context);

            // Act
            var productiondate = DateTime.Now.AddDays(1);
            var result = await controller.CreateProduction(new ProductionCreateRequest
            {
                JobId = 999,
                LineId = 1,
                LotInform = "Lot Inform 2 new",
                MaterialInform = "Material Inform 2 new",
                Notes = "Notes 2 new",
                ProductionDate = productiondate,
                ProductionQty = 400,
            });

            // Assert
            Assert.NotNull(result);
            var badRequest = result as BadRequestObjectResult;
            Assert.NotNull(badRequest);
            Assert.Equal("Invalid Job.", badRequest?.Value);
        }

        [Fact]
        public async Task CreateProduction_InvalidLine_Failure()
        {
            var controller = new ProductionsController(_context);

            // Act
            var productiondate = DateTime.Now.AddDays(1);
            var result = await controller.CreateProduction(new ProductionCreateRequest
            {
                JobId = 2,
                LineId = 999,
                LotInform = "Lot Inform 3 new",
                MaterialInform = "Material Inform 3 new",
                Notes = "Notes 3 new",
                ProductionDate = productiondate,
                ProductionQty = 400,
            });

            // Assert
            Assert.NotNull(result);
            var badRequest = result as BadRequestObjectResult;
            Assert.NotNull(badRequest);
            Assert.Equal("Invalid Process Line.", badRequest?.Value);
        }

        [Fact] 
        public async Task CreateProduction_InvalidArea_Failure()
        {
            var controller = new ProductionsController(_context);

            // Act
            var productiondate = DateTime.Now.AddDays(1);
            var result = await controller.CreateProduction(new ProductionCreateRequest
            {
                JobId = 2,
                LineId = 4,
                LotInform = "Lot Inform 4 new",
                MaterialInform = "Material Inform 4 new",
                Notes = "Notes 4 new",
                ProductionDate = productiondate,
                ProductionQty = 400,
            });

            // Assert
            Assert.NotNull(result);
            var badRequest = result as BadRequestObjectResult;
            Assert.NotNull(badRequest);
            Assert.Equal("Job and Process Line are not in the same Production Area.", badRequest?.Value);
        }

        [Fact]
        public async Task UpdateProduction_ValidData_Success() { 
            var controller = new ProductionsController(_context);

            // Act
            var productiondate = DateTime.Now;
            var result = await controller.UpdateProduction(1, new ProductionVm { 
                Id = 1,
                LotInform = "Lot Inform 1 update",
                MaterialInform = "Material Inform 1 update",
                Notes = "Notes 1 update",
                ProductionDate = productiondate,
                ProductionQty = 100,
            });

            // Asset 
            Assert.NotNull (result);
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            var production = okResult?.Value as ProductionVm;
            Assert.NotNull(production);

            Assert.Equal("Lot Inform 1 update", production.LotInform);
            Assert.Equal("Material Inform 1 update", production.MaterialInform);
            Assert.Equal("Notes 1 update", production.Notes);
            Assert.Equal(productiondate, production.ProductionDate);
            Assert.Equal(100, production.ProductionQty);

        }

        [Fact]
        public async void UpdateProduction_InvalidId_Failure()
        {
            var controller = new ProductionsController(_context);

            // Act
            var productiondate = DateTime.Now;
            var result = await controller.UpdateProduction(1, new ProductionVm
            {
                Id = 2,
                LotInform = "Lot Inform 2 update",
                MaterialInform = "Material Inform 2 update",
                Notes = "Notes 2 update",
                ProductionDate = productiondate,
                ProductionQty = 100,
            });

            // Asset 
            Assert.NotNull(result);
            var badResult = result as BadRequestObjectResult;
            Assert.NotNull(badResult);
            Assert.Equal("Invalid Production Id.",badResult?.Value);
        }

        [Fact]
        public async void UpdateProduction_NotFound_Failure() {
            var controller = new ProductionsController(_context);

            // Act
            var productiondate = DateTime.Now;
            var result = await controller.UpdateProduction(999, new ProductionVm
            {
                Id = 999,
                LotInform = "Lot Inform 999 update",
                MaterialInform = "Material Inform 999 update",
                Notes = "Notes 999 update",
                ProductionDate = productiondate,
                ProductionQty = 999,
            });

            // Asset 
            Assert.NotNull(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundResult);
            Assert.Equal("Production not found.", notFoundResult?.Value);
        }

        [Fact]
        public async Task EndProduction_ValidData_Success()
        {
            var controller = new ProductionsController(_context);

            // Act
            var result = await controller.EndProduction(2);

            // Asset 
            Assert.NotNull(result);
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            var production = okResult?.Value as ProductionVm;
            Assert.NotNull(production);

            Assert.NotNull(production.EndTime); 
        }

        [Fact]
        public async void EndProduction_InvalidId_Failure()
        {
            var controller = new ProductionsController(_context);

            // Act
            var productiondate = DateTime.Now;
            var result = await controller.EndProduction(-1);

            // Asset 
            Assert.NotNull(result);
            var badResult = result as BadRequestObjectResult;
            Assert.NotNull(badResult);
            Assert.Equal("Invalid Production Id.", badResult?.Value);
        }

        [Fact]
        public async void EndProduction_NotFound_Failure()
        {
            var controller = new ProductionsController(_context);

            // Act
            var productiondate = DateTime.Now;
            var result = await controller.EndProduction(999);

            // Asset 
            Assert.NotNull(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundResult);
            Assert.Equal("Production not found.", notFoundResult?.Value);
        }

        [Fact]
        public async Task DeleteProduction_ValidData_Success()
        {
            var controller = new ProductionsController(_context);

            // Act
            var result = await controller.DeleteProduction(4);

            // Asset 
            Assert.NotNull(result);
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            var production = okResult?.Value as ProductionVm;
            Assert.NotNull(production);

            Assert.Equal(4, production.Id);
        }

        [Fact]
        public async void DeleteProduction_InvalidId_Failure()
        {
            var controller = new ProductionsController(_context);

            // Act
            var productiondate = DateTime.Now;
            var result = await controller.DeleteProduction(-1);

            // Asset 
            Assert.NotNull(result);
            var badResult = result as BadRequestObjectResult;
            Assert.NotNull(badResult);
            Assert.Equal("Invalid Production Id.", badResult?.Value);
        }

        [Fact]
        public async void DeleteProduction_NotFound_Failure()
        {
            var controller = new ProductionsController(_context);

            // Act
            var productiondate = DateTime.Now;
            var result = await controller.DeleteProduction(999);

            // Asset 
            Assert.NotNull(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundResult);
            Assert.Equal("Production not found.", notFoundResult?.Value);
        }
    }
}
