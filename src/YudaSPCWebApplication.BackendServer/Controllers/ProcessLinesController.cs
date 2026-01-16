using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YudaSPCWebApplication.BackendServer.Data;
using YudaSPCWebApplication.ViewModels.System;

namespace YudaSPCWebApplication.BackendServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "Bearer")]
    public class ProcessLinesController(ApplicationDbContext context) : ControllerBase
    {
        // Controller code will go here

        public readonly ApplicationDbContext _context = context;

        /// <summary>
        /// Url: /api/processes/
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = _context.ProcessLines.ToList();

            if (result == null || result.Count == 0) return NotFound("No Process Lines found.");

            var lineVms = result.Select(process => new ProcesslineVm
            {
                Id = process.IntID,
                Name = process.StrProcessLineName ?? string.Empty,
                Code = process.StrProcessLineCode ?? string.Empty,
                ProcessId = process.IntProcessID
            }).ToList();

            return Ok(lineVms);
        }

        /// <summary>
        /// Url: /api/proccess/{Id}
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// 
        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var line = _context.ProcessLines.FirstOrDefault(r => r.IntID == Id);

            if (line == null)
            {
                return NotFound("Process Line not found.");
            }

            var lineVm = new ProcesslineVm
            {
                Id = line.IntID,
                Name = line.StrProcessLineName ?? string.Empty,
                Code = line.StrProcessLineCode ?? string.Empty,
                ProcessId = line.IntProcessID
            };
            return Ok(lineVm);
        }

        /// <summary>
        /// Url: /api/proccess/{Id}
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// 
        [HttpGet("GetByProcessId/{ProcessId:int}")]
        public async Task<IActionResult> GetByProcessId(int ProcessId)
        {
            var area = _context.Processes.FirstOrDefault(r => r.IntID == ProcessId);
            if (area == null)
            {
                return NotFound("Process not found.");
            }

            var result = _context.ProcessLines.Where(x => x.IntProcessID == ProcessId).ToList();

            if (result == null || result.Count == 0) return NotFound("No Process Lines found.");

            var lineVms = result.Select(line => new ProcesslineVm
            {
                Id = line.IntID,
                Name = line.StrProcessLineName ?? string.Empty,
                Code = line.StrProcessLineCode ?? string.Empty,
                ProcessId = line.IntProcessID
            }).ToList();

            return Ok(lineVms);
        }
    }
}
