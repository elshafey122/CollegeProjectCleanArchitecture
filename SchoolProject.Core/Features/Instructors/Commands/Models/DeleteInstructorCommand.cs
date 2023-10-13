using MediatR;
using SchoolProject.Core.Basics_Status;

namespace SchoolProject.Core.Features.Instructors.Commands.Models
{
    public class DeleteInstructorCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
