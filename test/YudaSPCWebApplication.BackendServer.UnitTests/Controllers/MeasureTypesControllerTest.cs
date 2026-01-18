using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using YudaSPCWebApplication.BackendServer.Controllers;
using YudaSPCWebApplication.BackendServer.Data;
using YudaSPCWebApplication.ViewModels.System;

namespace YudaSPCWebApplication.BackendServer.UnitTest.Controllers
{
    public class MeasureTypesControllerTest
    {
        public readonly ApplicationDbContext _context;
        public MeasureTypesControllerTest()
        {
            _context = InMemoryDbContext.GetApplicationDbContext();
            InMemoryDbContext.SeedMeasureTypes(_context);
        }

        [Fact]
        public void ShouldCreateInstance_NotNull_Success()
        {
            // Act
            var controller = new MeasureTypesController(_context);
            // Assert
            Assert.NotNull(controller);
        }

        [Fact]
        public void GetAll_HasData_Success()
        {
            var controller = new MeasureTypesController(_context);
            // Act
            var result = controller.GetAll().Result;
            // Assert
            Assert.NotNull(result);
            var okResult = result as Microsoft.AspNetCore.Mvc.OkObjectResult;
            var measureTypeList = okResult?.Value as IEnumerable<MeasureTypeVm>;

            Assert.Equal(3, measureTypeList?.Count());
            Assert.Contains(measureTypeList!, a => a.Name == "Length");
            Assert.Contains(measureTypeList!, a => a.Name == "Weight");
            Assert.Contains(measureTypeList!, a => a.Name == "Temperature");
        }

        [Fact]
        public async Task GetById_HasData_Success()
        {
            var controller = new MeasureTypesController(_context);
            // Act
            var result = await controller.GetById(1);
            // Assert
            Assert.NotNull(result);
            var okResult = result as OkObjectResult;
            var measureType = okResult?.Value as MeasureTypeVm;
            Assert.Equal("Length", measureType?.Name);

        }

        [Fact]
        public async Task CreateMeasureType_ValidData_Success()
        {
            var controller = new MeasureTypesController(_context);
            var request = new MeasureTypeCreateRequest
            {
                Name = "Pressure"
            };
            // Act
            var result = await controller.CreateMeasureType(request);
            // Assert
            Assert.NotNull(result);
            var createdAtActionResult = result as CreatedAtActionResult;
            Assert.NotNull(createdAtActionResult);
            var createdMeasureType = createdAtActionResult?.Value as MeasureTypeVm;
            Assert.NotNull(createdMeasureType);
            Assert.Equal("Pressure", createdMeasureType.Name);
        }

        [Fact]
        public async Task CreateMeasureType_DuplicateName_BadRequest()
        {
            var controller = new MeasureTypesController(_context);
            var request = new MeasureTypeCreateRequest
            {
                Name = "Length" // Already exists in seeded data
            };
            // Act
            var result = await controller.CreateMeasureType(request);
            // Assert
            Assert.NotNull(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            Assert.Equal("Measure type with the same name already exists.", badRequestResult.Value);
        }

        [Fact]
        public async Task CreateMeasureType_EmptyName_BadRequest()
        {
            var controller = new MeasureTypesController(_context);
            var request = new MeasureTypeCreateRequest
            {
                Name = "" // Empty name
            };
            // Act
            var result = await controller.CreateMeasureType(request);
            // Assert
            Assert.NotNull(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            Assert.Equal("Measure type name cannot be empty.", badRequestResult.Value);
        }

        [Fact]
        public async Task CreateMeasureType_NullName_BadRequest()
        {
            var controller = new MeasureTypesController(_context);
            var request = new MeasureTypeCreateRequest
            {
                Name = null // Null name
            };
            // Act
            var result = await controller.CreateMeasureType(request);
            // Assert
            Assert.NotNull(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            Assert.Equal("Measure type name cannot be empty.", badRequestResult.Value);
        }

        [Fact]
        public async Task UpdateMeasureType_ValidData_Success()
        {
            var controller = new MeasureTypesController(_context);
            var request = new MeasureTypeVm
            {
                Id = 1,
                Name = "Updated Length"
            };
            // Act
            var result = await controller.UpdateMeasureType(1, request);
            // Assert
            Assert.NotNull(result);
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            var updatedMeasureType = okResult?.Value as MeasureTypeVm;
            Assert.NotNull(updatedMeasureType);
            Assert.Equal("Updated Length", updatedMeasureType.Name);
        }

        [Fact]
        public async Task UpdateMeasureType_NonExistentId_NotFound()
        {
            var controller = new MeasureTypesController(_context);
            var request = new MeasureTypeVm
            {
                Id = 999, // Non-existent ID
                Name = "Non-existent"
            };
            // Act
            var result = await controller.UpdateMeasureType(999, request);
            // Assert
            Assert.NotNull(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundResult);
            Assert.Equal("Measure type not found.", notFoundResult.Value);
        }

        [Fact]
        public async Task UpdateMeasureType_DuplicateName_BadRequest()
        {
            var controller = new MeasureTypesController(_context);
            var request = new MeasureTypeVm
            {
                Id = 1,
                Name = "Weight" // Name already exists for another measure type
            };
            // Act
            var result = await controller.UpdateMeasureType(1, request);
            // Assert
            Assert.NotNull(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            Assert.Equal("Measure type with the same name already exists.", badRequestResult.Value);
        }

        [Fact]
        public async Task UpdateMeasureType_EmptyName_BadRequest()
        {
            var controller = new MeasureTypesController(_context);
            var request = new MeasureTypeVm
            {
                Id = 1,
                Name = "" // Empty name
            };
            // Act
            var result = await controller.UpdateMeasureType(1, request);
            // Assert
            Assert.NotNull(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            Assert.Equal("Measure type name cannot be empty.", badRequestResult.Value);
        }

        [Fact]
        public async Task UpdateMeasureType_NullName_BadRequest()
        {
            var controller = new MeasureTypesController(_context);
            var request = new MeasureTypeVm
            {
                Id = 1,
                Name = null // Null name
            };
            // Act
            var result = await controller.UpdateMeasureType(1, request);
            // Assert
            Assert.NotNull(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            Assert.Equal("Measure type name cannot be empty.", badRequestResult.Value);
        }

        [Fact]
        public async Task DeleteMeasureType_ValidId_Success()
        {
            var controller = new MeasureTypesController(_context);
            // Act
            var result = await controller.DeleteMeasureType(2);
            // Assert
            Assert.NotNull(result);
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            var createdMeasureType = okResult?.Value as MeasureTypeVm;
            Assert.NotNull(createdMeasureType);
            Assert.Equal("Weight", createdMeasureType.Name);
        }

        [Fact]
        public async Task DeleteMeasureType_NonExistentId_NotFound()
        {
            var controller = new MeasureTypesController(_context);
            // Act
            var result = await controller.DeleteMeasureType(999); // Non-existent ID
            // Assert
            Assert.NotNull(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundResult);
            Assert.Equal("Measure type not found.", notFoundResult.Value);
        }
    }
}
