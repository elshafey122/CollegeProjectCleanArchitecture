using MediatR;
using SchoolProject.Core.Basics_Status;
using SchoolProject.Core.Features.Instructors.Queries.ViewModels;

namespace SchoolProject.Core.Features.Instructors.Queries.Models
{
    public class GetInstructorByIdQuery : IRequest<Response<GetInstructorResponse>>
    {
        public int Id { get; set; }
    }
}
