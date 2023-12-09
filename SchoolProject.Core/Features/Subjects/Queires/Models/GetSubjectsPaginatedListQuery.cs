using MediatR;
using SchoolProject.Core.Features.Subjects.Queires.ViewModels;
using SchoolProject.Core.Wrappings;
using SchoolProject.Data.Enums;

namespace SchoolProject.Core.Features.Subjects.Queires.Models
{
    public class GetSubjectsPaginatedListQuery : IRequest<PaginatedResult<SubjectsPaginatedListResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Search { get; set; }
        public SubjectsOrderingEnum OrderBy { get; set; }
    }
}
