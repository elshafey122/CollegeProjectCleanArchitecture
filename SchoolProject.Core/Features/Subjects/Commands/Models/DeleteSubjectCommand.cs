using MediatR;
using SchoolProject.Core.Basics_Status;

namespace SchoolProject.Core.Features.Subjects.Commands.Models
{
    public class DeleteSubjectCommand : IRequest<Response<string>>
    {
        public int SubjectId { get; set; }
    }
}
