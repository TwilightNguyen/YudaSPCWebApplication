using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using YudaSPCWebApplication.BackendServer.Data;

namespace YudaSPCWebApplication.BackendServer.UnitTest
{
    public class InMemoryDbContextProductionArea
    {
        public ApplicationDbContext GetApplicationDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryApplicationDatabase")
                .Options;
            var dbContext = new ApplicationDbContext(options);
            return dbContext;
        }
    }
}
