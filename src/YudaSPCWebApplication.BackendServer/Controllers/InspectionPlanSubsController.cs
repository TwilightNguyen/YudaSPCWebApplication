using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YudaSPCWebApplication.BackendServer.Data;
using YudaSPCWebApplication.BackendServer.Data.Entities;
using YudaSPCWebApplication.ViewModels;
using YudaSPCWebApplication.ViewModels.System;

namespace YudaSPCWebApplication.BackendServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "Bearer")]
    public class InspectionPlanSubsController(ApplicationDbContext context) : ControllerBase
    {
        public readonly ApplicationDbContext _context = context;

        /// <summary>
        ///  URL: /api/inspectionplansubs/2
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateInspectionPlanSub([FromBody] InspectionPlanSubCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var inspPlan = _context.InspectionPlans.FirstOrDefault(p => p.IntID == request.InspPlanId && p.BoolDeleted == false);

            if (inspPlan == null)
            {
                return BadRequest("Invalid Inspection Plan.");
            }

            var inspPlanType = _context.InspPlanTypes.FirstOrDefault(p => p.IntID == request.PlanTypeId);

            if (inspPlanType == null)
            {
                return BadRequest("Invalid Inspection Plan Type.");
            }

            var charExists = _context.InspectionPlanSubs.FirstOrDefault(x =>
                x.IntInspPlanID == request.InspPlanId &&
                x.IntPlanTypeID == request.PlanTypeId &&
                x.BoolDeleted == false);

            if (charExists != null)
            {
                return BadRequest("Inspection Plan Sub with the same Inspection Plan and Plan Type already exists.");
            }

            var inspectionPlanSub = new InspectionPlanSub
            {
                IntInspPlanID = request.InspPlanId,
                IntPlanTypeID = request.PlanTypeId,
                DtCreateTime = DateTime.UtcNow,
                BoolDeleted = false,
            };

            _context.InspectionPlanSubs.Add(inspectionPlanSub);

            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return CreatedAtAction(
                    nameof(GetById),
                    new { Id = inspectionPlanSub.IntID },
                    new InspectionPlanSubVm
                    {
                        Id = inspectionPlanSub.IntID,
                        InspPlanId = inspectionPlanSub.IntInspPlanID,
                        PlanTypeId = inspectionPlanSub.IntPlanTypeID,
                        CreateTime = inspectionPlanSub.DtCreateTime,
                    }
                );
            }
            else
            {
                return BadRequest("Failed to create Inspection Plan Sub.");
            }
        }

        /// <summary>
        /// Url: /api/inspectionplansubs/
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var inspectionPlanSubs = _context.InspectionPlanSubs.Where(r => r.BoolDeleted == false).ToList();

            if (inspectionPlanSubs == null) return NotFound("No inspection plan sub found.");

            var inspectionPlanSubVms = inspectionPlanSubs.Select(inspectionPlanSub => new InspectionPlanSubVm
            {
                Id = inspectionPlanSub.IntID,
                InspPlanId = inspectionPlanSub.IntInspPlanID,
                PlanTypeId = inspectionPlanSub.IntPlanTypeID,
                CreateTime = inspectionPlanSub.DtCreateTime,
            });

            return Ok(inspectionPlanSubVms);
        }

        /// <summary>
        /// Url: /api/inspectionplansubs/?pageIndex=1&pageSize=10
        /// </summary>
        /// <returns></returns>
        [HttpGet("Pagging")]
        public async Task<IActionResult> GetPaging(int pageIndex, int pageSize)
        {
            var query = _context.InspectionPlanSubs.AsQueryable();

           
            List<InspectionPlanSubVm> items = [.. query.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(inspectionPlan => new InspectionPlanSubVm
                {
                    Id = inspectionPlan.IntID,
                    InspPlanId = inspectionPlan.IntInspPlanID,
                    PlanTypeId = inspectionPlan.IntPlanTypeID,
                    CreateTime = inspectionPlan.DtCreateTime,
                })];

            var paginaton = new Pagination<InspectionPlanSubVm>()
            {
                Items = items,
                TotalRecords = query.Count()
            };

            return Ok(paginaton);
        }

        /// <summary>
        /// Url: /api/inspectionplansubs/{Id}
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var inspectionPlanSub = _context.InspectionPlanSubs.FirstOrDefault(r => r.IntID == Id && r.BoolDeleted == false);

            if (inspectionPlanSub == null)
            {
                return NotFound("Inspection Plan Sub not found.");
            }

            var inspectionPlanSubVm = new InspectionPlanSubVm
            {
                Id  = inspectionPlanSub.IntID,
                InspPlanId = inspectionPlanSub.IntInspPlanID,
                PlanTypeId = inspectionPlanSub.IntPlanTypeID,
                CreateTime = inspectionPlanSub.DtCreateTime,
            };

            return Ok(inspectionPlanSubVm);
        }


        /// <summary>
        /// Url: /api/inspectionplansubs/{Id}
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet("/GetByInsPlanId/{InsPlanId:int}")]
        public async Task<IActionResult> GetByInsPlanId(int InsPlanId)
        {
            var inspectionPlanSubs = _context.InspectionPlanSubs.Where(r => r.IntInspPlanID == InsPlanId && r.BoolDeleted == false);

            if (inspectionPlanSubs == null || inspectionPlanSubs?.Count() == 0)
            {
                return NotFound("No Inspection Plan Subs found for the given Inspection Plan.");
            }

            var InspectionPlanSubVms = inspectionPlanSubs?.Select(inspectionPlanSub => new InspectionPlanSubVm
            {
                Id = inspectionPlanSub.IntID,
                InspPlanId = inspectionPlanSub.IntInspPlanID,
                PlanTypeId = inspectionPlanSub.IntPlanTypeID,
                CreateTime = inspectionPlanSub.DtCreateTime,
            });

            return Ok(InspectionPlanSubVms);
        }

        /// <summary>
        /// Url: /api/inspectionplansubs/{Id}
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> DeleteInspectionPlanSub(int Id)
        {
            var inspectionPlanSub = _context.InspectionPlanSubs.FirstOrDefault(r => r.IntID == Id && r.BoolDeleted == false);

            if (inspectionPlanSub == null)
            {
                return NotFound("Inspection Plan Sub not found.");
            }

            inspectionPlanSub.BoolDeleted = true;
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return Ok(new InspectionPlanSubVm
                {
                    Id = inspectionPlanSub.IntID,
                    InspPlanId = inspectionPlanSub.IntInspPlanID,
                    PlanTypeId = inspectionPlanSub.IntPlanTypeID,
                    CreateTime = inspectionPlanSub.DtCreateTime,
                });
            }
            else
            {
                return BadRequest("Failed to delete Inspection Plan Sub.");
            }
        }
    }
}
