using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YudaSPCWebApplication.BackendServer.Data;
using YudaSPCWebApplication.ViewModels.System.Shift;

namespace YudaSPCWebApplication.BackendServer.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "Bearer")]
    public class ShiftsController(ApplicationDbContext context) : ControllerBase
    {
        public readonly ApplicationDbContext _context = context;

        ///<summary>
        /// Url: /api/shifts
        /// </summary>
        /// 
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var shifts = await _context.Shifts.ToListAsync();
            if(shifts == null || shifts.Count == 0)
            {
                return NotFound("No Shift found.");
            }

            var shiftVms = shifts.Select(shift => new ShiftVm
            {
                Id = shift.IntID,
                Name = shift.StrNameShift,
                StartTime = shift.DtStartTime,
                EndTime = shift.DtEndTime,
            });

            return Ok(shiftVms);
        }

        ///<summary>
        /// Url: /api/shifts/{Id}
        /// </summary>
        /// 
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var shift = _context.Shifts.FirstOrDefault(x => x.IntID == id);

            if (shift == null) {
                return NotFound("Shift not found.");
            }
            var shiftVm = new ShiftVm
            {
                Id = shift.IntID,
                Name = shift.StrNameShift,
                StartTime = shift.DtStartTime,
                EndTime = shift.DtEndTime,
            };

            return Ok(shiftVm);
        }
    }


}
