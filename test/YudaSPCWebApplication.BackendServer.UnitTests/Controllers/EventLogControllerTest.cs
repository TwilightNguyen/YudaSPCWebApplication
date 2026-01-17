using System;
using System.Collections.Generic;
using System.Text;
using YudaSPCWebApplication.BackendServer.Data;

namespace YudaSPCWebApplication.BackendServer.UnitTest.Controllers
{
    public class EventLogControllerTest
    {
        private readonly ApplicationDbContext _context;

        public EventLogControllerTest()
        {
            _context = InMemoryDbContext.GetApplicationDbContext();
            InMemoryDbContext.SeedRoles(_context);
            InMemoryDbContext.SeedUsers(_context);
        }

    }
}
