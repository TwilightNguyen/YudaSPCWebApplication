using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.PortableExecutable;
using YudaSPCWebApplication.BackendServer.Data;
using YudaSPCWebApplication.BackendServer.Data.Entities;
using YudaSPCWebApplication.ViewModels;
using YudaSPCWebApplication.ViewModels.System;

namespace YudaSPCWebApplication.BackendServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "Bearer")]
    public class CharacteristicsController(ApplicationDbContext context) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;


        /// <summary>
        ///  URL: /api/characteristics
        /// </summary>
        /// <param name="roleVm"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateCharacteristic([FromBody] CharacteristicCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(request.CharacteristicName))
            {
                return BadRequest("Characteristic name cannot be empty.");
            }

            var process = _context.Processes.FirstOrDefault(p => p.IntID == request.ProcessId);
            
            if(process == null)
            {
                return BadRequest("Invalid Process.");
            }

            var charExists = _context.Characteristices.FirstOrDefault(x => 
                x.StrCharacteristicName == request.CharacteristicName && 
                x.IntProcessID == request.ProcessId &&
                x.BoolDeleted == false);

            if (charExists != null)
            {
                return BadRequest("Characteristic with the same name already exists in this process.");
            }

           
            var characteristic = new Characteristic
            {
                StrCharacteristicName = request.CharacteristicName,
                IntMeaTypeID = request.MeaTypeId,
                IntProcessID = request.ProcessId,
                IntCharacteristicType = request.CharacteristicType,
                StrCharacteristicUnit = request.CharacteristicUnit,
                IntDefectRateLimit = request.DefectRateLimit,
                IntEventEnable = request.EventEnable,
                IntEmailEventModel = request.EmailEventModel,
                IntDecimals = request.Decimals,
                BoolDeleted = false,
            };


            _context.Characteristices.Add(characteristic);

            var result = await _context.SaveChangesAsync();

            if(result > 0)
            {
                return CreatedAtAction(
                    nameof(GetById),
                    new { Id = characteristic.IntID }, 
                    characteristic
                );
            }
            else
            {
                return BadRequest("Failed to create characteristic.");
            }
        }

        /// <summary>
        /// Url: /api/characteristics/
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var characteristics = _context.Characteristices.Where(r => r.BoolDeleted == false).ToList();

            if (characteristics == null) return NotFound("No characteristics found.");

            var characteristicVms = characteristics.Select(characteristic => new CharacteristicVm
            {
                Id = characteristic.IntID,
                CharacteristicName = characteristic.StrCharacteristicName,
                MeaTypeId = characteristic.IntMeaTypeID,
                ProcessId = characteristic.IntProcessID,
                CharacteristicType = characteristic.IntCharacteristicType,
                CharacteristicUnit = characteristic.StrCharacteristicUnit,
                DefectRateLimit = characteristic.IntDefectRateLimit,
                EventEnable = characteristic.IntEventEnable,
                EmailEventModel = characteristic.IntEmailEventModel,
                Decimals = characteristic.IntDecimals,
                Deleted = characteristic.BoolDeleted,
            });
            

            return Ok(characteristicVms);
        }

        /// <summary>
        /// Url: /api/characteristics/?filter=serchString&pageIndex=1&pageSize=10
        /// </summary>
        /// <returns></returns>
        [HttpGet("Pagging")]
        public async Task<IActionResult> GetPaging(string? filter, int pageIndex, int pageSize)
        {
            var query = _context.Characteristices.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                query = query.Where(r => r.StrCharacteristicName!.Contains(filter) && r.BoolDeleted == false);
            }

            List<CharacteristicVm> items = [.. query.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(role => new CharacteristicVm
                {
                    Id = role.IntID,
                    CharacteristicName = role.StrCharacteristicName,
                    MeaTypeId = role.IntMeaTypeID,
                    ProcessId = role.IntProcessID,
                    CharacteristicType = role.IntCharacteristicType,
                    CharacteristicUnit = role.StrCharacteristicUnit,
                    DefectRateLimit = role.IntDefectRateLimit,
                    EventEnable = role.IntEventEnable,
                    EmailEventModel = role.IntEmailEventModel,
                    Decimals = role.IntDecimals,
                    Deleted = role.BoolDeleted,
                })];

            var paginaton = new Pagination<CharacteristicVm>()
            {
                Items = items,
                TotalRecords = query.Count()
            };

            return Ok(paginaton);
        }

        /// <summary>
        /// Url: /api/characteristics/{Id}
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// 
        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var characteristic = _context.Characteristices.FirstOrDefault(r => r.IntID == Id && r.BoolDeleted == false);

            if(characteristic == null)
            {
                return NotFound("Characteristic not found.");
            }

            var characteristicVm = new CharacteristicVm
            {
                Id = characteristic.IntID,
                CharacteristicName = characteristic.StrCharacteristicName,
                MeaTypeId = characteristic.IntMeaTypeID,
                ProcessId = characteristic.IntProcessID,
                CharacteristicType = characteristic.IntCharacteristicType,
                CharacteristicUnit = characteristic.StrCharacteristicUnit,
                DefectRateLimit = characteristic.IntDefectRateLimit,
                EventEnable = characteristic.IntEventEnable,
                EmailEventModel = characteristic.IntEmailEventModel,
                Decimals = characteristic.IntDecimals,
                Deleted = characteristic.BoolDeleted,
            };

            return Ok(characteristicVm);
        }


        /// <summary>
        /// Url: /api/characteristics/{Id}
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// 
        [HttpGet("/GetByProcessId/{ProcessId:int}")]
        public async Task<IActionResult> GetByProcessId(int ProcessId)
        {
            var characteristics = _context.Characteristices.Where(r => r.IntProcessID == ProcessId && r.BoolDeleted == false);

            if (characteristics == null)
            {
                return NotFound("No characteristics found for the specified process.");
            }

            var characteristicVms = characteristics.Select(characteristic => new CharacteristicVm
            {
                Id = characteristic.IntID,
                CharacteristicName = characteristic.StrCharacteristicName,
                MeaTypeId = characteristic.IntMeaTypeID,
                ProcessId = characteristic.IntProcessID,
                CharacteristicType = characteristic.IntCharacteristicType,
                CharacteristicUnit = characteristic.StrCharacteristicUnit,
                DefectRateLimit = characteristic.IntDefectRateLimit,
                EventEnable = characteristic.IntEventEnable,
                EmailEventModel = characteristic.IntEmailEventModel,
                Decimals = characteristic.IntDecimals,
                Deleted = characteristic.BoolDeleted,
            });

            return Ok(characteristicVms);
        }


        /// <summary>
        /// Url: /api/characteristics/{Id}
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// 
        [HttpPut("{Id:int}")]
        public async Task<IActionResult> UpdateCharacteristic(int Id, CharacteristicVm characterisitcVm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (Id < 0 || Id != characterisitcVm.Id)
            {
                return BadRequest("Invalid characteristic ID.");
            }

            var characteristic = _context.Characteristices.FirstOrDefault(r => r.IntID == Id && r.BoolDeleted == false);

            if (characteristic == null)
            {
                return NotFound("Characteristic not found.");
            }

            characteristic.StrCharacteristicName = characterisitcVm.CharacteristicName;
            characteristic.IntMeaTypeID = characterisitcVm.MeaTypeId;
            characteristic.IntProcessID = characterisitcVm.ProcessId;
            characteristic.IntCharacteristicType = characterisitcVm.CharacteristicType;
            characteristic.StrCharacteristicUnit = characterisitcVm.CharacteristicUnit;
            characteristic.IntDefectRateLimit = characterisitcVm.DefectRateLimit;
            characteristic.IntEventEnable = characterisitcVm.EventEnable;
            characteristic.IntEmailEventModel = characterisitcVm.EmailEventModel;
            characteristic.IntDecimals = characterisitcVm.Decimals;

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return NoContent();
            }
            else
            {
                return BadRequest("Failed to update characteristic.");
            }    
        }

        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> DeleteCharacteristic(int Id)
        {
            var characteristic = _context.Characteristices.FirstOrDefault(r => r.IntID == Id && r.BoolDeleted == false);
            
            if (characteristic == null)
            {
                return NotFound("Characteristic not found.");
            }

            characteristic.BoolDeleted = true;
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return Ok(new CharacteristicVm { 
                    Id = characteristic.IntID,
                    CharacteristicName = characteristic.StrCharacteristicName,
                    MeaTypeId = characteristic.IntMeaTypeID,
                    ProcessId = characteristic.IntProcessID,
                    CharacteristicType = characteristic.IntCharacteristicType,
                    CharacteristicUnit = characteristic.StrCharacteristicUnit,
                    DefectRateLimit = characteristic.IntDefectRateLimit,
                    EventEnable = characteristic.IntEventEnable,
                    EmailEventModel = characteristic.IntEmailEventModel,
                    Decimals = characteristic.IntDecimals,
                    Deleted = characteristic.BoolDeleted,
                });
            }
            else
            {
                return BadRequest("Failed to delete characteristic.");
            }
        }
    }
}
