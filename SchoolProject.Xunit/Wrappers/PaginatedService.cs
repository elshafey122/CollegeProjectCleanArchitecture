using SchoolProject.Core.Wrappings;
using SchoolProject.Data.Entities;
namespace CollegeProject.XUnitTest.Wrappers
{
    public class PaginatedService : IPaginatedService<Student>
    {
        public async Task<PaginatedResult<Student>> ReturnPaginatedResult(IQueryable<Student> source, int pageNumber, int pageSize)
        {
            return await source.ToPaginateListAsync(pageNumber, pageSize);
        }
    }
}
