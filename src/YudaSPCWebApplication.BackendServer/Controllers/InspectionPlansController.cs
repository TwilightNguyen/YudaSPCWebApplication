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
    public class InspectionPlansController(ApplicationDbContext context) : ControllerBase
    {
        public readonly ApplicationDbContext _context = context;

        /// <summary>
        ///  URL: /api/inspectionplans/
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateInspectionPlan([FromBody] InspectionPlanCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return BadRequest("Inspection Plan name cannot be empty.");
            }

            var area = _context.ProductionAreas.FirstOrDefault(p => p.IntID == request.AreaId);

            if (area == null)
            {
                return BadRequest("Invalid Production Area.");
            }

            
            var charExists = _context.InspectionPlans.FirstOrDefault(x =>
                x.StrInspPlanName == request.Name &&
                x.IntAreaID == request.AreaId &&
                x.BoolDeleted == false);

            if (charExists != null)
            {
                return BadRequest("Inspection Plan with the same name already exists in this Production Area.");
            }


            var inspectionPlan = new InspectionPlan
            {
                StrInspPlanName = request.Name,
                IntAreaID = request.AreaId,
                DtCreateTime = DateTime.UtcNow,
                BoolDeleted = false,
            };

            _context.InspectionPlans.Add(inspectionPlan);

            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return CreatedAtAction(
                    nameof(GetById),
                    new { Id = inspectionPlan.IntID },
                    new InspectionPlanVm {                         
                        Id = inspectionPlan.IntID,
                        Name = inspectionPlan.StrInspPlanName,
                        AreaId = inspectionPlan.IntAreaID,
                    }
                );
            }
            else
            {
                return BadRequest("Failed to create Inspection Plan.");
            }
        }

        /// <summary>
        /// Url: /api/inspectionplans/
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var inspectionPlans = _context.InspectionPlans.Where(r => r.BoolDeleted == false).ToList();

            if (inspectionPlans == null) return NotFound("No inspection plan found.");

            var inspectionPlanVms = inspectionPlans.Select(inspectionPlan => new InspectionPlanVm
            {
                Id = inspectionPlan.IntID,
                Name = inspectionPlan.StrInspPlanName,
                AreaId = inspectionPlan.IntAreaID,
                CreateTime = inspectionPlan.DtCreateTime,
                UpdateTime = inspectionPlan.DtUpdateTime,
            });


            return Ok(inspectionPlanVms);
        }

        /// <summary>
        /// Url: /api/inspectionplans/?filter=serchString&pageIndex=1&pageSize=10
        /// </summary>
        /// <returns></returns>
        [HttpGet("Pagging")]
        public async Task<IActionResult> GetPaging(string? filter, int pageIndex, int pageSize)
        {
            var query = _context.InspectionPlans.Where(r => r.BoolDeleted == false).AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                query = query.Where(r => r.StrInspPlanName!.Contains(filter));
            }

            List<InspectionPlanVm> items = [.. query.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(inspectionPlan => new InspectionPlanVm
                {
                    Id = inspectionPlan.IntID, 
                    Name = inspectionPlan.StrInspPlanName,
                    AreaId = inspectionPlan.IntAreaID,
                    CreateTime = inspectionPlan.DtCreateTime,
                    UpdateTime = inspectionPlan.DtUpdateTime,
                })];

            var paginaton = new Pagination<InspectionPlanVm>()
            {
                Items = items,
                TotalRecords = query.Count()
            };

            return Ok(paginaton);
        }

        /// <summary>
        /// Url: /api/inspectionplans/{Id}
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var inspectionPlan = _context.InspectionPlans.FirstOrDefault(r => r.IntID == Id && r.BoolDeleted == false);

            if (inspectionPlan == null)
            {
                return NotFound("Inspection Plan not found.");
            }

            var inspectionPlanVm = new InspectionPlanVm
            {
                Id = inspectionPlan.IntID,
                Name = inspectionPlan.StrInspPlanName,
                AreaId = inspectionPlan.IntAreaID,
                CreateTime = inspectionPlan.DtCreateTime,
                UpdateTime = inspectionPlan.DtUpdateTime,
            };

            return Ok(inspectionPlanVm);
        }


        /// <summary>
        /// Url: /api/inspectionplans/{Id}
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet("/GetByAreaId/{AreaId:int}")]
        public async Task<IActionResult> GetByAreaId(int AreaId)
        {
            var inspectionPlans = _context.InspectionPlans.Where(r => r.IntAreaID == AreaId && r.BoolDeleted == false);

            if (inspectionPlans == null || inspectionPlans?.Count() == 0)
            {
                return NotFound("No inspection plan found for the specified production area.");
            }

            var InspectionPlanVms = inspectionPlans?.Select(inspectionPlan => new InspectionPlanVm
            {
                Id = inspectionPlan.IntID,
                Name = inspectionPlan.StrInspPlanName,
                AreaId = inspectionPlan.IntAreaID,
                CreateTime = inspectionPlan.DtCreateTime,
                UpdateTime = inspectionPlan.DtUpdateTime,
            });

            return Ok(InspectionPlanVms);
        }


        /// <summary>
        /// Url: /api/inspectionplans/{Id}
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPut("{Id:int}")]
        public async Task<IActionResult> UpdateInspectionPlan(int Id, InspectionPlanVm inspectionPlanVm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (Id < 0 || Id != inspectionPlanVm.Id)
            {
                return BadRequest("Invalid inspection plan Id.");
            }

            var inspectionPlan = _context.InspectionPlans.FirstOrDefault(r => r.IntID == Id && r.BoolDeleted == false);

            if (inspectionPlan == null)
            {
                return NotFound("Inspection Plan not found.");
            }

            if (string.IsNullOrWhiteSpace(inspectionPlanVm.Name))
            {
                return BadRequest("Inspection Plan name cannot be empty.");
            }

            var insPlanExists = _context.InspectionPlans.FirstOrDefault(x =>
                x.StrInspPlanName == inspectionPlanVm.Name &&
                x.IntAreaID == inspectionPlanVm.AreaId &&
                x.BoolDeleted == false &&
                x.IntID != inspectionPlanVm.Id);

            if (insPlanExists != null)
            {
                return BadRequest("Inspection plan with the same name already exists in this production area.");
            }

            inspectionPlan.StrInspPlanName = inspectionPlanVm.Name;
            inspectionPlan.DtUpdateTime = DateTime.UtcNow;


            _context.InspectionPlans.Update(inspectionPlan);

            var result = await _context.SaveChangesAsync();
            
            if (result > 0)
            {
                return Ok(new InspectionPlanVm
                {
                    Id = inspectionPlan.IntID,
                    Name = inspectionPlan.StrInspPlanName,
                    AreaId = inspectionPlan.IntAreaID,
                    CreateTime = inspectionPlan.DtCreateTime,
                    UpdateTime = inspectionPlan.DtUpdateTime,
                });
            }
            else
            {
                return BadRequest("Failed to update inspection plan.");
            }
        }

        /// <summary>
        /// Url: /api/inspectionplans/{Id}
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> DeleteInspectionPlan(int Id)
        {
            var inspectionPlan = _context.InspectionPlans.FirstOrDefault(r => r.IntID == Id && r.BoolDeleted == false);

            if (inspectionPlan == null)
            {
                return NotFound("Inspection plan not found.");
            }

            inspectionPlan.BoolDeleted = true;
            _context.InspectionPlans.Update(inspectionPlan);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return Ok(new InspectionPlanVm
                {
                    Id = inspectionPlan.IntID,
                    Name = inspectionPlan.StrInspPlanName,
                    AreaId = inspectionPlan.IntAreaID,
                    CreateTime = inspectionPlan.DtCreateTime,
                    UpdateTime = inspectionPlan.DtUpdateTime,
                });
            }
            else
            {
                return BadRequest("Failed to delete inspection plan.");
            }
        }
    }
}
