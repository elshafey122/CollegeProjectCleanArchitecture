using MediatR;
using SchoolProject.Core.Basics_Status;
using SchoolProject.Core.Features.Departements.Queries.ViewModels;
using SchoolProject.Core.Wrappings;

namespace SchoolProject.Core.Features.Departements.Queries.Models
{
    public class GetDepartementListQuery : IRequest<Response<PaginatedResult<DepartementListResponse>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
