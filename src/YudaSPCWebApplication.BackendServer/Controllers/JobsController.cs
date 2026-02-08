using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using YudaSPCWebApplication.BackendServer.Data;
using YudaSPCWebApplication.BackendServer.Data.Entities;
using YudaSPCWebApplication.ViewModels;
using YudaSPCWebApplication.ViewModels.System;

namespace YudaSPCWebApplication.BackendServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "Bearer")]
    public class JobsController(ApplicationDbContext context): ControllerBase
    { 
        private readonly ApplicationDbContext _context = context;

        /// <summary>
        /// Url: /api/jobs
        /// </summary>
        /// 
        [HttpPut]
        public async Task<IActionResult> CreateJob([FromBody] JobCreateRequest request)
        {
            if (!ModelState.IsValid) { 
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(request.JobCode))
            {
                return BadRequest("Job Code cannot be empty."); 
            }

            var area = await _context.ProductionAreas.FirstOrDefaultAsync(pa => pa.IntID == request.AreaId);

            if (area == null)
            {
                return BadRequest("Invalid Production Area.");
            }

            var product = await _context.Products.FirstOrDefaultAsync(p => p.IntID == request.ProductId && p.BoolDeleted == false);

            if (product == null) {
                return BadRequest("Invalid Product.");
            }

            var jobExists = await _context.JobDatas.FirstOrDefaultAsync(j => 
                j.StrJobCode == request.JobCode && 
                j.IntAreaID == request.AreaId &&
                j.BoolDeleted == false );

            if (jobExists != null) {
                return BadRequest("Job with the same Job Code already exists in this Production Area.");
            }


            // Typical claim types (depends on your token/issuer)
            var userId = User.FindFirst("sub")?.Value
                         ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var iuser = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            var job = new JobData
            {
                IntAreaID = request.AreaId,
                IntProductID = request.ProductId,
                IntJobDecisionID = 1,
                StrJobCode = request.JobCode,
                StrPOCode = request.POCode,
                StrSOCode = request.SOCode,
                IntJobQty = request.JobQty,
                IntOutputQty = request.OutputQty,
                IntUserID = iuser?.IntUserID ?? -1,
                DtCreateTime = DateTime.Now,
                BoolDeleted = false
            };

            await _context.JobDatas.AddAsync(job);
            var result = await _context.SaveChangesAsync();

            if(result > 0)
            {
                return CreatedAtAction(
                nameof(GetById),
                new {Id = job.IntID},
                new JobVm{
                    Id = job.IntID,
                    AreaId = job.IntAreaID,
                    ProductId = job.IntProductID,
                    JobCode = job.StrJobCode,
                    POCode = job.StrPOCode,
                    SOCode = job.StrSOCode,
                    UserId = job.IntUserID,
                    CreateTime = job.DtCreateTime,
                    JobDecisionId = job.IntJobDecisionID,
                    JobQty = job.IntJobQty,
                    OutputQty = job.IntOutputQty,
                });
            }
            else
            {
                return BadRequest("Failed to create job.");
            }
        }

        ///<summary>
        /// Url: /api/jobs
        /// </summary>
        /// 
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var jobs = await _context.JobDatas.ToListAsync();

            if (jobs == null || jobs.Count == 0)
            {
                return BadRequest("No Job found.");
            }

            var jobVms = jobs.Select(job => new JobVm
            {
                Id = job.IntID,
                AreaId = job.IntAreaID,
                ProductId = job.IntProductID,
                JobCode = job.StrJobCode,
                POCode = job.StrPOCode,
                SOCode = job.StrSOCode,
                JobDecisionId = job.IntJobDecisionID,
                JobQty = job.IntJobQty,
                OutputQty = job.IntOutputQty,
                UserId = job.IntUserID,
                CreateTime = job.DtCreateTime,
            });

            return Ok(jobVms);
        }

        /// <summary>
        /// Url: /api/jobs/paging
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("Paging")]
        public async Task<IActionResult> GetPaging(string filter, int pageIndex, int pageSize)
        {
            var query = _context.JobDatas.Where(j => j.BoolDeleted == false).AsQueryable();

            if (string.IsNullOrWhiteSpace(filter))
            {
                query = query.Where(j => j.StrJobCode!.Contains(filter));
            }

            List<JobVm> items = [..query.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(job => new JobVm{ 
                    Id = job.IntID,
                    JobCode=job.StrJobCode,
                    POCode=job.StrPOCode,
                    SOCode  = job.StrSOCode,
                    JobDecisionId=job.IntJobDecisionID,
                    JobQty = job.IntJobQty,
                    OutputQty = job.IntOutputQty,
                    AreaId = job.IntAreaID,
                    ProductId = job.IntProductID,
                    CreateTime = job.DtCreateTime,
                    UserId = job.IntUserID,
                })];

            var pagination = new Pagination<JobVm> { 
                Items = items,
                TotalRecords = query.Count()
            };

            return Ok(pagination);
        }


        ///<summary>
        /// Url /api/jobs
        /// </summary>
        /// 
        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var job = await _context.JobDatas.FirstOrDefaultAsync(x => x.IntID == Id);

            if(job == null)
            {
                return BadRequest("Job not found.");
            }

            var jobVm = new JobVm
            {
                Id = job.IntID,
                AreaId = job.IntAreaID,
                ProductId = job.IntProductID,
                JobCode = job.StrJobCode,
                POCode = job.StrPOCode,
                SOCode = job.StrSOCode,
                JobDecisionId = job.IntJobDecisionID,
                JobQty = job.IntJobQty,
                OutputQty = job.IntOutputQty,
                CreateTime = job.DtCreateTime,
                UserId = job.IntUserID,
            };

            return Ok(jobVm);
        }

        ///<summary>
        ///Url: /api/jobs/GetByAreaId
        /// </summary>
        /// 
        [HttpGet("GetByAreaId/{AreaId:int}")]
        public async Task<IActionResult> GetByAreaId(int AreaId) { 
            var jobs = await _context.JobDatas.Where(j => j.IntAreaID == AreaId).ToListAsync();

            if (jobs == null || jobs.Count == 0) {
                return BadRequest("No jobs found for the specificed Production Area.");
            }

            var jobVms = jobs.Select(job => new JobVm {
                Id = job.IntID,
                AreaId = job.IntAreaID,
                ProductId = job.IntProductID,
                JobCode = job.StrJobCode,
                POCode = job.StrPOCode,
                SOCode = job.StrSOCode,
                JobDecisionId = job.IntJobDecisionID,
                JobQty = job.IntJobQty,
                OutputQty = job.IntOutputQty,
                CreateTime = job.DtCreateTime,
                UserId = job.IntUserID,
            });

            return Ok(jobVms);
        }

        /// <summary>
        /// Url: /api/jobs
        /// </summary>
        /// 
        [HttpPut("{Id:int}")]
        public async Task<IActionResult> UpdateJob(int Id, JobVm jobVm) {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(Id < 0 || Id == jobVm.Id)
            {
                return BadRequest("Invalid Job Id");
            }

            var job = await _context.JobDatas.FirstOrDefaultAsync(j => 
                j.IntID == jobVm.Id && 
                j.BoolDeleted == false
            );

            if (job == null) {
                return BadRequest("Job not found");
            }

            if (string.IsNullOrWhiteSpace(jobVm.JobCode)) {
                return BadRequest("Job Code cannot be empty.");
            }

            var jobExists = await _context.JobDatas.FirstOrDefaultAsync(j =>
                j.StrJobCode == jobVm.JobCode &&
                j.IntAreaID == jobVm.AreaId &&
                j.BoolDeleted == false
            );

            if (jobExists != null) {
                return BadRequest("Job with the same Job Code already exists in this Production Area.");
            }

            job.StrJobCode = jobVm.JobCode;
            job.StrPOCode = jobVm.POCode;
            job.StrSOCode = jobVm.SOCode;
            job.IntJobDecisionID = jobVm.JobDecisionId;
            job.IntJobQty = jobVm.JobQty;
            job.IntProductID = jobVm.ProductId;

            _context.JobDatas.Update(job);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return Ok(new JobVm
                {
                    Id = job.IntID,
                    AreaId = job.IntAreaID,
                    ProductId = job.IntProductID,
                    JobCode = job.StrJobCode,
                    POCode = job.StrPOCode,
                    SOCode = job.StrSOCode,
                    JobDecisionId = job.IntJobDecisionID,
                    JobQty = job.IntJobQty,
                    OutputQty = job.IntJobQty,
                    CreateTime = job.DtCreateTime,
                    UserId = job.IntUserID,
                });
            }
            else
            {
                return BadRequest("Failed to update Job.");
            }
        }

        ///<summary>
        ///Url: /api/jobs
        /// </summary>
        /// 
        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> DeleteJob(int Id)
        {
            var job = await _context.JobDatas.FirstOrDefaultAsync(j => j.IntID == Id && j.BoolDeleted == false);

            if(job == null)
            {
                return NotFound("Job not found");
            }

            job.BoolDeleted = true;

            _context.JobDatas.Update(job);

            var result = await _context.SaveChangesAsync();
            if (result > 0) {
                return Ok(new JobVm
                {
                    Id = job.IntID,
                    AreaId = job.IntAreaID,
                    ProductId = job.IntProductID,
                    JobCode = job.StrJobCode,
                    POCode = job.StrPOCode,
                    SOCode = job.StrSOCode,
                    JobDecisionId = job.IntJobDecisionID,
                    JobQty = job.IntJobQty,
                    OutputQty = job.IntJobQty,
                    CreateTime = job.DtCreateTime,
                    UserId = job.IntUserID,
                });
            }
            else
            {
                return BadRequest("Failed to delete job.");
            }
        }
    }
}
