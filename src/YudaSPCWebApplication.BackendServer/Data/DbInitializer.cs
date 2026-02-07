using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Text;
using YudaSPCWebApplication.BackendServer.Data.Entities;

namespace YudaSPCWebApplication.BackendServer.Data
{
    public class DbInitializer(ApplicationDbContext context,
                         UserManager<User> userManager,
                         RoleManager<Role> roleManager)
    {
        private readonly ApplicationDbContext _context = context;
        private readonly UserManager<User> _userManager = userManager;
        private readonly RoleManager<Role> _roleManager = roleManager;

        private readonly Dictionary<int, string> _roles = new()
        {
            {1, "Administrators" },
            {2, "Managers" },
            {3, "Supervisors" },
            {4, "Technician" },
            {5, "Operators" },
            {6, "Guests" }
        };

        public async Task Seed()
        {
            // Ensure database is created
            _context.Database.EnsureCreated();

            #region Quyền
            // Seed Roles
            if (!_roleManager.Roles.Any())
            {
                foreach (var rolePair in _roles)
                {
                    var role = new Role
                    {
                        IntRoleID = rolePair.Key,
                        StrRoleName = rolePair.Value,
                        IntLevel = rolePair.Key,
                        StrDescription = rolePair.Value,
                        IntRoleUser = 0,

                        Id = Guid.NewGuid().ToString(),
                        Name = rolePair.Value,
                        NormalizedName = rolePair.Value.ToUpper(),
                        ConcurrencyStamp = Guid.NewGuid().ToString()
                    };
                    await _roleManager.CreateAsync(role);
                }

                await _context.SaveChangesAsync();
            }
            #endregion Quyền

            #region Người dùng
            // Seed Admin User
            if (!_userManager.Users.Any())
            {
                var adminUser = new User
                {
                    StrFullName = "admin",
                    StrEmailAddress = "admin@gmail.com",
                    StrRoleID = "1",
                    IntEnable = 1,
                    StrPassword = Convert.ToHexString(MD5.HashData(Encoding.UTF8.GetBytes("Admin@123"))),
                    
                    Id = Guid.NewGuid().ToString(),
                    UserName = "admin",
                    Email = "admin@gmail.com",
                    NormalizedUserName = "admin".ToUpper(),
                    NormalizedEmail = "admin@gmail.com".ToUpper(),
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                }; 
                await _userManager.CreateAsync(adminUser, "Admin@123");
                await _context.SaveChangesAsync();

            }
            #endregion Người dùng

            #region Job Decision
            if (!_context.JobDecisions.Any()) {
                await _context.JobDecisions.AddRangeAsync(
                    new JobDecision{ StrDecision = "Not yet decision", IntColorCode = 16777215 },
                    new JobDecision{ StrDecision = "Pass", IntColorCode = 33280 },
                    new JobDecision{ StrDecision = "Sorting", IntColorCode = 16776960 },
                    new JobDecision{ StrDecision = "Rework", IntColorCode = 16776960 },
                    new JobDecision{ StrDecision = "AOD", IntColorCode = 16776960 },
                    new JobDecision{ StrDecision = "Reject", IntColorCode = 16776960 }
                );
            }
            #endregion Job decision
        }
    }
}