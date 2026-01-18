using YudaSPCWebApplication.BackendServer.Data;

namespace YudaSPCWebApplication.BackendServer.UnitTest.Controllers
{
    public class CharacteristicControllerTest 
    {
        public ApplicationDbContext _context;

        public CharacteristicControllerTest()
        {
            _context = InMemoryDbContext.GetApplicationDbContext();
        }
    }
}
