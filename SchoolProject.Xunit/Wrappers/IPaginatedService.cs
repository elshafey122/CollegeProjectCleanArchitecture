using SchoolProject.Core.Wrappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeProject.XUnitTest.Wrappers
{
    public interface IPaginatedService<T>
    {
        public Task<PaginatedResult<T>> ReturnPaginatedResult(IQueryable<T> source, int pageNumber, int pageSize);
    }
}
