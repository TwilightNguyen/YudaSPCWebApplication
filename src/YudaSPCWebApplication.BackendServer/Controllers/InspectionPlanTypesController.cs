using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using YudaSPCWebApplication.BackendServer.Data;
using YudaSPCWebApplication.ViewModels.System;

namespace YudaSPCWebApplication.BackendServer.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "Bearer")]
    public class InspectionPlanTypesController(ApplicationDbContext context) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;

        ///<summary>
        /// /api/inspectionplantypes
        /// </summary>
        /// 
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var inspPlanTypes = _context.InspPlanTypes.ToList();

            if (inspPlanTypes == null || inspPlanTypes.Count == 0) {
                return BadRequest("Inspection Plan Type not found.");
            }

            var inspPlanTypeVms = inspPlanTypes.Select(inspPlanType => new InspectionPlanVm
            {
                Id = inspPlanType.IntID,
                Name = inspPlanType.StrPlanTypeName??string.Empty,
            });

            return Ok(inspPlanTypeVms);
        }

        ///<summary>
        /// /api/inspectionplantypes/GetByInspPlanId
        /// </summary>
        /// 
        [HttpGet("GetByInspPlanId/{InspPlanId:int}")]
        public async Task<IActionResult> GetByInspPlanId(int InspPlanId)
        {
            var inspPlanSubs = await _context.InspectionPlanSubs
                .Where( i => i.BoolDeleted == false && i.IntInspPlanID == InspPlanId)
                .Select(i => i.IntPlanTypeID)
                .ToListAsync();

            var inspPlanTypes = await _context.InspPlanTypes.Where( i => inspPlanSubs.Contains(i.IntID)).ToListAsync();

            var inspPlanTypeVms = inspPlanTypes.Select(inspPlanType => new InspectionPlanTypeVm
            {
                Id = inspPlanType.IntID,
                Name = inspPlanType.StrPlanTypeName??string.Empty,
            });

            return Ok(inspPlanTypeVms);
        }
    }
}
