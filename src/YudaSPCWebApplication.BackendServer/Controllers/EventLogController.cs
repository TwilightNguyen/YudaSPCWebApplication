using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YudaSPCWebApplication.BackendServer.Data;
using YudaSPCWebApplication.BackendServer.Data.Entities;
using YudaSPCWebApplication.BackendServer.Services;
using YudaSPCWebApplication.ViewModels;
using YudaSPCWebApplication.ViewModels.System;

namespace YudaSPCWebApplication.BackendServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "Bearer")]
    public class EventLogController(
        ApplicationDbContext context
    ) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context; 

        /// <summary>
        /// Url: /api/eventlogs/
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var eventLogs = _context.EventLogs.ToList();

            if (eventLogs == null || eventLogs.Count == 0)
            {
                return NotFound("No event logs found.");
            }

            var eventLogVms = eventLogs.Select(eventLog => new EventLogVm
            {
                Id = eventLog.IntEventID,
                EventTime = eventLog.DtEventTime,
                EventCode = eventLog.StrEventCode,
                Event = eventLog.StrEvent,
                Station = eventLog.StrStation,
            });


            return Ok(eventLogVms);
        }

        /// <summary>
        /// Url: /api/eventlogs/?filter=serchString&pageIndex=1&pageSize=10
        /// </summary>
        /// <returns></returns>
        [HttpGet("Pagging")]
        public async Task<IActionResult> GetPaging(string? filter, int pageIndex, int pageSize)
        {
            var query = _context.EventLogs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                query = query.Where(r => r.StrStation!.Contains(filter));
            }

            List<EventLogVm> items = [.. query.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(role => new EventLogVm{
                    Id = role.IntEventID,
                    EventTime = role.DtEventTime,
                    EventCode = role.StrEventCode,
                    Event = role.StrEvent,
                    Station = role.StrStation,
                })];
            
            if (items.Count == 0)
            {
                return NotFound("No event logs found.");
            }

            var paginaton = new Pagination<EventLogVm>()
            {
                Items = items,
                TotalRecords = query.Count()
            };

            return Ok(paginaton);
        }

        /// <summary>
        /// Url: /api/eventlogs/{Id}
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// 
        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var eventLog = _context.EventLogs.FirstOrDefault(r => r.IntEventID == Id);

            if (eventLog == null)
            {
                return NotFound("Event log not found.");
            }
            var eventLogVm = new EventLogVm
            {
                Id = eventLog.IntEventID,
                EventTime = eventLog.DtEventTime,
                EventCode = eventLog.StrEventCode,
                Event = eventLog.StrEvent,
                Station = eventLog.StrStation,
            }; 
            return Ok(eventLogVm);
        }
    }
}
