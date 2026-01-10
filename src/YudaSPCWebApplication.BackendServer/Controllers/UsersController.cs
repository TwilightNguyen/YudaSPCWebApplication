using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using System.Security.Cryptography;
using System.Text;
using YudaSPCWebApplication.BackendServer.Data.Entities;
using YudaSPCWebApplication.ViewModels;
using YudaSPCWebApplication.ViewModels.System;
using static System.Net.Mime.MediaTypeNames;

namespace YudaSPCWebApplication.BackendServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "Bearer")]
    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public UsersController(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            using var _ = _userManager = userManager;
             _roleManager = roleManager;
        }


        /// <summary>
        ///  URL: /api/users
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(request.EmailAddress))
            {
                return BadRequest("Email cannot be empty.");
            }

            var userExists = await _userManager.FindByEmailAsync(request.EmailAddress);

            if (userExists == null)
            {
                return Conflict("Email already exists.");
            }


            var maxIntuserId = _userManager.Users
                .Max(r => (int?)r.IntUserID) ?? 0;

            // Use IdentityRole instead of Role for RoleManager
            var user = new User
            {
                StrFullName = request.FullName,
                StrEmailAddress = request.EmailAddress,
                StrRoleID = request.RoleID,
                IntEnable = request.Enable,
                StrPassword = Convert.ToHexString(MD5.HashData(Encoding.UTF8.GetBytes(request.Password))),

                Id = Guid.NewGuid().ToString(),
                UserName = request.FullName,
                Email = request.EmailAddress,
                NormalizedUserName = request.FullName.ToUpper(),
                NormalizedEmail = request.EmailAddress.ToUpper(),
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            return result.Succeeded ? Ok("User created successfully.") : BadRequest("User creation failed.");
        }

        /// <summary>
        /// Url: /api/users/
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();

            if (users == null) return NotFound("No users found.");

            var userVms = users.Select(user => new UserVm
            {
                EmailAddress = user.Email!,
                FullName = user.StrFullName??"",
                Department = user.StrDepartment!,
                StaffID = user.StrStaffID!,
                UserID = user.IntUserID,
                RoleID = user.StrRoleID,
                LastActivityTime = user.DtLastActivityTime,
                Enable = user.IntEnable,
                SelectedAreaID = user.StrSelectedAreaID
            }).ToList();

            return Ok(userVms);
        }

        /// <summary>
        /// Url: /api/users/?filter=serchString&pageIndex=1&pageSize=10
        /// </summary>
        [HttpGet("Pagging")]
        public async Task<IActionResult> GetUsersPaging(string? filter, int pageIndex, int pageSize)
        {
            var query = _userManager.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                query = query.Where(r => r.StrEmailAddress!.Contains(filter) || (r.StrFullName != null && r.StrFullName.Contains(filter)));
            }

            List<UserVm> items = [.. query.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(user => new UserVm
                {
                    EmailAddress = user.Email!,
                    Department = user.StrDepartment!,
                    FullName = user.StrFullName ?? string.Empty,
                    StaffID = user.StrStaffID!,
                    UserID = user.IntUserID,
                    RoleID = user.StrRoleID,
                    LastActivityTime = user.DtLastActivityTime,
                    Enable = user.IntEnable,
                    SelectedAreaID = user.StrSelectedAreaID
                })];

            var paginaton = new Pagination<UserVm>()
            {
                Items = items,
                TotalRecords = query.Count()
            };

            return Ok(paginaton);
        }

        /// <summary>
        /// Url: /api/users/{Id}
        /// </summary>
        /// 
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var user = _userManager.Users.FirstOrDefault(r => r.IntUserID == Id);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            var userVm = new UserVm
            {
                EmailAddress = user.Email!,
                FullName = user.StrFullName ?? string.Empty,
                Department = user.StrDepartment!,
                StaffID = user.StrStaffID!,
                UserID = user.IntUserID,
                RoleID = user.StrRoleID,
                LastActivityTime = user.DtLastActivityTime,
                Enable = user.IntEnable,
                SelectedAreaID = user.StrSelectedAreaID
            };
            return Ok(userVm);
        }

        /// <summary>
        /// Url: /api/users/{Id}
        /// </summary>
        /// 
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateUser(int Id, UserVm userVm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (Id < 0 || Id != userVm.UserID)
            {
                return BadRequest("User ID mismatch.");
            }

            var user = _userManager.Users.FirstOrDefault(r => r.IntUserID == Id);

            if (user == null)
            {
                return NotFound("User not found.");
            }
            
            user.StrRoleID = userVm.RoleID;
            user.StrFullName = userVm.FullName;
            user.StrEmailAddress = userVm.EmailAddress;
            user.IntEnable = userVm.Enable;
            user.StrDepartment = userVm.Department;
            user.StrStaffID = userVm.StaffID;
            user.StrSelectedAreaID = userVm.SelectedAreaID;

            user.UserName = userVm.FullName;
            user.NormalizedUserName = userVm.FullName.ToUpper();
            user.Email = userVm.EmailAddress;
            user.NormalizedEmail = userVm.EmailAddress.ToUpper();

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(result.Errors);
            } 
        }

        /// <summary>
        /// Url: /api/users/ChangePasswrod
        /// </summary>
        /// 
        [HttpPut("ChangePasswrod")]
        public async Task<IActionResult> ChangePassword([FromBody] UserChangePasswordRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
             
            var user = _userManager.Users.FirstOrDefault(r => r.IntUserID == request.UserID);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            
            if (result.Succeeded)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        /// <summary>
        /// Url: /api/users/ChangePasswrod
        /// </summary>
        /// 
        [HttpPut("AssignRole")]
        public async Task<IActionResult> AssignRole(int userId, int roleId)
        {

            if (userId < 0)
            {
                return BadRequest("User ID mismatch.");
            }

            if (roleId < 0)
            {
                return BadRequest("Role ID mismatch.");
            }

            var user = _userManager.Users.FirstOrDefault(r => r.IntUserID == userId);

            if (user == null)
            {
                return NotFound("User not found.");
            }


            var role = _roleManager.Roles.FirstOrDefault(r => r.IntLevel == roleId);
            
            if (role == null)
            {
                return NotFound("Role not found.");
            }

            int[] roleIds = user.StrRoleID != null
                ? [.. user.StrRoleID.Split([','], StringSplitOptions.RemoveEmptyEntries).Select(int.Parse)]
                : [];

            if (roleIds.Contains(role.IntRoleID))
            {
                return BadRequest("User already has this role.");
            }

            if(role.IntRoleUser == 0)
            {
                var maxLevel = _roleManager.Roles
                    .Where(r => r.IntRoleUser == 0)
                    .Max(r => (int?)r.IntLevel) ?? 0;

                if(role.IntLevel != maxLevel)
                {
                    var conflictingRoles = roleIds
                        .Select(rid => _roleManager.Roles.FirstOrDefault(r => r.IntRoleID == rid))
                        .Where(r => r != null && r.IntLevel < maxLevel && r.IntRoleUser == 0);  
                    
                    if (conflictingRoles.Any())
                        return BadRequest("Can't assign multiple roles to a single user.");
                }
            }

            _ = roleIds.Append(role.IntRoleID);

            user.StrRoleID = string.Join(",", roleIds);

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        /// <summary>
        /// Url: /api/users/ChangePasswrod
        /// </summary>
        /// 
        [HttpPut("UnassignRole")]
        public async Task<IActionResult> UnassignRole(int userId, int roleId)
        {
            if (userId < 0)
            {
                return BadRequest("User ID mismatch.");
            }

            if (roleId < 0)
            {
                return BadRequest("Role ID mismatch.");
            }

            var user = _userManager.Users.FirstOrDefault(r => r.IntUserID == userId);

            if (user == null)
            {
                return NotFound("User not found.");
            }


            var role = _roleManager.Roles.FirstOrDefault(r => r.IntLevel == roleId);

            if (role == null)
            {
                return NotFound("Role not found.");
            }

            int[] roleIds = user.StrRoleID != null
                ? [.. user.StrRoleID.Split([','], StringSplitOptions.RemoveEmptyEntries).Select(int.Parse)]
                : [];

            if (!roleIds.Contains(role.IntRoleID))
            {
                return BadRequest("User does not have this role.");
            }

            if (role.IntRoleUser == 0)
            {
                var maxLevel = _roleManager.Roles
                    .Where(r => r.IntRoleUser == 0)
                    .Max(r => (int?)r.IntLevel) ?? 0;

                 
                var GuestRole = _roleManager.Roles.FirstOrDefault(r => r != null && r.IntLevel == maxLevel && r.IntRoleUser == 0);
                _ = roleIds.Append(GuestRole!.IntRoleID);
            }

            _ = roleIds.Append(role.IntRoleID);

            user.StrRoleID = string.Join(",", roleIds);

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }


        /// <summary>
        /// Url: /api/users/{id}
        /// </summary>
        /// 
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteUser(int Id)
        {
            var user = _userManager.Users.FirstOrDefault(r => r.IntUserID == Id);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                var userVm = new UserVm
                {
                    EmailAddress = user.Email!,
                    FullName = user.StrFullName ?? string.Empty,
                    Department = user.StrDepartment!,
                    StaffID = user.StrStaffID!,
                    UserID = user.IntUserID,
                    RoleID = user.StrRoleID,
                    LastActivityTime = user.DtLastActivityTime,
                    Enable = user.IntEnable,
                    SelectedAreaID = user.StrSelectedAreaID
                };

                return Ok(userVm);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
    }
}
