using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using YudaSPCWebApplication.BackendServer.Data;
using YudaSPCWebApplication.BackendServer.Data.Entities;
using YudaSPCWebApplication.ViewModels;
using YudaSPCWebApplication.ViewModels.System.Production;

namespace YudaSPCWebApplication.BackendServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "Bearer")]
    public class ProductionsController(ApplicationDbContext context) : ControllerBase
    {
        public readonly ApplicationDbContext _context = context;

        /// <summary>
        /// Url: /api/productions/
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductionCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var job = await _context.JobDatas.FirstOrDefaultAsync(j => j.IntID == request.JobId && j.BoolDeleted == false );
            if(job == null)
            {
                return BadRequest("Invalid Job.");
            }

            var line = await _context.ProcessLines.FirstOrDefaultAsync(l => l.IntID == request.LineId);
            if(line == null)
            {
                return BadRequest("Invalid Process Line.");
            }

            var process = await _context.Processes.FirstOrDefaultAsync(p => p.IntID == line.IntProcessID);

            if (process == null || job.IntAreaID != process.IntAreaID) {
                return BadRequest("Job and Process Line are not in the same Production Area.");
            }

            // Typical claim types (depends on your token/issuer)
            var userId = User?.FindFirst("sub")?.Value
                     ?? User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var iuser = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);


            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var productionRunning = await _context.ProductionDatas.FirstOrDefaultAsync(p => p.IntLineID == request.LineId && p.DtEndTime == null && p.BoolDeleted == false);
                
                if(productionRunning != null)
                {
                    productionRunning.DtEndTime = DateTime.Now;
                    _context.ProductionDatas.Update(productionRunning);
                }

                var production = new ProductionData
                {
                    IntJobID = request.JobId,
                    IntLineID = request.LineId,
                    DtProductionDate = request.ProductionDate,
                    DtStartTime = DateTime.Now,
                    IntProductionQty = request.ProductionQty,
                    StrLotInform = request.LotInform,
                    StrMaterialInform = request.MaterialInform,
                    StrNotes = request.Notes,
                    DtEndTime = null,
                    BoolDeleted = false,
                    IntUserID = iuser?.IntUserID ?? -1,
                };

                _context.ProductionDatas.Add(production);

                var result = await _context.SaveChangesAsync();

                if(result > 1)
                {
                    return CreatedAtAction(
                        nameof(GetById),
                        new {Id = production.IntID},
                        new ProductionVm
                        {
                            Id = production.IntID,
                            JobId = production.IntJobID,
                            LineId = production.IntLineID,
                            ProductionDate = production.DtProductionDate,
                            ProductionQty = production.IntProductionQty,
                            LotInform = production.StrLotInform,
                            MaterialInform = production.StrMaterialInform,
                            Notes = production.StrNotes,
                            UserId = production.IntUserID ?? -1,
                            StartTime = production.DtStartTime,
                            EndTime = production.DtEndTime,
                        }
                    );
                }
                
            }
            catch (Exception) {

            }

            // 3. Nếu có lỗi, hoàn tác mọi thay đổi đã thực hiện ở trên
            await transaction.RollbackAsync();
            return BadRequest("Failed to create Production.");
        }

        ///<summary>
        /// Url: /api/productions/
        /// </summary>
        /// 
        [HttpGet]
        public async Task <IActionResult> GetAll()
        {
            var productions = await _context.ProductionDatas.Where(p => p.BoolDeleted == false).ToListAsync();

            if(productions == null || productions.Count == 0)
            {
                return NotFound("No Production found.");
            }

            var productionVms = productions.Select(production => new ProductionVm
            {
                Id = production.IntID,
                JobId = production.IntJobID,
                LineId = production.IntLineID,
                ProductionDate = production.DtStartTime,
                ProductionQty = production.IntProductionQty,
                LotInform = production.StrLotInform,
                MaterialInform = production.StrMaterialInform,
                Notes = production.StrNotes,
                StartTime = production.DtStartTime,
                EndTime = production.DtEndTime,
                UserId = production.IntUserID,
            });

            return Ok( productionVms );
        }

        ///<summary>
        /// Url: /api/productions/paging/
        /// </summary>
        /// 
        [HttpGet("Paging")]
        public async Task<IActionResult> GetPaging(int pageIndex, int pageSize)
        {
            var query = _context.ProductionDatas.Where(p => p.BoolDeleted == false).AsQueryable();
            
            List<ProductionVm> items = [..query.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(production => new ProductionVm{
                    Id = production.IntID,
                    JobId = production.IntJobID,
                    LineId = production.IntLineID,
                    ProductionDate = production.DtStartTime,
                    ProductionQty = production.IntProductionQty,
                    LotInform = production.StrLotInform,
                    MaterialInform = production.StrMaterialInform,
                    Notes = production.StrNotes,
                    StartTime = production.DtStartTime,
                    EndTime = production.DtEndTime,
                    UserId = production.IntUserID,
                })];

            var pagination = new Pagination<ProductionVm>
            {
                Items = items,
                TotalRecords = items.Count,
            };

            return Ok(pagination);
        }


        /// <summary>
        /// Url: /api/productions/{id}
        /// </summary>
        /// 
        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var production = await _context.ProductionDatas.FirstOrDefaultAsync(p => p.IntID == Id && p.BoolDeleted == false);
            
            if(production == null)
            {
                return NotFound("Production not found.");
            }

            var productionVm = new ProductionVm
            {
                Id = production.IntID,
                JobId = production.IntJobID,
                LineId = production.IntLineID,
                ProductionDate = production.DtStartTime,
                ProductionQty = production.IntProductionQty,
                LotInform = production.StrLotInform,
                MaterialInform = production.StrMaterialInform,
                Notes = production.StrNotes,
                StartTime = production.DtStartTime,
                EndTime = production.DtEndTime,
                UserId = production.IntUserID,
            };

            return Ok(productionVm);
        }

        ///<summary>
        /// Url: /api/productions/
        /// </summary>
        /// 
        [HttpPut("{Id:int}")]
        public async Task<IActionResult> UpdateProduction(int Id, ProductionVm productionVm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(Id < 0 || Id != productionVm.Id)
            {
                return BadRequest("Invalid Production Id.");
            }

            var production = await _context.ProductionDatas.FirstOrDefaultAsync(p => p.IntID == Id && p.BoolDeleted == false);

            if(production == null)
            {
                return NotFound("Production not found.");
            }

            production.StrLotInform = productionVm.LotInform;
            production.StrMaterialInform = productionVm.MaterialInform;
            production.StrNotes = productionVm.Notes;
            production.IntProductionQty = productionVm.ProductionQty;

            _context.ProductionDatas.Update(production);

            var result = await _context.SaveChangesAsync();

            if(result > 0)
            {
                return Ok(new ProductionVm
                {
                    Id = production.IntID,
                    JobId = production.IntJobID,
                    LineId = production.IntLineID,
                    ProductionDate = production.DtStartTime,
                    ProductionQty = production.IntProductionQty,
                    LotInform = production.StrLotInform,
                    MaterialInform = production.StrMaterialInform,
                    Notes = production.StrNotes,
                    StartTime = production.DtStartTime,
                    EndTime = production.DtEndTime,
                    UserId = production.IntUserID,
                });
            }
            else
            {
                return BadRequest("Failed to update production.");
            }
        }

        ///<summary>
        /// Url: /api/productions/endproduction
        /// </summary>
        /// 
        [HttpPut("EndProduction/{Id:int}")]
        public async Task<IActionResult> EndProduction(int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (Id < 0)
            {
                return BadRequest("Invalid Production Id.");
            }

            var production = await _context.ProductionDatas.FirstOrDefaultAsync(p => p.IntID == Id && p.BoolDeleted == false);

            if (production == null)
            {
                return NotFound("Production not found.");
            }

            production.DtEndTime = DateTime.Now;

            _context.ProductionDatas.Update(production);

            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return Ok(new ProductionVm
                {
                    Id = production.IntID,
                    JobId = production.IntJobID,
                    LineId = production.IntLineID,
                    ProductionDate = production.DtStartTime,
                    ProductionQty = production.IntProductionQty,
                    LotInform = production.StrLotInform,
                    MaterialInform = production.StrMaterialInform,
                    Notes = production.StrNotes,
                    StartTime = production.DtStartTime,
                    EndTime = production.DtEndTime,
                    UserId = production.IntUserID,
                });
            }
            else
            {
                return BadRequest("Failed to end production.");
            }
        }

        ///<summary>
        /// Url: /api/productions/
        /// </summary>
        /// 
        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> DeleteProduction(int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (Id < 0)
            {
                return BadRequest("Invalid Production Id.");
            }

            var production = await _context.ProductionDatas.FirstOrDefaultAsync(p => p.IntID == Id && p.BoolDeleted == false);

            if (production == null)
            {
                return NotFound("Production not found.");
            }

            production.BoolDeleted = true;

            _context.ProductionDatas.Update(production);

            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return Ok(new ProductionVm
                {
                    Id = production.IntID,
                    JobId = production.IntJobID,
                    LineId = production.IntLineID,
                    ProductionDate = production.DtStartTime,
                    ProductionQty = production.IntProductionQty,
                    LotInform = production.StrLotInform,
                    MaterialInform = production.StrMaterialInform,
                    Notes = production.StrNotes,
                    StartTime = production.DtStartTime,
                    EndTime = production.DtEndTime,
                    UserId = production.IntUserID,
                });
            }
            else
            {
                return BadRequest("Failed to delete production.");
            }
        }
    }
}
