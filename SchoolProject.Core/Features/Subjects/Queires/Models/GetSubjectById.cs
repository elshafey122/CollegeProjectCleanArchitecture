using MediatR;
using SchoolProject.Core.Basics_Status;
using SchoolProject.Core.Features.Subjects.Queires.ViewModels;

namespace SchoolProject.Core.Features.Subjects.Queires.Models
{
    public class GetSubjectById : IRequest<Response<SubjectByIdResponse>>
    {
        public int SubjectId { get; set; }
    }
}
