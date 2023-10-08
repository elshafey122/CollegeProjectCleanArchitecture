using Microsoft.EntityFrameworkCore;

namespace SchoolProject.Core.Wrappings
{
    public static class QuerablePagiExtensions
    {
        public static async Task<PaginatedResult<T>> ToPaginateListAsync<T>(this IQueryable<T> source, int pagenumber, int pagesize)
            where T : class
        {
            if (source == null)
            {
                throw new Exception("Empty");
            }
            pagenumber = pagenumber <= 0 ? 1 : pagenumber;
            pagesize = pagesize == 0 ? 10 : pagesize;
            int count = await source.AsNoTracking().CountAsync();  // total number of records
            if (count == 0)
            {
                return PaginatedResult<T>.Success(new List<T>(), count, pagenumber, pagesize);
            }
            var items = await source.Skip((pagenumber - 1) * pagesize).Take(pagesize).ToListAsync();
            return PaginatedResult<T>.Success(items, count, pagenumber, pagesize);
        }
    }
}
