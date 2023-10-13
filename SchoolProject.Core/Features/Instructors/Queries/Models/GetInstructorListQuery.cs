using MediatR;
using SchoolProject.Core.Basics_Status;
using SchoolProject.Core.Features.Instructors.Queries.ViewModels;
using SchoolProject.Core.Wrappings;

namespace SchoolProject.Core.Features.Instructors.Queries.Models
{
    public class GetInstructorListQuery : IRequest<Response<PaginatedResult<GetInstructorResponse>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
