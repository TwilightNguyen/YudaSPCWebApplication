using YudaSPCWebApplication.BackendServer.UnitTests.Helpers;

namespace YudaSPCWebApplication.BackendServer.UnitTests.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> AsAsyncQueryable<T>(this IEnumerable<T> input)
        {
            return new NotInDbSet<T>(input);
        }
    }
}