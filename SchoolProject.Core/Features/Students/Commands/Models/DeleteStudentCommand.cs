using MediatR;
using SchoolProject.Core.Basics_Status;

namespace SchoolProject.Core.Features.Students.Commands.Models
{
    public class DeleteStudentCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public DeleteStudentCommand(int id)
        {
            Id = id;
        }
    }
}
