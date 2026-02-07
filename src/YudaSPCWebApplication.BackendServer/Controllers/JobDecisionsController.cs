using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YudaSPCWebApplication.BackendServer.Data;
using YudaSPCWebApplication.ViewModels.System;

namespace YudaSPCWebApplication.BackendServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "Bearer")]
    public class JobDecisionsController(ApplicationDbContext context) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;

        ///<summary>
        /// /api/jobdecisions
        /// </summary>
        /// 
        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var jobDecisions = _context.JobDecisions.ToList();

            if (jobDecisions == null || jobDecisions.Count == 0) {
                return BadRequest("No job decision found.");
            }

            var jobDecisionVms = jobDecisions.Select(jobDecision => new JobDecisionVm
            {
                Id = jobDecision.IntID,
                Decision = jobDecision.StrDecision,
                ColorCode = jobDecision.IntColorCode,
            });

            return Ok(jobDecisionVms);
        }
    }
}
