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

        public static void SeedProcessLines(ApplicationDbContext context)
        {
            context.ProcessLines.AddRange(new List<ProcessLine>
            {
                new() { IntID = 1, StrProcessLineName = "Process A Line A", StrProcessLineCode = "A0A", IntProcessID = 1 },
                new() { IntID = 2, StrProcessLineName = "Process A Line B", StrProcessLineCode = "A0B", IntProcessID = 1 },
                new() { IntID = 3, StrProcessLineName = "Process B Line C", StrProcessLineCode = "B0C", IntProcessID = 2 }
            });
            context.SaveChanges();
        }

        public static void SeedEventLogs(ApplicationDbContext context)
        {
            context.EventLogs.AddRange(new List<EventLog>
            {
                new() { IntEventID = 1, DtEventTime = DateTime.UtcNow.AddHours(-2), StrEventCode = "E001", StrEvent = "User Admin logged in", StrStation = "192.168.1.12" },
                new() { IntEventID = 2, DtEventTime = DateTime.UtcNow.AddHours(-1), StrEventCode = "E002", StrEvent = "User Admin logged out", StrStation = "192.168.1.13" },
                new() { IntEventID = 3, DtEventTime = DateTime.UtcNow, StrEventCode = "E003", StrEvent = "Error occurred", StrStation = "192.168.1.12" }
            });
            context.SaveChanges();
        }

        public static void SeedMeasureTypes(ApplicationDbContext context)
        {
            context.MeasureTypes.AddRange(new List<MeasureType>
            {
                new() { IntID = 1, StrMeaType = "Length" },
                new() { IntID = 2, StrMeaType = "Weight" },
                new() { IntID = 3, StrMeaType = "Temperature" }
            });
            context.SaveChanges();
        }

        public static void SeedCharacteristics(ApplicationDbContext context)
        {
            context.Characteristics.AddRange(new List<Characteristic>
            {
                new() { IntID = 1, StrCharacteristicName = "Characteristic A", IntProcessID = 1, IntMeaTypeID = 1, IntCharacteristicType = 0, IntDecimals = 2, StrCharacteristicUnit = "G", IntDefectRateLimit = 10, IntEmailEventModel = 2, IntEventEnable = 1,BoolDeleted = false},
                new() { IntID = 2, StrCharacteristicName = "Characteristic B", IntProcessID = 1, IntMeaTypeID = 1, IntCharacteristicType = 0, IntDecimals = 2, StrCharacteristicUnit = "KG", IntDefectRateLimit = null, IntEmailEventModel = 0, IntEventEnable = 0,BoolDeleted = false},
                new() { IntID = 3, StrCharacteristicName = "Characteristic C", IntProcessID = 2, IntMeaTypeID = 1, IntCharacteristicType = 0, IntDecimals = 2, StrCharacteristicUnit = "ML", IntDefectRateLimit = null, IntEmailEventModel = 1, IntEventEnable = 1,BoolDeleted = false},
            });
            context.SaveChanges();
        }

        public static void SeedInspectionPlans(ApplicationDbContext context)
        {
            context.InspectionPlans.AddRange(new List<InspectionPlan>
            {
                new() { IntID = 1, StrInspPlanName = "Inspection Plan A", IntAreaID = 1, BoolDeleted = false },
                new() { IntID = 2, StrInspPlanName = "Inspection Plan B", IntAreaID = 1, BoolDeleted = false },
                new() { IntID = 3, StrInspPlanName = "Inspection Plan C", IntAreaID = 2,  BoolDeleted = false }
            });
            context.SaveChanges();
        }

        public static void SeedInspPlanTypes(ApplicationDbContext context)
        {
            context.InspPlanTypes.AddRange(new List<InspectionPlanType>
            {
                new() { IntID = -1, StrPlanTypeName = "[ None ]" },
                new() { IntID = 1, StrPlanTypeName = "FPI" },
                new() { IntID = 2, StrPlanTypeName = "IPQC" },
                new() { IntID = 3, StrPlanTypeName = "OQC" }
            });
            context.SaveChanges();
        }

        public static void SeedInspectionPlanSubs(ApplicationDbContext context)
        {
            context.InspectionPlanSubs.AddRange(new List<InspectionPlanSub>
            {
                new() { IntID = 1, IntInspPlanID = 1, IntPlanTypeID = 1, DtCreateTime = DateTime.UtcNow, BoolDeleted = false },
                new() { IntID = 2, IntInspPlanID = 1, IntPlanTypeID = 2, DtCreateTime = DateTime.UtcNow, BoolDeleted = false },
                new() { IntID = 3, IntInspPlanID = 2, IntPlanTypeID = 1, DtCreateTime = DateTime.UtcNow, BoolDeleted = false }
            });
            context.SaveChanges();
        }

        public static void SeedInspectionPlanDatas(ApplicationDbContext context)
        {
            context.InspectionPlanDatas.AddRange(new List<InspectionPlanData>
            {
                new() { IntID = 1, IntInspPlanSubID = 1, IntCharacteristicID = 1, FtLSL = 10, FtUSL = 20, FtLCL = 12, FtUCL = 18, BoolSPCChart = true, BoolDataEntry = true, IntPlanState = 1, FtCpkMax = 1.33, FtCpkMin = 1.00, BoolSpkControl = true, StrSampleSize = "5", FtPercentControlLimit = 95, BoolDeleted = false },
                new() { IntID = 2, IntInspPlanSubID = 1, IntCharacteristicID = 2, FtLSL = 5, FtUSL = 15, FtLCL = 7, FtUCL = 13, BoolSPCChart = false, BoolDataEntry = true, IntPlanState = 1, FtCpkMax = 1.50, FtCpkMin = 1.20, BoolSpkControl = false, StrSampleSize = "3", FtPercentControlLimit = 90, BoolDeleted = false },
                new() { IntID = 3, IntInspPlanSubID = 2, IntCharacteristicID = 3, FtLSL = 100, FtUSL = 200, FtLCL = 120, FtUCL = 180, BoolSPCChart = true, BoolDataEntry = false, IntPlanState = 2, FtCpkMax = 1.25, FtCpkMin = 1.10, BoolSpkControl = true, StrSampleSize = "4", FtPercentControlLimit = 92, BoolDeleted = false }
            });
            context.SaveChanges();
        }
    }
}
