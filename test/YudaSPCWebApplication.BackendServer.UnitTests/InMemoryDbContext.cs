using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using YudaSPCWebApplication.BackendServer.Data;
using YudaSPCWebApplication.BackendServer.Data.Entities;

namespace YudaSPCWebApplication.BackendServer.UnitTest
{
    public static class InMemoryDbContext
    {
        public static ApplicationDbContext GetApplicationDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var dbContext = new ApplicationDbContext(options);
            return dbContext;
        }

        public static void DisposeDbContext(ApplicationDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        public static void SeedProductionAreas(ApplicationDbContext context)
        {
            context.ProductionAreas.AddRange(new List<ProductionArea>
            {
                new() { IntID = 1, StrNameArea = "Tape" },
                new() { IntID = 2, StrNameArea = "Layout" },
                new() { IntID = 3, StrNameArea = "Block Vial" }
            }); 
            context.SaveChanges();
        }

        public static void SeedRoles(ApplicationDbContext context)
        {
            context.Roles.AddRange(new List<Role>
            {
                new() { Id = Guid.NewGuid().ToString(), Name = "Admin", IntRoleID = 1, StrRoleName = "Admin", StrDescription = "Administrator Role", IntLevel = 1, IntRoleUser = 5 },
                new() { Id = Guid.NewGuid().ToString(), Name = "User",  IntRoleID = 2, StrRoleName = "User", StrDescription = "User Role", IntLevel = 2, IntRoleUser = 10 },
                new() { Id = Guid.NewGuid().ToString(), Name = "Manager",  IntRoleID = 3, StrRoleName = "Manager", StrDescription = "Manager Role", IntLevel = 3, IntRoleUser = 3 },
                new() { Id = Guid.NewGuid().ToString(), Name = "Assistant",  IntRoleID = 4, StrRoleName = "Assistant", StrDescription = "Assistant Role", IntLevel = 4, IntRoleUser = 7 },
                new() { Id = Guid.NewGuid().ToString(), Name = "Supervisor",  IntRoleID = 5, StrRoleName = "Supervisor", StrDescription = "Supervisor Role", IntLevel = 5, IntRoleUser = 2 },
            });
            context.SaveChanges();
        }

        public static void SeedUsers(ApplicationDbContext context)
        {
            context.Users.AddRange(new List<User>
            {
                new() {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    NormalizedEmail = "ADMIN@GMAIL.COM",
                    Email = "admin@gmal.com",
                    StrFullName = "System Administrator",
                    IntUserID = 1,
                    IntEnable = 1,
                    StrRoleID = "1",
                    StrPassword = "Admin@123",
                    StrDepartment = "IT",
                    StrEmailAddress = "admin@gmal.com",
                    StrSelectedAreaID = "1",
                    StrStaffID = "A001",
                    DtLastActivityTime = DateTime.UtcNow
                },
                new() {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "super",
                    NormalizedUserName = "SUPER",
                    NormalizedEmail = "SUPER@GMAIL.COM",
                    Email = "super@gmal.com",
                    StrFullName = "System Supervisor",
                    IntUserID = 2,
                    IntEnable = 1,
                    StrRoleID = "2",
                    StrPassword = "Super@123",
                    StrDepartment = "IT",
                    StrEmailAddress = "super@gmal.com",
                    StrSelectedAreaID = "1",
                    StrStaffID = "S001",
                    DtLastActivityTime = DateTime.UtcNow
                },
                new() {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "manager",
                    NormalizedUserName = "MANAGER",
                    NormalizedEmail = "MANAGER@GMAIL.COM",
                    Email = "manager@gmal.com",
                    StrFullName = "System Manager",
                    IntUserID = 3,
                    IntEnable = 1,
                    StrRoleID = "3",
                    StrPassword = "Manager@123",
                    StrDepartment = "IT",
                    StrEmailAddress = "manager@gmal.com",
                    StrSelectedAreaID = "1",
                    StrStaffID = "M001",
                    DtLastActivityTime = DateTime.UtcNow
                },

                new() {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "technician",
                    NormalizedUserName = "TECHNICIAN",
                    NormalizedEmail = "TECHNICIAN@GMAIL.COM",
                    Email = "technician@gmal.com",
                    StrFullName = "System Technician",
                    IntUserID = 4,
                    IntEnable = 1,
                    StrRoleID = "4",
                    StrPassword = "Technician@123",
                    StrDepartment = "IT",
                    StrEmailAddress = "technician@gmal.com",
                    StrSelectedAreaID = "1",
                    StrStaffID = "T001",
                    DtLastActivityTime = DateTime.UtcNow
                },
                new() {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "operator",
                    NormalizedUserName = "OPERATOR",
                    NormalizedEmail = "OPERATOR@GMAIL.COM",
                    Email = "operator@gmal.com",
                    StrFullName = "System Operator",
                    IntUserID = 5,
                    IntEnable = 1,
                    StrRoleID = "5",
                    StrPassword = "Operator@123",
                    StrDepartment = "IT",
                    StrEmailAddress = "operator@gmal.com",
                    StrSelectedAreaID = "1",
                    StrStaffID = "O001",
                    DtLastActivityTime = DateTime.UtcNow
                },
                new() {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "guest",
                    NormalizedUserName = "GUEST",
                    NormalizedEmail = "GUEST@GMAIL.COM",
                    Email = "guest@gmal.com",
                    StrFullName = "System Guest",
                    IntUserID = 6,
                    IntEnable = 1,
                    StrRoleID = "6",
                    StrPassword = "Guest@123",
                    StrDepartment = "IT",
                    StrEmailAddress = "guest@gmal.com",
                    StrSelectedAreaID = "1",
                    StrStaffID = "G001",
                    DtLastActivityTime = DateTime.UtcNow
                },
            });
            context.SaveChanges();
        }

        public static void SeedProcesses(ApplicationDbContext context)
        {
            context.Processes.AddRange(new List<Process>
            {
                new() { IntID = 1, StrProcessName = "Process A", IntAreaID = 1 },
                new() { IntID = 2, StrProcessName = "Process B", IntAreaID = 1 },
                new() { IntID = 3, StrProcessName = "Process C", IntAreaID = 2 }
            });
            context.SaveChanges();
        }
    }
}
