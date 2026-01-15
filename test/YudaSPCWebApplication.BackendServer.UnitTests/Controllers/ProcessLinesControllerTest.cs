using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using YudaSPCWebApplication.BackendServer.Controllers;
using YudaSPCWebApplication.BackendServer.Data;
using YudaSPCWebApplication.BackendServer.Data.Entities;
using YudaSPCWebApplication.ViewModels.System;

namespace YudaSPCWebApplication.BackendServer.UnitTest.Controllers
{
    public class ProcessLinesControllerTest
    {
        private readonly ApplicationDbContext _context;

        public ProcessLinesControllerTest()
        {
            _context = InMemoryDbContext.GetApplicationDbContext();
            InMemoryDbContext.SeedProductionAreas(_context);
            InMemoryDbContext.SeedProcesses(_context);
            InMemoryDbContext.SeedProcessLines(_context);
        }


        [Fact]
        public void ShouldCreateInstance_NotNull_Success()
        {
            // Act
            var controller = new ProcessLinesController(_context);

            // Assert
            Assert.NotNull(controller);
        }

        [Fact]
        public async Task GetAll_HasData_Success()
        {
            var controller = new ProcessLinesController(_context);

            // Act
            var result = await controller.GetAll();
            // Assert
            Assert.NotNull(result);
            var okResult = result as OkObjectResult;
            var linesList = okResult?.Value as IEnumerable<ProcesslineVm>;
            Assert.Equal(3, linesList?.Count());
            Assert.Contains(linesList!, a => a.Name == "Process A Line A");
            Assert.Contains(linesList!, a => a.Name == "Process A Line B");
            Assert.Contains(linesList!, a => a.Name == "Process B Line C");

        }

        [Fact]
        public async Task GetById_HasData_Success()
        {
            var controller = new ProcessLinesController(_context);

            // Act
            var result = await controller.GetById(1);
            // Assert
            Assert.NotNull(result);
            var okResult = result as OkObjectResult;
            var process = okResult?.Value as ProcesslineVm;
            Assert.Equal(1, process?.Id);
        }

        [Fact]
        public async Task GetById_NotFound_Failed()
        {
            // Act
            var controller = new ProcessLinesController(_context);


            var result = await controller.GetById(4);

            // Assert
            Assert.NotNull(result);

            var notFoundRequest = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Process Line not found.", notFoundRequest.Value);
        }

        [Fact]
        public async Task GetByAreaId_HasData_Success()
        {
            var controller = new ProcessLinesController(_context);

            // Act
            var result = await controller.GetByProcessId(1);
            // Assert
            Assert.NotNull(result);
            var okResult = result as OkObjectResult;
            var linesList = okResult?.Value as IEnumerable<ProcesslineVm>;
            Assert.Equal(2, linesList?.Count());
            Assert.Contains(linesList!, a => a.Name == "Process A Line A");
            Assert.Contains(linesList!, a => a.Name == "Process A Line B");
        }

        [Fact]
        public async Task GetByProcessId_LinesNotFound_Failed()
        {
            // Act
            var controller = new ProcessLinesController(_context);

            var result = await controller.GetByProcessId(3);

            // Assert
            Assert.NotNull(result);

            var notFoundRequest = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("No Process Lines found.", notFoundRequest.Value);
        }

        [Fact]
        public async Task GetByProcessId_ProcessNotFound_Failed()
        {
            // Act
            var controller = new ProcessLinesController(_context);

            var result = await controller.GetByProcessId(4);

            // Assert
            Assert.NotNull(result);

            var notFoundRequest = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Process not found.", notFoundRequest.Value);
        }
    }
}
