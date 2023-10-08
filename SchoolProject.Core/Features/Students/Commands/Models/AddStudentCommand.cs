using MediatR;
using SchoolProject.Core.Basics_Status;

namespace SchoolProject.Core.Features.Students.Commands.Models
{
    public class AddStudentCommand : IRequest<Response<string>>
    {
        public string? StuNamear { get; set; }
        public string? StuNameen { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public int DepartementId { get; set; }
    }
}
