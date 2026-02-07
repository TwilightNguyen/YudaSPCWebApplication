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

            #region Plan Type
            if (!_context.InspPlanTypes.Any())
            {
                await _context.InspPlanTypes.AddRangeAsync(new List<InspectionPlanType>
                {
                    new(){ IntID = 1, StrPlanTypeName = "FPI" },
                    new(){ IntID = 2, StrPlanTypeName = "IPQC" },
                    new(){ IntID = 3, StrPlanTypeName = "OQC" },
                });
                await _context.SaveChangesAsync();
            }
            #endregion Plan Type

            #region Job Decision
            if (!_context.JobDecisions.Any()) {
                await _context.JobDecisions.AddRangeAsync( new List<JobDecision> {
                    new() { IntID = 1, StrDecision = "Not yet decision", IntColorCode = 16777215 },
                    new() { IntID = 2, StrDecision = "Pass", IntColorCode = 33280 },
                    new() { IntID = 3, StrDecision = "Sorting", IntColorCode = 16776960 },
                    new() { IntID = 4, StrDecision = "Rework", IntColorCode = 16776960 },
                    new() { IntID = 5, StrDecision = "AOD", IntColorCode = 16776960 },
                    new() { IntID = 6, StrDecision = "Reject", IntColorCode = 16776960 },
                });

                await _context.SaveChangesAsync( ); 
            }
            #endregion Job decision
        }
    }
}