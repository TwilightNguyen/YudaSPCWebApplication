using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YudaSPCWebApplication.BackendServer.Data.Entities;
using YudaSPCWebApplication.ViewModels;
using YudaSPCWebApplication.ViewModels.System;

namespace YudaSPCWebApplication.BackendServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<Role> _roleManager;

        public RolesController(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }


        /// <summary>
        ///  URL: /api/roles
        /// </summary>
        /// <param name="roleVm"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody]RoleVm roleVm)
        { 
            if (string.IsNullOrWhiteSpace(roleVm.Name))
            {
                return BadRequest("Role name cannot be empty.");
            }

            var roleExists = await _roleManager.RoleExistsAsync(roleVm.Name);
            if (roleExists)
            {
                return Conflict("Role already exists.");
            }
            // Use IdentityRole instead of Role for RoleManager
            var role = new Role
            {
                Id = Guid.NewGuid().ToString(),
                Name = roleVm.Name,
                NormalizedName = roleVm.Name.ToUpperInvariant(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),

                StrRoleName = roleVm.Name,
                StrDescription = roleVm.Description,
                IntRoleUser = roleVm.IntRoleUser,
                IntLevel = roleVm.IntLevel
            };

            var result = await _roleManager.CreateAsync(role);
            
            return result.Succeeded ? Ok("Role created successfully.") : BadRequest("Role creation failed.");
        }

        /// <summary>
        /// Url: /api/roles/
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();

            if (roles == null) return NotFound("No roles found.");

            var roleVms = roles.Select(role => new RoleVm
            {
                Name = role.StrRoleName ?? string.Empty,
                Description = role.StrDescription,
                IntRoleUser = role.IntRoleUser ?? -1,
                IntLevel = role.IntLevel,
                IntRoleID = role.IntRoleID
            }).ToList();

            return Ok(roleVms);
        }

        /// <summary>
        /// Url: /api/roles/?filter=serchString&pageIndex=1&pageSize=10
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetRolesPaging(string? filter,int pageIndex, int pageSize)
        {
            var query = _roleManager.Roles.AsQueryable();

            if(!string.IsNullOrWhiteSpace(filter))
            {
                query = query.Where(r => r.StrRoleName!.Contains(filter) || (r.StrDescription != null && r.StrDescription.Contains(filter)));
            }

            List<RoleVm> items = query.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(role => new RoleVm
                {
                    Name = role.StrRoleName ?? string.Empty,
                    Description = role.StrDescription,
                    IntRoleUser = role.IntRoleUser ?? -1,
                    IntLevel = role.IntLevel,
                    IntRoleID = role.IntRoleID
                }).ToList();

            var paginaton = new Pagination<RoleVm>()
            {
                Items = items,
                TotalRecords = query.Count()
            };

            return Ok(paginaton);
        }

        /// <summary>
        /// Url: /api/roles/{Id}
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// 
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var role = _roleManager.Roles.FirstOrDefault(r => r.IntRoleID == Id);

            if (role == null)
            {
                return NotFound("Role not found.");
            }

            var roleVm = new RoleVm
            {
                IntRoleID = role.IntRoleID,
                Name = role.StrRoleName ?? string.Empty,
                Description = role.StrDescription,
                IntRoleUser = role.IntRoleUser??-1,
                IntLevel = role.IntLevel
            };
            return Ok(roleVm);
        }


        /// <summary>
        /// Url: /api/roles/{Id}
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// 
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateRole(int Id, RoleVm roleVm)
        {
            if (Id < 0 || Id != roleVm.IntRoleID)
            {
                return BadRequest("Role ID mismatch.");
            }

            var role = _roleManager.Roles.FirstOrDefault(r => r.IntRoleID == Id);

            if (role == null)
            {
                return NotFound("Role not found.");
            }

            role.Name = roleVm.Name;
            role.NormalizedName = roleVm.Name.ToUpperInvariant();

            role.StrRoleName = roleVm.Name;
            role.StrDescription = roleVm.Description;
            role.IntRoleUser = roleVm.IntRoleUser;
            role.IntLevel = roleVm.IntLevel;

            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteRole(int Id)
        {
            var role = _roleManager.Roles.FirstOrDefault(r => r.IntRoleID == Id);
            if (role == null)
            {
                return NotFound("Role not found.");
            }
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                var roleVm = new RoleVm
                {
                    Name = role.StrRoleName ?? string.Empty,
                    Description = role.StrDescription,
                    IntRoleUser = role.IntRoleUser ?? -1,
                    IntLevel = role.IntLevel,
                    IntRoleID = role.IntRoleID
                };

                return Ok(roleVm);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
    }
}
