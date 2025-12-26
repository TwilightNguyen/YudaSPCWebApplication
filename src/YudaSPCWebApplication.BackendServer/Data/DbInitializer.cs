using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using YudaSPCWebApplication.BackendServer.Data.Entities;

namespace YudaSPCWebApplication.BackendServer.Data
{
    public class DbInitializer
    {
        private readonly ApplicationDbContext _context;

        private readonly Dictionary<int, string> _roles = new()
        {
            {1, "Administrators" },
            {2, "Managers" },
            {3, "Supervisors" },
            {4, "Technician" },
            {5, "Operators" },
            {6, "Guests" }
        };

        public DbInitializer(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Seed()
        {
            // Ensure database is created
            _context.Database.EnsureCreated();

            #region Quyền
            // Seed Roles
            if (!_context.Roles.Any())
            {
                foreach (var rolePair in _roles)
                {
                    var role = new Role
                    {
                        IntRoleID = rolePair.Key,
                        StrRoleName = rolePair.Value,
                        IntLevel = rolePair.Key,
                        StrDescription = rolePair.Value,
                        IntRoleUser = 0
                    };
                    await _context.Roles.AddAsync(role);
                }

                await _context.SaveChangesAsync();
            }
            #endregion Quyền

            #region Người dùng
            // Seed Admin User
            if (!_context.Users.Any())
            {
                var adminUser = new User
                {
                    StrFullName = "admin",
                    StrEmailAddress = "admin@gmail.com",
                    StrRoleID = "1",
                    IntEnable = 1,
                    StrPassword = Convert.ToHexString(MD5.HashData(Encoding.UTF8.GetBytes("admin@123")))
                };
                await _context.Users.AddAsync(adminUser);
                await _context.SaveChangesAsync();
            }
            #endregion Người dùng
        }
    }
}