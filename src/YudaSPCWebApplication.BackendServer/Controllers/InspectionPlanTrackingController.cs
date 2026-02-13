using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YudaSPCWebApplication.BackendServer.Data;
using YudaSPCWebApplication.ViewModels;
using YudaSPCWebApplication.ViewModels.System.InspectionPlanTracking;

namespace YudaSPCWebApplication.BackendServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "Bearer")]
    public class InspectionPlanTrackingController (ApplicationDbContext context): ControllerBase
    {
        public readonly ApplicationDbContext _context = context;

        /// <summary>
        /// Url: /api/inspectionplantracking/
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var inspPlanTrackings = _context.InspectionPlanTracking.ToList();

            if (inspPlanTrackings == null) return NotFound("No inspection plan tracking found.");

            var inspPlanTrackingVms = inspPlanTrackings.Select(inspectionPlanTracking => new InspectionPlanTrackingVm
            {
                InspPlanID = inspectionPlanTracking.IntInspPlanID,
                IntID = inspectionPlanTracking.IntID,
                PlanState = inspectionPlanTracking.IntPlanState,
                PlanTypeID = inspectionPlanTracking.IntPlanTypeID,
                UpdateTime = inspectionPlanTracking.DtUpdateTime,
                UserID = inspectionPlanTracking.IntUserID,
            });


            return Ok(inspPlanTrackingVms);
        }

        /// <summary>
        /// Url: /api/inspectionplantracking/Pagging/?pageIndex=1&pageSize=10
        /// </summary>
        /// <returns></returns>
        [HttpGet("Pagging")]
        public async Task<IActionResult> GetPaging(int pageIndex, int pageSize)
        {
            var baseQuery = _context.InspectionPlanTracking.AsNoTracking();

            var totalRecords = await baseQuery.CountAsync();

            var skip = (pageIndex - 1) * pageSize;

            var pageQuery =
                from ipt in baseQuery
                join u in _context.Users
                    on ipt.IntUserID equals u.IntUserID into gj   // đổi 'Id' đúng cột User
                join p in _context.InspectionPlans
                    on ipt.IntInspPlanID equals p.IntID into pgj
                join pt in _context.InspPlanTypes
                    on ipt.IntPlanTypeID equals pt.IntID into ptgj
                from u in gj.DefaultIfEmpty()
                from p in pgj.DefaultIfEmpty()
                from pt in ptgj.DefaultIfEmpty()
                select new InspectionPlanTrackingVm
                {
                    InspPlanID = ipt.IntInspPlanID,
                    IntID = ipt.IntID,
                    PlanState = ipt.IntPlanState,
                    PlanTypeID = ipt.IntPlanTypeID,
                    UpdateTime = ipt.DtUpdateTime,
                    UserID = ipt.IntUserID,
                    UserName = u.UserName,
                    InspPlanName = p.StrInspPlanName,
                    PlanTypeName = pt.StrPlanTypeName
                };

            var items = await pageQuery
                .OrderBy(x => x.InspPlanID)
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            var pagination = new Pagination<InspectionPlanTrackingVm>
            {
                Items = items,
                TotalRecords = totalRecords
            };

            return Ok(pagination);
        }

        /// <summary>
        /// Url: /api/inspectionplantracking/GetByInspPlanIdAndPlanTypeId/?intId=1planTypeId=1
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetByInspPlanIdAndPlanTypeId")]
        public async Task<IActionResult> GetByInspPlanIdAndPlanTypeId(int intId, int? planTypeId = -1)
        {
            var baseQuery = _context.InspectionPlanTracking
                .Where(x => x.IntInspPlanID == intId && (planTypeId == -1 || x.IntPlanTypeID == planTypeId))
                .AsNoTracking();

            var pageQuery =
                from ipt in baseQuery
                join u in _context.Users
                    on ipt.IntUserID equals u.IntUserID into gj   // đổi 'Id' đúng cột User
                join p in _context.InspectionPlans
                    on ipt.IntInspPlanID equals p.IntID into pgj
                join pt in _context.InspPlanTypes
                    on ipt.IntPlanTypeID equals pt.IntID into ptgj
                from u in gj.DefaultIfEmpty()
                from p in pgj.DefaultIfEmpty()
                from pt in ptgj.DefaultIfEmpty()
                select new InspectionPlanTrackingVm
                {
                    InspPlanID = ipt.IntInspPlanID,
                    IntID = ipt.IntID,
                    PlanState = ipt.IntPlanState,
                    PlanTypeID = ipt.IntPlanTypeID,
                    UpdateTime = ipt.DtUpdateTime,
                    UserID = ipt.IntUserID,
                    UserName = u.UserName,
                    InspPlanName = p.StrInspPlanName,
                    PlanTypeName = pt.StrPlanTypeName
                };

            

            var inspectionPlanTrackings = await pageQuery
                .OrderBy(x => x.UpdateTime)
                .ToListAsync();
            
            if (!await pageQuery.AnyAsync())
            {
                return NotFound("No inspection plan tracking found for the given Inspection Plan and Plan Type.");
            }

            return Ok(inspectionPlanTrackings);
        }
    }
}
