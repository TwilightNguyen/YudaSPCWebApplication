using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YudaSPCWebApplication.BackendServer.Data;
using YudaSPCWebApplication.ViewModels.System;

namespace YudaSPCWebApplication.BackendServer.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "Bearer")]
    public class ProductionAreasController(ApplicationDbContext context) : ControllerBase
    {
        // Controller code will go here

        public readonly ApplicationDbContext _context = context;

        /// <summary>
        /// Url: /api/productionarea/
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = _context.ProductionAreas.ToList();

            if (result == null || result.Count == 0) return NotFound("No production areas found.");

            var areaVms = result.Select(area => new ProductionAreaVm
            {
                Id = area.IntID,
                Name = area.StrNameArea??string.Empty
            }).ToList();

            return Ok(areaVms);
        }

        /// <summary>
        /// Url: /api/productionarea/{Id}
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// 
        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var area = _context.ProductionAreas.FirstOrDefault(r => r.IntID == Id);

            if (area == null)
            {
                return NotFound("Production Area not found.");
            }

            var areaVm = new ProductionAreaVm
            {
                Id = area.IntID,
                Name = area.StrNameArea ?? string.Empty
            };
            return Ok(areaVm);
        }
    }
}
